
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Reportes

    Public Function ObtenerTotalesAgentes() As Dictionary(Of String, Integer)
        Dim r As New Dictionary(Of String, Integer) From {
            {"TotalAgentes", 0},
            {"TotalAgentesQueMarcan", 0},
            {"AgentesHoy", 0},
            {"AgentesSinMarcar", 0},
            {"Inasistencias", 0},
            {"CumpleMes", 0},
            {"Ingresaron_4a6m", 0}
        }

        Dim sql As String = "
            DECLARE @hoy   date = CAST(GETDATE() AS date);
            DECLARE @maniana date = DATEADD(day, 1, @hoy);
            DECLARE @d180  date = DATEADD(day, -180, @hoy);
            DECLARE @d135  date = DATEADD(day, -135, @hoy);

            SELECT
                -- Total de agentes activos que deben marcar
                SUM(f.Activo * f.DebeMarcar)                                      AS TotalAgentesQueMarcan,
                SUM(f.Activo)                                                     AS TotalAgentes,
                -- De esos, los que marcaron hoy y no estan de vacaciones o con inasistencia
                SUM(f.Activo * f.DebeMarcar * ISNULL(h.HasHoy,0))                 AS AgentesHoy,
                -- De esos, los que marcaron hoy y estan de vacaciones o con inasistencia
                SUM(f.Activo * f.DebeMarcar * ISNULL(i.Inasistencias,0))          AS Inasistencias,
                -- Los que no marcaron hoy
                SUM(f.Activo * f.DebeMarcar * (1-ISNULL(h.HasHoy,0)))             AS AgentesSinMarcar,
                -- Agentes activos que cumplen años este mes
                SUM(f.Activo * f.CumpleMes)                                       AS CumpleMes,
                -- Agentes activos con fecha de ingreso entre -180 y -135 días
                SUM(f.Activo * f.Ingreso4a6)                                      AS Ingresaron_4a6m
            FROM Agentes a
            CROSS APPLY (
                SELECT
                    -- Activo: Baja NULL o cadena vacía y no 'Eventual'
                    CASE WHEN (a.Baja IS NULL OR a.Baja = '') 
                              AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
                         THEN 1 ELSE 0 END AS Activo,
                    -- Debe marcar: Nomarca = 0 (tratando NULL como 0)
                    CASE WHEN ISNULL(a.Nomarca,0) = 0 THEN 1 ELSE 0 END AS DebeMarcar,
                    -- Cumpleaños en el mes actual
                    CASE WHEN a.Nacimiento IS NOT NULL
                              AND MONTH(a.Nacimiento) = MONTH(@hoy)
                         THEN 1 ELSE 0 END AS CumpleMes,
                    -- Ingreso entre hace 180 y 135 días (inclusive)
                    CASE WHEN a.Ingreso IS NOT NULL
                              AND a.Ingreso LIKE '[0-3][0-9]/[01][0-9]/[12][0-9][0-9][0-9]'
                              AND ISDATE(a.Ingreso) = 1
                              AND CONVERT(date, a.Ingreso, 103) >= @d180
                              AND CONVERT(date, a.Ingreso, 103) <= @d135
                         THEN 1 ELSE 0 END AS Ingreso4a6
            ) f
            OUTER APPLY (
                SELECT TOP 1 1 AS HasHoy
                FROM Movimiento m
                WHERE m.Legajo = a.Legajo AND m.MotivoInasistencia IS NULL
                  AND m.Dia >= @hoy AND m.Dia < @maniana
            ) h
            OUTER APPLY (
                SELECT TOP 1 1 AS Inasistencias
                FROM Movimiento m
                WHERE m.Legajo = a.Legajo AND m.MotivoInasistencia IS NOT NULL
                  AND m.Dia >= @hoy AND m.Dia < @maniana
            ) i;
        "

        Try
            Using dt = DSM.ExecuteQuery(DSM.Personal, sql)
                If dt.Rows.Count > 0 Then
                    r("TotalAgentes") = CInt(dt.Rows(0)("TotalAgentes"))
                    r("TotalAgentesQueMarcan") = CInt(dt.Rows(0)("TotalAgentesQueMarcan"))
                    r("AgentesHoy") = CInt(dt.Rows(0)("AgentesHoy"))
                    r("AgentesSinMarcar") = CInt(dt.Rows(0)("AgentesSinMarcar"))
                    r("Inasistencias") = CInt(dt.Rows(0)("Inasistencias"))
                    r("CumpleMes") = CInt(dt.Rows(0)("CumpleMes"))
                    r("Ingresaron_4a6m") = CInt(dt.Rows(0)("Ingresaron_4a6m"))
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al obtener totales: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return r
    End Function


    ' por cada agente, si debe marcar o no, consolea si hoy lo hizo o no
    Public Sub DebugDebeMarcar()
        Dim sql As String = "
            DECLARE @hoy date     = CAST(GETDATE() AS date);
            DECLARE @maniana date = DATEADD(day, 1, @hoy);
            SELECT
                a.Legajo,
                a.Nombre,
                -- Activo: Baja NULL o cadena vacía (si fuera texto)
                CASE WHEN a.Baja IS NULL OR LTRIM(RTRIM(CAST(a.Baja AS nvarchar(50)))) = '' THEN 1 ELSE 0 END AS Activo,
                CASE WHEN ISNULL(a.Nomarca,0) = 0 THEN 1 ELSE 0 END AS DebeMarcar,
                -- Tiene al menos un movimiento hoy
                CASE WHEN EXISTS (
                    SELECT 1
                    FROM Movimiento m
                    WHERE m.Legajo = a.Legajo
                      AND m.Dia >= @hoy AND m.Dia < @maniana
                ) THEN 1 ELSE 0 END AS HaMarcadoHoy
            FROM Agentes a
            ORDER BY a.Legajo;
            "
        Try
            Using dt = DSM.ExecuteQuery(DSM.Personal, sql)
                For Each row As DataRow In dt.Rows
                    Dim legajo = CInt(row("Legajo"))
                    Dim nombre = CStr(row("Nombre"))
                    Dim activo = CInt(row("Activo")) = 1
                    Dim debeMarcar = CInt(row("DebeMarcar")) = 1
                    Dim haMarcadoHoy = CInt(row("HaMarcadoHoy")) = 1
                    Debug.WriteLine($"Legajo {legajo} - {nombre}: Activo={activo}, DebeMarcar={debeMarcar}, HaMarcadoHoy={haMarcadoHoy}")
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al obtener totales: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Public Sub ListadoHorario(desde As Date, hasta As Date, idSucursal As Integer, txtSucursal As String, soloTarde As Boolean)
        Try
            DSM.Execute(DSM.Personal, "DELETE FROM ListaHorario")

            Dim tardeWhere As String = If(soloTarde,
            "AND CONVERT(time, Movimiento.Entro) >= '08:00:00' AND CONVERT(time, Movimiento.Entro) < '09:30:00'",
            "")

            Dim sql As String = $"
                INSERT INTO ListaHorario (Legajo, Nombre, Dia, Entro, Salio, HsCumplidas, Instituto, Motivo, CUIL, NroDto, TipoDto)
                SELECT 
                    Agentes.Legajo,
                    Agentes.Nombre,
                    Movimiento.Dia,
                    CONVERT(time(0), Movimiento.Entro)  AS Entro,
                    CONVERT(time(0), Movimiento.Salio)  AS Salio,
                    Movimiento.HsCumplidas,
                    Agentes.Instituto,
                    Movimiento.MotivoInasistencia       AS Motivo,
                    Agentes.CUIL,
                    Agentes.NroDto,
                    Agentes.TipoDto
                FROM Agentes
                INNER JOIN Movimiento ON Agentes.Legajo = Movimiento.Legajo
                WHERE Movimiento.Dia >= @desde
                  AND Movimiento.Dia < DATEADD(day, 1, @hasta)
                  {tardeWhere}
                  AND (@sucursal IS NULL OR @sucursal = '(Todas)' OR Agentes.Instituto = @sucursal)
                GROUP BY
                    Agentes.Legajo, Agentes.Nombre,
                    Movimiento.Dia,
                    CONVERT(time(0), Movimiento.Entro),
                    CONVERT(time(0), Movimiento.Salio),
                    Movimiento.HsCumplidas,
                    Agentes.Instituto, Movimiento.MotivoInasistencia,
                    Agentes.CUIL, Agentes.NroDto, Agentes.TipoDto;"

            Dim parametros = CmdParams(
                "@desde", desde,
                "@hasta", hasta,
                "@sucursal", If(txtSucursal = "(Todas)", DBNull.Value, txtSucursal)
            )

            DSM.Execute(DSM.Personal, sql, parametros)

        Catch ex As Exception
            MessageBox.Show("Error al calcular listado horario: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Public Sub ListadoMensualPorSucursal(desde As Date, hasta As Date, idSucursal As Integer, txtSucursal As String, soloNegativos As Boolean)

        Dim sqlListadoMensual As String = $"
-- 1) Limpiar
DELETE FROM ListadoMensual

