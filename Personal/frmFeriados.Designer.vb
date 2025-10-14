<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFeriados
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        LblDia = New Label()
        DtpDia = New DateTimePicker()
        LblZona = New Label()
        LblMotivo = New Label()
        TxtMotivo = New TextBox()
        ChkCreado = New CheckBox()
        CmdAgregar = New Button()
        CmdModificar = New Button()
        CmdBorrar = New Button()
        CmdAceptar = New Button()
        CmdCancelar = New Button()
        CmdSalir = New Button()
        cmbZonas = New ComboBox()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(12, 12)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(45, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Buscar:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(59, 9)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(513, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Control
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle1.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        DgvListado.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(12, 48)
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.Size = New Size(560, 248)
        DgvListado.TabIndex = 2
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = SystemColors.ControlText
        lnkCopiar.Location = New Point(348, 303)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 3
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        lnkCopiar.VisitedLinkColor = SystemColors.ControlText
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(453, 303)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 4
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' LblDia
        ' 
        LblDia.AutoSize = True
        LblDia.Location = New Point(12, 331)
        LblDia.Name = "LblDia"
        LblDia.Size = New Size(27, 15)
        LblDia.TabIndex = 5
        LblDia.Text = "Día:"
        ' 
        ' DtpDia
        ' 
        DtpDia.Format = DateTimePickerFormat.Short
        DtpDia.Location = New Point(46, 328)
        DtpDia.Name = "DtpDia"
        DtpDia.Size = New Size(110, 23)
        DtpDia.TabIndex = 6
        ' 
        ' LblZona
        ' 
        LblZona.AutoSize = True
        LblZona.Location = New Point(170, 331)
        LblZona.Name = "LblZona"
        LblZona.Size = New Size(37, 15)
        LblZona.TabIndex = 7
        LblZona.Text = "Zona:"
        ' 
        ' LblMotivo
        ' 
        LblMotivo.AutoSize = True
        LblMotivo.Location = New Point(12, 357)
        LblMotivo.Name = "LblMotivo"
        LblMotivo.Size = New Size(48, 15)
        LblMotivo.TabIndex = 9
        LblMotivo.Text = "Motivo:"
        ' 
        ' TxtMotivo
        ' 
        TxtMotivo.Location = New Point(65, 354)
        TxtMotivo.Name = "TxtMotivo"
        TxtMotivo.Size = New Size(395, 23)
        TxtMotivo.TabIndex = 10
        ' 
        ' ChkCreado
        ' 
        ChkCreado.AutoSize = True
        ChkCreado.Checked = True
        ChkCreado.CheckState = CheckState.Checked
        ChkCreado.Location = New Point(466, 356)
        ChkCreado.Name = "ChkCreado"
        ChkCreado.Size = New Size(64, 19)
        ChkCreado.TabIndex = 11
        ChkCreado.Text = "Creado"
        ChkCreado.UseVisualStyleBackColor = True
        ChkCreado.Visible = False
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Cursor = Cursors.Hand
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(12, 384)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 30)
        CmdAgregar.TabIndex = 12
        CmdAgregar.Text = "A&gregar"
        ' 
        ' CmdModificar
        ' 
        CmdModificar.Cursor = Cursors.Hand
        CmdModificar.FlatStyle = FlatStyle.Flat
        CmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdModificar.Location = New Point(93, 384)
        CmdModificar.Name = "CmdModificar"
        CmdModificar.Size = New Size(75, 30)
        CmdModificar.TabIndex = 13
        CmdModificar.Text = "&Modificar"
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.Cursor = Cursors.Hand
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(174, 384)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 30)
        CmdBorrar.TabIndex = 14
        CmdBorrar.Text = "&Borrar"
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.Cursor = Cursors.Hand
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(335, 384)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 15
        CmdAceptar.Text = "&Aceptar"
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.Cursor = Cursors.Hand
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(416, 384)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 30)
        CmdCancelar.TabIndex = 16
        CmdCancelar.Text = "&Cancelar"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.Cursor = Cursors.Hand
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(497, 384)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 17
        CmdSalir.Text = "&Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmbZonas
        ' 
        cmbZonas.FormattingEnabled = True
        cmbZonas.Location = New Point(213, 328)
        cmbZonas.Name = "cmbZonas"
        cmbZonas.Size = New Size(121, 23)
        cmbZonas.TabIndex = 18
        ' 
        ' frmFeriados
        ' 
        ClientSize = New Size(584, 424)
        Controls.Add(cmbZonas)
        Controls.Add(CmdSalir)
        Controls.Add(CmdCancelar)
        Controls.Add(CmdAceptar)
        Controls.Add(CmdBorrar)
        Controls.Add(CmdModificar)
        Controls.Add(CmdAgregar)
        Controls.Add(ChkCreado)
        Controls.Add(TxtMotivo)
        Controls.Add(LblMotivo)
        Controls.Add(LblZona)
        Controls.Add(DtpDia)
        Controls.Add(LblDia)
        Controls.Add(chkEncabezados)
        Controls.Add(lnkCopiar)
        Controls.Add(LblBuscar)
        Controls.Add(TxtBuscar)
        Controls.Add(DgvListado)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmFeriados"
        Text = "Nomenclador - Feriados"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents LblDia As Label
    Friend WithEvents DtpDia As DateTimePicker
    Friend WithEvents LblZona As Label
    Friend WithEvents LblMotivo As Label
    Friend WithEvents TxtMotivo As TextBox
    Friend WithEvents ChkCreado As CheckBox
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents CmdModificar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents cmbZonas As ComboBox
End Class