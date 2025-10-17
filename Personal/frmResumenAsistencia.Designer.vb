<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResumenAsistencia
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        CmdSalir = New Button()
        CmdImprimir = New Button()
        CmdInasistencias = New Button()
        DgvListado = New DataGridView()
        DgvInasistencias = New DataGridView()
        TxtLegajo = New TextBox()
        TxtAnio = New TextBox()
        TxtDiasTrabajados = New TextBox()
        TxtPromedio = New TextBox()
        TxtDiasPromedio = New TextBox()
        TxtOficina = New TextBox()
        TxtEncargado = New TextBox()
        TxtCategoria = New TextBox()
        TxtCaracter = New TextBox()
        TxtInstituto = New TextBox()
        TxtHorasContrato = New TextBox()
        TxtObligacionMensual = New TextBox()
        TxtDiasLicencia = New TextBox()
        TxtComentarios = New TextBox()
        CmbMeses = New ComboBox()
        CmbNombres = New ComboBox()
        Label2 = New Label()
        lblDesde = New Label()
        lblHasta = New Label()
        lblAnio = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        Label11 = New Label()
        Label12 = New Label()
        Label13 = New Label()
        LblSaldo = New Label()
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        txtLegajoEventual = New TextBox()
        Label1 = New Label()
        lblLegajo = New Label()
        dtpHasta = New DateTimePicker()
        dtpDesde = New DateTimePicker()
        lblMes = New Label()
        Label23 = New Label()
        Label24 = New Label()
        Label25 = New Label()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        CType(DgvInasistencias, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(711, 542)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 0
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(609, 542)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 1
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.Cursor = Cursors.Hand
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(755, 584)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 2
        CmdSalir.Text = "&Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdImprimir
        ' 
        CmdImprimir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdImprimir.Cursor = Cursors.Hand
        CmdImprimir.FlatStyle = FlatStyle.Flat
        CmdImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdImprimir.Location = New Point(671, 584)
        CmdImprimir.Margin = New Padding(4, 3, 4, 3)
        CmdImprimir.Name = "CmdImprimir"
        CmdImprimir.Size = New Size(75, 30)
        CmdImprimir.TabIndex = 3
        CmdImprimir.Text = "&Imprimir"
        CmdImprimir.UseVisualStyleBackColor = True
        ' 
        ' CmdInasistencias
        ' 
        CmdInasistencias.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdInasistencias.Cursor = Cursors.Hand
        CmdInasistencias.FlatStyle = FlatStyle.Flat
        CmdInasistencias.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdInasistencias.Location = New Point(555, 584)
        CmdInasistencias.Margin = New Padding(4, 3, 4, 3)
        CmdInasistencias.Name = "CmdInasistencias"
        CmdInasistencias.Size = New Size(108, 30)
        CmdInasistencias.TabIndex = 4
        CmdInasistencias.Text = "&Inasistencias"
        CmdInasistencias.UseVisualStyleBackColor = True
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(8, 249)
        DgvListado.Margin = New Padding(4, 3, 4, 3)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(824, 287)
        DgvListado.TabIndex = 8
        ' 
        ' DgvInasistencias
        ' 
        DgvInasistencias.AllowUserToAddRows = False
        DgvInasistencias.AllowUserToDeleteRows = False
        DgvInasistencias.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvInasistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvInasistencias.Location = New Point(159, 272)
        DgvInasistencias.Margin = New Padding(4, 3, 4, 3)
        DgvInasistencias.MultiSelect = False
        DgvInasistencias.Name = "DgvInasistencias"
        DgvInasistencias.ReadOnly = True
        DgvInasistencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvInasistencias.Size = New Size(507, 231)
        DgvInasistencias.TabIndex = 9
        DgvInasistencias.Visible = False
        ' 
        ' TxtLegajo
        ' 
        TxtLegajo.BackColor = SystemColors.Window
        TxtLegajo.Font = New Font("Segoe UI", 9F)
        TxtLegajo.Location = New Point(716, 19)
        TxtLegajo.Margin = New Padding(4, 3, 4, 3)
        TxtLegajo.Name = "TxtLegajo"
        TxtLegajo.Size = New Size(93, 23)
        TxtLegajo.TabIndex = 11
        ' 
        ' TxtAnio
        ' 
        TxtAnio.Font = New Font("Segoe UI", 9F)
        TxtAnio.Location = New Point(560, 48)
        TxtAnio.Margin = New Padding(4, 3, 4, 3)
        TxtAnio.Name = "TxtAnio"
        TxtAnio.Size = New Size(69, 23)
        TxtAnio.TabIndex = 12
        ' 
        ' TxtDiasTrabajados
        ' 
        TxtDiasTrabajados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TxtDiasTrabajados.Font = New Font("Segoe UI", 9F)
        TxtDiasTrabajados.ForeColor = Color.Red
        TxtDiasTrabajados.Location = New Point(354, 560)
        TxtDiasTrabajados.Margin = New Padding(4, 3, 4, 3)
        TxtDiasTrabajados.Name = "TxtDiasTrabajados"
        TxtDiasTrabajados.ReadOnly = True
        TxtDiasTrabajados.Size = New Size(69, 23)
        TxtDiasTrabajados.TabIndex = 15
        TxtDiasTrabajados.TextAlign = HorizontalAlignment.Center
        ' 
        ' TxtPromedio
        ' 
        TxtPromedio.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TxtPromedio.Font = New Font("Segoe UI", 9F)
        TxtPromedio.ForeColor = Color.Red
        TxtPromedio.Location = New Point(354, 592)
        TxtPromedio.Margin = New Padding(4, 3, 4, 3)
        TxtPromedio.Name = "TxtPromedio"
        TxtPromedio.ReadOnly = True
        TxtPromedio.Size = New Size(93, 23)
        TxtPromedio.TabIndex = 16
        TxtPromedio.TextAlign = HorizontalAlignment.Center
        ' 
        ' TxtDiasPromedio
        ' 
        TxtDiasPromedio.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TxtDiasPromedio.Font = New Font("Segoe UI", 9F)
        TxtDiasPromedio.ForeColor = Color.Red
        TxtDiasPromedio.Location = New Point(457, 593)
        TxtDiasPromedio.Margin = New Padding(4, 3, 4, 3)
        TxtDiasPromedio.Name = "TxtDiasPromedio"
        TxtDiasPromedio.ReadOnly = True
        TxtDiasPromedio.Size = New Size(69, 23)
        TxtDiasPromedio.TabIndex = 17
        TxtDiasPromedio.TextAlign = HorizontalAlignment.Center
        ' 
        ' TxtOficina
        ' 
        TxtOficina.BackColor = SystemColors.Window
        TxtOficina.Font = New Font("Segoe UI", 9F)
        TxtOficina.Location = New Point(93, 12)
        TxtOficina.Margin = New Padding(4, 3, 4, 3)
        TxtOficina.Name = "TxtOficina"
        TxtOficina.ReadOnly = True
        TxtOficina.Size = New Size(233, 23)
        TxtOficina.TabIndex = 18
        ' 
        ' TxtEncargado
        ' 
        TxtEncargado.BackColor = SystemColors.Window
        TxtEncargado.Font = New Font("Segoe UI", 9F)
        TxtEncargado.Location = New Point(463, 12)
        TxtEncargado.Margin = New Padding(4, 3, 4, 3)
        TxtEncargado.Name = "TxtEncargado"
        TxtEncargado.ReadOnly = True
        TxtEncargado.Size = New Size(353, 23)
        TxtEncargado.TabIndex = 19
        ' 
        ' TxtCategoria
        ' 
        TxtCategoria.BackColor = SystemColors.Window
        TxtCategoria.Font = New Font("Segoe UI", 9F)
        TxtCategoria.Location = New Point(93, 43)
        TxtCategoria.Margin = New Padding(4, 3, 4, 3)
        TxtCategoria.Name = "TxtCategoria"
        TxtCategoria.ReadOnly = True
        TxtCategoria.Size = New Size(174, 23)
        TxtCategoria.TabIndex = 20
        ' 
        ' TxtCaracter
        ' 
        TxtCaracter.BackColor = SystemColors.Window
        TxtCaracter.Font = New Font("Segoe UI", 9F)
        TxtCaracter.Location = New Point(450, 43)
        TxtCaracter.Margin = New Padding(4, 3, 4, 3)
        TxtCaracter.Name = "TxtCaracter"
        TxtCaracter.ReadOnly = True
        TxtCaracter.Size = New Size(139, 23)
        TxtCaracter.TabIndex = 21
        ' 
        ' TxtInstituto
        ' 
        TxtInstituto.BackColor = SystemColors.Window
        TxtInstituto.Font = New Font("Segoe UI", 9F)
        TxtInstituto.Location = New Point(659, 43)
        TxtInstituto.Margin = New Padding(4, 3, 4, 3)
        TxtInstituto.Name = "TxtInstituto"
        TxtInstituto.ReadOnly = True
        TxtInstituto.Size = New Size(157, 23)
        TxtInstituto.TabIndex = 22
        ' 
        ' TxtHorasContrato
        ' 
        TxtHorasContrato.BackColor = SystemColors.Window
        TxtHorasContrato.Font = New Font("Segoe UI", 9F)
        TxtHorasContrato.Location = New Point(93, 74)
        TxtHorasContrato.Margin = New Padding(4, 3, 4, 3)
        TxtHorasContrato.Name = "TxtHorasContrato"
        TxtHorasContrato.ReadOnly = True
        TxtHorasContrato.Size = New Size(69, 23)
        TxtHorasContrato.TabIndex = 23
        ' 
        ' TxtObligacionMensual
        ' 
        TxtObligacionMensual.BackColor = SystemColors.Window
        TxtObligacionMensual.Font = New Font("Segoe UI", 9F)
        TxtObligacionMensual.Location = New Point(305, 74)
        TxtObligacionMensual.Margin = New Padding(4, 3, 4, 3)
        TxtObligacionMensual.Name = "TxtObligacionMensual"
        TxtObligacionMensual.ReadOnly = True
        TxtObligacionMensual.Size = New Size(69, 23)
        TxtObligacionMensual.TabIndex = 24
        ' 
        ' TxtDiasLicencia
        ' 
        TxtDiasLicencia.BackColor = SystemColors.Window
        TxtDiasLicencia.Font = New Font("Segoe UI", 9F)
        TxtDiasLicencia.Location = New Point(747, 74)
        TxtDiasLicencia.Margin = New Padding(4, 3, 4, 3)
        TxtDiasLicencia.Name = "TxtDiasLicencia"
        TxtDiasLicencia.ReadOnly = True
        TxtDiasLicencia.Size = New Size(69, 23)
        TxtDiasLicencia.TabIndex = 25
        ' 
        ' TxtComentarios
        ' 
        TxtComentarios.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TxtComentarios.Font = New Font("Segoe UI", 9F)
        TxtComentarios.Location = New Point(8, 556)
        TxtComentarios.Margin = New Padding(4, 3, 4, 3)
        TxtComentarios.Multiline = True
        TxtComentarios.Name = "TxtComentarios"
        TxtComentarios.Size = New Size(231, 57)
        TxtComentarios.TabIndex = 26
        ' 
        ' CmbMeses
        ' 
        CmbMeses.DropDownStyle = ComboBoxStyle.DropDownList
        CmbMeses.Font = New Font("Segoe UI", 9F)
        CmbMeses.FormattingEnabled = True
        CmbMeses.Location = New Point(388, 50)
        CmbMeses.Margin = New Padding(4, 3, 4, 3)
        CmbMeses.Name = "CmbMeses"
        CmbMeses.Size = New Size(124, 23)
        CmbMeses.TabIndex = 35
        ' 
        ' CmbNombres
        ' 
        CmbNombres.Font = New Font("Segoe UI", 9F)
        CmbNombres.FormattingEnabled = True
        CmbNombres.Location = New Point(59, 19)
        CmbNombres.Margin = New Padding(4, 3, 4, 3)
        CmbNombres.Name = "CmbNombres"
        CmbNombres.Size = New Size(593, 23)
        CmbNombres.TabIndex = 36
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(14, 90)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(45, 15)
        Label2.TabIndex = 38
        Label2.Text = "Legajo:"
        ' 
        ' lblDesde
        ' 
        lblDesde.AutoSize = True
        lblDesde.Location = New Point(8, 54)
        lblDesde.Margin = New Padding(4, 0, 4, 0)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(42, 15)
        lblDesde.TabIndex = 39
        lblDesde.Text = "Desde:"
        ' 
        ' lblHasta
        ' 
        lblHasta.AutoSize = True
        lblHasta.Location = New Point(180, 54)
        lblHasta.Margin = New Padding(4, 0, 4, 0)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(40, 15)
        lblHasta.TabIndex = 40
        lblHasta.Text = "Hasta:"
        ' 
        ' lblAnio
        ' 
        lblAnio.AutoSize = True
        lblAnio.Location = New Point(520, 53)
        lblAnio.Margin = New Padding(4, 0, 4, 0)
        lblAnio.Name = "lblAnio"
        lblAnio.Size = New Size(32, 15)
        lblAnio.TabIndex = 41
        lblAnio.Text = "Año:"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(14, 16)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(48, 15)
        Label6.TabIndex = 42
        Label6.Text = "Oficina:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(393, 16)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(66, 15)
        Label7.TabIndex = 43
        Label7.Text = "Encargado:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(14, 47)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(61, 15)
        Label8.TabIndex = 44
        Label8.Text = "Categoría:"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(394, 47)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(54, 15)
        Label9.TabIndex = 45
        Label9.Text = "Carácter:"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(597, 47)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(54, 15)
        Label10.TabIndex = 46
        Label10.Text = "Sucursal:"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(14, 78)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(41, 15)
        Label11.TabIndex = 47
        Label11.Text = "Horas:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(175, 78)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(116, 15)
        Label12.TabIndex = 48
        Label12.Text = "Obligación Mensual:"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(604, 78)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(128, 15)
        Label13.TabIndex = 49
        Label13.Text = "Días de Licencia Anual:"
        ' 
        ' LblSaldo
        ' 
        LblSaldo.AutoSize = True
        LblSaldo.Location = New Point(14, 110)
        LblSaldo.Margin = New Padding(4, 0, 4, 0)
        LblSaldo.Name = "LblSaldo"
        LblSaldo.Size = New Size(100, 15)
        LblSaldo.TabIndex = 50
        LblSaldo.Text = "Saldo Vacaciones:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(TxtOficina)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(TxtEncargado)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(TxtCategoria)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(TxtCaracter)
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(TxtInstituto)
        GroupBox1.Controls.Add(Label11)
        GroupBox1.Controls.Add(TxtHorasContrato)
        GroupBox1.Controls.Add(Label12)
        GroupBox1.Controls.Add(TxtObligacionMensual)
        GroupBox1.Controls.Add(Label13)
        GroupBox1.Controls.Add(TxtDiasLicencia)
        GroupBox1.Controls.Add(LblSaldo)
        GroupBox1.ForeColor = Color.Black
        GroupBox1.Location = New Point(8, 85)
        GroupBox1.Margin = New Padding(4, 3, 4, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4, 3, 4, 3)
        GroupBox1.Size = New Size(824, 158)
        GroupBox1.TabIndex = 59
        GroupBox1.TabStop = False
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(txtLegajoEventual)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(lblLegajo)
        GroupBox2.Controls.Add(dtpHasta)
        GroupBox2.Controls.Add(dtpDesde)
        GroupBox2.Controls.Add(lblMes)
        GroupBox2.Controls.Add(Label23)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(TxtLegajo)
        GroupBox2.Controls.Add(lblDesde)
        GroupBox2.Controls.Add(lblHasta)
        GroupBox2.Controls.Add(lblAnio)
        GroupBox2.Controls.Add(TxtAnio)
        GroupBox2.Controls.Add(CmbMeses)
        GroupBox2.Controls.Add(CmbNombres)
        GroupBox2.Location = New Point(7, 3)
        GroupBox2.Margin = New Padding(4, 3, 4, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(4, 3, 4, 3)
        GroupBox2.Size = New Size(817, 83)
        GroupBox2.TabIndex = 60
        GroupBox2.TabStop = False
        ' 
        ' txtLegajoEventual
        ' 
        txtLegajoEventual.BackColor = Color.White
        txtLegajoEventual.BorderStyle = BorderStyle.FixedSingle
        txtLegajoEventual.Font = New Font("Segoe UI", 9F)
        txtLegajoEventual.Location = New Point(738, 48)
        txtLegajoEventual.Margin = New Padding(4, 3, 4, 3)
        txtLegajoEventual.Name = "txtLegajoEventual"
        txtLegajoEventual.ReadOnly = True
        txtLegajoEventual.Size = New Size(71, 23)
        txtLegajoEventual.TabIndex = 48
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(637, 53)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(93, 15)
        Label1.TabIndex = 47
        Label1.Text = "Legajo Eventual:"
        ' 
        ' lblLegajo
        ' 
        lblLegajo.AutoSize = True
        lblLegajo.Location = New Point(663, 22)
        lblLegajo.Margin = New Padding(4, 0, 4, 0)
        lblLegajo.Name = "lblLegajo"
        lblLegajo.Size = New Size(45, 15)
        lblLegajo.TabIndex = 46
        lblLegajo.Text = "Legajo:"
        ' 
        ' dtpHasta
        ' 
        dtpHasta.Font = New Font("Segoe UI", 9F)
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(227, 50)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(114, 23)
        dtpHasta.TabIndex = 45
        ' 
        ' dtpDesde
        ' 
        dtpDesde.Font = New Font("Segoe UI", 9F)
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(59, 50)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(114, 23)
        dtpDesde.TabIndex = 44
        ' 
        ' lblMes
        ' 
        lblMes.AutoSize = True
        lblMes.Location = New Point(348, 54)
        lblMes.Margin = New Padding(4, 0, 4, 0)
        lblMes.Name = "lblMes"
        lblMes.Size = New Size(32, 15)
        lblMes.TabIndex = 43
        lblMes.Text = "Mes:"
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(8, 22)
        Label23.Margin = New Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(48, 15)
        Label23.TabIndex = 42
        Label23.Text = "Agente:"
        ' 
        ' Label24
        ' 
        Label24.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label24.AutoSize = True
        Label24.Location = New Point(258, 596)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(62, 15)
        Label24.TabIndex = 61
        Label24.Text = "Promedio:"
        ' 
        ' Label25
        ' 
        Label25.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label25.AutoSize = True
        Label25.Location = New Point(258, 564)
        Label25.Margin = New Padding(4, 0, 4, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(91, 15)
        Label25.TabIndex = 62
        Label25.Text = "Días Trabajados:"
        ' 
        ' frmResumenAsistencia
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(837, 622)
        Controls.Add(DgvInasistencias)
        Controls.Add(Label25)
        Controls.Add(Label24)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(CmdSalir)
        Controls.Add(CmdImprimir)
        Controls.Add(CmdInasistencias)
        Controls.Add(DgvListado)
        Controls.Add(TxtDiasTrabajados)
        Controls.Add(TxtPromedio)
        Controls.Add(TxtDiasPromedio)
        Controls.Add(TxtComentarios)
        Controls.Add(chkEncabezados)
        Controls.Add(lnkCopiar)
        Margin = New Padding(4, 3, 4, 3)
        MinimizeBox = False
        MinimumSize = New Size(853, 661)
        Name = "frmResumenAsistencia"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Consultas - Resumen de Asistencia"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        CType(DgvInasistencias, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdImprimir As Button
    Friend WithEvents CmdInasistencias As Button
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents DgvInasistencias As DataGridView
    Friend WithEvents TxtLegajo As TextBox
    Friend WithEvents TxtAnio As TextBox
    Friend WithEvents TxtDiasTrabajados As TextBox
    Friend WithEvents TxtPromedio As TextBox
    Friend WithEvents TxtDiasPromedio As TextBox
    Friend WithEvents TxtOficina As TextBox
    Friend WithEvents TxtEncargado As TextBox
    Friend WithEvents TxtCategoria As TextBox
    Friend WithEvents TxtCaracter As TextBox
    Friend WithEvents TxtInstituto As TextBox
    Friend WithEvents TxtHorasContrato As TextBox
    Friend WithEvents TxtObligacionMensual As TextBox
    Friend WithEvents TxtDiasLicencia As TextBox
    Friend WithEvents TxtComentarios As TextBox
    Friend WithEvents TxtVac2019 As TextBox
    Friend WithEvents TxtVac2020 As TextBox
    Friend WithEvents TxtVac2021 As TextBox
    Friend WithEvents TxtVac2022 As TextBox
    Friend WithEvents TxtVac2023 As TextBox
    Friend WithEvents TxtVac2024 As TextBox
    Friend WithEvents TxtVac2025 As TextBox
    Friend WithEvents TxtVac2026 As TextBox
    Friend WithEvents CmbMeses As ComboBox
    Friend WithEvents CmbNombres As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblDesde As Label
    Friend WithEvents lblHasta As Label
    Friend WithEvents lblAnio As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents LblSaldo As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label23 As Label
    Friend WithEvents lblMes As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents lblLegajo As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents txtLegajoEventual As TextBox
End Class
