<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReporteBonos
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dtpDesde = New DateTimePicker()
        lblDesde = New Label()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        cmbSucursal = New ComboBox()
        lblSucursal = New Label()
        chkHojaPorAgente = New CheckBox()
        btnSalir = New Button()
        cmdVer = New Button()
        SuspendLayout()
        '
        'dtpDesde
        '
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(12, 30)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(114, 23)
        dtpDesde.TabIndex = 0
        '
        'lblDesde
        '
        lblDesde.AutoSize = True
        lblDesde.Location = New Point(12, 9)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(42, 15)
        lblDesde.TabIndex = 1
        lblDesde.Text = "Desde:"
        '
        'lblHasta
        '
        lblHasta.AutoSize = True
        lblHasta.Location = New Point(132, 9)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(40, 15)
        lblHasta.TabIndex = 3
        lblHasta.Text = "Hasta:"
        '
        'dtpHasta
        '
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(132, 30)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(114, 23)
        dtpHasta.TabIndex = 2
        '
        'cmbSucursal
        '
        cmbSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSucursal.FormattingEnabled = True
        cmbSucursal.Location = New Point(252, 30)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(169, 23)
        cmbSucursal.TabIndex = 4
        '
        'lblSucursal
        '
        lblSucursal.AutoSize = True
        lblSucursal.Location = New Point(252, 9)
        lblSucursal.Name = "lblSucursal"
        lblSucursal.Size = New Size(54, 15)
        lblSucursal.TabIndex = 5
        lblSucursal.Text = "Sucursal:"
        '
        'chkHojaPorAgente
        '
        chkHojaPorAgente.AutoSize = True
        chkHojaPorAgente.Location = New Point(12, 68)
        chkHojaPorAgente.Name = "chkHojaPorAgente"
        chkHojaPorAgente.Size = New Size(114, 19)
        chkHojaPorAgente.TabIndex = 6
        chkHojaPorAgente.Text = "Hoja por agente"
        chkHojaPorAgente.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(336, 96)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(85, 30)
        btnSalir.TabIndex = 8
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        '
        'cmdVer
        '
        cmdVer.BackColor = SystemColors.Control
        cmdVer.Cursor = Cursors.Hand
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdVer.ForeColor = SystemColors.ControlText
        cmdVer.Location = New Point(245, 96)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(85, 30)
        cmdVer.TabIndex = 7
        cmdVer.Text = "Ver"
        cmdVer.UseVisualStyleBackColor = False
        '
        'frmReporteBonos
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(436, 139)
        Controls.Add(cmdVer)
        Controls.Add(btnSalir)
        Controls.Add(chkHojaPorAgente)
        Controls.Add(lblSucursal)
        Controls.Add(cmbSucursal)
        Controls.Add(lblHasta)
        Controls.Add(dtpHasta)
        Controls.Add(lblDesde)
        Controls.Add(dtpDesde)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmReporteBonos"
        Text = "Reporte de Bonos"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblDesde As Label
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents lblSucursal As Label
    Friend WithEvents chkHojaPorAgente As CheckBox
    Friend WithEvents btnSalir As Button
    Friend WithEvents cmdVer As Button
End Class
