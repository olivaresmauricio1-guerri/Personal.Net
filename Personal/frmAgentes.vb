Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmAgentes
    Private tabla As New DataTable()
    Private tablaGrupoFamiliar As New DataTable()
    Private tablaComentarios As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmAgentes

    ' Variables para gestión de grupo familiar
    Private filaGrupoActual As DataGridViewRow
    Private filaGrupoActualIndice As Integer = -1

    ' Variables para gestión de comentarios
    Private filaComentarioActual As DataGridViewRow
    Private filaComentarioActualIndice As Integer = -1

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmAgentes()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmAgentes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmAgentes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormModoConsulta()
        GridBuscar()
        GridConfigurarColumnas()
        CargarComboBoxes()
        Me.KeyPreview = True
    End Sub

    Private Sub CargarComboBoxes()
        Try
            ' Cargar Institutos/Sucursales
            CargarCombos(cmbInstituto, "Institutos", "Descripcion", "Descripcion")

            ' Cargar Encargados/Jefes
            CargarCombos(cmbJefe, "Encargados", "Encargado", "Encargado")

            ' Cargar Categorias
            CargarCombos(cmbCategoria, "Categorias", "Descripcion", "Descripcion")

            ' Cargar Escalafones
            CargarCombos(cmbEscalafon, "Escalafon", "Descripcion", "Descripcion")

            ' Cargar Estado Parental
            CargarCombos(cmbEstadoParental, "EstadoParental", "Estado", "Estado")

            ' Cargar Caracter
            CargarCombos(cmbCaracter, "Caracter", "Descripcion", "Descripcion")

            ' Cargar Motivos de Desvinculacion
            CargarCombos(CmbMotivo, "Motivos", "Motivo", "Motivo")

            ' Motivos para Comentarios
            CargarCombos(cmbMotivoComentario, "Inasistencias", "Descripcion", "Descripcion")

            ' Configurar ComboBoxes con valores fijos
            ConfigurarComboBoxesFijos()

        Catch ex As Exception
            MessageBox.Show("Error al cargar datos de los ComboBoxes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ConfigurarComboBoxesFijos()
        ' Sexo
        cmbSexo.Items.Clear()
        cmbSexo.Items.AddRange({"M", "F"})

        ' Tipo de Documento
        cmbTipoDto.Items.Clear()
        cmbTipoDto.Items.AddRange({"DNI", "LC", "LE", "CI", "PAS"})

        ' Horas Diarias
        cmbHorasDiarias.Items.Clear()
        cmbHorasDiarias.Items.AddRange({"4", "6", "8", "Tiempo Completo", "Dedicación Full Time"})

    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        Try
            If TxtBuscar.Text.Trim = "" Then
                GridBuscar()
            Else
                Dim filtro As String = TxtBuscar.Text.Trim.ToUpper()
                Dim sql As String = "SELECT Legajo, Nombre, Instituto, Secretaria, Escalafon, Jefe, Caracter " &
                                  "FROM Agentes " &
                                  "WHERE UPPER(Nombre) LIKE '%" & filtro & "%' " &
                                  "OR UPPER(Instituto) LIKE '%" & filtro & "%' " &
                                  "OR UPPER(Secretaria) LIKE '%" & filtro & "%' " &
                                  "OR UPPER(Escalafon) LIKE '%" & filtro & "%' " &
                                  "OR UPPER(Jefe) LIKE '%" & filtro & "%' " &
                                  "OR UPPER(Caracter) LIKE '%" & filtro & "%' " &
                                  "OR CAST(Legajo AS VARCHAR) LIKE '%" & filtro & "%' " &
                                  "ORDER BY Nombre"
                
                tabla = DSM.ExecuteQuery(DSM.Personal, sql)
                DgvListado.DataSource = tabla
                
                If tabla.Rows.Count = 0 Then
                    FormLimpiarSeleccionado()
                End If
            End If
            FormModoConsulta()
        Catch ex As Exception
            MessageBox.Show("Error en la búsqueda: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
        Try
            DgvListado.ColumnHeadersVisible = chkEncabezados.Checked
            DgvListado.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al cambiar encabezados: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Eventos de botones principales
    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
        txtLegajo.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        FormModoEdicion()
        txtNombre.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este agente?", "Confirmar borrado", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim legajo = Convert.ToInt32(filaActual.Cells("Legajo").Value)

            ' Eliminar registros relacionados primero
            Dim sqlGrupo = "DELETE FROM GrupoFamiliar WHERE Legajo = @Legajo"
            Dim sqlComentarios = "DELETE FROM Comentarios WHERE Legajo = @Legajo"
            Dim sqlAgente = "DELETE FROM Agentes WHERE Legajo = @Legajo"

            Dim parametros = CmdParams("@Legajo", legajo)

            DSM.Execute(DSM.Personal, sqlGrupo, parametros, True)
            DSM.Execute(DSM.Personal, sqlComentarios, parametros, True)
            DSM.Execute(DSM.Personal, sqlAgente, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Not ValidarDatos() Then Return

        Try
            If filaActual Is Nothing Then
                ' INSERT
                InsertarNuevoAgente()
            Else
                ' UPDATE
                ActualizarAgente()
            End If

            FormModoConsulta()
            GridBuscar()
            MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        FormModoConsulta()
        If filaActual IsNot Nothing Then
            FormObtenerSeleccionado()
        Else
            FormLimpiarSeleccionado()
        End If
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        Try
            If DgvListado.SelectedRows.Count > 0 Then
                Dim texto As String = ""

                ' Agregar encabezados si están habilitados
                If chkEncabezados.Checked Then
                    For i As Integer = 0 To DgvListado.Columns.Count - 1
                        If i > 0 Then texto += vbTab
                        texto += DgvListado.Columns(i).HeaderText
                    Next
                    texto += vbCrLf
                End If

                ' Agregar filas seleccionadas
                For Each row As DataGridViewRow In DgvListado.SelectedRows
                    For i As Integer = 0 To row.Cells.Count - 1
                        If i > 0 Then texto += vbTab
                        texto += If(row.Cells(i).Value IsNot Nothing, row.Cells(i).Value.ToString(), "")
                    Next
                    texto += vbCrLf
                Next

                If texto.Length > 0 Then
                    Clipboard.SetText(texto)
                    MessageBox.Show("Datos copiados al portapapeles", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Seleccione al menos una fila para copiar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al copiar datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Métodos de validación
    Private Function ValidarDatos() As Boolean
        ' Validar Legajo
        If String.IsNullOrEmpty(txtLegajo.Text.Trim) Then
            MessageBox.Show("El campo Legajo no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLegajo.Focus()
            Return False
        End If

        ' Validar que el legajo sea numérico
        Dim legajo As Integer
        If Not Integer.TryParse(txtLegajo.Text.Trim, legajo) Then
            MessageBox.Show("El Legajo debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLegajo.Focus()
            Return False
        End If

        ' Validar Nombre
        If String.IsNullOrEmpty(txtNombre.Text.Trim) Then
            MessageBox.Show("El campo Nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombre.Focus()
            Return False
        End If

        ' Validar Instituto
        If String.IsNullOrEmpty(cmbInstituto.Text.Trim) Then
            MessageBox.Show("Debe seleccionar un Instituto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbInstituto.Focus()
            Return False
        End If

        Return True
    End Function

    ' Métodos CRUD para Agentes
    Private Sub InsertarNuevoAgente()
        Dim sql As String = "INSERT INTO Agentes (Legajo, TipoDto, NroDto, CargoPampa, Cargo, Instituto, Nombre, CorreroE, Sexo, Nacimiento, " &
                           "Calle, Nro, Localidad, Oficina, Critico, MayorDedicacion, HorasDedicacion, HorasSemanales, HorasDiarias, " &
                           "Escalafon, Jefe, LicAnual, Caracter, Comentario, Nomarca, Secretaria, Telefono, Interno, Celular, Rpv, " &
                           "iNGRESO, Baja, CUIL, TITULO, UltimaActualizacion, Motivo, EstadoParental, FechaJubilacion, MarcaAqui) " &
                           "VALUES (@Legajo, @TipoDto, @NroDto, @CargoPampa, @Cargo, @Instituto, @Nombre, @CorreroE, @Sexo, @Nacimiento, " &
                           "@Calle, @Nro, @Localidad, @Oficina, @Critico, @MayorDedicacion, @HorasDedicacion, @HorasSemanales, @HorasDiarias, " &
                           "@Escalafon, @Jefe, @LicAnual, @Caracter, @Comentario, @Nomarca, @Secretaria, @Telefono, @Interno, @Celular, @Rpv, " &
                           "@iNGRESO, @Baja, @CUIL, @TITULO, @UltimaActualizacion, @Motivo, @EstadoParental, @FechaJubilacion, @MarcaAqui)"

        Dim parametros = ObtenerParametrosAgente()
        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Sub ActualizarAgente()
        Dim sql As String = "UPDATE Agentes SET TipoDto=@TipoDto, NroDto=@NroDto, CargoPampa=@CargoPampa, Cargo=@Cargo, Instituto=@Instituto, " &
                           "Nombre=@Nombre, CorreroE=@CorreroE, Sexo=@Sexo, Nacimiento=@Nacimiento, Calle=@Calle, Nro=@Nro, " &
                           "Localidad=@Localidad, Oficina=@Oficina, Critico=@Critico, MayorDedicacion=@MayorDedicacion, " &
                           "HorasDedicacion=@HorasDedicacion, HorasSemanales=@HorasSemanales, HorasDiarias=@HorasDiarias, " &
                           "Escalafon=@Escalafon, Jefe=@Jefe, LicAnual=@LicAnual, Caracter=@Caracter, Comentario=@Comentario, " &
                           "Nomarca=@Nomarca, Secretaria=@Secretaria, Telefono=@Telefono, Interno=@Interno, Celular=@Celular, " &
                           "Rpv=@Rpv, iNGRESO=@iNGRESO, Baja=@Baja, CUIL=@CUIL, TITULO=@TITULO, UltimaActualizacion=@UltimaActualizacion, " &
                           "Motivo=@Motivo, EstadoParental=@EstadoParental, FechaJubilacion=@FechaJubilacion, MarcaAqui=@MarcaAqui " &
                           "WHERE Legajo=@Legajo"

        Dim parametros = ObtenerParametrosAgente()
        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Function ObtenerParametrosAgente() As Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object) From {
            {"@Legajo", If(String.IsNullOrEmpty(txtLegajo.Text.Trim), DBNull.Value, Convert.ToInt32(txtLegajo.Text.Trim))},
            {"@TipoDto", If(String.IsNullOrEmpty(cmbTipoDto.Text.Trim), DBNull.Value, cmbTipoDto.Text.Trim)},
            {"@NroDto", If(String.IsNullOrEmpty(txtNroDto.Text.Trim), DBNull.Value, If(Integer.TryParse(txtNroDto.Text.Trim, 0), Convert.ToInt32(txtNroDto.Text.Trim), DBNull.Value))},
            {"@CargoPampa", If(String.IsNullOrEmpty(txtCargoPampa.Text.Trim), DBNull.Value, If(Integer.TryParse(txtCargoPampa.Text.Trim, 0), Convert.ToInt32(txtCargoPampa.Text.Trim), DBNull.Value))},
            {"@Cargo", If(String.IsNullOrEmpty(txtCargo.Text.Trim), DBNull.Value, txtCargo.Text.Trim)},
            {"@Instituto", If(String.IsNullOrEmpty(cmbInstituto.Text.Trim), DBNull.Value, cmbInstituto.Text.Trim)},
            {"@Nombre", If(String.IsNullOrEmpty(txtNombre.Text.Trim), DBNull.Value, txtNombre.Text.Trim)},
            {"@CorreroE", If(String.IsNullOrEmpty(txtCorreoE.Text.Trim), DBNull.Value, txtCorreoE.Text.Trim)},
            {"@Sexo", If(String.IsNullOrEmpty(cmbSexo.Text.Trim), DBNull.Value, cmbSexo.Text.Trim)},
            {"@Nacimiento", If(dtpNacimiento.Value = dtpNacimiento.MinDate, DBNull.Value, dtpNacimiento.Value)},
            {"@Calle", If(String.IsNullOrEmpty(txtCalle.Text.Trim), DBNull.Value, txtCalle.Text.Trim)},
            {"@Nro", If(String.IsNullOrEmpty(txtNro.Text.Trim), DBNull.Value, txtNro.Text.Trim)},
            {"@Localidad", If(String.IsNullOrEmpty(txtLocalidad.Text.Trim), DBNull.Value, txtLocalidad.Text.Trim)},
            {"@Oficina", If(String.IsNullOrEmpty(txtOficina.Text.Trim), DBNull.Value, txtOficina.Text.Trim)},
            {"@Critico", chkCritico.Checked},
            {"@MayorDedicacion", chkMayorDedicacion.Checked},
            {"@HorasDedicacion", If(String.IsNullOrEmpty(txtHorasDedicacion.Text.Trim), DBNull.Value, txtHorasDedicacion.Text.Trim)},
            {"@HorasSemanales", If(String.IsNullOrEmpty(txtHorasSemanales.Text.Trim), DBNull.Value, txtHorasSemanales.Text.Trim)},
            {"@HorasDiarias", If(String.IsNullOrEmpty(cmbHorasDiarias.Text.Trim), DBNull.Value, cmbHorasDiarias.Text.Trim)},
            {"@Escalafon", If(String.IsNullOrEmpty(cmbEscalafon.Text.Trim), DBNull.Value, cmbEscalafon.Text.Trim)},
            {"@Jefe", If(String.IsNullOrEmpty(cmbJefe.Text.Trim), DBNull.Value, cmbJefe.Text.Trim)},
            {"@LicAnual", If(String.IsNullOrEmpty(txtLicAnual.Text.Trim), DBNull.Value, txtLicAnual.Text.Trim)},
            {"@Caracter", If(String.IsNullOrEmpty(cmbCaracter.Text.Trim), DBNull.Value, cmbCaracter.Text.Trim)},
            {"@Comentario", If(String.IsNullOrEmpty(txtComentario.Text.Trim), DBNull.Value, txtComentario.Text.Trim)},
            {"@Nomarca", chkNomarca.Checked},
            {"@Secretaria", If(String.IsNullOrEmpty(cmbCategoria.Text.Trim), DBNull.Value, cmbCategoria.Text.Trim)},
            {"@Telefono", If(String.IsNullOrEmpty(txtTelefono.Text.Trim), DBNull.Value, txtTelefono.Text.Trim)},
            {"@Interno", If(String.IsNullOrEmpty(txtInterno.Text.Trim), DBNull.Value, txtInterno.Text.Trim)},
            {"@Celular", If(String.IsNullOrEmpty(txtCelular.Text.Trim), DBNull.Value, txtCelular.Text.Trim)},
            {"@Rpv", If(String.IsNullOrEmpty(txtRpv.Text.Trim), DBNull.Value, txtRpv.Text.Trim)},
            {"@iNGRESO", If(String.IsNullOrEmpty(txtIngreso.Text.Trim), DBNull.Value, txtIngreso.Text.Trim)},
            {"@Baja", If(String.IsNullOrEmpty(txtBaja.Text.Trim), DBNull.Value, txtBaja.Text.Trim)},
            {"@CUIL", If(String.IsNullOrEmpty(txtCUIL.Text.Trim), DBNull.Value, txtCUIL.Text.Trim)},
            {"@TITULO", If(String.IsNullOrEmpty(txtTitulo.Text.Trim), DBNull.Value, txtTitulo.Text.Trim)},
            {"@UltimaActualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"@Motivo", If(String.IsNullOrEmpty(txtMotivo.Text.Trim), DBNull.Value, txtMotivo.Text.Trim)},
            {"@EstadoParental", If(String.IsNullOrEmpty(cmbEstadoParental.Text.Trim), DBNull.Value, cmbEstadoParental.Text.Trim)},
            {"@FechaJubilacion", If(String.IsNullOrEmpty(txtFechaJubilacion.Text.Trim), DBNull.Value, txtFechaJubilacion.Text.Trim)},
            {"@MarcaAqui", chkMarcaAqui.Checked}
        }
    End Function

    ' Métodos CRUD para GrupoFamiliar
    Private Sub InsertarGrupoFamiliar(legajo As Integer, nombre As String, parentesco As String, nacimiento As DateTime?, edad As Integer?, ocupacion As String, nivel As String)
        Dim sql As String = "INSERT INTO GrupoFamiliar (Legajo, Nombre, Parentesco, Nacimiento, Edad, Ocupacion, Nivel) " &
                           "VALUES (@Legajo, @Nombre, @Parentesco, @Nacimiento, @Edad, @Ocupacion, @Nivel)"

        Dim parametros = CmdParams(
            "@Legajo", legajo,
            "@Nombre", If(String.IsNullOrEmpty(nombre), DBNull.Value, nombre),
            "@Parentesco", If(String.IsNullOrEmpty(parentesco), DBNull.Value, parentesco),
            "@Nacimiento", If(nacimiento.HasValue, nacimiento.Value, DBNull.Value),
            "@Edad", If(edad.HasValue, edad.Value, DBNull.Value),
            "@Ocupacion", If(String.IsNullOrEmpty(ocupacion), DBNull.Value, ocupacion),
            "@Nivel", If(String.IsNullOrEmpty(nivel), DBNull.Value, nivel)
        )

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    ' Métodos CRUD para Comentarios
    Private Sub InsertarComentario(legajo As Integer, fecha As DateTime, comenta As String, motivo As String)
        Dim sql As String = "INSERT INTO Comentarios (Legajo, Fecha, Comenta, Motivo) " &
                           "VALUES (@Legajo, @Fecha, @Comenta, @Motivo)"

        Dim parametros = CmdParams(
            "@Legajo", legajo,
            "@Fecha", fecha,
            "@Comenta", If(String.IsNullOrEmpty(comenta), DBNull.Value, comenta),
            "@Motivo", If(String.IsNullOrEmpty(motivo), DBNull.Value, motivo)
        )

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    ' Métodos auxiliares
    Private Sub LimpiarFormulario()
        ' Limpiar campos de texto
        txtLegajo.Clear()
        txtNombre.Clear()
        txtCorreoE.Clear()
        txtCalle.Clear()
        txtNro.Clear()
        txtLocalidad.Clear()
        txtOficina.Clear()
        txtHorasDedicacion.Clear()
        txtHorasSemanales.Clear()
        txtLicAnual.Clear()
        txtComentario.Clear()
        txtTelefono.Clear()
        txtInterno.Clear()
        txtCelular.Clear()
        txtRpv.Clear()
        txtIngreso.Clear()
        txtBaja.Clear()
        txtCUIL.Clear()
        txtTitulo.Clear()
        txtMotivo.Clear()
        txtFechaJubilacion.Clear()
        txtNroDto.Clear()
        txtCargoPampa.Clear()
        txtCargo.Clear()

        ' Limpiar ComboBoxes
        cmbInstituto.SelectedIndex = -1
        cmbSexo.SelectedIndex = -1
        cmbHorasDiarias.SelectedIndex = -1
        cmbEscalafon.SelectedIndex = -1
        cmbJefe.SelectedIndex = -1
        cmbCaracter.SelectedIndex = -1
        cmbCategoria.SelectedIndex = -1
        cmbEstadoParental.SelectedIndex = -1
        cmbTipoDto.SelectedIndex = -1

        ' Limpiar CheckBoxes
        chkCritico.Checked = False
        chkMayorDedicacion.Checked = False
        chkNomarca.Checked = False
        chkMarcaAqui.Checked = False

        ' Resetear DateTimePicker
        dtpNacimiento.Value = DateTime.Now
    End Sub

    Private Sub CargarDatosEnFormulario(row As DataRow)
        If row IsNot Nothing Then
            txtLegajo.Text = If(IsDBNull(row("Legajo")), "", row("Legajo").ToString())
            txtNombre.Text = If(IsDBNull(row("Nombre")), "", row("Nombre").ToString())
            cmbInstituto.Text = If(IsDBNull(row("Instituto")), "", row("Instituto").ToString())
            txtCorreoE.Text = If(IsDBNull(row("CorreroE")), "", row("CorreroE").ToString())
            cmbSexo.Text = If(IsDBNull(row("Sexo")), "", row("Sexo").ToString())

            If Not IsDBNull(row("Nacimiento")) Then
                dtpNacimiento.Value = Convert.ToDateTime(row("Nacimiento"))
            End If

            txtCalle.Text = If(IsDBNull(row("Calle")), "", row("Calle").ToString())
            txtNro.Text = If(IsDBNull(row("Nro")), "", row("Nro").ToString())
            txtLocalidad.Text = If(IsDBNull(row("Localidad")), "", row("Localidad").ToString())
            txtOficina.Text = If(IsDBNull(row("Oficina")), "", row("Oficina").ToString())

            chkCritico.Checked = If(IsDBNull(row("Critico")), False, Convert.ToBoolean(row("Critico")))
            chkMayorDedicacion.Checked = If(IsDBNull(row("MayorDedicacion")), False, Convert.ToBoolean(row("MayorDedicacion")))

            txtHorasDedicacion.Text = If(IsDBNull(row("HorasDedicacion")), "", row("HorasDedicacion").ToString())
            txtHorasSemanales.Text = If(IsDBNull(row("HorasSemanales")), "", row("HorasSemanales").ToString())
            cmbHorasDiarias.Text = If(IsDBNull(row("HorasDiarias")), "", row("HorasDiarias").ToString())
            cmbEscalafon.Text = If(IsDBNull(row("Escalafon")), "", row("Escalafon").ToString())
            cmbJefe.Text = If(IsDBNull(row("Jefe")), "", row("Jefe").ToString())
            txtLicAnual.Text = If(IsDBNull(row("LicAnual")), "", row("LicAnual").ToString())
            cmbCaracter.Text = If(IsDBNull(row("Caracter")), "", row("Caracter").ToString())
            txtComentario.Text = If(IsDBNull(row("Comentario")), "", row("Comentario").ToString())

            chkNomarca.Checked = If(IsDBNull(row("Nomarca")), False, Convert.ToBoolean(row("Nomarca")))
            cmbCategoria.Text = If(IsDBNull(row("Secretaria")), "", row("Secretaria").ToString())
            txtTelefono.Text = If(IsDBNull(row("Telefono")), "", row("Telefono").ToString())
            txtInterno.Text = If(IsDBNull(row("Interno")), "", row("Interno").ToString())
            txtCelular.Text = If(IsDBNull(row("Celular")), "", row("Celular").ToString())
            txtRpv.Text = If(IsDBNull(row("Rpv")), "", row("Rpv").ToString())
            txtIngreso.Text = If(IsDBNull(row("iNGRESO")), "", row("iNGRESO").ToString())
            txtBaja.Text = If(IsDBNull(row("Baja")), "", row("Baja").ToString())
            txtCUIL.Text = If(IsDBNull(row("CUIL")), "", row("CUIL").ToString())
            txtTitulo.Text = If(IsDBNull(row("TITULO")), "", row("TITULO").ToString())
            txtMotivo.Text = If(IsDBNull(row("Motivo")), "", row("Motivo").ToString())
            cmbEstadoParental.Text = If(IsDBNull(row("EstadoParental")), "", row("EstadoParental").ToString())
            txtFechaJubilacion.Text = If(IsDBNull(row("FechaJubilacion")), "", row("FechaJubilacion").ToString())

            chkMarcaAqui.Checked = If(IsDBNull(row("MarcaAqui")), False, Convert.ToBoolean(row("MarcaAqui")))
            cmbTipoDto.Text = If(IsDBNull(row("TipoDto")), "", row("TipoDto").ToString())
            txtNroDto.Text = If(IsDBNull(row("NroDto")), "", row("NroDto").ToString())
            txtCargoPampa.Text = If(IsDBNull(row("CargoPampa")), "", row("CargoPampa").ToString())
            txtCargo.Text = If(IsDBNull(row("Cargo")), "", row("Cargo").ToString())
        End If
    End Sub

    Private Sub ConfigurarModoFormulario(modo As String)
        Select Case modo.ToUpper()
            Case "AGREGAR"
                LimpiarFormulario()
                HabilitarControles(True)
                ' Configurar botones para modo agregar
                SetControlesEnabled(False, btnAgregar, btnModificar, btnBorrar)
                SetControlesEnabled(True, btnAceptar, btnCancelar)
                txtLegajo.Focus()

            Case "MODIFICAR"
                HabilitarControles(True)
                txtLegajo.Enabled = False ' No permitir modificar el legajo
                ' Configurar botones para modo modificar
                SetControlesEnabled(False, btnAgregar, btnModificar, btnBorrar)
                SetControlesEnabled(True, btnAceptar, btnCancelar)
                txtNombre.Focus()

            Case "CONSULTA"
                HabilitarControles(False)
                ' Configurar botones para modo consulta
                SetControlesEnabled(True, btnAgregar, btnModificar, btnBorrar)
                SetControlesEnabled(False, btnAceptar, btnCancelar)
        End Select
    End Sub

    Private Sub HabilitarControles(habilitar As Boolean)
        ' Usar la función SetControlesEnabled de Funciones.vb para habilitar/deshabilitar controles
        SetControlesEnabled(habilitar, txtLegajo, txtNombre, cmbInstituto, txtCorreoE, cmbSexo, dtpNacimiento,
                           txtCalle, txtNro, txtLocalidad, txtOficina, chkCritico, chkMayorDedicacion,
                           txtHorasDedicacion, txtHorasSemanales, cmbHorasDiarias, cmbEscalafon, cmbJefe,
                           txtLicAnual, cmbCaracter, txtComentario, chkNomarca, cmbCategoria, txtTelefono,
                           txtInterno, txtCelular, txtRpv, txtIngreso, txtBaja, txtCUIL, txtTitulo,
                           txtMotivo, cmbEstadoParental, txtFechaJubilacion, chkMarcaAqui, cmbTipoDto,
                           txtNroDto, CmbMotivo, txtCargo)
    End Sub

    ' Métodos auxiliares faltantes
    Private Sub GridBuscar()
        Try
            Dim sql As String = "SELECT * FROM Agentes ORDER BY Nombre"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)
            DgvListado.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error al cargar datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormModoConsulta()
        HabilitarControles(False)
        ' Configurar botones para modo consulta usando SetControlesEnabled
        SetControlesEnabled(True, btnAgregar, btnModificar, btnBorrar)
        SetControlesEnabled(False, btnAceptar, btnCancelar)
    End Sub

    Private Sub FormModoEdicion()
        HabilitarControles(True)
        ' Configurar botones para modo edición usando SetControlesEnabled
        SetControlesEnabled(False, btnAgregar, btnModificar, btnBorrar)
        SetControlesEnabled(True, btnAceptar, btnCancelar)
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            CargarDatosEnFormulario(CType(filaActual.DataBoundItem, DataRowView).Row)
        End If
    End Sub

    Private Sub FormLimpiarSeleccionado()
        LimpiarFormulario()
        filaActual = Nothing
        filaActualIndice = -1
    End Sub

    Private Sub AplicarSeleccionActual()
        If DgvListado.CurrentRow IsNot Nothing Then
            Dim dataRowView As DataRowView = CType(DgvListado.CurrentRow.DataBoundItem, DataRowView)
            filaActual = DgvListado.CurrentRow
            filaActualIndice = DgvListado.CurrentRow.Index
        End If
    End Sub

    Private Sub GridConfigurarColumnas()
        Try
            If DgvListado.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In DgvListado.Columns
                    col.Visible = False
                Next
                ' Configurar columnas del grid principal
                DgvListado.Columns("Legajo").Visible = True
                DgvListado.Columns("Legajo").HeaderText = "Legajo"

                DgvListado.Columns("Nombre").Visible = True
                DgvListado.Columns("Nombre").HeaderText = "Nombre"

                DgvListado.Columns("Instituto").Visible = True
                DgvListado.Columns("Instituto").HeaderText = "Sucursal"

                DgvListado.Columns("Cuil").Visible = True
                DgvListado.Columns("Cuil").HeaderText = "CUIL"

                DgvListado.Columns("Telefono").Visible = True
                DgvListado.Columns("Telefono").HeaderText = "Teléfono"

                DgvListado.Columns("Celular").Visible = True
                DgvListado.Columns("Celular").HeaderText = "Celular"

                DgvListado.Columns("Interno").Visible = True
                DgvListado.Columns("Interno").HeaderText = "Interno"

                DgvListado.Columns("CorreoE").Visible = True
                DgvListado.Columns("CorreoE").HeaderText = "E-Mail"

                ConfigurarEstiloGrid(DgvListado)

            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try
    End Sub

End Class