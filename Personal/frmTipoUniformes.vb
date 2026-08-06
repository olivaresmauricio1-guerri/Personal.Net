Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmTipoUniformes
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmTipoUniformes

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmTipoUniformes()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmTipoUniformes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmTipoUniformes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormModoConsulta()
        GridBuscar()
        GridConfigurarColumnas()
        Me.KeyPreview = True
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado, chkEncabezados.Checked)
            e.Handled = True
        End If
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        If e.RowIndex < 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        AplicarSeleccionActual()
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

    Private Sub chkEncabezados_CheckedChanged(sender As Object, e As EventArgs) Handles chkEncabezados.CheckedChanged
        DgvListado.Focus()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
        TxtIdTipoUniforme.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        FormModoEdicion()
        TxtIdTipoUniforme.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este tipo de uniforme?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim idTipoUniforme = Convert.ToInt32(filaActual.Cells("idTipoUniforme").Value)
            Dim sql = "DELETE FROM TipoUniformes WHERE idTipoUniforme = @idTipoUniforme"
            Dim parametros = CmdParams("@idTipoUniforme", idTipoUniforme)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim tipoUniforme = TxtTipoUniforme.Text.Trim

        If filaActual Is Nothing Then
            Dim sql = "INSERT INTO TipoUniformes (TipoUniforme) VALUES (@TipoUniforme)"
            Dim parametros = CmdParams("@TipoUniforme", If(String.IsNullOrEmpty(tipoUniforme), DBNull.Value, tipoUniforme))
            DSM.Execute(DSM.Personal, sql, parametros, True)
        Else
            If TxtTipoUniforme.Text.Trim = "" Then
                MessageBox.Show("El campo Tipo Uniforme no puede estar vacío.")
                TxtTipoUniforme.Focus()
                Return
            End If

            Dim idTipoUniforme = Convert.ToInt32(filaActual.Cells("idTipoUniforme").Value)
            Dim sql = "UPDATE TipoUniformes SET TipoUniforme = @TipoUniforme WHERE idTipoUniforme = @idTipoUniforme"
            Dim parametros = CmdParams("@TipoUniforme", If(String.IsNullOrEmpty(tipoUniforme), DBNull.Value, tipoUniforme), "@idTipoUniforme", idTipoUniforme)
            DSM.Execute(DSM.Personal, sql, parametros, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Public Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT idTipoUniforme, TipoUniforme FROM TipoUniformes"

        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE ( TipoUniforme LIKE @TipoUniforme "
            parametros.Add("@TipoUniforme")
            parametros.Add($"%{texto}%")

            Dim esNumero As Boolean = Integer.TryParse(texto, 0)
            If esNumero Then
                sql &= " OR idTipoUniforme = @idTipoUniforme "
                parametros.Add("@idTipoUniforme")
                parametros.Add(Convert.ToInt32(texto))
            End If

            sql &= ")"
        End If

        sql &= " ORDER BY idTipoUniforme"

        Dim result = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = result

        If result.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        filaActualIndice = 0
        filaActual = DgvListado.Rows(0)
        FormObtenerSeleccionado()
    End Sub

    Private Sub AplicarSeleccionActual()
        If DgvListado Is Nothing OrElse DgvListado.CurrentRow Is Nothing Then Return

        If DgvListado.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        Dim idx = DgvListado.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return

        FormModoConsulta()
        FormLimpiarSeleccionado()

        filaActualIndice = idx
        filaActual = DgvListado.CurrentRow
        FormObtenerSeleccionado()
    End Sub

    Public Sub GridConfigurarColumnas()
        DgvListado.Columns("idTipoUniforme").HeaderText = "Cod Tipo"
        DgvListado.Columns("TipoUniforme").HeaderText = "Tipo Uniforme"

        DgvListado.Columns("idTipoUniforme").Width = 80
        DgvListado.Columns("TipoUniforme").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ConfigurarEstiloGrid(DgvListado)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtIdTipoUniforme.Text = String.Empty
        TxtTipoUniforme.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtIdTipoUniforme.Text = filaActual.Cells("idTipoUniforme").Value.ToString()
            TxtTipoUniforme.Text = If(filaActual.Cells("TipoUniforme").Value IsNot DBNull.Value, filaActual.Cells("TipoUniforme").Value.ToString(), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, TxtIdTipoUniforme, TxtTipoUniforme)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, TxtIdTipoUniforme, TxtTipoUniforme)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
    End Sub
End Class
