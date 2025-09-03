<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTipoInasistencias
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
        Label12 = New Label()
        chkSale = New CheckBox()
        Label11 = New Label()
        TxtDecreto = New TextBox()
        Label10 = New Label()
        TxtAnioDto = New TextBox()
        Label9 = New Label()
        TxtAnexo = New TextBox()
        Label8 = New Label()
        chkMensual = New CheckBox()
        Label7 = New Label()
        chkHabil = New CheckBox()
        Label6 = New Label()
        chkCorrido = New CheckBox()
        Label5 = New Label()
        TxtMes = New TextBox()
        Label4 = New Label()
        TxtAno = New TextBox()
        Label3 = New Label()
        chkGoce = New CheckBox()
        Label2 = New Label()
        TxtPunto = New TextBox()
        Label1 = New Label()
        TxtInciso = New TextBox()
        LabelArticulo = New Label()
        TxtArticulo = New TextBox()
        LabelDescripcion = New Label()
        TxtDescripcion = New TextBox()
        LabelCodigo = New Label()
        TxtCodigo = New TextBox()
        LabelBuscar = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(1048, 396)
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
        lnkCopiar.Location = New Point(941, 397)
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
        CmdSalir.Location = New Point(1089, 536)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 22
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(1004, 536)
        CmdCancelar.Margin = New Padding(4, 3, 4, 3)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 30)
        CmdCancelar.TabIndex = 21
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(921, 536)
        CmdAceptar.Margin = New Padding(4, 3, 4, 3)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 20
        CmdAceptar.Text = "Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(171, 536)
        CmdBorrar.Margin = New Padding(4, 3, 4, 3)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 30)
        CmdBorrar.TabIndex = 19
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' CmdModificar
        ' 
        CmdModificar.FlatStyle = FlatStyle.Flat
        CmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdModificar.Location = New Point(88, 536)
        CmdModificar.Margin = New Padding(4, 3, 4, 3)
        CmdModificar.Name = "CmdModificar"
        CmdModificar.Size = New Size(75, 30)
        CmdModificar.TabIndex = 18
        CmdModificar.Text = "Modificar"
        CmdModificar.UseVisualStyleBackColor = True
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(5, 536)
        CmdAgregar.Margin = New Padding(4, 3, 4, 3)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 30)
        CmdAgregar.TabIndex = 17
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(698, 479)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(31, 15)
        Label12.TabIndex = 59
        Label12.Text = "Sale:"
        ' 
        ' chkSale
        ' 
        chkSale.AutoSize = True
        chkSale.Location = New Point(738, 479)
        chkSale.Margin = New Padding(4, 3, 4, 3)
        chkSale.Name = "chkSale"
        chkSale.Size = New Size(15, 14)
        chkSale.TabIndex = 16
        chkSale.UseVisualStyleBackColor = True
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(439, 479)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(51, 15)
        Label11.TabIndex = 57
        Label11.Text = "Decreto:"
        ' 
        ' TxtDecreto
        ' 
        TxtDecreto.Location = New Point(498, 475)
        TxtDecreto.Margin = New Padding(4, 3, 4, 3)
        TxtDecreto.MaxLength = 50
        TxtDecreto.Name = "TxtDecreto"
        TxtDecreto.Size = New Size(179, 23)
        TxtDecreto.TabIndex = 15
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(309, 479)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(54, 15)
        Label10.TabIndex = 55
        Label10.Text = "Año Dto:"
        ' 
        ' TxtAnioDto
        ' 
        TxtAnioDto.Location = New Point(371, 475)
        TxtAnioDto.Margin = New Padding(4, 3, 4, 3)
        TxtAnioDto.Name = "TxtAnioDto"
        TxtAnioDto.Size = New Size(60, 23)
        TxtAnioDto.TabIndex = 14
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(179, 479)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(43, 15)
        Label9.TabIndex = 53
        Label9.Text = "Anexo:"
        ' 
        ' TxtAnexo
        ' 
        TxtAnexo.Location = New Point(230, 475)
        TxtAnexo.Margin = New Padding(4, 3, 4, 3)
        TxtAnexo.Name = "TxtAnexo"
        TxtAnexo.Size = New Size(60, 23)
        TxtAnexo.TabIndex = 13
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(88, 479)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(55, 15)
        Label8.TabIndex = 51
        Label8.Text = "Mensual:"
        ' 
        ' chkMensual
        ' 
        chkMensual.AutoSize = True
        chkMensual.Location = New Point(151, 479)
        chkMensual.Margin = New Padding(4, 3, 4, 3)
        chkMensual.Name = "chkMensual"
        chkMensual.Size = New Size(15, 14)
        chkMensual.TabIndex = 12
        chkMensual.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(9, 479)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(38, 15)
        Label7.TabIndex = 49
        Label7.Text = "Hábil:"
        ' 
        ' chkHabil
        ' 
        chkHabil.AutoSize = True
        chkHabil.Location = New Point(55, 479)
        chkHabil.Margin = New Padding(4, 3, 4, 3)
        chkHabil.Name = "chkHabil"
        chkHabil.Size = New Size(15, 14)
        chkHabil.TabIndex = 1
        chkHabil.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(1048, 444)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(50, 15)
        Label6.TabIndex = 47
        Label6.Text = "Corrido:"
        ' 
        ' chkCorrido
        ' 
        chkCorrido.AutoSize = True
        chkCorrido.Location = New Point(1106, 444)
        chkCorrido.Margin = New Padding(4, 3, 4, 3)
        chkCorrido.Name = "chkCorrido"
        chkCorrido.Size = New Size(15, 14)
        chkCorrido.TabIndex = 10
        chkCorrido.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(964, 444)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(32, 15)
        Label5.TabIndex = 45
        Label5.Text = "Mes:"
        ' 
        ' TxtMes
        ' 
        TxtMes.Location = New Point(1004, 440)
        TxtMes.Margin = New Padding(4, 3, 4, 3)
        TxtMes.Name = "TxtMes"
        TxtMes.Size = New Size(32, 23)
        TxtMes.TabIndex = 9
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(861, 444)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(32, 15)
        Label4.TabIndex = 43
        Label4.Text = "Año:"
        ' 
        ' TxtAno
        ' 
        TxtAno.Location = New Point(901, 440)
        TxtAno.Margin = New Padding(4, 3, 4, 3)
        TxtAno.Name = "TxtAno"
        TxtAno.Size = New Size(49, 23)
        TxtAno.TabIndex = 8
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(793, 444)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(37, 15)
        Label3.TabIndex = 41
        Label3.Text = "Goce:"
        ' 
        ' chkGoce
        ' 
        chkGoce.AutoSize = True
        chkGoce.Location = New Point(838, 444)
        chkGoce.Margin = New Padding(4, 3, 4, 3)
        chkGoce.Name = "chkGoce"
        chkGoce.Size = New Size(15, 14)
        chkGoce.TabIndex = 7
        chkGoce.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(635, 444)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(42, 15)
        Label2.TabIndex = 39
        Label2.Text = "Punto:"
        ' 
        ' TxtPunto
        ' 
        TxtPunto.Location = New Point(685, 440)
        TxtPunto.Margin = New Padding(4, 3, 4, 3)
        TxtPunto.Name = "TxtPunto"
        TxtPunto.Size = New Size(100, 23)
        TxtPunto.TabIndex = 6
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(548, 444)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(41, 15)
        Label1.TabIndex = 37
        Label1.Text = "Inciso:"
        ' 
        ' TxtInciso
        ' 
        TxtInciso.Location = New Point(597, 440)
        TxtInciso.Margin = New Padding(4, 3, 4, 3)
        TxtInciso.MaxLength = 1
        TxtInciso.Name = "TxtInciso"
        TxtInciso.Size = New Size(30, 23)
        TxtInciso.TabIndex = 5
        ' 
        ' LabelArticulo
        ' 
        LabelArticulo.AutoSize = True
        LabelArticulo.Location = New Point(397, 444)
        LabelArticulo.Margin = New Padding(4, 0, 4, 0)
        LabelArticulo.Name = "LabelArticulo"
        LabelArticulo.Size = New Size(52, 15)
        LabelArticulo.TabIndex = 35
        LabelArticulo.Text = "Artículo:"
        ' 
        ' TxtArticulo
        ' 
        TxtArticulo.Location = New Point(457, 440)
        TxtArticulo.Margin = New Padding(4, 3, 4, 3)
        TxtArticulo.Name = "TxtArticulo"
        TxtArticulo.Size = New Size(83, 23)
        TxtArticulo.TabIndex = 4
        ' 
        ' LabelDescripcion
        ' 
        LabelDescripcion.AutoSize = True
        LabelDescripcion.Location = New Point(130, 444)
        LabelDescripcion.Margin = New Padding(4, 0, 4, 0)
        LabelDescripcion.Name = "LabelDescripcion"
        LabelDescripcion.Size = New Size(72, 15)
        LabelDescripcion.TabIndex = 33
        LabelDescripcion.Text = "Descripción:"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Location = New Point(210, 440)
        TxtDescripcion.Margin = New Padding(4, 3, 4, 3)
        TxtDescripcion.MaxLength = 38
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(179, 23)
        TxtDescripcion.TabIndex = 3
        ' 
        ' LabelCodigo
        ' 
        LabelCodigo.AutoSize = True
        LabelCodigo.Location = New Point(5, 444)
        LabelCodigo.Margin = New Padding(4, 0, 4, 0)
        LabelCodigo.Name = "LabelCodigo"
        LabelCodigo.Size = New Size(49, 15)
        LabelCodigo.TabIndex = 31
        LabelCodigo.Text = "Código:"
        ' 
        ' TxtCodigo
        ' 
        TxtCodigo.Location = New Point(62, 440)
        TxtCodigo.Margin = New Padding(4, 3, 4, 3)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.Size = New Size(60, 23)
        TxtCodigo.TabIndex = 2
        ' 
        ' LabelBuscar
        ' 
        LabelBuscar.AutoSize = True
        LabelBuscar.Location = New Point(8, 15)
        LabelBuscar.Margin = New Padding(4, 0, 4, 0)
        LabelBuscar.Name = "LabelBuscar"
        LabelBuscar.Size = New Size(45, 15)
        LabelBuscar.TabIndex = 61
        LabelBuscar.Text = "Buscar:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(65, 12)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(502, 23)
        TxtBuscar.TabIndex = 1
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
        DgvListado.Size = New Size(1160, 349)
        DgvListado.TabIndex = 4
        ' 
        ' frmTipoInasistencias
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1172, 574)
        Controls.Add(DgvListado)
        Controls.Add(LabelBuscar)
        Controls.Add(TxtBuscar)
        Controls.Add(Label12)
        Controls.Add(chkSale)
        Controls.Add(Label11)
        Controls.Add(TxtDecreto)
        Controls.Add(Label10)
        Controls.Add(TxtAnioDto)
        Controls.Add(Label9)
        Controls.Add(TxtAnexo)
        Controls.Add(Label8)
        Controls.Add(chkMensual)
        Controls.Add(Label7)
        Controls.Add(chkHabil)
        Controls.Add(Label6)
        Controls.Add(chkCorrido)
        Controls.Add(Label5)
        Controls.Add(TxtMes)
        Controls.Add(Label4)
        Controls.Add(TxtAno)
        Controls.Add(Label3)
        Controls.Add(chkGoce)
        Controls.Add(Label2)
        Controls.Add(TxtPunto)
        Controls.Add(Label1)
        Controls.Add(TxtInciso)
        Controls.Add(LabelArticulo)
        Controls.Add(TxtArticulo)
        Controls.Add(LabelDescripcion)
        Controls.Add(TxtDescripcion)
        Controls.Add(LabelCodigo)
        Controls.Add(TxtCodigo)
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
        Name = "frmTipoInasistencias"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Nomenclador - Tipo Inasistencias"
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
    Friend WithEvents Label12 As Label
    Friend WithEvents chkSale As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtDecreto As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtAnioDto As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtAnexo As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents chkMensual As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents chkHabil As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents chkCorrido As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtMes As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtAno As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents chkGoce As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtPunto As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtInciso As TextBox
    Friend WithEvents LabelArticulo As Label
    Friend WithEvents TxtArticulo As TextBox
    Friend WithEvents LabelDescripcion As Label
    Friend WithEvents TxtDescripcion As TextBox
    Friend WithEvents LabelCodigo As Label
    Friend WithEvents TxtCodigo As TextBox
    Friend WithEvents LabelBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView

End Class
