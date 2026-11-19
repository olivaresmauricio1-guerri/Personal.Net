Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmReporteBonos

    Private Shared instancia As frmReporteBonos

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmReporteBonos()
            instancia.MdiParent = mdiParent
        End If

        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmReporteBonos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboSucursales()

        Dim hoy = DateTime.Today
        dtpDesde.Value = New DateTime(hoy.Year, hoy.Month, 1)
        dtpHasta.Value = New DateTime(hoy.Year, hoy.Month, DateTime.DaysInMonth(hoy.Year, hoy.Month))
        chkHojaPorAgente.Checked = True
    End Sub

    Private Sub frmReporteBonos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        If dtpDesde.Value.Date > dtpHasta.Value.Date Then
            MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If cmbSucursal.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione una sucursal.", "Reporte de bonos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim sucursalId As Integer = CInt(cmbSucursal.SelectedValue)
        Dim sucursalTexto As String = cmbSucursal.Text.Trim()
        Dim vista As String = If(chkHojaPorAgente.Checked, "agente", "normal")

        Dim argumentos =
            "Personal bonoconfirmaciones " &
            $"fechadesde {dtpDesde.Value:yyyy-MM-dd} " &
            $"fechahasta {dtpHasta.Value:yyyy-MM-dd} " &
            $"sucursalid {sucursalId} " &
            $"sucursal ""{sucursalTexto}"" " &
            $"vista {vista}"

        Try
            Process.Start(General.ReportesPath, argumentos)
        Catch ex As Exception
            MessageBox.Show("Error al abrir el reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub CargarComboSucursales()
        Dim sql As String = "SELECT * FROM Institutos ORDER BY Descripcion"
        Dim sucursales As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)

        cmbSucursal.DataSource = sucursales
        cmbSucursal.DisplayMember = "Descripcion"
        cmbSucursal.ValueMember = "id"
        cmbSucursal.SelectedValue = 2
    End Sub
End Class
