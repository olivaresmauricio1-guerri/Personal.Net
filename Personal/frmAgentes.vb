Imports Microsoft.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmAgentes
    Private _suspenderAccionFiltros As Boolean = False
    'Public Property MostrarSoloEventuales As Boolean?

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

    Public Shared Sub AbrirInstancia(mdiParent As Form, Optional soloEventuales As Boolean? = Nothing)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmAgentes()
            instancia.MdiParent = mdiParent
        End If
        'instancia.MostrarSoloEventuales = soloEventuales
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmAgentes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmAgentes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _suspenderAccionFiltros = True
        CargarComboBoxes()
        FormModoConsulta()
        ConfiguraColListado()
        GridBuscar()
        ConfiguraColListado()
        ConfiguraColComentario()
        ConfiguraColFamilia()
        ConfiguraColEquipamiento()
        _suspenderAccionFiltros = False
        Me.KeyPreview = True
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If _suspenderAccionFiltros Then Exit Sub
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        ConfiguraColListado()
    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If _suspenderAccionFiltros Then Exit Sub
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        ConfiguraColListado()
    End Sub

    Private Sub radActivos_CheckedChanged(sender As Object, e As EventArgs) Handles radActivos.CheckedChanged
        If _suspenderAccionFiltros Then Exit Sub
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        ConfiguraColListado()
    End Sub

    Private Sub radTodos_CheckedChanged(sender As Object, e As EventArgs) Handles radTodos.CheckedChanged
        If _suspenderAccionFiltros Then Exit Sub
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        ConfiguraColListado()
    End Sub

    Private Sub radeventuales_CheckedChanged(sender As Object, e As EventArgs) Handles radEventuales.CheckedChanged
        If _suspenderAccionFiltros Then Exit Sub
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        ConfiguraColListado()
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvListado.KeyDown
        If _suspenderAccionFiltros Then Exit Sub
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(dgvListado, chkEncabezados.Checked)
            e.Handled = True
        End If
        Dim filaActualIndex = dgvListado.CurrentRow.Index
        SeleccionarFila(filaActualIndex)
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListado.CellClick
        If _suspenderAccionFiltros Then Exit Sub

        If e.RowIndex < 0 Then
            filaActual = Nothing
            filaActualIndice = -1
            FormLimpiarSeleccionado()
            Return
        End If
        AplicarSeleccionActual()
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles dgvListado.SelectionChanged
        If _suspenderAccionFiltros Then Exit Sub

        If dgvListado.SelectedRows.Count > 0 Then
            AplicarSeleccionActual()
        End If
    End Sub

    Private Sub cmbCaracter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCaracter.SelectedIndexChanged
        If _suspenderAccionFiltros Then Exit Sub
        If filaActualIndice <= 0 Then
            Return
        End If

        If filaActual.Cells("Caracter").Value = "Eventual" AndAlso cmbCaracter.Text = "Efectivo" Then
            txtLegajo.BackColor = Color.Gold
            txtLegajo.ReadOnly = False
        Else
            txtLegajo.BackColor = SystemColors.Control
            txtLegajo.ReadOnly = True
        End If
    End Sub

    ' Eventos de botones principales
    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        dgvListado.ClearSelection()
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
        txtLegajo.BackColor = Color.Gold
        txtLegajo.ReadOnly = False
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
            ConfiguraColListado()
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
                If Not ActualizarAgente() Then
                    Return
                End If
            End If

            FormModoConsulta()
            GridBuscar()
            ' ConfiguraColListado()
            If dgvListado.CurrentRow IsNot Nothing Then
                filaActual = dgvListado.CurrentRow
            ElseIf dgvListado.Rows.Count > 0 Then
                dgvListado.CurrentCell = dgvListado.Rows(0).Cells(0)
                filaActual = dgvListado.CurrentRow
            End If
            SeleccionarFila(0)

            txtLegajo.BackColor = SystemColors.Control
            txtLegajo.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Public Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click, btnCancelar.Click
        FormModoConsulta()

        txtLegajo.BackColor = SystemColors.Control
        txtLegajo.ReadOnly = True

        If filaActual Is Nothing Then
            SeleccionarFila(0)
        Else
            FormObtenerSeleccionado()
        End If
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub btnAgregarFamiliar_Click(sender As Object, e As EventArgs) Handles btnAgregarFamiliar.Click
        If Not ValidarDatosFamiliar() Then Return

        Dim legajo As String = txtLegajo.Text.Trim()
        Dim nombre As String = txtNombreFamiliar.Text.Trim()
        Dim fechaNac As Date = dtpNacimientoFamiliar.Value
        Dim edad As String = txtEdadFamiliar.Text.Trim()
        Dim parentesco As String = cmbParentesco.Text.Trim()
        Dim ocupacion As String = txtOcupacionFamiliar.Text.Trim()
        Dim nivel As String = cmbNivelEstudio.Text.Trim()


        Dim sql As String = "INSERT INTO GrupoFamiliar (Legajo, Nombre, Parentesco, Nacimiento, Edad, Ocupacion, Nivel) 
                         VALUES (@Legajo, @Nombre, @Parentesco, @Nacimiento, @Edad, @Ocupacion, @Nivel)"
        Dim parametros = CmdParams(
            "@Legajo", CInt(legajo),
            "@Nombre", nombre,
            "@Parentesco", parentesco,
            "@Nacimiento", fechaNac,
            "@Edad", CInt(edad),
            "@Ocupacion", ocupacion,
            "@Nivel", nivel
        )
        DSM.Execute(DSM.Personal, sql, parametros, True)

        FormModoConsulta()
        GridBuscar()
        ConfiguraColListado()
        CargaGrupoFamiliar(Convert.ToInt32(txtLegajo.Text.Trim))
    End Sub
    Private Sub btnEliminarFamiliar_Click(sender As Object, e As EventArgs) Handles btnEliminarFamiliar.Click
        If DgvGrupoFamiliar.CurrentRow Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este familiar?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim Principal = Convert.ToInt32(DgvGrupoFamiliar.CurrentRow.Cells("Id").Value)
            Dim sql = "DELETE FROM GrupoFamiliar WHERE Id = @Id"
            Dim parametros = CmdParams("@Id", Principal)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
            ConfiguraColListado()
            CargaGrupoFamiliar(Convert.ToInt32(txtLegajo.Text.Trim))
        End If
    End Sub
    Private Sub btnAgregarComentario_Click(sender As Object, e As EventArgs) Handles btnAgregarComentario.Click
        If Not ValidarDatosComentarios() Then Return

        Dim legajo As String = txtLegajo.Text.Trim()
        Dim comentario As String = txtComentaComentario.Text.Trim()
        Dim fechaComentario As Date = dtpFechaComentario.Value
        Dim motivo As String = cmbMotivoComentario.Text.Trim()


        Dim sql As String = "INSERT INTO Comentarios (Legajo, Fecha, Comenta, Motivo) 
                         VALUES (@Legajo, @FechaComentario, @Comentario, @Motivo)"
        Dim parametros = CmdParams(
            "@Legajo", CInt(legajo),
            "@Comentario", comentario,
            "@Motivo", motivo,
            "@FechaComentario", fechaComentario
        )
        DSM.Execute(DSM.Personal, sql, parametros, True)

        FormModoConsulta()
        GridBuscar()
        ConfiguraColListado()
        CargaComentario(Convert.ToInt32(txtLegajo.Text.Trim))

    End Sub

    Private Sub btnEliminarComentario_Click(sender As Object, e As EventArgs) Handles btnEliminarComentario.Click
        If DgvComentarios.CurrentRow Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este comentario?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim Principal = Convert.ToInt32(DgvComentarios.CurrentRow.Cells("IdComentario").Value)
            Dim sql = "DELETE FROM Comentarios WHERE IdComentario = @Id"
            Dim parametros = CmdParams("@Id", Principal)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
            ConfiguraColListado()
            CargaComentario(Convert.ToInt32(txtLegajo.Text.Trim))
        End If
    End Sub

    Private Sub btnAgregarEquipamiento_Click(sender As Object, e As EventArgs) Handles btnAgregarEquipamiento.Click
        If Not ValidarDatosEquipamiento() Then Return

        Dim legajo As String = txtLegajo.Text.Trim()
        Dim tipo As String = cmbTipoEquipamiento.Text.Trim()
        Dim marca As String = txtMarcaEquipamiento.Text.Trim()
        Dim modelo As String = txtModeloEquipamiento.Text.Trim()
        Dim nroSerie As String = txtNroSerieEquipamiento.Text.Trim()
        Dim imei As String = txtIMEIEquipamiento.Text.Trim()
        Dim fecha As Date = dtpFechaEquipamiento.Value
        Dim observaciones As String = txtObservacionesEquipamiento.Text.Trim()
        Dim nrotel As String = txtNroTelEquipoamiento.Text.Trim()

        Dim sql As String = "INSERT INTO Equipamiento (Legajo, Tipo, Marca, Modelo, NroSerie, IMEI, Fecha, Observaciones, NroTel) 
                         VALUES (@Legajo, @Tipo, @Marca, @Modelo, @NroSerie, @IMEI, @Fecha, @Observaciones, @NroTel)"
        Dim parametros = CmdParams(
            "@Legajo", CInt(legajo),
            "@Tipo", tipo,
            "@Marca", marca,
            "@Modelo", modelo,
            "@NroSerie", nroSerie,
            "@IMEI", imei,
            "@Fecha", fecha,
            "@Observaciones", observaciones,
            "@NroTel", nrotel
        )
        DSM.Execute(DSM.Personal, sql, parametros, True)

        FormModoConsulta()
        GridBuscar()
        ConfiguraColListado()
        CargaEquipamiento(Convert.ToInt32(txtLegajo.Text.Trim))
    End Sub

    Private Sub btnEliminarEquipamiento_Click(sender As Object, e As EventArgs) Handles btnEliminarEquipamiento.Click
        If DgvEquipamiento.CurrentRow Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este equipamiento?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim Principal = Convert.ToInt32(DgvEquipamiento.CurrentRow.Cells("Id").Value)
            Dim sql = "DELETE FROM Equipamiento WHERE Id = @Id"
            Dim parametros = CmdParams("@Id", Principal)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
            ConfiguraColListado()
            CargaEquipamiento(Convert.ToInt32(txtLegajo.Text.Trim))
        End If
    End Sub

    Private Sub txtNroDto_LostFocus(sender As Object, e As EventArgs) Handles txtNroDto.LostFocus
        ' Validar DNI
        If Len(txtNroDto.Text) < 7 Then
            MessageBox.Show("Debe ingresar un DNI válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroDto.Focus()
            Return
        End If
        If cmbTipoDto.Text = "DNI" Then
            Dim cuil As String = ObtenerCUIL(txtNroDto.Text, cmbSexo.Text.ToUpper)
            txtCUIL.Text = If(cuil.Length = 11, $"{cuil.Substring(0, 2)}-{cuil.Substring(2, 8)}-{cuil.Substring(10, 1)}", cuil)
        End If
    End Sub
    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        CopiarDataGrid(dgvListado, chkEncabezados.Checked)
    End Sub
    Private Sub chkEncabezados_CheckedChanged(sender As Object, e As EventArgs)
        dgvListado.Focus()
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
            MessageBox.Show("Debe seleccionar una Sucursal.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbInstituto.Focus()
            Return False
        End If

        ' Validar Caracter
        If String.IsNullOrEmpty(cmbCaracter.Text.Trim) Then
            MessageBox.Show("Debe seleccionar un Caracter.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCaracter.Focus()
            Return False
        End If

        ' Validar DNI
        If String.IsNullOrEmpty(txtNroDto.Text.Trim) Then
            MessageBox.Show("El campo DNI no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroDto.Focus()
            Return False
        End If

        ' Validar fecha de baja si chkBaja esta seleccionado   
        If chkBaja.Checked Then
            If dtpBaja.Value = Date.MinValue Then
                MessageBox.Show("Seleccione una fecha de baja válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                dtpBaja.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(CmbMotivo.Text.Trim) Then
                MessageBox.Show("Seleccione un motivo de baja.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbMotivo.Focus()
                Return False
            End If
        End If

        Return True
    End Function
    Private Function ValidarDatosFamiliar() As Boolean

        If String.IsNullOrEmpty(txtNombreFamiliar.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombreFamiliar.Focus()
            Return False
        End If

        If dtpNacimientoFamiliar.Value = Date.MinValue Then
            MessageBox.Show("Seleccione una fecha de nacimiento válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpNacimientoFamiliar.Focus()
            Return False
        End If

        If Not IsNumeric(txtEdadFamiliar.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor numérico para la Edad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEdadFamiliar.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(cmbParentesco.Text.Trim()) Then
            MessageBox.Show("Seleccione un valor para el campo Parentesco.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbParentesco.Focus()
            Return False
        End If
        Return True
    End Function
    Private Function ValidarDatosComentarios() As Boolean

        If String.IsNullOrEmpty(txtComentaComentario.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Comentario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombreFamiliar.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(cmbMotivoComentario.Text.Trim()) Then
            MessageBox.Show("Seleccione un valor para el campo Motivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbParentesco.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarDatosEquipamiento() As Boolean
        If String.IsNullOrEmpty(cmbTipoEquipamiento.Text.Trim()) Then
            MessageBox.Show("Seleccione un valor para el campo Tipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbTipoEquipamiento.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtMarcaEquipamiento.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Marca.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMarcaEquipamiento.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtModeloEquipamiento.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Modelo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtModeloEquipamiento.Focus()
            Return False
        End If

        If dtpFechaEquipamiento.Value = Date.MinValue Then
            MessageBox.Show("Seleccione una fecha válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpFechaEquipamiento.Focus()
            Return False
        End If

        Return True
    End Function

    ' Métodos CRUD para Agentes
    Private Sub InsertarNuevoAgente()
        Dim sql As String = "SELECT COUNT(*) FROM Agentes WHERE Legajo = @Legajo OR NroDto = @NroDto"
        Dim parametrosCheck = CmdParams(
            "@Legajo", Convert.ToInt32(txtLegajo.Text.Trim),
            "@NroDto", txtNroDto.Text.Trim)

        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametrosCheck, True)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim existe As Integer = Convert.ToInt32(dt.Rows(0)(0))
            If existe > 0 Then
                MessageBox.Show("Ya existe un Agente con este Nro de Legajo o DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        sql = "INSERT INTO Agentes (Legajo, TipoDto, NroDto, Instituto, Nombre, CorreoE, Sexo, Nacimiento, " &
                           "Calle, Nro, Localidad, HorasDiarias, " &
                           "Escalafon, Jefe, LicAnual, Caracter, Comentario, Nomarca, Cargo, Telefono, Interno, Celular, Rpv,  " &
                           "iNGRESO, Baja, CUIL, TITULO, UltimaActualizacion,  EstadoParental, FechaJubilacion, LegajoEventual) " &
                           "VALUES (@Legajo, @TipoDto, @NroDto,   @Instituto, @Nombre, @CorreoE, @Sexo, @Nacimiento, " &
                           "@Calle, @Nro, @Localidad,  @HorasDiarias, " &
                           "@Escalafon, @Jefe, @LicAnual, @Caracter, @Comentario, @Nomarca, @Cargo, @Telefono, @Interno, @Celular, @Urgencias,  " &
                           "@iNGRESO, @Baja, @CUIL, @TITULO, @UltimaActualizacion, @EstadoParental, @FechaJubilacion, @LegajoEventual)"

        Dim parametros = ObtenerParametrosAgente(True)
        DSM.Execute(DSM.Personal, sql, parametros, True)
        MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function ActualizarAgente() As Boolean
        Dim sql As String = "UPDATE Agentes SET Legajo=@Legajo, LegajoEventual=@LegajoEventual, " &
                           "TipoDto=@TipoDto, NroDto=@NroDto,  Instituto=@Instituto, " &
                           "Nombre=@Nombre, CorreoE=@CorreoE, Sexo=@Sexo, Nacimiento=@Nacimiento, Calle=@Calle, Nro=@Nro, " &
                           "Localidad=@Localidad, HorasDiarias=@HorasDiarias, " &
                           "Escalafon=@Escalafon, Jefe=@Jefe, LicAnual=@LicAnual, Caracter=@Caracter, Comentario=@Comentario, " &
                           "Nomarca=@Nomarca, Cargo=@Cargo, Telefono=@Telefono, Interno=@Interno, Celular=@Celular, Rpv=@Urgencias, " &
                           "iNGRESO=@iNGRESO, Baja=@Baja, Motivo=@Motivo, CUIL=@CUIL, TITULO=@TITULO, UltimaActualizacion=@UltimaActualizacion, " &
                           "EstadoParental=@EstadoParental, FechaJubilacion=@FechaJubilacion " &
                           "WHERE Legajo=@LegajoEventual"
        Try
            Dim parametros = ObtenerParametrosAgente()

            If cmbCaracter.Text = "Eventual" AndAlso (filaActual.Cells("Caracter").Value = "Efectivo") Then
                Throw New Exception("Cambio de Efectivo a Eventual no permitido.")
            End If

            DSM.Execute(DSM.Personal, sql, parametros, True)
            MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            FormModoConsulta()

            Return True
        Catch ex As Exception
            MessageBox.Show("Error al actualizar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return False
    End Function

    Private Function ObtenerParametrosAgente(Optional esNuevo = False) As Dictionary(Of String, Object)
        Dim legajo As Integer = If(String.IsNullOrEmpty(txtLegajo.Text.Trim), DBNull.Value, Convert.ToInt32(txtLegajo.Text.Trim))
        Dim legajoEventual As Integer = If(String.IsNullOrEmpty(txtLegajoEventual.Text.Trim), 0, Convert.ToInt32(txtLegajoEventual.Text.Trim))

        If (Not esNuevo) Or (cmbCaracter.Text = "Eventual" AndAlso esNuevo) Then
            legajoEventual = legajo
        End If

        If (Not esNuevo) AndAlso cmbCaracter.Text = "Efectivo" AndAlso (filaActual.Cells("Caracter").Value = "Eventual") Then
            legajoEventual = filaActual.Cells("Legajo").Value
        End If

        Return New Dictionary(Of String, Object) From {
            {"@Legajo", legajo},
            {"@LegajoEventual", legajoEventual},
            {"@TipoDto", If(String.IsNullOrEmpty(cmbTipoDto.Text.Trim), DBNull.Value, cmbTipoDto.Text.Trim)},
            {"@NroDto", If(String.IsNullOrEmpty(txtNroDto.Text.Trim), DBNull.Value, If(Integer.TryParse(txtNroDto.Text.Trim, 0), Convert.ToInt32(txtNroDto.Text.Trim), DBNull.Value))},
            {"@Instituto", If(String.IsNullOrEmpty(cmbInstituto.Text.Trim), DBNull.Value, cmbInstituto.Text.Trim)},
            {"@Nombre", If(String.IsNullOrEmpty(txtNombre.Text.Trim), DBNull.Value, txtNombre.Text.Trim)},
            {"@CorreoE", If(String.IsNullOrEmpty(txtCorreoE.Text.Trim), DBNull.Value, txtCorreoE.Text.Trim)},
            {"@Sexo", If(String.IsNullOrEmpty(cmbSexo.Text.Trim), DBNull.Value, cmbSexo.Text.Trim)},
            {"@Nacimiento", If(dtpNacimiento.Value = dtpNacimiento.MinDate, DBNull.Value, dtpNacimiento.Value)},
            {"@Calle", If(String.IsNullOrEmpty(txtCalle.Text.Trim), DBNull.Value, txtCalle.Text.Trim)},
            {"@Nro", If(String.IsNullOrEmpty(txtNro.Text.Trim), DBNull.Value, txtNro.Text.Trim)},
            {"@Localidad", If(String.IsNullOrEmpty(txtLocalidad.Text.Trim), DBNull.Value, txtLocalidad.Text.Trim)},
            {"@Urgencias", If(String.IsNullOrEmpty(txtUrgencias.Text.Trim), DBNull.Value, txtUrgencias.Text.Trim)},
            {"@HorasDiarias", If(String.IsNullOrEmpty(cmbHorasDiarias.Text.Trim), DBNull.Value, cmbHorasDiarias.Text.Trim)},
            {"@Escalafon", If(String.IsNullOrEmpty(cmbEscalafon.Text.Trim), DBNull.Value, cmbEscalafon.Text.Trim)},
            {"@Jefe", If(String.IsNullOrEmpty(cmbJefe.Text.Trim), DBNull.Value, cmbJefe.Text.Trim)},
            {"@LicAnual", If(String.IsNullOrEmpty(txtLicAnual.Text.Trim), DBNull.Value, txtLicAnual.Text.Trim)},
            {"@Caracter", If(String.IsNullOrEmpty(cmbCaracter.Text.Trim), DBNull.Value, cmbCaracter.Text.Trim)},
            {"@Comentario", If(String.IsNullOrEmpty(txtComentario.Text.Trim), DBNull.Value, txtComentario.Text.Trim)},
            {"@Nomarca", chkNomarca.Checked},
            {"@Cargo", If(String.IsNullOrEmpty(cmbCategoria.Text.Trim), DBNull.Value, cmbCategoria.Text.Trim)},
            {"@Telefono", If(String.IsNullOrEmpty(txtTelefono.Text.Trim), DBNull.Value, txtTelefono.Text.Trim)},
            {"@Interno", If(String.IsNullOrEmpty(txtInterno.Text.Trim), DBNull.Value, txtInterno.Text.Trim)},
            {"@Celular", If(String.IsNullOrEmpty(txtCelular.Text.Trim), DBNull.Value, txtCelular.Text.Trim)},
            {"@iNGRESO", If(dtpIngreso.Value = dtpIngreso.MinDate, DBNull.Value, dtpIngreso.Value)},
            {"@Baja", If(Not chkBaja.Checked OrElse dtpBaja.Value = dtpBaja.MinDate, DBNull.Value, dtpBaja.Value)},
            {"@CUIL", If(String.IsNullOrEmpty(txtCUIL.Text.Trim), DBNull.Value, txtCUIL.Text.Trim)},
            {"@TITULO", If(String.IsNullOrEmpty(txtTitulo.Text.Trim), DBNull.Value, txtTitulo.Text.Trim)},
            {"@UltimaActualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"@EstadoParental", If(String.IsNullOrEmpty(cmbEstadoParental.Text.Trim), DBNull.Value, cmbEstadoParental.Text.Trim)},
            {"@Motivo", If(Not chkBaja.Checked OrElse String.IsNullOrEmpty(CmbMotivo.Text.Trim), DBNull.Value, CmbMotivo.Text.Trim)},
            {"@FechaJubilacion", If(String.IsNullOrEmpty(txtFechaJubilacion.Text.Trim), DBNull.Value, txtFechaJubilacion.Text.Trim)}
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

    Private Sub CargarDatosEnFormulario(row As DataRow)
        If row IsNot Nothing Then
            txtLegajo.Text = If(IsDBNull(row("Legajo")), "", row("Legajo").ToString())
            txtLegajoEventual.Text = If(IsDBNull(row("LegajoEventual")), "", row("LegajoEventual").ToString())
            txtNombre.Text = If(IsDBNull(row("Nombre")), "", row("Nombre").ToString())
            cmbInstituto.Text = If(IsDBNull(row("Instituto")), "", row("Instituto").ToString())
            txtCorreoE.Text = If(IsDBNull(row("CorreoE")), "", row("CorreoE").ToString())
            cmbSexo.Text = If(IsDBNull(row("Sexo")), "", row("Sexo").ToString())

            If Not IsDBNull(row("Nacimiento")) Then
                dtpNacimiento.Value = Convert.ToDateTime(row("Nacimiento"))
            End If

            txtCalle.Text = If(IsDBNull(row("Calle")), "", row("Calle").ToString())
            txtNro.Text = If(IsDBNull(row("Nro")), "", row("Nro").ToString())
            txtLocalidad.Text = If(IsDBNull(row("Localidad")), "", row("Localidad").ToString())
            txtUrgencias.Text = If(IsDBNull(row("HorasDedicacion")), "", row("HorasDedicacion").ToString())

            cmbHorasDiarias.Text = If(IsDBNull(row("HorasDiarias")), "", row("HorasDiarias").ToString())
            cmbEscalafon.Text = If(IsDBNull(row("Escalafon")), "", row("Escalafon").ToString())
            cmbJefe.Text = If(IsDBNull(row("Jefe")), "", row("Jefe").ToString())
            txtLicAnual.Text = If(IsDBNull(row("LicAnual")), "", row("LicAnual").ToString())
            cmbCaracter.Text = If(IsDBNull(row("Caracter")), "", row("Caracter").ToString())
            txtComentario.Text = If(IsDBNull(row("Comentario")), "", row("Comentario").ToString())

            chkNomarca.Checked = If(IsDBNull(row("Nomarca")), False, Convert.ToBoolean(row("Nomarca")))
            cmbCategoria.Text = If(IsDBNull(row("Cargo")), "", row("Cargo").ToString())
            txtTelefono.Text = If(IsDBNull(row("Telefono")), "", row("Telefono").ToString())
            txtInterno.Text = If(IsDBNull(row("Interno")), "", row("Interno").ToString())
            txtCelular.Text = If(IsDBNull(row("Celular")), "", row("Celular").ToString())
            txtUrgencias.Text = If(IsDBNull(row("Rpv")), "", row("Rpv").ToString())
            txtUltimaActualizacion.Text = If(IsDBNull(row("UltimaActualizacion")), "", row("UltimaActualizacion").ToString())

            If Not IsDBNull(row("iNGRESO")) AndAlso row("iNGRESO").ToString().Trim() <> "" Then
                dtpIngreso.Value = Convert.ToDateTime(row("iNGRESO"))
                CalcularAntiguedad()
            End If

            If Not IsDBNull(row("Baja")) AndAlso row("Baja").ToString().Trim() <> "" Then
                chkBaja.Checked = True
                dtpBaja.Visible = True

                dtpBaja.Format = DateTimePickerFormat.Custom
                dtpBaja.CustomFormat = "dd/MM/yyyy"  ' Formato normal de fecha
                dtpBaja.Value = Convert.ToDateTime(row("Baja"))

                CmbMotivo.Visible = True
                CmbMotivo.Text = If(IsDBNull(row("Motivo")), "", row("Motivo").ToString())

            Else
                chkBaja.Checked = False
                dtpBaja.Visible = False

                dtpBaja.Format = DateTimePickerFormat.Custom
                dtpBaja.CustomFormat = " "  ' Espacio en blanco = no muestra nada
                dtpBaja.Value = dtpBaja.MinDate

                CmbMotivo.Visible = False
                CmbMotivo.Text = If(IsDBNull(row("Motivo")), "", row("Motivo").ToString())
            End If

            txtCUIL.Text = If(IsDBNull(row("CUIL")), "", row("CUIL").ToString())
            txtTitulo.Text = If(IsDBNull(row("TITULO")), "", row("TITULO").ToString())

            cmbEstadoParental.Text = If(IsDBNull(row("EstadoParental")), "", row("EstadoParental").ToString())
            txtFechaJubilacion.Text = If(IsDBNull(row("FechaJubilacion")), "", row("FechaJubilacion").ToString())
            cmbTipoDto.Text = If(IsDBNull(row("TipoDto")), "", row("TipoDto").ToString())
            txtNroDto.Text = If(IsDBNull(row("NroDto")), "", row("NroDto").ToString())


            'pctFoto.Image = Image.FromFile("F:\Imagenes\LEG_" & txtLegajo.Text.Trim & ".jpg")
            Dim rutaFoto As String = "F:\Imagenes\LEG_" & txtLegajo.Text.Trim & ".jpg"
            Dim rutaGenerica As String = "F:\Imagenes\foto_generica.jpg"

            If System.IO.File.Exists(rutaFoto) Then
                pctFoto.Image = Image.FromFile(rutaFoto)
            ElseIf System.IO.File.Exists(rutaGenerica) Then
                pctFoto.Image = Image.FromFile(rutaGenerica)
            Else
                pctFoto.Image = Nothing ' O una imagen por defecto desde recursos
            End If
        End If
    End Sub

    Private Sub GridBuscar()
        ' SeleccionarFila(0)

        Try
            Dim texto As String = txtBuscar.Text.Trim()
            Dim sql As String = "SELECT * FROM Agentes WHERE 1=1"
            Dim parametros As New List(Of Object)()

            ' Filtrar activos si corresponde
            If radActivos.Checked Then
                sql &= " AND (Baja IS NULL OR Baja = '') AND (Caracter <> 'Eventual' OR Caracter IS NULL OR Caracter = '')"
            End If

            ' Filtrar por eventuales
            If radEventuales.Checked Then
                sql &= " AND (Baja IS NULL OR Baja = '') AND Caracter = 'Eventual'"
            End If

            Dim sucursal As String = cmbSucursal.Text.Trim()
            If Not String.IsNullOrEmpty(sucursal) And sucursal <> "(Todas)" Then
                sql &= " AND Instituto = @Sucursal"
                parametros.AddRange(New Object() {"@Sucursal", sucursal})
            End If

            ' Filtro por búsqueda en varias columnas
            If Not String.IsNullOrEmpty(texto) Then
                sql &= " AND (Nombre LIKE @Nombre OR NroDto LIKE @DNI OR Legajo LIKE @Legajo)"
                parametros.AddRange(New Object() {"@Nombre", $"%{texto}%", "@DNI", $"{texto.Trim()}%", "@Legajo", $"{texto.Trim()}%"})
            End If

            ' Filtro Eventuales
            'If MostrarSoloEventuales.HasValue Then
            '    sql &= " AND Eventual = @Eventual"
            '    parametros.AddRange(New Object() {"@Eventual", If(MostrarSoloEventuales.Value, 1, 0)})
            'End If

            ' Orden
            sql &= " ORDER BY Nombre"

            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
            dgvListado.DataSource = dt
            lblTotalAgentes.Text = "Total de Empleados: " & dt.Rows.Count.ToString()

            ' SeleccionarFila(0)
        Catch ex As Exception
            MessageBox.Show("Error al cargar datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargaGrupoFamiliar(legajo As Integer)
        Try
            ' 1. Traer los registros del grupo familiar
            Dim sql As String = "SELECT * FROM GrupoFamiliar WHERE Legajo = @Legajo"
            Dim parametros As New List(Of Object) From {"@Legajo", txtLegajo.Text.Trim}

            Dim tabla As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
            DgvGrupoFamiliar.DataSource = tabla

            ' 2. Recorrer registros y actualizar edad
            For Each fila As DataRow In tabla.Rows
                Dim fecha1 As Date = Convert.ToDateTime(fila("Nacimiento"))
                Dim fecha2 As Date = Date.Now
                Dim diferencia As Integer = DateDiff(DateInterval.Year, fecha1, fecha2)

                ' Ajuste para que no cuente el año si todavía no cumplió
                If fecha1.AddYears(diferencia) > fecha2 Then
                    diferencia -= 1
                End If

                ' 3. Actualizar el campo EDAD en BD
                Dim sqlUpdate As String = "UPDATE GrupoFamiliar SET Edad = @Edad WHERE Id = @Id"
                Dim parametrosUpdate As New List(Of Object) From {"@Edad", diferencia, "@Id", fila("Id")}
                DSM.ExecuteQuery(DSM.Personal, sqlUpdate, CmdParams(parametrosUpdate.ToArray()))
            Next

            ConfiguraColFamilia()

        Catch ex As Exception
            MessageBox.Show("Error al actualizar edades: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CargaComentario(legajo As Integer)
        Try
            ' 1. Traer los registros del grupo familiar
            Dim sql As String = "SELECT * FROM Comentarios WHERE Legajo = @Legajo"
            Dim parametros As New List(Of Object) From {"@Legajo", txtLegajo.Text.Trim}

            Dim tabla As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
            DgvComentarios.DataSource = tabla

            ConfiguraColComentario()

        Catch ex As Exception
            MessageBox.Show("Error al cargar comentarios: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargaEquipamiento(legajo As Integer)
        Try
            ' Traer los registros del equipamiento
            Dim sql As String = "SELECT * FROM Equipamiento WHERE Legajo = @Legajo ORDER BY Fecha DESC"
            Dim parametros As New List(Of Object) From {"@Legajo", legajo}

            Dim tabla As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
            DgvEquipamiento.DataSource = tabla

            ' Configurar columnas usando el método dedicado
            ConfiguraColEquipamiento()

        Catch ex As Exception
            MessageBox.Show("Error al cargar equipamiento: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

        txtUrgencias.Clear()

        txtLicAnual.Clear()
        txtComentario.Clear()
        txtTelefono.Clear()
        txtInterno.Clear()
        txtCelular.Clear()
        txtUltimaActualizacion.Clear()

        txtCUIL.Clear()
        txtTitulo.Clear()
        txtFechaJubilacion.Clear()
        txtNroDto.Clear()
        txtAntiguedad.Clear()

        pctFoto.Image = Nothing

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
        chkNomarca.Checked = False

        ' Resetear DateTimePicker
        dtpNacimiento.Value = DateTime.Now

        ' Limpiar campos de equipamiento
        cmbTipoEquipamiento.SelectedIndex = -1
        txtMarcaEquipamiento.Clear()
        txtModeloEquipamiento.Clear()
        txtNroSerieEquipamiento.Clear()
        txtIMEIEquipamiento.Clear()
        txtObservacionesEquipamiento.Clear()
        txtNroTelEquipoamiento.Clear()
        dtpFechaEquipamiento.Value = DateTime.Now

        ' Limpiar DataGridViews
        DgvGrupoFamiliar.DataSource = Nothing
        DgvComentarios.DataSource = Nothing
        DgvEquipamiento.DataSource = Nothing
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            CargarDatosEnFormulario(CType(filaActual.DataBoundItem, DataRowView).Row)
            CargaGrupoFamiliar(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaComentario(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaEquipamiento(Convert.ToInt32(txtLegajo.Text.Trim))
        End If
    End Sub

    Private Sub FormLimpiarSeleccionado()
        LimpiarFormulario()
        filaActual = Nothing
        filaActualIndice = -1
    End Sub

    Private Sub AplicarSeleccionActual()

        If dgvListado Is Nothing OrElse dgvListado.CurrentRow Is Nothing Then Return

        If dgvListado.SelectedRows.Count > 1 Then
            filaActual = Nothing
            filaActualIndice = -1
            FormLimpiarSeleccionado()
            Return
        End If

        Dim idx = dgvListado.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return

        FormModoConsulta()
        ' FormLimpiarSeleccionado()

        filaActual = dgvListado.CurrentRow
        filaActualIndice = idx
        FormObtenerSeleccionado()
    End Sub

    Private Sub SeleccionarFila(numero As Integer)
        If dgvListado.Rows.Count > 0 Then
            dgvListado.Rows(numero).Selected = True
        End If
        AplicarSeleccionActual()
    End Sub

    Private Sub CalcularAntiguedad()
        If dtpIngreso.Value <= DateTime.Now Then
            Dim fechaIngreso As DateTime = dtpIngreso.Value
            Dim fechaActual As DateTime = DateTime.Now

            ' Calcular años completos
            Dim años As Integer = fechaActual.Year - fechaIngreso.Year

            ' Ajustar si aún no ha pasado el aniversario este año
            If fechaActual.Month < fechaIngreso.Month OrElse
           (fechaActual.Month = fechaIngreso.Month AndAlso fechaActual.Day < fechaIngreso.Day) Then
                años -= 1
            End If

            If años >= 1 Then
                ' Si tiene 1 año o más, mostrar solo años
                If años = 1 Then
                    txtAntiguedad.Text = "1 año"
                Else
                    txtAntiguedad.Text = años.ToString() & " años"
                End If
            Else
                ' Si tiene menos de 1 año, calcular meses
                Dim meses As Integer = 0
                Dim fechaTemporal As DateTime = fechaIngreso

                While fechaTemporal.AddMonths(1) <= fechaActual
                    meses += 1
                    fechaTemporal = fechaTemporal.AddMonths(1)
                End While

                If meses = 0 Then
                    txtAntiguedad.Text = "Menos de 1 mes"
                ElseIf meses = 1 Then
                    txtAntiguedad.Text = "1 mes"
                Else
                    txtAntiguedad.Text = meses.ToString() & " meses"
                End If
            End If
        Else
            txtAntiguedad.Text = "Fecha futura"
        End If
    End Sub

    Private Sub HabilitarControles(habilitar As Boolean)
        ' Usar la función SetControlesEnabled de Funciones.vb para habilitar/deshabilitar controles
        SetControlesEnabled(habilitar, txtLegajo, txtNombre, cmbInstituto, txtCorreoE, cmbSexo, dtpNacimiento,
                           txtCalle, txtNro, txtLocalidad,
                           txtUrgencias, cmbHorasDiarias, cmbEscalafon, cmbJefe,
                           txtLicAnual, cmbCaracter, txtComentario, chkNomarca, cmbCategoria, txtTelefono,
                           txtInterno, txtCelular, txtUltimaActualizacion, dtpIngreso, dtpBaja, txtTitulo,
                            cmbEstadoParental, txtFechaJubilacion, cmbTipoDto, txtCUIL, txtNroDto, CmbMotivo, txtAntiguedad)
    End Sub
    Private Sub dtpNacimientoFamiliar_ValueChanged(sender As Object, e As EventArgs) Handles dtpNacimientoFamiliar.ValueChanged

        ' Validar que la fecha no sea futura
        If dtpNacimientoFamiliar.Value > Date.Today Then
            txtEdadFamiliar.Text = "0"
            Return
        End If

        Dim edad As Integer = Date.Today.Year - dtpNacimientoFamiliar.Value.Year
        If Date.Today < dtpNacimientoFamiliar.Value.AddYears(edad) Then edad -= 1
        txtEdadFamiliar.Text = edad.ToString()

    End Sub

    Private Sub DgvGrupoFamiliar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvGrupoFamiliar.CellDoubleClick
        DgvGrupoFamiliar.ReadOnly = False
    End Sub

    Private Sub DgvGrupoFamiliar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvGrupoFamiliar.CellEndEdit

        ' Ejecutar la misma lógica del RowLeave para actualizar la base de datos
        Dim rowIndex = DgvGrupoFamiliar.Rows(e.RowIndex)
        Dim idTrans = Convert.ToInt32(rowIndex.Cells("Id").Value)

        Dim nombre = rowIndex.Cells("Nombre").Value
        Dim parentesco = rowIndex.Cells("Parentesco").Value
        Dim nacimiento = rowIndex.Cells("Nacimiento").Value
        Dim edad = rowIndex.Cells("Edad").Value
        Dim ocupacion = rowIndex.Cells("Ocupacion").Value
        Dim nivel = rowIndex.Cells("Nivel").Value

        Dim sqlUpdateMovimiento As String = "
            UPDATE GrupoFamiliar 
            SET 
                Nombre = @Nombre,
                Parentesco = @Parentesco,
                Nacimiento = @Nacimiento,
                Edad = @Edad,
                Ocupacion = @Ocupacion,
                Nivel = @Nivel
            WHERE Id = @id"
        Dim parametros = CmdParams(
            "@Nombre", nombre, "@Parentesco", parentesco, "@Nacimiento", nacimiento, "@Edad", edad,
            "@Ocupacion", ocupacion, "@Nivel", nivel, "@id", idTrans)

        DSM.Execute(DSM.Personal, sqlUpdateMovimiento, parametros, True)
        DgvGrupoFamiliar.ReadOnly = True
    End Sub
    Private Sub DgvComentarios_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvComentarios.CellDoubleClick
        DgvComentarios.ReadOnly = False
    End Sub

    Private Sub DgvComentarios_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvComentarios.CellEndEdit

        ' Ejecutar la misma lógica del RowLeave para actualizar la base de datos
        Dim rowIndex = DgvComentarios.Rows(e.RowIndex)
        Dim idTrans = Convert.ToInt32(rowIndex.Cells("idComentario").Value)
        Dim fecha = rowIndex.Cells("Fecha").Value
        Dim comenta = rowIndex.Cells("Comenta").Value
        Dim motivo = rowIndex.Cells("Motivo").Value

        Dim sqlUpdateComentario As String = "
            UPDATE Comentarios 
            SET 
                Fecha = @Fecha,
                Comenta = @Comenta,
                Motivo = @Motivo
            WHERE IdComentario = @idComentario"
        Dim parametros = CmdParams(
            "@Fecha", fecha, "@Comenta", comenta, "@Motivo", motivo, "@idComentario", idTrans)

        DSM.Execute(DSM.Personal, sqlUpdateComentario, parametros, True)

        DgvComentarios.ReadOnly = True

    End Sub

    Private Sub DgvEquipamiento_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvEquipamiento.CellDoubleClick
        DgvEquipamiento.ReadOnly = False
    End Sub
    Private Sub DgvEquipamiento_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvEquipamiento.CellEndEdit
        Dim rowIndex = DgvEquipamiento.Rows(e.RowIndex)
        Dim idTrans = Convert.ToInt32(rowIndex.Cells("Id").Value)
        Dim tipo = rowIndex.Cells("Tipo").Value
        Dim marca = rowIndex.Cells("Marca").Value
        Dim modelo = rowIndex.Cells("Modelo").Value
        Dim nroTel = rowIndex.Cells("NroTel").Value
        Dim nroSerie = rowIndex.Cells("NroSerie").Value
        Dim imei = rowIndex.Cells("IMEI").Value
        Dim fecha = rowIndex.Cells("Fecha").Value
        Dim observaciones = rowIndex.Cells("Observaciones").Value

        Dim sqlUpdateEquipamiento As String = "
            UPDATE Equipamiento 
            SET 
                Tipo = @TipoEquipamiento,
                Marca = @Marca,
                Modelo = @Modelo,
                NroTel = @NroTel,
                NroSerie = @NroSerie,
                IMEI = @IMEI,
                Fecha = @Fecha,
                Observaciones = @Observaciones
            WHERE Id = @Id"
        Dim parametros = CmdParams(
            "@TipoEquipamiento", tipo, "@Marca", marca, "@Modelo", modelo, "@NroTel", nroTel, "@NroSerie", nroSerie,
            "@IMEI", imei, "@Fecha", fecha, "@Observaciones", observaciones, "@Id", idTrans)

        DSM.Execute(DSM.Personal, sqlUpdateEquipamiento, parametros, True)
        DgvEquipamiento.ReadOnly = True

    End Sub
    Private Sub btnDocumentacion_Click(sender As Object, e As EventArgs) Handles btnDocumentacion.Click
        ' Verificar que hay un agente seleccionado
        If filaActual Is Nothing OrElse filaActualIndice < 0 Then
            MessageBox.Show("Debe seleccionar un agente para ver su documentación.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Obtener el legajo del agente seleccionado
        Dim legajo = txtLegajo.Text 'filaActual.Cells("Legajo").Value.ToString

        ' Crear y mostrar el formulario de documentación
        Dim frmDoc As New frmDocumentacion
        frmDoc.Legajo = legajo
        frmDoc.MdiParent = MdiParent
        frmDoc.Show()
    End Sub

    Private Sub ConfiguraColListado()
        Try
            If dgvListado.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In dgvListado.Columns
                    col.Visible = False
                Next

                ConfigurarEstiloGrid(dgvListado)

                ' Configurar columnas del grid principal
                dgvListado.Columns("Legajo").Visible = True
                dgvListado.Columns("Legajo").HeaderText = "Legajo"
                dgvListado.Columns("Legajo").Width = 50
                dgvListado.Columns("Legajo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvListado.Columns("Legajo").DisplayIndex = 0

                dgvListado.Columns("Nombre").Visible = True
                dgvListado.Columns("Nombre").HeaderText = "Nombre"
                dgvListado.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dgvListado.Columns("Nombre").DisplayIndex = 1

                dgvListado.Columns("Cuil").Visible = True
                dgvListado.Columns("Cuil").HeaderText = "CUIL"
                dgvListado.Columns("Cuil").Width = 110
                dgvListado.Columns("Cuil").DisplayIndex = 2

                dgvListado.Columns("Instituto").Visible = True
                dgvListado.Columns("Instituto").HeaderText = "Sucursal"
                dgvListado.Columns("Instituto").Width = 150
                dgvListado.Columns("Instituto").DisplayIndex = 3

                dgvListado.Columns("Telefono").Visible = True
                dgvListado.Columns("Telefono").HeaderText = "Teléfono"
                dgvListado.Columns("Telefono").Width = 110
                dgvListado.Columns("Telefono").DisplayIndex = 4

                dgvListado.Columns("Celular").Visible = True
                dgvListado.Columns("Celular").HeaderText = "Celular"
                dgvListado.Columns("Celular").Width = 110
                dgvListado.Columns("Celular").DisplayIndex = 5

                dgvListado.Columns("Interno").Visible = True
                dgvListado.Columns("Interno").HeaderText = "Interno"
                dgvListado.Columns("Interno").Width = 60
                dgvListado.Columns("Interno").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvListado.Columns("Interno").DisplayIndex = 6

                dgvListado.Columns("CorreoE").Visible = True
                dgvListado.Columns("CorreoE").HeaderText = "E-Mail"
                dgvListado.Columns("CorreoE").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dgvListado.Columns("CorreoE").DisplayIndex = 7

                dgvListado.Columns("Caracter").Visible = True
                dgvListado.Columns("Caracter").HeaderText = "Caracter"
                dgvListado.Columns("Caracter").Width = 120
                dgvListado.Columns("Caracter").DisplayIndex = 8

            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try
    End Sub

    Private Sub ConfiguraColFamilia()
        Try
            If DgvGrupoFamiliar.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In DgvGrupoFamiliar.Columns
                    col.Visible = False
                Next
                ' Configurar columnas del grid principal

                DgvGrupoFamiliar.Columns("Id").Visible = False
                DgvGrupoFamiliar.Columns("Id").HeaderText = "Id"

                DgvGrupoFamiliar.Columns("Nombre").Visible = True
                DgvGrupoFamiliar.Columns("Nombre").HeaderText = "Nombre"
                DgvGrupoFamiliar.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ' Configurar columna Parentesco como ComboBox
                If DgvGrupoFamiliar.Columns.Contains("Parentesco") Then
                    ' Obtener el índice de la columna actual
                    Dim indiceParentesco As Integer = DgvGrupoFamiliar.Columns("Parentesco").Index

                    ' Remover la columna existente
                    DgvGrupoFamiliar.Columns.Remove("Parentesco")

                    ' Crear una nueva columna ComboBox
                    Dim colParentesco As New DataGridViewComboBoxColumn()
                    colParentesco.Name = "Parentesco"
                    colParentesco.HeaderText = "Parentesco"
                    colParentesco.DataPropertyName = "Parentesco"
                    colParentesco.Width = 120

                    ' Usar los mismos valores que cmbParentesco
                    For Each item As Object In cmbParentesco.Items
                        colParentesco.Items.Add(item)
                    Next

                    ' Insertar en la posición original
                    DgvGrupoFamiliar.Columns.Insert(indiceParentesco, colParentesco)
                End If

                DgvGrupoFamiliar.Columns("Nacimiento").Visible = True
                DgvGrupoFamiliar.Columns("Nacimiento").HeaderText = "Fecha de Nacimiento"
                DgvGrupoFamiliar.Columns("Nacimiento").Width = 80
                DgvGrupoFamiliar.Columns("Nacimiento").DefaultCellStyle.Format = "dd/MM/yyyy"

                DgvGrupoFamiliar.Columns("Edad").Visible = True
                DgvGrupoFamiliar.Columns("Edad").HeaderText = "Edad"
                DgvGrupoFamiliar.Columns("Edad").Width = 50

                DgvGrupoFamiliar.Columns("Ocupacion").Visible = True
                DgvGrupoFamiliar.Columns("Ocupacion").HeaderText = "Ocupación"
                DgvGrupoFamiliar.Columns("Ocupacion").Width = 150

                ' Configurar columna Parentesco como ComboBox
                If DgvGrupoFamiliar.Columns.Contains("Nivel") Then
                    ' Obtener el índice de la columna actual
                    Dim indiceNivel As Integer = DgvGrupoFamiliar.Columns("Nivel").Index

                    ' Remover la columna existente
                    DgvGrupoFamiliar.Columns.Remove("Nivel")

                    ' Crear una nueva columna ComboBox
                    Dim colNivel As New DataGridViewComboBoxColumn()
                    colNivel.Name = "Nivel"
                    colNivel.HeaderText = "Nivel de Estudio"
                    colNivel.DataPropertyName = "Nivel"
                    colNivel.Width = 150

                    ' Usar los mismos valores que cmbParentesco
                    For Each item As Object In cmbNivelEstudio.Items
                        colNivel.Items.Add(item)
                    Next

                    ' Insertar en la posición original
                    DgvGrupoFamiliar.Columns.Insert(indiceNivel, colNivel)
                End If

                ConfigurarEstiloGrid(DgvGrupoFamiliar)

            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try
    End Sub

    Private Sub ConfiguraColComentario()
        Try
            If DgvComentarios.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In DgvComentarios.Columns
                    col.Visible = False
                Next
                ' Configurar columnas del grid principal
                DgvComentarios.Columns("IdComentario").Visible = False
                DgvComentarios.Columns("IdComentario").HeaderText = "Id"

                DgvComentarios.Columns("Fecha").Visible = True
                DgvComentarios.Columns("Fecha").HeaderText = "Fecha"
                DgvComentarios.Columns("Fecha").Width = 100

                DgvComentarios.Columns("Comenta").Visible = True
                DgvComentarios.Columns("Comenta").HeaderText = "Comentario"
                DgvComentarios.Columns("Comenta").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ' Configurar columna Parentesco como ComboBox
                If DgvComentarios.Columns.Contains("Motivo") Then
                    ' Obtener el índice de la columna actual
                    Dim indiceMotivo As Integer = DgvComentarios.Columns("Motivo").Index

                    ' Remover la columna existente
                    DgvComentarios.Columns.Remove("Motivo")

                    ' Crear una nueva columna ComboBox
                    Dim colMotivo As New DataGridViewComboBoxColumn()
                    colMotivo.Name = "Motivo"
                    colMotivo.HeaderText = "Motivo"
                    colMotivo.DataPropertyName = "Motivo"
                    colMotivo.Width = 300

                    ' Motivos para Comentarios
                    CargarCombosGrid(colMotivo, "Inasistencias", "Descripcion", "Descripcion")

                    ' Insertar en la posición original
                    DgvComentarios.Columns.Insert(indiceMotivo, colMotivo)
                End If

                ConfigurarEstiloGrid(DgvComentarios)

            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try
    End Sub

    Private Sub ConfiguraColEquipamiento()
        Try
            If DgvEquipamiento.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In DgvEquipamiento.Columns
                    col.Visible = False
                Next
                ' Configurar columnas del grid de equipamiento

                DgvEquipamiento.Columns("Id").Visible = False
                DgvEquipamiento.Columns("Id").HeaderText = "Id"

                DgvEquipamiento.Columns("Legajo").Visible = False
                DgvEquipamiento.Columns("Legajo").HeaderText = "Legajo"

                ' Configurar columna Parentesco como ComboBox
                If DgvEquipamiento.Columns.Contains("Tipo") Then
                    ' Obtener el índice de la columna actual
                    Dim indiceTipo As Integer = DgvEquipamiento.Columns("Tipo").Index

                    ' Remover la columna existente
                    DgvEquipamiento.Columns.Remove("Tipo")

                    ' Crear una nueva columna ComboBox
                    Dim colTipo As New DataGridViewComboBoxColumn()
                    colTipo.Name = "Tipo"
                    colTipo.HeaderText = "Tipo"
                    colTipo.DataPropertyName = "Tipo"
                    colTipo.Width = 100

                    ' Usar los mismos valores que cmbParentesco
                    For Each item As Object In cmbTipoEquipamiento.Items
                        colTipo.Items.Add(item)
                    Next

                    ' Insertar en la posición original
                    DgvEquipamiento.Columns.Insert(indiceTipo, colTipo)
                End If

                DgvEquipamiento.Columns("Marca").Visible = True
                DgvEquipamiento.Columns("Marca").HeaderText = "Marca"
                DgvEquipamiento.Columns("Marca").Width = 80

                DgvEquipamiento.Columns("Modelo").Visible = True
                DgvEquipamiento.Columns("Modelo").HeaderText = "Modelo"
                DgvEquipamiento.Columns("Modelo").Width = 100

                DgvEquipamiento.Columns("NroTel").Visible = True
                DgvEquipamiento.Columns("NroTel").HeaderText = "Nro. Teléfono"
                DgvEquipamiento.Columns("NroTel").Width = 120

                DgvEquipamiento.Columns("NroSerie").Visible = True
                DgvEquipamiento.Columns("NroSerie").HeaderText = "Nro. Serie"
                DgvEquipamiento.Columns("NroSerie").Width = 100

                DgvEquipamiento.Columns("IMEI").Visible = True
                DgvEquipamiento.Columns("IMEI").HeaderText = "IMEI"
                DgvEquipamiento.Columns("IMEI").Width = 100

                DgvEquipamiento.Columns("Fecha").Visible = True
                DgvEquipamiento.Columns("Fecha").HeaderText = "Fecha"
                DgvEquipamiento.Columns("Fecha").Width = 100
                DgvEquipamiento.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"

                DgvEquipamiento.Columns("Observaciones").Visible = True
                DgvEquipamiento.Columns("Observaciones").HeaderText = "Observaciones"
                DgvEquipamiento.Columns("Observaciones").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ConfigurarEstiloGrid(DgvEquipamiento)

            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try
    End Sub

    Private Sub CargarComboBoxes()
        Try
            ' Cargar sucursales
            CargarCombos(cmbSucursal, "Institutos", "Descripcion", "Descripcion")

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

        ' Parentesco
        cmbParentesco.Items.Clear()
        cmbParentesco.Items.AddRange({"Padre", "Madre", "Cónyuge", "Hijo/a", "Nieto/a", "Otros"})

        ' Nivel de estudio
        cmbNivelEstudio.Items.Clear()
        cmbNivelEstudio.Items.AddRange({"Guarderia", "Primario", "Secundario", "Terciario", "Universitario", "Postgrado", "Sin Estudio"})

        ' Tipo de equipamiento
        cmbTipoEquipamiento.Items.Clear()
        cmbTipoEquipamiento.Items.AddRange({"Celular", "Notebook", "Otros"})

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
    Private Function ObtenerCUIL(dni As String, genero As String) As String
        Dim prefijo As String
        Dim cuilSinVerificador As String
        Dim suma As Integer
        Dim verificador As Integer
        Dim factores() As Integer = {5, 4, 3, 2, 7, 6, 5, 4, 3, 2}

        ' Determinar el prefijo según el género
        Select Case genero
            Case "M" ' Masculino
                prefijo = "20"
            Case "F" ' Femenino
                prefijo = "27"
            Case "E" ' Extranjero
                prefijo = "23"
            Case Else
                Return "Género no válido"
        End Select

        ' Crear la base del CUIL sin el dígito verificador
        cuilSinVerificador = prefijo & dni

        ' Calcular la suma del producto de cada dígito por su factor correspondiente
        suma = 0
        For i As Integer = 0 To 9
            suma += Convert.ToInt32(cuilSinVerificador(i).ToString()) * factores(i)
        Next

        ' Calcular el dígito verificador
        verificador = 11 - (suma Mod 11)

        If verificador = 11 Then
            verificador = 0
        ElseIf verificador = 10 Then
            ' Ajustar el prefijo si el verificador es 10
            If prefijo = "20" Then
                prefijo = "23"
            ElseIf prefijo = "27" Then
                prefijo = "23"
            ElseIf prefijo = "23" Then
                prefijo = "27"
            End If
            cuilSinVerificador = prefijo & dni

            ' Recalcular la suma y el verificador con el nuevo prefijo
            suma = 0
            For i As Integer = 0 To 9
                suma += Convert.ToInt32(cuilSinVerificador(i).ToString()) * factores(i)
            Next
            verificador = 11 - (suma Mod 11)

            If verificador = 11 Then
                verificador = 0
            ElseIf verificador = 10 Then
                verificador = 9 ' Ajuste adicional si es necesario
            End If
        End If

        ' Formar el CUIL completo
        Return prefijo & dni & verificador
    End Function

    Private Sub chkBaja_CheckedChanged(sender As Object, e As EventArgs) Handles chkBaja.CheckedChanged
        If chkBaja.Checked Then
            dtpBaja.Visible = True
            CmbMotivo.Visible = True
        Else
            dtpBaja.Visible = False
            CmbMotivo.Visible = False
            CmbMotivo.SelectedIndex = -1
        End If
    End Sub

    Private Sub DgvGrupoFamiliar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvGrupoFamiliar.CellContentClick

    End Sub
End Class