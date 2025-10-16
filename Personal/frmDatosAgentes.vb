Public Class frmDatosAgentes
    Private Shared instancia As frmDatosAgentes

    Public Shared Sub AbrirInstancia(mdiParent As Form, Optional soloEventuales As Boolean? = Nothing)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmDatosAgentes()
            instancia.MdiParent = mdiParent
        End If
        'instancia.MostrarSoloEventuales = soloEventuales
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Public Shared Function ObtenerInstancia() As frmDatosAgentes
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmDatosAgentes()
        End If
        Return instancia
    End Function

    Private Sub frmDatosAgentes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MostrarAgentes()
        MostrarAgentesQueMarcaron()
        MostrarAgentesQueNoMarcaron()
        MostrarVacaciones()
        MostrarCumpleMes()
        MostrarIngreso6m()
    End Sub

    'Private Sub tabDatosAgentes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabDatosAgentes.SelectedIndexChanged
    '    ' segun el indice llamar a un metodo u otro
    '    Select Case tabDatosAgentes.SelectedIndex
    '        Case 0
    '            MostrarAgentes()
    '        Case 1
    '            MostrarAgentesQueMarcaron()
    '        Case 2
    '            MostrarAgentesQueNoMarcaron()
    '        Case 3
    '            MostrarVacaciones()
    '        Case 4
    '            MostrarCumpleMes()
    '    End Select
    'End Sub

    Private Sub MostrarAgentes()
        Dim dt As DataTable = Reportes.ObtenerAgentes()
        dgvAgentes.DataSource = dt
        TabPage1.Text = "Agentes (" & dt.Rows.Count & ")"
        ConfigurarGrid(dgvAgentes)
    End Sub

    Private Sub MostrarAgentesQueMarcaron()
        Dim dt As DataTable = Reportes.ObtenerAgentesQueMarcaron()
        dgvMarcaron.DataSource = dt
        TabPage2.Text = "Marcaron (" & dt.Rows.Count & ")"
        ConfigurarGrid(dgvMarcaron)
    End Sub

    Private Sub MostrarAgentesQueNoMarcaron()
        Dim dt As DataTable = Reportes.ObtenerAgentesQueNoMarcaron()
        dgvSinMarcar.DataSource = dt
        TabPage3.Text = "Sin Marcar (" & dt.Rows.Count & ")"
        ConfigurarGrid(dgvSinMarcar)
    End Sub

    Private Sub MostrarVacaciones()
        Dim dt As DataTable = Reportes.ObtenerAgentesDeVacaciones()
        dgvVacaciones.DataSource = dt
        TabPage4.Text = "Vacaciones (" & dt.Rows.Count & ")"
        ConfigurarGrid(dgvVacaciones)
    End Sub

    Private Sub MostrarCumpleMes()
        Dim dt As DataTable = Reportes.ObtenerAgentesCumplenMes()
        dgvCumpleMes.DataSource = dt
        TabPage5.Text = "Cumpleaños del Mes (" & dt.Rows.Count & ")"
        ConfigurarGrid(dgvCumpleMes)
    End Sub

    Private Sub MostrarIngreso6m()
        Dim dt As DataTable = Reportes.ObtenerAgentesIngresaron6m()
        dgvIngreso6m.DataSource = dt
        TabPage6.Text = "Ingreso 6m (" & dt.Rows.Count & ")"
        ConfigurarGrid(dgvIngreso6m)
    End Sub

    Private Sub ConfigurarGrid(grid As DataGridView)
        grid.ReadOnly = True
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False

        grid.Columns("Legajo").HeaderText = "Legajo"
        grid.Columns("Legajo").Width = 80

        grid.Columns("Nombre").HeaderText = "Nombre"
        grid.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        If grid.Columns.Contains("Instituto") Then
            grid.Columns("Instituto").HeaderText = "Sucursal"
            grid.Columns("Instituto").Width = 150
        End If

        If grid.Columns.Contains("CUIL") Then
            grid.Columns("CUIL").HeaderText = "CUIL"
            grid.Columns("CUIL").Width = 120
        End If

        If grid.Columns.Contains("Ingreso") Then
            grid.Columns("Ingreso").HeaderText = "Ingreso"
            grid.Columns("Ingreso").Width = 80
        End If

        If grid.Columns.Contains("Nomarca") Then
            grid.Columns("Nomarca").HeaderText = "No Marca"
            grid.Columns("Nomarca").Width = 80
        End If

        If grid.Columns.Contains("Egreso") Then
            grid.Columns("Egreso").HeaderText = "Egreso"
            grid.Columns("Egreso").Width = 80
        End If

        If grid.Columns.Contains("MotivoInasistencia") Then
            grid.Columns("MotivoInasistencia").HeaderText = "Motivo"
            grid.Columns("MotivoInasistencia").Width = 150
        End If

        If grid.Columns.Contains("Nacimiento") Then
            grid.Columns("Nacimiento").HeaderText = "Día"
            grid.Columns("Nacimiento").Width = 50
        End If

        Funciones.ConfigurarEstiloGrid(grid)
    End Sub
End Class