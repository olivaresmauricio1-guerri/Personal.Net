<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHorario
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dtpDesde = New DateTimePicker()
        lblDesde = New Label()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        cmbSucursal = New ComboBox()
        lblSucursal = New Label()
        chkTarde = New CheckBox()
        btnSalir = New Button()
        cmdVer = New Button()
        radNormal = New RadioButton()
        radAgente = New RadioButton()
        radSemana = New RadioButton()
        SuspendLayout()
        ' 
        ' dtpDesde
        ' 
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(12, 30)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(114, 23)
        dtpDesde.TabIndex = 0
        ' 
        ' lblDesde
        ' 
        lblDesde.AutoSize = True
        lblDesde.Location = New Point(12, 9)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(42, 15)
        lblDesde.TabIndex = 1
        lblDesde.Text = "Desde:"
        ' 
        ' lblHasta
        ' 
        lblHasta.AutoSize = True
        lblHasta.Location = New Point(132, 9)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(40, 15)
        lblHasta.TabIndex = 3
        lblHasta.Text = "Hasta:"
        ' 
        ' dtpHasta
        ' 
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(132, 30)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(114, 23)
        dtpHasta.TabIndex = 2
        ' 
        ' cmbSucursal
        ' 
        cmbSucursal.FormattingEnabled = True
        cmbSucursal.Location = New Point(252, 30)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(221, 23)
        cmbSucursal.TabIndex = 4
        ' 
        ' lblSucursal
        ' 
        lblSucursal.AutoSize = True
        lblSucursal.Location = New Point(252, 9)
        lblSucursal.Name = "lblSucursal"
        lblSucursal.Size = New Size(54, 15)
        lblSucursal.TabIndex = 5
        lblSucursal.Text = "Sucursal:"
        ' 
        ' chkTarde
        ' 
        chkTarde.AutoSize = True
        chkTarde.Location = New Point(12, 96)
        chkTarde.Name = "chkTarde"
        chkTarde.Size = New Size(102, 19)
        chkTarde.TabIndex = 6
        chkTarde.Text = "Sólo tardanzas"
        chkTarde.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(388, 96)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(85, 30)
        btnSalir.TabIndex = 7
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdVer
        ' 
        cmdVer.BackColor = SystemColors.Control
        cmdVer.Cursor = Cursors.Hand
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        cmdVer.ForeColor = SystemColors.ControlText
        cmdVer.Location = New Point(297, 96)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(85, 30)
        cmdVer.TabIndex = 8
        cmdVer.Text = "Ver"
        cmdVer.UseVisualStyleBackColor = False
        ' 
        ' radNormal
        ' 
        radNormal.AutoSize = True
        radNormal.Location = New Point(12, 63)
        radNormal.Name = "radNormal"
        radNormal.Size = New Size(93, 19)
        radNormal.TabIndex = 9
        radNormal.TabStop = True
        radNormal.Text = "Vista Normal"
        radNormal.UseVisualStyleBackColor = True
        ' 
        ' radAgente
        ' 
        radAgente.AutoSize = True
        radAgente.Location = New Point(123, 63)
        radAgente.Name = "radAgente"
        radAgente.Size = New Size(110, 19)
        radAgente.TabIndex = 10
        radAgente.TabStop = True
        radAgente.Text = "Hoja por agente"
        radAgente.UseVisualStyleBackColor = True
        ' 
        ' radSemana
        ' 
        radSemana.AutoSize = True
        radSemana.Location = New Point(251, 63)
        radSemana.Name = "radSemana"
        radSemana.Size = New Size(115, 19)
        radSemana.TabIndex = 11
        radSemana.TabStop = True
        radSemana.Text = "Hoja por semana"
        radSemana.UseVisualStyleBackColor = True
        ' 
        ' frmHorario
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(482, 139)
        Controls.Add(radSemana)
        Controls.Add(radAgente)
        Controls.Add(radNormal)
        Controls.Add(cmdVer)
        Controls.Add(btnSalir)
        Controls.Add(chkTarde)
        Controls.Add(lblSucursal)
        Controls.Add(cmbSucursal)
        Controls.Add(lblHasta)
        Controls.Add(dtpHasta)
        Controls.Add(lblDesde)
        Controls.Add(dtpDesde)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmHorario"
        Text = "Listado de Control Horario por Día"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblDesde As Label
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents lblSucursal As Label
    Friend WithEvents chkTarde As CheckBox
    Friend WithEvents btnSalir As Button
    Friend WithEvents cmdVer As Button
    Friend WithEvents radNormal As RadioButton
    Friend WithEvents radAgente As RadioButton
    Friend WithEvents radSemana As RadioButton
End Class
