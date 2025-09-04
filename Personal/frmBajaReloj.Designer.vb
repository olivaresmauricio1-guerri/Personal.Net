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
        btnImportar = New Button()
        btnConectar = New Button()
        chkMonitoreo = New CheckBox()
        CType(dgvRelojes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvRelojes
        ' 
        dgvRelojes.AllowUserToAddRows = False
        dgvRelojes.AllowUserToDeleteRows = False
        dgvRelojes.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        dgvRelojes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRelojes.BackgroundColor = SystemColors.Window
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
        ' btnImportar
        ' 
        btnImportar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnImportar.Location = New Point(158, 326)
        btnImportar.Name = "btnImportar"
        btnImportar.Size = New Size(140, 36)
        btnImportar.TabIndex = 1
        btnImportar.Text = "Importar logs"
        btnImportar.UseVisualStyleBackColor = True
        ' 
        ' btnConectar
        ' 
        btnConectar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnConectar.Location = New Point(12, 326)
        btnConectar.Name = "btnConectar"
        btnConectar.Size = New Size(140, 36)
        btnConectar.TabIndex = 2
        btnConectar.Text = "Conectar"
        btnConectar.UseVisualStyleBackColor = True
        ' 
        ' chkMonitoreo
        ' 
        chkMonitoreo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        chkMonitoreo.AutoSize = True
        chkMonitoreo.Location = New Point(316, 335)
        chkMonitoreo.Name = "chkMonitoreo"
        chkMonitoreo.Size = New Size(132, 19)
        chkMonitoreo.TabIndex = 3
        chkMonitoreo.Text = "Monitoreo (cada 5s)"
        chkMonitoreo.UseVisualStyleBackColor = True
        ' 
        ' frmBajaReloj
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 374)
        Controls.Add(btnImportar)
        Controls.Add(btnConectar)
        Controls.Add(chkMonitoreo)
        Controls.Add(dgvRelojes)
        Name = "frmBajaReloj"
        Text = "Monitoreo de Relojes ZKTeco"
        CType(dgvRelojes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents dgvRelojes As System.Windows.Forms.DataGridView
    Friend WithEvents btnImportar As System.Windows.Forms.Button
    Friend WithEvents btnConectar As System.Windows.Forms.Button
    Friend WithEvents chkMonitoreo As System.Windows.Forms.CheckBox

End Class
