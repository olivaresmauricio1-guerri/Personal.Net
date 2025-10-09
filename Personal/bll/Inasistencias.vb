Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Inasistencias

    ' Devuelve Legajo, Nombre, Dia para cada día hábil sin movimiento
    Public Function BuscarFaltas(desde As Date, hasta As Date, sucursal As String) As DataTable
        Dim whereSucursal As String = If(String.IsNullOrWhiteSpace(sucursal), "", " AND e.Instituto = @pInstituto ")

        Dim sql As String = $"
            SET NOCOUNT ON;
            SET DATEFIRST 1;         -- lunes = 1
            SET DATEFORMAT dmy;      -- interpreta 'dd/mm/yyyy' correctamente

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
            WHERE DATEPART(WEEKDAY, d) BETWEEN 1 AND 5
              AND NOT EXISTS(SELECT 1 FROM dbo.Feriados f WHERE f.Dia = d)
            OPTION (MAXRECURSION 0);

            SELECT 
                e.Legajo,
                e.Nombre,
                e.Instituto,
                dh.Dia
            FROM dbo.Agentes e
            CROSS JOIN #dias_habiles dh
            -- calcula fecha de Ingreso si el string es convertible a date
            CROSS APPLY (
                SELECT CASE WHEN ISDATE(e.Ingreso) = 1 THEN CAST(e.Ingreso AS date) END AS IngresoDate
            ) AS x
            WHERE e.Baja IS NULL {whereSucursal}
              AND e.nomarca = 0
              -- omite días anteriores al ingreso cuando Ingreso es válido
              AND (x.IngresoDate IS NULL OR dh.Dia >= x.IngresoDate)
              AND NOT EXISTS(
                    SELECT 1
                    FROM dbo.Movimiento m
                    WHERE m.Legajo = e.Legajo
                      AND CONVERT(date, m.Dia) = dh.Dia
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


    ' detectar inasistencias: por cada empleado activo, para cada dia habil (lunes a viernes, sin feriados) dentro del periodo desde / hasta,
    ' si no tiene movimientos en la tabla de movimientos, crear un registro con MotivoInasistencia = 'Ausente'
    Public Function DetectarInasistencias(desde As Date, hasta As Date) As Boolean
        Dim sql = "
            SET XACT_ABORT ON;
            SET NOCOUNT ON;

            BEGIN TRY
                BEGIN TRAN;
                SET DATEFIRST 1; -- Lunes = 1

                DECLARE @desde date = @pDesde;
                DECLARE @hasta date = @pHasta;

                -- Normaliza por si vienen invertidos
                IF (@hasta < @desde)
                BEGIN
                    DECLARE @tmp date = @desde;
                    SET @desde = @hasta;
                    SET @hasta = @tmp;
                END

                IF OBJECT_ID('tempdb..#dias_habiles') IS NOT NULL DROP TABLE #dias_habiles;

                ;WITH dias AS (
                    SELECT d = @desde
                    UNION ALL
                    SELECT DATEADD(day, 1, d)
                    FROM dias
                    WHERE d < @hasta   -- INCLUYE ambas puntas: desde..hasta
                )
                SELECT d AS Dia
                INTO #dias_habiles
                FROM dias
                WHERE DATEPART(WEEKDAY, d) BETWEEN 1 AND 5         -- Lunes a Viernes
                  AND NOT EXISTS (
                        SELECT 1
                        FROM dbo.Feriados f
                        WHERE f.Fecha = d AND f.Activo = 1
                  )
                OPTION (MAXRECURSION 0);

                INSERT INTO dbo.Movimiento
                    (Legajo, Dia, Entro, Salio, HsCumplidas, MotivoInasistencia, Comentario,
                     SinFicha, Autorizo, NoPromedia, Nopromedianada)
                SELECT
                    e.Legajo,
                    dh.Dia,
                    NULL, NULL, NULL,
                    'Ausente',
                    NULL,
                    0,
                    NULL,
                    0,
                    0
                FROM dbo.Empleados e
                CROSS JOIN #dias_habiles dh
                WHERE e.Activo = 1
                  AND NOT EXISTS (
                        SELECT 1
                        FROM dbo.Movimiento m
                        WHERE m.Legajo = e.Legajo
                          AND CONVERT(date, m.Dia) = dh.Dia
                  );

                COMMIT;
            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0 ROLLBACK;
                THROW;
            END CATCH
        "

        Try
            Dim parametros = CmdParams("@pDesde", desde.Date, "@pHasta", hasta.Date)
            DSM.Execute(DSM.Personal, sql, parametros)
            Return True
        Catch ex As Exception
            Debug.WriteLine("Error al detectar inasistencias: " & ex.Message)
            Return False
        End Try
    End Function


End Module
