<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAgentes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        tabDatos = New TabControl()
        tabDatosAgente = New TabPage()
        tlpDatosAgente = New TableLayoutPanel()
        Panel3Col = New Panel()
        chkSindicato = New CheckBox()
        btnSaldoVacaciones = New Button()
        lblSaldoVacaciones = New Label()
        lblAntiguedadCorregida = New Label()
        chkBaja = New CheckBox()
        txtAntiguedad = New TextBox()
        Label10 = New Label()
        dtpBaja = New DateTimePicker()
        dtpIngreso = New DateTimePicker()
        Label12 = New Label()
        CmbMotivo = New ComboBox()
        txtFechaJubilacion = New TextBox()
        Label25 = New Label()
        txtLicAnual = New TextBox()
        Label30 = New Label()
        txtComentario = New TextBox()
        Label16 = New Label()
        txtUltimaActualizacion = New TextBox()
        Label23 = New Label()
        Label24 = New Label()
        Label18 = New Label()
        Panel2Col = New Panel()
        cmbEstadoParental = New ComboBox()
        cmbHorasDiarias = New ComboBox()
        Label13 = New Label()
        txtInterno = New TextBox()
        cmbJefe = New ComboBox()
        Label21 = New Label()
        Label15 = New Label()
        cmbCaracter = New ComboBox()
        cmbEscalafon = New ComboBox()
        Label14 = New Label()
        Label17 = New Label()
        cmbInstituto = New ComboBox()
        Label29 = New Label()
        txtCUIL = New TextBox()
        Label3 = New Label()
        Label26 = New Label()
        cmbCategoria = New ComboBox()
        Label19 = New Label()
        Panel1Col = New Panel()
        txtLegajoEventual = New TextBox()
        lblLegajoEventual = New Label()
        Label8 = New Label()
        txtNro = New TextBox()
        Label4 = New Label()
        txtCorreoE = New TextBox()
        txtCelular = New TextBox()
        txtTitulo = New TextBox()
        Label22 = New Label()
        Label7 = New Label()
        Label27 = New Label()
        txtCalle = New TextBox()
        dtpNacimiento = New DateTimePicker()
        txtLocalidad = New TextBox()
        cmbSexo = New ComboBox()
        Label9 = New Label()
        Label6 = New Label()
        Label11 = New Label()
        txtUrgencias = New TextBox()
        txtLegajo = New TextBox()
        lblLegajo = New Label()
        Label5 = New Label()
        txtNombre = New TextBox()
        txtTelefono = New TextBox()
        Label2 = New Label()
        Label20 = New Label()
        txtNroDto = New TextBox()
        cmbTipoDto = New ComboBox()
        Label34 = New Label()
        Panel1 = New Panel()
        chkNomarca = New CheckBox()
        pctFoto = New PictureBox()
        Panel2 = New Panel()
        btnAgregar = New Button()
        btnModificar = New Button()
        btnBorrar = New Button()
        Panel3 = New Panel()
        btnBonos = New Button()
        btnAceptar = New Button()
        btnCancelar = New Button()
        Panel4 = New Panel()
        btnDocumentacion = New Button()
        tabGrupoFamiliar = New TabPage()
        GroupBox2 = New GroupBox()
        cmbNivelEstudio = New ComboBox()
        cmbParentesco = New ComboBox()
        DgvGrupoFamiliar = New DataGridView()
        btnEliminarFamiliar = New Button()
        btnAgregarFamiliar = New Button()
        Label40 = New Label()
        txtOcupacionFamiliar = New TextBox()
        Label39 = New Label()
        txtEdadFamiliar = New TextBox()
        Label38 = New Label()
        dtpNacimientoFamiliar = New DateTimePicker()
        Label37 = New Label()
        Label36 = New Label()
        txtNombreFamiliar = New TextBox()
        Label35 = New Label()
        tabComentarios = New TabPage()
        GroupBox3 = New GroupBox()
        DgvComentarios = New DataGridView()
        btnEliminarComentario = New Button()
        btnAgregarComentario = New Button()
        cmbMotivoComentario = New ComboBox()
        Label43 = New Label()
        txtComentaComentario = New TextBox()
        Label42 = New Label()
        dtpFechaComentario = New DateTimePicker()
        Label41 = New Label()
        TabPage1 = New TabPage()
        GroupBoxEquipamiento = New GroupBox()
        txtNroTelEquipoamiento = New TextBox()
        Label28 = New Label()
        DgvEquipamiento = New DataGridView()
        btnEliminarEquipamiento = New Button()
        btnAgregarEquipamiento = New Button()
        cmbTipoEquipamiento = New ComboBox()
        Label44 = New Label()
        txtMarcaEquipamiento = New TextBox()
        Label45 = New Label()
        txtModeloEquipamiento = New TextBox()
        Label46 = New Label()
        txtNroSerieEquipamiento = New TextBox()
        Label47 = New Label()
        txtIMEIEquipamiento = New TextBox()
        Label48 = New Label()
        dtpFechaEquipamiento = New DateTimePicker()
        Label49 = New Label()
        txtObservacionesEquipamiento = New TextBox()
        Label50 = New Label()
        TabPage2 = New TabPage()
        GroupBox1 = New GroupBox()
        btmImprimirTalles = New Button()
        btnActualizaTaller = New Button()
        Label32 = New Label()
        dgvTallesAgentes = New DataGridView()
        cmbTalleUniforme = New ComboBox()
        dgvUniformes = New DataGridView()
        btnEliminarUniforme = New Button()
        btnAgregarUniforme = New Button()
        cmbTipoUniforme = New ComboBox()
        Label31 = New Label()
        Label33 = New Label()
        dtpFechaUniforme = New DateTimePicker()
        Label51 = New Label()
        txtObservacionesUniforme = New TextBox()
        Label52 = New Label()
        tabBonos = New TabPage()
        GroupBox4 = New GroupBox()
        rdbSeleccion = New RadioButton()
        rdbTodos = New RadioButton()
        dgvBonos = New DataGridView()
        btnEnviarBono = New Button()
        DataGridView1 = New DataGridView()
        DataGridView2 = New DataGridView()
        Button3 = New Button()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        radActivos = New RadioButton()
        radTodos = New RadioButton()
        lblTotalAgentes = New Label()
        txtBuscar = New TextBox()
        lblBuscar = New Label()
        dgvListado = New DataGridView()
        btnSalir = New Button()
        lblSucursal = New Label()
        cmbSucursal = New ComboBox()
        radEventuales = New RadioButton()
        CmbCate = New ComboBox()
        Label1 = New Label()
        radBaja = New RadioButton()
        tabDatos.SuspendLayout()
        tabDatosAgente.SuspendLayout()
        tlpDatosAgente.SuspendLayout()
        Panel3Col.SuspendLayout()
        Panel2Col.SuspendLayout()
        Panel1Col.SuspendLayout()
        Panel1.SuspendLayout()
        CType(pctFoto, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        tabGrupoFamiliar.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DgvGrupoFamiliar, ComponentModel.ISupportInitialize).BeginInit()
        tabComentarios.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(DgvComentarios, ComponentModel.ISupportInitialize).BeginInit()
        TabPage1.SuspendLayout()
        GroupBoxEquipamiento.SuspendLayout()
        CType(DgvEquipamiento, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvTallesAgentes, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvUniformes, ComponentModel.ISupportInitialize).BeginInit()
        tabBonos.SuspendLayout()
        GroupBox4.SuspendLayout()
        CType(dgvBonos, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tabDatos
        ' 
        tabDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tabDatos.Controls.Add(tabDatosAgente)
        tabDatos.Controls.Add(tabGrupoFamiliar)
        tabDatos.Controls.Add(tabComentarios)
        tabDatos.Controls.Add(TabPage1)
        tabDatos.Controls.Add(TabPage2)
        tabDatos.Controls.Add(tabBonos)
        tabDatos.Location = New Point(10, 177)
        tabDatos.Margin = New Padding(4, 3, 4, 3)
        tabDatos.Name = "tabDatos"
        tabDatos.SelectedIndex = 0
        tabDatos.Size = New Size(1164, 386)
        tabDatos.TabIndex = 8
        ' 
        ' tabDatosAgente
        ' 
        tabDatosAgente.Controls.Add(tlpDatosAgente)
        tabDatosAgente.Location = New Point(4, 24)
        tabDatosAgente.Margin = New Padding(4, 3, 4, 3)
        tabDatosAgente.Name = "tabDatosAgente"
        tabDatosAgente.Padding = New Padding(4, 3, 4, 3)
        tabDatosAgente.Size = New Size(1156, 358)
        tabDatosAgente.TabIndex = 0
        tabDatosAgente.Text = "Datos del Agente"
        tabDatosAgente.UseVisualStyleBackColor = True
        ' 
        ' tlpDatosAgente
        ' 
        tlpDatosAgente.ColumnCount = 4
        tlpDatosAgente.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 38.50156F))
        tlpDatosAgente.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 24.3496361F))
        tlpDatosAgente.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 37.1488037F))
        tlpDatosAgente.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 185F))
        tlpDatosAgente.Controls.Add(Panel3Col, 2, 0)
        tlpDatosAgente.Controls.Add(Panel2Col, 1, 0)
        tlpDatosAgente.Controls.Add(Panel1Col, 0, 0)
        tlpDatosAgente.Controls.Add(Panel1, 3, 0)
        tlpDatosAgente.Controls.Add(Panel2, 0, 1)
        tlpDatosAgente.Controls.Add(Panel3, 2, 1)
        tlpDatosAgente.Controls.Add(Panel4, 3, 1)
        tlpDatosAgente.Dock = DockStyle.Fill
        tlpDatosAgente.Location = New Point(4, 3)
        tlpDatosAgente.Name = "tlpDatosAgente"
        tlpDatosAgente.RowCount = 2
        tlpDatosAgente.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpDatosAgente.RowStyles.Add(New RowStyle(SizeType.Absolute, 44F))
        tlpDatosAgente.Size = New Size(1148, 352)
        tlpDatosAgente.TabIndex = 0
        ' 
        ' Panel3Col
        ' 
        Panel3Col.Controls.Add(chkSindicato)
        Panel3Col.Controls.Add(btnSaldoVacaciones)
        Panel3Col.Controls.Add(lblSaldoVacaciones)
        Panel3Col.Controls.Add(lblAntiguedadCorregida)
        Panel3Col.Controls.Add(chkBaja)
        Panel3Col.Controls.Add(txtAntiguedad)
        Panel3Col.Controls.Add(Label10)
        Panel3Col.Controls.Add(dtpBaja)
        Panel3Col.Controls.Add(dtpIngreso)
        Panel3Col.Controls.Add(Label12)
        Panel3Col.Controls.Add(CmbMotivo)
        Panel3Col.Controls.Add(txtFechaJubilacion)
        Panel3Col.Controls.Add(Label25)
        Panel3Col.Controls.Add(txtLicAnual)
        Panel3Col.Controls.Add(Label30)
        Panel3Col.Controls.Add(txtComentario)
        Panel3Col.Controls.Add(Label16)
        Panel3Col.Controls.Add(txtUltimaActualizacion)
        Panel3Col.Controls.Add(Label23)
        Panel3Col.Controls.Add(Label24)
        Panel3Col.Controls.Add(Label18)
        Panel3Col.Dock = DockStyle.Fill
        Panel3Col.Location = New Point(607, 3)
        Panel3Col.Name = "Panel3Col"
        Panel3Col.Size = New Size(351, 302)
        Panel3Col.TabIndex = 2
        ' 
        ' chkSindicato
        ' 
        chkSindicato.AutoSize = True
        chkSindicato.Location = New Point(199, 11)
        chkSindicato.Name = "chkSindicato"
        chkSindicato.Size = New Size(119, 19)
        chkSindicato.TabIndex = 79
        chkSindicato.Text = "Afiliado Sindicato"
        chkSindicato.UseVisualStyleBackColor = True
        ' 
        ' btnSaldoVacaciones
        ' 
        btnSaldoVacaciones.Cursor = Cursors.Hand
        btnSaldoVacaciones.Location = New Point(275, 66)
        btnSaldoVacaciones.Name = "btnSaldoVacaciones"
        btnSaldoVacaciones.Size = New Size(32, 23)
        btnSaldoVacaciones.TabIndex = 78
        btnSaldoVacaciones.Text = "?"
        btnSaldoVacaciones.UseVisualStyleBackColor = True
        ' 
        ' lblSaldoVacaciones
        ' 
        lblSaldoVacaciones.AutoSize = True
        lblSaldoVacaciones.Location = New Point(190, 71)
        lblSaldoVacaciones.Margin = New Padding(4, 0, 4, 0)
        lblSaldoVacaciones.Name = "lblSaldoVacaciones"
        lblSaldoVacaciones.Size = New Size(71, 15)
        lblSaldoVacaciones.TabIndex = 77
        lblSaldoVacaciones.Text = "Saldo: _ dias"
        ' 
        ' lblAntiguedadCorregida
        ' 
        lblAntiguedadCorregida.AutoSize = True
        lblAntiguedadCorregida.Location = New Point(190, 42)
        lblAntiguedadCorregida.Margin = New Padding(4, 0, 4, 0)
        lblAntiguedadCorregida.Name = "lblAntiguedadCorregida"
        lblAntiguedadCorregida.Size = New Size(0, 15)
        lblAntiguedadCorregida.TabIndex = 76
        ' 
        ' chkBaja
        ' 
        chkBaja.AutoSize = True
        chkBaja.Location = New Point(98, 130)
        chkBaja.Name = "chkBaja"
        chkBaja.Size = New Size(15, 14)
        chkBaja.TabIndex = 75
        chkBaja.UseVisualStyleBackColor = True
        ' 
        ' txtAntiguedad
        ' 
        txtAntiguedad.Location = New Point(98, 37)
        txtAntiguedad.Margin = New Padding(4, 3, 4, 3)
        txtAntiguedad.Name = "txtAntiguedad"
        txtAntiguedad.Size = New Size(84, 23)
        txtAntiguedad.TabIndex = 74
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(9, 42)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(72, 15)
        Label10.TabIndex = 73
        Label10.Text = "Antigüedad:"
        ' 
        ' dtpBaja
        ' 
        dtpBaja.Format = DateTimePickerFormat.Short
        dtpBaja.Location = New Point(120, 125)
        dtpBaja.Margin = New Padding(4, 3, 4, 3)
        dtpBaja.Name = "dtpBaja"
        dtpBaja.Size = New Size(104, 23)
        dtpBaja.TabIndex = 32
        dtpBaja.Value = New Date(2025, 10, 14, 0, 0, 0, 0)
        ' 
        ' dtpIngreso
        ' 
        dtpIngreso.Format = DateTimePickerFormat.Short
        dtpIngreso.Location = New Point(98, 8)
        dtpIngreso.Margin = New Padding(4, 3, 4, 3)
        dtpIngreso.Name = "dtpIngreso"
        dtpIngreso.Size = New Size(90, 23)
        dtpIngreso.TabIndex = 29
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(9, 159)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(48, 15)
        Label12.TabIndex = 72
        Label12.Text = "Motivo:"
        ' 
        ' CmbMotivo
        ' 
        CmbMotivo.FormattingEnabled = True
        CmbMotivo.Location = New Point(98, 154)
        CmbMotivo.Margin = New Padding(4, 3, 4, 3)
        CmbMotivo.Name = "CmbMotivo"
        CmbMotivo.Size = New Size(188, 23)
        CmbMotivo.TabIndex = 33
        ' 
        ' txtFechaJubilacion
        ' 
        txtFechaJubilacion.Location = New Point(98, 94)
        txtFechaJubilacion.Margin = New Padding(4, 3, 4, 3)
        txtFechaJubilacion.Name = "txtFechaJubilacion"
        txtFechaJubilacion.Size = New Size(88, 23)
        txtFechaJubilacion.TabIndex = 31
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(9, 130)
        Label25.Margin = New Padding(4, 0, 4, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(32, 15)
        Label25.TabIndex = 57
        Label25.Text = "Baja:"
        ' 
        ' txtLicAnual
        ' 
        txtLicAnual.Enabled = False
        txtLicAnual.Location = New Point(98, 66)
        txtLicAnual.Margin = New Padding(4, 3, 4, 3)
        txtLicAnual.Name = "txtLicAnual"
        txtLicAnual.Size = New Size(59, 23)
        txtLicAnual.TabIndex = 30
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(9, 99)
        Label30.Margin = New Padding(4, 0, 4, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(70, 15)
        Label30.TabIndex = 65
        Label30.Text = "Fec Jubilac.:"
        ' 
        ' txtComentario
        ' 
        txtComentario.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtComentario.Location = New Point(9, 235)
        txtComentario.Margin = New Padding(4, 3, 4, 3)
        txtComentario.Multiline = True
        txtComentario.Name = "txtComentario"
        txtComentario.ScrollBars = ScrollBars.Both
        txtComentario.Size = New Size(334, 59)
        txtComentario.TabIndex = 36
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(9, 71)
        Label16.Margin = New Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(87, 15)
        Label16.TabIndex = 36
        Label16.Text = "Días Lic. Anual:"
        ' 
        ' txtUltimaActualizacion
        ' 
        txtUltimaActualizacion.Location = New Point(98, 182)
        txtUltimaActualizacion.Margin = New Padding(4, 3, 4, 3)
        txtUltimaActualizacion.Name = "txtUltimaActualizacion"
        txtUltimaActualizacion.Size = New Size(188, 23)
        txtUltimaActualizacion.TabIndex = 34
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(9, 187)
        Label23.Margin = New Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(76, 15)
        Label23.TabIndex = 51
        Label23.Text = "Últ. Actualiz.:"
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(9, 13)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(49, 15)
        Label24.TabIndex = 53
        Label24.Text = "Ingreso:"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(9, 217)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(73, 15)
        Label18.TabIndex = 42
        Label18.Text = "Comentario:"
        ' 
        ' Panel2Col
        ' 
        Panel2Col.Controls.Add(cmbEstadoParental)
        Panel2Col.Controls.Add(cmbHorasDiarias)
        Panel2Col.Controls.Add(Label13)
        Panel2Col.Controls.Add(txtInterno)
        Panel2Col.Controls.Add(cmbJefe)
        Panel2Col.Controls.Add(Label21)
        Panel2Col.Controls.Add(Label15)
        Panel2Col.Controls.Add(cmbCaracter)
        Panel2Col.Controls.Add(cmbEscalafon)
        Panel2Col.Controls.Add(Label14)
        Panel2Col.Controls.Add(Label17)
        Panel2Col.Controls.Add(cmbInstituto)
        Panel2Col.Controls.Add(Label29)
        Panel2Col.Controls.Add(txtCUIL)
        Panel2Col.Controls.Add(Label3)
        Panel2Col.Controls.Add(Label26)
        Panel2Col.Controls.Add(cmbCategoria)
        Panel2Col.Controls.Add(Label19)
        Panel2Col.Dock = DockStyle.Fill
        Panel2Col.Location = New Point(373, 3)
        Panel2Col.Name = "Panel2Col"
        Panel2Col.Size = New Size(228, 302)
        Panel2Col.TabIndex = 1
        ' 
        ' cmbEstadoParental
        ' 
        cmbEstadoParental.FormattingEnabled = True
        cmbEstadoParental.Location = New Point(86, 213)
        cmbEstadoParental.Margin = New Padding(4, 3, 4, 3)
        cmbEstadoParental.Name = "cmbEstadoParental"
        cmbEstadoParental.Size = New Size(134, 23)
        cmbEstadoParental.TabIndex = 27
        ' 
        ' cmbHorasDiarias
        ' 
        cmbHorasDiarias.FormattingEnabled = True
        cmbHorasDiarias.Location = New Point(86, 184)
        cmbHorasDiarias.Margin = New Padding(4, 3, 4, 3)
        cmbHorasDiarias.Name = "cmbHorasDiarias"
        cmbHorasDiarias.Size = New Size(134, 23)
        cmbHorasDiarias.TabIndex = 26
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(9, 188)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(62, 15)
        Label13.TabIndex = 30
        Label13.Text = "Hs Diarias:"
        ' 
        ' txtInterno
        ' 
        txtInterno.Location = New Point(86, 245)
        txtInterno.Margin = New Padding(4, 3, 4, 3)
        txtInterno.Name = "txtInterno"
        txtInterno.Size = New Size(136, 23)
        txtInterno.TabIndex = 28
        ' 
        ' cmbJefe
        ' 
        cmbJefe.FormattingEnabled = True
        cmbJefe.Location = New Point(86, 155)
        cmbJefe.Margin = New Padding(4, 3, 4, 3)
        cmbJefe.Name = "cmbJefe"
        cmbJefe.Size = New Size(134, 23)
        cmbJefe.TabIndex = 25
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(9, 249)
        Label21.Margin = New Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(48, 15)
        Label21.TabIndex = 47
        Label21.Text = "Interno:"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(9, 159)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(69, 15)
        Label15.TabIndex = 34
        Label15.Text = "A Cargo de:"
        ' 
        ' cmbCaracter
        ' 
        cmbCaracter.FormattingEnabled = True
        cmbCaracter.Location = New Point(86, 68)
        cmbCaracter.Margin = New Padding(4, 3, 4, 3)
        cmbCaracter.Name = "cmbCaracter"
        cmbCaracter.Size = New Size(134, 23)
        cmbCaracter.TabIndex = 22
        ' 
        ' cmbEscalafon
        ' 
        cmbEscalafon.FormattingEnabled = True
        cmbEscalafon.Location = New Point(86, 126)
        cmbEscalafon.Margin = New Padding(4, 3, 4, 3)
        cmbEscalafon.Name = "cmbEscalafon"
        cmbEscalafon.Size = New Size(133, 23)
        cmbEscalafon.TabIndex = 24
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(9, 130)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(34, 15)
        Label14.TabIndex = 32
        Label14.Text = "Area:"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(9, 72)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(54, 15)
        Label17.TabIndex = 44
        Label17.Text = "Carácter:"
        ' 
        ' cmbInstituto
        ' 
        cmbInstituto.FormattingEnabled = True
        cmbInstituto.Location = New Point(86, 97)
        cmbInstituto.Margin = New Padding(4, 3, 4, 3)
        cmbInstituto.Name = "cmbInstituto"
        cmbInstituto.Size = New Size(133, 23)
        cmbInstituto.TabIndex = 23
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(9, 217)
        Label29.Margin = New Padding(4, 0, 4, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(74, 15)
        Label29.TabIndex = 63
        Label29.Text = "Est. Parental:"
        ' 
        ' txtCUIL
        ' 
        txtCUIL.Location = New Point(86, 10)
        txtCUIL.Margin = New Padding(4, 3, 4, 3)
        txtCUIL.Name = "txtCUIL"
        txtCUIL.Size = New Size(134, 23)
        txtCUIL.TabIndex = 20
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(9, 101)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(54, 15)
        Label3.TabIndex = 0
        Label3.Text = "Sucursal:"
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(9, 14)
        Label26.Margin = New Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(35, 15)
        Label26.TabIndex = 73
        Label26.Text = "CUIL:"
        ' 
        ' cmbCategoria
        ' 
        cmbCategoria.FormattingEnabled = True
        cmbCategoria.Location = New Point(86, 39)
        cmbCategoria.Margin = New Padding(4, 3, 4, 3)
        cmbCategoria.Name = "cmbCategoria"
        cmbCategoria.Size = New Size(134, 23)
        cmbCategoria.TabIndex = 21
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(9, 43)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(61, 15)
        Label19.TabIndex = 43
        Label19.Text = "Categoría:"
        ' 
        ' Panel1Col
        ' 
        Panel1Col.Controls.Add(txtLegajoEventual)
        Panel1Col.Controls.Add(lblLegajoEventual)
        Panel1Col.Controls.Add(Label8)
        Panel1Col.Controls.Add(txtNro)
        Panel1Col.Controls.Add(Label4)
        Panel1Col.Controls.Add(txtCorreoE)
        Panel1Col.Controls.Add(txtCelular)
        Panel1Col.Controls.Add(txtTitulo)
        Panel1Col.Controls.Add(Label22)
        Panel1Col.Controls.Add(Label7)
        Panel1Col.Controls.Add(Label27)
        Panel1Col.Controls.Add(txtCalle)
        Panel1Col.Controls.Add(dtpNacimiento)
        Panel1Col.Controls.Add(txtLocalidad)
        Panel1Col.Controls.Add(cmbSexo)
        Panel1Col.Controls.Add(Label9)
        Panel1Col.Controls.Add(Label6)
        Panel1Col.Controls.Add(Label11)
        Panel1Col.Controls.Add(txtUrgencias)
        Panel1Col.Controls.Add(txtLegajo)
        Panel1Col.Controls.Add(lblLegajo)
        Panel1Col.Controls.Add(Label5)
        Panel1Col.Controls.Add(txtNombre)
        Panel1Col.Controls.Add(txtTelefono)
        Panel1Col.Controls.Add(Label2)
        Panel1Col.Controls.Add(Label20)
        Panel1Col.Controls.Add(txtNroDto)
        Panel1Col.Controls.Add(cmbTipoDto)
        Panel1Col.Controls.Add(Label34)
        Panel1Col.Dock = DockStyle.Fill
        Panel1Col.Location = New Point(3, 3)
        Panel1Col.Name = "Panel1Col"
        Panel1Col.Size = New Size(364, 302)
        Panel1Col.TabIndex = 0
        ' 
        ' txtLegajoEventual
        ' 
        txtLegajoEventual.Location = New Point(269, 11)
        txtLegajoEventual.Margin = New Padding(4, 3, 4, 3)
        txtLegajoEventual.Name = "txtLegajoEventual"
        txtLegajoEventual.ReadOnly = True
        txtLegajoEventual.Size = New Size(74, 23)
        txtLegajoEventual.TabIndex = 79
        ' 
        ' lblLegajoEventual
        ' 
        lblLegajoEventual.AutoSize = True
        lblLegajoEventual.Location = New Point(168, 14)
        lblLegajoEventual.Margin = New Padding(4, 0, 4, 0)
        lblLegajoEventual.Name = "lblLegajoEventual"
        lblLegajoEventual.Size = New Size(93, 15)
        lblLegajoEventual.TabIndex = 78
        lblLegajoEventual.Text = "Legajo Eventual:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(253, 130)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(33, 15)
        Label8.TabIndex = 77
        Label8.Text = "Nro.:"
        ' 
        ' txtNro
        ' 
        txtNro.Location = New Point(294, 126)
        txtNro.Margin = New Padding(4, 3, 4, 3)
        txtNro.Name = "txtNro"
        txtNro.Size = New Size(54, 23)
        txtNro.TabIndex = 13
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(9, 249)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(44, 15)
        Label4.TabIndex = 10
        Label4.Text = "E-Mail:"
        ' 
        ' txtCorreoE
        ' 
        txtCorreoE.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtCorreoE.Location = New Point(86, 242)
        txtCorreoE.Margin = New Padding(4, 3, 4, 3)
        txtCorreoE.Name = "txtCorreoE"
        txtCorreoE.Size = New Size(262, 23)
        txtCorreoE.TabIndex = 18
        ' 
        ' txtCelular
        ' 
        txtCelular.Location = New Point(86, 271)
        txtCelular.Margin = New Padding(4, 3, 4, 3)
        txtCelular.Name = "txtCelular"
        txtCelular.Size = New Size(99, 23)
        txtCelular.TabIndex = 16
        ' 
        ' txtTitulo
        ' 
        txtTitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtTitulo.Location = New Point(86, 213)
        txtTitulo.Margin = New Padding(4, 3, 4, 3)
        txtTitulo.Name = "txtTitulo"
        txtTitulo.Size = New Size(262, 23)
        txtTitulo.TabIndex = 17
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(9, 274)
        Label22.Margin = New Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(73, 15)
        Label22.TabIndex = 75
        Label22.Text = "Corporativo:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(9, 130)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(61, 15)
        Label7.TabIndex = 16
        Label7.Text = "Domicilio:"
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(9, 217)
        Label27.Margin = New Padding(4, 0, 4, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(54, 15)
        Label27.TabIndex = 61
        Label27.Text = "Estudios:"
        ' 
        ' txtCalle
        ' 
        txtCalle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtCalle.Location = New Point(86, 126)
        txtCalle.Margin = New Padding(4, 3, 4, 3)
        txtCalle.Name = "txtCalle"
        txtCalle.Size = New Size(139, 23)
        txtCalle.TabIndex = 12
        ' 
        ' dtpNacimiento
        ' 
        dtpNacimiento.Format = DateTimePickerFormat.Short
        dtpNacimiento.Location = New Point(86, 67)
        dtpNacimiento.Margin = New Padding(4, 3, 4, 3)
        dtpNacimiento.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        dtpNacimiento.Name = "dtpNacimiento"
        dtpNacimiento.Size = New Size(96, 23)
        dtpNacimiento.TabIndex = 8
        ' 
        ' txtLocalidad
        ' 
        txtLocalidad.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtLocalidad.Location = New Point(86, 155)
        txtLocalidad.Margin = New Padding(4, 3, 4, 3)
        txtLocalidad.Name = "txtLocalidad"
        txtLocalidad.Size = New Size(262, 23)
        txtLocalidad.TabIndex = 14
        ' 
        ' cmbSexo
        ' 
        cmbSexo.FormattingEnabled = True
        cmbSexo.Items.AddRange(New Object() {"M", "F"})
        cmbSexo.Location = New Point(249, 67)
        cmbSexo.Margin = New Padding(4, 3, 4, 3)
        cmbSexo.Name = "cmbSexo"
        cmbSexo.Size = New Size(57, 23)
        cmbSexo.TabIndex = 9
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(9, 159)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(61, 15)
        Label9.TabIndex = 20
        Label9.Text = "Localidad:"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(9, 71)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(72, 15)
        Label6.TabIndex = 53
        Label6.Text = "Nacimiento:"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(192, 189)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(59, 15)
        Label11.TabIndex = 26
        Label11.Text = "Urgencias"
        ' 
        ' txtUrgencias
        ' 
        txtUrgencias.Location = New Point(259, 185)
        txtUrgencias.Margin = New Padding(4, 3, 4, 3)
        txtUrgencias.Name = "txtUrgencias"
        txtUrgencias.Size = New Size(89, 23)
        txtUrgencias.TabIndex = 19
        ' 
        ' txtLegajo
        ' 
        txtLegajo.Location = New Point(86, 10)
        txtLegajo.Margin = New Padding(4, 3, 4, 3)
        txtLegajo.Name = "txtLegajo"
        txtLegajo.ReadOnly = True
        txtLegajo.Size = New Size(74, 23)
        txtLegajo.TabIndex = 6
        ' 
        ' lblLegajo
        ' 
        lblLegajo.AutoSize = True
        lblLegajo.Location = New Point(9, 14)
        lblLegajo.Margin = New Padding(4, 0, 4, 0)
        lblLegajo.Name = "lblLegajo"
        lblLegajo.Size = New Size(45, 15)
        lblLegajo.TabIndex = 0
        lblLegajo.Text = "Legajo:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(206, 71)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(34, 15)
        Label5.TabIndex = 71
        Label5.Text = "Sexo:"
        ' 
        ' txtNombre
        ' 
        txtNombre.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtNombre.Location = New Point(86, 39)
        txtNombre.Margin = New Padding(4, 3, 4, 3)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(262, 23)
        txtNombre.TabIndex = 7
        ' 
        ' txtTelefono
        ' 
        txtTelefono.Location = New Point(86, 184)
        txtTelefono.Margin = New Padding(4, 3, 4, 3)
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(96, 23)
        txtTelefono.TabIndex = 15
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(9, 43)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(54, 15)
        Label2.TabIndex = 0
        Label2.Text = "Nombre:"
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(9, 188)
        Label20.Margin = New Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(56, 15)
        Label20.TabIndex = 45
        Label20.Text = "Teléfono:"
        ' 
        ' txtNroDto
        ' 
        txtNroDto.Location = New Point(151, 96)
        txtNroDto.Margin = New Padding(4, 3, 4, 3)
        txtNroDto.Name = "txtNroDto"
        txtNroDto.Size = New Size(135, 23)
        txtNroDto.TabIndex = 11
        ' 
        ' cmbTipoDto
        ' 
        cmbTipoDto.FormattingEnabled = True
        cmbTipoDto.Items.AddRange(New Object() {"DNI", "LC", "LE", "CI", "Pasaporte"})
        cmbTipoDto.Location = New Point(86, 97)
        cmbTipoDto.Margin = New Padding(4, 3, 4, 3)
        cmbTipoDto.Name = "cmbTipoDto"
        cmbTipoDto.Size = New Size(57, 23)
        cmbTipoDto.TabIndex = 10
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(9, 101)
        Label34.Margin = New Padding(4, 0, 4, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(54, 15)
        Label34.TabIndex = 2
        Label34.Text = "Nro Doc:"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(chkNomarca)
        Panel1.Controls.Add(pctFoto)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(964, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(181, 302)
        Panel1.TabIndex = 3
        ' 
        ' chkNomarca
        ' 
        chkNomarca.AutoSize = True
        chkNomarca.Location = New Point(8, 245)
        chkNomarca.Margin = New Padding(4, 3, 4, 3)
        chkNomarca.Name = "chkNomarca"
        chkNomarca.Size = New Size(138, 19)
        chkNomarca.TabIndex = 42
        chkNomarca.Text = "No registra asistencia"
        chkNomarca.UseVisualStyleBackColor = True
        ' 
        ' pctFoto
        ' 
        pctFoto.BorderStyle = BorderStyle.FixedSingle
        pctFoto.Location = New Point(0, 3)
        pctFoto.Name = "pctFoto"
        pctFoto.Size = New Size(185, 236)
        pctFoto.SizeMode = PictureBoxSizeMode.StretchImage
        pctFoto.TabIndex = 10
        pctFoto.TabStop = False
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(btnAgregar)
        Panel2.Controls.Add(btnModificar)
        Panel2.Controls.Add(btnBorrar)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(3, 311)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(364, 38)
        Panel2.TabIndex = 4
        ' 
        ' btnAgregar
        ' 
        btnAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnAgregar.BackColor = SystemColors.Control
        btnAgregar.Cursor = Cursors.Hand
        btnAgregar.FlatStyle = FlatStyle.Flat
        btnAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregar.Location = New Point(4, 5)
        btnAgregar.Margin = New Padding(4, 3, 4, 3)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New Size(88, 30)
        btnAgregar.TabIndex = 5
        btnAgregar.Text = "Agregar"
        btnAgregar.UseVisualStyleBackColor = False
        ' 
        ' btnModificar
        ' 
        btnModificar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnModificar.BackColor = SystemColors.Control
        btnModificar.Cursor = Cursors.Hand
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnModificar.Location = New Point(100, 5)
        btnModificar.Margin = New Padding(4, 3, 4, 3)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(88, 30)
        btnModificar.TabIndex = 37
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = False
        ' 
        ' btnBorrar
        ' 
        btnBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnBorrar.BackColor = SystemColors.Control
        btnBorrar.Cursor = Cursors.Hand
        btnBorrar.FlatStyle = FlatStyle.Flat
        btnBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBorrar.Location = New Point(196, 5)
        btnBorrar.Margin = New Padding(4, 3, 4, 3)
        btnBorrar.Name = "btnBorrar"
        btnBorrar.Size = New Size(88, 30)
        btnBorrar.TabIndex = 38
        btnBorrar.Text = "Borrar"
        btnBorrar.UseVisualStyleBackColor = False
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(btnBonos)
        Panel3.Controls.Add(btnAceptar)
        Panel3.Controls.Add(btnCancelar)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(607, 311)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(351, 38)
        Panel3.TabIndex = 5
        ' 
        ' btnBonos
        ' 
        btnBonos.BackColor = SystemColors.Control
        btnBonos.FlatStyle = FlatStyle.Flat
        btnBonos.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnBonos.Location = New Point(216, 5)
        btnBonos.Name = "btnBonos"
        btnBonos.Size = New Size(121, 30)
        btnBonos.TabIndex = 47
        btnBonos.Text = "Bonos de Sueldo"
        btnBonos.UseVisualStyleBackColor = False
        ' 
        ' btnAceptar
        ' 
        btnAceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnAceptar.BackColor = SystemColors.Control
        btnAceptar.Cursor = Cursors.Hand
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAceptar.Location = New Point(25, 5)
        btnAceptar.Margin = New Padding(4, 3, 4, 3)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New Size(88, 30)
        btnAceptar.TabIndex = 39
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = False
        ' 
        ' btnCancelar
        ' 
        btnCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCancelar.BackColor = SystemColors.Control
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCancelar.Location = New Point(121, 5)
        btnCancelar.Margin = New Padding(4, 3, 4, 3)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(88, 30)
        btnCancelar.TabIndex = 40
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(btnDocumentacion)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(964, 311)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(181, 38)
        Panel4.TabIndex = 6
        ' 
        ' btnDocumentacion
        ' 
        btnDocumentacion.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnDocumentacion.BackColor = Color.SteelBlue
        btnDocumentacion.Cursor = Cursors.Hand
        btnDocumentacion.FlatStyle = FlatStyle.Flat
        btnDocumentacion.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnDocumentacion.ForeColor = Color.White
        btnDocumentacion.Location = New Point(5, 5)
        btnDocumentacion.Name = "btnDocumentacion"
        btnDocumentacion.Size = New Size(173, 30)
        btnDocumentacion.TabIndex = 1
        btnDocumentacion.Text = "Documentación"
        btnDocumentacion.UseVisualStyleBackColor = False
        ' 
        ' tabGrupoFamiliar
        ' 
        tabGrupoFamiliar.Controls.Add(GroupBox2)
        tabGrupoFamiliar.Location = New Point(4, 24)
        tabGrupoFamiliar.Margin = New Padding(4, 3, 4, 3)
        tabGrupoFamiliar.Name = "tabGrupoFamiliar"
        tabGrupoFamiliar.Padding = New Padding(4, 3, 4, 3)
        tabGrupoFamiliar.Size = New Size(1156, 358)
        tabGrupoFamiliar.TabIndex = 1
        tabGrupoFamiliar.Text = "Grupo Familiar"
        tabGrupoFamiliar.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(cmbNivelEstudio)
        GroupBox2.Controls.Add(cmbParentesco)
        GroupBox2.Controls.Add(DgvGrupoFamiliar)
        GroupBox2.Controls.Add(btnEliminarFamiliar)
        GroupBox2.Controls.Add(btnAgregarFamiliar)
        GroupBox2.Controls.Add(Label40)
        GroupBox2.Controls.Add(txtOcupacionFamiliar)
        GroupBox2.Controls.Add(Label39)
        GroupBox2.Controls.Add(txtEdadFamiliar)
        GroupBox2.Controls.Add(Label38)
        GroupBox2.Controls.Add(dtpNacimientoFamiliar)
        GroupBox2.Controls.Add(Label37)
        GroupBox2.Controls.Add(Label36)
        GroupBox2.Controls.Add(txtNombreFamiliar)
        GroupBox2.Controls.Add(Label35)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(4, 3)
        GroupBox2.Margin = New Padding(4, 3, 4, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(4, 3, 4, 3)
        GroupBox2.Size = New Size(1148, 352)
        GroupBox2.TabIndex = 0
        GroupBox2.TabStop = False
        GroupBox2.Text = "Información del Grupo Familiar"
        ' 
        ' cmbNivelEstudio
        ' 
        cmbNivelEstudio.DropDownStyle = ComboBoxStyle.DropDownList
        cmbNivelEstudio.Location = New Point(739, 41)
        cmbNivelEstudio.Margin = New Padding(4, 3, 4, 3)
        cmbNivelEstudio.Name = "cmbNivelEstudio"
        cmbNivelEstudio.Size = New Size(185, 23)
        cmbNivelEstudio.TabIndex = 15
        ' 
        ' cmbParentesco
        ' 
        cmbParentesco.DropDownStyle = ComboBoxStyle.DropDownList
        cmbParentesco.Location = New Point(219, 41)
        cmbParentesco.Margin = New Padding(4, 3, 4, 3)
        cmbParentesco.Name = "cmbParentesco"
        cmbParentesco.Size = New Size(155, 23)
        cmbParentesco.TabIndex = 2
        ' 
        ' DgvGrupoFamiliar
        ' 
        DgvGrupoFamiliar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvGrupoFamiliar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvGrupoFamiliar.Location = New Point(11, 70)
        DgvGrupoFamiliar.Margin = New Padding(4, 3, 4, 3)
        DgvGrupoFamiliar.Name = "DgvGrupoFamiliar"
        DgvGrupoFamiliar.ReadOnly = True
        DgvGrupoFamiliar.Size = New Size(1128, 276)
        DgvGrupoFamiliar.TabIndex = 14
        ' 
        ' btnEliminarFamiliar
        ' 
        btnEliminarFamiliar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnEliminarFamiliar.FlatStyle = FlatStyle.Flat
        btnEliminarFamiliar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEliminarFamiliar.Location = New Point(1064, 34)
        btnEliminarFamiliar.Margin = New Padding(4, 3, 4, 3)
        btnEliminarFamiliar.Name = "btnEliminarFamiliar"
        btnEliminarFamiliar.Size = New Size(75, 30)
        btnEliminarFamiliar.TabIndex = 13
        btnEliminarFamiliar.Text = "Eliminar"
        btnEliminarFamiliar.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarFamiliar
        ' 
        btnAgregarFamiliar.FlatStyle = FlatStyle.Flat
        btnAgregarFamiliar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregarFamiliar.Location = New Point(982, 34)
        btnAgregarFamiliar.Margin = New Padding(4, 3, 4, 3)
        btnAgregarFamiliar.Name = "btnAgregarFamiliar"
        btnAgregarFamiliar.Size = New Size(75, 30)
        btnAgregarFamiliar.TabIndex = 12
        btnAgregarFamiliar.Text = "Agregar"
        btnAgregarFamiliar.UseVisualStyleBackColor = True
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(739, 23)
        Label40.Margin = New Padding(4, 0, 4, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(95, 15)
        Label40.TabIndex = 10
        Label40.Text = "Nivel de Estudio:"
        ' 
        ' txtOcupacionFamiliar
        ' 
        txtOcupacionFamiliar.Location = New Point(511, 41)
        txtOcupacionFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtOcupacionFamiliar.Name = "txtOcupacionFamiliar"
        txtOcupacionFamiliar.Size = New Size(220, 23)
        txtOcupacionFamiliar.TabIndex = 9
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Location = New Point(511, 23)
        Label39.Margin = New Padding(4, 0, 4, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(68, 15)
        Label39.TabIndex = 8
        Label39.Text = "Ocupación:"
        ' 
        ' txtEdadFamiliar
        ' 
        txtEdadFamiliar.Location = New Point(932, 41)
        txtEdadFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtEdadFamiliar.Name = "txtEdadFamiliar"
        txtEdadFamiliar.Size = New Size(42, 23)
        txtEdadFamiliar.TabIndex = 7
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Location = New Point(932, 23)
        Label38.Margin = New Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(36, 15)
        Label38.TabIndex = 6
        Label38.Text = "Edad:"
        ' 
        ' dtpNacimientoFamiliar
        ' 
        dtpNacimientoFamiliar.Format = DateTimePickerFormat.Short
        dtpNacimientoFamiliar.Location = New Point(381, 41)
        dtpNacimientoFamiliar.Margin = New Padding(4, 3, 4, 3)
        dtpNacimientoFamiliar.Name = "dtpNacimientoFamiliar"
        dtpNacimientoFamiliar.Size = New Size(122, 23)
        dtpNacimientoFamiliar.TabIndex = 5
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(381, 23)
        Label37.Margin = New Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(122, 15)
        Label37.TabIndex = 4
        Label37.Text = "Fecha de Nacimiento:"
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(219, 22)
        Label36.Margin = New Padding(4, 0, 4, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(68, 15)
        Label36.TabIndex = 2
        Label36.Text = "Parentesco:"
        ' 
        ' txtNombreFamiliar
        ' 
        txtNombreFamiliar.Location = New Point(11, 41)
        txtNombreFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtNombreFamiliar.Name = "txtNombreFamiliar"
        txtNombreFamiliar.Size = New Size(200, 23)
        txtNombreFamiliar.TabIndex = 1
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(11, 23)
        Label35.Margin = New Padding(4, 0, 4, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(54, 15)
        Label35.TabIndex = 0
        Label35.Text = "Nombre:"
        ' 
        ' tabComentarios
        ' 
        tabComentarios.Controls.Add(GroupBox3)
        tabComentarios.Location = New Point(4, 24)
        tabComentarios.Margin = New Padding(4, 3, 4, 3)
        tabComentarios.Name = "tabComentarios"
        tabComentarios.Padding = New Padding(4, 3, 4, 3)
        tabComentarios.Size = New Size(1156, 358)
        tabComentarios.TabIndex = 2
        tabComentarios.Text = "Comentarios"
        tabComentarios.UseVisualStyleBackColor = True
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(DgvComentarios)
        GroupBox3.Controls.Add(btnEliminarComentario)
        GroupBox3.Controls.Add(btnAgregarComentario)
        GroupBox3.Controls.Add(cmbMotivoComentario)
        GroupBox3.Controls.Add(Label43)
        GroupBox3.Controls.Add(txtComentaComentario)
        GroupBox3.Controls.Add(Label42)
        GroupBox3.Controls.Add(dtpFechaComentario)
        GroupBox3.Controls.Add(Label41)
        GroupBox3.Dock = DockStyle.Fill
        GroupBox3.Location = New Point(4, 3)
        GroupBox3.Margin = New Padding(4, 3, 4, 3)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(4, 3, 4, 3)
        GroupBox3.Size = New Size(1148, 352)
        GroupBox3.TabIndex = 0
        GroupBox3.TabStop = False
        GroupBox3.Text = "Comentarios y Novedades"
        ' 
        ' DgvComentarios
        ' 
        DgvComentarios.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvComentarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvComentarios.Location = New Point(11, 121)
        DgvComentarios.Margin = New Padding(4, 3, 4, 3)
        DgvComentarios.Name = "DgvComentarios"
        DgvComentarios.ReadOnly = True
        DgvComentarios.Size = New Size(1128, 225)
        DgvComentarios.TabIndex = 8
        ' 
        ' btnEliminarComentario
        ' 
        btnEliminarComentario.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnEliminarComentario.FlatStyle = FlatStyle.Flat
        btnEliminarComentario.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEliminarComentario.Location = New Point(1064, 85)
        btnEliminarComentario.Margin = New Padding(4, 3, 4, 3)
        btnEliminarComentario.Name = "btnEliminarComentario"
        btnEliminarComentario.Size = New Size(75, 30)
        btnEliminarComentario.TabIndex = 7
        btnEliminarComentario.Text = "Eliminar"
        btnEliminarComentario.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarComentario
        ' 
        btnAgregarComentario.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnAgregarComentario.FlatStyle = FlatStyle.Flat
        btnAgregarComentario.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregarComentario.Location = New Point(981, 85)
        btnAgregarComentario.Margin = New Padding(4, 3, 4, 3)
        btnAgregarComentario.Name = "btnAgregarComentario"
        btnAgregarComentario.Size = New Size(75, 30)
        btnAgregarComentario.TabIndex = 6
        btnAgregarComentario.Text = "Agregar"
        btnAgregarComentario.UseVisualStyleBackColor = True
        ' 
        ' cmbMotivoComentario
        ' 
        cmbMotivoComentario.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMotivoComentario.Location = New Point(384, 22)
        cmbMotivoComentario.Margin = New Padding(4, 3, 4, 3)
        cmbMotivoComentario.Name = "cmbMotivoComentario"
        cmbMotivoComentario.Size = New Size(250, 23)
        cmbMotivoComentario.TabIndex = 6
        ' 
        ' Label43
        ' 
        Label43.AutoSize = True
        Label43.Location = New Point(328, 27)
        Label43.Margin = New Padding(4, 0, 4, 0)
        Label43.Name = "Label43"
        Label43.Size = New Size(48, 15)
        Label43.TabIndex = 4
        Label43.Text = "Motivo:"
        ' 
        ' txtComentaComentario
        ' 
        txtComentaComentario.Location = New Point(93, 58)
        txtComentaComentario.Margin = New Padding(4, 3, 4, 3)
        txtComentaComentario.Multiline = True
        txtComentaComentario.Name = "txtComentaComentario"
        txtComentaComentario.ScrollBars = ScrollBars.Vertical
        txtComentaComentario.Size = New Size(541, 57)
        txtComentaComentario.TabIndex = 3
        ' 
        ' Label42
        ' 
        Label42.AutoSize = True
        Label42.Location = New Point(13, 61)
        Label42.Margin = New Padding(4, 0, 4, 0)
        Label42.Name = "Label42"
        Label42.Size = New Size(73, 15)
        Label42.TabIndex = 2
        Label42.Text = "Comentario:"
        ' 
        ' dtpFechaComentario
        ' 
        dtpFechaComentario.Format = DateTimePickerFormat.Short
        dtpFechaComentario.Location = New Point(93, 23)
        dtpFechaComentario.Margin = New Padding(4, 3, 4, 3)
        dtpFechaComentario.Name = "dtpFechaComentario"
        dtpFechaComentario.Size = New Size(209, 23)
        dtpFechaComentario.TabIndex = 1
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.Location = New Point(13, 27)
        Label41.Margin = New Padding(4, 0, 4, 0)
        Label41.Name = "Label41"
        Label41.Size = New Size(41, 15)
        Label41.TabIndex = 0
        Label41.Text = "Fecha:"
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(GroupBoxEquipamiento)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1156, 358)
        TabPage1.TabIndex = 3
        TabPage1.Text = "Equipamiento"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' GroupBoxEquipamiento
        ' 
        GroupBoxEquipamiento.Controls.Add(txtNroTelEquipoamiento)
        GroupBoxEquipamiento.Controls.Add(Label28)
        GroupBoxEquipamiento.Controls.Add(DgvEquipamiento)
        GroupBoxEquipamiento.Controls.Add(btnEliminarEquipamiento)
        GroupBoxEquipamiento.Controls.Add(btnAgregarEquipamiento)
        GroupBoxEquipamiento.Controls.Add(cmbTipoEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label44)
        GroupBoxEquipamiento.Controls.Add(txtMarcaEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label45)
        GroupBoxEquipamiento.Controls.Add(txtModeloEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label46)
        GroupBoxEquipamiento.Controls.Add(txtNroSerieEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label47)
        GroupBoxEquipamiento.Controls.Add(txtIMEIEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label48)
        GroupBoxEquipamiento.Controls.Add(dtpFechaEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label49)
        GroupBoxEquipamiento.Controls.Add(txtObservacionesEquipamiento)
        GroupBoxEquipamiento.Controls.Add(Label50)
        GroupBoxEquipamiento.Dock = DockStyle.Fill
        GroupBoxEquipamiento.Location = New Point(3, 3)
        GroupBoxEquipamiento.Name = "GroupBoxEquipamiento"
        GroupBoxEquipamiento.Size = New Size(1150, 352)
        GroupBoxEquipamiento.TabIndex = 0
        GroupBoxEquipamiento.TabStop = False
        GroupBoxEquipamiento.Text = "Equipamiento del Agente"
        ' 
        ' txtNroTelEquipoamiento
        ' 
        txtNroTelEquipoamiento.Location = New Point(11, 94)
        txtNroTelEquipoamiento.Name = "txtNroTelEquipoamiento"
        txtNroTelEquipoamiento.Size = New Size(100, 23)
        txtNroTelEquipoamiento.TabIndex = 16
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(11, 76)
        Label28.Name = "Label28"
        Label28.Size = New Size(79, 15)
        Label28.TabIndex = 15
        Label28.Text = "Nro Teléfono:"
        ' 
        ' DgvEquipamiento
        ' 
        DgvEquipamiento.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvEquipamiento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvEquipamiento.Location = New Point(11, 123)
        DgvEquipamiento.Margin = New Padding(4, 3, 4, 3)
        DgvEquipamiento.Name = "DgvEquipamiento"
        DgvEquipamiento.ReadOnly = True
        DgvEquipamiento.Size = New Size(1129, 223)
        DgvEquipamiento.TabIndex = 14
        ' 
        ' btnEliminarEquipamiento
        ' 
        btnEliminarEquipamiento.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnEliminarEquipamiento.FlatStyle = FlatStyle.Flat
        btnEliminarEquipamiento.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEliminarEquipamiento.Location = New Point(1065, 87)
        btnEliminarEquipamiento.Margin = New Padding(4, 3, 4, 3)
        btnEliminarEquipamiento.Name = "btnEliminarEquipamiento"
        btnEliminarEquipamiento.Size = New Size(75, 30)
        btnEliminarEquipamiento.TabIndex = 13
        btnEliminarEquipamiento.Text = "Eliminar"
        btnEliminarEquipamiento.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarEquipamiento
        ' 
        btnAgregarEquipamiento.FlatStyle = FlatStyle.Flat
        btnAgregarEquipamiento.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregarEquipamiento.Location = New Point(983, 87)
        btnAgregarEquipamiento.Margin = New Padding(4, 3, 4, 3)
        btnAgregarEquipamiento.Name = "btnAgregarEquipamiento"
        btnAgregarEquipamiento.Size = New Size(75, 30)
        btnAgregarEquipamiento.TabIndex = 12
        btnAgregarEquipamiento.Text = "Agregar"
        btnAgregarEquipamiento.UseVisualStyleBackColor = True
        ' 
        ' cmbTipoEquipamiento
        ' 
        cmbTipoEquipamiento.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoEquipamiento.FormattingEnabled = True
        cmbTipoEquipamiento.Items.AddRange(New Object() {"Celular", "Tablet", "Notebook", "Radio", "GPS", "Otro"})
        cmbTipoEquipamiento.Location = New Point(11, 41)
        cmbTipoEquipamiento.Name = "cmbTipoEquipamiento"
        cmbTipoEquipamiento.Size = New Size(100, 23)
        cmbTipoEquipamiento.TabIndex = 1
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.Location = New Point(11, 23)
        Label44.Name = "Label44"
        Label44.Size = New Size(34, 15)
        Label44.TabIndex = 0
        Label44.Text = "Tipo:"
        ' 
        ' txtMarcaEquipamiento
        ' 
        txtMarcaEquipamiento.Location = New Point(118, 41)
        txtMarcaEquipamiento.Name = "txtMarcaEquipamiento"
        txtMarcaEquipamiento.Size = New Size(100, 23)
        txtMarcaEquipamiento.TabIndex = 3
        ' 
        ' Label45
        ' 
        Label45.AutoSize = True
        Label45.Location = New Point(118, 23)
        Label45.Name = "Label45"
        Label45.Size = New Size(43, 15)
        Label45.TabIndex = 2
        Label45.Text = "Marca:"
        ' 
        ' txtModeloEquipamiento
        ' 
        txtModeloEquipamiento.Location = New Point(225, 41)
        txtModeloEquipamiento.Name = "txtModeloEquipamiento"
        txtModeloEquipamiento.Size = New Size(100, 23)
        txtModeloEquipamiento.TabIndex = 5
        ' 
        ' Label46
        ' 
        Label46.AutoSize = True
        Label46.Location = New Point(225, 23)
        Label46.Name = "Label46"
        Label46.Size = New Size(51, 15)
        Label46.TabIndex = 4
        Label46.Text = "Modelo:"
        ' 
        ' txtNroSerieEquipamiento
        ' 
        txtNroSerieEquipamiento.Location = New Point(332, 41)
        txtNroSerieEquipamiento.Name = "txtNroSerieEquipamiento"
        txtNroSerieEquipamiento.Size = New Size(120, 23)
        txtNroSerieEquipamiento.TabIndex = 7
        ' 
        ' Label47
        ' 
        Label47.AutoSize = True
        Label47.Location = New Point(332, 23)
        Label47.Name = "Label47"
        Label47.Size = New Size(61, 15)
        Label47.TabIndex = 6
        Label47.Text = "Nro. Serie:"
        ' 
        ' txtIMEIEquipamiento
        ' 
        txtIMEIEquipamiento.Location = New Point(459, 41)
        txtIMEIEquipamiento.Name = "txtIMEIEquipamiento"
        txtIMEIEquipamiento.Size = New Size(120, 23)
        txtIMEIEquipamiento.TabIndex = 9
        ' 
        ' Label48
        ' 
        Label48.AutoSize = True
        Label48.Location = New Point(459, 23)
        Label48.Name = "Label48"
        Label48.Size = New Size(33, 15)
        Label48.TabIndex = 8
        Label48.Text = "IMEI:"
        ' 
        ' dtpFechaEquipamiento
        ' 
        dtpFechaEquipamiento.Format = DateTimePickerFormat.Short
        dtpFechaEquipamiento.Location = New Point(586, 41)
        dtpFechaEquipamiento.Name = "dtpFechaEquipamiento"
        dtpFechaEquipamiento.Size = New Size(100, 23)
        dtpFechaEquipamiento.TabIndex = 11
        ' 
        ' Label49
        ' 
        Label49.AutoSize = True
        Label49.Location = New Point(586, 23)
        Label49.Name = "Label49"
        Label49.Size = New Size(41, 15)
        Label49.TabIndex = 10
        Label49.Text = "Fecha:"
        ' 
        ' txtObservacionesEquipamiento
        ' 
        txtObservacionesEquipamiento.Location = New Point(118, 94)
        txtObservacionesEquipamiento.Name = "txtObservacionesEquipamiento"
        txtObservacionesEquipamiento.Size = New Size(331, 23)
        txtObservacionesEquipamiento.TabIndex = 13
        ' 
        ' Label50
        ' 
        Label50.AutoSize = True
        Label50.Location = New Point(118, 76)
        Label50.Name = "Label50"
        Label50.Size = New Size(87, 15)
        Label50.TabIndex = 12
        Label50.Text = "Observaciones:"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(GroupBox1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1156, 358)
        TabPage2.TabIndex = 4
        TabPage2.Text = "Uniforme"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(btmImprimirTalles)
        GroupBox1.Controls.Add(btnActualizaTaller)
        GroupBox1.Controls.Add(Label32)
        GroupBox1.Controls.Add(dgvTallesAgentes)
        GroupBox1.Controls.Add(cmbTalleUniforme)
        GroupBox1.Controls.Add(dgvUniformes)
        GroupBox1.Controls.Add(btnEliminarUniforme)
        GroupBox1.Controls.Add(btnAgregarUniforme)
        GroupBox1.Controls.Add(cmbTipoUniforme)
        GroupBox1.Controls.Add(Label31)
        GroupBox1.Controls.Add(Label33)
        GroupBox1.Controls.Add(dtpFechaUniforme)
        GroupBox1.Controls.Add(Label51)
        GroupBox1.Controls.Add(txtObservacionesUniforme)
        GroupBox1.Controls.Add(Label52)
        GroupBox1.Location = New Point(3, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(1150, 352)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Uniforme"
        ' 
        ' btmImprimirTalles
        ' 
        btmImprimirTalles.FlatStyle = FlatStyle.Flat
        btmImprimirTalles.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btmImprimirTalles.Location = New Point(464, 94)
        btmImprimirTalles.Name = "btmImprimirTalles"
        btmImprimirTalles.Size = New Size(79, 23)
        btmImprimirTalles.TabIndex = 33
        btmImprimirTalles.Text = "Imprimir"
        btmImprimirTalles.UseVisualStyleBackColor = True
        ' 
        ' btnActualizaTaller
        ' 
        btnActualizaTaller.FlatStyle = FlatStyle.Flat
        btnActualizaTaller.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnActualizaTaller.Location = New Point(337, 94)
        btnActualizaTaller.Name = "btnActualizaTaller"
        btnActualizaTaller.Size = New Size(121, 23)
        btnActualizaTaller.TabIndex = 32
        btnActualizaTaller.Text = "Actualizar Talles"
        btnActualizaTaller.UseVisualStyleBackColor = True
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Location = New Point(558, 97)
        Label32.Name = "Label32"
        Label32.Size = New Size(184, 15)
        Label32.TabIndex = 31
        Label32.Text = "Historial de Entrega de Uniformes"
        ' 
        ' dgvTallesAgentes
        ' 
        dgvTallesAgentes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvTallesAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTallesAgentes.Location = New Point(11, 123)
        dgvTallesAgentes.Margin = New Padding(4, 3, 4, 3)
        dgvTallesAgentes.Name = "dgvTallesAgentes"
        dgvTallesAgentes.ReadOnly = True
        dgvTallesAgentes.Size = New Size(532, 223)
        dgvTallesAgentes.TabIndex = 30
        ' 
        ' cmbTalleUniforme
        ' 
        cmbTalleUniforme.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTalleUniforme.FormattingEnabled = True
        cmbTalleUniforme.Items.AddRange(New Object() {"Celular", "Tablet", "Notebook", "Radio", "GPS", "Otro"})
        cmbTalleUniforme.Location = New Point(143, 41)
        cmbTalleUniforme.Name = "cmbTalleUniforme"
        cmbTalleUniforme.Size = New Size(81, 23)
        cmbTalleUniforme.TabIndex = 28
        ' 
        ' dgvUniformes
        ' 
        dgvUniformes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvUniformes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUniformes.Location = New Point(558, 123)
        dgvUniformes.Margin = New Padding(4, 3, 4, 3)
        dgvUniformes.Name = "dgvUniformes"
        dgvUniformes.ReadOnly = True
        dgvUniformes.Size = New Size(582, 223)
        dgvUniformes.TabIndex = 27
        ' 
        ' btnEliminarUniforme
        ' 
        btnEliminarUniforme.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnEliminarUniforme.FlatStyle = FlatStyle.Flat
        btnEliminarUniforme.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEliminarUniforme.Location = New Point(1065, 87)
        btnEliminarUniforme.Margin = New Padding(4, 3, 4, 3)
        btnEliminarUniforme.Name = "btnEliminarUniforme"
        btnEliminarUniforme.Size = New Size(75, 30)
        btnEliminarUniforme.TabIndex = 25
        btnEliminarUniforme.Text = "Eliminar"
        btnEliminarUniforme.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarUniforme
        ' 
        btnAgregarUniforme.FlatStyle = FlatStyle.Flat
        btnAgregarUniforme.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregarUniforme.Location = New Point(983, 87)
        btnAgregarUniforme.Margin = New Padding(4, 3, 4, 3)
        btnAgregarUniforme.Name = "btnAgregarUniforme"
        btnAgregarUniforme.Size = New Size(75, 30)
        btnAgregarUniforme.TabIndex = 23
        btnAgregarUniforme.Text = "Agregar"
        btnAgregarUniforme.UseVisualStyleBackColor = True
        ' 
        ' cmbTipoUniforme
        ' 
        cmbTipoUniforme.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoUniforme.FormattingEnabled = True
        cmbTipoUniforme.Items.AddRange(New Object() {"Celular", "Tablet", "Notebook", "Radio", "GPS", "Otro"})
        cmbTipoUniforme.Location = New Point(11, 41)
        cmbTipoUniforme.Name = "cmbTipoUniforme"
        cmbTipoUniforme.Size = New Size(126, 23)
        cmbTipoUniforme.TabIndex = 16
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(11, 23)
        Label31.Name = "Label31"
        Label31.Size = New Size(34, 15)
        Label31.TabIndex = 15
        Label31.Text = "Tipo:"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(143, 23)
        Label33.Name = "Label33"
        Label33.Size = New Size(34, 15)
        Label33.TabIndex = 19
        Label33.Text = "Talle:"
        ' 
        ' dtpFechaUniforme
        ' 
        dtpFechaUniforme.Format = DateTimePickerFormat.Short
        dtpFechaUniforme.Location = New Point(231, 41)
        dtpFechaUniforme.Name = "dtpFechaUniforme"
        dtpFechaUniforme.Size = New Size(100, 23)
        dtpFechaUniforme.TabIndex = 22
        ' 
        ' Label51
        ' 
        Label51.AutoSize = True
        Label51.Location = New Point(231, 23)
        Label51.Name = "Label51"
        Label51.Size = New Size(41, 15)
        Label51.TabIndex = 21
        Label51.Text = "Fecha:"
        ' 
        ' txtObservacionesUniforme
        ' 
        txtObservacionesUniforme.Location = New Point(11, 94)
        txtObservacionesUniforme.Name = "txtObservacionesUniforme"
        txtObservacionesUniforme.Size = New Size(320, 23)
        txtObservacionesUniforme.TabIndex = 26
        ' 
        ' Label52
        ' 
        Label52.AutoSize = True
        Label52.Location = New Point(11, 76)
        Label52.Name = "Label52"
        Label52.Size = New Size(87, 15)
        Label52.TabIndex = 24
        Label52.Text = "Observaciones:"
        ' 
        ' tabBonos
        ' 
        tabBonos.Controls.Add(GroupBox4)
        tabBonos.Location = New Point(4, 24)
        tabBonos.Name = "tabBonos"
        tabBonos.Padding = New Padding(3)
        tabBonos.Size = New Size(1156, 358)
        tabBonos.TabIndex = 5
        tabBonos.Text = "Bonos de Sueldo"
        tabBonos.UseVisualStyleBackColor = True
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(rdbSeleccion)
        GroupBox4.Controls.Add(rdbTodos)
        GroupBox4.Controls.Add(dgvBonos)
        GroupBox4.Controls.Add(btnEnviarBono)
        GroupBox4.Controls.Add(DataGridView1)
        GroupBox4.Controls.Add(DataGridView2)
        GroupBox4.Controls.Add(Button3)
        GroupBox4.Location = New Point(3, 3)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(1150, 352)
        GroupBox4.TabIndex = 1
        GroupBox4.TabStop = False
        GroupBox4.Text = "Bonos de Sueldo"
        ' 
        ' rdbSeleccion
        ' 
        rdbSeleccion.AutoSize = True
        rdbSeleccion.Location = New Point(932, 37)
        rdbSeleccion.Name = "rdbSeleccion"
        rdbSeleccion.Size = New Size(75, 19)
        rdbSeleccion.TabIndex = 47
        rdbSeleccion.Text = "Selección"
        rdbSeleccion.UseVisualStyleBackColor = True
        ' 
        ' rdbTodos
        ' 
        rdbTodos.AutoSize = True
        rdbTodos.Checked = True
        rdbTodos.Location = New Point(869, 37)
        rdbTodos.Name = "rdbTodos"
        rdbTodos.Size = New Size(57, 19)
        rdbTodos.TabIndex = 46
        rdbTodos.TabStop = True
        rdbTodos.Text = "Todos"
        rdbTodos.UseVisualStyleBackColor = True
        ' 
        ' dgvBonos
        ' 
        dgvBonos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBonos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBonos.Location = New Point(11, 67)
        dgvBonos.Margin = New Padding(4, 3, 4, 3)
        dgvBonos.Name = "dgvBonos"
        dgvBonos.ReadOnly = True
        dgvBonos.Size = New Size(1132, 279)
        dgvBonos.TabIndex = 34
        ' 
        ' btnEnviarBono
        ' 
        btnEnviarBono.FlatStyle = FlatStyle.Flat
        btnEnviarBono.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnEnviarBono.Location = New Point(1022, 31)
        btnEnviarBono.Name = "btnEnviarBono"
        btnEnviarBono.Size = New Size(121, 30)
        btnEnviarBono.TabIndex = 32
        btnEnviarBono.Text = "Enviar por Correo"
        btnEnviarBono.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(11, 375)
        DataGridView1.Margin = New Padding(4, 3, 4, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.Size = New Size(1482, 223)
        DataGridView1.TabIndex = 30
        ' 
        ' DataGridView2
        ' 
        DataGridView2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(558, 375)
        DataGridView2.Margin = New Padding(4, 3, 4, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.ReadOnly = True
        DataGridView2.Size = New Size(1532, 223)
        DataGridView2.TabIndex = 27
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button3.FlatStyle = FlatStyle.Flat
        Button3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button3.Location = New Point(2015, 339)
        Button3.Margin = New Padding(4, 3, 4, 3)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 30)
        Button3.TabIndex = 25
        Button3.Text = "Eliminar"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(1051, 166)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 7
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(949, 166)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 6
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' radActivos
        ' 
        radActivos.AutoSize = True
        radActivos.Checked = True
        radActivos.Location = New Point(723, 7)
        radActivos.Name = "radActivos"
        radActivos.Size = New Size(64, 19)
        radActivos.TabIndex = 3
        radActivos.TabStop = True
        radActivos.Text = "Activos"
        radActivos.UseVisualStyleBackColor = True
        ' 
        ' radTodos
        ' 
        radTodos.AutoSize = True
        radTodos.Location = New Point(793, 7)
        radTodos.Name = "radTodos"
        radTodos.Size = New Size(57, 19)
        radTodos.TabIndex = 4
        radTodos.Text = "Todos"
        radTodos.UseVisualStyleBackColor = True
        ' 
        ' lblTotalAgentes
        ' 
        lblTotalAgentes.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblTotalAgentes.AutoSize = True
        lblTotalAgentes.Location = New Point(1018, 9)
        lblTotalAgentes.Margin = New Padding(4, 0, 4, 0)
        lblTotalAgentes.Name = "lblTotalAgentes"
        lblTotalAgentes.Size = New Size(98, 15)
        lblTotalAgentes.TabIndex = 5
        lblTotalAgentes.Text = "Total de Agentes:"
        ' 
        ' txtBuscar
        ' 
        txtBuscar.Location = New Point(63, 5)
        txtBuscar.Margin = New Padding(4, 3, 4, 3)
        txtBuscar.Name = "txtBuscar"
        txtBuscar.Size = New Size(219, 23)
        txtBuscar.TabIndex = 1
        ' 
        ' lblBuscar
        ' 
        lblBuscar.AutoSize = True
        lblBuscar.Location = New Point(10, 9)
        lblBuscar.Margin = New Padding(4, 0, 4, 0)
        lblBuscar.Name = "lblBuscar"
        lblBuscar.Size = New Size(45, 15)
        lblBuscar.TabIndex = 0
        lblBuscar.Text = "Buscar:"
        ' 
        ' dgvListado
        ' 
        dgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvListado.Location = New Point(10, 35)
        dgvListado.Margin = New Padding(4, 3, 4, 3)
        dgvListado.Name = "dgvListado"
        dgvListado.Size = New Size(1161, 125)
        dgvListado.TabIndex = 2
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(1082, 569)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 30)
        btnSalir.TabIndex = 41
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' lblSucursal
        ' 
        lblSucursal.AutoSize = True
        lblSucursal.Location = New Point(318, 9)
        lblSucursal.Margin = New Padding(4, 0, 4, 0)
        lblSucursal.Name = "lblSucursal"
        lblSucursal.Size = New Size(54, 15)
        lblSucursal.TabIndex = 11
        lblSucursal.Text = "Sucursal:"
        ' 
        ' cmbSucursal
        ' 
        cmbSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSucursal.FormattingEnabled = True
        cmbSucursal.Location = New Point(379, 5)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(121, 23)
        cmbSucursal.TabIndex = 2
        ' 
        ' radEventuales
        ' 
        radEventuales.AutoSize = True
        radEventuales.Location = New Point(917, 7)
        radEventuales.Name = "radEventuales"
        radEventuales.Size = New Size(81, 19)
        radEventuales.TabIndex = 42
        radEventuales.Text = "Eventuales"
        radEventuales.UseVisualStyleBackColor = True
        ' 
        ' CmbCate
        ' 
        CmbCate.DropDownStyle = ComboBoxStyle.DropDownList
        CmbCate.FormattingEnabled = True
        CmbCate.Location = New Point(575, 6)
        CmbCate.Name = "CmbCate"
        CmbCate.Size = New Size(132, 23)
        CmbCate.TabIndex = 43
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(507, 10)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(61, 15)
        Label1.TabIndex = 44
        Label1.Text = "Categoría:"
        ' 
        ' radBaja
        ' 
        radBaja.AutoSize = True
        radBaja.Location = New Point(856, 7)
        radBaja.Name = "radBaja"
        radBaja.Size = New Size(47, 19)
        radBaja.TabIndex = 45
        radBaja.Text = "Baja"
        radBaja.UseVisualStyleBackColor = True
        ' 
        ' frmAgentes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1184, 611)
        Controls.Add(radBaja)
        Controls.Add(CmbCate)
        Controls.Add(Label1)
        Controls.Add(radEventuales)
        Controls.Add(chkEncabezados)
        Controls.Add(lnkCopiar)
        Controls.Add(cmbSucursal)
        Controls.Add(lblSucursal)
        Controls.Add(tabDatos)
        Controls.Add(btnSalir)
        Controls.Add(lblBuscar)
        Controls.Add(lblTotalAgentes)
        Controls.Add(radTodos)
        Controls.Add(radActivos)
        Controls.Add(txtBuscar)
        Controls.Add(dgvListado)
        Margin = New Padding(4, 3, 4, 3)
        MinimizeBox = False
        MinimumSize = New Size(1200, 650)
        Name = "frmAgentes"
        Text = "Actualizaciones - Mantenimiento de Agentes"
        tabDatos.ResumeLayout(False)
        tabDatosAgente.ResumeLayout(False)
        tlpDatosAgente.ResumeLayout(False)
        Panel3Col.ResumeLayout(False)
        Panel3Col.PerformLayout()
        Panel2Col.ResumeLayout(False)
        Panel2Col.PerformLayout()
        Panel1Col.ResumeLayout(False)
        Panel1Col.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(pctFoto, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        tabGrupoFamiliar.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(DgvGrupoFamiliar, ComponentModel.ISupportInitialize).EndInit()
        tabComentarios.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        CType(DgvComentarios, ComponentModel.ISupportInitialize).EndInit()
        TabPage1.ResumeLayout(False)
        GroupBoxEquipamiento.ResumeLayout(False)
        GroupBoxEquipamiento.PerformLayout()
        CType(DgvEquipamiento, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvTallesAgentes, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvUniformes, ComponentModel.ISupportInitialize).EndInit()
        tabBonos.ResumeLayout(False)
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        CType(dgvBonos, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ChkTodos As CheckBox
    Friend WithEvents ChkActivos As CheckBox
    Friend WithEvents radActivos As RadioButton
    Friend WithEvents radTodos As RadioButton
    Friend WithEvents lblTotalAgentes As Label
    Friend WithEvents txtBuscar As TextBox
    Friend WithEvents lblBuscar As Label
    Friend WithEvents dgvListado As DataGridView
    Friend WithEvents tabDatos As TabControl
    Friend WithEvents tabDatosAgente As TabPage
    Friend WithEvents tabGrupoFamiliar As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents DgvGrupoFamiliar As DataGridView
    Friend WithEvents btnEliminarFamiliar As Button
    Friend WithEvents btnAgregarFamiliar As Button
    Friend WithEvents Label40 As Label
    Friend WithEvents txtOcupacionFamiliar As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents txtEdadFamiliar As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents dtpNacimientoFamiliar As DateTimePicker
    Friend WithEvents Label37 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents txtNombreFamiliar As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents cmbParentesco As ComboBox
    Friend WithEvents tabComentarios As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents DgvComentarios As DataGridView
    Friend WithEvents btnEliminarComentario As Button
    Friend WithEvents btnAgregarComentario As Button
    Friend WithEvents cmbMotivoComentario As ComboBox
    Friend WithEvents Label43 As Label
    Friend WithEvents txtComentaComentario As TextBox
    Friend WithEvents Label42 As Label
    Friend WithEvents dtpFechaComentario As DateTimePicker
    Friend WithEvents Label41 As Label
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnBorrar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents pctFoto As PictureBox
    Friend WithEvents lblSucursal As Label
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents Panel1Col As Panel
    Friend WithEvents Label25 As Label
    Friend WithEvents cmbCaracter As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtLegajo As TextBox
    Friend WithEvents lblLegajo As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbInstituto As ComboBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCorreoE As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCalle As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbTipoDto As ComboBox
    Friend WithEvents Label34 As Label
    Friend WithEvents txtUrgencias As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel2Col As Panel
    Friend WithEvents chkNomarca As CheckBox
    Friend WithEvents Label12 As Label
    Friend WithEvents CmbMotivo As ComboBox
    Friend WithEvents txtTitulo As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents dtpNacimiento As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbEscalafon As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtLicAnual As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbCategoria As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents txtNroDto As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Panel3Col As Panel
    Friend WithEvents txtCelular As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtCUIL As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents cmbSexo As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbHorasDiarias As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cmbJefe As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents cmbEstadoParental As ComboBox
    Friend WithEvents txtLocalidad As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtInterno As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents txtFechaJubilacion As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents txtUltimaActualizacion As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents tlpDatosAgente As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtNro As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnAceptar As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents dtpBaja As DateTimePicker
    Friend WithEvents dtpIngreso As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbNivelEstudio As ComboBox
    Friend WithEvents txtAntiguedad As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btnDocumentacion As Button
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBoxEquipamiento As GroupBox
    Friend WithEvents DgvEquipamiento As DataGridView
    Friend WithEvents btnEliminarEquipamiento As Button
    Friend WithEvents btnAgregarEquipamiento As Button
    Friend WithEvents cmbTipoEquipamiento As ComboBox
    Friend WithEvents Label44 As Label
    Friend WithEvents txtMarcaEquipamiento As TextBox
    Friend WithEvents Label45 As Label
    Friend WithEvents txtModeloEquipamiento As TextBox
    Friend WithEvents Label46 As Label
    Friend WithEvents txtNroSerieEquipamiento As TextBox
    Friend WithEvents Label47 As Label
    Friend WithEvents txtIMEIEquipamiento As TextBox
    Friend WithEvents Label48 As Label
    Friend WithEvents dtpFechaEquipamiento As DateTimePicker
    Friend WithEvents Label49 As Label
    Friend WithEvents txtObservacionesEquipamiento As TextBox
    Friend WithEvents Label50 As Label
    Friend WithEvents txtNroTelEquipoamiento As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents radEventuales As RadioButton
    Friend WithEvents txtLegajoEventual As TextBox
    Friend WithEvents lblLegajoEventual As Label
    Friend WithEvents chkBaja As CheckBox
    Friend WithEvents lblAntiguedadCorregida As Label
    Friend WithEvents btnSaldoVacaciones As Button
    Friend WithEvents lblSaldoVacaciones As Label
    Friend WithEvents CmbCate As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkSindicato As CheckBox
    Friend WithEvents radBaja As RadioButton
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbTalleUniforme As ComboBox
    Friend WithEvents dgvUniformes As DataGridView
    Friend WithEvents btnEliminarUniforme As Button
    Friend WithEvents btnAgregarUniforme As Button
    Friend WithEvents cmbTipoUniforme As ComboBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents dtpFechaUniforme As DateTimePicker
    Friend WithEvents Label51 As Label
    Friend WithEvents txtObservacionesUniforme As TextBox
    Friend WithEvents Label52 As Label
    Friend WithEvents dgvTallesAgentes As DataGridView
    Friend WithEvents dgvBonos As DataGridView
    Friend WithEvents Label32 As Label
    Friend WithEvents btnActualizaTaller As Button
    Friend WithEvents btmImprimirTalles As Button
    Friend WithEvents btnBonos As Button
    Friend WithEvents tabBonos As TabPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents DataGridView3 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents btnEnviarBono As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents Button3 As Button
    Friend WithEvents rdbSeleccion As RadioButton
    Friend WithEvents rdbTodos As RadioButton

End Class
