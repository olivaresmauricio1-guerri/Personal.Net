Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmLstMensual
    Private Shared instancia As frmLstMensual

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmLstMensual()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmAreas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmLstMensual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboSucursales()

        Dim hoy = DateTime.Today
        dtpDesde.Value = New DateTime(hoy.Year, hoy.Month, 1)
        dtpHasta.Value = New DateTime(hoy.Year, hoy.Month, DateTime.DaysInMonth(hoy.Year, hoy.Month))

    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        ' validar fechas
        If dtpDesde.Value > dtpHasta.Value Then
            MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim sucursalId As Integer = CInt(cmbSucursal.SelectedValue)
        Dim sucursalTexto As String = cmbSucursal.Text
        Dim soloNegativos As Boolean = chkNegativos.Checked

        Reportes.ListadoMensualPorSucursal(dtpDesde.Value, dtpHasta.Value, sucursalId, sucursalTexto, soloNegativos)

        Try
            Process.Start(General.ReportesPath, "Personal listames")
        Catch ex As Exception
            MessageBox.Show("Error al abrir el reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub CargarComboSucursales()
        Dim sql As String = "Select * from Institutos order by Descripcion"
        Dim sucursales As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)

        cmbSucursal.DataSource = sucursales
        cmbSucursal.DisplayMember = "Descripcion"
        cmbSucursal.ValueMember = "id"

        cmbSucursal.SelectedValue = 2
    End Sub

    Private Calcular()
End Class