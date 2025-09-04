Imports System.Text.Json
Imports System.Runtime.InteropServices

Partial Class frmBajaReloj

    Private Class Reloj
        Public Property Nombre As String
        Public Property Ip As String
        Public Property Puerto As Integer
        Public Property Zk As Object          ' COM (late binding) - se creará al Conectar
        Public Property Conectado As Boolean
        Public Property UltimaVerif As DateTime?
    End Class

    Private relojes As New List(Of Reloj)
    Private WithEvents tmr As New Timer()

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

    ' --- LOAD: liviano (no registra COM, no conecta, no arranca timer) ---
    Private Sub frmBajaReloj_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Columnas (si no están)
        If dgvRelojes.Columns.Count = 0 Then
            dgvRelojes.Columns.Clear()
            dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Nombre", .HeaderText = "Reloj"})
            dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "IP", .HeaderText = "IP"})
            dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Puerto", .HeaderText = "Puerto"})
            dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Estado", .HeaderText = "Estado"})
            dgvRelojes.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "Ultima", .HeaderText = "Última verificación"})
        End If

        CargarRelojes() ' solo filas; no crea COM
        tmr.Interval = 5000 ' 5s
        ' NO se inicia el timer acá
    End Sub

    Private Sub frmBajaReloj_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        For Each reloj In relojes
            Try
                If reloj.Zk IsNot Nothing Then reloj.Zk.Desconectar()
            Catch
            End Try

            Try
                If reloj.Zk IsNot Nothing Then Marshal.FinalReleaseComObject(reloj.Zk)
            Catch
            End Try
        Next
        relojes.Clear()
        instancia = Nothing
    End Sub

    ' --- Botón CONECTAR: registra COM (si hace falta), crea COM y conecta ---
    Private Sub btnConectar_Click(sender As Object, e As EventArgs) Handles btnConectar.Click
        Try
            ' registra solo si falta
            If (Type.GetTypeFromProgID("ZkBridge.Attendance", throwOnError:=False) Is Nothing) Then
                Funciones.AsegurarRegistroZkBridge() ' o EnsureZkBridgeRegistered()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        SetBusy(True)
        Try
            ' obtener el indice de la fila seleccionada
            If dgvRelojes.SelectedRows.Count > 0 Then
                Dim indice = dgvRelojes.SelectedRows(0).Index
                ConectarReloj(indice) ' síncrono; se dispara a demanda
            End If

            If chkMonitoreo.Checked Then tmr.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Conectar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub

    ' --- Check monitoreo: start/stop timer, sin conectar automáticamente ---
    Private Sub chkMonitoreo_CheckedChanged(sender As Object, e As EventArgs) Handles chkMonitoreo.CheckedChanged
        If chkMonitoreo.Checked Then
            tmr.Start()
        Else
            tmr.Stop()
        End If
    End Sub

    ' --- Importar: a demanda; ignora relojes no conectados ---
    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        MessageBox.Show("Funcionalidad de importación no implementada en este ejemplo.", "Importar logs", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'Dim total As Integer = 0
        'Dim detalle As New List(Of String)

        'SetBusy(True)
        'Try
        '    For Each r In relojes
        '        If Not r.Conectado OrElse r.Zk Is Nothing Then Continue For
        '        Try
        '            Dim json As String = r.Zk.LeerLogs()
        '            Dim count As Integer = 0
        '            If Not String.IsNullOrWhiteSpace(json) Then
        '                Using doc = JsonDocument.Parse(json)
        '                    If doc.RootElement.ValueKind = JsonValueKind.Array Then
        '                        count = doc.RootElement.GetArrayLength()
        '                    End If
        '                End Using
        '            End If
        '            total += count
        '            detalle.Add($"{r.Nombre}: {count} registros")
        '        Catch ex As Exception
        '            detalle.Add($"{r.Nombre}: error ({ex.Message})")
        '        End Try
        '    Next
        'Finally
        '    SetBusy(False)
        'End Try

        'MessageBox.Show(String.Join(Environment.NewLine, detalle) &
        '                Environment.NewLine & Environment.NewLine &
        '                $"TOTAL: {total} registros",
        '                "Importación de logs",
        '                MessageBoxButtons.OK,
        '                MessageBoxIcon.Information)
    End Sub

    ' --- Timer: solo “pingea” si hay COM creado ---
    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
        For i = 0 To relojes.Count - 1
            Dim r = relojes(i)
            If r.Zk Is Nothing Then Continue For
            Try
                Dim ok As Boolean = r.Zk.Ping()
                r.Conectado = ok
                r.UltimaVerif = DateTime.Now
            Catch
                r.Conectado = False
                r.UltimaVerif = DateTime.Now
            End Try
            PintarFila(i, r)
        Next
    End Sub

    ' ---- Lógica ----
    Private Sub CargarRelojes()
        ' TODO: reemplazar por tus IPs reales
        Dim endpoints = {
            New With {.Nombre = "Reloj Casa Central", .IP = "192.168.2.5", .Puerto = 4370},
            New With {.Nombre = "Reloj Autoshop Mdz", .IP = "192.168.2.6", .Puerto = 4370},
            New With {.Nombre = "Reloj Zona Franca", .IP = "192.168.3.5", .Puerto = 4370},
            New With {.Nombre = "Reloj Halpern", .IP = "192.168.4.50", .Puerto = 4370},
            New With {.Nombre = "Reloj Neuquen", .IP = "181.171.90.106", .Puerto = 4370}
        }

        relojes.Clear()
        dgvRelojes.Rows.Clear()

        For Each ep In endpoints
            Dim r As New Reloj With {
                .Nombre = ep.Nombre,
                .Ip = ep.IP,
                .Puerto = ep.Puerto,
                .Zk = Nothing, ' <-- NO creamos COM acá
                .Conectado = False,
                .UltimaVerif = Nothing
            }
            relojes.Add(r)
            dgvRelojes.Rows.Add(r.Nombre, r.Ip, r.Puerto.ToString(), "—", "—")
        Next

        ConfigurarEstiloGrid(dgvRelojes)
    End Sub

    Private Sub ConectarReloj(indice As Integer)
        Dim reloj = relojes(indice)

        If reloj.Zk Is Nothing Then
            reloj.Zk = Funciones.CrearZkBridge()
        End If

        Try
            Dim ok As Boolean = reloj.Zk.Conectar(reloj.Ip, reloj.Puerto)
            reloj.Conectado = ok
            reloj.UltimaVerif = DateTime.Now
        Catch
            reloj.Conectado = False
            reloj.UltimaVerif = DateTime.Now
        End Try
        PintarFila(indice, reloj)
    End Sub

    Private Sub PintarFila(rowIndex As Integer, r As Reloj)
        If rowIndex < 0 OrElse rowIndex >= dgvRelojes.Rows.Count Then Return
        Dim row = dgvRelojes.Rows(rowIndex)
        row.Cells("Estado").Value = If(r.Conectado, "Conectado", "Desconectado")
        row.Cells("Ultima").Value = If(r.UltimaVerif.HasValue, r.UltimaVerif.Value.ToString("yyyy-MM-dd HH:mm:ss"), "—")
        row.DefaultCellStyle.BackColor =
            If(r.Conectado,
               Drawing.Color.FromArgb(220, 255, 235),
               Drawing.Color.FromArgb(255, 235, 235))
    End Sub

    ' UX: cursor de espera y deshabilitar botones mientras corre algo pesado
    Private Sub SetBusy(busy As Boolean)
        Me.UseWaitCursor = busy
        btnConectar.Enabled = Not busy
        btnImportar.Enabled = Not busy
        chkMonitoreo.Enabled = Not busy
        Me.Refresh()
    End Sub

End Class
