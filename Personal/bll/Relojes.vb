Imports System.Globalization
Imports System.Security.Cryptography

Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Relojes

    Public ReadOnly SucursalAlcorta = "A"
    Public ReadOnly SucursalGaray = "B"
    Public ReadOnly SucursalBelgrano = "C"

    Public Class Reloj
        Public Property Nombre As String
        Public Property Ip As String
        Public Property Puerto As Integer
        Public Property CommKey As Integer = 0
        Public Property Conectado As Boolean
        Public Property Ubicacion As String
        Public Property UltimaVerif As DateTime?
        Public Property Conectando As Boolean
    End Class

    Public Function ObtenerRelojes() As List(Of Reloj)
        Dim relojes As New List(Of Reloj)

        Dim sql = "SELECT RelojId, Nombre, Ip, Puerto, Ubicacion, ClaveCom FROM dbo.Relojes WHERE Activo = 1 ORDER BY RelojId;"
        Dim dtRelojes As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, Nothing)

        For Each row As DataRow In dtRelojes.Rows
            Dim reloj As New Reloj With {
                .Nombre = CStr(row("Nombre")),
                .Ip = CStr(row("Ip")),
                .Puerto = If(IsDBNull(row("Puerto")), 4370, Convert.ToInt32(row("Puerto"))),
                .CommKey = If(IsDBNull(row("ClaveCom")), 0, Convert.ToInt32(row("ClaveCom"))),
                .Conectado = False,
                .Ubicacion = CStr(row("Ubicacion")),
                .UltimaVerif = Nothing
            }
            relojes.Add(reloj)
        Next

        Return relojes
    End Function

    Public Function ObtenerRutas() As List(Of (red As String, mask As String, gateway As String))
        Dim sql = "SELECT Red, Mascara, Gateway FROM dbo.RutasEstaticas WHERE Activo = 1;"
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, Nothing)

        Dim rutas As New List(Of (red As String, mask As String, gateway As String))
        For Each row As DataRow In dt.Rows
            rutas.Add((CStr(row("Red")), CStr(row("Mascara")), CStr(row("Gateway"))))
        Next
        Return rutas
    End Function

    Public Function ObtenerMarcacionesBA(reloj As Reloj) As DataTable
        Dim sucursal = ""
        If reloj.Nombre = "Alcorta" Then sucursal = Relojes.SucursalAlcorta
        If reloj.Nombre = "Garay" Then sucursal = Relojes.SucursalGaray
        If reloj.Nombre = "Belgrano" Then sucursal = Relojes.SucursalBelgrano

        Dim Desde = DateTime.Now.AddDays(-90)
        Dim fechaStr = "#" & Desde.ToString("MM/dd/yyyy") & "#"

        Dim sql = "SELECT * FROM Fichadas " &
          "WHERE FIReloj = @Sucursal AND FIFecha >= " & fechaStr & ";"
        Dim parametros = CmdParams("@Sucursal", sucursal)
        Return DSM.ExecuteQuery(DSM.RelojesBA_, sql, parametros)
    End Function

    Public Function RegistrarMarcacion(reloj As Reloj, legajo As String, fechahoraStr As String) As Integer

        ' si la fechahora es mayor a ahora + 1 hora, no registrar
        If Not DateTime.TryParseExact(fechahoraStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, Nothing) Then
            Return 0
        End If

        Dim sql = "
            INSERT INTO dbo.Marcaciones (dispositivo, puerto, legajo, fechahora)
            SELECT @dispositivo, @puerto, @legajo, @fechahora
            WHERE NOT EXISTS (
                SELECT 1 FROM dbo.Marcaciones WITH (UPDLOCK, HOLDLOCK)
                WHERE legajo = @legajo AND fechahora = @fechahora
            );"

        Dim fh As DateTime
        Dim okDate = DateTime.TryParseExact(fechahoraStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, fh)

        If Not okDate Then Return 0
        Dim parametros = CmdParams("@dispositivo", reloj.Ip, "@puerto", reloj.Puerto, "@legajo", legajo, "@fechaHora", fh)

        Try
            DSM.ExecuteQuery(DSM.Personal, sql, parametros)
            Return 1
        Catch
            Return 0
        End Try
    End Function

    ' procesar marcacion: si de la tabla marcaciones existen registros con procesado = 0, estos deben llevarse a la tabla de movimientos
    ' con las siguientes reglas:
    ' - si el legajo no existe en la tabla de empleados, descartar la marcacion
    ' - si existen marcaciones para ese legajo en el mismo dia, ordenarlas por hora y tomar la primera como entrada y la ultima como salida
    ' - si hay mas de dos marcaciones en el dia, volver a crear otro registro de movimiento con la misma regla
    ' - si las marcaciones son impares, repetir la ultima como salida provisoriamente
    ' - en cada registro de movimiento, calcular HsCumplidas como la diferencia entre la entrada y la salida
    Public Function ProcesarMarcaciones(reloj As Reloj) As Boolean
        Dim sql = "
            SET XACT_ABORT ON;
            SET NOCOUNT ON;

            BEGIN TRAN;

            IF OBJECT_ID('tempdb..#mov_pre') IS NOT NULL DROP TABLE #mov_pre;

            ;WITH dias_pendientes AS (
              /* (legajo,dia) que tienen marcaciones sin procesar y el legajo es sólo dígitos */
              SELECT DISTINCT
                  legajo_str = LTRIM(RTRIM(m.legajo)),
                  dia        = CAST(m.fechahora AS date)
              FROM dbo.Marcaciones m
              WHERE m.procesado = 0
                AND m.legajo IS NOT NULL
                AND LTRIM(RTRIM(m.legajo)) <> ''
                AND LTRIM(RTRIM(m.legajo)) NOT LIKE '%[^0-9]%'   -- sólo 0-9
            ),
            base_todo AS (
              /* Trae TODAS las marcaciones de esos (legajo,dia), sin castear el legajo */
              SELECT
                  m.id,
                  legajo_str = LTRIM(RTRIM(m.legajo)),
                  m.fechahora,
                  dia = CAST(m.fechahora AS date),
                  m.procesado
              FROM dbo.Marcaciones m
              INNER JOIN dias_pendientes d
                  ON d.dia        = CAST(m.fechahora AS date)
                 AND d.legajo_str = LTRIM(RTRIM(m.legajo))
            ),
            secuenciadas AS (
              SELECT
                  b.*,
                  rn = ROW_NUMBER() OVER (PARTITION BY b.legajo_str, b.dia ORDER BY b.fechahora)
              FROM base_todo b
            ),
            pares AS (
              /* Empareja (entrada impar) con la siguiente (salida); si falta, duplica última */
              SELECT
                  s1.legajo_str             AS LegajoStr,
                  CAST(s1.dia AS datetime)  AS Dia,
                  s1.fechahora              AS Entro,
                  ISNULL(s2.fechahora, s1.fechahora) AS Salio,
                  s1.id                     AS entrada_id,
                  ISNULL(s2.id, s1.id)      AS salida_id
              FROM (
                  SELECT legajo_str, dia, id, fechahora, rn
                  FROM secuenciadas
                  WHERE rn % 2 = 1
              ) AS s1
              LEFT JOIN (
                  SELECT legajo_str, dia, id, fechahora, rn
                  FROM secuenciadas
              ) AS s2
                ON s2.legajo_str = s1.legajo_str
               AND s2.dia        = s1.dia
               AND s2.rn         = s1.rn + 1
            ),
            movimientos_pre AS (
              /* Ahora sí: convertimos el legajo (ya seguro numérico) a INT */
              SELECT
                  LegajoInt = CAST(LegajoStr AS int),
                  Dia,
                  Entro,
                  Salio,
                  HsCumplidas = CONVERT(
                      varchar(5),
                      DATEADD(minute, DATEDIFF(minute, Entro, Salio), 0),
                      108
                  ),
                  SinFicha       = CAST(0 AS bit),
                  Autorizo       = CAST(NULL AS nvarchar(50)),
                  NoPromedia     = CAST(0 AS bit),
                  Nopromedianada = CAST(0 AS bit),
                  entrada_id,
                  salida_id
              FROM pares
            )
            SELECT
                Legajo      = LegajoInt,
                Dia, Entro, Salio,
                HsCumplidas = CAST(HsCumplidas AS nvarchar(12)),
                SinFicha, Autorizo, NoPromedia, Nopromedianada,
                entrada_id, salida_id
            INTO #mov_pre
            FROM movimientos_pre;

            /* 1) UPSERT a Movimiento (actualiza si ya existe Legajo+Dia+Entro) */
            MERGE dbo.Movimiento AS tgt
            USING (
              SELECT DISTINCT
                  Legajo, Dia, Entro, Salio, HsCumplidas,
                  SinFicha, Autorizo, NoPromedia, Nopromedianada
              FROM #mov_pre
            ) AS src
            ON  tgt.Legajo = src.Legajo
            AND tgt.Dia    = src.Dia
            AND tgt.Entro  = src.Entro
            WHEN MATCHED THEN
              UPDATE SET
                  tgt.Salio       = src.Salio,
                  tgt.HsCumplidas = src.HsCumplidas
            WHEN NOT MATCHED BY TARGET THEN
              INSERT (Legajo, Dia, Entro, Salio, HsCumplidas, MotivoInasistencia, Comentario,
                      SinFicha, Autorizo, NoPromedia, Nopromedianada)
              VALUES (src.Legajo, src.Dia, src.Entro, src.Salio, src.HsCumplidas, NULL, NULL,
                      src.SinFicha, src.Autorizo, src.NoPromedia, src.Nopromedianada)
            ;

            /* 2) Marca como procesadas sólo las usadas en esta corrida */
            UPDATE m
            SET m.procesado = 1
            FROM dbo.Marcaciones m
            WHERE EXISTS (
              SELECT 1 FROM #mov_pre p WHERE p.entrada_id = m.id OR p.salida_id = m.id
            );

            COMMIT;
            "
        Dim parametros = CmdParams("@dispositivo", reloj.Ip)

        Try
            DSM.Execute(DSM.Personal, sql, parametros)
            Return True
        Catch ex As Exception
            Debug.WriteLine("Error al procesar marcaciones: " & ex.Message)
            Return False
        End Try
    End Function

End Module
