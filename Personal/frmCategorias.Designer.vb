<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCategorias
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
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        CmdSalir = New Button()
        CmdCancelar = New Button()
        CmdAceptar = New Button()
        CmdBorrar = New Button()
        CmdModificar = New Button()
        CmdAgregar = New Button()
        Label6 = New Label()
        TxtDescripcion = New TextBox()
        Label2 = New Label()
        TxtIdCategoria = New TextBox()
        Label1 = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(446, 295)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 3
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(339, 296)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 2
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(492, 392)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 11
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(407, 392)
        CmdCancelar.Margin = New Padding(4, 3, 4, 3)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 30)
        CmdCancelar.TabIndex = 10
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(324, 392)
        CmdAceptar.Margin = New Padding(4, 3, 4, 3)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 9
        CmdAceptar.Text = "Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(171, 391)
        CmdBorrar.Margin = New Padding(4, 3, 4, 3)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 30)
        CmdBorrar.TabIndex = 8
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' CmdModificar
        ' 
        CmdModificar.FlatStyle = FlatStyle.Flat
        CmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdModificar.Location = New Point(88, 391)
        CmdModificar.Margin = New Padding(4, 3, 4, 3)
        CmdModificar.Name = "CmdModificar"
        CmdModificar.Size = New Size(75, 30)
        CmdModificar.TabIndex = 7
        CmdModificar.Text = "Modificar"
        CmdModificar.UseVisualStyleBackColor = True
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(5, 391)
        CmdAgregar.Margin = New Padding(4, 3, 4, 3)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 30)
        CmdAgregar.TabIndex = 6
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(147, 344)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(72, 15)
        Label6.TabIndex = 21
        Label6.Text = "Descripción:"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Location = New Point(227, 339)
        TxtDescripcion.Margin = New Padding(4, 3, 4, 3)
        TxtDescripcion.Multiline = True
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(338, 24)
        TxtDescripcion.TabIndex = 20
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(4, 344)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(86, 15)
        Label2.TabIndex = 13
        Label2.Text = "Cod Categoría:"
        ' 
        ' TxtIdCategoria
        ' 
        TxtIdCategoria.Enabled = False
        TxtIdCategoria.Location = New Point(94, 340)
        TxtIdCategoria.Margin = New Padding(4, 3, 4, 3)
        TxtIdCategoria.Name = "TxtIdCategoria"
        TxtIdCategoria.Size = New Size(40, 23)
        TxtIdCategoria.TabIndex = 12
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(8, 15)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 23
        Label1.Text = "Buscar:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(65, 12)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(502, 23)
        TxtBuscar.TabIndex = 22
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(7, 41)
        DgvListado.Margin = New Padding(4, 3, 4, 3)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.Size = New Size(560, 248)
        DgvListado.TabIndex = 24
        ' 
        ' frmCategorias
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(578, 441)
        Controls.Add(DgvListado)
        Controls.Add(Label1)
        Controls.Add(TxtBuscar)
        Controls.Add(Label6)
        Controls.Add(TxtDescripcion)
        Controls.Add(Label2)
        Controls.Add(TxtIdCategoria)
        Controls.Add(chkEncabezados)
        Controls.Add(lnkCopiar)
        Controls.Add(CmdSalir)
        Controls.Add(CmdCancelar)
        Controls.Add(CmdAceptar)
        Controls.Add(CmdBorrar)
        Controls.Add(CmdModificar)
        Controls.Add(CmdAgregar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCategorias"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Nomenclador - Categorías"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents CmdModificar As Button
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtDescripcion As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtIdCategoria As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView

End Class
