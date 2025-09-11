Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmHorario

    Private Shared instancia As frmHorario

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmHorario()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmAreas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmHorario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboSucursales()

        Dim hoy = DateTime.Today
        dtpDesde.Value = New DateTime(hoy.Year, hoy.Month, 1)
        dtpHasta.Value = New DateTime(hoy.Year, hoy.Month, DateTime.DaysInMonth(hoy.Year, hoy.Month))
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click

        If dtpDesde.Value > dtpHasta.Value Then
            MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim sucursalId As Integer = CInt(cmbSucursal.SelectedValue)
        Dim sucursalTexto As String = cmbSucursal.Text
        Dim soloTarde As Boolean = chkTarde.Checked
        Dim vista As String = If(radSemana.Checked, "semana", If(radAgente.Checked, "agente", "normal"))

        Reportes.ListadoHorario(dtpDesde.Value, dtpHasta.Value, sucursalId, sucursalTexto, soloTarde)

        Try
            Process.Start(General.ReportesPath, $"Personal lsthorario vista {vista}")
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