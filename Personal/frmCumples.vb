Public Class frmCumples
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmCumples

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmCumples()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmCumples_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub frmCumples_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cargar Motivos de Desvinculacion
        CargarCombos(CmbMeses, "Meses", "Mes", "IdMes", "idMes")
    End Sub

    Private Sub CmdVer_Click(sender As Object, e As EventArgs) Handles CmdVer.Click

    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Try
            Close()
        Catch ex As Exception
            MessageBox.Show($"Error al cerrar el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class