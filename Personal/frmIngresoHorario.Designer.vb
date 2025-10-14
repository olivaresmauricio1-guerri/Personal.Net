<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIngresoHorario
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
        lblTitulo = New Label()
        grpSeleccionAgente = New GroupBox()
        cmbAgentes = New ComboBox()
        lblAgente = New Label()
        txtDocumento = New TextBox()
        lblDocumento = New Label()
        grpEstadoSistema = New GroupBox()
        DtpDia = New DateTimePicker()
        txtHsCumplidas = New TextBox()
        Label3 = New Label()
        txtEntro = New TextBox()
        txtSalio = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txtHora = New TextBox()
        lblFecha = New Label()
        lblHora = New Label()
        btnMarcar = New Button()
        btnSalir = New Button()
        grpSeleccionAgente.SuspendLayout()
        grpEstadoSistema.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTitulo
        ' 
        lblTitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblTitulo.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitulo.ForeColor = Color.DarkBlue
        lblTitulo.Location = New Point(12, 9)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(335, 25)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Ingreso de Horario"
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' grpSeleccionAgente
        ' 
        grpSeleccionAgente.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpSeleccionAgente.Controls.Add(cmbAgentes)
        grpSeleccionAgente.Controls.Add(lblAgente)
        grpSeleccionAgente.Controls.Add(txtDocumento)
        grpSeleccionAgente.Controls.Add(lblDocumento)
        grpSeleccionAgente.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpSeleccionAgente.Location = New Point(12, 47)
        grpSeleccionAgente.Name = "grpSeleccionAgente"
        grpSeleccionAgente.Size = New Size(335, 85)
        grpSeleccionAgente.TabIndex = 1
        grpSeleccionAgente.TabStop = False
        grpSeleccionAgente.Text = "Selección de Agente"
        ' 
        ' cmbAgentes
        ' 
        cmbAgentes.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmbAgentes.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbAgentes.AutoCompleteSource = AutoCompleteSource.ListItems
        cmbAgentes.Font = New Font("Segoe UI", 9F)
        cmbAgentes.FormattingEnabled = True
        cmbAgentes.Location = New Point(94, 22)
        cmbAgentes.Name = "cmbAgentes"
        cmbAgentes.Size = New Size(235, 23)
        cmbAgentes.TabIndex = 1
        ' 
        ' lblAgente
        ' 
        lblAgente.AutoSize = True
        lblAgente.Font = New Font("Segoe UI", 9F)
        lblAgente.Location = New Point(15, 25)
        lblAgente.Name = "lblAgente"
        lblAgente.Size = New Size(48, 15)
        lblAgente.TabIndex = 0
        lblAgente.Text = "Agente:"
        ' 
        ' txtDocumento
        ' 
        txtDocumento.BackColor = Color.White
        txtDocumento.Font = New Font("Segoe UI", 9F)
        txtDocumento.Location = New Point(94, 51)
        txtDocumento.Name = "txtDocumento"
        txtDocumento.ReadOnly = True
        txtDocumento.Size = New Size(150, 23)
        txtDocumento.TabIndex = 3
        ' 
        ' lblDocumento
        ' 
        lblDocumento.AutoSize = True
        lblDocumento.Font = New Font("Segoe UI", 9F)
        lblDocumento.Location = New Point(15, 54)
        lblDocumento.Name = "lblDocumento"
        lblDocumento.Size = New Size(73, 15)
        lblDocumento.TabIndex = 2
        lblDocumento.Text = "Documento:"
        ' 
        ' grpEstadoSistema
        ' 
        grpEstadoSistema.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpEstadoSistema.Controls.Add(DtpDia)
        grpEstadoSistema.Controls.Add(txtHsCumplidas)
        grpEstadoSistema.Controls.Add(Label3)
        grpEstadoSistema.Controls.Add(txtEntro)
        grpEstadoSistema.Controls.Add(txtSalio)
        grpEstadoSistema.Controls.Add(Label1)
        grpEstadoSistema.Controls.Add(Label2)
        grpEstadoSistema.Controls.Add(txtHora)
        grpEstadoSistema.Controls.Add(lblFecha)
        grpEstadoSistema.Controls.Add(lblHora)
        grpEstadoSistema.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpEstadoSistema.Location = New Point(12, 138)
        grpEstadoSistema.Name = "grpEstadoSistema"
        grpEstadoSistema.Size = New Size(335, 170)
        grpEstadoSistema.TabIndex = 3
        grpEstadoSistema.TabStop = False
        ' 
        ' DtpDia
        ' 
        DtpDia.Format = DateTimePickerFormat.Short
        DtpDia.Location = New Point(79, 25)
        DtpDia.Name = "DtpDia"
        DtpDia.Size = New Size(106, 23)
        DtpDia.TabIndex = 10
        ' 
        ' txtHsCumplidas
        ' 
        txtHsCumplidas.BackColor = Color.White
        txtHsCumplidas.Location = New Point(227, 119)
        txtHsCumplidas.Name = "txtHsCumplidas"
        txtHsCumplidas.ReadOnly = True
        txtHsCumplidas.Size = New Size(100, 23)
        txtHsCumplidas.TabIndex = 9
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(227, 98)
        Label3.Name = "Label3"
        Label3.Size = New Size(101, 15)
        Label3.TabIndex = 8
        Label3.Text = "Horas Cumplidas:"
        ' 
        ' txtEntro
        ' 
        txtEntro.BackColor = Color.White
        txtEntro.Location = New Point(15, 119)
        txtEntro.Name = "txtEntro"
        txtEntro.ReadOnly = True
        txtEntro.Size = New Size(100, 23)
        txtEntro.TabIndex = 7
        ' 
        ' txtSalio
        ' 
        txtSalio.BackColor = Color.White
        txtSalio.Location = New Point(121, 119)
        txtSalio.Name = "txtSalio"
        txtSalio.ReadOnly = True
        txtSalio.Size = New Size(100, 23)
        txtSalio.TabIndex = 6
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(121, 98)
        Label1.Name = "Label1"
        Label1.Size = New Size(36, 15)
        Label1.TabIndex = 4
        Label1.Text = "Salió:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(15, 95)
        Label2.Name = "Label2"
        Label2.Size = New Size(40, 15)
        Label2.TabIndex = 5
        Label2.Text = "Entró:"
        ' 
        ' txtHora
        ' 
        txtHora.BackColor = Color.White
        txtHora.Location = New Point(79, 54)
        txtHora.Name = "txtHora"
        txtHora.ReadOnly = True
        txtHora.Size = New Size(106, 23)
        txtHora.TabIndex = 3
        ' 
        ' lblFecha
        ' 
        lblFecha.AutoSize = True
        lblFecha.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblFecha.ForeColor = Color.Black
        lblFecha.Location = New Point(15, 25)
        lblFecha.Name = "lblFecha"
        lblFecha.Size = New Size(42, 15)
        lblFecha.TabIndex = 0
        lblFecha.Text = "Fecha:"
        ' 
        ' lblHora
        ' 
        lblHora.AutoSize = True
        lblHora.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblHora.ForeColor = Color.Black
        lblHora.Location = New Point(15, 53)
        lblHora.Name = "lblHora"
        lblHora.Size = New Size(37, 15)
        lblHora.TabIndex = 1
        lblHora.Text = "Hora:"
        ' 
        ' btnMarcar
        ' 
        btnMarcar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnMarcar.BackColor = SystemColors.Control
        btnMarcar.Cursor = Cursors.Hand
        btnMarcar.FlatStyle = FlatStyle.Flat
        btnMarcar.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnMarcar.ForeColor = Color.Black
        btnMarcar.Location = New Point(133, 314)
        btnMarcar.Name = "btnMarcar"
        btnMarcar.Size = New Size(115, 30)
        btnMarcar.TabIndex = 5
        btnMarcar.Text = "Marcar Horario"
        btnMarcar.UseVisualStyleBackColor = False
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(259, 314)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 30)
        btnSalir.TabIndex = 6
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' frmIngresoHorario
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(359, 354)
        Controls.Add(btnMarcar)
        Controls.Add(btnSalir)
        Controls.Add(grpEstadoSistema)
        Controls.Add(grpSeleccionAgente)
        Controls.Add(lblTitulo)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmIngresoHorario"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Ingreso Horario"
        grpSeleccionAgente.ResumeLayout(False)
        grpSeleccionAgente.PerformLayout()
        grpEstadoSistema.ResumeLayout(False)
        grpEstadoSistema.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents grpSeleccionAgente As GroupBox
    Friend WithEvents cmbAgentes As ComboBox
    Friend WithEvents lblAgente As Label
    Friend WithEvents txtDocumento As TextBox
    Friend WithEvents lblDocumento As Label
    Friend WithEvents grpEstadoSistema As GroupBox
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblHora As Label
    Friend WithEvents txtHsCumplidas As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtEntro As TextBox
    Friend WithEvents txtSalio As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtHora As TextBox
    Friend WithEvents btnMarcar As Button
    Friend WithEvents btnSalir As Button
    Friend WithEvents DtpDia As DateTimePicker

End Class
