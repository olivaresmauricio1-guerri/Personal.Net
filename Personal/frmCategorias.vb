Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmCategorias
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmCategorias

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmCategorias()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmCategorias_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmCategorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        TxtIdCategoria.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        FormModoEdicion()
        TxtIdCategoria.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar esta categoría?", "Confirmar borrado", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim IdCategoria = Convert.ToInt32(filaActual.Cells("IdCategoria").Value)
            Dim sql = "DELETE FROM Categorias WHERE IdCategoria = @IdCategoria"
            Dim parametros = CmdParams("@IdCategoria", IdCategoria)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click

        Dim orden = ""
        Dim escala1 = ""
        Dim escala2 = ""
        Dim descripcion = TxtDescripcion.Text.Trim
        Dim ordenInt = 0


        If filaActual Is Nothing Then
            ' INSERT
            Dim sql = "INSERT INTO Categorias (Orden, Escala1, Escala2,  Descripcion) VALUES (@Orden, @Escala1, @Escala2, @Descripcion)"
            Dim parametros = CmdParams("@Orden", If(String.IsNullOrEmpty(orden), DBNull.Value, ordenInt), "@Escala1", If(String.IsNullOrEmpty(escala1), DBNull.Value, escala1), "@Escala2", If(String.IsNullOrEmpty(escala2), DBNull.Value, escala2), "@Descripcion", If(String.IsNullOrEmpty(descripcion), DBNull.Value, descripcion))
            DSM.Execute(DSM.Personal, sql, parametros, True)
        Else
            ' UPDATE
            If TxtDescripcion.Text.Trim = "" Then
                MessageBox.Show("El campo Descripción no puede estar vacío.")
                TxtDescripcion.Focus()
                Return
            End If

            Dim idCategoria = Convert.ToInt32(filaActual.Cells("IdCategoria").Value)
            Dim sql = "UPDATE Categorias SET  Descripcion = @Descripcion WHERE IdCategoria = @IdCategoria"
            Dim parametros = CmdParams("@Descripcion", If(String.IsNullOrEmpty(descripcion), DBNull.Value, descripcion), "@IdCategoria", idCategoria)
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
    ''' Obtiene las categorías filtradas por texto de búsqueda.
    ''' Si el texto de búsqueda es numérico, también filtra por IdCategoria.
    ''' Selecciona la primera fila si hay resultados, o se limpia el formulario si no hay resultados.
    ''' </summary>
    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT IdCategoria,  Descripcion FROM Categorias"

        ' Lista de parámetros como pares nombre-valor
        Dim parametros As New List(Of Object)

        ' Si se especifica un texto de búsqueda, agregar condiciones al SQL
        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE ( Descripcion LIKE @Descripcion "
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")


            Dim esNumero As Boolean = Integer.TryParse(texto, 0)
            If esNumero Then
                sql &= " OR IdCategoria = @IdCategoria "
                parametros.Add("@IdCategoria")
                parametros.Add(Convert.ToInt32(texto))
            End If

            sql &= ")"
        End If

        sql &= " ORDER BY IdCategoria"

        Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = tabla

        ' si no hay resultados, limpiar la selección
        If tabla.Rows.Count = 0 Then
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
        DgvListado.Columns("IdCategoria").HeaderText = "Cod Categoría"
        DgvListado.Columns("Descripcion").HeaderText = "Descripción"

        DgvListado.Columns("IdCategoria").Width = 80
        DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ConfigurarEstiloGrid(DgvListado)
    End Sub

    ''' <summary>
    ''' Limpia los campos de entrada del formulario.
    ''' Se utiliza para limpiar la selección actual y preparar el formulario para una nueva entrada.
    ''' </summary>
    Private Sub FormLimpiarSeleccionado()
        TxtIdCategoria.Text = String.Empty
        TxtDescripcion.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene los valores de la fila seleccionada y los muestra en los controles del formulario.
    ''' Se utiliza para mostrar los detalles de la categoría seleccionada en modo edición.
    ''' </summary>
    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtIdCategoria.Text = filaActual.Cells("IdCategoria").Value.ToString()
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value, filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para comenzar una edicion.
    ''' </summary>
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, TxtIdCategoria, TxtDescripcion)
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para aceptar o cancelar una edicion.
    ''' </summary>
    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, TxtIdCategoria, TxtDescripcion)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
    End Sub

End Class