Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmEncargados
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmEncargados

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmEncargados()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmEncargados_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmEncargados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        TxtEncargado.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        FormModoEdicion()
        TxtInstituto.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este encargado?", "Confirmar borrado", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim Id = Convert.ToInt32(filaActual.Cells("Id").Value)
            Dim sql = "DELETE FROM Encargados WHERE Id = @Id"
            Dim parametros = CmdParams("@Id", Id)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim instituto = TxtInstituto.Text.Trim
        Dim encargado = TxtEncargado.Text.Trim
        Dim oficina = TxtOficina.Text.Trim
        Dim telefono = TxtTelefono.Text.Trim

        ' Validaciones básicas
        If String.IsNullOrEmpty(instituto) Then
            MessageBox.Show("El campo Instituto no puede estar vacío.")
            TxtInstituto.Focus()
            Return
        End If

        If String.IsNullOrEmpty(encargado) Then
            MessageBox.Show("El campo Encargado no puede estar vacío.")
            TxtEncargado.Focus()
            Return
        End If

        If filaActual Is Nothing Then
            ' INSERT
            Dim sql = "INSERT INTO Encargados (Instituto, Encargado, Oficina, Telefono) VALUES (@Instituto, @Encargado, @Oficina, @Telefono)"
            Dim parametros = CmdParams("@Instituto", instituto, "@Encargado", encargado, "@Oficina", If(String.IsNullOrEmpty(oficina), DBNull.Value, oficina), "@Telefono", If(String.IsNullOrEmpty(telefono), DBNull.Value, telefono))
            DSM.Execute(DSM.Personal, sql, parametros, True)
        Else
            ' UPDATE
            Dim id = Convert.ToInt32(filaActual.Cells("Id").Value)
            Dim sql = "UPDATE Encargados SET Instituto = @Instituto, Encargado = @Encargado, Oficina = @Oficina, Telefono = @Telefono WHERE Id = @Id"
            Dim parametros = CmdParams("@Instituto", instituto, "@Encargado", encargado, "@Oficina", If(String.IsNullOrEmpty(oficina), DBNull.Value, oficina), "@Telefono", If(String.IsNullOrEmpty(telefono), DBNull.Value, telefono), "@Id", id)
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
    ''' Obtiene los encargados filtrados por texto de búsqueda.
    ''' Si el texto de búsqueda es numérico, también filtra por Id.
    ''' Selecciona la primera fila si hay resultados, o se limpia el formulario si no hay resultados.
    ''' </summary>
    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT Id, Instituto, Encargado, Oficina, Telefono FROM Encargados"

        ' Lista de parámetros como pares nombre-valor
        Dim parametros As New List(Of Object)

        ' Si se especifica un texto de búsqueda, agregar condiciones al SQL
        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE (Instituto LIKE @Instituto OR Encargado LIKE @Encargado OR Oficina LIKE @Oficina OR Telefono LIKE @Telefono"
            parametros.Add("@Instituto")
            parametros.Add($"%{texto}%")
            parametros.Add("@Encargado")
            parametros.Add($"%{texto}%")
            parametros.Add("@Oficina")
            parametros.Add($"%{texto}%")
            parametros.Add("@Telefono")
            parametros.Add($"%{texto}%")

            Dim esNumero As Boolean = Integer.TryParse(texto, 0)
            If esNumero Then
                sql &= " OR Id = @Id"
                parametros.Add("@Id")
                parametros.Add(Convert.ToInt32(texto))
            End If

            sql &= ")"
        End If

        sql &= " ORDER BY Id"

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
        DgvListado.Columns("Id").HeaderText = "ID"
        DgvListado.Columns("Instituto").HeaderText = "Sucursal"
        DgvListado.Columns("Encargado").HeaderText = "Encargado"
        DgvListado.Columns("Oficina").HeaderText = "Oficina"
        DgvListado.Columns("Telefono").HeaderText = "Teléfono"

        DgvListado.Columns("Id").Width = 50
        DgvListado.Columns("Instituto").Width = 100
        DgvListado.Columns("Encargado").Width = 200
        DgvListado.Columns("Oficina").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        'DgvListado.Columns("Oficina").Width = 150
        DgvListado.Columns("Telefono").Width = 120

        ConfigurarEstiloGrid(DgvListado)
    End Sub

    ''' <summary>
    ''' Limpia los campos de entrada del formulario.
    ''' Se utiliza para limpiar la selección actual y preparar el formulario para una nueva entrada.
    ''' </summary>
    Private Sub FormLimpiarSeleccionado()
        TxtId.Text = String.Empty
        TxtInstituto.Text = String.Empty
        TxtEncargado.Text = String.Empty
        TxtOficina.Text = String.Empty
        TxtTelefono.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene los valores de la fila seleccionada y los muestra en los controles del formulario.
    ''' Se utiliza para mostrar los detalles del encargado seleccionado en modo edición.
    ''' </summary>
    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtId.Text = filaActual.Cells("Id").Value.ToString()
            TxtInstituto.Text = If(filaActual.Cells("Instituto").Value IsNot DBNull.Value, filaActual.Cells("Instituto").Value.ToString(), String.Empty)
            TxtEncargado.Text = If(filaActual.Cells("Encargado").Value IsNot DBNull.Value, filaActual.Cells("Encargado").Value.ToString(), String.Empty)
            TxtOficina.Text = If(filaActual.Cells("Oficina").Value IsNot DBNull.Value, filaActual.Cells("Oficina").Value.ToString(), String.Empty)
            TxtTelefono.Text = If(filaActual.Cells("Telefono").Value IsNot DBNull.Value, filaActual.Cells("Telefono").Value.ToString(), String.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para comenzar una edicion.
    ''' </summary>
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, TxtInstituto, TxtEncargado, TxtOficina, TxtTelefono)
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para aceptar o cancelar una edicion.
    ''' </summary>
    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, TxtInstituto, TxtEncargado, TxtOficina, TxtTelefono)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
    End Sub

End Class