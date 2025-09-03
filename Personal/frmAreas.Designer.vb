<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAreas
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
        TxtDescripcion = New TextBox()
        Label3 = New Label()
        CmdBorrar = New Button()
        chkEncabezados = New CheckBox()
        CmdModificar = New Button()
        lnkCopiar = New LinkLabel()
        CmdAgregar = New Button()
        TxtPrincipal = New TextBox()
        Label2 = New Label()
        Label1 = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        CmdSalir = New Button()
        CmdCancelar = New Button()
        CmdAceptar = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Location = New Point(228, 335)
        TxtDescripcion.Margin = New Padding(4, 3, 4, 3)
        TxtDescripcion.Multiline = True
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(343, 23)
        TxtDescripcion.TabIndex = 12
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(148, 338)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(72, 15)
        Label3.TabIndex = 16
        Label3.Text = "Descripción:"
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(175, 379)
        CmdBorrar.Margin = New Padding(4, 3, 4, 3)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 30)
        CmdBorrar.TabIndex = 13
        CmdBorrar.Text = "&Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(444, 291)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 19
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' CmdModificar
        ' 
        CmdModificar.FlatStyle = FlatStyle.Flat
        CmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdModificar.Location = New Point(92, 379)
        CmdModificar.Margin = New Padding(4, 3, 4, 3)
        CmdModificar.Name = "CmdModificar"
        CmdModificar.Size = New Size(75, 30)
        CmdModificar.TabIndex = 9
        CmdModificar.Text = "&Modificar"
        CmdModificar.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(320, 292)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 17
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(9, 379)
        CmdAgregar.Margin = New Padding(4, 3, 4, 3)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 30)
        CmdAgregar.TabIndex = 6
        CmdAgregar.Text = "&Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' TxtPrincipal
        ' 
        TxtPrincipal.Location = New Point(76, 335)
        TxtPrincipal.Margin = New Padding(4, 3, 4, 3)
        TxtPrincipal.Name = "TxtPrincipal"
        TxtPrincipal.Size = New Size(59, 23)
        TxtPrincipal.TabIndex = 7
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 339)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 15)
        Label2.TabIndex = 10
        Label2.Text = "Principal:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(13, 12)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 14
        Label1.Text = "Buscar:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(70, 9)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(501, 23)
        TxtBuscar.TabIndex = 11
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(13, 39)
        DgvListado.Margin = New Padding(4, 3, 4, 3)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.Size = New Size(558, 246)
        DgvListado.TabIndex = 8
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(497, 379)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 23
        CmdSalir.Text = "&Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(414, 379)
        CmdCancelar.Margin = New Padding(4, 3, 4, 3)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 30)
        CmdCancelar.TabIndex = 22
        CmdCancelar.Text = "&Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(331, 379)
        CmdAceptar.Margin = New Padding(4, 3, 4, 3)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 21
        CmdAceptar.Text = "&Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' frmAreas
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(584, 418)
        Controls.Add(CmdSalir)
        Controls.Add(CmdCancelar)
        Controls.Add(CmdAceptar)
        Controls.Add(TxtDescripcion)
        Controls.Add(Label3)
        Controls.Add(CmdBorrar)
        Controls.Add(chkEncabezados)
        Controls.Add(CmdModificar)
        Controls.Add(lnkCopiar)
        Controls.Add(CmdAgregar)
        Controls.Add(TxtPrincipal)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TxtBuscar)
        Controls.Add(DgvListado)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(600, 457)
        Name = "frmAreas"
        Text = "Nomenclador - Áreas"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents TxtDescripcion As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents CmdModificar As Button
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents TxtPrincipal As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdAceptar As Button
End Class