;WITH base AS (
  SELECT
    A.Legajo,
    A.Instituto,
    A.Nombre,
    M.NoPromedia,
    A.Oficina,
    A.Critico,
    A.MayorDedicacion,
    A.HorasDedicacion,
    M.Dia,
    A.Comentario,
    M.HsCumplidas,
    M.MotivoInasistencia,
    A.HorasSemanales,
    A.HorasDiarias,
    -- Pasar HH:MM o HH:MM:SS a minutos SIN TRY_CONVERT/TYPE time
    CASE
      WHEN M.HsCumplidas IS NOT NULL AND LTRIM(RTRIM(M.HsCumplidas)) <> '' THEN
        CASE
          WHEN LEN(M.HsCumplidas) = 5
               AND ISDATE('19000101 ' + M.HsCumplidas + ':00') = 1
            THEN DATEDIFF(minute, '19000101',
                 CONVERT(datetime, '19000101 ' + M.HsCumplidas + ':00', 108))
          WHEN ISDATE('19000101 ' + M.HsCumplidas) = 1
            THEN DATEDIFF(minute, '19000101',
                 CONVERT(datetime, '19000101 ' + M.HsCumplidas, 108))
          ELSE 0
        END
      ELSE 0
    END AS MinCumplidas
  FROM Agentes A
  INNER JOIN Movimiento M ON M.Legajo = A.Legajo
  WHERE M.Dia >= @p_desde AND M.Dia <= @p_hasta
    AND (@p_sucursal IS NULL OR @p_sucursal = '(Todas)' OR A.Instituto = @p_sucursal)
),
dias_validos AS (
  -- no hoy, sin inasistencia, NoPromedia=0
  SELECT DISTINCT
    Legajo,
    DATEDIFF(day, 0, Dia) AS DiaKey
  FROM base
  WHERE MotivoInasistencia IS NULL
    AND DATEDIFF(day, 0, Dia) <> DATEDIFF(day, 0, GETDATE())
    AND ISNULL(NoPromedia, 0) = 0
),
dias_x_legajo AS (
  SELECT Legajo, COUNT(*) AS DiasTrabajados
  FROM dias_validos
  GROUP BY Legajo
),
totales AS (
  SELECT
    b.Legajo,
    MIN(b.Instituto)        AS Instituto,
    MIN(b.Nombre)           AS Nombre,
    MIN(b.Oficina)          AS Oficina,
    MAX(CASE WHEN b.Critico = 1 THEN 1 ELSE 0 END) AS Critico,
    MAX(CASE WHEN b.MayorDedicacion = 1 THEN 1 ELSE 0 END) AS MayorDedicacion,
    MIN(b.HorasDedicacion)  AS HorasDedicacion,
    MIN(b.Comentario)       AS Comentario,
    MIN(b.HorasSemanales)   AS HsSemanalesTxt,   -- nvarchar
    MIN(b.HorasDiarias)     AS HorasDiariasTxt,  -- nvarchar
    SUM(b.MinCumplidas)     AS TotalMin
  FROM base b
  GROUP BY b.Legajo
),
calc AS (
  SELECT
    t.Legajo,
    t.Instituto,
    t.Nombre,
    t.Oficina,
    CAST(t.Critico AS bit)         AS Critico,
    CAST(t.MayorDedicacion AS bit) AS MayorDedicacion,
    t.HorasDedicacion,
    t.Comentario,
    ISNULL(t.HsSemanalesTxt, N'0')  AS HsSemanales,
    ISNULL(t.HorasDiariasTxt, N'0') AS HorasDiarias,
    ISNULL(d.DiasTrabajados, 0)     AS DiasTrabajados,
    t.TotalMin,
    CASE WHEN ISNULL(d.DiasTrabajados, 0) > 0
         THEN t.TotalMin / d.DiasTrabajados
         ELSE 0 END                 AS PromMin,
    -- HorasDiarias a minutos usando ISNUMERIC y REPLACE
    CAST(ROUND(
      ISNULL(
        CASE WHEN ISNUMERIC(REPLACE(ISNULL(t.HorasDiariasTxt, '0'), ',', '.')) = 1
             THEN CAST(REPLACE(ISNULL(t.HorasDiariasTxt, '0'), ',', '.') AS float)
             ELSE 0 END * 60.0, 0.0), 0
    ) AS int)                         AS T0_Min
  FROM totales t
  LEFT JOIN dias_x_legajo d ON d.Legajo = t.Legajo
),
formateado AS (
  SELECT
    c.*,
    (c.PromMin / 60) AS PromH,
    (c.PromMin % 60) AS PromM,
    (c.PromMin - c.T0_Min) AS DifMinPorDia
  FROM calc c
)

