Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports System.Data
Imports System.Text

Public Class frmBuscaFalta
    Private Shared instancia As frmBuscaFalta

    ' --- Estado en memoria para lo que ve el usuario ---
    Private _faltas As DataTable ' columnas: Legajo(int), Nombre(nvarchar), Dia(date)

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmBuscaFalta()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmBuscaFalta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' primer dia del mes anterior
        dtpDesde.Value = New Date(Date.Today.Year, Date.Today.Month, 1).AddMonths(-1)

        ' ultimo dia del mes anterior
        dtpHasta.Value = New Date(Date.Today.Year, Date.Today.Month, 1).AddDays(-1)

        CargarComboSucursales()
        PrepararGrid()
        RefrescarListado() ' carga inicial
        FormatearGrid()
    End Sub

    ' ============ PASO 1: BUSCAR FALTAS (solo lectura) ============
    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        RefrescarListado()
    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        RefrescarListado()
    End Sub

    Private Sub cmbInstituto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInstituto.SelectedIndexChanged
        RefrescarListado()
    End Sub

    Private Sub RefrescarListado()
        Dim desde = NormalizarFecha(dtpDesde.Value.Date)
        Dim hasta = NormalizarFecha(dtpHasta.Value.Date)
        Dim sucursal = If(cmbInstituto.SelectedValue IsNot Nothing, cmbInstituto.SelectedValue.ToString(), String.Empty)

        _faltas = Inasistencias.BuscarFaltas(desde, hasta, sucursal)
        dgvFaltas.DataSource = _faltas
        lblTotal.Text = $"Faltas encontradas: {If(_faltas Is Nothing, 0, _faltas.Rows.Count)}"
    End Sub

    Private Shared Function NormalizarFecha(d As Date) As Date
        Return New Date(d.Year, d.Month, d.Day)
    End Function

    Private Sub PrepararGrid()
        dgvFaltas.AutoGenerateColumns = True
        dgvFaltas.MultiSelect = True
        dgvFaltas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFaltas.ReadOnly = True
        dgvFaltas.AllowUserToAddRows = False
        dgvFaltas.AllowUserToDeleteRows = False
    End Sub

    Private Sub FormatearGrid()
        If dgvFaltas.Columns.Contains("Dia") Then
            dgvFaltas.Columns("Dia").DefaultCellStyle.Format = "dd/MM/yyyy"
            dgvFaltas.Columns("Dia").HeaderText = "Día"
            dgvFaltas.Columns("Dia").DisplayIndex = 0
        End If
        If dgvFaltas.Columns.Contains("Legajo") Then
            dgvFaltas.Columns("Legajo").HeaderText = "Legajo"
            dgvFaltas.Columns("Legajo").Width = 70
            dgvFaltas.Columns("Legajo").DisplayIndex = 1
        End If
        If dgvFaltas.Columns.Contains("Nombre") Then
            dgvFaltas.Columns("Nombre").HeaderText = "Empleado"
            dgvFaltas.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvFaltas.Columns("Nombre").DisplayIndex = 2
        End If
        If dgvFaltas.Columns.Contains("Instituto") Then
            dgvFaltas.Columns("Instituto").HeaderText = "Sucursal"
            dgvFaltas.Columns("Instituto").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvFaltas.Columns("Instituto").DisplayIndex = 3
        End If
        Funciones.ConfigurarEstiloGrid(dgvFaltas)
    End Sub


    ' ============ PASO 2: CREAR registro por cada fila seleccionada ============
    Private Sub btnCrearRegistro_Click(sender As Object, e As EventArgs) Handles btnCrearRegistro.Click
        Dim selRows = dgvFaltas.SelectedRows
        If selRows Is Nothing OrElse selRows.Count = 0 Then
            MessageBox.Show("Seleccioná una o más filas del listado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim confirm = MessageBox.Show($"Se crearán {selRows.Count} registros de inasistencia. ¿Continuar?",
                                      "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm <> DialogResult.Yes Then Return
        Dim errores As New List(Of String)
        For Each row As DataGridViewRow In selRows
            Dim legajo As Integer = CInt(row.Cells("Legajo").Value)
            Dim dia As Date = CDate(row.Cells("Dia").Value)
            Dim ok = Inasistencias.InsertarInasistencia(legajo, dia)
            If Not ok Then
                errores.Add($"Legajo {legajo} - Día {dia:dd/MM/yyyy}")
            End If
        Next
        If errores.Count = 0 Then
            MessageBox.Show("Inasistencias creadas correctamente.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim msg = "Algunas inasistencias no se pudieron crear:" & Environment.NewLine &
                      String.Join(Environment.NewLine, errores)
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        RefrescarListado()
    End Sub




    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub CargarComboSucursales()
        Dim sql As String = $"SELECT Descripcion FROM Institutos ORDER BY Descripcion"
        Dim sucursales As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)

        cmbInstituto.DataSource = sucursales
        cmbInstituto.DisplayMember = "Descripcion"
        cmbInstituto.ValueMember = "Descripcion"

        cmbInstituto.SelectedValue = "Casa Central"
    End Sub
End Class
