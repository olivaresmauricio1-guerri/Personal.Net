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
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        GroupBox1 = New GroupBox()
        CmbMotivo = New ComboBox()
        Label45 = New Label()
        txtFechaJubilacion = New TextBox()
        Label30 = New Label()
        cmbEstadoParental = New ComboBox()
        Label29 = New Label()
        txtTitulo = New TextBox()
        Label27 = New Label()
        txtCUIL = New TextBox()
        Label26 = New Label()
        txtBaja = New TextBox()
        Label25 = New Label()
        txtIngreso = New TextBox()
        Label24 = New Label()
        txtUltimaActualizacion = New TextBox()
        Label23 = New Label()
        txtCelular = New TextBox()
        Label22 = New Label()
        txtInterno = New TextBox()
        Label21 = New Label()
        txtTelefono = New TextBox()
        Label20 = New Label()
        cmbCategoria = New ComboBox()
        Label19 = New Label()
        chkNomarca = New CheckBox()
        txtComentario = New TextBox()
        Label18 = New Label()
        cmbCaracter = New ComboBox()
        Label17 = New Label()
        txtLicAnual = New TextBox()
        Label16 = New Label()
        cmbJefe = New ComboBox()
        Label15 = New Label()
        cmbEscalafon = New ComboBox()
        Label14 = New Label()
        cmbHorasDiarias = New ComboBox()
        Label13 = New Label()
        txtUrgencias = New TextBox()
        Label11 = New Label()
        txtOficina = New TextBox()
        Label10 = New Label()
        txtLocalidad = New TextBox()
        Label9 = New Label()
        txtNro = New TextBox()
        Label8 = New Label()
        txtCalle = New TextBox()
        Label7 = New Label()
        dtpNacimiento = New DateTimePicker()
        Label6 = New Label()
        cmbSexo = New ComboBox()
        Label5 = New Label()
        txtCorreoE = New TextBox()
        Label4 = New Label()
        txtCargo = New TextBox()
        Label31 = New Label()
        txtNroDto = New TextBox()
        Label33 = New Label()
        cmbTipoDto = New ComboBox()
        Label34 = New Label()
        cmbInstituto = New ComboBox()
        Label3 = New Label()
        txtNombre = New TextBox()
        Label2 = New Label()
        txtLegajo = New TextBox()
        Label1 = New Label()
        TabPage2 = New TabPage()
        GroupBox2 = New GroupBox()
        DgvGrupoFamiliar = New DataGridView()
        btnEliminarFamiliar = New Button()
        btnAgregarFamiliar = New Button()
        txtNivelFamiliar = New TextBox()
        Label40 = New Label()
        txtOcupacionFamiliar = New TextBox()
        Label39 = New Label()
        txtEdadFamiliar = New TextBox()
        Label38 = New Label()
        dtpNacimientoFamiliar = New DateTimePicker()
        Label37 = New Label()
        txtParentescoFamiliar = New TextBox()
        Label36 = New Label()
        txtNombreFamiliar = New TextBox()
        Label35 = New Label()
        cmbParentesco = New ComboBox()
        TabPage3 = New TabPage()
        GroupBox3 = New GroupBox()
        DgvComentarios = New DataGridView()
        btnEliminarComentario = New Button()
        btnAgregarComentario = New Button()
        txtMotivoComentario = New TextBox()
        cmbMotivoComentario = New ComboBox()
        Label43 = New Label()
        txtComentaComentario = New TextBox()
        Label42 = New Label()
        dtpFechaComentario = New DateTimePicker()
        Label41 = New Label()
        Panel1 = New Panel()
        btnSalir = New Button()
        btnCancelar = New Button()
        btnAceptar = New Button()
        btnBorrar = New Button()
        btnModificar = New Button()
        btnAgregar = New Button()
        Panel2 = New Panel()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        TxtBuscar = New TextBox()
        Label44 = New Label()
        DgvListado = New DataGridView()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        GroupBox1.SuspendLayout()
        TabPage2.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DgvGrupoFamiliar, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(DgvComentarios, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Location = New Point(14, 14)
        TabControl1.Margin = New Padding(4, 3, 4, 3)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(933, 691)
        TabControl1.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(GroupBox1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Margin = New Padding(4, 3, 4, 3)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(4, 3, 4, 3)
        TabPage1.Size = New Size(925, 663)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Datos del Agente"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CmbMotivo)
        GroupBox1.Controls.Add(Label45)
        GroupBox1.Controls.Add(txtFechaJubilacion)
        GroupBox1.Controls.Add(Label30)
        GroupBox1.Controls.Add(cmbEstadoParental)
        GroupBox1.Controls.Add(Label29)
        GroupBox1.Controls.Add(txtTitulo)
        GroupBox1.Controls.Add(Label27)
        GroupBox1.Controls.Add(txtCUIL)
        GroupBox1.Controls.Add(Label26)
        GroupBox1.Controls.Add(txtBaja)
        GroupBox1.Controls.Add(Label25)
        GroupBox1.Controls.Add(txtIngreso)
        GroupBox1.Controls.Add(Label24)
        GroupBox1.Controls.Add(txtUltimaActualizacion)
        GroupBox1.Controls.Add(Label23)
        GroupBox1.Controls.Add(txtCelular)
        GroupBox1.Controls.Add(Label22)
        GroupBox1.Controls.Add(txtInterno)
        GroupBox1.Controls.Add(Label21)
        GroupBox1.Controls.Add(txtTelefono)
        GroupBox1.Controls.Add(Label20)
        GroupBox1.Controls.Add(cmbCategoria)
        GroupBox1.Controls.Add(Label19)
        GroupBox1.Controls.Add(chkNomarca)
        GroupBox1.Controls.Add(txtComentario)
        GroupBox1.Controls.Add(Label18)
        GroupBox1.Controls.Add(cmbCaracter)
        GroupBox1.Controls.Add(Label17)
        GroupBox1.Controls.Add(txtLicAnual)
        GroupBox1.Controls.Add(Label16)
        GroupBox1.Controls.Add(cmbJefe)
        GroupBox1.Controls.Add(Label15)
        GroupBox1.Controls.Add(cmbEscalafon)
        GroupBox1.Controls.Add(Label14)
        GroupBox1.Controls.Add(cmbHorasDiarias)
        GroupBox1.Controls.Add(Label13)
        GroupBox1.Controls.Add(txtUrgencias)
        GroupBox1.Controls.Add(Label11)
        GroupBox1.Controls.Add(txtOficina)
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(txtLocalidad)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(txtNro)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(txtCalle)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(dtpNacimiento)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(cmbSexo)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(txtCorreoE)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(txtCargo)
        GroupBox1.Controls.Add(Label31)
        GroupBox1.Controls.Add(txtNroDto)
        GroupBox1.Controls.Add(Label33)
        GroupBox1.Controls.Add(cmbTipoDto)
        GroupBox1.Controls.Add(Label34)
        GroupBox1.Controls.Add(cmbInstituto)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(txtNombre)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(txtLegajo)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(4, 3)
        GroupBox1.Margin = New Padding(4, 3, 4, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4, 3, 4, 3)
        GroupBox1.Size = New Size(917, 657)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        ' 
        ' CmbMotivo
        ' 
        CmbMotivo.FormattingEnabled = True
        CmbMotivo.Location = New Point(418, 281)
        CmbMotivo.Margin = New Padding(4, 3, 4, 3)
        CmbMotivo.Name = "CmbMotivo"
        CmbMotivo.Size = New Size(209, 23)
        CmbMotivo.TabIndex = 69
        ' 
        ' Label45
        ' 
        Label45.AutoSize = True
        Label45.Location = New Point(322, 285)
        Label45.Margin = New Padding(4, 0, 4, 0)
        Label45.Name = "Label45"
        Label45.Size = New Size(44, 15)
        Label45.TabIndex = 68
        Label45.Text = "Moivo:"
        ' 
        ' txtFechaJubilacion
        ' 
        txtFechaJubilacion.Location = New Point(758, 283)
        txtFechaJubilacion.Margin = New Padding(4, 3, 4, 3)
        txtFechaJubilacion.Name = "txtFechaJubilacion"
        txtFechaJubilacion.Size = New Size(143, 23)
        txtFechaJubilacion.TabIndex = 66
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(641, 286)
        Label30.Margin = New Padding(4, 0, 4, 0)
        Label30.Name = "Label30"
        Label30.Size = New Size(97, 15)
        Label30.TabIndex = 65
        Label30.Text = "Fecha Jubilación:"
        ' 
        ' cmbEstadoParental
        ' 
        cmbEstadoParental.FormattingEnabled = True
        cmbEstadoParental.Location = New Point(418, 134)
        cmbEstadoParental.Margin = New Padding(4, 3, 4, 3)
        cmbEstadoParental.Name = "cmbEstadoParental"
        cmbEstadoParental.Size = New Size(139, 23)
        cmbEstadoParental.TabIndex = 64
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(322, 135)
        Label29.Margin = New Padding(4, 0, 4, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(91, 15)
        Label29.TabIndex = 63
        Label29.Text = "Estado Parental:"
        ' 
        ' txtTitulo
        ' 
        txtTitulo.Location = New Point(758, 135)
        txtTitulo.Margin = New Padding(4, 3, 4, 3)
        txtTitulo.Name = "txtTitulo"
        txtTitulo.Size = New Size(143, 23)
        txtTitulo.TabIndex = 60
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(641, 138)
        Label27.Margin = New Padding(4, 0, 4, 0)
        Label27.Name = "Label27"
        Label27.Size = New Size(41, 15)
        Label27.TabIndex = 59
        Label27.Text = "Título:"
        ' 
        ' txtCUIL
        ' 
        txtCUIL.Location = New Point(758, 47)
        txtCUIL.Margin = New Padding(4, 3, 4, 3)
        txtCUIL.Name = "txtCUIL"
        txtCUIL.Size = New Size(139, 23)
        txtCUIL.TabIndex = 58
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(641, 51)
        Label26.Margin = New Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(35, 15)
        Label26.TabIndex = 57
        Label26.Text = "CUIL:"
        ' 
        ' txtBaja
        ' 
        txtBaja.Location = New Point(93, 276)
        txtBaja.Margin = New Padding(4, 3, 4, 3)
        txtBaja.Name = "txtBaja"
        txtBaja.Size = New Size(139, 23)
        txtBaja.TabIndex = 56
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(12, 279)
        Label25.Margin = New Padding(4, 0, 4, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(32, 15)
        Label25.TabIndex = 55
        Label25.Text = "Baja:"
        ' 
        ' txtIngreso
        ' 
        txtIngreso.Location = New Point(758, 224)
        txtIngreso.Margin = New Padding(4, 3, 4, 3)
        txtIngreso.Name = "txtIngreso"
        txtIngreso.Size = New Size(143, 23)
        txtIngreso.TabIndex = 54
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(641, 227)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(49, 15)
        Label24.TabIndex = 53
        Label24.Text = "Ingreso:"
        ' 
        ' txtUltimaActualizacion
        ' 
        txtUltimaActualizacion.Location = New Point(765, 254)
        txtUltimaActualizacion.Margin = New Padding(4, 3, 4, 3)
        txtUltimaActualizacion.Name = "txtUltimaActualizacion"
        txtUltimaActualizacion.Size = New Size(136, 23)
        txtUltimaActualizacion.TabIndex = 52
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(641, 258)
        Label23.Margin = New Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(119, 15)
        Label23.TabIndex = 51
        Label23.Text = "Última Actualización:"
        ' 
        ' txtCelular
        ' 
        txtCelular.Location = New Point(758, 106)
        txtCelular.Margin = New Padding(4, 3, 4, 3)
        txtCelular.Name = "txtCelular"
        txtCelular.Size = New Size(143, 23)
        txtCelular.TabIndex = 50
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(641, 109)
        Label22.Margin = New Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(47, 15)
        Label22.TabIndex = 49
        Label22.Text = "Celular:"
        ' 
        ' txtInterno
        ' 
        txtInterno.Location = New Point(93, 190)
        txtInterno.Margin = New Padding(4, 3, 4, 3)
        txtInterno.Name = "txtInterno"
        txtInterno.Size = New Size(139, 23)
        txtInterno.TabIndex = 48
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(12, 188)
        Label21.Margin = New Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(48, 15)
        Label21.TabIndex = 47
        Label21.Text = "Interno:"
        ' 
        ' txtTelefono
        ' 
        txtTelefono.Location = New Point(418, 105)
        txtTelefono.Margin = New Padding(4, 3, 4, 3)
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(139, 23)
        txtTelefono.TabIndex = 46
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(322, 106)
        Label20.Margin = New Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(56, 15)
        Label20.TabIndex = 45
        Label20.Text = "Teléfono:"
        ' 
        ' cmbCategoria
        ' 
        cmbCategoria.FormattingEnabled = True
        cmbCategoria.Location = New Point(758, 166)
        cmbCategoria.Margin = New Padding(4, 3, 4, 3)
        cmbCategoria.Name = "cmbCategoria"
        cmbCategoria.Size = New Size(143, 23)
        cmbCategoria.TabIndex = 44
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(641, 170)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(61, 15)
        Label19.TabIndex = 43
        Label19.Text = "Categoría:"
        ' 
        ' chkNomarca
        ' 
        chkNomarca.AutoSize = True
        chkNomarca.Location = New Point(418, 349)
        chkNomarca.Margin = New Padding(4, 3, 4, 3)
        chkNomarca.Name = "chkNomarca"
        chkNomarca.Size = New Size(78, 19)
        chkNomarca.TabIndex = 42
        chkNomarca.Text = "No marca"
        chkNomarca.UseVisualStyleBackColor = True
        ' 
        ' txtComentario
        ' 
        txtComentario.Location = New Point(93, 305)
        txtComentario.Margin = New Padding(4, 3, 4, 3)
        txtComentario.Multiline = True
        txtComentario.Name = "txtComentario"
        txtComentario.ScrollBars = ScrollBars.Both
        txtComentario.Size = New Size(209, 120)
        txtComentario.TabIndex = 41
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(12, 309)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(73, 15)
        Label18.TabIndex = 40
        Label18.Text = "Comentario:"
        ' 
        ' cmbCaracter
        ' 
        cmbCaracter.FormattingEnabled = True
        cmbCaracter.Location = New Point(418, 252)
        cmbCaracter.Margin = New Padding(4, 3, 4, 3)
        cmbCaracter.Name = "cmbCaracter"
        cmbCaracter.Size = New Size(209, 23)
        cmbCaracter.TabIndex = 39
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(322, 255)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(54, 15)
        Label17.TabIndex = 38
        Label17.Text = "Carácter:"
        ' 
        ' txtLicAnual
        ' 
        txtLicAnual.Location = New Point(418, 223)
        txtLicAnual.Margin = New Padding(4, 3, 4, 3)
        txtLicAnual.Name = "txtLicAnual"
        txtLicAnual.Size = New Size(209, 23)
        txtLicAnual.TabIndex = 37
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(322, 226)
        Label16.Margin = New Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(62, 15)
        Label16.TabIndex = 36
        Label16.Text = "Lic. Anual:"
        ' 
        ' cmbJefe
        ' 
        cmbJefe.FormattingEnabled = True
        cmbJefe.Location = New Point(418, 194)
        cmbJefe.Margin = New Padding(4, 3, 4, 3)
        cmbJefe.Name = "cmbJefe"
        cmbJefe.Size = New Size(209, 23)
        cmbJefe.TabIndex = 35
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(322, 197)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(30, 15)
        Label15.TabIndex = 34
        Label15.Text = "Jefe:"
        ' 
        ' cmbEscalafon
        ' 
        cmbEscalafon.FormattingEnabled = True
        cmbEscalafon.Location = New Point(418, 165)
        cmbEscalafon.Margin = New Padding(4, 3, 4, 3)
        cmbEscalafon.Name = "cmbEscalafon"
        cmbEscalafon.Size = New Size(209, 23)
        cmbEscalafon.TabIndex = 33
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(322, 168)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(60, 15)
        Label14.TabIndex = 32
        Label14.Text = "Escalafón:"
        ' 
        ' cmbHorasDiarias
        ' 
        cmbHorasDiarias.FormattingEnabled = True
        cmbHorasDiarias.Location = New Point(418, 310)
        cmbHorasDiarias.Margin = New Padding(4, 3, 4, 3)
        cmbHorasDiarias.Name = "cmbHorasDiarias"
        cmbHorasDiarias.Size = New Size(209, 23)
        cmbHorasDiarias.TabIndex = 31
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(322, 313)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(79, 15)
        Label13.TabIndex = 30
        Label13.Text = "Horas Diarias:"
        ' 
        ' txtUrgencias
        ' 
        txtUrgencias.Location = New Point(93, 132)
        txtUrgencias.Margin = New Padding(4, 3, 4, 3)
        txtUrgencias.Name = "txtUrgencias"
        txtUrgencias.Size = New Size(209, 23)
        txtUrgencias.TabIndex = 27
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(12, 134)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(59, 15)
        Label11.TabIndex = 26
        Label11.Text = "Urgencias"
        ' 
        ' txtOficina
        ' 
        txtOficina.Location = New Point(93, 247)
        txtOficina.Margin = New Padding(4, 3, 4, 3)
        txtOficina.Name = "txtOficina"
        txtOficina.Size = New Size(209, 23)
        txtOficina.TabIndex = 23
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(12, 250)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(48, 15)
        Label10.TabIndex = 22
        Label10.Text = "Oficina:"
        ' 
        ' txtLocalidad
        ' 
        txtLocalidad.Location = New Point(93, 102)
        txtLocalidad.Margin = New Padding(4, 3, 4, 3)
        txtLocalidad.Name = "txtLocalidad"
        txtLocalidad.Size = New Size(209, 23)
        txtLocalidad.TabIndex = 21
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(12, 106)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(61, 15)
        Label9.TabIndex = 20
        Label9.Text = "Localidad:"
        ' 
        ' txtNro
        ' 
        txtNro.Location = New Point(758, 77)
        txtNro.Margin = New Padding(4, 3, 4, 3)
        txtNro.Name = "txtNro"
        txtNro.Size = New Size(143, 23)
        txtNro.TabIndex = 19
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(641, 80)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(30, 15)
        Label8.TabIndex = 18
        Label8.Text = "Nro:"
        ' 
        ' txtCalle
        ' 
        txtCalle.Location = New Point(418, 76)
        txtCalle.Margin = New Padding(4, 3, 4, 3)
        txtCalle.Name = "txtCalle"
        txtCalle.Size = New Size(209, 23)
        txtCalle.TabIndex = 17
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(322, 78)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(36, 15)
        Label7.TabIndex = 16
        Label7.Text = "Calle:"
        ' 
        ' dtpNacimiento
        ' 
        dtpNacimiento.Format = DateTimePickerFormat.Short
        dtpNacimiento.Location = New Point(762, 18)
        dtpNacimiento.Margin = New Padding(4, 3, 4, 3)
        dtpNacimiento.Name = "dtpNacimiento"
        dtpNacimiento.Size = New Size(135, 23)
        dtpNacimiento.TabIndex = 15
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(641, 22)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(72, 15)
        Label6.TabIndex = 14
        Label6.Text = "Nacimiento:"
        ' 
        ' cmbSexo
        ' 
        cmbSexo.FormattingEnabled = True
        cmbSexo.Items.AddRange(New Object() {"M", "F"})
        cmbSexo.Location = New Point(93, 73)
        cmbSexo.Margin = New Padding(4, 3, 4, 3)
        cmbSexo.Name = "cmbSexo"
        cmbSexo.Size = New Size(96, 23)
        cmbSexo.TabIndex = 13
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(12, 76)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(34, 15)
        Label5.TabIndex = 12
        Label5.Text = "Sexo:"
        ' 
        ' txtCorreoE
        ' 
        txtCorreoE.Location = New Point(93, 161)
        txtCorreoE.Margin = New Padding(4, 3, 4, 3)
        txtCorreoE.Name = "txtCorreoE"
        txtCorreoE.Size = New Size(209, 23)
        txtCorreoE.TabIndex = 11
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 164)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(57, 15)
        Label4.TabIndex = 10
        Label4.Text = "Correo-E:"
        ' 
        ' txtCargo
        ' 
        txtCargo.Location = New Point(93, 218)
        txtCargo.Margin = New Padding(4, 3, 4, 3)
        txtCargo.Name = "txtCargo"
        txtCargo.Size = New Size(209, 23)
        txtCargo.TabIndex = 9
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(12, 217)
        Label31.Margin = New Padding(4, 0, 4, 0)
        Label31.Name = "Label31"
        Label31.Size = New Size(42, 15)
        Label31.TabIndex = 8
        Label31.Text = "Cargo:"
        ' 
        ' txtNroDto
        ' 
        txtNroDto.Location = New Point(418, 47)
        txtNroDto.Margin = New Padding(4, 3, 4, 3)
        txtNroDto.Name = "txtNroDto"
        txtNroDto.Size = New Size(209, 23)
        txtNroDto.TabIndex = 5
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(322, 49)
        Label33.Margin = New Padding(4, 0, 4, 0)
        Label33.Name = "Label33"
        Label33.Size = New Size(52, 15)
        Label33.TabIndex = 4
        Label33.Text = "Nro Dto:"
        ' 
        ' cmbTipoDto
        ' 
        cmbTipoDto.FormattingEnabled = True
        cmbTipoDto.Items.AddRange(New Object() {"DNI", "LC", "LE", "CI", "Pasaporte"})
        cmbTipoDto.Location = New Point(93, 44)
        cmbTipoDto.Margin = New Padding(4, 3, 4, 3)
        cmbTipoDto.Name = "cmbTipoDto"
        cmbTipoDto.Size = New Size(96, 23)
        cmbTipoDto.TabIndex = 3
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(12, 47)
        Label34.Margin = New Padding(4, 0, 4, 0)
        Label34.Name = "Label34"
        Label34.Size = New Size(56, 15)
        Label34.TabIndex = 2
        Label34.Text = "Tipo Dto:"
        ' 
        ' cmbInstituto
        ' 
        cmbInstituto.FormattingEnabled = True
        cmbInstituto.Location = New Point(758, 195)
        cmbInstituto.Margin = New Padding(4, 3, 4, 3)
        cmbInstituto.Name = "cmbInstituto"
        cmbInstituto.Size = New Size(143, 23)
        cmbInstituto.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(641, 199)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(54, 15)
        Label3.TabIndex = 0
        Label3.Text = "Instituto:"
        ' 
        ' txtNombre
        ' 
        txtNombre.Location = New Point(418, 16)
        txtNombre.Margin = New Padding(4, 3, 4, 3)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(209, 23)
        txtNombre.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(322, 17)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(54, 15)
        Label2.TabIndex = 0
        Label2.Text = "Nombre:"
        ' 
        ' txtLegajo
        ' 
        txtLegajo.Location = New Point(93, 15)
        txtLegajo.Margin = New Padding(4, 3, 4, 3)
        txtLegajo.Name = "txtLegajo"
        txtLegajo.Size = New Size(96, 23)
        txtLegajo.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 18)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 0
        Label1.Text = "Legajo:"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(GroupBox2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Margin = New Padding(4, 3, 4, 3)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(4, 3, 4, 3)
        TabPage2.Size = New Size(925, 663)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Grupo Familiar"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(DgvGrupoFamiliar)
        GroupBox2.Controls.Add(btnEliminarFamiliar)
        GroupBox2.Controls.Add(btnAgregarFamiliar)
        GroupBox2.Controls.Add(txtNivelFamiliar)
        GroupBox2.Controls.Add(Label40)
        GroupBox2.Controls.Add(txtOcupacionFamiliar)
        GroupBox2.Controls.Add(Label39)
        GroupBox2.Controls.Add(txtEdadFamiliar)
        GroupBox2.Controls.Add(Label38)
        GroupBox2.Controls.Add(dtpNacimientoFamiliar)
        GroupBox2.Controls.Add(Label37)
        GroupBox2.Controls.Add(txtParentescoFamiliar)
        GroupBox2.Controls.Add(Label36)
        GroupBox2.Controls.Add(txtNombreFamiliar)
        GroupBox2.Controls.Add(Label35)
        GroupBox2.Controls.Add(cmbParentesco)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(4, 3)
        GroupBox2.Margin = New Padding(4, 3, 4, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(4, 3, 4, 3)
        GroupBox2.Size = New Size(917, 657)
        GroupBox2.TabIndex = 0
        GroupBox2.TabStop = False
        GroupBox2.Text = "Información del Grupo Familiar"
        ' 
        ' DgvGrupoFamiliar
        ' 
        DgvGrupoFamiliar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvGrupoFamiliar.Location = New Point(12, 138)
        DgvGrupoFamiliar.Margin = New Padding(4, 3, 4, 3)
        DgvGrupoFamiliar.Name = "DgvGrupoFamiliar"
        DgvGrupoFamiliar.Size = New Size(887, 277)
        DgvGrupoFamiliar.TabIndex = 14
        ' 
        ' btnEliminarFamiliar
        ' 
        btnEliminarFamiliar.Location = New Point(758, 92)
        btnEliminarFamiliar.Margin = New Padding(4, 3, 4, 3)
        btnEliminarFamiliar.Name = "btnEliminarFamiliar"
        btnEliminarFamiliar.Size = New Size(88, 27)
        btnEliminarFamiliar.TabIndex = 13
        btnEliminarFamiliar.Text = "Eliminar"
        btnEliminarFamiliar.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarFamiliar
        ' 
        btnAgregarFamiliar.Location = New Point(642, 92)
        btnAgregarFamiliar.Margin = New Padding(4, 3, 4, 3)
        btnAgregarFamiliar.Name = "btnAgregarFamiliar"
        btnAgregarFamiliar.Size = New Size(88, 27)
        btnAgregarFamiliar.TabIndex = 12
        btnAgregarFamiliar.Text = "Agregar"
        btnAgregarFamiliar.UseVisualStyleBackColor = True
        ' 
        ' txtNivelFamiliar
        ' 
        txtNivelFamiliar.Location = New Point(642, 58)
        txtNivelFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtNivelFamiliar.Name = "txtNivelFamiliar"
        txtNivelFamiliar.Size = New Size(209, 23)
        txtNivelFamiliar.TabIndex = 11
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(560, 61)
        Label40.Margin = New Padding(4, 0, 4, 0)
        Label40.Name = "Label40"
        Label40.Size = New Size(37, 15)
        Label40.TabIndex = 10
        Label40.Text = "Nivel:"
        ' 
        ' txtOcupacionFamiliar
        ' 
        txtOcupacionFamiliar.Location = New Point(642, 23)
        txtOcupacionFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtOcupacionFamiliar.Name = "txtOcupacionFamiliar"
        txtOcupacionFamiliar.Size = New Size(209, 23)
        txtOcupacionFamiliar.TabIndex = 9
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Location = New Point(560, 27)
        Label39.Margin = New Padding(4, 0, 4, 0)
        Label39.Name = "Label39"
        Label39.Size = New Size(68, 15)
        Label39.TabIndex = 8
        Label39.Text = "Ocupación:"
        ' 
        ' txtEdadFamiliar
        ' 
        txtEdadFamiliar.Location = New Point(327, 92)
        txtEdadFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtEdadFamiliar.Name = "txtEdadFamiliar"
        txtEdadFamiliar.Size = New Size(209, 23)
        txtEdadFamiliar.TabIndex = 7
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Location = New Point(245, 96)
        Label38.Margin = New Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New Size(36, 15)
        Label38.TabIndex = 6
        Label38.Text = "Edad:"
        ' 
        ' dtpNacimientoFamiliar
        ' 
        dtpNacimientoFamiliar.Format = DateTimePickerFormat.Short
        dtpNacimientoFamiliar.Location = New Point(327, 58)
        dtpNacimientoFamiliar.Margin = New Padding(4, 3, 4, 3)
        dtpNacimientoFamiliar.Name = "dtpNacimientoFamiliar"
        dtpNacimientoFamiliar.Size = New Size(209, 23)
        dtpNacimientoFamiliar.TabIndex = 5
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(245, 61)
        Label37.Margin = New Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New Size(72, 15)
        Label37.TabIndex = 4
        Label37.Text = "Nacimiento:"
        ' 
        ' txtParentescoFamiliar
        ' 
        txtParentescoFamiliar.Location = New Point(327, 23)
        txtParentescoFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtParentescoFamiliar.Name = "txtParentescoFamiliar"
        txtParentescoFamiliar.Size = New Size(209, 23)
        txtParentescoFamiliar.TabIndex = 3
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(245, 27)
        Label36.Margin = New Padding(4, 0, 4, 0)
        Label36.Name = "Label36"
        Label36.Size = New Size(68, 15)
        Label36.TabIndex = 2
        Label36.Text = "Parentesco:"
        ' 
        ' txtNombreFamiliar
        ' 
        txtNombreFamiliar.Location = New Point(93, 23)
        txtNombreFamiliar.Margin = New Padding(4, 3, 4, 3)
        txtNombreFamiliar.Name = "txtNombreFamiliar"
        txtNombreFamiliar.Size = New Size(139, 23)
        txtNombreFamiliar.TabIndex = 1
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(12, 27)
        Label35.Margin = New Padding(4, 0, 4, 0)
        Label35.Name = "Label35"
        Label35.Size = New Size(54, 15)
        Label35.TabIndex = 0
        Label35.Text = "Nombre:"
        ' 
        ' cmbParentesco
        ' 
        cmbParentesco.DropDownStyle = ComboBoxStyle.DropDownList
        cmbParentesco.Location = New Point(12, 58)
        cmbParentesco.Margin = New Padding(4, 3, 4, 3)
        cmbParentesco.Name = "cmbParentesco"
        cmbParentesco.Size = New Size(233, 23)
        cmbParentesco.TabIndex = 2
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(GroupBox3)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Margin = New Padding(4, 3, 4, 3)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(4, 3, 4, 3)
        TabPage3.Size = New Size(925, 663)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Comentarios"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(DgvComentarios)
        GroupBox3.Controls.Add(btnEliminarComentario)
        GroupBox3.Controls.Add(btnAgregarComentario)
        GroupBox3.Controls.Add(txtMotivoComentario)
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
        GroupBox3.Size = New Size(917, 657)
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
        DgvComentarios.Size = New Size(887, 277)
        DgvComentarios.TabIndex = 8
        ' 
        ' btnEliminarComentario
        ' 
        btnEliminarComentario.Location = New Point(758, 92)
        btnEliminarComentario.Margin = New Padding(4, 3, 4, 3)
        btnEliminarComentario.Name = "btnEliminarComentario"
        btnEliminarComentario.Size = New Size(88, 27)
        btnEliminarComentario.TabIndex = 7
        btnEliminarComentario.Text = "Eliminar"
        btnEliminarComentario.UseVisualStyleBackColor = True
        ' 
        ' btnAgregarComentario
        ' 
        btnAgregarComentario.Location = New Point(642, 92)
        btnAgregarComentario.Margin = New Padding(4, 3, 4, 3)
        btnAgregarComentario.Name = "btnAgregarComentario"
        btnAgregarComentario.Size = New Size(88, 27)
        btnAgregarComentario.TabIndex = 6
        btnAgregarComentario.Text = "Agregar"
        btnAgregarComentario.UseVisualStyleBackColor = True
        ' 
        ' txtMotivoComentario
        ' 
        txtMotivoComentario.Location = New Point(408, 58)
        txtMotivoComentario.Margin = New Padding(4, 3, 4, 3)
        txtMotivoComentario.Name = "txtMotivoComentario"
        txtMotivoComentario.Size = New Size(443, 23)
        txtMotivoComentario.TabIndex = 5
        ' 
        ' cmbMotivoComentario
        ' 
        cmbMotivoComentario.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMotivoComentario.Location = New Point(408, 92)
        cmbMotivoComentario.Margin = New Padding(4, 3, 4, 3)
        cmbMotivoComentario.Name = "cmbMotivoComentario"
        cmbMotivoComentario.Size = New Size(443, 23)
        cmbMotivoComentario.TabIndex = 6
        ' 
        ' Label43
        ' 
        Label43.AutoSize = True
        Label43.Location = New Point(327, 61)
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
        Label42.Location = New Point(12, 61)
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
        Label41.Location = New Point(12, 27)
        Label41.Margin = New Padding(4, 0, 4, 0)
        Label41.Name = "Label41"
        Label41.Size = New Size(41, 15)
        Label41.TabIndex = 0
        Label41.Text = "Fecha:"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(btnSalir)
        Panel1.Controls.Add(btnCancelar)
        Panel1.Controls.Add(btnAceptar)
        Panel1.Controls.Add(btnBorrar)
        Panel1.Controls.Add(btnModificar)
        Panel1.Controls.Add(btnAgregar)
        Panel1.Location = New Point(6, 711)
        Panel1.Margin = New Padding(4, 3, 4, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(933, 58)
        Panel1.TabIndex = 1
        ' 
        ' btnSalir
        ' 
        btnSalir.Location = New Point(817, 12)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 35)
        btnSalir.TabIndex = 5
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = True
        ' 
        ' btnCancelar
        ' 
        btnCancelar.Location = New Point(700, 12)
        btnCancelar.Margin = New Padding(4, 3, 4, 3)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(88, 35)
        btnCancelar.TabIndex = 4
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = True
        ' 
        ' btnAceptar
        ' 
        btnAceptar.Location = New Point(583, 12)
        btnAceptar.Margin = New Padding(4, 3, 4, 3)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New Size(88, 35)
        btnAceptar.TabIndex = 3
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = True
        ' 
        ' btnBorrar
        ' 
        btnBorrar.Location = New Point(233, 12)
        btnBorrar.Margin = New Padding(4, 3, 4, 3)
        btnBorrar.Name = "btnBorrar"
        btnBorrar.Size = New Size(88, 35)
        btnBorrar.TabIndex = 2
        btnBorrar.Text = "Borrar"
        btnBorrar.UseVisualStyleBackColor = True
        ' 
        ' btnModificar
        ' 
        btnModificar.Location = New Point(117, 12)
        btnModificar.Margin = New Padding(4, 3, 4, 3)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(88, 35)
        btnModificar.TabIndex = 1
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = True
        ' 
        ' btnAgregar
        ' 
        btnAgregar.Location = New Point(12, 12)
        btnAgregar.Margin = New Padding(4, 3, 4, 3)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New Size(88, 35)
        btnAgregar.TabIndex = 0
        btnAgregar.Text = "Agregar"
        btnAgregar.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(lnkCopiar)
        Panel2.Controls.Add(chkEncabezados)
        Panel2.Controls.Add(TxtBuscar)
        Panel2.Controls.Add(Label44)
        Panel2.Controls.Add(DgvListado)
        Panel2.Location = New Point(957, 14)
        Panel2.Margin = New Padding(4, 3, 4, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(467, 528)
        Panel2.TabIndex = 2
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.Location = New Point(373, 46)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(42, 15)
        lnkCopiar.TabIndex = 4
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar"
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(257, 46)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(94, 19)
        chkEncabezados.TabIndex = 3
        chkEncabezados.Text = "Encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(93, 12)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(349, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.Location = New Point(12, 15)
        Label44.Margin = New Padding(4, 0, 4, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(45, 15)
        Label44.TabIndex = 0
        Label44.Text = "Buscar:"
        ' 
        ' DgvListado
        ' 
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(12, 81)
        DgvListado.Margin = New Padding(4, 3, 4, 3)
        DgvListado.Name = "DgvListado"
        DgvListado.Size = New Size(443, 438)
        DgvListado.TabIndex = 2
        ' 
        ' frmAgentes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1435, 781)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Controls.Add(TabControl1)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmAgentes"
        Text = "Actualizaciones - Mantenimiento de Agentes"
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        TabPage2.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(DgvGrupoFamiliar, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        CType(DgvComentarios, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtLegajo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbInstituto As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCorreoE As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbSexo As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpNacimiento As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCalle As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtNro As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtLocalidad As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtOficina As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtUrgencias As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cmbHorasDiarias As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cmbEscalafon As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents cmbJefe As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtLicAnual As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbCaracter As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents cmbCategoria As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txtInterno As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents txtCelular As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtUltimaActualizacion As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents txtIngreso As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txtBaja As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents txtCUIL As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents txtTitulo As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents cmbEstadoParental As ComboBox
    Friend WithEvents Label29 As Label
    Friend WithEvents txtFechaJubilacion As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents txtCargo As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents txtNroDto As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents cmbTipoDto As ComboBox
    Friend WithEvents Label34 As Label
    Friend WithEvents chkNomarca As CheckBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtNombreFamiliar As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents txtParentescoFamiliar As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents dtpNacimientoFamiliar As DateTimePicker
    Friend WithEvents Label37 As Label
    Friend WithEvents txtEdadFamiliar As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents txtOcupacionFamiliar As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents txtNivelFamiliar As TextBox
    Friend WithEvents Label40 As Label
    Friend WithEvents btnAgregarFamiliar As Button
    Friend WithEvents btnEliminarFamiliar As Button
    Friend WithEvents DgvGrupoFamiliar As DataGridView
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents dtpFechaComentario As DateTimePicker
    Friend WithEvents Label41 As Label
    Friend WithEvents txtComentaComentario As TextBox
    Friend WithEvents Label42 As Label
    Friend WithEvents txtMotivoComentario As TextBox
    Friend WithEvents cmbMotivoComentario As ComboBox
    Friend WithEvents cmbParentesco As ComboBox
    Friend WithEvents Label43 As Label
    Friend WithEvents btnAgregarComentario As Button
    Friend WithEvents btnEliminarComentario As Button
    Friend WithEvents DgvComentarios As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnAgregar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnBorrar As Button
    Friend WithEvents btnAceptar As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnSalir As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label44 As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents CmbMotivo As ComboBox
    Friend WithEvents Label45 As Label

End Class