-- 2) Insertar resultado
INSERT INTO ListadoMensual
 (Legajo, Instituto, Nombre, Oficina, Critico, MayorDedicacion, HorasDedicacion,
  Desde, Hasta, HsSemanales, Promedio, DiasTrabajados, HorasDiarias, Diferencia,
  Comentario, Debe, HsCumplidas)
SELECT
  f.Legajo,
  LEFT(ISNULL(f.Instituto, N''), 50)       AS Instituto,
  LEFT(ISNULL(f.Nombre, N''), 50)          AS Nombre,
  LEFT(ISNULL(f.Oficina, N''), 50)         AS Oficina,
  f.Critico,
  f.MayorDedicacion,
  LEFT(ISNULL(f.HorasDedicacion, N''), 11) AS HorasDedicacion,
  @p_desde                                  AS Desde,
  @p_hasta                                  AS Hasta,
  LEFT(ISNULL(f.HsSemanales, N''), 50)     AS HsSemanales,
  -- Promedio 'H.MM' (máx 10)
  LEFT(CAST(f.PromH AS varchar(10)) + '.' + RIGHT('00' + CAST(f.PromM AS varchar(2)), 2), 10) AS Promedio,
  LEFT(CAST(ISNULL(f.DiasTrabajados, 0) AS nvarchar(10)), 10) AS DiasTrabajados,
  LEFT(ISNULL(f.HorasDiarias, N'0'), 5)    AS HorasDiarias,
  LEFT(
    CASE
      WHEN ABS(f.DifMinPorDia) >= 60 THEN
        (CASE WHEN f.DifMinPorDia < 0 THEN '-' ELSE '' END) +
        CAST(ABS(f.DifMinPorDia) / 60 AS varchar(10)) + 'h ' +
        CAST(ABS(f.DifMinPorDia) % 60 AS varchar(10)) + 'm'
      ELSE
        CAST(f.DifMinPorDia AS nvarchar(10))
    END, 10)                                AS Diferencia,
  LEFT(ISNULL(f.Comentario, N''), 100)      AS Comentario,
  LEFT(
    (CASE WHEN (f.DifMinPorDia * ISNULL(f.DiasTrabajados, 0)) < 0 THEN '-' ELSE '' END) +
    CAST(ABS(f.DifMinPorDia * ISNULL(f.DiasTrabajados, 0)) / 60 AS varchar(10)) + 'h ' +
    CAST(ABS(f.DifMinPorDia * ISNULL(f.DiasTrabajados, 0)) % 60 AS varchar(10)) + 'm'
  , 50)                                     AS Debe,
  LEFT(CONVERT(varchar(30), CAST(ROUND(f.TotalMin / 60.0, 2) AS decimal(10,2))), 50) AS HsCumplidas
