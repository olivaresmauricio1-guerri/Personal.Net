<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBajaReloj
    Inherits System.Windows.Forms.Form

    'Limpiar recursos
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador requiere este procedimiento
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dgvRelojes = New DataGridView()
        btnImportarTodo = New Button()
        cmdSalir = New Button()
        btnConectar = New Button()
        cmdImportarSeleccionado = New Button()
        CType(dgvRelojes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvRelojes
        ' 
        dgvRelojes.AllowUserToAddRows = False
        dgvRelojes.AllowUserToDeleteRows = False
        dgvRelojes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvRelojes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRelojes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRelojes.Location = New Point(12, 12)
        dgvRelojes.MultiSelect = False
        dgvRelojes.Name = "dgvRelojes"
        dgvRelojes.ReadOnly = True
        dgvRelojes.RowHeadersVisible = False
        dgvRelojes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRelojes.Size = New Size(776, 300)
        dgvRelojes.TabIndex = 0
        ' 
        ' btnImportarTodo
        ' 
        btnImportarTodo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnImportarTodo.Cursor = Cursors.Hand
        btnImportarTodo.FlatStyle = FlatStyle.Flat
        btnImportarTodo.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnImportarTodo.Location = New Point(168, 332)
        btnImportarTodo.Name = "btnImportarTodo"
        btnImportarTodo.Size = New Size(124, 30)
        btnImportarTodo.TabIndex = 1
        btnImportarTodo.Text = "Importar Todo"
        btnImportarTodo.UseVisualStyleBackColor = True
        ' 
        ' cmdSalir
        ' 
        cmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.Cursor = Cursors.Hand
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(688, 332)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(100, 30)
        cmdSalir.TabIndex = 3
        cmdSalir.Text = "Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' btnConectar
        ' 
        btnConectar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnConectar.Cursor = Cursors.Hand
        btnConectar.FlatStyle = FlatStyle.Flat
        btnConectar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnConectar.Location = New Point(12, 332)
        btnConectar.Name = "btnConectar"
        btnConectar.Size = New Size(150, 30)
        btnConectar.TabIndex = 2
        btnConectar.Text = "Probar Conexiones"
        btnConectar.UseVisualStyleBackColor = True
        ' 
        ' cmdImportarSeleccionado
        ' 
        cmdImportarSeleccionado.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmdImportarSeleccionado.Cursor = Cursors.Hand
        cmdImportarSeleccionado.FlatStyle = FlatStyle.Flat
        cmdImportarSeleccionado.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdImportarSeleccionado.Location = New Point(298, 332)
        cmdImportarSeleccionado.Name = "cmdImportarSeleccionado"
        cmdImportarSeleccionado.Size = New Size(171, 30)
        cmdImportarSeleccionado.TabIndex = 4
        cmdImportarSeleccionado.Text = "Importar Seleccionado"
        cmdImportarSeleccionado.UseVisualStyleBackColor = True
        cmdImportarSeleccionado.Visible = False
        ' 
        ' frmBajaReloj
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 374)
        Controls.Add(cmdImportarSeleccionado)
        Controls.Add(cmdSalir)
        Controls.Add(btnImportarTodo)
        Controls.Add(btnConectar)
        Controls.Add(dgvRelojes)
        MinimizeBox = False
        MinimumSize = New Size(816, 413)
        Name = "frmBajaReloj"
        Text = "Monitoreo de Relojes ZKTeco"
        CType(dgvRelojes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents dgvRelojes As System.Windows.Forms.DataGridView
    Friend WithEvents btnImportarTodo As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As Button
    Friend WithEvents btnConectar As Button
    Friend WithEvents cmdImportarSeleccionado As Button

End Class
