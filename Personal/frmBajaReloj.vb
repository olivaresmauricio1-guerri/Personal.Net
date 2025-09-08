Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text.Json
Imports System.Threading

Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Class frmBajaReloj

    Private Class Reloj
        Public Property Nombre As String
        Public Property Ip As String
        Public Property Puerto As Integer
        Public Property CommKey As Integer = 0
        Public Property Conectado As Boolean
        Public Property UltimaVerif As DateTime?
        Public Property Conectando As Boolean
    End Class

    Private relojes As New List(Of Reloj)

    Private Shared instancia As frmBajaReloj
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmBajaReloj()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    ' ------------------------------------------------------------------------------------------------------------------------
    ' ------------------------------------------------------------------------------------------------------------------------
    ' ------------------------------------------------------------------------------------------------------------------------

    Private Sub frmBajaReloj_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InicializarGrilla()
        CargarRelojes()
    End Sub

    Private Sub frmBajaReloj_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        relojes.Clear()
        instancia = Nothing
    End Sub

    Private Sub dgvRelojes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRelojes.SelectionChanged
        Dim indiceSeleccionado As Integer = -1
        If dgvRelojes.SelectedRows.Count > 0 Then
            indiceSeleccionado = dgvRelojes.SelectedRows(0).Index
        End If
        cmdImportarSeleccionado.Visible = (indiceSeleccionado >= 0 AndAlso indiceSeleccionado < relojes.Count)
    End Sub

    Private Sub btnImportarTodo_Click(sender As Object, e As EventArgs) Handles btnImportarTodo.Click
        InicializarGrilla()
        CargarRelojes()

        If Not AsegurarRegistroZkBridge() Then
            MessageBox.Show("No se pudo registrar el componente ZKBridge. Verifique que tiene permisos de administrador.", "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        For i = 0 To relojes.Count - 1
            ConectarRelojAsync(i, True)
        Next
    End Sub

    Private Sub cmdImportarSeleccionado_Click(sender As Object, e As EventArgs) Handles cmdImportarSeleccionado.Click
        Dim indiceSeleccionado As Integer = -1
        If dgvRelojes.SelectedRows.Count > 0 Then
            indiceSeleccionado = dgvRelojes.SelectedRows(0).Index
        End If
        If indiceSeleccionado < 0 OrElse indiceSeleccionado >= relojes.Count Then
            MessageBox.Show("No hay ningún reloj seleccionado.", "Importar marcaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not AsegurarRegistroZkBridge() Then
            MessageBox.Show("No se pudo registrar el componente ZKBridge. Verifique que tiene permisos de administrador.", "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        ConectarRelojAsync(indiceSeleccionado, True)
    End Sub

    Private Sub btnConectar_Click(sender As Object, e As EventArgs) Handles btnConectar.Click
        InicializarGrilla()
        CargarRelojes()

        If Not AsegurarRegistroZkBridge() Then
            MessageBox.Show("No se pudo registrar el componente ZKBridge. Verifique que tiene permisos de administrador.", "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        For i = 0 To relojes.Count - 1
            ConectarRelojAsync(i)
        Next
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    ' ------------------------------------------------------------------------------------------------------------------------
    ' ------------------------------------------------------------------------------------------------------------------------
    ' ------------------------------------------------------------------------------------------------------------------------

    Private Sub InicializarGrilla()
        dgvRelojes.Columns.Clear()
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Nombre", .HeaderText = "Reloj"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "IP", .HeaderText = "IP"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Puerto", .HeaderText = "Puerto"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Estado", .HeaderText = "Estado"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Ultima", .HeaderText = "Última verificación"})
    End Sub

    Private Sub CargarRelojes()

        Dim sql = "SELECT Nombre, Ip, Puerto, ClaveCom FROM dbo.Relojes WHERE Activo = 1 ORDER BY RelojId;"
        Dim dtRelojes As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, Nothing)

        relojes.Clear()
        dgvRelojes.Rows.Clear()

        For Each row As DataRow In dtRelojes.Rows
            Dim reloj As New Reloj With {
                .Nombre = CStr(row("Nombre")),
                .Ip = CStr(row("Ip")),
                .Puerto = If(IsDBNull(row("Puerto")), 4370, Convert.ToInt32(row("Puerto"))),
                .CommKey = If(IsDBNull(row("ClaveCom")), 0, Convert.ToInt32(row("ClaveCom"))),
                .Conectado = False,
                .UltimaVerif = Nothing
            }
            relojes.Add(reloj)
            dgvRelojes.Rows.Add(reloj.Nombre, reloj.Ip, reloj.Puerto.ToString(), "—", "—")
        Next

        dgvRelojes.ClearSelection()
        dgvRelojes.AllowUserToResizeColumns = True
        dgvRelojes.AllowUserToResizeRows = False
        dgvRelojes.AllowUserToOrderColumns = True

        dgvRelojes.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("IP").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("Puerto").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("Estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("Ultima").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ConfigurarEstiloGrid(dgvRelojes)
        CargarRutasDesdeDb()
    End Sub

    Private Sub CargarRutasDesdeDb()
        Dim sql = "
        SELECT Red, Mascara, Gateway
        FROM dbo.RutasEstaticas
        WHERE Activo = 1;"
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, Nothing)

        Dim rutas As New List(Of (red As String, mask As String, gw As String))
        For Each row As DataRow In dt.Rows
            rutas.Add((CStr(row("Red")), CStr(row("Mascara")), CStr(row("Gateway"))))
        Next

        Red.EstablecerRutas(rutas)
    End Sub

    ' registra ZkBridge si no está registrado
    Private Function AsegurarRegistroZkBridge()
        Try
            If (Type.GetTypeFromProgID("ZkBridge.Attendance", throwOnError:=False) Is Nothing) Then
                Funciones.AsegurarRegistroZkBridge()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return False
    End Function

    ' abre un hilo para el reloj indicado y conecta
    Private Sub ConectarRelojAsync(indice As Integer, Optional cargarMarcaciones As Boolean = False)
        If indice < 0 OrElse indice >= relojes.Count Then Return
        Dim reloj = relojes(indice)
        If reloj.Conectando Then Return

        reloj.Conectando = True
        If Me.IsHandleCreated Then Me.BeginInvoke(
            Sub()
                dgvRelojes.Rows(indice).Cells("Estado").Value = "Conectando..."
                dgvRelojes.Rows(indice).Cells("Ultima").Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                ResaltarEstado(indice)
            End Sub)

        Dim th As New Thread(
            Sub()
                Dim zk As Object = Nothing
                Try
                    ' ---- todo lo de este hilo queda dentro de Try para no afectar otros relojes ----

                    ' rutas si aplica (no comparte estado entre hilos)
                    Try
                        If Red.NecesitaRutaParaIp(reloj.Ip) Then Red.AsegurarRutasParaIp(reloj.Ip)
                    Catch exRt As Exception
                        ' no aborta: registra y sigue
                        ' Opcional: loguear exRt.Message
                    End Try

                    ' COM en este STA
                    Try
                        zk = Funciones.CrearZkBridge()
                    Catch exCom As Exception
                        reloj.Conectando = False
                        reloj.Conectado = False
                        reloj.UltimaVerif = DateTime.Now
                        If Me.IsHandleCreated Then SafeUI(
                            Sub()
                                dgvRelojes.Rows(indice).Cells("Estado").Value = "Error al crear COM"
                                dgvRelojes.Rows(indice).Cells("Ultima").Value = reloj.UltimaVerif.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                ResaltarEstado(indice)
                            End Sub)
                        Return
                    End Try

                    Dim ok As Boolean = False
                    Try
                        ok = zk.Conectar(reloj.Ip, reloj.Puerto, reloj.CommKey)
                    Catch exConn As Exception
                        ok = False
                    End Try

                    reloj.Conectando = False
                    reloj.Conectado = ok
                    reloj.UltimaVerif = DateTime.Now

                    If Me.IsHandleCreated Then SafeUI(
                        Sub()
                            dgvRelojes.Rows(indice).Cells("Estado").Value = If(ok, "Conectado", "Desconectado")
                            dgvRelojes.Rows(indice).Cells("Ultima").Value = reloj.UltimaVerif.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            ResaltarEstado(indice)
                        End Sub)

                    ' ================== Importación opcional con % ==================
                    If ok AndAlso cargarMarcaciones Then
                        If Me.IsHandleCreated Then SafeUI(
                            Sub()
                                dgvRelojes.Rows(indice).Cells("Estado").Value = "Importando..."
                                ResaltarEstado(indice)
                            End Sub)

                        ' 1) Cargar logs en memoria del bridge
                        Dim totalLogs As Integer = 0
                        Try
                            zk.CargarLogs()
                            ' Si expusiste los métodos; si no, asumí PAGE_SIZE=50
                            Try : totalLogs = CInt(zk.TotalLogs()) : Catch : totalLogs = 0 : End Try
                        Catch
                            totalLogs = 0
                        End Try

                        ' 2) Iterar por páginas y calcular %
                        Dim pagina As Integer = 0
                        Dim processed As Integer = 0
                        Dim pageSize As Integer = 50
                        Try : pageSize = CInt(zk.PageSize()) : Catch : pageSize = 50 : End Try

                        Dim json As String = Nothing
                        Try : json = zk.ObtenerLogs(pagina) : Catch : json = "[]" : End Try

                        While Not String.IsNullOrWhiteSpace(json)
                            Dim batch As Integer = 0

                            Try
                                Using doc = JsonDocument.Parse(json)
                                    If doc.RootElement.ValueKind = JsonValueKind.Array Then
                                        Dim arr = doc.RootElement.EnumerateArray()
                                        Dim hay = arr.Any()
                                        If Not hay Then Exit While

                                        For Each elem In doc.RootElement.EnumerateArray()
                                            If Not elem.TryGetProperty("id", Nothing) Then Continue For
                                            If Not elem.TryGetProperty("fechaHora", Nothing) Then Continue For

                                            Dim sql = "
                                                INSERT INTO dbo.Marcaciones (dispositivo, puerto, legajo, fechahora)
                                                SELECT @dispositivo, @puerto, @legajo, @fechahora
                                                WHERE NOT EXISTS (
                                                    SELECT 1 FROM dbo.Marcaciones WITH (UPDLOCK, HOLDLOCK)
                                                    WHERE legajo = @legajo AND fechahora = @fechahora
                                                );"

                                            Dim fh As DateTime
                                            Dim okDate = DateTime.TryParseExact(
                                                elem.GetProperty("fechaHora").GetString(),
                                                "yyyy-MM-dd HH:mm:ss",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None,
                                                fh)

                                            If Not okDate Then Continue For
                                            Dim parametros = CmdParams(
                                                "@dispositivo", reloj.Ip,
                                                "@puerto", reloj.Puerto,
                                                "@legajo", elem.GetProperty("id").ToString(),
                                                "@fechaHora", fh)

                                            Try
                                                DSM.ExecuteQuery(DSM.Personal, sql, parametros)
                                                batch += 1
                                            Catch
                                                ' error de inserción: continuar sin interrumpir
                                            End Try
                                        Next
                                    End If
                                End Using
                            Catch
                                ' error parseando JSON: continuar
                            End Try

                            processed += batch

                            ' %: si no conocemos total, estimamos con páginas (opcional)
                            Dim pct As Integer
                            If totalLogs > 0 Then
                                pct = CInt(Math.Floor(processed * 100.0 / totalLogs))
                            Else
                                ' fallback: % aproximado por páginas
                                Dim estimado As Integer = (pagina + 1) * pageSize
                                pct = CInt(Math.Min(100, Math.Floor(estimado * 100.0 / Math.Max(estimado, 1))))
                            End If

                            If Me.IsHandleCreated Then SafeUI(
                                Sub()
                                    dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importando {pct}%"
                                    ResaltarEstado(indice)
                                End Sub)

                            pagina += 1
                            Try : json = zk.ObtenerLogs(pagina) : Catch : Exit While : End Try
                        End While

                        If Me.IsHandleCreated Then SafeUI(
                            Sub()
                                If totalLogs > 0 Then
                                    dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importado {processed}/{totalLogs}"
                                Else
                                    dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importado {processed}"
                                End If
                                ResaltarEstado(indice)
                            End Sub)
                    End If

                Catch ex As Exception
                    ' Cualquier excepción NO afecta a otros hilos/relojes
                    reloj.Conectando = False
                    reloj.Conectado = False
                    reloj.UltimaVerif = DateTime.Now
                    If Me.IsHandleCreated Then SafeUI(
                        Sub()
                            dgvRelojes.Rows(indice).Cells("Estado").Value = "Error/Timeout"
                            dgvRelojes.Rows(indice).Cells("Ultima").Value = reloj.UltimaVerif.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            ResaltarEstado(indice)
                        End Sub)
                Finally
                    ' Liberar COM con doble try
                    Try
                        If zk IsNot Nothing Then
                            Try : zk.Desconectar() : Catch : End Try
                        End If
                    Finally
                        Try
                            If zk IsNot Nothing Then Marshal.FinalReleaseComObject(zk)
                        Catch
                        End Try
                    End Try
                End Try
            End Sub)

        th.IsBackground = True
        th.SetApartmentState(ApartmentState.STA) ' COM/ActiveX ⇒ STA
        th.Start() ' ← ¡no esperamos! la UI sigue
    End Sub

    Private Sub SafeUI(action As Action)
        Try
            If Not Me.IsHandleCreated Then Return
            Me.BeginInvoke(New MethodInvoker(
            Sub()
                Try
                    action()
                Catch
                    ' swallow: la UI no debe romper el hilo de importación
                End Try
            End Sub))
        Catch
            ' swallow BeginInvoke failures
        End Try
    End Sub

    ' da formato a la fila según estado del reloj
    Private Sub ResaltarEstado(indice As Integer)
        Dim reloj = relojes(indice)
        Dim row = dgvRelojes.Rows(indice)

        Dim colorSel As Drawing.Color
        If reloj.Conectando Then
            colorSel = Drawing.Color.FromArgb(255, 249, 196) ' amarillo pálido
        ElseIf reloj.Conectado Then
            colorSel = Drawing.Color.FromArgb(220, 255, 235) ' verde claro
        Else
            colorSel = Drawing.Color.FromArgb(255, 235, 235) ' rojo claro
        End If

        row.DefaultCellStyle.BackColor = colorSel
    End Sub
End Class