FROM formateado f

-- 3) Inasistencias
UPDATE L
   SET L.Inasistencias = X.Cuenta
FROM ListadoMensual L
INNER JOIN (
    SELECT Legajo, COUNT(MotivoInasistencia) AS Cuenta
    FROM Movimiento
    WHERE Dia >= @p_desde AND Dia <= @p_hasta
    GROUP BY Legajo
) AS X ON X.Legajo = L.Legajo

-- 4) Solo negativos (opcional)
IF @p_soloNegativos = 1
BEGIN
  DELETE L
    FROM ListadoMensual L
    CROSS APPLY (
       SELECT
         CASE
           WHEN L.Diferencia IS NULL OR LTRIM(RTRIM(L.Diferencia)) = '' THEN NULL
           ELSE SUBSTRING(
                  L.Diferencia,
                  1,
                  CASE WHEN PATINDEX('%[^0-9-]%', L.Diferencia + 'x') = 0
                       THEN LEN(L.Diferencia)
                       ELSE PATINDEX('%[^0-9-]%', L.Diferencia + 'x') - 1
                  END
                )
         END AS leadnum
    ) A
  WHERE ISNUMERIC(A.leadnum) = 1
    AND CAST(A.leadnum AS int) >= 0
END
"

        Try
            Dim parametros = CmdParams(
                "@p_desde", desde,
                "@p_hasta", hasta,
                "@p_sucursal", If(txtSucursal = "(Todas)", DBNull.Value, txtSucursal),
                "@p_soloNegativos", soloNegativos
            )
            DSM.Execute(DSM.Personal, sqlListadoMensual, parametros)

        Catch ex As Exception
            MessageBox.Show("Error al calcular listado mensual: " & ex.Message,
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function ObtenerAgentes()
        Dim sql As String = "
            SELECT Legajo, Nombre, Instituto, CUIL, Ingreso, Nomarca
            FROM Agentes a 
            WHERE (a.Baja IS NULL OR a.Baja = '') 
              AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
            ORDER BY Legajo"
        Return DSM.ExecuteQuery(DSM.Personal, sql)
    End Function

    Public Function ObtenerAgentesQueMarcaron() As DataTable
        Dim sql As String = "
            DECLARE @hoy date      = CAST(GETDATE() AS date);
            DECLARE @maniana date  = DATEADD(day, 1, @hoy);

            ;WITH marcas_hoy AS (
              SELECT 
                m.Legajo,
                MIN(m.Entro) AS Entro,
                MAX(m.Salio) AS Salio
              FROM Movimiento m
              WHERE m.MotivoInasistencia IS NULL
                AND m.Dia >= @hoy AND m.Dia < @maniana
              GROUP BY m.Legajo
            )
            SELECT 
              a.Legajo, a.Nombre, a.Instituto,
              mh.Entro, mh.Salio
            FROM Agentes a
            INNER JOIN marcas_hoy mh ON mh.Legajo = a.Legajo
            WHERE (a.Baja IS NULL OR a.Baja = '')
              AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
              AND (a.Nomarca = 0 OR a.Nomarca IS NULL)
            ORDER BY a.Legajo;"
        Return DSM.ExecuteQuery(DSM.Personal, sql)
    End Function

    Public Function ObtenerAgentesQueNoMarcaron() As DataTable
        Dim sql As String = "
            DECLARE @hoy date      = CAST(GETDATE() AS date);
            DECLARE @maniana date  = DATEADD(day, 1, @hoy);

            ;WITH marcas_hoy AS (
              SELECT 
                m.Legajo,
                MIN(m.Entro) AS Entro,
                MAX(m.Salio) AS Salio
              FROM Movimiento m
              WHERE m.MotivoInasistencia IS NULL
                AND m.Dia >= @hoy AND m.Dia < @maniana
              GROUP BY m.Legajo
            )
            SELECT 
              a.Legajo, a.Nombre, a.Instituto, 
              CAST(NULL AS datetime) AS Entro,
              CAST(NULL AS datetime) AS Salio
            FROM Agentes a
            LEFT JOIN marcas_hoy mh ON mh.Legajo = a.Legajo
            WHERE (a.Baja IS NULL OR a.Baja = '')
              AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
              AND (a.Nomarca = 0 OR a.Nomarca IS NULL)
              AND mh.Legajo IS NULL
            ORDER BY a.Legajo;"
        Return DSM.ExecuteQuery(DSM.Personal, sql)
    End Function

    Public Function ObtenerAgentesDeVacaciones() As DataTable
        Dim sql As String = "
            DECLARE @hoy date      = CAST(GETDATE() AS date);
            DECLARE @maniana date  = DATEADD(day, 1, @hoy);

            ;WITH vacaciones_hoy AS (
              SELECT 
                m.Legajo,
                MIN(m.Entro) AS Entro,
                MAX(m.Salio) AS Salio,
                MIN(m.MotivoInasistencia) AS Motivo
              FROM Movimiento m
              WHERE m.MotivoInasistencia IS NOT NULL
                AND m.Dia >= @hoy AND m.Dia < @maniana
              GROUP BY m.Legajo
            )
            SELECT 
              a.Legajo, a.Nombre, a.Instituto, vh.Motivo as MotivoInasistencia
            FROM Agentes a
            INNER JOIN vacaciones_hoy vh ON vh.Legajo = a.Legajo
            WHERE (a.Baja IS NULL OR a.Baja = '')
              AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
              AND (a.Nomarca = 0 OR a.Nomarca IS NULL)
            ORDER BY a.Legajo;"
        Return DSM.ExecuteQuery(DSM.Personal, sql)
    End Function

    Public Function ObtenerAgentesCumplenMes() As DataTable
        Dim sql As String = "
            DECLARE @hoy date      = CAST(GETDATE() AS date);
            SELECT 
              a.Legajo, a.Nombre, a.Instituto, DAY(a.Nacimiento) AS Nacimiento
            FROM Agentes a
            WHERE (a.Baja IS NULL OR a.Baja = '')
              AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
              AND a.Nacimiento IS NOT NULL
              AND MONTH(a.Nacimiento) = MONTH(@hoy)
            ORDER BY a.Legajo;"
        Return DSM.ExecuteQuery(DSM.Personal, sql)
    End Function

    Public Function ObtenerAgentesIngresaron6m() As DataTable
        Dim sql As String = "
            DECLARE @hoy  date = CAST(GETDATE() AS date);
            DECLARE @d180 date = DATEADD(day, -180, @hoy);
            DECLARE @d135 date = DATEADD(day, -135, @hoy);

            ;WITH A AS (
              SELECT 
                a.Legajo, a.Nombre, a.Instituto, a.Ingreso,
                -- normalizo el string (trim)
                LTRIM(RTRIM(a.Ingreso)) AS IngresoStr
              FROM Agentes a
              WHERE (a.Baja IS NULL OR a.Baja = '')
                AND (a.Caracter <> 'Eventual' OR a.Caracter IS NULL OR a.Caracter = '')
            ),
            P AS (
              SELECT 
                Legajo, Nombre, Instituto, Ingreso,
                CASE 
                  -- Formato dd/MM/yyyy (ej: 02/06/2025)
                  WHEN IngresoStr LIKE '[0-3][0-9]/[01][0-9]/[12][0-9][0-9][0-9]'
                       AND ISDATE(IngresoStr) = 1
                    THEN CONVERT(date, IngresoStr, 103)

                  -- Formato ISO yyyy-MM-dd (ej: 2025-06-03)
                  WHEN IngresoStr LIKE '[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9]'
                       AND ISDATE(IngresoStr) = 1
                    THEN CAST(IngresoStr AS date)

                  ELSE NULL
                END AS IngresoDate
              FROM A
            )
            SELECT 
              Legajo, Nombre, Instituto, Ingreso
            FROM P
            WHERE IngresoDate IS NOT NULL
              AND IngresoDate >= @d180   -- inclusive
              AND IngresoDate <= @d135   -- inclusive
            ORDER BY Legajo;"
        Return DSM.ExecuteQuery(DSM.Personal, sql)
    End Function



End Module
