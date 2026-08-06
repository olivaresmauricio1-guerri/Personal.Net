<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnvioRecibos
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
        btnCargaRecibos = New Button()
        dgvListaRecibos = New DataGridView()
        dtpPeriodo = New DateTimePicker()
        Label1 = New Label()
        dgvProcesos = New DataGridView()
        btnSalir = New Button()
        btnEnviar = New Button()
        rdbTodos = New RadioButton()
        rdbSeleccion = New RadioButton()
        CType(dgvListaRecibos, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvProcesos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnCargaRecibos
        ' 
        btnCargaRecibos.FlatStyle = FlatStyle.Flat
        btnCargaRecibos.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCargaRecibos.Location = New Point(6, 442)
        btnCargaRecibos.Name = "btnCargaRecibos"
        btnCargaRecibos.Size = New Size(88, 30)
        btnCargaRecibos.TabIndex = 0
        btnCargaRecibos.Text = "Cargar"
        btnCargaRecibos.UseVisualStyleBackColor = True
        ' 
        ' dgvListaRecibos
        ' 
        dgvListaRecibos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvListaRecibos.Location = New Point(261, 41)
        dgvListaRecibos.Name = "dgvListaRecibos"
        dgvListaRecibos.Size = New Size(871, 390)
        dgvListaRecibos.TabIndex = 1
        ' 
        ' dtpPeriodo
        ' 
        dtpPeriodo.Format = DateTimePickerFormat.Short
        dtpPeriodo.Location = New Point(119, 12)
        dtpPeriodo.Name = "dtpPeriodo"
        dtpPeriodo.Size = New Size(100, 23)
        dtpPeriodo.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 16)
        Label1.Name = "Label1"
        Label1.Size = New Size(107, 15)
        Label1.TabIndex = 3
        Label1.Text = "Período Liquidado:"
        ' 
        ' dgvProcesos
        ' 
        dgvProcesos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProcesos.Location = New Point(6, 41)
        dgvProcesos.Name = "dgvProcesos"
        dgvProcesos.ReadOnly = True
        dgvProcesos.Size = New Size(249, 390)
        dgvProcesos.TabIndex = 4
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(1035, 442)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 30)
        btnSalir.TabIndex = 42
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnEnviar
        ' 
        btnEnviar.FlatStyle = FlatStyle.Flat
        btnEnviar.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnEnviar.Location = New Point(100, 442)
        btnEnviar.Name = "btnEnviar"
        btnEnviar.Size = New Size(88, 30)
        btnEnviar.TabIndex = 43
        btnEnviar.Text = "Enviar"
        btnEnviar.UseVisualStyleBackColor = True
        ' 
        ' rdbTodos
        ' 
        rdbTodos.AutoSize = True
        rdbTodos.Checked = True
        rdbTodos.Location = New Point(210, 448)
        rdbTodos.Name = "rdbTodos"
        rdbTodos.Size = New Size(57, 19)
        rdbTodos.TabIndex = 44
        rdbTodos.TabStop = True
        rdbTodos.Text = "Todos"
        rdbTodos.UseVisualStyleBackColor = True
        ' 
        ' rdbSeleccion
        ' 
        rdbSeleccion.AutoSize = True
        rdbSeleccion.Location = New Point(273, 448)
        rdbSeleccion.Name = "rdbSeleccion"
        rdbSeleccion.Size = New Size(75, 19)
        rdbSeleccion.TabIndex = 45
        rdbSeleccion.Text = "Selección"
        rdbSeleccion.UseVisualStyleBackColor = True
        ' 
        ' frmEnvioRecibos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1136, 484)
        Controls.Add(rdbSeleccion)
        Controls.Add(rdbTodos)
        Controls.Add(btnEnviar)
        Controls.Add(btnSalir)
        Controls.Add(dgvProcesos)
        Controls.Add(Label1)
        Controls.Add(dtpPeriodo)
        Controls.Add(dgvListaRecibos)
        Controls.Add(btnCargaRecibos)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "frmEnvioRecibos"
        Text = "Distribución de Recibos de Sueldos"
        CType(dgvListaRecibos, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvProcesos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnCargaRecibos As Button
    Friend WithEvents dgvListaRecibos As DataGridView
    Friend WithEvents dtpPeriodo As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvProcesos As DataGridView
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnEnviar As Button
    Friend WithEvents rdbTodos As RadioButton
    Friend WithEvents rdbSeleccion As RadioButton
End Class
