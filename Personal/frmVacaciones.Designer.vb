<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVacaciones
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
        dgvListado = New DataGridView()
        btnSalir = New Button()
        btnCalcularDisponibles = New Button()
        CType(dgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvListado
        ' 
        dgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvListado.Location = New Point(12, 12)
        dgvListado.Name = "dgvListado"
        dgvListado.Size = New Size(776, 462)
        dgvListado.TabIndex = 0
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(701, 484)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 30)
        btnSalir.TabIndex = 42
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnCalcularDisponibles
        ' 
        btnCalcularDisponibles.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCalcularDisponibles.BackColor = SystemColors.Control
        btnCalcularDisponibles.Cursor = Cursors.Hand
        btnCalcularDisponibles.FlatStyle = FlatStyle.Flat
        btnCalcularDisponibles.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCalcularDisponibles.Location = New Point(13, 484)
        btnCalcularDisponibles.Margin = New Padding(4, 3, 4, 3)
        btnCalcularDisponibles.Name = "btnCalcularDisponibles"
        btnCalcularDisponibles.Size = New Size(198, 30)
        btnCalcularDisponibles.TabIndex = 43
        btnCalcularDisponibles.Text = "Calcular Disponibles: 0000"
        btnCalcularDisponibles.UseVisualStyleBackColor = False
        ' 
        ' frmVacaciones
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(801, 526)
        Controls.Add(btnCalcularDisponibles)
        Controls.Add(btnSalir)
        Controls.Add(dgvListado)
        Name = "frmVacaciones"
        Text = "Vacaciones"
        CType(dgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvListado As DataGridView
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnCalcularDisponibles As Button
End Class
