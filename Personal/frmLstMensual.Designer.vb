<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLstMensual
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        dtpDesde = New DateTimePicker()
        lblDesde = New Label()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        cmbSucursal = New ComboBox()
        lblSucursal = New Label()
        chkNegativos = New CheckBox()
        btnSalir = New Button()
        cmdVer = New Button()
        SuspendLayout()
        ' 
        ' dtpDesde
        ' 
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(12, 30)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(114, 23)
        dtpDesde.TabIndex = 0
        ' 
        ' lblDesde
        ' 
        lblDesde.AutoSize = True
        lblDesde.Location = New Point(12, 9)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(42, 15)
        lblDesde.TabIndex = 1
        lblDesde.Text = "Desde:"
        ' 
        ' lblHasta
        ' 
        lblHasta.AutoSize = True
        lblHasta.Location = New Point(132, 9)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(40, 15)
        lblHasta.TabIndex = 3
        lblHasta.Text = "Hasta:"
        ' 
        ' dtpHasta
        ' 
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(132, 30)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(114, 23)
        dtpHasta.TabIndex = 2
        ' 
        ' cmbSucursal
        ' 
        cmbSucursal.FormattingEnabled = True
        cmbSucursal.Location = New Point(252, 30)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(221, 23)
        cmbSucursal.TabIndex = 4
        ' 
        ' lblSucursal
        ' 
        lblSucursal.AutoSize = True
        lblSucursal.Location = New Point(252, 9)
        lblSucursal.Name = "lblSucursal"
        lblSucursal.Size = New Size(54, 15)
        lblSucursal.TabIndex = 5
        lblSucursal.Text = "Sucursal:"
        ' 
        ' chkNegativos
        ' 
        chkNegativos.AutoSize = True
        chkNegativos.Location = New Point(12, 75)
        chkNegativos.Name = "chkNegativos"
        chkNegativos.Size = New Size(105, 19)
        chkNegativos.TabIndex = 6
        chkNegativos.Text = "Sólo Negativos"
        chkNegativos.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(388, 68)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(85, 30)
        btnSalir.TabIndex = 7
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdVer
        ' 
        cmdVer.BackColor = SystemColors.Control
        cmdVer.Cursor = Cursors.Hand
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdVer.ForeColor = SystemColors.ControlText
        cmdVer.Location = New Point(297, 68)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(85, 30)
        cmdVer.TabIndex = 8
        cmdVer.Text = "Ver"
        cmdVer.UseVisualStyleBackColor = False
        ' 
        ' frmLstMensual
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(482, 106)
        Controls.Add(cmdVer)
        Controls.Add(btnSalir)
        Controls.Add(chkNegativos)
        Controls.Add(lblSucursal)
        Controls.Add(cmbSucursal)
        Controls.Add(lblHasta)
        Controls.Add(dtpHasta)
        Controls.Add(lblDesde)
        Controls.Add(dtpDesde)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLstMensual"
        Text = "Listado Mensual"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblDesde As Label
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents lblSucursal As Label
    Friend WithEvents chkNegativos As CheckBox
    Friend WithEvents btnSalir As Button
    Friend WithEvents cmdVer As Button
End Class
