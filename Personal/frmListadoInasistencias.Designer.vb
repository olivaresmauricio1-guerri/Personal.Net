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
        lblBuscar = New Label()
        txtAnio = New TextBox()
        cmbMes = New ComboBox()
        lblMes = New Label()
        cmdSalir = New Button()
        cmdVer = New Button()
        SuspendLayout()
        ' 
        ' lblBuscar
        ' 
        lblBuscar.AutoSize = True
        lblBuscar.Location = New Point(180, 15)
        lblBuscar.Name = "lblBuscar"
        lblBuscar.Size = New Size(32, 15)
        lblBuscar.TabIndex = 8
        lblBuscar.Text = "Año:"
        ' 
        ' txtAnio
        ' 
        txtAnio.Location = New Point(218, 12)
        txtAnio.Name = "txtAnio"
        txtAnio.Size = New Size(64, 23)
        txtAnio.TabIndex = 9
        ' 
        ' cmbMes
        ' 
        cmbMes.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMes.Location = New Point(47, 12)
        cmbMes.Name = "cmbMes"
        cmbMes.Size = New Size(115, 23)
        cmbMes.TabIndex = 7
        ' 
        ' lblMes
        ' 
        lblMes.AutoSize = True
        lblMes.Location = New Point(9, 15)
        lblMes.Name = "lblMes"
        lblMes.Size = New Size(32, 15)
        lblMes.TabIndex = 6
        lblMes.Text = "Mes:"
        ' 
        ' cmdSalir
        ' 
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(196, 61)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(88, 30)
        cmdSalir.TabIndex = 12
        cmdSalir.Text = "Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdVer
        ' 
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdVer.Location = New Point(100, 60)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(88, 30)
        cmdVer.TabIndex = 13
        cmdVer.Text = "Ver"
        cmdVer.UseVisualStyleBackColor = True
        ' 
        ' frmListadoInasistencias
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(293, 102)
        Controls.Add(cmdVer)
        Controls.Add(cmdSalir)
        Controls.Add(txtAnio)
        Controls.Add(lblBuscar)
        Controls.Add(cmbMes)
        Controls.Add(lblMes)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmListadoInasistencias"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Listado de Inasistencias"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents cmbMes As System.Windows.Forms.ComboBox
    Friend WithEvents txtAnio As System.Windows.Forms.TextBox
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdVer As System.Windows.Forms.Button
    Friend WithEvents lblBuscar As System.Windows.Forms.Label
    Friend WithEvents lblMes As System.Windows.Forms.Label

End Class