Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Inasistencias

    ' Devuelve Legajo, Nombre, Instituto y Dia para cada día hábil sin movimiento,
    ' respetando feriados nacionales (Zona='Todas') y provinciales según la zona del Instituto.
    Public Function BuscarFaltas(desde As Date, hasta As Date, sucursal As String) As DataTable
        Dim whereSucursal As String = If(String.IsNullOrWhiteSpace(sucursal), "", " AND e.Instituto = @pInstituto ")

        Dim sql As String = $"
            SET NOCOUNT ON;
            SET DATEFIRST 1;         -- lunes = 1
            SET DATEFORMAT dmy;      -- interpreta 'dd/mm/yyyy'

            DECLARE @desde date = @pDesde, @hasta date = @pHasta;
            IF (@hasta < @desde) BEGIN DECLARE @t date = @desde; SET @desde=@hasta; SET @hasta=@t; END;

            IF OBJECT_ID('tempdb..#dias_habiles') IS NOT NULL DROP TABLE #dias_habiles;

            ;WITH dias AS (
                SELECT d = @desde
                UNION ALL SELECT DATEADD(day,1,d) FROM dias WHERE d < @hasta
            )
            SELECT d AS Dia
            INTO #dias_habiles
            FROM dias
            -- Sólo días de lunes a viernes (feriados se filtran por empleado más abajo)
            WHERE DATEPART(WEEKDAY, d) BETWEEN 1 AND 5
            OPTION (MAXRECURSION 0);

            SELECT 
                e.Legajo,
                e.Nombre,
                e.Instituto,
                dh.Dia
            FROM dbo.Agentes e
            INNER JOIN dbo.Institutos i
                ON i.Descripcion = e.Instituto
            CROSS JOIN #dias_habiles dh
            -- calcula fecha de Ingreso si el string es convertible a date
            CROSS APPLY (
                SELECT CASE WHEN ISDATE(e.Ingreso) = 1 THEN CAST(e.Ingreso AS date) END AS IngresoDate
            ) AS x
            WHERE e.Baja IS NULL {whereSucursal}
              AND e.nomarca = 0
              -- omite días anteriores al ingreso cuando Ingreso es válido
              AND (x.IngresoDate IS NULL OR dh.Dia >= x.IngresoDate)
              -- NO contar si hay movimiento ese día
              AND NOT EXISTS(
                    SELECT 1
                    FROM dbo.Movimiento m
                    WHERE m.Legajo = e.Legajo
                      AND CONVERT(date, m.Dia) = dh.Dia
              )
              -- NO contar si es feriado aplicable al empleado:
              -- aplica si el feriado es nacional (Zona='Todas') o si coincide con la zona del instituto
              AND NOT EXISTS(
                    SELECT 1
                    FROM dbo.Feriados f
                    WHERE CONVERT(date, f.Dia) = dh.Dia
                      AND (f.Zona = 'Todas' OR f.Zona = i.Zona)
              )
            ORDER BY dh.Dia, e.Legajo;
        "

        Dim parametros = CmdParams("@pDesde", desde.Date, "@pHasta", hasta.Date, "@pInstituto", sucursal)
        Dim dt = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
        Return dt
    End Function


    ' Inserta 1 registro si no existe ya para ese legajo/día
    Public Function InsertarInasistencia(legajo As Integer, dia As Date) As Boolean
        Dim sql As String = "
            SET NOCOUNT ON;
            IF NOT EXISTS(
                SELECT 1 FROM dbo.Movimiento 
                WHERE Legajo=@pLegajo AND CONVERT(date, Dia)=@pDia
            )
            BEGIN
                INSERT INTO dbo.Movimiento
                    (Legajo, Dia, Entro, Salio, HsCumplidas, MotivoInasistencia, Comentario, SinFicha, Autorizo, NoPromedia, Nopromedianada)
                VALUES
                    (@pLegajo, @pDia, @hora, @hora, '00:00:00', 'Ausente', NULL, 1, NULL, 0, 0);
            END
        "

        Try
            Dim hora As Date = dia & " 12:00:00"
            DSM.Execute(DSM.Personal, sql, CmdParams("@pLegajo", legajo, "@pDia", dia, "@hora", hora))
            Return True
        Catch ex As Exception
            MessageBox.Show("Error al crear inasistencia: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

End Module
