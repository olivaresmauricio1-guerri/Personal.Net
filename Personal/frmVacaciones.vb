Public Class frmVacaciones
    Private Shared instancia As frmVacaciones

    Public Shared Sub AbrirInstancia(mdiParent As Form, Optional soloEventuales As Boolean? = Nothing)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmVacaciones()
            instancia.MdiParent = mdiParent
        End If
        'instancia.MostrarSoloEventuales = soloEventuales
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmVacaciones_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmVacaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim añoActual As Integer = Date.Now.Year
        btnCalcularDisponibles.Text = "Calcular Disponibles: " & añoActual.ToString()
        CargarListado()
    End Sub

    Private Sub btnCalcularDisponibles_Click(sender As Object, e As EventArgs) Handles btnCalcularDisponibles.Click
        ActualizarVacacionesDisponibles()
        CargarListado()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub CargarListado()
        Dim dtListado As DataTable = New DataTable()
        Dim dtAgentes = Reportes.ObtenerAgentes()
        Dim dtVacaciones As DataTable = Vacaciones.ObtenerVacaciones()

        For Each row As DataRow In dtAgentes.Rows

        Next

        dgvListado.DataSource = dtVacaciones
        ConfigurarGrid()

        'For Each row As DataRow In dtAgentes.Rows
        '    Dim legajo As Integer = Convert.ToInt32(row("Legajo"))
        '    Dim nombre As String = Convert.ToString(row("Nombre"))
        '    Dim fechaIngreso As Date = If(Not IsDBNull(row("Ingreso")), Convert.ToDateTime(row("Ingreso")), Date.MinValue)
        '    Dim diasDisponibles As Integer = Vacaciones.CalcularDiasVacaciones(fechaIngreso)
        '    Dim newRow As DataRow = dtListado.NewRow()
        '    newRow("Legajo") = legajo
        '    newRow("Nombre") = nombre
        '    newRow("Fecha Ingreso") = fechaIngreso.ToShortDateString()
        '    newRow("Días Vacaciones Disponibles") = diasDisponibles
        '    dtListado.Rows.Add(newRow)
        'Next

        'dgvListado.DataSource = dtAgentes
        'Funciones.ConfigurarEstiloGrid(dgvListado)
    End Sub

    Private Sub ConfigurarGrid()
        Dim grid = dgvListado

        grid.Columns("Legajo").HeaderText = "Legajo"
        grid.Columns("Legajo").Width = 80

        grid.Columns("Nombre").HeaderText = "Nombre"
        grid.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        grid.Columns("Instituto").HeaderText = "Sucursal"
        grid.Columns("Instituto").Width = 120

        grid.Columns("Motivo").HeaderText = "Motivo"
        grid.Columns("Motivo").Width = 120

        grid.Columns("diasDisponibles").HeaderText = "Disp."
        grid.Columns("diasDisponibles").Width = 50
        grid.Columns("diasDisponibles").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grid.Columns("diasRestantes").HeaderText = "Rest."
        grid.Columns("diasRestantes").Width = 50
        grid.Columns("diasRestantes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Funciones.ConfigurarEstiloGrid(dgvListado)
    End Sub

    Private Sub ActualizarVacacionesDisponibles()
        Dim dtAgentes = Reportes.ObtenerAgentes()
        For Each row As DataRow In dtAgentes.Rows
            Dim legajo As Integer = Convert.ToInt32(row("Legajo"))
            Vacaciones.ActualizarVacacionesPorAgente(legajo)
        Next
    End Sub
End Class