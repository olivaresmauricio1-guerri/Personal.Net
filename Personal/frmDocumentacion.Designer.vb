<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocumentacion
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
        grpConsultas = New GroupBox()
        lstArchivos = New ListBox()
        grpImportaciones = New GroupBox()
        rbOtrosDatos = New RadioButton()
        rbComprobanteAlta = New RadioButton()
        rbCurriculum = New RadioButton()
        lblTipoArchivo = New Label()
        btnSubirArchivo = New Button()
        btnEliminar = New Button()
        btnSalir = New Button()
        openFileDialog = New OpenFileDialog()
        grpConsultas.SuspendLayout()
        grpImportaciones.SuspendLayout()
        SuspendLayout()
        ' 
        ' grpConsultas
        ' 
        grpConsultas.Controls.Add(lstArchivos)
        grpConsultas.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        grpConsultas.ForeColor = Color.Navy
        grpConsultas.Location = New Point(6, 6)
        grpConsultas.Name = "grpConsultas"
        grpConsultas.Size = New Size(323, 274)
        grpConsultas.TabIndex = 0
        grpConsultas.TabStop = False
        grpConsultas.Text = "Consultas - Doble click para abrir"
        ' 
        ' lstArchivos
        ' 
        lstArchivos.Font = New Font("Microsoft Sans Serif", 9F)
        lstArchivos.FormattingEnabled = True
        lstArchivos.ItemHeight = 15
        lstArchivos.Location = New Point(6, 25)
        lstArchivos.Name = "lstArchivos"
        lstArchivos.Size = New Size(311, 244)
        lstArchivos.TabIndex = 0
        ' 
        ' grpImportaciones
        ' 
        grpImportaciones.Controls.Add(rbOtrosDatos)
        grpImportaciones.Controls.Add(rbComprobanteAlta)
        grpImportaciones.Controls.Add(rbCurriculum)
        grpImportaciones.Controls.Add(lblTipoArchivo)
        grpImportaciones.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        grpImportaciones.ForeColor = Color.Navy
        grpImportaciones.Location = New Point(6, 286)
        grpImportaciones.Name = "grpImportaciones"
        grpImportaciones.Size = New Size(323, 125)
        grpImportaciones.TabIndex = 1
        grpImportaciones.TabStop = False
        grpImportaciones.Text = "Importaciones"
        ' 
        ' rbOtrosDatos
        ' 
        rbOtrosDatos.AutoSize = True
        rbOtrosDatos.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rbOtrosDatos.ForeColor = Color.Black
        rbOtrosDatos.Location = New Point(30, 95)
        rbOtrosDatos.Name = "rbOtrosDatos"
        rbOtrosDatos.Size = New Size(87, 19)
        rbOtrosDatos.TabIndex = 3
        rbOtrosDatos.TabStop = True
        rbOtrosDatos.Text = "Otros Datos"
        rbOtrosDatos.UseVisualStyleBackColor = True
        ' 
        ' rbComprobanteAlta
        ' 
        rbComprobanteAlta.AutoSize = True
        rbComprobanteAlta.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rbComprobanteAlta.ForeColor = Color.Black
        rbComprobanteAlta.Location = New Point(30, 73)
        rbComprobanteAlta.Name = "rbComprobanteAlta"
        rbComprobanteAlta.Size = New Size(139, 19)
        rbComprobanteAlta.TabIndex = 2
        rbComprobanteAlta.TabStop = True
        rbComprobanteAlta.Text = "Comprobante de Alta"
        rbComprobanteAlta.UseVisualStyleBackColor = True
        ' 
        ' rbCurriculum
        ' 
        rbCurriculum.AutoSize = True
        rbCurriculum.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rbCurriculum.ForeColor = Color.Black
        rbCurriculum.Location = New Point(30, 52)
        rbCurriculum.Name = "rbCurriculum"
        rbCurriculum.Size = New Size(85, 19)
        rbCurriculum.TabIndex = 1
        rbCurriculum.TabStop = True
        rbCurriculum.Text = "Currículum"
        rbCurriculum.UseVisualStyleBackColor = True
        ' 
        ' lblTipoArchivo
        ' 
        lblTipoArchivo.AutoSize = True
        lblTipoArchivo.Font = New Font("Microsoft Sans Serif", 9F)
        lblTipoArchivo.ForeColor = Color.Black
        lblTipoArchivo.Location = New Point(15, 28)
        lblTipoArchivo.Name = "lblTipoArchivo"
        lblTipoArchivo.Size = New Size(225, 15)
        lblTipoArchivo.TabIndex = 0
        lblTipoArchivo.Text = "Seleccione el tipo de archivo a importar:"
        ' 
        ' btnSubirArchivo
        ' 
        btnSubirArchivo.FlatStyle = FlatStyle.Flat
        btnSubirArchivo.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        btnSubirArchivo.Location = New Point(6, 426)
        btnSubirArchivo.Name = "btnSubirArchivo"
        btnSubirArchivo.Size = New Size(135, 30)
        btnSubirArchivo.TabIndex = 4
        btnSubirArchivo.Text = "Subir Archivo PDF"
        btnSubirArchivo.UseVisualStyleBackColor = True
        ' 
        ' btnEliminar
        ' 
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnEliminar.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        btnEliminar.Location = New Point(147, 426)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New Size(88, 30)
        btnEliminar.TabIndex = 2
        btnEliminar.Text = "Eliminar"
        btnEliminar.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.BackColor = Color.IndianRed
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(241, 426)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(88, 30)
        btnSalir.TabIndex = 3
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' openFileDialog
        ' 
        openFileDialog.Filter = "Archivos PDF|*.pdf"
        openFileDialog.Title = "Seleccionar archivo PDF"
        ' 
        ' frmDocumentacion
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(332, 466)
        Controls.Add(btnSubirArchivo)
        Controls.Add(btnSalir)
        Controls.Add(btnEliminar)
        Controls.Add(grpImportaciones)
        Controls.Add(grpConsultas)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MaximumSize = New Size(348, 505)
        MinimizeBox = False
        Name = "frmDocumentacion"
        StartPosition = FormStartPosition.CenterParent
        Text = "Documentación"
        grpConsultas.ResumeLayout(False)
        grpImportaciones.ResumeLayout(False)
        grpImportaciones.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents grpConsultas As GroupBox
    Friend WithEvents lstArchivos As ListBox
    Friend WithEvents grpImportaciones As GroupBox
    Friend WithEvents lblTipoArchivo As Label
    Friend WithEvents rbCurriculum As RadioButton
    Friend WithEvents rbComprobanteAlta As RadioButton
    Friend WithEvents rbOtrosDatos As RadioButton
    Friend WithEvents btnSubirArchivo As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnSalir As Button
    Friend WithEvents openFileDialog As OpenFileDialog
End Class
