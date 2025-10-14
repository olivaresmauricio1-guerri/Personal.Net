Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmFeriados
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmFeriados

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmFeriados()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmFeriados_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmFeriados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormModoConsulta()
        GridBuscar()
        GridConfigurarColumnas()
        CargarCombos(cmbZonas, "Zonas", "Descripcion", "Descripcion", "idZona")
        Me.KeyPreview = True
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado, chkEncabezados.Checked)
            e.Handled = True
        End If
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        If e.RowIndex < 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        AplicarSeleccionActual()
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

    Private Sub chkEncabezados_CheckedChanged(sender As Object, e As EventArgs) Handles chkEncabezados.CheckedChanged
        DgvListado.Focus()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
        DtpDia.Focus()
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        FormModoEdicion()
        DtpDia.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este feriado?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim diaOriginal As Date = Convert.ToDateTime(filaActual.Cells("Dia").Value)
            Dim zonaOriginal As String = If(IsDBNull(filaActual.Cells("Zona").Value), Nothing, Convert.ToString(filaActual.Cells("Zona").Value))

            Dim sql = "DELETE FROM Feriados WHERE CAST(Dia AS DATE) = CAST(@Dia AS DATE) AND ISNULL(Zona,'') = ISNULL(@Zona,'')"
            Dim parametros = CmdParams("@Dia", diaOriginal.Date, "@Zona", If(zonaOriginal, String.Empty))
            DSM.Execute(DSM.Personal, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim dia As Date = DtpDia.Value.Date
        Dim motivo As String = TxtMotivo.Text.Trim()
        Dim zona As String = cmbZonas.Text.Trim()
        Dim creado As Boolean = ChkCreado.Checked

        If filaActual Is Nothing Then
            ' INSERT
            Dim sql = "INSERT INTO Feriados (Dia, Motivo, creado, Zona) VALUES (@Dia, @Motivo, @Creado, @Zona)"
            Dim parametros = CmdParams("@Dia", dia, "@Motivo", If(String.IsNullOrEmpty(motivo), DBNull.Value, motivo), "@Creado", creado, "@Zona", If(String.IsNullOrEmpty(zona), DBNull.Value, zona))
            DSM.Execute(DSM.Personal, sql, parametros, True)
        Else
            ' UPDATE (usa Dia + Zona originales como clave)
            Dim diaOriginal As Date = Convert.ToDateTime(filaActual.Cells("Dia").Value)
            Dim zonaOriginal As String = If(IsDBNull(filaActual.Cells("Zona").Value), Nothing, Convert.ToString(filaActual.Cells("Zona").Value))

            Dim sql = "UPDATE Feriados SET Dia = @Dia, Motivo = @Motivo, creado = @Creado, Zona = @Zona " &
                      "WHERE CAST(Dia AS DATE) = CAST(@DiaOriginal AS DATE) AND ISNULL(Zona,'') = ISNULL(@ZonaOriginal,'')"
            Dim parametros = CmdParams(
                "@Dia", dia,
                "@Motivo", If(String.IsNullOrEmpty(motivo), DBNull.Value, motivo),
                "@Creado", creado,
                "@Zona", If(String.IsNullOrEmpty(zona), DBNull.Value, zona),
                "@DiaOriginal", diaOriginal.Date,
                "@ZonaOriginal", If(zonaOriginal, String.Empty)
            )
            DSM.Execute(DSM.Personal, sql, parametros, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Public Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    ''' <summary>
    ''' Obtiene los feriados filtrados por texto de bÃºsqueda.
    ''' Filtra por Motivo/Zona como texto y por fecha si el texto es una fecha vÃ¡lida.
    ''' </summary>
    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT Dia, Motivo, creado, Zona FROM Feriados WHERE YEAR(Dia) = @AnioActual"

        Dim parametros As New List(Of Object)
        parametros.Add("@AnioActual")
        parametros.Add(Date.Now.Year)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (Motivo LIKE @Motivo OR Zona LIKE @Zona"
            parametros.Add("@Motivo")
            parametros.Add($"%{texto}%")
            parametros.Add("@Zona")
            parametros.Add($"%{texto}%")

            Dim fechaBuscada As Date
            If Date.TryParse(texto, fechaBuscada) Then
                sql &= " OR CAST(Dia AS DATE) = @Dia"
                parametros.Add("@Dia")
                parametros.Add(fechaBuscada.Date)
            End If

            sql &= ")"
        End If

        sql &= " ORDER BY Dia DESC"

        tabla = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = tabla

        If tabla.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        filaActualIndice = 0
        filaActual = DgvListado.Rows(0)
        FormObtenerSeleccionado()
    End Sub

    Private Sub AplicarSeleccionActual()
        If DgvListado Is Nothing OrElse DgvListado.CurrentRow Is Nothing Then Return

        If DgvListado.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        Dim idx = DgvListado.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return

        FormModoConsulta()
        FormLimpiarSeleccionado()

        filaActualIndice = idx
        filaActual = DgvListado.CurrentRow
        FormObtenerSeleccionado()
    End Sub

    ''' <summary>
    ''' Configura las columnas del DataGridView DgvListado.
    ''' </summary>
    Public Sub GridConfigurarColumnas()
        'ocultar todas las columnas menos las que se configuren
        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next

        If DgvListado.Columns.Contains("Dia") Then
            DgvListado.Columns("Dia").Visible = True
            DgvListado.Columns("Dia").HeaderText = "Día"
            DgvListado.Columns("Dia").Width = 90
            DgvListado.Columns("Dia").DefaultCellStyle.Format = "dd/MM/yyyy"
        End If

        If DgvListado.Columns.Contains("Motivo") Then
            DgvListado.Columns("Motivo").Visible = True
            DgvListado.Columns("Motivo").HeaderText = "Motivo"
            DgvListado.Columns("Motivo").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DgvListado.Columns.Contains("Zona") Then
            DgvListado.Columns("Zona").Visible = True
            DgvListado.Columns("Zona").HeaderText = "Zona"
            DgvListado.Columns("Zona").Width = 120
        End If

        ConfigurarEstiloGrid(DgvListado)
    End Sub

    ''' <summary>
    ''' Limpia los campos de entrada del formulario.
    ''' </summary>
    Private Sub FormLimpiarSeleccionado()
        DtpDia.Value = Date.Today
        TxtMotivo.Text = String.Empty
        cmbZonas.Text = String.Empty
        ChkCreado.Checked = False
    End Sub

    ''' <summary>
    ''' Obtiene los valores de la fila seleccionada y los muestra en los controles.
    ''' </summary>
    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            If filaActual.Cells("Dia").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("Dia").Value) Then
                DtpDia.Value = Convert.ToDateTime(filaActual.Cells("Dia").Value)
            Else
                DtpDia.Value = Date.Today
            End If

            TxtMotivo.Text = If(IsDBNull(filaActual.Cells("Motivo").Value), String.Empty, filaActual.Cells("Motivo").Value.ToString())
            cmbZonas.Text = If(IsDBNull(filaActual.Cells("Zona").Value), String.Empty, filaActual.Cells("Zona").Value.ToString())
            Dim creadoVal As Boolean = False
            If filaActual.Cells("creado").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("creado").Value) Then
                creadoVal = Convert.ToBoolean(filaActual.Cells("creado").Value)
            End If
            ChkCreado.Checked = creadoVal
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para comenzar una ediciÃ³n.
    ''' </summary>
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, DtpDia, cmbZonas, TxtMotivo, ChkCreado)
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para aceptar o cancelar una ediciÃ³n.
    ''' </summary>
    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, DtpDia, cmbZonas, TxtMotivo, ChkCreado)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
    End Sub

End Class
