Imports Microsoft.Identity.Client.AuthScheme

Public Class frmLstAgentes
    Private Shared instancia As frmLstAgentes

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmLstAgentes()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmLstAgentes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub
    Private Sub MuestraListado(lst As String)
        ' Abrir el reporte correspondiente segun el parámetro lst que viene de cmblistado
        Try
            Select Case lst
                Case "Analítico"
                    Process.Start(General.ReportesPath, "Personal lstempleados")
                Case "Resumen por Sucursal"
                    Process.Start(General.ReportesPath, "Personal lstresumido")
                Case "Grupo Familiar"
                    Process.Start(General.ReportesPath, "Personal listagrupofamiliar")
                Case Else
                    MessageBox.Show("Listado no reconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select

        Catch ex As Exception
            MessageBox.Show($"Error al abrir el listado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdVer_Click(sender As Object, e As EventArgs) Handles CmdVer.Click
        MuestraListado(cmbListado.Text)
    End Sub
End Class