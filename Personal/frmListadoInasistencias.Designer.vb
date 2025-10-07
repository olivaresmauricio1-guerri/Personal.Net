<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListadoInasistencias
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblBuscar = New System.Windows.Forms.Label()
        Me.txtAnio = New System.Windows.Forms.TextBox()
        Me.cmbMes = New System.Windows.Forms.ComboBox()
        Me.lblMes = New System.Windows.Forms.Label()
        Me.cmdSalir = New System.Windows.Forms.Button()
        Me.cmdVer = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        ' 
        ' lblBuscar
        ' 
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.Location = New System.Drawing.Point(180, 15)
        Me.lblBuscar.Name = "lblBuscar"
        Me.lblBuscar.Size = New System.Drawing.Size(31, 15)
        Me.lblBuscar.TabIndex = 8
        Me.lblBuscar.Text = "Año:"
        ' 
        ' txtAnio
        ' 
        Me.txtAnio.Location = New System.Drawing.Point(218, 12)
        Me.txtAnio.Name = "txtAnio"
        Me.txtAnio.Size = New System.Drawing.Size(64, 23)
        Me.txtAnio.TabIndex = 9
        ' 
        ' cmbMes
        ' 
        Me.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMes.Location = New System.Drawing.Point(47, 12)
        Me.cmbMes.Name = "cmbMes"
        Me.cmbMes.Size = New System.Drawing.Size(115, 23)
        Me.cmbMes.TabIndex = 7
        ' 
        ' lblMes
        ' 
        Me.lblMes.AutoSize = True
        Me.lblMes.Location = New System.Drawing.Point(9, 15)
        Me.lblMes.Name = "lblMes"
        Me.lblMes.Size = New System.Drawing.Size(33, 15)
        Me.lblMes.TabIndex = 6
        Me.lblMes.Text = "Mes:"
        ' 
        ' cmdSalir
        ' 
        Me.cmdSalir.BackColor = System.Drawing.Color.IndianRed
        Me.cmdSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSalir.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSalir.ForeColor = System.Drawing.Color.White
        Me.cmdSalir.Location = New System.Drawing.Point(196, 61)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(88, 30)
        Me.cmdSalir.TabIndex = 12
        Me.cmdSalir.Text = "Salir"
        Me.cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdVer
        ' 
        Me.cmdVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdVer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdVer.Location = New System.Drawing.Point(100, 60)
        Me.cmdVer.Name = "cmdVer"
        Me.cmdVer.Size = New System.Drawing.Size(88, 30)
        Me.cmdVer.TabIndex = 13
        Me.cmdVer.Text = "Ver"
        Me.cmdVer.UseVisualStyleBackColor = True
        ' 
        ' frmListadoInasistencias
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 102)
        Me.Controls.Add(Me.cmdVer)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.txtAnio)
        Me.Controls.Add(Me.lblBuscar)
        Me.Controls.Add(Me.cmbMes)
        Me.Controls.Add(Me.lblMes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmListadoInasistencias"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Listado de Inasistencias"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbMes As System.Windows.Forms.ComboBox
    Friend WithEvents txtAnio As System.Windows.Forms.TextBox
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdVer As System.Windows.Forms.Button
    Friend WithEvents lblBuscar As System.Windows.Forms.Label
    Friend WithEvents lblMes As System.Windows.Forms.Label

End Class