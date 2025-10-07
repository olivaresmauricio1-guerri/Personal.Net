<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLstAgentes
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
        Label1 = New Label()
        cmbListado = New ComboBox()
        CmdVer = New Button()
        CmdSalir = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(70, 15)
        Label1.TabIndex = 0
        Label1.Text = "Seleccione:"
        ' 
        ' cmbListado
        ' 
        cmbListado.FormattingEnabled = True
        cmbListado.Items.AddRange(New Object() {"Analítico", "Resumen por Sucursal", "Grupo Familiar"})
        cmbListado.Location = New Point(88, 6)
        cmbListado.Name = "cmbListado"
        cmbListado.Size = New Size(182, 23)
        cmbListado.TabIndex = 1
        ' 
        ' CmdVer
        ' 
        CmdVer.FlatStyle = FlatStyle.Flat
        CmdVer.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdVer.Location = New Point(88, 47)
        CmdVer.Name = "CmdVer"
        CmdVer.Size = New Size(88, 30)
        CmdVer.TabIndex = 2
        CmdVer.Text = "Ver"
        CmdVer.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(182, 47)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(88, 30)
        CmdSalir.TabIndex = 3
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmLstAgentes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(291, 89)
        Controls.Add(CmdSalir)
        Controls.Add(CmdVer)
        Controls.Add(cmbListado)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLstAgentes"
        Text = "Listado de Agentes"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cmbListado As ComboBox
    Friend WithEvents CmdVer As Button
    Friend WithEvents CmdSalir As Button
End Class
