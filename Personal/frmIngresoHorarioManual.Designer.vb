<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIngresoHorarioManual
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
        grpHorarios = New GroupBox()
        DtpDia = New DateTimePicker()
        lblFecha = New Label()
        dtpHoraEntrada = New DateTimePicker()
        lblHoraEntrada = New Label()
        dtpHoraSalida = New DateTimePicker()
        lblHoraSalida = New Label()
        txtHorasTrabajadas = New TextBox()
        lblHorasTrabajadas = New Label()
        grpComentario = New GroupBox()
        txtComentario = New TextBox()
        btnGuardar = New Button()
        btnLimpiar = New Button()
        btnSalir = New Button()
        grpSeleccionAgente.SuspendLayout()
        grpHorarios.SuspendLayout()
        grpComentario.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTitulo
        ' 
        lblTitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblTitulo.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitulo.ForeColor = Color.DarkBlue
        lblTitulo.Location = New Point(12, 9)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(460, 25)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Ingreso Manual de Horarios"
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' grpSeleccionAgente
        ' 
        grpSeleccionAgente.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpSeleccionAgente.Controls.Add(cmbAgentes)
        grpSeleccionAgente.Controls.Add(lblAgente)
        grpSeleccionAgente.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpSeleccionAgente.Location = New Point(12, 47)
        grpSeleccionAgente.Name = "grpSeleccionAgente"
        grpSeleccionAgente.Size = New Size(460, 67)
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
        cmbAgentes.Location = New Point(86, 22)
        cmbAgentes.Name = "cmbAgentes"
        cmbAgentes.Size = New Size(368, 23)
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
        ' grpHorarios
        ' 
        grpHorarios.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpHorarios.Controls.Add(DtpDia)
        grpHorarios.Controls.Add(lblFecha)
        grpHorarios.Controls.Add(dtpHoraEntrada)
        grpHorarios.Controls.Add(lblHoraEntrada)
        grpHorarios.Controls.Add(dtpHoraSalida)
        grpHorarios.Controls.Add(lblHoraSalida)
        grpHorarios.Controls.Add(txtHorasTrabajadas)
        grpHorarios.Controls.Add(lblHorasTrabajadas)
        grpHorarios.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpHorarios.Location = New Point(12, 120)
        grpHorarios.Name = "grpHorarios"
        grpHorarios.Size = New Size(460, 120)
        grpHorarios.TabIndex = 2
        grpHorarios.TabStop = False
        grpHorarios.Text = "Horarios"
        ' 
        ' DtpDia
        ' 
        DtpDia.Format = DateTimePickerFormat.Short
        DtpDia.Location = New Point(100, 28)
        DtpDia.Name = "DtpDia"
        DtpDia.Size = New Size(120, 23)
        DtpDia.TabIndex = 1
        ' 
        ' lblFecha
        ' 
        lblFecha.AutoSize = True
        lblFecha.Font = New Font("Segoe UI", 9F)
        lblFecha.Location = New Point(15, 28)
        lblFecha.Name = "lblFecha"
        lblFecha.Size = New Size(41, 15)
        lblFecha.TabIndex = 0
        lblFecha.Text = "Fecha:"
        ' 
        ' dtpHoraEntrada
        ' 
        dtpHoraEntrada.Format = DateTimePickerFormat.Time
        dtpHoraEntrada.Location = New Point(100, 57)
        dtpHoraEntrada.Name = "dtpHoraEntrada"
        dtpHoraEntrada.ShowUpDown = True
        dtpHoraEntrada.Size = New Size(120, 23)
        dtpHoraEntrada.TabIndex = 3
        ' 
        ' lblHoraEntrada
        ' 
        lblHoraEntrada.AutoSize = True
        lblHoraEntrada.Font = New Font("Segoe UI", 9F)
        lblHoraEntrada.Location = New Point(15, 57)
        lblHoraEntrada.Name = "lblHoraEntrada"
        lblHoraEntrada.Size = New Size(79, 15)
        lblHoraEntrada.TabIndex = 2
        lblHoraEntrada.Text = "Hora Entrada:"
        ' 
        ' dtpHoraSalida
        ' 
        dtpHoraSalida.Format = DateTimePickerFormat.Time
        dtpHoraSalida.Location = New Point(334, 54)
        dtpHoraSalida.Name = "dtpHoraSalida"
        dtpHoraSalida.ShowUpDown = True
        dtpHoraSalida.Size = New Size(120, 23)
        dtpHoraSalida.TabIndex = 5
        ' 
        ' lblHoraSalida
        ' 
        lblHoraSalida.AutoSize = True
        lblHoraSalida.Font = New Font("Segoe UI", 9F)
        lblHoraSalida.Location = New Point(263, 57)
        lblHoraSalida.Name = "lblHoraSalida"
        lblHoraSalida.Size = New Size(70, 15)
        lblHoraSalida.TabIndex = 4
        lblHoraSalida.Text = "Hora Salida:"
        ' 
        ' txtHorasTrabajadas
        ' 
        txtHorasTrabajadas.BackColor = Color.LightGray
        txtHorasTrabajadas.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtHorasTrabajadas.Location = New Point(334, 83)
        txtHorasTrabajadas.Name = "txtHorasTrabajadas"
        txtHorasTrabajadas.ReadOnly = True
        txtHorasTrabajadas.Size = New Size(120, 23)
        txtHorasTrabajadas.TabIndex = 7
        txtHorasTrabajadas.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblHorasTrabajadas
        ' 
        lblHorasTrabajadas.AutoSize = True
        lblHorasTrabajadas.Font = New Font("Segoe UI", 9F)
        lblHorasTrabajadas.Location = New Point(230, 86)
        lblHorasTrabajadas.Name = "lblHorasTrabajadas"
        lblHorasTrabajadas.Size = New Size(100, 15)
        lblHorasTrabajadas.TabIndex = 6
        lblHorasTrabajadas.Text = "Horas Trabajadas:"
        ' 
        ' grpComentario
        ' 
        grpComentario.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpComentario.Controls.Add(txtComentario)
        grpComentario.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpComentario.Location = New Point(12, 246)
        grpComentario.Name = "grpComentario"
        grpComentario.Size = New Size(460, 99)
        grpComentario.TabIndex = 3
        grpComentario.TabStop = False
        grpComentario.Text = "Comentario"
        ' 
        ' txtComentario
        ' 
        txtComentario.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtComentario.Font = New Font("Segoe UI", 9F)
        txtComentario.Location = New Point(15, 22)
        txtComentario.MaxLength = 500
        txtComentario.Multiline = True
        txtComentario.Name = "txtComentario"
        txtComentario.ScrollBars = ScrollBars.Vertical
        txtComentario.Size = New Size(439, 68)
        txtComentario.TabIndex = 1
        ' 
        ' btnGuardar
        ' 
        btnGuardar.BackColor = SystemColors.Control
        btnGuardar.Cursor = Cursors.Hand
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnGuardar.ForeColor = Color.Black
        btnGuardar.Location = New Point(206, 360)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(85, 30)
        btnGuardar.TabIndex = 5
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        ' 
        ' btnLimpiar
        ' 
        btnLimpiar.BackColor = SystemColors.Control
        btnLimpiar.Cursor = Cursors.Hand
        btnLimpiar.FlatStyle = FlatStyle.Flat
        btnLimpiar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnLimpiar.ForeColor = Color.Black
        btnLimpiar.Location = New Point(297, 360)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(85, 30)
        btnLimpiar.TabIndex = 5
        btnLimpiar.Text = "Cancelar"
        btnLimpiar.UseVisualStyleBackColor = False
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSalir.BackColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(388, 360)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(85, 30)
        btnSalir.TabIndex = 6
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = True
        ' 
        ' frmIngresoHorarioManual
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(484, 397)
        Controls.Add(btnGuardar)
        Controls.Add(btnLimpiar)
        Controls.Add(btnSalir)
        Controls.Add(grpComentario)
        Controls.Add(grpHorarios)
        Controls.Add(grpSeleccionAgente)
        Controls.Add(lblTitulo)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmIngresoHorarioManual"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Ingreso Manual de Horarios"
        grpSeleccionAgente.ResumeLayout(False)
        grpSeleccionAgente.PerformLayout()
        grpHorarios.ResumeLayout(False)
        grpHorarios.PerformLayout()
        grpComentario.ResumeLayout(False)
        grpComentario.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents grpSeleccionAgente As GroupBox
    Friend WithEvents cmbAgentes As ComboBox
    Friend WithEvents lblAgente As Label
    Friend WithEvents grpHorarios As GroupBox
    Friend WithEvents DtpDia As DateTimePicker
    Friend WithEvents lblFecha As Label
    Friend WithEvents dtpHoraEntrada As DateTimePicker
    Friend WithEvents lblHoraEntrada As Label
    Friend WithEvents dtpHoraSalida As DateTimePicker
    Friend WithEvents lblHoraSalida As Label
    Friend WithEvents txtHorasTrabajadas As TextBox
    Friend WithEvents lblHorasTrabajadas As Label
    Friend WithEvents grpComentario As GroupBox
    Friend WithEvents txtComentario As TextBox

    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents btnSalir As Button

End Class