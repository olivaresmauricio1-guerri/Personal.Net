<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDatosAgentes
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
        tabDatosAgentes = New TabControl()
        TabPage1 = New TabPage()
        dgvAgentes = New DataGridView()
        TabPage2 = New TabPage()
        dgvMarcaron = New DataGridView()
        TabPage3 = New TabPage()
        dgvSinMarcar = New DataGridView()
        TabPage4 = New TabPage()
        dgvVacaciones = New DataGridView()
        TabPage5 = New TabPage()
        dgvCumpleMes = New DataGridView()
        TabPage6 = New TabPage()
        dgvIngreso6m = New DataGridView()
        tabDatosAgentes.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(dgvAgentes, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        CType(dgvMarcaron, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        CType(dgvSinMarcar, ComponentModel.ISupportInitialize).BeginInit()
        TabPage4.SuspendLayout()
        CType(dgvVacaciones, ComponentModel.ISupportInitialize).BeginInit()
        TabPage5.SuspendLayout()
        CType(dgvCumpleMes, ComponentModel.ISupportInitialize).BeginInit()
        TabPage6.SuspendLayout()
        CType(dgvIngreso6m, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tabDatosAgentes
        ' 
        tabDatosAgentes.Controls.Add(TabPage1)
        tabDatosAgentes.Controls.Add(TabPage2)
        tabDatosAgentes.Controls.Add(TabPage3)
        tabDatosAgentes.Controls.Add(TabPage4)
        tabDatosAgentes.Controls.Add(TabPage5)
        tabDatosAgentes.Controls.Add(TabPage6)
        tabDatosAgentes.Dock = DockStyle.Fill
        tabDatosAgentes.Location = New Point(0, 0)
        tabDatosAgentes.Name = "tabDatosAgentes"
        tabDatosAgentes.SelectedIndex = 0
        tabDatosAgentes.Size = New Size(737, 756)
        tabDatosAgentes.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(dgvAgentes)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(617, 728)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Agentes"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' dgvAgentes
        ' 
        dgvAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAgentes.Dock = DockStyle.Fill
        dgvAgentes.Location = New Point(3, 3)
        dgvAgentes.Name = "dgvAgentes"
        dgvAgentes.Size = New Size(611, 722)
        dgvAgentes.TabIndex = 0
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(dgvMarcaron)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(617, 728)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Marcaron"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' dgvMarcaron
        ' 
        dgvMarcaron.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMarcaron.Dock = DockStyle.Fill
        dgvMarcaron.Location = New Point(3, 3)
        dgvMarcaron.Name = "dgvMarcaron"
        dgvMarcaron.Size = New Size(611, 722)
        dgvMarcaron.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(dgvSinMarcar)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(617, 728)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Sin Marcar"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' dgvSinMarcar
        ' 
        dgvSinMarcar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSinMarcar.Dock = DockStyle.Fill
        dgvSinMarcar.Location = New Point(3, 3)
        dgvSinMarcar.Name = "dgvSinMarcar"
        dgvSinMarcar.Size = New Size(611, 722)
        dgvSinMarcar.TabIndex = 0
        ' 
        ' TabPage4
        ' 
        TabPage4.Controls.Add(dgvVacaciones)
        TabPage4.Location = New Point(4, 24)
        TabPage4.Name = "TabPage4"
        TabPage4.Padding = New Padding(3)
        TabPage4.Size = New Size(617, 728)
        TabPage4.TabIndex = 3
        TabPage4.Text = "Vacaciones"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' dgvVacaciones
        ' 
        dgvVacaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvVacaciones.Dock = DockStyle.Fill
        dgvVacaciones.Location = New Point(3, 3)
        dgvVacaciones.Name = "dgvVacaciones"
        dgvVacaciones.Size = New Size(611, 722)
        dgvVacaciones.TabIndex = 0
        ' 
        ' TabPage5
        ' 
        TabPage5.Controls.Add(dgvCumpleMes)
        TabPage5.Location = New Point(4, 24)
        TabPage5.Name = "TabPage5"
        TabPage5.Padding = New Padding(3)
        TabPage5.Size = New Size(617, 728)
        TabPage5.TabIndex = 4
        TabPage5.Text = "Cumpleaños del Mes"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' dgvCumpleMes
        ' 
        dgvCumpleMes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCumpleMes.Dock = DockStyle.Fill
        dgvCumpleMes.Location = New Point(3, 3)
        dgvCumpleMes.Name = "dgvCumpleMes"
        dgvCumpleMes.Size = New Size(611, 722)
        dgvCumpleMes.TabIndex = 0
        ' 
        ' TabPage6
        ' 
        TabPage6.Controls.Add(dgvIngreso6m)
        TabPage6.Location = New Point(4, 24)
        TabPage6.Name = "TabPage6"
        TabPage6.Size = New Size(729, 728)
        TabPage6.TabIndex = 5
        TabPage6.Text = "Ingreso 6m"
        TabPage6.UseVisualStyleBackColor = True
        ' 
        ' dgvIngreso6m
        ' 
        dgvIngreso6m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvIngreso6m.Dock = DockStyle.Fill
        dgvIngreso6m.Location = New Point(0, 0)
        dgvIngreso6m.Name = "dgvIngreso6m"
        dgvIngreso6m.Size = New Size(729, 728)
        dgvIngreso6m.TabIndex = 1
        ' 
        ' frmDatosAgentes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(737, 756)
        Controls.Add(tabDatosAgentes)
        MinimizeBox = False
        MinimumSize = New Size(504, 725)
        Name = "frmDatosAgentes"
        Text = "Datos de Agentes a la Fecha"
        tabDatosAgentes.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        CType(dgvAgentes, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        CType(dgvMarcaron, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        CType(dgvSinMarcar, ComponentModel.ISupportInitialize).EndInit()
        TabPage4.ResumeLayout(False)
        CType(dgvVacaciones, ComponentModel.ISupportInitialize).EndInit()
        TabPage5.ResumeLayout(False)
        CType(dgvCumpleMes, ComponentModel.ISupportInitialize).EndInit()
        TabPage6.ResumeLayout(False)
        CType(dgvIngreso6m, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tabDatosAgentes As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents dgvAgentes As DataGridView
    Friend WithEvents dgvMarcaron As DataGridView
    Friend WithEvents dgvSinMarcar As DataGridView
    Friend WithEvents dgvVacaciones As DataGridView
    Friend WithEvents dgvCumpleMes As DataGridView
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents dgvIngreso6m As DataGridView
End Class
