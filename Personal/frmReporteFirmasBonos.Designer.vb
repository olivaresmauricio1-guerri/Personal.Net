<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReporteFirmasBonos
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        pnlFiltros = New Panel()
        lblTotales = New Label()
        btnCerrar = New Button()
        btnBuscar = New Button()
        cboProceso = New ComboBox()
        lblProceso = New Label()
        dtpHasta = New DateTimePicker()
        lblHasta = New Label()
        dtpDesde = New DateTimePicker()
        lblDesde = New Label()
        splitPrincipal = New SplitContainer()
        grpResumen = New GroupBox()
        dgvResumen = New DataGridView()
        grpDetalle = New GroupBox()
        dgvDetalle = New DataGridView()
        pnlFiltros.SuspendLayout()
        CType(splitPrincipal, ComponentModel.ISupportInitialize).BeginInit()
        splitPrincipal.Panel1.SuspendLayout()
        splitPrincipal.Panel2.SuspendLayout()
        splitPrincipal.SuspendLayout()
        grpResumen.SuspendLayout()
        CType(dgvResumen, ComponentModel.ISupportInitialize).BeginInit()
        grpDetalle.SuspendLayout()
        CType(dgvDetalle, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'pnlFiltros
        '
        pnlFiltros.Controls.Add(lblTotales)
        pnlFiltros.Controls.Add(btnCerrar)
        pnlFiltros.Controls.Add(btnBuscar)
        pnlFiltros.Controls.Add(cboProceso)
        pnlFiltros.Controls.Add(lblProceso)
        pnlFiltros.Controls.Add(dtpHasta)
        pnlFiltros.Controls.Add(lblHasta)
        pnlFiltros.Controls.Add(dtpDesde)
        pnlFiltros.Controls.Add(lblDesde)
        pnlFiltros.Dock = DockStyle.Top
        pnlFiltros.Location = New Point(0, 0)
        pnlFiltros.Name = "pnlFiltros"
        pnlFiltros.Size = New Size(1184, 56)
        pnlFiltros.TabIndex = 0
        '
        'lblTotales
        '
        lblTotales.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblTotales.AutoSize = True
        lblTotales.Location = New Point(780, 20)
        lblTotales.Name = "lblTotales"
        lblTotales.Size = New Size(93, 15)
        lblTotales.TabIndex = 8
        lblTotales.Text = "Procesos: 0    R"
        '
        'btnCerrar
        '
        btnCerrar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnCerrar.BackColor = Color.IndianRed
        btnCerrar.FlatStyle = FlatStyle.Flat
        btnCerrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCerrar.ForeColor = Color.White
        btnCerrar.Location = New Point(1092, 13)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Size = New Size(80, 30)
        btnCerrar.TabIndex = 7
        btnCerrar.Text = "Cerrar"
        btnCerrar.UseVisualStyleBackColor = False
        '
        'btnBuscar
        '
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnBuscar.Location = New Point(680, 13)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(80, 30)
        btnBuscar.TabIndex = 6
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = True
        '
        'cboProceso
        '
        cboProceso.DropDownStyle = ComboBoxStyle.DropDownList
        cboProceso.FormattingEnabled = True
        cboProceso.Location = New Point(402, 17)
        cboProceso.Name = "cboProceso"
        cboProceso.Size = New Size(263, 23)
        cboProceso.TabIndex = 5
        '
        'lblProceso
        '
        lblProceso.AutoSize = True
        lblProceso.Location = New Point(346, 21)
        lblProceso.Name = "lblProceso"
        lblProceso.Size = New Size(50, 15)
        lblProceso.TabIndex = 4
        lblProceso.Text = "Proceso:"
        '
        'dtpHasta
        '
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(240, 17)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(90, 23)
        dtpHasta.TabIndex = 3
        '
        'lblHasta
        '
        lblHasta.AutoSize = True
        lblHasta.Location = New Point(199, 21)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(38, 15)
        lblHasta.TabIndex = 2
        lblHasta.Text = "Hasta:"
        '
        'dtpDesde
        '
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(57, 17)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(90, 23)
        dtpDesde.TabIndex = 1
        '
        'lblDesde
        '
        lblDesde.AutoSize = True
        lblDesde.Location = New Point(12, 21)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(41, 15)
        lblDesde.TabIndex = 0
        lblDesde.Text = "Desde:"
        '
        'splitPrincipal
        '
        splitPrincipal.Dock = DockStyle.Fill
        splitPrincipal.Location = New Point(0, 56)
        splitPrincipal.Name = "splitPrincipal"
        splitPrincipal.Orientation = Orientation.Horizontal
        '
        'splitPrincipal.Panel1
        '
        splitPrincipal.Panel1.Controls.Add(grpResumen)
        '
        'splitPrincipal.Panel2
        '
        splitPrincipal.Panel2.Controls.Add(grpDetalle)
        splitPrincipal.Size = New Size(1184, 625)
        splitPrincipal.SplitterDistance = 180
        splitPrincipal.TabIndex = 1
        '
        'grpResumen
        '
        grpResumen.Controls.Add(dgvResumen)
        grpResumen.Dock = DockStyle.Fill
        grpResumen.Location = New Point(0, 0)
        grpResumen.Name = "grpResumen"
        grpResumen.Size = New Size(1184, 180)
        grpResumen.TabIndex = 0
        grpResumen.TabStop = False
        grpResumen.Text = "Resumen por proceso"
        '
        'dgvResumen
        '
        dgvResumen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResumen.Dock = DockStyle.Fill
        dgvResumen.Location = New Point(3, 19)
        dgvResumen.Name = "dgvResumen"
        dgvResumen.Size = New Size(1178, 158)
        dgvResumen.TabIndex = 0
        '
        'grpDetalle
        '
        grpDetalle.Controls.Add(dgvDetalle)
        grpDetalle.Dock = DockStyle.Fill
        grpDetalle.Location = New Point(0, 0)
        grpDetalle.Name = "grpDetalle"
        grpDetalle.Size = New Size(1184, 441)
        grpDetalle.TabIndex = 0
        grpDetalle.TabStop = False
        grpDetalle.Text = "Detalle por agente"
        '
        'dgvDetalle
        '
        dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDetalle.Dock = DockStyle.Fill
        dgvDetalle.Location = New Point(3, 19)
        dgvDetalle.Name = "dgvDetalle"
        dgvDetalle.Size = New Size(1178, 419)
        dgvDetalle.TabIndex = 0
        '
        'frmReporteFirmasBonos
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(1184, 681)
        Controls.Add(splitPrincipal)
        Controls.Add(pnlFiltros)
        MinimumSize = New Size(1000, 560)
        Name = "frmReporteFirmasBonos"
        Text = "Reporte de Firmas de Bonos"
        pnlFiltros.ResumeLayout(False)
        pnlFiltros.PerformLayout()
        splitPrincipal.Panel1.ResumeLayout(False)
        splitPrincipal.Panel2.ResumeLayout(False)
        CType(splitPrincipal, ComponentModel.ISupportInitialize).EndInit()
        splitPrincipal.ResumeLayout(False)
        grpResumen.ResumeLayout(False)
        CType(dgvResumen, ComponentModel.ISupportInitialize).EndInit()
        grpDetalle.ResumeLayout(False)
        CType(dgvDetalle, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlFiltros As Panel
    Friend WithEvents btnCerrar As Button
    Friend WithEvents btnBuscar As Button
    Friend WithEvents cboProceso As ComboBox
    Friend WithEvents lblProceso As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblDesde As Label
    Friend WithEvents splitPrincipal As SplitContainer
    Friend WithEvents grpResumen As GroupBox
    Friend WithEvents dgvResumen As DataGridView
    Friend WithEvents grpDetalle As GroupBox
    Friend WithEvents dgvDetalle As DataGridView
    Friend WithEvents lblTotales As Label
End Class
