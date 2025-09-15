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
        TableLayoutPanel1 = New TableLayoutPanel()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        GroupBox1 = New GroupBox()
        Panel1Col = New Panel()
        txtBaja = New TextBox()
        Label25 = New Label()
        cmbCaracter = New ComboBox()
        Label17 = New Label()
        txtComentario = New TextBox()
        Label18 = New Label()
        txtLegajo = New TextBox()
        Label1 = New Label()
        txtNombre = New TextBox()
        Label2 = New Label()
        cmbInstituto = New ComboBox()
        txtIngreso = New TextBox()
        Label24 = New Label()
        Label3 = New Label()
        txtCorreoE = New TextBox()
        Label4 = New Label()
        txtCalle = New TextBox()
        Label7 = New Label()
        cmbTipoDto = New ComboBox()
        Label34 = New Label()
        txtUrgencias = New TextBox()
        Label11 = New Label()
        Panel2Col = New Panel()
        chkNomarca = New CheckBox()
        Label12 = New Label()
        CmbMotivo = New ComboBox()
        txtTitulo = New TextBox()
        Label27 = New Label()
        txtNro = New TextBox()
        Label8 = New Label()
        dtpNacimiento = New DateTimePicker()
        Label6 = New Label()
        cmbEscalafon = New ComboBox()
        Label14 = New Label()
        txtLicAnual = New TextBox()
        Label16 = New Label()
        cmbCategoria = New ComboBox()
        Label19 = New Label()
        txtTelefono = New TextBox()
        txtNroDto = New TextBox()
        Label33 = New Label()
        Label20 = New Label()
        Panel3Col = New Panel()
        txtCelular = New TextBox()
        Label22 = New Label()
        txtCUIL = New TextBox()
        Label26 = New Label()
        cmbSexo = New ComboBox()
        Label5 = New Label()
        cmbHorasDiarias = New ComboBox()
        Label13 = New Label()
        cmbJefe = New ComboBox()
        Label15 = New Label()
        cmbEstadoParental = New ComboBox()
        txtLocalidad = New TextBox()
        Label9 = New Label()
        txtInterno = New TextBox()
        Label21 = New Label()
        txtFechaJubilacion = New TextBox()
        Label29 = New Label()
        Label30 = New Label()
        txtUltimaActualizacion = New TextBox()
        Label23 = New Label()
        TabPage2 = New TabPage()
        GroupBox2 = New GroupBox()
        DgvGrupoFamiliar = New DataGridView()
        btnEliminarFamiliar = New Button()
        btnAgregarFamiliar = New Button()
        txtNivelEstudio = New TextBox()
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
        cmbParentesco = New ComboBox()
        TabPage3 = New TabPage()
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
        Panel2 = New Panel()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        optActivos = New RadioButton()
        opTodos = New RadioButton()
        Label10 = New Label()
        TxtBuscar = New TextBox()
        Label44 = New Label()
        DgvListado = New DataGridView()
        Panel1 = New Panel()
        btnSalir = New Button()
        btnCancelar = New Button()
        btnAceptar = New Button()
        btnBorrar = New Button()
        btnModificar = New Button()
        btnAgregar = New Button()
        pctFoto = New PictureBox()
        TableLayoutPanel1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        GroupBox1.SuspendLayout()
        Panel1Col.SuspendLayout()
        Panel2Col.SuspendLayout()
        Panel3Col.SuspendLayout()
        TabPage2.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DgvGrupoFamiliar, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(DgvComentarios, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(pctFoto, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 205F))
        TableLayoutPanel1.Controls.Add(TabControl1, 0, 1)
        TableLayoutPanel1.Controls.Add(Panel2, 0, 0)
        TableLayoutPanel1.Controls.Add(Panel1, 0, 2)
        TableLayoutPanel1.Controls.Add(pctFoto, 1, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 439F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 86F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(1069, 772)
        TableLayoutPanel1.TabIndex = 2
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(4, 250)
        TabControl1.Margin = New Padding(4, 3, 4, 3)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(856, 433)
        TabControl1.TabIndex = 8
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(GroupBox1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Margin = New Padding(4, 3, 4, 3)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(4, 3, 4, 3)
        TabPage1.Size = New Size(848, 405)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Datos del Agente"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Panel1Col)
        GroupBox1.Controls.Add(Panel2Col)
        GroupBox1.Controls.Add(Panel3Col)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(4, 3)
        GroupBox1.Margin = New Padding(4, 3, 4, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4, 3, 4, 3)
        GroupBox1.Size = New Size(840, 399)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        ' 
        ' Panel1Col
        ' 
        Panel1Col.Controls.Add(txtBaja)
        Panel1Col.Controls.Add(Label25)
        Panel1Col.Controls.Add(cmbCaracter)
        Panel1Col.Controls.Add(Label17)
        Panel1Col.Controls.Add(txtComentario)
        Panel1Col.Controls.Add(Label18)
        Panel1Col.Controls.Add(txtLegajo)
        Panel1Col.Controls.Add(Label1)
        Panel1Col.Controls.Add(txtNombre)
        Panel1Col.Controls.Add(Label2)
        Panel1Col.Controls.Add(cmbInstituto)
        Panel1Col.Controls.Add(txtIngreso)
        Panel1Col.Controls.Add(Label24)
        Panel1Col.Controls.Add(Label3)
        Panel1Col.Controls.Add(txtCorreoE)
        Panel1Col.Controls.Add(Label4)
        Panel1Col.Controls.Add(txtCalle)
        Panel1Col.Controls.Add(Label7)
        Panel1Col.Controls.Add(cmbTipoDto)
        Panel1Col.Controls.Add(Label34)
        Panel1Col.Controls.Add(txtUrgencias)
        Panel1Col.Controls.Add(Label11)
        Panel1Col.Location = New Point(5, 14)
        Panel1Col.Name = "Panel1Col"
        Panel1Col.Size = New Size(294, 371)
        Panel1Col.TabIndex = 0
        ' 
        ' txtBaja
        ' 
        txtBaja.Location = New Point(81, 269)
        txtBaja.Margin = New Padding(4, 3, 4, 3)
        txtBaja.Name = "txtBaja"
        txtBaja.Size = New Size(143, 23)
        txtBaja.TabIndex = 58
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(4, 272)
        Label25.Margin = New Padding(4, 0, 4, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(32, 15)
        Label25.TabIndex = 57
        Label25.Text = "Baja:"
        ' 
        ' cmbCaracter
        ' 
        cmbCaracter.FormattingEnabled = True
        cmbCaracter.Location = New Point(81, 177)
        cmbCaracter.Margin = New Padding(4, 3, 4, 3)
        cmbCaracter.Name = "cmbCaracter"
        cmbCaracter.Size = New Size(136, 23)
        cmbCaracter.TabIndex = 45
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(4, 180)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(54, 15)
        Label17.TabIndex = 44
        Label17.Text = "Carácter:"
        ' 
        ' txtComentario
        ' 
        txtComentario.Location = New Point(81, 300)
        txtComentario.Margin = New Padding(4, 3, 4, 3)
        txtComentario.Multiline = True
        txtComentario.Name = "txtComentario"
        txtComentario.ScrollBars = ScrollBars.Both
        txtComentario.Size = New Size(209, 66)
        txtComentario.TabIndex = 43
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(4, 303)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(73, 15)
        Label18.TabIndex = 42
        Label18.Text = "Comentario:"
        ' 
        ' txtLegajo
        ' 
        txtLegajo.Location = New Point(81, 0)
        txtLegajo.Margin = New Padding(4, 3, 4, 3)
        txtLegajo.Name = "txtLegajo"
        txtLegajo.Size = New Size(96, 23)
        txtLegajo.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(3, 3)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 0
        Label1.Text = "Legajo:"
        ' 
        ' txtNombre
        ' 
        txtNombre.Location = New Point(81, 29)
        txtNombre.Margin = New Padding(4, 3, 4, 3)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(209, 23)
        txtNombre.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(3, 32)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(54, 15)
        Label2.TabIndex = 0
        Label2.Text = "Nombre:"
        ' 
        ' cmbInstituto
        ' 
        cmbInstituto.FormattingEnabled = True
        cmbInstituto.Location = New Point(81, 211)
        cmbInstituto.Margin = New Padding(4, 3, 4, 3)
        cmbInstituto.Name = "cmbInstituto"
        cmbInstituto.Size = New Size(143, 23)
        cmbInstituto.TabIndex = 1
        ' 
        ' txtIngreso
        ' 
        txtIngreso.Location = New Point(81, 240)
        txtIngreso.Margin = New Padding(4, 3, 4, 3)
        txtIngreso.Name = "txtIngreso"
        txtIngreso.Size = New Size(143, 23)
        txtIngreso.TabIndex = 54
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(3, 243)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(49, 15)
        Label24.TabIndex = 53
        Label24.Text = "Ingreso:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(3, 214)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(54, 15)
        Label3.TabIndex = 0
        Label3.Text = "Sucursal:"
        ' 
        ' txtCorreoE
        ' 
        txtCorreoE.Location = New Point(81, 87)
        txtCorreoE.Margin = New Padding(4, 3, 4, 3)
        txtCorreoE.Name = "txtCorreoE"
        txtCorreoE.Size = New Size(209, 23)
        txtCorreoE.TabIndex = 11
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(3, 90)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(44, 15)
        Label4.TabIndex = 10
        Label4.Text = "E-Mail:"
        ' 
        ' txtCalle
        ' 
        txtCalle.Location = New Point(81, 116)
        txtCalle.Margin = New Padding(4, 3, 4, 3)
        txtCalle.Name = "txtCalle"
        txtCalle.Size = New Size(209, 23)
        txtCalle.TabIndex = 17
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(3, 119)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(36, 15)
        Label7.TabIndex = 16
        Label7.Text = "Calle:"
        ' 
        ' cmbTipoDto
        ' 
        cmbTipoDto.FormattingEnabled = True
        cmbTipoDto.Items.AddRange(New Object() {"DNI", "LC", "LE", "CI", "Pasaporte"})
        cmbTipoDto.Location = New Point(81, 58)
        cmbTipoDto.Margin = New Padding(4, 3, 4, 3)
        cmbTipoDto.Name = "cmbTipoDto"
        cmbTipoDto.Size = New Size(57, 23)
        cmbTipoDto.TabIndex = 3
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(4, 64)
        Label34.Margin = New Padding(4, 0, 4, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(56, 15)
        Label34.TabIndex = 2
        Label34.Text = "Tipo Dto:"
        ' 
        ' txtUrgencias
        ' 
        txtUrgencias.Location = New Point(81, 145)
        txtUrgencias.Margin = New Padding(4, 3, 4, 3)
        txtUrgencias.Name = "txtUrgencias"
        txtUrgencias.Size = New Size(143, 23)
        txtUrgencias.TabIndex = 27
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(3, 148)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(59, 15)
        Label11.TabIndex = 26
        Label11.Text = "Urgencias"
        ' 
        ' Panel2Col
        ' 
        Panel2Col.Controls.Add(chkNomarca)
        Panel2Col.Controls.Add(Label12)
        Panel2Col.Controls.Add(CmbMotivo)
        Panel2Col.Controls.Add(txtTitulo)
        Panel2Col.Controls.Add(Label27)
        Panel2Col.Controls.Add(txtNro)
        Panel2Col.Controls.Add(Label8)
        Panel2Col.Controls.Add(dtpNacimiento)
        Panel2Col.Controls.Add(Label6)
        Panel2Col.Controls.Add(cmbEscalafon)
        Panel2Col.Controls.Add(Label14)
        Panel2Col.Controls.Add(txtLicAnual)
        Panel2Col.Controls.Add(Label16)
        Panel2Col.Controls.Add(cmbCategoria)
        Panel2Col.Controls.Add(Label19)
        Panel2Col.Controls.Add(txtTelefono)
        Panel2Col.Controls.Add(txtNroDto)
        Panel2Col.Controls.Add(Label33)
        Panel2Col.Controls.Add(Label20)
        Panel2Col.Location = New Point(305, 14)
        Panel2Col.Name = "Panel2Col"
        Panel2Col.Size = New Size(233, 371)
        Panel2Col.TabIndex = 1
        ' 
        ' chkNomarca
        ' 
        chkNomarca.AutoSize = True
        chkNomarca.Location = New Point(85, 301)
        chkNomarca.Margin = New Padding(4, 3, 4, 3)
        chkNomarca.Name = "chkNomarca"
        chkNomarca.Size = New Size(78, 19)
        chkNomarca.TabIndex = 73
        chkNomarca.Text = "No marca"
        chkNomarca.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(3, 268)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(48, 15)
        Label12.TabIndex = 72
        Label12.Text = "Motivo:"
        ' 
        ' CmbMotivo
        ' 
        CmbMotivo.FormattingEnabled = True
        CmbMotivo.Location = New Point(83, 265)
        CmbMotivo.Margin = New Padding(4, 3, 4, 3)
        CmbMotivo.Name = "CmbMotivo"
        CmbMotivo.Size = New Size(142, 23)
        CmbMotivo.TabIndex = 71
        ' 
        ' txtTitulo
        ' 
        txtTitulo.Location = New Point(82, 150)
        txtTitulo.Margin = New Padding(4, 3, 4, 3)
        txtTitulo.Name = "txtTitulo"
        txtTitulo.Size = New Size(143, 23)
        txtTitulo.TabIndex = 62
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(5, 152)
        Label27.Margin = New Padding(4, 0, 4, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(54, 15)
        Label27.TabIndex = 61
        Label27.Text = "Estudios:"
        ' 
        ' txtNro
        ' 
        txtNro.Location = New Point(83, 121)
        txtNro.Margin = New Padding(4, 3, 4, 3)
        txtNro.Name = "txtNro"
        txtNro.Size = New Size(67, 23)
        txtNro.TabIndex = 56
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(5, 124)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(30, 15)
        Label8.TabIndex = 55
        Label8.Text = "Nro:"
        ' 
        ' dtpNacimiento
        ' 
        dtpNacimiento.Format = DateTimePickerFormat.Short
        dtpNacimiento.Location = New Point(83, 31)
        dtpNacimiento.Margin = New Padding(4, 3, 4, 3)
        dtpNacimiento.Name = "dtpNacimiento"
        dtpNacimiento.Size = New Size(135, 23)
        dtpNacimiento.TabIndex = 54
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(5, 34)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(72, 15)
        Label6.TabIndex = 53
        Label6.Text = "Nacimiento:"
        ' 
        ' cmbEscalafon
        ' 
        cmbEscalafon.FormattingEnabled = True
        cmbEscalafon.Location = New Point(82, 208)
        cmbEscalafon.Margin = New Padding(4, 3, 4, 3)
        cmbEscalafon.Name = "cmbEscalafon"
        cmbEscalafon.Size = New Size(143, 23)
        cmbEscalafon.TabIndex = 33
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(3, 212)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(48, 15)
        Label14.TabIndex = 32
        Label14.Text = "Oficina:"
        ' 
        ' txtLicAnual
        ' 
        txtLicAnual.Location = New Point(82, 236)
        txtLicAnual.Margin = New Padding(4, 3, 4, 3)
        txtLicAnual.Name = "txtLicAnual"
        txtLicAnual.Size = New Size(59, 23)
        txtLicAnual.TabIndex = 37
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(3, 241)
        Label16.Margin = New Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(62, 15)
        Label16.TabIndex = 36
        Label16.Text = "Lic. Anual:"
        ' 
        ' cmbCategoria
        ' 
        cmbCategoria.FormattingEnabled = True
        cmbCategoria.Location = New Point(83, 179)
        cmbCategoria.Margin = New Padding(4, 3, 4, 3)
        cmbCategoria.Name = "cmbCategoria"
        cmbCategoria.Size = New Size(142, 23)
        cmbCategoria.TabIndex = 44
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(3, 183)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(61, 15)
        Label19.TabIndex = 43
        Label19.Text = "Categoría:"
        ' 
        ' txtTelefono
        ' 
        txtTelefono.Location = New Point(82, 92)
        txtTelefono.Margin = New Padding(4, 3, 4, 3)
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(136, 23)
        txtTelefono.TabIndex = 46
        ' 
        ' txtNroDto
        ' 
        txtNroDto.Location = New Point(83, 63)
        txtNroDto.Margin = New Padding(4, 3, 4, 3)
        txtNroDto.Name = "txtNroDto"
        txtNroDto.Size = New Size(135, 23)
        txtNroDto.TabIndex = 5
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(5, 65)
        Label33.Margin = New Padding(4, 0, 4, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(52, 15)
        Label33.TabIndex = 4
        Label33.Text = "Nro Dto:"
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(4, 94)
        Label20.Margin = New Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(56, 15)
        Label20.TabIndex = 45
        Label20.Text = "Teléfono:"
        ' 
        ' Panel3Col
        ' 
        Panel3Col.Controls.Add(txtCelular)
        Panel3Col.Controls.Add(Label22)
        Panel3Col.Controls.Add(txtCUIL)
        Panel3Col.Controls.Add(Label26)
        Panel3Col.Controls.Add(cmbSexo)
        Panel3Col.Controls.Add(Label5)
        Panel3Col.Controls.Add(cmbHorasDiarias)
        Panel3Col.Controls.Add(Label13)
        Panel3Col.Controls.Add(cmbJefe)
        Panel3Col.Controls.Add(Label15)
        Panel3Col.Controls.Add(cmbEstadoParental)
        Panel3Col.Controls.Add(txtLocalidad)
        Panel3Col.Controls.Add(Label9)
        Panel3Col.Controls.Add(txtInterno)
        Panel3Col.Controls.Add(Label21)
        Panel3Col.Controls.Add(txtFechaJubilacion)
        Panel3Col.Controls.Add(Label29)
        Panel3Col.Controls.Add(Label30)
        Panel3Col.Controls.Add(txtUltimaActualizacion)
        Panel3Col.Controls.Add(Label23)
        Panel3Col.Location = New Point(544, 14)
        Panel3Col.Name = "Panel3Col"
        Panel3Col.Size = New Size(299, 370)
        Panel3Col.TabIndex = 2
        ' 
        ' txtCelular
        ' 
        txtCelular.Location = New Point(86, 91)
        txtCelular.Margin = New Padding(4, 3, 4, 3)
        txtCelular.Name = "txtCelular"
        txtCelular.Size = New Size(136, 23)
        txtCelular.TabIndex = 76
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(3, 99)
        Label22.Margin = New Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(47, 15)
        Label22.TabIndex = 75
        Label22.Text = "Celular:"
        ' 
        ' txtCUIL
        ' 
        txtCUIL.Location = New Point(86, 62)
        txtCUIL.Margin = New Padding(4, 3, 4, 3)
        txtCUIL.Name = "txtCUIL"
        txtCUIL.Size = New Size(136, 23)
        txtCUIL.TabIndex = 74
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(3, 64)
        Label26.Margin = New Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(35, 15)
        Label26.TabIndex = 73
        Label26.Text = "CUIL:"
        ' 
        ' cmbSexo
        ' 
        cmbSexo.FormattingEnabled = True
        cmbSexo.Items.AddRange(New Object() {"M", "F"})
        cmbSexo.Location = New Point(86, 33)
        cmbSexo.Margin = New Padding(4, 3, 4, 3)
        cmbSexo.Name = "cmbSexo"
        cmbSexo.Size = New Size(57, 23)
        cmbSexo.TabIndex = 72
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(3, 36)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(34, 15)
        Label5.TabIndex = 71
        Label5.Text = "Sexo:"
        ' 
        ' cmbHorasDiarias
        ' 
        cmbHorasDiarias.FormattingEnabled = True
        cmbHorasDiarias.Location = New Point(88, 207)
        cmbHorasDiarias.Margin = New Padding(4, 3, 4, 3)
        cmbHorasDiarias.Name = "cmbHorasDiarias"
        cmbHorasDiarias.Size = New Size(134, 23)
        cmbHorasDiarias.TabIndex = 31
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(3, 211)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(79, 15)
        Label13.TabIndex = 30
        Label13.Text = "Horas Diarias:"
        ' 
        ' cmbJefe
        ' 
        cmbJefe.FormattingEnabled = True
        cmbJefe.Location = New Point(86, 178)
        cmbJefe.Margin = New Padding(4, 3, 4, 3)
        cmbJefe.Name = "cmbJefe"
        cmbJefe.Size = New Size(136, 23)
        cmbJefe.TabIndex = 35
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(3, 181)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(69, 15)
        Label15.TabIndex = 34
        Label15.Text = "A Cargo de:"
        ' 
        ' cmbEstadoParental
        ' 
        cmbEstadoParental.FormattingEnabled = True
        cmbEstadoParental.Location = New Point(107, 149)
        cmbEstadoParental.Margin = New Padding(4, 3, 4, 3)
        cmbEstadoParental.Name = "cmbEstadoParental"
        cmbEstadoParental.Size = New Size(115, 23)
        cmbEstadoParental.TabIndex = 64
        ' 
        ' txtLocalidad
        ' 
        txtLocalidad.Location = New Point(86, 120)
        txtLocalidad.Margin = New Padding(4, 3, 4, 3)
        txtLocalidad.Name = "txtLocalidad"
        txtLocalidad.Size = New Size(209, 23)
        txtLocalidad.TabIndex = 21
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(3, 123)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(61, 15)
        Label9.TabIndex = 20
        Label9.Text = "Localidad:"
        ' 
        ' txtInterno
        ' 
        txtInterno.Location = New Point(88, 236)
        txtInterno.Margin = New Padding(4, 3, 4, 3)
        txtInterno.Name = "txtInterno"
        txtInterno.Size = New Size(136, 23)
        txtInterno.TabIndex = 48
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(3, 240)
        Label21.Margin = New Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(48, 15)
        Label21.TabIndex = 47
        Label21.Text = "Interno:"
        ' 
        ' txtFechaJubilacion
        ' 
        txtFechaJubilacion.Location = New Point(108, 265)
        txtFechaJubilacion.Margin = New Padding(4, 3, 4, 3)
        txtFechaJubilacion.Name = "txtFechaJubilacion"
        txtFechaJubilacion.Size = New Size(116, 23)
        txtFechaJubilacion.TabIndex = 66
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(3, 152)
        Label29.Margin = New Padding(4, 0, 4, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(91, 15)
        Label29.TabIndex = 63
        Label29.Text = "Estado Parental:"
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(3, 268)
        Label30.Margin = New Padding(4, 0, 4, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(97, 15)
        Label30.TabIndex = 65
        Label30.Text = "Fecha Jubilación:"
        ' 
        ' txtUltimaActualizacion
        ' 
        txtUltimaActualizacion.Location = New Point(130, 294)
        txtUltimaActualizacion.Margin = New Padding(4, 3, 4, 3)
        txtUltimaActualizacion.Name = "txtUltimaActualizacion"
        txtUltimaActualizacion.Size = New Size(94, 23)
        txtUltimaActualizacion.TabIndex = 52
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(3, 298)
        Label23.Margin = New Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(119, 15)
        Label23.TabIndex = 51
        Label23.Text = "Última Actualización:"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(GroupBox2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Margin = New Padding(4, 3, 4, 3)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(4, 3, 4, 3)
        TabPage2.Size = New Size(848, 405)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Grupo Familiar"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(DgvGrupoFamiliar)
        GroupBox2.Controls.Add(btnEliminarFamiliar)
        GroupBox2.Controls.Add(btnAgregarFamiliar)
        GroupBox2.Controls.Add(txtNivelEstudio)
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
        GroupBox2.Controls.Add(cmbParentesco)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(4, 3)
        GroupBox2.Margin = New Padding(4, 3, 4, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(4, 3, 4, 3)
        GroupBox2.Size = New Size(840, 399)
        GroupBox2.TabIndex = 0
        GroupBox2.TabStop = False
        GroupBox2.Text = "Información del Grupo Familiar"
        ' 
        ' DgvGrupoFamiliar
        ' 
        DgvGrupoFamiliar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvGrupoFamiliar.Location = New Point(11, 107)
        DgvGrupoFamiliar.Margin = New Padding(4, 3, 4, 3)
        DgvGrupoFamiliar.Name = "DgvGrupoFamiliar"
        DgvGrupoFamiliar.Size = New Size(839, 286)
        DgvGrupoFamiliar.TabIndex = 14
        ' 
        ' btnEliminarFamiliar
        ' 
        btnEliminarFamiliar.FlatStyle = FlatStyle.Flat
        btnEliminarFamiliar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEliminarFamiliar.Location = New Point(757, 71)
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
        btnAgregarFamiliar.Location = New Point(674, 71)
        btnAgregarFamiliar.Margin = New Padding(4, 3, 4, 3)
        btnAgregarFamiliar.Name = "btnAgregarFamiliar"
        btnAgregarFamiliar.Size = New Size(75, 30)
        btnAgregarFamiliar.TabIndex = 12
        btnAgregarFamiliar.Text = "Agregar"
        btnAgregarFamiliar.UseVisualStyleBackColor = True
        ' 
        ' txtNivelEstudio
        ' 
        txtNivelEstudio.Location = New Point(364, 52)
        txtNivelEstudio.Margin = New Padding(4, 3, 4, 3)
        txtNivelEstudio.Name = "txtNivelEstudio"
        txtNivelEstudio.Size = New Size(143, 23)
        txtNivelEstudio.TabIndex = 11
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(261, 55)
        Label40.Margin = New Padding(4, 0, 4, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(95, 15)
        Label40.TabIndex = 10
        Label40.Text = "Nivel de Estudio:"
        ' 
        ' txtOcupacionFamiliar
        ' 
        txtOcupacionFamiliar.Location = New Point(85, 52)
        txtOcupacionFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtOcupacionFamiliar.Name = "txtOcupacionFamiliar"
        txtOcupacionFamiliar.Size = New Size(162, 23)
        txtOcupacionFamiliar.TabIndex = 9
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Location = New Point(4, 55)
        Label39.Margin = New Padding(4, 0, 4, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(68, 15)
        Label39.TabIndex = 8
        Label39.Text = "Ocupación:"
        ' 
        ' txtEdadFamiliar
        ' 
        txtEdadFamiliar.Location = New Point(509, 23)
        txtEdadFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtEdadFamiliar.Name = "txtEdadFamiliar"
        txtEdadFamiliar.Size = New Size(42, 23)
        txtEdadFamiliar.TabIndex = 7
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Location = New Point(465, 27)
        Label38.Margin = New Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(36, 15)
        Label38.TabIndex = 6
        Label38.Text = "Edad:"
        ' 
        ' dtpNacimientoFamiliar
        ' 
        dtpNacimientoFamiliar.Format = DateTimePickerFormat.Short
        dtpNacimientoFamiliar.Location = New Point(342, 23)
        dtpNacimientoFamiliar.Margin = New Padding(4, 3, 4, 3)
        dtpNacimientoFamiliar.Name = "dtpNacimientoFamiliar"
        dtpNacimientoFamiliar.Size = New Size(107, 23)
        dtpNacimientoFamiliar.TabIndex = 5
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(212, 27)
        Label37.Margin = New Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(122, 15)
        Label37.TabIndex = 4
        Label37.Text = "Fecha de Nacimiento:"
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(568, 28)
        Label36.Margin = New Padding(4, 0, 4, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(68, 15)
        Label36.TabIndex = 2
        Label36.Text = "Parentesco:"
        ' 
        ' txtNombreFamiliar
        ' 
        txtNombreFamiliar.Location = New Point(85, 23)
        txtNombreFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtNombreFamiliar.Name = "txtNombreFamiliar"
        txtNombreFamiliar.Size = New Size(121, 23)
        txtNombreFamiliar.TabIndex = 1
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(5, 27)
        Label35.Margin = New Padding(4, 0, 4, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(54, 15)
        Label35.TabIndex = 0
        Label35.Text = "Nombre:"
        ' 
        ' cmbParentesco
        ' 
        cmbParentesco.DropDownStyle = ComboBoxStyle.DropDownList
        cmbParentesco.Location = New Point(644, 24)
        cmbParentesco.Margin = New Padding(4, 3, 4, 3)
        cmbParentesco.Name = "cmbParentesco"
        cmbParentesco.Size = New Size(155, 23)
        cmbParentesco.TabIndex = 2
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(GroupBox3)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Margin = New Padding(4, 3, 4, 3)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(4, 3, 4, 3)
        TabPage3.Size = New Size(848, 405)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Comentarios"
        TabPage3.UseVisualStyleBackColor = True
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
        GroupBox3.Size = New Size(840, 399)
        GroupBox3.TabIndex = 0
        GroupBox3.TabStop = False
        GroupBox3.Text = "Comentarios y Novedades"
        ' 
        ' DgvComentarios
        ' 
        DgvComentarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvComentarios.Location = New Point(12, 138)
        DgvComentarios.Margin = New Padding(4, 3, 4, 3)
        DgvComentarios.Name = "DgvComentarios"
        DgvComentarios.Size = New Size(839, 255)
        DgvComentarios.TabIndex = 8
        ' 
        ' btnEliminarComentario
        ' 
        btnEliminarComentario.FlatStyle = FlatStyle.Flat
        btnEliminarComentario.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEliminarComentario.Location = New Point(757, 102)
        btnEliminarComentario.Margin = New Padding(4, 3, 4, 3)
        btnEliminarComentario.Name = "btnEliminarComentario"
        btnEliminarComentario.Size = New Size(75, 30)
        btnEliminarComentario.TabIndex = 7
        btnEliminarComentario.Text = "Eliminar"
        btnEliminarComentario.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarComentario
        ' 
        btnAgregarComentario.FlatStyle = FlatStyle.Flat
        btnAgregarComentario.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregarComentario.Location = New Point(674, 102)
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
        txtComentaComentario.Size = New Size(209, 57)
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
        ' Panel2
        ' 
        Panel2.Controls.Add(chkEncabezados)
        Panel2.Controls.Add(lnkCopiar)
        Panel2.Controls.Add(optActivos)
        Panel2.Controls.Add(opTodos)
        Panel2.Controls.Add(Label10)
        Panel2.Controls.Add(TxtBuscar)
        Panel2.Controls.Add(Label44)
        Panel2.Controls.Add(DgvListado)
        Panel2.Location = New Point(4, 3)
        Panel2.Margin = New Padding(4, 3, 4, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(856, 241)
        Panel2.TabIndex = 4
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(728, 222)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 7
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(626, 222)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 6
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' optActivos
        ' 
        optActivos.AutoSize = True
        optActivos.Checked = True
        optActivos.Location = New Point(454, 6)
        optActivos.Name = "optActivos"
        optActivos.Size = New Size(64, 19)
        optActivos.TabIndex = 2
        optActivos.TabStop = True
        optActivos.Text = "Activos"
        optActivos.UseVisualStyleBackColor = True
        ' 
        ' opTodos
        ' 
        opTodos.AutoSize = True
        opTodos.Location = New Point(527, 6)
        opTodos.Name = "opTodos"
        opTodos.Size = New Size(57, 19)
        opTodos.TabIndex = 3
        opTodos.Text = "Todos"
        opTodos.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(694, 8)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(113, 15)
        Label10.TabIndex = 5
        Label10.Text = "Total de Empleados:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(93, 5)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(349, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.Location = New Point(12, 8)
        Label44.Margin = New Padding(4, 0, 4, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(45, 15)
        Label44.TabIndex = 0
        Label44.Text = "Buscar:"
        ' 
        ' DgvListado
        ' 
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(6, 34)
        DgvListado.Margin = New Padding(4, 3, 4, 3)
        DgvListado.Name = "DgvListado"
        DgvListado.Size = New Size(848, 182)
        DgvListado.TabIndex = 2
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(btnSalir)
        Panel1.Controls.Add(btnCancelar)
        Panel1.Controls.Add(btnAceptar)
        Panel1.Controls.Add(btnBorrar)
        Panel1.Controls.Add(btnModificar)
        Panel1.Controls.Add(btnAgregar)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(4, 689)
        Panel1.Margin = New Padding(4, 3, 4, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(856, 80)
        Panel1.TabIndex = 7
        ' 
        ' btnSalir
        ' 
        btnSalir.BackColor = Color.IndianRed
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(770, 3)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 35)
        btnSalir.TabIndex = 5
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnCancelar
        ' 
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCancelar.Location = New Point(674, 3)
        btnCancelar.Margin = New Padding(4, 3, 4, 3)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(88, 35)
        btnCancelar.TabIndex = 4
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = True
        ' 
        ' btnAceptar
        ' 
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAceptar.Location = New Point(578, 3)
        btnAceptar.Margin = New Padding(4, 3, 4, 3)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New Size(88, 35)
        btnAceptar.TabIndex = 3
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = True
        ' 
        ' btnBorrar
        ' 
        btnBorrar.FlatStyle = FlatStyle.Flat
        btnBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBorrar.Location = New Point(204, 12)
        btnBorrar.Margin = New Padding(4, 3, 4, 3)
        btnBorrar.Name = "btnBorrar"
        btnBorrar.Size = New Size(88, 35)
        btnBorrar.TabIndex = 2
        btnBorrar.Text = "Borrar"
        btnBorrar.UseVisualStyleBackColor = True
        ' 
        ' btnModificar
        ' 
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnModificar.Location = New Point(108, 12)
        btnModificar.Margin = New Padding(4, 3, 4, 3)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(88, 35)
        btnModificar.TabIndex = 1
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = True
        ' 
        ' btnAgregar
        ' 
        btnAgregar.FlatStyle = FlatStyle.Flat
        btnAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregar.Location = New Point(12, 12)
        btnAgregar.Margin = New Padding(4, 3, 4, 3)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New Size(88, 35)
        btnAgregar.TabIndex = 0
        btnAgregar.Text = "Agregar"
        btnAgregar.UseVisualStyleBackColor = True
        ' 
        ' pctFoto
        ' 
        pctFoto.BorderStyle = BorderStyle.FixedSingle
        pctFoto.Location = New Point(867, 250)
        pctFoto.Name = "pctFoto"
        pctFoto.Size = New Size(183, 236)
        pctFoto.SizeMode = PictureBoxSizeMode.StretchImage
        pctFoto.TabIndex = 10
        pctFoto.TabStop = False
        ' 
        ' frmAgentes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1069, 772)
        Controls.Add(TableLayoutPanel1)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmAgentes"
        Text = "Actualizaciones - Mantenimiento de Agentes"
        TableLayoutPanel1.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        Panel1Col.ResumeLayout(False)
        Panel1Col.PerformLayout()
        Panel2Col.ResumeLayout(False)
        Panel2Col.PerformLayout()
        Panel3Col.ResumeLayout(False)
        Panel3Col.PerformLayout()
        TabPage2.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(DgvGrupoFamiliar, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        CType(DgvComentarios, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        CType(pctFoto, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents ChkTodos As CheckBox
    Friend WithEvents ChkActivos As CheckBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents optActivos As RadioButton
    Friend WithEvents opTodos As RadioButton
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents Label44 As Label
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel1Col As Panel
    Friend WithEvents txtBaja As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents cmbCaracter As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtLegajo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbInstituto As ComboBox
    Friend WithEvents txtIngreso As TextBox
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
    Friend WithEvents txtNro As TextBox
    Friend WithEvents Label8 As Label
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
    Friend WithEvents Label33 As Label
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
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents DgvGrupoFamiliar As DataGridView
    Friend WithEvents btnEliminarFamiliar As Button
    Friend WithEvents btnAgregarFamiliar As Button
    Friend WithEvents txtNivelEstudio As TextBox
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
    Friend WithEvents TabPage3 As TabPage
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnAceptar As Button
    Friend WithEvents btnBorrar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents pctFoto As PictureBox

End Class
