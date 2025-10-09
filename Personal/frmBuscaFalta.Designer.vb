<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuscaFalta
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
        dgvFaltas = New DataGridView()
        lblDesde = New Label()
        dtpDesde = New DateTimePicker()
        btnSalir = New Button()
        btnCrearRegistro = New Button()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        lblTotal = New Label()
        cmbInstituto = New ComboBox()
        lblInstituto = New Label()
        CType(dgvFaltas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvFaltas
        ' 
        dgvFaltas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvFaltas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFaltas.Location = New Point(12, 38)
        dgvFaltas.Name = "dgvFaltas"
        dgvFaltas.Size = New Size(670, 569)
        dgvFaltas.TabIndex = 0
        ' 
        ' lblDesde
        ' 
        lblDesde.AutoSize = True
        lblDesde.Location = New Point(12, 13)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(42, 15)
        lblDesde.TabIndex = 1
        lblDesde.Text = "Desde:"
        ' 
        ' dtpDesde
        ' 
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(60, 9)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(101, 23)
        dtpDesde.TabIndex = 2
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(594, 620)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 30)
        btnSalir.TabIndex = 42
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnCrearRegistro
        ' 
        btnCrearRegistro.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCrearRegistro.BackColor = SystemColors.Control
        btnCrearRegistro.Cursor = Cursors.Hand
        btnCrearRegistro.FlatStyle = FlatStyle.Flat
        btnCrearRegistro.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCrearRegistro.Location = New Point(12, 620)
        btnCrearRegistro.Margin = New Padding(4, 3, 4, 3)
        btnCrearRegistro.Name = "btnCrearRegistro"
        btnCrearRegistro.Size = New Size(254, 30)
        btnCrearRegistro.TabIndex = 43
        btnCrearRegistro.Text = "Crear Inasistencia a Seleccionados"
        btnCrearRegistro.UseVisualStyleBackColor = False
        ' 
        ' lblHasta
        ' 
        lblHasta.AutoSize = True
        lblHasta.Location = New Point(183, 13)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(40, 15)
        lblHasta.TabIndex = 45
        lblHasta.Text = "Hasta:"
        ' 
        ' dtpHasta
        ' 
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(231, 9)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(101, 23)
        dtpHasta.TabIndex = 46
        ' 
        ' lblTotal
        ' 
        lblTotal.AutoSize = True
        lblTotal.Location = New Point(564, 13)
        lblTotal.Name = "lblTotal"
        lblTotal.Size = New Size(0, 15)
        lblTotal.TabIndex = 47
        ' 
        ' cmbInstituto
        ' 
        cmbInstituto.DropDownStyle = ComboBoxStyle.DropDownList
        cmbInstituto.FormattingEnabled = True
        cmbInstituto.Location = New Point(412, 9)
        cmbInstituto.Margin = New Padding(4, 3, 4, 3)
        cmbInstituto.Name = "cmbInstituto"
        cmbInstituto.Size = New Size(133, 23)
        cmbInstituto.TabIndex = 49
        ' 
        ' lblInstituto
        ' 
        lblInstituto.AutoSize = True
        lblInstituto.Location = New Point(350, 13)
        lblInstituto.Margin = New Padding(4, 0, 4, 0)
        lblInstituto.Name = "lblInstituto"
        lblInstituto.Size = New Size(54, 15)
        lblInstituto.TabIndex = 48
        lblInstituto.Text = "Sucursal:"
        ' 
        ' frmBuscaFalta
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(694, 662)
        Controls.Add(cmbInstituto)
        Controls.Add(lblInstituto)
        Controls.Add(lblTotal)
        Controls.Add(dtpHasta)
        Controls.Add(lblHasta)
        Controls.Add(btnCrearRegistro)
        Controls.Add(btnSalir)
        Controls.Add(dtpDesde)
        Controls.Add(lblDesde)
        Controls.Add(dgvFaltas)
        MinimizeBox = False
        Name = "frmBuscaFalta"
        Text = "frmBuscaFalta"
        CType(dgvFaltas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dgvFaltas As DataGridView
    Friend WithEvents lblDesde As Label
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnCrearRegistro As Button
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents lblTotal As Label
    Friend WithEvents cmbInstituto As ComboBox
    Friend WithEvents lblInstituto As Label
End Class
