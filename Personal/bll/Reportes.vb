
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Reportes

    Public Sub ListadoHorario(desde As Date, hasta As Date, idSucursal As Integer, txtSucursal As String, soloTarde As Boolean)
        Try
            DSM.Execute(DSM.Personal, "DELETE FROM ListaHorario")

            Dim tardeWhere As String = If(soloTarde,
            "AND CONVERT(time, Movimiento.Entro) >= '08:00:00' AND CONVERT(time, Movimiento.Entro) < '09:30:00'",
            "")

            Dim sql As String = $"
                INSERT INTO ListaHorario (Legajo, Nombre, Oficina, Dia, Entro, Salio, HsCumplidas, Instituto, Motivo, CUIL, NroDto, TipoDto)
                SELECT 
                    Agentes.Legajo,
                    Agentes.Nombre,
                    Agentes.Cargo,
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
                    Agentes.Legajo, Agentes.Nombre, Agentes.Cargo,
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
    A.Secretaria,
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
    MIN(b.Secretaria)       AS Secretaria,
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
    t.Secretaria,
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
  Comentario, Debe, Secretaria, HsCumplidas)
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
  LEFT(ISNULL(f.Secretaria, N''), 50)       AS Secretaria,
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
End Module
