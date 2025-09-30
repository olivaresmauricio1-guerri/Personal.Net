<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInasistenciasJustificadas
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
        TableLayoutPanel1 = New TableLayoutPanel()
        Panel1 = New Panel()
        CmdSalir = New Button()
        CmdCancelar = New Button()
        CmdAceptar = New Button()
        CmdBorrar = New Button()
        CmdAgregar = New Button()
        Panel5 = New Panel()
        LblSaldo = New Label()
        ChkCorridos = New CheckBox()
        TxtComentario = New TextBox()
        Label7 = New Label()
        TxtDias = New TextBox()
        Label6 = New Label()
        DtpFecha = New DateTimePicker()
        Label5 = New Label()
        CmbTipoInasistencia = New ComboBox()
        Label4 = New Label()
        GroupBox2 = New GroupBox()
        DgvListado = New DataGridView()
        CmbAgente = New ComboBox()
        Label3 = New Label()
        TableLayoutPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel5.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(Panel1, 0, 2)
        TableLayoutPanel1.Controls.Add(Panel5, 0, 1)
        TableLayoutPanel1.Controls.Add(GroupBox2, 0, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 330F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 55F))
        TableLayoutPanel1.Size = New Size(561, 571)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(CmdSalir)
        Panel1.Controls.Add(CmdCancelar)
        Panel1.Controls.Add(CmdAceptar)
        Panel1.Controls.Add(CmdBorrar)
        Panel1.Controls.Add(CmdAgregar)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(3, 519)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(555, 49)
        Panel1.TabIndex = 10
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(474, 6)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 10
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(380, 6)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(88, 30)
        CmdCancelar.TabIndex = 9
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(286, 6)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(88, 30)
        CmdAceptar.TabIndex = 8
        CmdAceptar.Text = "Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(97, 6)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(88, 30)
        CmdBorrar.TabIndex = 11
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(3, 6)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(88, 30)
        CmdAgregar.TabIndex = 2
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(LblSaldo)
        Panel5.Controls.Add(ChkCorridos)
        Panel5.Controls.Add(TxtComentario)
        Panel5.Controls.Add(Label7)
        Panel5.Controls.Add(TxtDias)
        Panel5.Controls.Add(Label6)
        Panel5.Controls.Add(DtpFecha)
        Panel5.Controls.Add(Label5)
        Panel5.Controls.Add(CmbTipoInasistencia)
        Panel5.Controls.Add(Label4)
        Panel5.Dock = DockStyle.Fill
        Panel5.Location = New Point(3, 333)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(555, 180)
        Panel5.TabIndex = 9
        ' 
        ' LblSaldo
        ' 
        LblSaldo.AutoSize = True
        LblSaldo.Font = New Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblSaldo.ForeColor = Color.Blue
        LblSaldo.Location = New Point(286, 72)
        LblSaldo.Name = "LblSaldo"
        LblSaldo.Size = New Size(51, 13)
        LblSaldo.TabIndex = 21
        LblSaldo.Text = "Saldo: -"
        ' 
        ' ChkCorridos
        ' 
        ChkCorridos.AutoSize = True
        ChkCorridos.Location = New Point(175, 70)
        ChkCorridos.Name = "ChkCorridos"
        ChkCorridos.Size = New Size(96, 19)
        ChkCorridos.TabIndex = 6
        ChkCorridos.Text = "Días Corridos"
        ChkCorridos.UseVisualStyleBackColor = True
        ' 
        ' TxtComentario
        ' 
        TxtComentario.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtComentario.Location = New Point(88, 99)
        TxtComentario.Multiline = True
        TxtComentario.Name = "TxtComentario"
        TxtComentario.ScrollBars = ScrollBars.Vertical
        TxtComentario.Size = New Size(458, 74)
        TxtComentario.TabIndex = 7
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(5, 101)
        Label7.Name = "Label7"
        Label7.Size = New Size(73, 15)
        Label7.TabIndex = 18
        Label7.Text = "Comentario:"
        ' 
        ' TxtDias
        ' 
        TxtDias.Location = New Point(88, 70)
        TxtDias.Name = "TxtDias"
        TxtDias.Size = New Size(70, 23)
        TxtDias.TabIndex = 5
        TxtDias.Text = "1"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(5, 73)
        Label6.Name = "Label6"
        Label6.Size = New Size(32, 15)
        Label6.TabIndex = 16
        Label6.Text = "Días:"
        ' 
        ' DtpFecha
        ' 
        DtpFecha.Format = DateTimePickerFormat.Short
        DtpFecha.Location = New Point(88, 42)
        DtpFecha.Name = "DtpFecha"
        DtpFecha.Size = New Size(106, 23)
        DtpFecha.TabIndex = 4
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(5, 47)
        Label5.Name = "Label5"
        Label5.Size = New Size(41, 15)
        Label5.TabIndex = 14
        Label5.Text = "Fecha:"
        ' 
        ' CmbTipoInasistencia
        ' 
        CmbTipoInasistencia.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        CmbTipoInasistencia.FormattingEnabled = True
        CmbTipoInasistencia.Location = New Point(88, 10)
        CmbTipoInasistencia.Name = "CmbTipoInasistencia"
        CmbTipoInasistencia.Size = New Size(458, 23)
        CmbTipoInasistencia.TabIndex = 3
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(5, 12)
        Label4.Name = "Label4"
        Label4.Size = New Size(48, 15)
        Label4.TabIndex = 12
        Label4.Text = "Motivo:"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(DgvListado)
        GroupBox2.Controls.Add(CmbAgente)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(3, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(555, 324)
        GroupBox2.TabIndex = 5
        GroupBox2.TabStop = False
        GroupBox2.Text = "Historial de Inasistencia"
        ' 
        ' DgvListado
        ' 
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(6, 64)
        DgvListado.Name = "DgvListado"
        DgvListado.Size = New Size(549, 254)
        DgvListado.TabIndex = 2
        ' 
        ' CmbAgente
        ' 
        CmbAgente.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        CmbAgente.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CmbAgente.AutoCompleteSource = AutoCompleteSource.ListItems
        CmbAgente.FormattingEnabled = True
        CmbAgente.Location = New Point(88, 23)
        CmbAgente.Name = "CmbAgente"
        CmbAgente.Size = New Size(458, 23)
        CmbAgente.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(5, 26)
        Label3.Name = "Label3"
        Label3.Size = New Size(48, 15)
        Label3.TabIndex = 0
        Label3.Text = "Agente:"
        ' 
        ' frmInasistenciasJustificadas
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(561, 571)
        Controls.Add(TableLayoutPanel1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmInasistenciasJustificadas"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Inasistencias Justificadas"
        TableLayoutPanel1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CmbAgente As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents LblSaldo As Label
    Friend WithEvents ChkCorridos As CheckBox
    Friend WithEvents TxtComentario As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtDias As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents DtpFecha As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents CmbTipoInasistencia As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents DgvListado As DataGridView
End Class
