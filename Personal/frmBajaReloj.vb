Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text.Json
Imports System.Threading

Partial Class frmBajaReloj

    Private _relojes As New List(Of Reloj)

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
        _relojes.Clear()
        instancia = Nothing
    End Sub

    Private Sub dgvRelojes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRelojes.SelectionChanged
        Dim indiceSeleccionado As Integer = -1
        If dgvRelojes.SelectedRows.Count > 0 Then
            indiceSeleccionado = dgvRelojes.SelectedRows(0).Index
        End If
        cmdImportarSeleccionado.Visible = (indiceSeleccionado >= 0 AndAlso indiceSeleccionado < _relojes.Count)
    End Sub

    'Private Sub btnImportarTodo_Click(sender As Object, e As EventArgs) Handles btnImportarTodo.Click
    '    InicializarGrilla()
    '    CargarRelojes()

    '    If Not Funciones.AsegurarRegistroZkBridge() Then
    '        MessageBox.Show("No se pudo registrar el componente ZKBridge. Verifique que tiene permisos de administrador.", "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return
    '    End If
    '    For i = 0 To _relojes.Count - 1
    '        ConectarRelojAsync(i, True)
    '    Next
    'End Sub

    Private Sub cmdImportarSeleccionado_Click(sender As Object, e As EventArgs) Handles cmdImportarSeleccionado.Click
        Dim indiceSeleccionado As Integer = -1
        If dgvRelojes.SelectedRows.Count > 0 Then
            indiceSeleccionado = dgvRelojes.SelectedRows(0).Index
        End If
        If indiceSeleccionado < 0 OrElse indiceSeleccionado >= _relojes.Count Then
            MessageBox.Show("No hay ningún reloj seleccionado.", "Importar marcaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not Funciones.AsegurarRegistroZkBridge() Then
            MessageBox.Show("No se pudo registrar el componente ZKBridge. Verifique que tiene permisos de administrador.", "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        ConectarRelojAsync(indiceSeleccionado, True)
    End Sub

    Private Sub btnConectar_Click(sender As Object, e As EventArgs) Handles btnConectar.Click

        InicializarGrilla()
        CargarRelojes()

        If Not Funciones.AsegurarRegistroZkBridge() Then
            MessageBox.Show("No se pudo registrar el componente ZKBridge. Verifique que tiene permisos de administrador.", "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        For i = 0 To _relojes.Count - 1
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
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Ubicacion", .HeaderText = "Ubicación"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Nombre", .HeaderText = "Reloj"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "IP", .HeaderText = "IP"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Puerto", .HeaderText = "Puerto"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Estado", .HeaderText = "Estado"})
        dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Ultima", .HeaderText = "Última verificación"})
    End Sub

    Private Sub CargarRelojes()
        _relojes = Relojes.ObtenerRelojes()

        dgvRelojes.Rows.Clear()
        For Each reloj As Reloj In _relojes
            dgvRelojes.Rows.Add(reloj.Ubicacion, reloj.Nombre, reloj.Ip, reloj.Puerto.ToString(), "—", "—")
        Next

        dgvRelojes.ClearSelection()
        ConfigurarGrillaRelojes()
        CargarRutas()
    End Sub

    Private Sub ConfigurarGrillaRelojes()
        dgvRelojes.AllowUserToResizeColumns = True
        dgvRelojes.AllowUserToResizeRows = False
        dgvRelojes.AllowUserToOrderColumns = True

        dgvRelojes.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("IP").Width = 120
        dgvRelojes.Columns("Puerto").Width = 60
        dgvRelojes.Columns("Ubicacion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("Estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvRelojes.Columns("Ultima").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ConfigurarEstiloGrid(dgvRelojes)
    End Sub

    Private Sub CargarRutas()
        Dim rutas = Relojes.ObtenerRutas()
        Red.EstablecerRutas(rutas)
    End Sub

    Private Sub ConectarRelojAsync(indice As Integer, Optional cargarMarcaciones As Boolean = False)
        If indice < 0 OrElse indice >= _relojes.Count Then Return
        Dim reloj = _relojes(indice)
        If reloj.Conectando Then Return

        If reloj.Ubicacion = "BsAires" Then
            conectarRelojIndirectoAsync(indice, cargarMarcaciones)
        Else
            ConectarRelojDirectoAsync(indice, cargarMarcaciones)
        End If
    End Sub

    ' abre un hilo para conectar a una bbdd remota y traer marcaciones, las guarda localmente
    ' por ahora no implementado, 
    ' mockea conexion, con un delay de 2 segundos, y luego mestra "conectado"
    Private Sub conectarRelojIndirectoAsync(indice As Integer, Optional cargarMarcaciones As Boolean = False)
        Dim reloj = _relojes(indice)
        If reloj.Conectando Then Return
        reloj.Conectando = True
        If Me.IsHandleCreated Then Me.BeginInvoke(
            Sub()
                dgvRelojes.Rows(indice).Cells("Estado").Value = "Conectando (indirecto)..."
                dgvRelojes.Rows(indice).Cells("Ultima").Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                ResaltarEstado(indice)
            End Sub)
        Dim th As New Thread(
            Sub()
                ToggleUI(False)

                reloj.Conectando = False
                reloj.Conectado = True
                reloj.UltimaVerif = DateTime.Now
                If Me.IsHandleCreated Then SafeUI(
                    Sub()
                        dgvRelojes.Rows(indice).Cells("Estado").Value = "Conectado (indirecto)"
                        dgvRelojes.Rows(indice).Cells("Ultima").Value = reloj.UltimaVerif.Value.ToString("yyyy-MM-dd HH:mm:ss")
                        ResaltarEstado(indice)
                    End Sub)

                If cargarMarcaciones Then
                    Dim dt As DataTable = Relojes.ObtenerMarcacionesBA(reloj)
                    Dim total = dt.Rows.Count
                    Dim procesadas = 0
                    For Each row As DataRow In dt.Rows
                        ' quitar ceros a la izquierda del legajo
                        Dim legajo = CStr(row("FILegajo"))
                        legajo = legajo.TrimStart("0"c)
                        Dim fechahora = Convert.ToDateTime(row("FIFecha")).ToString("yyyy-MM-dd HH:mm:ss")
                        procesadas += Relojes.RegistrarMarcacion(reloj, legajo, fechahora)
                        If Me.IsHandleCreated Then SafeUI(
                            Sub()
                                Dim pct = CInt(Math.Floor(procesadas * 100.0 / Math.Max(total, 1)))
                                dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importando (indirecto) ({pct}%)"
                                ResaltarEstado(indice)
                            End Sub)
                    Next
                    Try : Relojes.ProcesarMarcaciones(reloj) : Catch : End Try
                    If Me.IsHandleCreated Then SafeUI(
                        Sub()
                            dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importado (indirecto) {procesadas}/{total}"
                            ResaltarEstado(indice)
                        End Sub)
                End If

                ToggleUI(True)
            End Sub)
        th.IsBackground = True
        th.SetApartmentState(ApartmentState.STA) ' COM/ActiveX ⇒ STA
        th.Start() ' ← ¡no esperamos! la UI sigue
    End Sub

    ' abre un hilo para el reloj indicado y conecta
    Private Sub ConectarRelojDirectoAsync(indice As Integer, Optional cargarMarcaciones As Boolean = False)
        Dim reloj = _relojes(indice)
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

                ToggleUI(False)

                ' COM en este STA
                Dim zk As Object = Nothing

                Try
                    ' rutas si aplica (no comparte estado entre hilos)
                    Try
                        If Red.NecesitaRutaParaIp(reloj.Ip) Then Red.AsegurarRutasParaIp(reloj.Ip)
                    Catch : End Try
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

                        If Me.IsHandleCreated Then SafeUI(
                            Sub()
                                dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importando... ({totalLogs})"
                                ResaltarEstado(indice)
                            End Sub)

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

                                            Dim legajo = elem.GetProperty("id").ToString()
                                            Dim fechahoraStr = elem.GetProperty("fechaHora").GetString()
                                            batch += Relojes.RegistrarMarcacion(reloj, legajo, fechahoraStr)
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
                                    dgvRelojes.Rows(indice).Cells("Estado").Value = $"Importando ({pct}%)"
                                    ResaltarEstado(indice)
                                End Sub)

                            pagina += 1
                            Try : json = zk.ObtenerLogs(pagina) : Catch : Exit While : End Try
                        End While

                        Try : Relojes.ProcesarMarcaciones(reloj) : Catch : End Try

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

                    ToggleUI(True)
                End Try
            End Sub)

        th.IsBackground = True
        th.SetApartmentState(ApartmentState.STA) ' COM/ActiveX ⇒ STA
        th.Start() ' ← ¡no esperamos! la UI sigue
    End Sub

    Private Sub SafeUI(action As Action)
        Try
            If Not Me.IsHandleCreated Then Return
            Me.BeginInvoke(
                New MethodInvoker(
                    Sub()
                        Try : action() : Catch : End Try
                    End Sub))
        Catch
        End Try
    End Sub

    Private Sub UI(action As Action)
        If Me.IsDisposed Then Return
        If Me.InvokeRequired Then
            Try
                Me.BeginInvoke(action)
            Catch
                ' si el form se cerró en el medio, ignorar
            End Try
        Else
            action()
        End If
    End Sub

    Private Sub ToggleUI(enable As Boolean)
        UI(Sub()
               btnImportarTodo.Enabled = enable
               btnConectar.Enabled = enable
               cmdImportarSeleccionado.Enabled = enable
           End Sub)
    End Sub

    ' da formato a la fila según estado del reloj
    Private Sub ResaltarEstado(indice As Integer)
        Dim reloj = _relojes(indice)
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
