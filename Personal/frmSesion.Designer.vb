<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSesion
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
        Frame1 = New GroupBox()
        Label2 = New Label()
        btnAceptar = New Button()
        btnCancelar = New Button()
        Label1 = New Label()
        txtPassword = New TextBox()
        TxtUsuario = New TextBox()
        Frame1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Frame1
        ' 
        Frame1.Controls.Add(Label2)
        Frame1.Controls.Add(btnAceptar)
        Frame1.Controls.Add(btnCancelar)
        Frame1.Controls.Add(Label1)
        Frame1.Controls.Add(txtPassword)
        Frame1.Controls.Add(TxtUsuario)
        Frame1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Frame1.Location = New Point(9, 0)
        Frame1.Name = "Frame1"
        Frame1.Size = New Size(226, 138)
        Frame1.TabIndex = 0
        Frame1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 9.0F)
        Label2.ForeColor = SystemColors.ControlText
        Label2.Location = New Point(9, 64)
        Label2.Name = "Label2"
        Label2.Size = New Size(70, 19)
        Label2.TabIndex = 3
        Label2.Text = "Contraseña:"
        ' 
        ' btnAceptar
        ' 
        btnAceptar.BackColor = Color.SteelBlue
        btnAceptar.Cursor = Cursors.Hand
        btnAceptar.FlatAppearance.BorderSize = 0
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnAceptar.ForeColor = Color.White
        btnAceptar.Location = New Point(9, 101)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New Size(95, 30)
        btnAceptar.TabIndex = 5
        btnAceptar.Text = "Conectar"
        btnAceptar.UseVisualStyleBackColor = False
        ' 
        ' btnCancelar
        ' 
        btnCancelar.BackColor = Color.IndianRed
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.FlatAppearance.BorderSize = 0
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnCancelar.ForeColor = Color.White
        btnCancelar.Location = New Point(122, 101)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(95, 30)
        btnCancelar.TabIndex = 6
        btnCancelar.Text = "&Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 9.0F)
        Label1.ForeColor = SystemColors.ControlText
        Label1.Location = New Point(27, 29)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 19)
        Label1.TabIndex = 1
        Label1.Text = "Usuario:"
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Color.White
        txtPassword.BorderStyle = BorderStyle.FixedSingle
        txtPassword.Font = New Font("Segoe UI", 9.0F)
        txtPassword.ForeColor = Color.Black
        txtPassword.Location = New Point(83, 62)
        txtPassword.MaxLength = 20
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(134, 23)
        txtPassword.TabIndex = 4
        ' 
        ' TxtUsuario
        ' 
        TxtUsuario.BackColor = Color.White
        TxtUsuario.BorderStyle = BorderStyle.FixedSingle
        TxtUsuario.Font = New Font("Segoe UI", 9.0F)
        TxtUsuario.ForeColor = Color.Black
        TxtUsuario.Location = New Point(83, 27)
        TxtUsuario.MaxLength = 10
        TxtUsuario.Name = "TxtUsuario"
        TxtUsuario.Size = New Size(134, 23)
        TxtUsuario.TabIndex = 2
        ' 
        ' frmSesion
        ' 
        AcceptButton = btnAceptar
        AutoScaleDimensions = New SizeF(7.0F, 13.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(243, 145)
        Controls.Add(Frame1)
        Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSesion"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Inicio de Sesión"
        Frame1.ResumeLayout(False)
        Frame1.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents Frame1 As GroupBox
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnAceptar As Button
    Friend WithEvents TxtUsuario As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class