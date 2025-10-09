Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

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
        CargarCombos(CmbMeses, "Meses", "idMes", "Mes", "idMes")
    End Sub

    Private Sub CmdVer_Click(sender As Object, e As EventArgs) Handles CmdVer.Click
        Try
            Dim sql1 = "DELETE FROM Cumple2"
            DSM.Execute(DSM.Personal, sql1, Nothing, True)

            ' Consulta para insertar datos en la tabla Cumple2
            Dim consultaInsert As String = "INSERT INTO Cumple2 (Legajo, Nacimiento, Nombre, Instituto) " &
            "SELECT Agentes.Legajo, Agentes.Nacimiento, Agentes.Nombre, Agentes.Instituto " &
            "FROM Agentes " &
            "WHERE MONTH(Agentes.Nacimiento) = @Mes " &
            "AND (Agentes.Baja = '' OR Agentes.Baja IS NULL)"

            ' Parámetros para la consulta
            Dim parametros = CmdParams("@Mes", CInt(CmbMeses.SelectedValue))

            ' Ejecutar la consulta de inserción
            DSM.Execute(DSM.Personal, consultaInsert, parametros)

            ' Imprimir el reporte
            Process.Start(General.ReportesPath, "Personal listamae2")

        Catch ex As Exception
            MessageBox.Show($"Error al obtener la lista de cumpleaños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click

        Close()

    End Sub

End Class