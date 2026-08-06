Imports Microsoft.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class frmAgentes
    Private Const NombreColumnaSeleccionBonos As String = "SeleccionadoBono"
    Private _suspenderAccionFiltros As Boolean = False
    'Public Property MostrarSoloEventuales As Boolean?

    Private tabla As New DataTable()
    Private tablaGrupoFamiliar As New DataTable()
    Private tablaComentarios As New DataTable()
    Private tablaTalles As DataTable
    Private tablaBonos As DataTable
    Private handlersTallesAgentesInicializados As Boolean = False
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmAgentes

    ' Variables para gesti�n de grupo familiar
    Private filaGrupoActual As DataGridViewRow
    Private filaGrupoActualIndice As Integer = -1

    ' Variables para gesti�n de comentarios
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
        GridBuscar()
        ConfiguraColListado()
        ConfiguraColComentario()
        ConfiguraColFamilia()
        ConfiguraColEquipamiento()
        ModificaAgente()
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
    Private Sub CmbCate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCate.SelectedIndexChanged
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
    Private Sub radBaja_CheckedChanged(sender As Object, e As EventArgs) Handles radBaja.CheckedChanged
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
            txtLegajo.BackColor = System.Drawing.Color.Gold
            txtLegajo.ReadOnly = False
        Else
            txtLegajo.BackColor = System.Drawing.SystemColors.Control
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
        txtLegajo.BackColor = System.Drawing.Color.Gold
        txtLegajo.ReadOnly = False
        txtLegajo.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        FormModoEdicion()
        txtNombre.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("�Est� seguro de que desea eliminar este agente?", "Confirmar borrado", MessageBoxButtons.YesNo) = DialogResult.Yes Then
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

        If MessageBox.Show("�Est� seguro de que desea eliminar este familiar?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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

        If MessageBox.Show("�Est� seguro de que desea eliminar este comentario?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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

    Private Sub btnAgregarUniforme_Click(sender As Object, e As EventArgs) Handles btnAgregarUniforme.Click
        Dim legajoText = txtLegajo.Text.Trim
        Dim legajo As Integer
        If Not Integer.TryParse(legajoText, legajo) Then
            MessageBox.Show("El Legajo debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLegajo.Focus()
            Return
        End If

        Dim tipoUniforme = cmbTipoUniforme.Text.Trim
        Dim talleUniforme = cmbTalleUniforme.Text.Trim
        Dim fecha = dtpFechaUniforme.Value
        Dim observaciones = txtObservacionesUniforme.Text.Trim


        Dim sql = "INSERT INTO Uniformes (Legajo, TipoUniforme, Talle, Fecha, Observaciones) " &
                          "VALUES (@Legajo, @TipoUniforme, @Talle, @Fecha, @Observaciones)"
        Dim parametros = CmdParams(
            "@Legajo", legajo,
            "@TipoUniforme", If(String.IsNullOrEmpty(tipoUniforme), DBNull.Value, tipoUniforme),
            "@Talle", If(String.IsNullOrEmpty(talleUniforme), DBNull.Value, talleUniforme),
            "@Fecha", fecha,
            "@Observaciones", If(String.IsNullOrEmpty(observaciones), DBNull.Value, observaciones)
        )
        DSM.Execute(DSM.Personal, sql, parametros, True)

        FormModoConsulta()
        GridBuscar()
        ConfiguraColListado()
        CargaUniformes(Convert.ToInt32(txtLegajo.Text.Trim))
    End Sub
    Private Sub btnEliminarEquipamiento_Click(sender As Object, e As EventArgs) Handles btnEliminarEquipamiento.Click
        If DgvEquipamiento.CurrentRow Is Nothing Then Return

        If MessageBox.Show("�Est� seguro de que desea eliminar este equipamiento?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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
    Private Sub btnEliminarUniforme_Click(sender As Object, e As EventArgs) Handles btnEliminarUniforme.Click
        If dgvUniformes.CurrentRow Is Nothing Then Return

        If MessageBox.Show("�Est� seguro de que desea eliminar este uniforme?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim Principal = Convert.ToInt32(dgvUniformes.CurrentRow.Cells("IdUniforme").Value)
            Dim sql = "DELETE FROM Uniformes WHERE IdUniforme = @Id"
            Dim parametros = CmdParams("@Id", Principal)
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
            ConfiguraColListado()
            CargaUniformes(Convert.ToInt32(txtLegajo.Text.Trim))
        End If
    End Sub
    Private Sub txtNroDto_LostFocus(sender As Object, e As EventArgs) Handles txtNroDto.LostFocus
        ' Validar DNI
        'If Len(txtNroDto.Text) < 7 Then
        '    MessageBox.Show("Debe ingresar un DNI v�lido.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    txtNroDto.Focus()
        '    Return
        'End If
        If cmbTipoDto.Text = "DNI" And Len(txtNroDto.Text) >= 7 Then
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
    ' M�todos de validaci�n
    Private Function ValidarDatos() As Boolean
        ' Validar Legajo
        If String.IsNullOrEmpty(txtLegajo.Text.Trim) Then
            MessageBox.Show("El campo Legajo no puede estar vac�o.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLegajo.Focus()
            Return False
        End If

        ' Validar que el legajo sea num�rico
        Dim legajo As Integer
        If Not Integer.TryParse(txtLegajo.Text.Trim, legajo) Then
            MessageBox.Show("El Legajo debe ser un n�mero v�lido.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLegajo.Focus()
            Return False
        End If

        ' Validar Nombre
        If String.IsNullOrEmpty(txtNombre.Text.Trim) Then
            MessageBox.Show("El campo Nombre no puede estar vac�o.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombre.Focus()
            Return False
        End If

        ' Validar Instituto
        If String.IsNullOrEmpty(cmbInstituto.Text.Trim) Then
            MessageBox.Show("Debe seleccionar una Sucursal.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbInstituto.Focus()
            Return False
        End If

        ' Validar Caracter
        If String.IsNullOrEmpty(cmbCaracter.Text.Trim) Then
            MessageBox.Show("Debe seleccionar un Caracter.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCaracter.Focus()
            Return False
        End If

        If dtpNacimiento.Value = dtpNacimiento.MinDate Then
            MessageBox.Show("Debe seleccionar una fecha de nacimiento v�lida.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpNacimiento.Focus()
            Return False
        End If

        ' Validar DNI
        If String.IsNullOrEmpty(txtNroDto.Text.Trim) Or Len(txtNroDto.Text) < 7 Then
            MessageBox.Show("Debe ingresar un DNI v�lido.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroDto.Focus()
            Return False
        End If

        ' Validar fecha de baja si chkBaja esta seleccionado   
        If chkBaja.Checked Then
            If dtpBaja.Value = Date.MinValue Then
                MessageBox.Show("Seleccione una fecha de baja v�lida.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                dtpBaja.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(CmbMotivo.Text.Trim) Then
                MessageBox.Show("Seleccione un motivo de baja.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbMotivo.Focus()
                Return False
            End If
        End If

        Return True
    End Function
    Private Function ValidarDatosFamiliar() As Boolean

        If String.IsNullOrEmpty(txtNombreFamiliar.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Nombre.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombreFamiliar.Focus()
            Return False
        End If

        If dtpNacimientoFamiliar.Value = Date.MinValue Then
            MessageBox.Show("Seleccione una fecha de nacimiento v�lida.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpNacimientoFamiliar.Focus()
            Return False
        End If

        If Not IsNumeric(txtEdadFamiliar.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor num�rico para la Edad.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEdadFamiliar.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(cmbParentesco.Text.Trim()) Then
            MessageBox.Show("Seleccione un valor para el campo Parentesco.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbParentesco.Focus()
            Return False
        End If
        Return True
    End Function
    Private Function ValidarDatosComentarios() As Boolean

        If String.IsNullOrEmpty(txtComentaComentario.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Comentario.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombreFamiliar.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(cmbMotivoComentario.Text.Trim()) Then
            MessageBox.Show("Seleccione un valor para el campo Motivo.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbParentesco.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarDatosEquipamiento() As Boolean
        If String.IsNullOrEmpty(cmbTipoEquipamiento.Text.Trim()) Then
            MessageBox.Show("Seleccione un valor para el campo Tipo.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbTipoEquipamiento.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtMarcaEquipamiento.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Marca.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMarcaEquipamiento.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtModeloEquipamiento.Text.Trim()) Then
            MessageBox.Show("Ingrese un valor para el campo Modelo.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtModeloEquipamiento.Focus()
            Return False
        End If

        If dtpFechaEquipamiento.Value = Date.MinValue Then
            MessageBox.Show("Seleccione una fecha v�lida.", "Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpFechaEquipamiento.Focus()
            Return False
        End If

        Return True
    End Function

    ' M�todos CRUD para Agentes
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
                           "iNGRESO, Baja, CUIL, TITULO, UltimaActualizacion,  EstadoParental, FechaJubilacion, LegajoEventual, Sindicato) " &
                           "VALUES (@Legajo, @TipoDto, @NroDto,   @Instituto, @Nombre, @CorreoE, @Sexo, @Nacimiento, " &
                           "@Calle, @Nro, @Localidad,  @HorasDiarias, " &
                           "@Escalafon, @Jefe, @LicAnual, @Caracter, @Comentario, @Nomarca, @Cargo, @Telefono, @Interno, @Celular, @Urgencias,  " &
                           "@iNGRESO, @Baja, @CUIL, @TITULO, @UltimaActualizacion, @EstadoParental, @FechaJubilacion, @LegajoEventual, @Sindicato)"

        Dim parametros = ObtenerParametrosAgente(True)
        DSM.Execute(DSM.Personal, sql, parametros, True)
        MessageBox.Show("Datos guardados correctamente.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function ActualizarAgente() As Boolean
        Dim sql As String = "UPDATE Agentes SET Legajo=@Legajo, LegajoEventual=@LegajoEventual, " &
                           "TipoDto=@TipoDto, NroDto=@NroDto,  Instituto=@Instituto, " &
                           "Nombre=@Nombre, CorreoE=@CorreoE, Sexo=@Sexo, Nacimiento=@Nacimiento, Calle=@Calle, Nro=@Nro, " &
                           "Localidad=@Localidad, HorasDiarias=@HorasDiarias, " &
                           "Escalafon=@Escalafon, Jefe=@Jefe, LicAnual=@LicAnual, Caracter=@Caracter, Comentario=@Comentario, " &
                           "Nomarca=@Nomarca, Cargo=@Cargo, Telefono=@Telefono, Interno=@Interno, Celular=@Celular, Rpv=@Urgencias, " &
                           "iNGRESO=@iNGRESO, Baja=@Baja, Motivo=@Motivo, CUIL=@CUIL, TITULO=@TITULO, UltimaActualizacion=@UltimaActualizacion, " &
                           "EstadoParental=@EstadoParental, FechaJubilacion=@FechaJubilacion, Sindicato=@Sindicato " &
                           "WHERE Legajo=@LegajoEventual"
        Try
            Dim parametros = ObtenerParametrosAgente()

            If cmbCaracter.Text = "Eventual" AndAlso (filaActual.Cells("Caracter").Value = "Efectivo") Then
                Throw New Exception("Cambio de Efectivo a Eventual no permitido.")
            End If

            DSM.Execute(DSM.Personal, sql, parametros, True)
            MessageBox.Show("Datos guardados correctamente.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Dim baja As String = ""
        If (Not esNuevo) Then
            baja = If(Not chkBaja.Checked OrElse dtpBaja.Value = dtpBaja.MinDate, "", dtpBaja.Value.ToString().Substring(0, 10))
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
            {"@Baja", baja},
            {"@CUIL", If(String.IsNullOrEmpty(txtCUIL.Text.Trim), DBNull.Value, txtCUIL.Text.Trim)},
            {"@TITULO", If(String.IsNullOrEmpty(txtTitulo.Text.Trim), DBNull.Value, txtTitulo.Text.Trim)},
            {"@UltimaActualizacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"@EstadoParental", If(String.IsNullOrEmpty(cmbEstadoParental.Text.Trim), DBNull.Value, cmbEstadoParental.Text.Trim)},
            {"@Motivo", If(Not chkBaja.Checked OrElse String.IsNullOrEmpty(CmbMotivo.Text.Trim), DBNull.Value, CmbMotivo.Text.Trim)},
            {"@FechaJubilacion", If(String.IsNullOrEmpty(txtFechaJubilacion.Text.Trim), DBNull.Value, txtFechaJubilacion.Text.Trim)},
            {"@Sindicato", chkSindicato.Checked}
        }
    End Function

    ' M�todos CRUD para GrupoFamiliar
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

    ' M�todos CRUD para Comentarios
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
                dtpNacimiento.Format = DateTimePickerFormat.Custom
                dtpNacimiento.CustomFormat = "dd/MM/yyyy"
                dtpNacimiento.Value = Convert.ToDateTime(row("Nacimiento"))
            Else
                dtpNacimiento.Format = DateTimePickerFormat.Custom
                dtpNacimiento.CustomFormat = " "
                dtpNacimiento.Value = dtpNacimiento.MinDate
            End If

            txtCalle.Text = If(IsDBNull(row("Calle")), "", row("Calle").ToString())
            txtNro.Text = If(IsDBNull(row("Nro")), "", row("Nro").ToString())
            txtLocalidad.Text = If(IsDBNull(row("Localidad")), "", row("Localidad").ToString())
            txtUrgencias.Text = If(IsDBNull(row("HorasDedicacion")), "", row("HorasDedicacion").ToString())

            cmbHorasDiarias.Text = If(IsDBNull(row("HorasDiarias")), "", row("HorasDiarias").ToString())
            cmbEscalafon.Text = If(IsDBNull(row("Escalafon")), "", row("Escalafon").ToString())
            cmbJefe.Text = If(IsDBNull(row("Jefe")), "", row("Jefe").ToString())
            cmbCaracter.Text = If(IsDBNull(row("Caracter")), "", row("Caracter").ToString())
            txtComentario.Text = If(IsDBNull(row("Comentario")), "", row("Comentario").ToString())

            Dim licAnual = Vacaciones.ObtenerDiasDisponiblesVacaciones(txtLegajo.Text)
            txtLicAnual.Text = licAnual

            chkNomarca.Checked = If(IsDBNull(row("Nomarca")), False, Convert.ToBoolean(row("Nomarca")))
            cmbCategoria.Text = If(IsDBNull(row("Cargo")), "", row("Cargo").ToString())
            txtTelefono.Text = If(IsDBNull(row("Telefono")), "", row("Telefono").ToString())
            txtInterno.Text = If(IsDBNull(row("Interno")), "", row("Interno").ToString())
            txtCelular.Text = If(IsDBNull(row("Celular")), "", row("Celular").ToString())
            txtUrgencias.Text = If(IsDBNull(row("Rpv")), "", row("Rpv").ToString())
            txtUltimaActualizacion.Text = If(IsDBNull(row("UltimaActualizacion")), "", row("UltimaActualizacion").ToString())

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

            If Not IsDBNull(row("iNGRESO")) AndAlso row("iNGRESO").ToString().Trim() <> "" Then
                dtpIngreso.Value = Convert.ToDateTime(row("iNGRESO"))
                CalcularAntiguedad()
            End If

            txtCUIL.Text = If(IsDBNull(row("CUIL")), "", row("CUIL").ToString())
            txtTitulo.Text = If(IsDBNull(row("TITULO")), "", row("TITULO").ToString())

            cmbEstadoParental.Text = If(IsDBNull(row("EstadoParental")), "", row("EstadoParental").ToString())
            txtFechaJubilacion.Text = If(IsDBNull(row("FechaJubilacion")), "", row("FechaJubilacion").ToString())
            cmbTipoDto.Text = If(IsDBNull(row("TipoDto")), "", row("TipoDto").ToString())
            txtNroDto.Text = If(IsDBNull(row("NroDto")), "", row("NroDto").ToString())
            chkSindicato.Checked = If(IsDBNull(row("Sindicato")), False, Convert.ToBoolean(row("Sindicato")))

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

            ActualizarSaldoVacaciones()
        End If
    End Sub

    Private Sub dtpBaja_ValueChanged(sender As Object, e As EventArgs) Handles dtpBaja.ValueChanged
        Dim baja = If(dtpBaja.Value = dtpBaja.MinDate, DBNull.Value, dtpBaja.Value)
        If Not IsDBNull(baja) Then
            chkBaja.Checked = True
            dtpBaja.Visible = True
            dtpBaja.Format = DateTimePickerFormat.Custom
            dtpBaja.CustomFormat = "dd/MM/yyyy"  ' Formato normal de fecha
            dtpBaja.Value = Convert.ToDateTime(baja)
        Else
            chkBaja.Checked = False
            dtpBaja.Visible = False
            dtpBaja.Format = DateTimePickerFormat.Custom
            dtpBaja.CustomFormat = " "  ' Espacio en blanco = no muestra nada
            dtpBaja.Value = dtpBaja.MinDate
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

            ' Filtrar por Baja
            If radBaja.Checked Then
                sql &= " AND (Baja IS NOT NULL AND Baja <> '')"
            End If

            Dim sucursalActualId As Integer
            If Integer.TryParse(General.SucursalActual, sucursalActualId) AndAlso sucursalActualId = 3 Then
                sql &= " AND Instituto IN (@Inst1, @Inst2, @Inst3)"
                parametros.AddRange(New Object() {"@Inst1", "Alcorta", "@Inst2", "Belgrano", "@Inst3", "Garay"})
            End If

            Dim sucursal As String = cmbSucursal.Text.Trim()
            If Not String.IsNullOrEmpty(sucursal) And sucursal <> "(Todas)" Then
                sql &= " AND Instituto = @Sucursal"
                parametros.AddRange(New Object() {"@Sucursal", sucursal})
            End If

            Dim categoria As String = CmbCate.Text.Trim()
            If Not String.IsNullOrEmpty(categoria) And categoria <> "(Todas)" Then
                sql &= " AND Cargo = @Cargo"
                parametros.AddRange(New Object() {"@Cargo", categoria})
            End If

            ' Filtro por b�squeda en varias columnas
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

            ConfiguraColListado()

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

                ' Ajuste para que no cuente el a�o si todav�a no cumpli�
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

            ' Configurar columnas usando el m�todo dedicado
            ConfiguraColEquipamiento()

        Catch ex As Exception
            MessageBox.Show("Error al cargar equipamiento: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CargaUniformes(legajo As Integer)
        Try
            ' Traer los registros de uniformes
            Dim sql As String = "SELECT * FROM Uniformes WHERE Legajo = @Legajo ORDER BY Fecha DESC"
            Dim parametros As New List(Of Object) From {"@Legajo", legajo}
            Dim tabla As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
            dgvUniformes.DataSource = tabla

            ' Configurar columnas usando el m�todo dedicado
            ConfiguraColUniformes()
        Catch ex As Exception
            MessageBox.Show("Error al cargar uniformes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CargaTiposUniformes(legajo As Integer)
        Try
            If tablaTalles Is Nothing Then
                Dim sqlTalles As String = "SELECT idTalle, Talle FROM Talles ORDER BY Talle"
                tablaTalles = DSM.ExecuteQuery(DSM.Personal, sqlTalles)
            End If

            Dim sql As String =
                "SELECT tu.idTipoUniforme, tu.TipoUniforme, at.idTalle, at.FechaActualizacion " &
                "FROM TipoUniformes tu " &
                "LEFT JOIN AgenteTalles at ON at.Legajo = @Legajo AND at.idTipoUniforme = tu.idTipoUniforme " &
                "ORDER BY tu.TipoUniforme"

            Dim parametros = CmdParams("@Legajo", legajo)
            Dim tabla As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
            dgvTallesAgentes.DataSource = tabla

            ' Configurar columnas usando el m�todo dedicado
            ConfiguraColTipoUniformes()
        Catch ex As Exception
            MessageBox.Show("Error al cargar tipos de uniformes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CargaBonos(legajo As Integer)
        Try
            ' Traer los registros de uniformes
            Dim sql As String = "SELECT * FROM RecibosProcesosDetalles WHERE Legajo = @Legajo  ORDER BY PeriodoProcesado DESC" 'AND PeriodoProcesado >= @FechaInicio AND PeriodoProcesado <= @FechaFin
            'Dim parametros As New List(Of Object) From {"@Legajo", legajo, "@FechaInicio", dtpFechaInicio.Value, "@FechaFin", dtpFechaFin.Value}
            Dim parametros As New List(Of Object) From {
            "@Legajo", legajo}
            Dim tabla As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
            dgvBonos.DataSource = tabla
            AsegurarColumnaSeleccionBonos()

            ' Configurar columnas usando el mtodo dedicado
            ConfiguraColBonos()
        Catch ex As Exception
            MessageBox.Show("Error al cargar bonos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' M�todos auxiliares
    Private Sub LimpiarFormulario()

        ' datos del agente
        txtLegajo.Clear()
        txtLegajoEventual.Clear()
        txtNombre.Clear()
        dtpNacimiento.Value = dtpNacimiento.MinDate
        dtpNacimiento.Format = DateTimePickerFormat.Custom
        dtpNacimiento.CustomFormat = " "
        cmbSexo.SelectedIndex = -1
        cmbTipoDto.SelectedIndex = -1
        txtNroDto.Clear()
        txtCalle.Clear()
        txtNro.Clear()
        txtLocalidad.Clear()
        txtTelefono.Clear()
        txtUrgencias.Clear()
        txtTitulo.Clear()
        txtCorreoE.Clear()
        txtCelular.Clear()

        txtCUIL.Clear()
        cmbCategoria.SelectedIndex = -1
        cmbCaracter.SelectedIndex = -1
        cmbInstituto.SelectedIndex = -1
        cmbEscalafon.SelectedIndex = -1
        cmbJefe.SelectedIndex = -1
        cmbHorasDiarias.SelectedIndex = -1
        cmbEstadoParental.SelectedIndex = -1
        txtInterno.Clear()

        dtpIngreso.Value = DateTime.Now
        txtAntiguedad.Clear()
        txtLicAnual.Clear()
        txtFechaJubilacion.Clear()
        chkBaja.Checked = False
        dtpBaja.Value = dtpBaja.MinDate
        CmbMotivo.SelectedIndex = -1
        txtUltimaActualizacion.Clear()
        txtComentario.Clear()

        pctFoto.Image = Nothing
        chkNomarca.Checked = False

        ' limpiar grupo familiar
        txtNombreFamiliar.Clear()
        cmbParentesco.SelectedIndex = -1
        dtpNacimientoFamiliar.Value = DateTime.Now
        txtOcupacionFamiliar.Clear()
        cmbNivelEstudio.SelectedIndex = -1
        txtEdadFamiliar.Clear()

        ' Limpiar campos de comentarios
        txtComentaComentario.Clear()
        cmbMotivoComentario.SelectedIndex = -1
        dtpFechaComentario.Value = DateTime.Now

        ' Limpiar campos de equipamiento
        cmbTipoEquipamiento.SelectedIndex = -1
        txtMarcaEquipamiento.Clear()
        txtModeloEquipamiento.Clear()
        txtNroSerieEquipamiento.Clear()
        txtIMEIEquipamiento.Clear()
        dtpFechaEquipamiento.Value = DateTime.Now
        txtNroTelEquipoamiento.Clear()
        txtObservacionesEquipamiento.Clear()

        ' Limpiar DataGridViews
        DgvGrupoFamiliar.DataSource = Nothing
        DgvComentarios.DataSource = Nothing
        DgvEquipamiento.DataSource = Nothing

        ConfiguraColListado()
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            CargarDatosEnFormulario(CType(filaActual.DataBoundItem, DataRowView).Row)
            CargaGrupoFamiliar(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaComentario(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaEquipamiento(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaUniformes(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaTiposUniformes(Convert.ToInt32(txtLegajo.Text.Trim))
            CargaBonos(Convert.ToInt32(txtLegajo.Text.Trim))
            ModificaAgente()
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
            ' si tiene fecha de baja, usar esa fecha para el c�lculo, sino la fecha actual
            Dim fechaActual As DateTime = If(chkBaja.Checked AndAlso dtpBaja.Value > dtpBaja.MinDate, dtpBaja.Value, DateTime.Now)

            ' Calcular a�os completos
            Dim años As Integer = fechaActual.Year - fechaIngreso.Year

            ' Ajustar si a�n no ha pasado el aniversario este a�o
            If fechaActual.Month < fechaIngreso.Month OrElse (fechaActual.Month = fechaIngreso.Month AndAlso fechaActual.Day < fechaIngreso.Day) Then
                años -= 1
            End If

            If años >= 1 Then
                ' Si tiene 1 a�o o m�s, mostrar solo a�os
                If años = 1 Then
                    txtAntiguedad.Text = "1 a�o"
                Else
                    txtAntiguedad.Text = años.ToString() & " a�os"
                End If
            Else
                ' Si tiene menos de 1 a�o, calcular meses
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

            lblAntiguedadCorregida.Text = ""
            Dim AjusteAntiguedad As Integer = filaActual.Cells("AjusteAntiguedad").Value
            If (AjusteAntiguedad <> 0) Then
                lblAntiguedadCorregida.Text = "Antiguedad Corregida: " & (años + AjusteAntiguedad).ToString() & " a�os"
            End If
        Else
            txtAntiguedad.Text = "Fecha futura"
        End If
    End Sub

    Private Sub HabilitarControles(habilitar As Boolean)
        ' Usar la funci�n SetControlesEnabled de Funciones.vb para habilitar/deshabilitar controles
        Funciones.SetControlesEnabled(habilitar, txtLegajo, txtNombre, cmbInstituto, txtCorreoE, cmbSexo, dtpNacimiento,
                           txtCalle, txtNro, txtLocalidad,
                           txtUrgencias, cmbHorasDiarias, cmbEscalafon, cmbJefe,
                            cmbCaracter, txtComentario, chkNomarca, cmbCategoria, txtTelefono,
                           txtInterno, txtCelular, txtUltimaActualizacion, dtpIngreso, chkBaja, dtpBaja, txtTitulo,
                            cmbEstadoParental, txtFechaJubilacion, cmbTipoDto, txtCUIL, txtNroDto, CmbMotivo, txtAntiguedad, chkSindicato)
        'txtLicAnual, 
    End Sub
    Private Sub dtpNacimiento_ValueChanged(sender As Object, e As EventArgs) Handles dtpNacimiento.ValueChanged
        If dtpNacimiento.Value = dtpNacimiento.MinDate Then
            dtpNacimiento.Format = DateTimePickerFormat.Custom
            dtpNacimiento.CustomFormat = " "
        Else
            dtpNacimiento.Format = DateTimePickerFormat.Custom
            dtpNacimiento.CustomFormat = "dd/MM/yyyy"
        End If
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

        ' Ejecutar la misma l�gica del RowLeave para actualizar la base de datos
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

        ' Ejecutar la misma l�gica del RowLeave para actualizar la base de datos
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

        Dim sqlUpdateEquipamiento = "
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

    Private Sub dgvTallesAgentes_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs)
        If dgvTallesAgentes Is Nothing OrElse dgvTallesAgentes.CurrentCell Is Nothing Then Return
        If dgvTallesAgentes.IsCurrentCellDirty Then
            dgvTallesAgentes.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgvTallesAgentes_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If dgvTallesAgentes Is Nothing Then Return
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If dgvTallesAgentes.Columns(e.ColumnIndex).Name <> "Talle" Then Return

        Dim legajoText As String = txtLegajo.Text.Trim()
        Dim legajo As Integer
        If Not Integer.TryParse(legajoText, legajo) Then Return

        Dim row = dgvTallesAgentes.Rows(e.RowIndex)
        If row Is Nothing Then Return

        Dim idTipoUniformeObj = row.Cells("idTipoUniforme").Value
        If idTipoUniformeObj Is Nothing OrElse idTipoUniformeObj Is DBNull.Value Then Return

        Dim idTipoUniforme As Integer = Convert.ToInt32(idTipoUniformeObj)
        Dim tipoUniforme As String = Convert.ToString(row.Cells("TipoUniforme").Value)

        Dim idTalleObj = row.Cells("Talle").Value
        If idTalleObj Is Nothing OrElse idTalleObj Is DBNull.Value Then
            Dim sqlDelete As String = "DELETE FROM AgenteTalles WHERE Legajo = @Legajo AND idTipoUniforme = @idTipoUniforme"
            DSM.Execute(DSM.Personal, sqlDelete, CmdParams("@Legajo", legajo, "@idTipoUniforme", idTipoUniforme), True)
            If dgvTallesAgentes.Columns.Contains("FechaActualizacion") Then
                row.Cells("FechaActualizacion").Value = DBNull.Value
            End If
            Return
        End If

        Dim idTalle As Integer = Convert.ToInt32(idTalleObj)
        Dim talle As String = ""

        If tablaTalles IsNot Nothing Then
            Dim rows = tablaTalles.Select("idTalle = " & idTalle.ToString())
            If rows IsNot Nothing AndAlso rows.Length > 0 Then
                talle = Convert.ToString(rows(0)("Talle"))
            End If
        End If

        Dim fechaActualizacion As DateTime = DateTime.Now

        Dim sqlUpsert As String = "
IF EXISTS (SELECT 1 FROM AgenteTalles WHERE Legajo = @Legajo AND idTipoUniforme = @idTipoUniforme)
    UPDATE AgenteTalles
    SET
        idTalle = @idTalle,
        FechaActualizacion = @FechaActualizacion
    WHERE Legajo = @Legajo AND idTipoUniforme = @idTipoUniforme
ELSE
    INSERT INTO AgenteTalles (Legajo, idTipoUniforme, idTalle, FechaActualizacion)
    VALUES (@Legajo, @idTipoUniforme,  @idTalle, @FechaActualizacion)
"

        Dim parametros = CmdParams(
            "@Legajo", legajo,
            "@idTipoUniforme", idTipoUniforme,
            "@idTalle", idTalle,
            "@FechaActualizacion", fechaActualizacion
        )

        DSM.Execute(DSM.Personal, sqlUpsert, parametros, True)
        If dgvTallesAgentes.Columns.Contains("FechaActualizacion") Then
            row.Cells("FechaActualizacion").Value = fechaActualizacion
        End If
    End Sub

    Private Sub dgvTallesAgentes_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        e.ThrowException = False
    End Sub

    Private Sub btnDocumentacion_Click(sender As Object, e As EventArgs) Handles btnDocumentacion.Click
        ' Verificar que hay un agente seleccionado
        If filaActual Is Nothing OrElse filaActualIndice < 0 Then
            MessageBox.Show("Debe seleccionar un agente para ver su documentaci�n.", "Informaci�n", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Obtener el legajo del agente seleccionado
        Dim legajo = txtLegajo.Text 'filaActual.Cells("Legajo").Value.ToString

        ' Crear y mostrar el formulario de documentaci�n
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
                dgvListado.Columns("Instituto").Width = 80
                dgvListado.Columns("Instituto").DisplayIndex = 3

                dgvListado.Columns("Telefono").Visible = True
                dgvListado.Columns("Telefono").HeaderText = "Tel�fono"
                dgvListado.Columns("Telefono").Width = 110
                dgvListado.Columns("Telefono").DisplayIndex = 4

                dgvListado.Columns("Celular").Visible = True
                dgvListado.Columns("Celular").HeaderText = "Celular"
                dgvListado.Columns("Celular").Width = 110
                dgvListado.Columns("Celular").DisplayIndex = 5

                dgvListado.Columns("RPV").Visible = True
                dgvListado.Columns("RPV").HeaderText = "Urgencias"
                dgvListado.Columns("RPV").Width = 130
                dgvListado.Columns("RPV").DisplayIndex = 6

                dgvListado.Columns("Interno").Visible = True
                dgvListado.Columns("Interno").HeaderText = "Interno"
                dgvListado.Columns("Interno").Width = 60
                dgvListado.Columns("Interno").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvListado.Columns("Interno").DisplayIndex = 7

                dgvListado.Columns("CorreoE").Visible = True
                dgvListado.Columns("CorreoE").HeaderText = "E-Mail"
                dgvListado.Columns("CorreoE").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dgvListado.Columns("CorreoE").DisplayIndex = 8

                dgvListado.Columns("Caracter").Visible = True
                dgvListado.Columns("Caracter").HeaderText = "Caracter"
                dgvListado.Columns("Caracter").Width = 120
                dgvListado.Columns("Caracter").DisplayIndex = 9

            End If
        Catch ex As Exception
            ' Ignorar errores de configuraci�n de columnas
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
                    ' Obtener el �ndice de la columna actual
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

                    ' Insertar en la posici�n original
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
                DgvGrupoFamiliar.Columns("Ocupacion").HeaderText = "Ocupaci�n"
                DgvGrupoFamiliar.Columns("Ocupacion").Width = 150

                ' Configurar columna Parentesco como ComboBox
                If DgvGrupoFamiliar.Columns.Contains("Nivel") Then
                    ' Obtener el �ndice de la columna actual
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

                    ' Insertar en la posici�n original
                    DgvGrupoFamiliar.Columns.Insert(indiceNivel, colNivel)
                End If

                ConfigurarEstiloGrid(DgvGrupoFamiliar)

            End If
        Catch ex As Exception
            ' Ignorar errores de configuraci�n de columnas
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
                    ' Obtener el �ndice de la columna actual
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

                    ' Insertar en la posici�n original
                    DgvComentarios.Columns.Insert(indiceMotivo, colMotivo)
                End If

                ConfigurarEstiloGrid(DgvComentarios)

            End If
        Catch ex As Exception
            ' Ignorar errores de configuraci�n de columnas
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
                    ' Obtener el �ndice de la columna actual
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

                    ' Insertar en la posici�n original
                    DgvEquipamiento.Columns.Insert(indiceTipo, colTipo)
                End If

                DgvEquipamiento.Columns("Marca").Visible = True
                DgvEquipamiento.Columns("Marca").HeaderText = "Marca"
                DgvEquipamiento.Columns("Marca").Width = 80

                DgvEquipamiento.Columns("Modelo").Visible = True
                DgvEquipamiento.Columns("Modelo").HeaderText = "Modelo"
                DgvEquipamiento.Columns("Modelo").Width = 100

                DgvEquipamiento.Columns("NroTel").Visible = True
                DgvEquipamiento.Columns("NroTel").HeaderText = "Nro. Tel�fono"
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
            ' Ignorar errores de configuraci�n de columnas
        End Try
    End Sub
    Private Sub ConfiguraColUniformes()
        Try
            If dgvUniformes.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In dgvUniformes.Columns
                    col.Visible = False
                Next
                ' Configurar columnas del grid de uniformes
                dgvUniformes.Columns("idUniforme").Visible = False
                dgvUniformes.Columns("idUniforme").HeaderText = "Id"
                dgvUniformes.Columns("Legajo").Visible = False
                dgvUniformes.Columns("Legajo").HeaderText = "Legajo"
                dgvUniformes.Columns("TipoUniforme").Visible = True
                dgvUniformes.Columns("TipoUniforme").HeaderText = "Tipo"
                dgvUniformes.Columns("TipoUniforme").Width = 150
                dgvUniformes.Columns("Talle").Visible = True
                dgvUniformes.Columns("Talle").HeaderText = "Talle"
                dgvUniformes.Columns("Talle").Width = 80
                dgvUniformes.Columns("Fecha").Visible = True
                dgvUniformes.Columns("Fecha").HeaderText = "Fecha"
                dgvUniformes.Columns("Fecha").Width = 100
                dgvUniformes.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"
                dgvUniformes.Columns("Observaciones").Visible = True
                dgvUniformes.Columns("Observaciones").HeaderText = "Observaciones"
                dgvUniformes.Columns("Observaciones").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ConfigurarEstiloGrid(dgvUniformes)
            End If
        Catch ex As Exception
            ' Ignorar errores de configuraci�n de columnas
        End Try
    End Sub

    Private Sub ConfiguraColTipoUniformes()
        Try
            If dgvTallesAgentes.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In dgvTallesAgentes.Columns
                    col.Visible = False
                Next

                dgvTallesAgentes.ReadOnly = False

                dgvTallesAgentes.Columns("idTipoUniforme").Visible = False
                dgvTallesAgentes.Columns("idTipoUniforme").HeaderText = "Id"
                dgvTallesAgentes.Columns("idTipoUniforme").ReadOnly = True

                dgvTallesAgentes.Columns("TipoUniforme").Visible = True
                dgvTallesAgentes.Columns("TipoUniforme").HeaderText = "Tipo"
                dgvTallesAgentes.Columns("TipoUniforme").Width = 200
                dgvTallesAgentes.Columns("TipoUniforme").ReadOnly = True

                If dgvTallesAgentes.Columns.Contains("idTalle") Then
                    Dim indiceIdTalle As Integer = dgvTallesAgentes.Columns("idTalle").Index
                    dgvTallesAgentes.Columns.Remove("idTalle")

                    Dim colTalle As New DataGridViewComboBoxColumn()
                    colTalle.Name = "Talle"
                    colTalle.HeaderText = "Talle"
                    colTalle.DataPropertyName = "idTalle"
                    colTalle.DisplayMember = "Talle"
                    colTalle.ValueMember = "idTalle"
                    colTalle.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                    colTalle.Width = 120

                    If tablaTalles IsNot Nothing Then
                        colTalle.DataSource = tablaTalles
                    End If

                    dgvTallesAgentes.Columns.Insert(indiceIdTalle, colTalle)
                ElseIf dgvTallesAgentes.Columns.Contains("Talle") Then
                    Dim colTalle = TryCast(dgvTallesAgentes.Columns("Talle"), DataGridViewComboBoxColumn)
                    If colTalle IsNot Nothing Then
                        colTalle.DataPropertyName = "idTalle"
                        colTalle.DisplayMember = "Talle"
                        colTalle.ValueMember = "idTalle"
                        colTalle.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                        If tablaTalles IsNot Nothing Then
                            colTalle.DataSource = tablaTalles
                        End If
                    End If
                End If

                If dgvTallesAgentes.Columns.Contains("Talle") Then
                    dgvTallesAgentes.Columns("Talle").Visible = True
                    dgvTallesAgentes.Columns("Talle").Width = 120
                    dgvTallesAgentes.Columns("Talle").ReadOnly = False
                End If

                If dgvTallesAgentes.Columns.Contains("FechaActualizacion") Then
                    dgvTallesAgentes.Columns("FechaActualizacion").Visible = True
                    dgvTallesAgentes.Columns("FechaActualizacion").HeaderText = "Actualización"
                    dgvTallesAgentes.Columns("FechaActualizacion").Width = 110
                    dgvTallesAgentes.Columns("FechaActualizacion").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvTallesAgentes.Columns("FechaActualizacion").ReadOnly = True
                End If

                ConfigurarEstiloGrid(dgvTallesAgentes)

                If Not handlersTallesAgentesInicializados Then
                    AddHandler dgvTallesAgentes.CurrentCellDirtyStateChanged, AddressOf dgvTallesAgentes_CurrentCellDirtyStateChanged
                    AddHandler dgvTallesAgentes.CellValueChanged, AddressOf dgvTallesAgentes_CellValueChanged
                    AddHandler dgvTallesAgentes.DataError, AddressOf dgvTallesAgentes_DataError
                    handlersTallesAgentesInicializados = True
                End If
            End If
        Catch ex As Exception
            ' Ignorar errores de configuraci�n de columnas
        End Try
    End Sub
    Private Sub ConfiguraColBonos()
        Try
            If dgvBonos.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In dgvBonos.Columns
                    col.Visible = False
                    col.ReadOnly = True
                Next

                dgvBonos.ReadOnly = False
                dgvBonos.AllowUserToAddRows = False

                If dgvBonos.Columns.Contains(NombreColumnaSeleccionBonos) Then
                    dgvBonos.Columns(NombreColumnaSeleccionBonos).Visible = rdbSeleccion.Checked
                    dgvBonos.Columns(NombreColumnaSeleccionBonos).Width = 30
                    dgvBonos.Columns(NombreColumnaSeleccionBonos).ReadOnly = False
                    dgvBonos.Columns(NombreColumnaSeleccionBonos).DisplayIndex = 0
                End If

                dgvBonos.Columns("PeriodoProcesado").Visible = True
                dgvBonos.Columns("PeriodoProcesado").HeaderText = "Periodo Liquidado"
                dgvBonos.Columns("PeriodoProcesado").Width = 150
                dgvBonos.Columns("PeriodoProcesado").DefaultCellStyle.Format = "MM/yyyy"

                dgvBonos.Columns("FechaGeneracion").Visible = True
                dgvBonos.Columns("FechaGeneracion").HeaderText = "Fecha Generacion"
                dgvBonos.Columns("FechaGeneracion").Width = 200
                dgvBonos.Columns("FechaGeneracion").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"

                dgvBonos.Columns("ArchivoPdf").Visible = True
                dgvBonos.Columns("ArchivoPdf").HeaderText = "Archivo PDF"
                dgvBonos.Columns("ArchivoPdf").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                dgvBonos.Columns("EstadoEnvio").Visible = True
                dgvBonos.Columns("EstadoEnvio").HeaderText = "Estado Envio"
                dgvBonos.Columns("EstadoEnvio").Width = 150

                dgvBonos.Columns("FechaEnvio").Visible = True
                dgvBonos.Columns("FechaEnvio").HeaderText = "Fecha Envio"
                dgvBonos.Columns("FechaEnvio").Width = 150
                dgvBonos.Columns("FechaEnvio").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"

                dgvBonos.Columns("RutaPdf").Visible = True
                dgvBonos.Columns("RutaPdf").HeaderText = "Ruta PDF"
                dgvBonos.Columns("RutaPdf").Width = 200


                ConfigurarEstiloGrid(dgvBonos)
            End If
        Catch ex As Exception
            ' Ignorar errores de configuracin de columnas
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

            ' Cargar Tipos de Uniforme y Talles
            CargarCombos(cmbTipoUniforme, "TipoUniformes", "TipoUniforme", "TipoUniforme")
            CargarCombos(cmbTalleUniforme, "Talles", "Talle", "Talle")

            ' Configurar ComboBoxes con valores fijos
            ConfigurarComboBoxesFijos()

            ' Cargar Categorias para filtrado
            CargarCombos(CmbCate, "Categorias", "Descripcion", "Descripcion")
            Dim dt As DataTable = CType(CmbCate.DataSource, DataTable)
            Dim nuevaFila As DataRow = dt.NewRow()
            nuevaFila("Descripcion") = "(Todas)"
            dt.Rows.InsertAt(nuevaFila, 0)
            CmbCate.DataSource = dt
            CmbCate.SelectedIndex = 0

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
        cmbHorasDiarias.Items.AddRange({"4", "6", "8", "Tiempo Completo", "Dedicaci�n Full Time"})

        ' Parentesco
        cmbParentesco.Items.Clear()
        cmbParentesco.Items.AddRange({"Padre", "Madre", "C�nyuge", "Hijo/a", "Nieto/a", "Otros"})

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
        ' Configurar botones para modo edici�n usando SetControlesEnabled
        SetControlesEnabled(False, btnAgregar, btnModificar, btnBorrar)
        SetControlesEnabled(True, btnAceptar, btnCancelar)
    End Sub
    Private Function ObtenerCUIL(dni As String, genero As String) As String
        Dim prefijo As String
        Dim cuilSinVerificador As String
        Dim suma As Integer
        Dim verificador As Integer
        Dim factores() As Integer = {5, 4, 3, 2, 7, 6, 5, 4, 3, 2}

        ' Determinar el prefijo seg�n el g�nero
        Select Case genero
            Case "M" ' Masculino
                prefijo = "20"
            Case "F" ' Femenino
                prefijo = "27"
            Case "E" ' Extranjero
                prefijo = "23"
            Case Else
                Return "G�nero no v�lido"
        End Select

        ' Crear la base del CUIL sin el d�gito verificador
        cuilSinVerificador = prefijo & dni

        ' Calcular la suma del producto de cada d�gito por su factor correspondiente
        suma = 0
        For i As Integer = 0 To 9
            suma += Convert.ToInt32(cuilSinVerificador(i).ToString()) * factores(i)
        Next

        ' Calcular el d�gito verificador
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

    Private Sub btnSaldoVacaciones_Click(sender As Object, e As EventArgs) Handles btnSaldoVacaciones.Click
        If filaActual Is Nothing Then
            lblSaldoVacaciones.Text = "Saldo: "
            lblSaldoVacaciones.ForeColor = System.Drawing.Color.Blue
            Return
        End If

        Dim legajo = filaActual.Cells("Legajo").Value
        Dim saldos = Vacaciones.ObtenerSaldosVacaciones(legajo)
        Dim saldoTexto As String = "Saldo: " & vbCrLf & vbCrLf
        For Each fila As DataRow In saldos.Rows
            saldoTexto &= $"{fila("Motivo")}: {fila("diasRestantes")}, " & vbCrLf
        Next
        MessageBox.Show(saldoTexto, "Informaci�n", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub ActualizarSaldoVacaciones()
        If filaActual Is Nothing Then
            lblSaldoVacaciones.Text = "Saldo: "
            lblSaldoVacaciones.ForeColor = System.Drawing.Color.Blue
            Return
        End If

        Dim legajo = filaActual.Cells("Legajo").Value
        Dim saldos = Vacaciones.ObtenerSaldosVacaciones(legajo)
        Dim saldoTotal = 0
        For Each fila As DataRow In saldos.Rows
            saldoTotal += Convert.ToInt32(fila("diasRestantes"))
        Next

        lblSaldoVacaciones.Text = "Saldo: " & saldoTotal.ToString() & " d�as"
    End Sub
    Private Sub ModificaAgente()
        'Busca en la tabla Autoriza, si el usuarioactual tiene el valor true en ModificaAgente entonces habilita el boton de modificar, agregar, eliminar
        Try
            Dim sql = "SELECT AltaAgente FROM Autoriza WHERE Usuario = @Usuario"
            Dim tabla = DSM.ExecuteQuery(DSM.Stock, sql, CmdParams("@Usuario", General.UsuarioActual))
            If tabla.Rows.Count > 0 AndAlso Convert.ToBoolean(tabla.Rows(0)("AltaAgente")) Then
                btnModificar.Enabled = True
                btnAgregar.Enabled = True
                btnBorrar.Enabled = True
            Else
                btnModificar.Enabled = False
                btnAgregar.Enabled = False
                btnBorrar.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show("Error al verificar permisos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ObtieneGoogle()

        Try

            Dim credentialPath As String =
        "F:\Personal.net\infotalles.json"

            Dim spreadsheetId As String = "1sGPEGyS4IBOlIQv_NXF75a-h1ZnWslV2DozU57AxAxg"
            '"1mOXQTwr399hU_dwD_XolMxP1fPzYJ5mh2Gjo5YsUqMI"

            Dim sheetName As String = "Respuestas de formulario 1"

            Dim range As String = sheetName & "!A:I"

            ' =========================
            ' AUTENTICACION
            ' =========================

            Dim credential = GoogleCredential.FromFile(credentialPath).CreateScoped(SheetsService.Scope.Spreadsheets)

            ' =========================
            ' SERVICIO
            ' =========================

            Dim service As New SheetsService(
        New BaseClientService.Initializer() With {
            .HttpClientInitializer = credential,
            .ApplicationName = "MiSistema"
        })

            ' =========================
            ' LECTURA GOOGLE SHEETS
            ' =========================

            Dim request =
        service.Spreadsheets.Values.Get(
            spreadsheetId,
            range)

            Dim response = request.Execute()

            Dim values = response.Values

            Dim filaGoogle As Integer = 2

            ' =========================
            ' CARGAR TALLES EN MEMORIA
            ' =========================

            Dim dicTalles As Dictionary(Of String, Integer)
            Dim dicTiposUniforme As Dictionary(Of String, Integer)

            dicTalles = CargarTalles()
            dicTiposUniforme = CargarTiposUniforme()

            If values IsNot Nothing AndAlso values.Count > 0 Then

                For Each fila As IList(Of Object) In values.Skip(1)

                    Dim legajo As String = ""
                    If fila.Count > 1 Then
                        legajo = fila(1).ToString()
                    End If

                    'If legajo = txtLegajo.Text.Trim Then
                    Dim nombre As String = ""
                    Dim sucursal As String = ""

                    Dim talleCalzado As String = ""
                    Dim talleRemera As String = ""
                    Dim talleCampera As String = ""
                    Dim talleCamperon As String = ""
                    Dim tallePantalon As String = ""
                    Dim talleFaja As String = ""

                    Dim importado As String = ""

                    ' =========================
                    ' LEER COLUMNAS GOOGLE
                    ' =========================



                    If fila.Count > 2 Then
                        nombre = fila(2).ToString()
                    End If

                    If fila.Count > 3 Then
                        sucursal = fila(3).ToString()
                    End If

                    If fila.Count > 4 Then
                        talleCalzado = fila(4).ToString()
                    End If

                    If fila.Count > 5 Then
                        talleRemera = fila(5).ToString()
                    End If

                    If fila.Count > 6 Then
                        talleCampera = fila(6).ToString()
                    End If

                    If fila.Count > 7 Then
                        tallePantalon = fila(7).ToString()
                    End If

                    If fila.Count > 8 Then
                        talleFaja = fila(8).ToString()
                    End If

                    'If fila.Count > 9 Then
                    '    importado = fila(9).ToString()
                    'End If

                    ' =========================
                    ' SOLO NO IMPORTADOS
                    ' =========================

                    'If legajo = Convert.ToInt32(txtLegajo.Text.Trim) Then 'AndAlso (importado = "" OrElse importado = "0") Then
                    Dim legajoInt As Integer
                    If Not Integer.TryParse(legajo.Trim(), legajoInt) Then
                        filaGoogle += 1
                        Continue For
                    End If

                    Dim fechaActualizacion As DateTime = DateTime.Now

                    ' =========================
                    ' CONVERTIR TALLES → IDs
                    ' =========================

                    Dim idTalleCalzado As Integer =
            ObtenerIdTalle(dicTalles, talleCalzado)

                    Dim idTalleRemera As Integer =
            ObtenerIdTalle(dicTalles, talleRemera)

                    Dim idTalleCampera As Integer =
            ObtenerIdTalle(dicTalles, talleCampera)

                    Dim idTallePantalon As Integer =
            ObtenerIdTalle(dicTalles, tallePantalon)

                    Dim idTalleFaja As Integer =
            ObtenerIdTalle(dicTalles, talleFaja)


                    ' ====================================================
                    ' ACA GUARDAS EN AgenteTalles
                    ' ====================================================
                    Dim sqlUpsert As String = "
                            IF EXISTS (SELECT 1 FROM AgenteTalles WHERE Legajo = @Legajo AND idTipoUniforme = @idTipoUniforme)
                                UPDATE AgenteTalles
                                SET
                                    idTalle = @idTalle,
                                    FechaActualizacion = @FechaActualizacion
                                WHERE Legajo = @Legajo AND idTipoUniforme = @idTipoUniforme
                            ELSE
                                INSERT INTO AgenteTalles (Legajo, idTipoUniforme, idTalle, FechaActualizacion)
                                VALUES (@Legajo, @idTipoUniforme, @idTalle, @FechaActualizacion)
                            "

                    Dim operaciones As New List(Of (tipoKey As String, idTalle As Integer)) From {
                    ("ZAPATO", idTalleCalzado),
                    ("REMERA M/C", idTalleRemera),
                    ("REMERA M/L", idTalleRemera),
                    ("CAMPERA", idTalleCampera),
                    ("CHALECO", idTalleCampera),
                    ("PANTALÓN", idTallePantalon),
                    ("FAJA", idTalleFaja)
                }

                    Dim ok As Boolean = True

                    For Each op In operaciones
                        If op.idTalle <= 0 Then
                            Continue For
                        End If

                        Dim idTipoUniforme As Integer = ObtenerIdTipoUniforme(dicTiposUniforme, op.tipoKey)
                        If idTipoUniforme <= 0 Then
                            ok = False
                            Exit For
                        End If

                        Dim parametros = CmdParams(
                        "@Legajo", legajoInt,
                        "@idTipoUniforme", idTipoUniforme,
                        "@idTalle", op.idTalle,
                        "@FechaActualizacion", fechaActualizacion
                    )

                        DSM.Execute(DSM.Personal, sqlUpsert, parametros, True)
                    Next

                    If Not ok Then
                        filaGoogle += 1
                        Continue For
                    End If

                    ' =========================
                    ' MARCAR GOOGLE = 1
                    ' =========================

                    '    Dim updateRange As String =
                    'sheetName & "!J" & filaGoogle

                    '    Dim valueRange As New Google.Apis.Sheets.v4.Data.ValueRange()

                    '    valueRange.Values =
                    'New List(Of IList(Of Object)) From {
                    '    New List(Of Object) From {"1"}
                    '}

                    '    Dim updateRequest =
                    'service.Spreadsheets.Values.Update(
                    '    valueRange,
                    '    spreadsheetId,
                    '    updateRange)

                    '    updateRequest.ValueInputOption =
                    'SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW

                    '    updateRequest.Execute()

                    'End If
                    'End If

                    filaGoogle += 1

                Next

            Else

                MsgBox("No hay datos.")

            End If

            MsgBox("Proceso finalizado. Se procesaron " & (filaGoogle - 2).ToString() & " filas.")

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        'AplicarSeleccionActual()
        'CargaUniformes(Convert.ToInt32(txtLegajo.Text.Trim))
        'CargaTiposUniformes(Convert.ToInt32(txtLegajo.Text.Trim))
    End Sub
    Private Function CargarTalles() As Dictionary(Of String, Integer)
        Dim dic As New Dictionary(Of String, Integer)

        Dim sql As String = "SELECT idTalle, Talle FROM Talles"
        Dim tablaTalles As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)

        If tablaTalles Is Nothing OrElse tablaTalles.Rows.Count = 0 Then
            Return dic
        End If

        For Each fila As DataRow In tablaTalles.Rows
            If fila Is Nothing OrElse fila.IsNull("idTalle") OrElse fila.IsNull("Talle") Then Continue For

            Dim id As Integer = Convert.ToInt32(fila("idTalle"))
            Dim talle As String = Convert.ToString(fila("Talle")).Trim().ToUpperInvariant()

            If talle = "" Then Continue For
            If Not dic.ContainsKey(talle) Then
                dic.Add(talle, id)
            End If
        Next

        Return dic

    End Function
    Private Function ObtenerIdTalle(
    dic As Dictionary(Of String, Integer),
    talle As String) As Integer

        If talle Is Nothing Then Return 0

        talle = talle.Trim.ToUpper

        If talle = "" Then Return 0

        If dic.ContainsKey(talle) Then

            Return dic(talle)

        End If

        Return 0

    End Function

    Private Function CargarTiposUniforme() As Dictionary(Of String, Integer)
        Dim dic As New Dictionary(Of String, Integer)

        Dim sql As String = "SELECT idTipoUniforme, TipoUniforme FROM TipoUniformes"
        Dim tablaTipos As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)

        If tablaTipos Is Nothing OrElse tablaTipos.Rows.Count = 0 Then
            Return dic
        End If

        For Each fila As DataRow In tablaTipos.Rows
            If fila Is Nothing OrElse fila.IsNull("idTipoUniforme") OrElse fila.IsNull("TipoUniforme") Then Continue For

            Dim id As Integer = Convert.ToInt32(fila("idTipoUniforme"))
            Dim tipo As String = Convert.ToString(fila("TipoUniforme")).Trim().ToUpperInvariant()

            If tipo = "" Then Continue For
            If Not dic.ContainsKey(tipo) Then
                dic.Add(tipo, id)
            End If
        Next

        Return dic
    End Function

    Private Function ObtenerIdTipoUniforme(dic As Dictionary(Of String, Integer), tipoUniforme As String) As Integer
        If tipoUniforme Is Nothing Then Return 0

        Dim key As String = tipoUniforme.Trim().ToUpperInvariant()
        If key = "" Then Return 0

        If dic.ContainsKey(key) Then
            Return dic(key)
        End If

        Return 0
    End Function
    Private Sub dgvTallesAgentes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTallesAgentes.CellClick

        If e.RowIndex < 0 Then Return

        Dim row = dgvTallesAgentes.Rows(e.RowIndex)

        Dim tipoUniforme = Convert.ToString(row.Cells("TipoUniforme").Value)
        cmbTipoUniforme.Text = tipoUniforme

        Dim idTalleObj = row.Cells("Talle").Value
        If idTalleObj Is Nothing OrElse idTalleObj Is DBNull.Value Then
            cmbTalleUniforme.SelectedIndex = -1
            Return
        End If

        Dim idTalle = Convert.ToInt32(idTalleObj)

        If tablaTalles IsNot Nothing Then
            Dim rows = tablaTalles.Select("idTalle = " & idTalle.ToString)
            If rows IsNot Nothing AndAlso rows.Length > 0 Then
                Dim talleTexto = Convert.ToString(rows(0)("Talle"))
                cmbTalleUniforme.Text = talleTexto
            Else
                cmbTalleUniforme.SelectedIndex = -1
            End If
        Else
            cmbTalleUniforme.SelectedIndex = -1
        End If
    End Sub

    Private Sub btnActualizaTaller_Click(sender As Object, e As EventArgs) Handles btnActualizaTaller.Click
        ObtieneGoogle()
    End Sub

    Private Sub btmImprimirTalles_Click(sender As Object, e As EventArgs) Handles btmImprimirTalles.Click
        Try
            Dim sql1 = "DELETE FROM WAgentesTalles"
            DSM.Execute(DSM.Personal, sql1, Nothing, True)

            ' Consulta para insertar datos en la tabla Cumple2
            Dim consultaInsert = "-- Si querés regenerar la tabla cada vez
                DELETE FROM WAgentesTalles;

                INSERT INTO WAgentesTalles
                (
                    Legajo,
                    Nombre,
                    Instituto,
                    Remera,
                    Pantalon,
                    Campera,
                    Camperon,
                    Faja,
                    Chaleco,
                    Zapato
                )
                SELECT
                    a.Legajo,
                    a.Nombre,
                    a.Instituto,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'REMERA M/C'
                            THEN t.Talle
                        END) AS Remera,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'PANTALÓN'
                            THEN t.Talle
                        END) AS Pantalon,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'CAMPERA'
                            THEN t.Talle
                        END) AS Campera,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'CAMPERON'
                            THEN t.Talle
                        END) AS Camperon,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'FAJA'
                            THEN t.Talle
                        END) AS Faja,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'CHALECO'
                            THEN t.Talle
                        END) AS Chaleco,

                    MAX(CASE
                            WHEN tu.TipoUniforme = 'ZAPATO'
                            THEN t.Talle
                        END) AS Zapato

                FROM Agentes a
                LEFT JOIN AgenteTalles at
                    ON a.Legajo = at.Legajo
                LEFT JOIN TipoUniformes tu
                    ON at.idTipoUniforme = tu.idTipoUniforme
                LEFT JOIN Talles t
                    ON at.idTalle = t.idTalle

                WHERE a.Instituto IN
                (
                    'Halpern',
                    'Autoshop',
                    'Neuquen',
                    'Buenos Aires',
                    'Belgrano',
                    'Garay',
                    'Reconstruccion',
                    'Alcorta',
                    'Zona Franca'
                )
                AND (a.Baja IS NULL OR a.Baja = '')

                GROUP BY
                    a.Legajo,
                    a.Nombre,
                    a.Instituto

                ORDER BY
                    a.Instituto, a.Nombre;"

            ' Ejecutar la consulta de inserción
            DSM.Execute(DSM.Personal, consultaInsert)

            ' Imprimir el reporte
            Process.Start(ReportesPath, "Personal tallespersonal")

        Catch ex As Exception
            MessageBox.Show($"Error al obtener la lista de talles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBonos_Click(sender As Object, e As EventArgs) Handles btnBonos.Click
        ' Crear y mostrar el formulario de documentaci�n
        Dim frmRecibo As New frmEnvioRecibos
        frmRecibo.MdiParent = MdiParent
        frmRecibo.Show()
    End Sub

    Private Sub btnEnviarBono_Click(sender As Object, e As EventArgs) Handles btnEnviarBono.Click
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.Expect100Continue = True

        Dim legajoTexto = txtLegajo.Text.Trim()
        If String.IsNullOrWhiteSpace(legajoTexto) Then
            MessageBox.Show("Seleccione un agente para enviar los bonos.", "Agente no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim correoDestino = txtCorreoE.Text.Trim()
        If String.IsNullOrWhiteSpace(correoDestino) Then
            MessageBox.Show("El agente seleccionado no tiene correo electrónico informado.", "Correo no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim dtCorreoConfig = ObtenerConfiguracionCorreoBonos()
        If dtCorreoConfig Is Nothing OrElse dtCorreoConfig.Rows.Count = 0 Then
            MessageBox.Show("No se encontraron configuraciones de correo para el envío.", "Configuración faltante", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim bonos = ObtenerBonosParaEnviar()
        bonos = FiltrarBonosSegunModo(bonos)
        If bonos.Rows.Count = 0 Then
            Dim mensajeSinDatos = If(rdbSeleccion.Checked,
                                     "No hay bonos seleccionados para enviar.",
                                     "El agente seleccionado no tiene bonos para enviar.")
            MessageBox.Show(mensajeSinDatos, "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim filaConfig = dtCorreoConfig.Rows(0)
        Dim servidorSMTP As String = filaConfig("Servidor_SMTP").ToString().Trim()
        Dim puertoSMTP As Integer = 587
        Dim usuario As String = filaConfig("Envio_Mail").ToString().Trim()
        Dim contraseña As String = filaConfig("password_mail").ToString()
        Dim asuntoBase As String = filaConfig("Asunto").ToString().Trim()
        Dim mensajeBase As String = filaConfig("Mensaje").ToString().Trim()

        If String.IsNullOrWhiteSpace(servidorSMTP) OrElse String.IsNullOrWhiteSpace(usuario) Then
            MessageBox.Show("La configuración SMTP está incompleta.", "Configuración inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim enviados As Integer = 0
        Dim sinArchivo As Integer = 0
        Dim conError As Integer = 0

        Try
            Cursor.Current = Cursors.WaitCursor
            btnEnviarBono.Enabled = False

            Using smtpClient As New SmtpClient(servidorSMTP, puertoSMTP) With {
                .DeliveryMethod = SmtpDeliveryMethod.Network,
                .UseDefaultCredentials = False,
                .EnableSsl = True,
                .Credentials = New NetworkCredential(usuario, contraseña),
                .Timeout = 100000
            }
                For Each filaBono As DataRow In bonos.Rows
                    Dim idDetalle = Convert.ToInt32(filaBono("IdDetalle"))
                    Dim rutaPdf = filaBono("RutaPdf").ToString().Trim()
                    Dim periodoTexto = ObtenerPeriodoProcesadoBono(filaBono("PeriodoProcesado"))

                    If String.IsNullOrWhiteSpace(rutaPdf) OrElse Not File.Exists(rutaPdf) Then
                        ActualizarResultadoEnvioBono(idDetalle, "ARCHIVO_NO_ENCONTRADO", "No se encontró el archivo PDF a adjuntar.")
                        sinArchivo += 1
                        Continue For
                    End If

                    Dim asunto = ConstruirTextoCorreoBono(asuntoBase, periodoTexto)
                    Dim mensaje = ConstruirTextoCorreoBono(mensajeBase, periodoTexto)

                    Try
                        Using mail As New MailMessage()
                            mail.From = New MailAddress(usuario)
                            mail.Subject = asunto
                            mail.Body = mensaje
                            mail.To.Add(correoDestino)
                            mail.Attachments.Add(New Attachment(rutaPdf))

                            smtpClient.Send(mail)
                        End Using

                        ActualizarResultadoEnvioBono(idDetalle, "ENVIADO", $"Enviado correctamente a {correoDestino}.")
                        enviados += 1
                    Catch exEnvio As Exception
                        Dim detalleError = If(exEnvio.InnerException?.Message, exEnvio.Message)
                        ActualizarResultadoEnvioBono(idDetalle, "ERROR", LimitarTextoBono(detalleError, 500))
                        conError += 1
                    End Try
                Next
            End Using

            CargaBonos(Convert.ToInt32(legajoTexto))

            MessageBox.Show(
                $"Proceso de envío finalizado.{Environment.NewLine}" &
                $"Enviados: {enviados}{Environment.NewLine}" &
                $"Sin archivo: {sinArchivo}{Environment.NewLine}" &
                $"Con error: {conError}",
                "Envío finalizado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

        Catch ex As Exception
            Dim detail = If(ex.InnerException?.Message, ex.Message)
            MessageBox.Show("Error al enviar correos: " & detail, "SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnEnviarBono.Enabled = True
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub dgvBonos_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvBonos.CurrentCellDirtyStateChanged
        If dgvBonos.IsCurrentCellDirty AndAlso dgvBonos.CurrentCell IsNot Nothing AndAlso
            dgvBonos.CurrentCell.OwningColumn.Name = NombreColumnaSeleccionBonos Then
            dgvBonos.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub rdbBonosModo_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTodos.CheckedChanged, rdbSeleccion.CheckedChanged
        If dgvBonos.Columns.Contains(NombreColumnaSeleccionBonos) Then
            dgvBonos.Columns(NombreColumnaSeleccionBonos).Visible = rdbSeleccion.Checked
        End If
    End Sub

    Private Sub dgvBonos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBonos.CellDoubleClick
        If e.RowIndex < 0 Then
            Return
        End If

        Dim fila = dgvBonos.Rows(e.RowIndex)
        Dim valorRuta = fila.Cells("RutaPdf").Value
        Dim rutaArchivo = If(valorRuta IsNot Nothing, valorRuta.ToString(), String.Empty)

        If String.IsNullOrWhiteSpace(rutaArchivo) OrElse Not File.Exists(rutaArchivo) Then
            MessageBox.Show("No se encontró el archivo PDF seleccionado.", "Archivo no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Process.Start(New ProcessStartInfo(rutaArchivo) With {
                .UseShellExecute = True
            })
        Catch ex As Exception
            MessageBox.Show($"No se pudo abrir el archivo seleccionado.{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AsegurarColumnaSeleccionBonos()
        If dgvBonos.Columns.Contains(NombreColumnaSeleccionBonos) Then
            Return
        End If

        Dim columnaSeleccion As New DataGridViewCheckBoxColumn() With {
            .Name = NombreColumnaSeleccionBonos,
            .HeaderText = "",
            .Width = 30,
            .ReadOnly = False,
            .FalseValue = False,
            .TrueValue = True
        }

        dgvBonos.Columns.Insert(0, columnaSeleccion)
    End Sub

    Private Function ObtenerBonosParaEnviar() As DataTable
        Dim tablaBonosActual = TryCast(dgvBonos.DataSource, DataTable)
        If tablaBonosActual Is Nothing Then
            Return New DataTable()
        End If

        Return tablaBonosActual.Copy()
    End Function

    Private Function FiltrarBonosSegunModo(bonos As DataTable) As DataTable
        If Not rdbSeleccion.Checked Then
            Return bonos
        End If

        Dim idsSeleccionados = ObtenerIdsBonosSeleccionados()
        If idsSeleccionados.Count = 0 Then
            Return bonos.Clone()
        End If

        Dim bonosSeleccionados = bonos.Clone()
        For Each fila As DataRow In bonos.Rows
            Dim idDetalle = Convert.ToInt32(fila("IdDetalle"))
            If idsSeleccionados.Contains(idDetalle) Then
                bonosSeleccionados.ImportRow(fila)
            End If
        Next

        Return bonosSeleccionados
    End Function

    Private Function ObtenerIdsBonosSeleccionados() As HashSet(Of Integer)
        Dim ids As New HashSet(Of Integer)()

        For Each fila As DataGridViewRow In dgvBonos.Rows
            If fila.IsNewRow Then
                Continue For
            End If

            Dim valorSeleccion = fila.Cells(NombreColumnaSeleccionBonos).Value
            Dim seleccionado = False
            If valorSeleccion IsNot Nothing AndAlso valorSeleccion IsNot DBNull.Value Then
                Boolean.TryParse(valorSeleccion.ToString(), seleccionado)
            End If

            If Not seleccionado OrElse fila.Cells("IdDetalle")?.Value Is Nothing Then
                Continue For
            End If

            ids.Add(Convert.ToInt32(fila.Cells("IdDetalle").Value))
        Next

        Return ids
    End Function

    Private Function ObtenerConfiguracionCorreoBonos() As DataTable
        Dim sql = "SELECT TOP 1 Servidor_SMTP, Envio_Mail, password_mail, Asunto, Mensaje " &
                  "FROM EnvioCorreos WHERE NroEnvio = @NroEnvio"

        Return DSM.ExecuteQuery(DSM.Stock, sql, CmdParams("@NroEnvio", 5))
    End Function

    Private Function ObtenerPeriodoProcesadoBono(valorPeriodo As Object) As String
        If valorPeriodo Is Nothing OrElse valorPeriodo Is DBNull.Value Then
            Return "período no informado"
        End If

        Dim fechaPeriodo As DateTime
        If TypeOf valorPeriodo Is DateTime Then
            fechaPeriodo = DirectCast(valorPeriodo, DateTime)
        ElseIf Not DateTime.TryParse(valorPeriodo.ToString(), fechaPeriodo) Then
            Return valorPeriodo.ToString()
        End If

        Return fechaPeriodo.ToString("MMMM yyyy", New CultureInfo("es-AR"))
    End Function

    Private Function ConstruirTextoCorreoBono(textoBase As String, periodoTexto As String) As String
        Dim texto = If(textoBase, String.Empty).Trim()
        If String.IsNullOrWhiteSpace(texto) Then
            Return periodoTexto
        End If

        If texto.EndsWith(" ") Then
            Return texto & periodoTexto
        End If

        Return texto & " " & periodoTexto
    End Function

    Private Sub ActualizarResultadoEnvioBono(idDetalle As Integer, estadoEnvio As String, mensajeEnvio As String)
        Dim sql = "UPDATE RecibosProcesosDetalles SET " &
                  "FechaEnvio = @FechaEnvio, " &
                  "EstadoEnvio = @EstadoEnvio, " &
                  "MensajeEnvio = @MensajeEnvio " &
                  "WHERE IdDetalle = @IdDetalle"

        Dim parametros = CmdParams(
            "@FechaEnvio", DateTime.Now,
            "@EstadoEnvio", estadoEnvio,
            "@MensajeEnvio", LimitarTextoBono(mensajeEnvio, 500),
            "@IdDetalle", idDetalle)

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Function LimitarTextoBono(texto As String, longitudMaxima As Integer) As String
        If String.IsNullOrEmpty(texto) Then
            Return String.Empty
        End If

        If texto.Length <= longitudMaxima Then
            Return texto
        End If

        Return texto.Substring(0, longitudMaxima)
    End Function
End Class
