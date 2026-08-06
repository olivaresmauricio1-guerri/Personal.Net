Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmTalles
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmTalles

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmTalles()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmTalles_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmTalles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        TxtIdTalle.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        FormModoEdicion()
        TxtIdTalle.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este talle?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim idTalle = Convert.ToInt32(filaActual.Cells("idTalle").Value)
            Dim sql = "DELETE FROM Talles WHERE idTalle = @idTalle"
            Dim parametros = CmdParams("@idTalle", idTalle)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click

        Dim talle = TxtTalle.Text.Trim

        If filaActual Is Nothing Then
            ' INSERT
            Dim sql = "INSERT INTO Talles (Talle) VALUES (@Talle)"
            Dim parametros = CmdParams("@Talle", If(String.IsNullOrEmpty(talle), DBNull.Value, talle))
            DSM.Execute(DSM.Personal, sql, parametros, True)
        Else
            ' UPDATE
            If TxtTalle.Text.Trim = "" Then
                MessageBox.Show("El campo Talle no puede estar vacío.")
                TxtTalle.Focus()
                Return
            End If

            Dim idTalle = Convert.ToInt32(filaActual.Cells("idTalle").Value)
            Dim sql = "UPDATE Talles SET Talle = @Talle WHERE idTalle = @idTalle"
            Dim parametros = CmdParams("@Talle", If(String.IsNullOrEmpty(talle), DBNull.Value, talle), "@idTalle", idTalle)
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

    ''' <summary>
    ''' Obtiene los talles filtrados por texto de búsqueda.
    ''' Si el texto de búsqueda es numérico, también filtra por idTalle.
    ''' Selecciona la primera fila si hay resultados, o se limpia el formulario si no hay resultados.
    ''' </summary>
    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT idTalle, Talle FROM Talles"

        ' Lista de parámetros como pares nombre-valor
        Dim parametros As New List(Of Object)

        ' Si se especifica un texto de búsqueda, agregar condiciones al SQL
        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE ( Talle LIKE @Talle "
            parametros.Add("@Talle")
            parametros.Add($"%{texto}%")


            Dim esNumero As Boolean = Integer.TryParse(texto, 0)
            If esNumero Then
                sql &= " OR idTalle = @idTalle "
                parametros.Add("@idTalle")
                parametros.Add(Convert.ToInt32(texto))
            End If

            sql &= ")"
        End If

        sql &= " ORDER BY idTalle"

        Dim result = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = result

        ' si no hay resultados, limpiar la selección
        If result.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        ' Si hay resultados, seleccionar la primera fila
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

    ''' <summary>
    ''' Configura las columnas del DataGridView DgvListado.
    ''' Establece visibilidad, encabezados y anchos de columnas.
    ''' </summary>
    Public Sub GridConfigurarColumnas()
        DgvListado.Columns("idTalle").HeaderText = "Cod Talle"
        DgvListado.Columns("Talle").HeaderText = "Talle"

        DgvListado.Columns("idTalle").Width = 80
        DgvListado.Columns("Talle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ConfigurarEstiloGrid(DgvListado)
    End Sub

    ''' <summary>
    ''' Limpia los campos de entrada del formulario.
    ''' Se utiliza para limpiar la selección actual y preparar el formulario para una nueva entrada.
    ''' </summary>
    Private Sub FormLimpiarSeleccionado()
        TxtIdTalle.Text = String.Empty
        TxtTalle.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene los valores de la fila seleccionada y los muestra en los controles del formulario.
    ''' Se utiliza para mostrar los detalles del talle seleccionado en modo edición.
    ''' </summary>
    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtIdTalle.Text = filaActual.Cells("idTalle").Value.ToString()
            TxtTalle.Text = If(filaActual.Cells("Talle").Value IsNot DBNull.Value, filaActual.Cells("Talle").Value.ToString(), String.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para comenzar una edicion.
    ''' </summary>
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, TxtIdTalle, TxtTalle)
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para aceptar o cancelar una edicion.
    ''' </summary>
    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, TxtIdTalle, TxtTalle)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
    End Sub

End Class