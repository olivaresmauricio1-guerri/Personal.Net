<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEncargados
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
        TxtTelefono = New TextBox()
        Label5 = New Label()
        TxtOficina = New TextBox()
        Label4 = New Label()
        TxtEncargado = New TextBox()
        Label3 = New Label()
        cmbSucursal = New ComboBox()
        Label2 = New Label()
        TxtId = New TextBox()
        Label1 = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(661, 295)
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
        lnkCopiar.Location = New Point(554, 296)
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
        CmdSalir.Location = New Point(697, 420)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 15
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(612, 420)
        CmdCancelar.Margin = New Padding(4, 3, 4, 3)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 30)
        CmdCancelar.TabIndex = 14
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(529, 420)
        CmdAceptar.Margin = New Padding(4, 3, 4, 3)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 13
        CmdAceptar.Text = "Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(171, 420)
        CmdBorrar.Margin = New Padding(4, 3, 4, 3)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 30)
        CmdBorrar.TabIndex = 12
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' CmdModificar
        ' 
        CmdModificar.FlatStyle = FlatStyle.Flat
        CmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdModificar.Location = New Point(88, 420)
        CmdModificar.Margin = New Padding(4, 3, 4, 3)
        CmdModificar.Name = "CmdModificar"
        CmdModificar.Size = New Size(75, 30)
        CmdModificar.TabIndex = 11
        CmdModificar.Text = "Modificar"
        CmdModificar.UseVisualStyleBackColor = True
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(5, 420)
        CmdAgregar.Margin = New Padding(4, 3, 4, 3)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 30)
        CmdAgregar.TabIndex = 10
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(579, 332)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(56, 15)
        Label6.TabIndex = 29
        Label6.Text = "Teléfono:"
        ' 
        ' TxtTelefono
        ' 
        TxtTelefono.Location = New Point(645, 328)
        TxtTelefono.Margin = New Padding(4, 3, 4, 3)
        TxtTelefono.Name = "TxtTelefono"
        TxtTelefono.Size = New Size(100, 23)
        TxtTelefono.TabIndex = 28
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(9, 360)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(48, 15)
        Label5.TabIndex = 27
        Label5.Text = "Oficina:"
        ' 
        ' TxtOficina
        ' 
        TxtOficina.Location = New Point(74, 356)
        TxtOficina.Margin = New Padding(4, 3, 4, 3)
        TxtOficina.Name = "TxtOficina"
        TxtOficina.Size = New Size(172, 23)
        TxtOficina.TabIndex = 26
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(140, 331)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(66, 15)
        Label4.TabIndex = 25
        Label4.Text = "Encargado:"
        ' 
        ' TxtEncargado
        ' 
        TxtEncargado.Location = New Point(223, 328)
        TxtEncargado.Margin = New Padding(4, 3, 4, 3)
        TxtEncargado.Name = "TxtEncargado"
        TxtEncargado.Size = New Size(332, 23)
        TxtEncargado.TabIndex = 24
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(254, 360)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(54, 15)
        Label3.TabIndex = 23
        Label3.Text = "Sucursal:"
        ' 
        ' cmbSucursal
        ' 
        cmbSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSucursal.FormattingEnabled = True
        cmbSucursal.Location = New Point(324, 356)
        cmbSucursal.Margin = New Padding(4, 3, 4, 3)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(231, 23)
        cmbSucursal.TabIndex = 22
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(11, 331)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(21, 15)
        Label2.TabIndex = 21
        Label2.Text = "ID:"
        ' 
        ' TxtId
        ' 
        TxtId.Enabled = False
        TxtId.Location = New Point(74, 327)
        TxtId.Margin = New Padding(4, 3, 4, 3)
        TxtId.Name = "TxtId"
        TxtId.Size = New Size(40, 23)
        TxtId.TabIndex = 20
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(8, 15)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 31
        Label1.Text = "Buscar:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(65, 12)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(502, 23)
        TxtBuscar.TabIndex = 30
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
        DgvListado.Size = New Size(773, 248)
        DgvListado.TabIndex = 32
        ' 
        ' frmEncargados
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(785, 461)
        Controls.Add(DgvListado)
        Controls.Add(Label1)
        Controls.Add(TxtBuscar)
        Controls.Add(Label6)
        Controls.Add(TxtTelefono)
        Controls.Add(Label5)
        Controls.Add(TxtOficina)
        Controls.Add(Label4)
        Controls.Add(TxtEncargado)
        Controls.Add(Label3)
        Controls.Add(cmbSucursal)
        Controls.Add(Label2)
        Controls.Add(TxtId)
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
        Name = "frmEncargados"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Nomenclador - Encargados"
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
    Friend WithEvents TxtTelefono As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtOficina As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtEncargado As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtId As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView

End Class
