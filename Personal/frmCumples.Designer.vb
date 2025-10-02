<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCumples
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
        GroupBox1 = New GroupBox()
        CmdSalir = New Button()
        CmdVer = New Button()
        Label1 = New Label()
        CmbMeses = New ComboBox()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CmdSalir)
        GroupBox1.Controls.Add(CmdVer)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(CmbMeses)
        GroupBox1.Location = New Point(1, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(203, 104)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Seleccione un Mes"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(105, 67)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(88, 30)
        CmdSalir.TabIndex = 4
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdVer
        ' 
        CmdVer.FlatStyle = FlatStyle.Flat
        CmdVer.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdVer.Location = New Point(11, 67)
        CmdVer.Name = "CmdVer"
        CmdVer.Size = New Size(88, 30)
        CmdVer.TabIndex = 3
        CmdVer.Text = "Ver"
        CmdVer.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(11, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(32, 15)
        Label1.TabIndex = 2
        Label1.Text = "Mes:"
        ' 
        ' CmbMeses
        ' 
        CmbMeses.FormattingEnabled = True
        CmbMeses.Location = New Point(58, 27)
        CmbMeses.Name = "CmbMeses"
        CmbMeses.Size = New Size(135, 23)
        CmbMeses.TabIndex = 1
        ' 
        ' frmCumples
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(210, 112)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCumples"
        Text = "Cumpleaños del Mes"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdVer As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbMeses As ComboBox
End Class
