Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmTipoInasistencias
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmTipoInasistencias

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmTipoInasistencias()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmInasistencias_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmInasistencias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        TxtCodigo.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        FormModoEdicion()
        TxtDescripcion.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar esta inasistencia?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim Codigo = Convert.ToInt32(filaActual.Cells("Codigo").Value)
            Dim sql = "DELETE FROM Inasistencias WHERE Codigo = @Codigo"
            Dim parametros = CmdParams("@Codigo", Codigo)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim codigo = TxtCodigo.Text.Trim
        Dim descripcion = TxtDescripcion.Text.Trim
        Dim articulo = TxtArticulo.Text.Trim
        Dim inciso = TxtInciso.Text.Trim
        Dim punto = TxtPunto.Text.Trim
        Dim ano = TxtAno.Text.Trim
        Dim mes = TxtMes.Text.Trim
        Dim anexo = TxtAnexo.Text.Trim
        Dim anioDto = TxtAnioDto.Text.Trim
        Dim decreto = TxtDecreto.Text.Trim

        ' Validaciones básicas
        If String.IsNullOrEmpty(descripcion) Then
            MessageBox.Show("El campo Descripción no puede estar vacío.")
            TxtDescripcion.Focus()
            Return
        End If

        If filaActual Is Nothing Then
            ' INSERT - Validar que el código sea obligatorio y único
            If String.IsNullOrEmpty(codigo) Then
                MessageBox.Show("El campo Código no puede estar vacío.")
                TxtCodigo.Focus()
                Return
            End If

            Dim codigoInt As Integer
            If Not Integer.TryParse(codigo, codigoInt) Then
                MessageBox.Show("El código debe ser un número entero válido.")
                TxtCodigo.Focus()
                Return
            End If

            ' Verificar que el código no exista
            Dim parametrosExiste As New List(Of Object)
            Dim sqlVerificar = "SELECT COUNT(*) FROM Inasistencias WHERE Codigo = @Codigo"
            parametrosExiste.Add("@Codigo")
            parametrosExiste.Add(codigo)

            Dim tabla = DSM.ExecuteQuery(DSM.Personal, sqlVerificar, CmdParams(parametrosExiste.ToArray()))
            Dim existe = Convert.ToInt32(tabla.Rows(0)(0))

            If existe > 0 Then
                MessageBox.Show("Ya existe una inasistencia con ese código. Por favor, ingrese un código diferente.")
                TxtCodigo.Focus()
                Return
            End If

            Dim sql = "INSERT INTO Inasistencias (Codigo, Descripcion, Articulo, Inciso, Punto, Goce, Año, Mes, Corrido, Habil, Mensual, Anexo, AnioDto, Decreto, Sale) VALUES (@Codigo, @Descripcion, @Articulo, @Inciso, @Punto, @Goce, @Ano, @Mes, @Corrido, @Habil, @Mensual, @Anexo, @AnioDto, @Decreto, @Sale)"
            Dim parametros = CmdParams("@Codigo", codigoInt, "@Descripcion", descripcion, "@Articulo", If(String.IsNullOrEmpty(articulo), DBNull.Value, articulo), "@Inciso", If(String.IsNullOrEmpty(inciso), DBNull.Value, inciso), "@Punto", If(String.IsNullOrEmpty(punto), DBNull.Value, punto), "@Goce", chkGoce.Checked, "@Ano", If(String.IsNullOrEmpty(ano), DBNull.Value, Convert.ToInt32(ano)), "@Mes", If(String.IsNullOrEmpty(mes), DBNull.Value, Convert.ToInt32(mes)), "@Corrido", chkCorrido.Checked, "@Habil", chkHabil.Checked, "@Mensual", chkMensual.Checked, "@Anexo", If(String.IsNullOrEmpty(anexo), DBNull.Value, Convert.ToInt32(anexo)), "@AnioDto", If(String.IsNullOrEmpty(anioDto), DBNull.Value, Convert.ToInt32(anioDto)), "@Decreto", If(String.IsNullOrEmpty(decreto), DBNull.Value, decreto), "@Sale", chkSale.Checked)
            DSM.Execute(DSM.Personal, sql, parametros, True)
        Else
            ' UPDATE
            Dim codigoActual = Convert.ToInt32(filaActual.Cells("Codigo").Value)
            Dim sql = "UPDATE Inasistencias SET Descripcion = @Descripcion, Articulo = @Articulo, Inciso = @Inciso, Punto = @Punto, Goce = @Goce, Año = @Ano, Mes = @Mes, Corrido = @Corrido, Habil = @Habil, Mensual = @Mensual, Anexo = @Anexo, AnioDto = @AnioDto, Decreto = @Decreto, Sale = @Sale WHERE Codigo = @Codigo"
            Dim parametros = CmdParams("@Descripcion", descripcion, "@Articulo", If(String.IsNullOrEmpty(articulo), DBNull.Value, articulo), "@Inciso", If(String.IsNullOrEmpty(inciso), DBNull.Value, inciso), "@Punto", If(String.IsNullOrEmpty(punto), DBNull.Value, punto), "@Goce", chkGoce.Checked, "@Ano", If(String.IsNullOrEmpty(ano), DBNull.Value, Convert.ToInt32(ano)), "@Mes", If(String.IsNullOrEmpty(mes), DBNull.Value, Convert.ToInt32(mes)), "@Corrido", chkCorrido.Checked, "@Habil", chkHabil.Checked, "@Mensual", chkMensual.Checked, "@Anexo", If(String.IsNullOrEmpty(anexo), DBNull.Value, Convert.ToInt32(anexo)), "@AnioDto", If(String.IsNullOrEmpty(anioDto), DBNull.Value, Convert.ToInt32(anioDto)), "@Decreto", If(String.IsNullOrEmpty(decreto), DBNull.Value, decreto), "@Sale", chkSale.Checked, "@Codigo", codigoActual)
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
    ''' Obtiene las inasistencias filtradas por texto de búsqueda.
    ''' Si el texto de búsqueda es numérico, también filtra por Codigo.
    ''' Selecciona la primera fila si hay resultados, o se limpia el formulario si no hay resultados.
    ''' </summary>
    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT Codigo, Descripcion, Articulo, Inciso, Punto, Goce, Año, Mes, Corrido, Habil, Mensual, Anexo, AnioDto, Decreto, Sale FROM Inasistencias"

        ' Lista de parámetros como pares nombre-valor
        Dim parametros As New List(Of Object)

        ' Si se especifica un texto de búsqueda, agregar condiciones al SQL
        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE (Descripcion LIKE @Descripcion OR Articulo LIKE @Articulo OR Decreto LIKE @Decreto"
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")
            parametros.Add("@Articulo")
            parametros.Add($"%{texto}%")
            parametros.Add("@Decreto")
            parametros.Add($"%{texto}%")

            Dim esNumero As Boolean = Integer.TryParse(texto, 0)
            If esNumero Then
                sql &= " OR Codigo = @Codigo"
                parametros.Add("@Codigo")
                parametros.Add(Convert.ToInt32(texto))
            End If

            sql &= ")"
        End If

        sql &= " ORDER BY Codigo"

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
        DgvListado.Columns("Codigo").HeaderText = "Código"
        DgvListado.Columns("Descripcion").HeaderText = "Descripción"
        DgvListado.Columns("Articulo").HeaderText = "Artículo"
        DgvListado.Columns("Inciso").HeaderText = "Inciso"
        DgvListado.Columns("Punto").HeaderText = "Punto"
        DgvListado.Columns("Goce").HeaderText = "Goce"
        DgvListado.Columns("Año").HeaderText = "Año"
        DgvListado.Columns("Mes").HeaderText = "Mes"
        DgvListado.Columns("Corrido").HeaderText = "Corrido"
        DgvListado.Columns("Habil").HeaderText = "Hábil"
        DgvListado.Columns("Mensual").HeaderText = "Mensual"
        DgvListado.Columns("Anexo").HeaderText = "Anexo"
        DgvListado.Columns("AnioDto").HeaderText = "Año Dto"
        DgvListado.Columns("Decreto").HeaderText = "Decreto"
        DgvListado.Columns("Sale").HeaderText = "Sale"

        DgvListado.Columns("Codigo").Width = 60
        DgvListado.Columns("Descripcion").Width = 200
        DgvListado.Columns("Articulo").Width = 80
        DgvListado.Columns("Inciso").Width = 50
        DgvListado.Columns("Punto").Width = 80
        DgvListado.Columns("Goce").Width = 50
        DgvListado.Columns("Año").Width = 50
        DgvListado.Columns("Mes").Width = 50
        DgvListado.Columns("Corrido").Width = 60
        DgvListado.Columns("Habil").Width = 50
        DgvListado.Columns("Mensual").Width = 60
        DgvListado.Columns("Anexo").Width = 60
        DgvListado.Columns("AnioDto").Width = 70
        DgvListado.Columns("Decreto").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DgvListado.Columns("Sale").Width = 50

        ConfigurarEstiloGrid(DgvListado)
    End Sub

    ''' <summary>
    ''' Limpia los campos de entrada del formulario.
    ''' Se utiliza para limpiar la selección actual y preparar el formulario para una nueva entrada.
    ''' </summary>
    Private Sub FormLimpiarSeleccionado()
        TxtCodigo.Text = String.Empty
        TxtDescripcion.Text = String.Empty
        TxtArticulo.Text = String.Empty
        TxtInciso.Text = String.Empty
        TxtPunto.Text = String.Empty
        chkGoce.Checked = False
        TxtAno.Text = String.Empty
        TxtMes.Text = String.Empty
        chkCorrido.Checked = False
        chkHabil.Checked = False
        chkMensual.Checked = False
        TxtAnexo.Text = String.Empty
        TxtAnioDto.Text = String.Empty
        TxtDecreto.Text = String.Empty
        chkSale.Checked = False
    End Sub

    ''' <summary>
    ''' Obtiene los valores de la fila seleccionada y los muestra en los controles del formulario.
    ''' Se utiliza para mostrar los detalles de la inasistencia seleccionada en modo edición.
    ''' </summary>
    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtCodigo.Text = filaActual.Cells("Codigo").Value.ToString()
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value, filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
            TxtArticulo.Text = If(filaActual.Cells("Articulo").Value IsNot DBNull.Value, filaActual.Cells("Articulo").Value.ToString(), String.Empty)
            TxtInciso.Text = If(filaActual.Cells("Inciso").Value IsNot DBNull.Value, filaActual.Cells("Inciso").Value.ToString(), String.Empty)
            TxtPunto.Text = If(filaActual.Cells("Punto").Value IsNot DBNull.Value, filaActual.Cells("Punto").Value.ToString(), String.Empty)
            chkGoce.Checked = If(filaActual.Cells("Goce").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Goce").Value), False)
            TxtAno.Text = If(filaActual.Cells("Año").Value IsNot DBNull.Value, filaActual.Cells("Año").Value.ToString(), String.Empty)
            TxtMes.Text = If(filaActual.Cells("Mes").Value IsNot DBNull.Value, filaActual.Cells("Mes").Value.ToString(), String.Empty)
            chkCorrido.Checked = If(filaActual.Cells("Corrido").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Corrido").Value), False)
            chkHabil.Checked = If(filaActual.Cells("Habil").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Habil").Value), False)
            chkMensual.Checked = If(filaActual.Cells("Mensual").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Mensual").Value), False)
            TxtAnexo.Text = If(filaActual.Cells("Anexo").Value IsNot DBNull.Value, filaActual.Cells("Anexo").Value.ToString(), String.Empty)
            TxtAnioDto.Text = If(filaActual.Cells("AnioDto").Value IsNot DBNull.Value, filaActual.Cells("AnioDto").Value.ToString(), String.Empty)
            TxtDecreto.Text = If(filaActual.Cells("Decreto").Value IsNot DBNull.Value, filaActual.Cells("Decreto").Value.ToString(), String.Empty)
            chkSale.Checked = If(filaActual.Cells("Sale").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Sale").Value), False)
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para comenzar una edicion.
    ''' </summary>
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, TxtCodigo, TxtDescripcion, TxtArticulo, TxtInciso, TxtPunto, chkGoce, TxtAno, TxtMes, chkCorrido, chkHabil, chkMensual, TxtAnexo, TxtAnioDto, TxtDecreto, chkSale)
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para aceptar o cancelar una edicion.
    ''' </summary>
    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, TxtDescripcion, TxtArticulo, TxtInciso, TxtPunto, chkGoce, TxtAno, TxtMes, chkCorrido, chkHabil, chkMensual, TxtAnexo, TxtAnioDto, TxtDecreto, chkSale)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)

        ' En modo edición, TxtCodigo solo es editable cuando se agrega un nuevo registro
        TxtCodigo.Enabled = (filaActual Is Nothing)
    End Sub

End Class