Imports System.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmReporteFirmasBonos
    Private Shared instancia As frmReporteFirmasBonos
    Private _idProcesoInicial As Integer?

    Public Shared Sub AbrirInstancia(mdiParent As Form, Optional idProceso As Integer? = Nothing)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmReporteFirmasBonos()
            instancia.MdiParent = mdiParent
        End If

        instancia._idProcesoInicial = idProceso
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
        instancia.CargarDatosIniciales()
    End Sub

    Private Sub frmReporteFirmasBonos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDesde.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        dtpHasta.Value = dtpDesde.Value.AddMonths(1).AddDays(-1)
        PrepararGrillas()
        CargarDatosIniciales()
    End Sub

    Private Sub frmReporteFirmasBonos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub CargarDatosIniciales()
        If Not IsHandleCreated Then
            Return
        End If

        Dim idProcesoSeleccionado = If(_idProcesoInicial.HasValue, _idProcesoInicial, ObtenerProcesoSeleccionadoCombo())
        CargarProcesos(idProcesoSeleccionado)
        CargarDatos()
    End Sub

    Private Sub PrepararGrillas()
        dgvResumen.AutoGenerateColumns = True
        dgvResumen.ReadOnly = True
        dgvResumen.MultiSelect = False
        dgvResumen.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResumen.AllowUserToAddRows = False
        dgvResumen.AllowUserToDeleteRows = False
        dgvResumen.AllowUserToResizeRows = False

        dgvDetalle.AutoGenerateColumns = True
        dgvDetalle.ReadOnly = True
        dgvDetalle.MultiSelect = False
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDetalle.AllowUserToAddRows = False
        dgvDetalle.AllowUserToDeleteRows = False
        dgvDetalle.AllowUserToResizeRows = False
    End Sub

    Private Sub CargarProcesos(Optional idProcesoSeleccionado As Integer? = Nothing)
        Dim sql = "SELECT IdProceso, PeriodoProcesado, ISNULL(FechaFin, FechaInicio) AS FechaProceso, " &
                  "ISNULL(EstadoProceso, 'PENDIENTE') AS EstadoProceso, ISNULL(CantidadArchivosGenerados, 0) AS CantidadArchivosGenerados " &
                  "FROM RecibosProcesos " &
                  "WHERE PeriodoProcesado >= @FechaDesde AND PeriodoProcesado < @FechaHasta " &
                  "ORDER BY ISNULL(FechaFin, FechaInicio) DESC, IdProceso DESC"

        Dim parametros = CmdParams(
            "@FechaDesde", New DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1),
            "@FechaHasta", New DateTime(dtpHasta.Value.Year, dtpHasta.Value.Month, 1).AddMonths(1))

        Dim tablaProcesos = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
        Dim origen As New DataTable()
        origen.Columns.Add("IdProceso", GetType(Integer))
        origen.Columns.Add("Descripcion", GetType(String))

        Dim filaTodos = origen.NewRow()
        filaTodos("IdProceso") = 0
        filaTodos("Descripcion") = "(Todos los procesos)"
        origen.Rows.Add(filaTodos)

        For Each fila As DataRow In tablaProcesos.Rows
            Dim filaCombo = origen.NewRow()
            Dim idProceso = Convert.ToInt32(fila("IdProceso"))
            Dim periodo = Convert.ToDateTime(fila("PeriodoProcesado"))
            Dim fechaProceso = Convert.ToDateTime(fila("FechaProceso"))
            Dim estado = Convert.ToString(fila("EstadoProceso")).Trim()
            Dim cantidad = Convert.ToInt32(fila("CantidadArchivosGenerados"))

            filaCombo("IdProceso") = idProceso
            filaCombo("Descripcion") = $"{idProceso} | {periodo:MM/yyyy} | {fechaProceso:dd/MM/yyyy HH:mm} | {estado} | Archivos: {cantidad}"
            origen.Rows.Add(filaCombo)
        Next

        cboProceso.DataSource = origen
        cboProceso.DisplayMember = "Descripcion"
        cboProceso.ValueMember = "IdProceso"

        If idProcesoSeleccionado.HasValue AndAlso origen.Select("IdProceso = " & idProcesoSeleccionado.Value.ToString()).Length > 0 Then
            cboProceso.SelectedValue = idProcesoSeleccionado.Value
        Else
            cboProceso.SelectedValue = 0
        End If
    End Sub

    Private Sub CargarDatos()
        Dim idProceso = ObtenerProcesoSeleccionadoCombo()
        dgvResumen.DataSource = ObtenerResumen(idProceso)
        ConfigurarResumen()

        dgvDetalle.DataSource = ObtenerDetalle(idProceso)
        ConfigurarDetalle()

        lblTotales.Text = $"Procesos: {dgvResumen.Rows.Count}    Registros: {dgvDetalle.Rows.Count}"
        _idProcesoInicial = Nothing
    End Sub

    Private Function ObtenerProcesoSeleccionadoCombo() As Integer
        If cboProceso.SelectedValue Is Nothing OrElse cboProceso.SelectedValue Is DBNull.Value Then
            Return 0
        End If

        Return Convert.ToInt32(cboProceso.SelectedValue)
    End Function

    Private Function ObtenerResumen(idProceso As Integer) As DataTable
        Dim sql = "SELECT rp.IdProceso, rp.PeriodoProcesado, ISNULL(rp.FechaFin, rp.FechaInicio) AS FechaProceso, " &
                  "COUNT(*) AS TotalAgentes, " &
                  "SUM(CASE WHEN ISNULL(rpd.EstadoEnvio,'PENDIENTE') = 'ENVIADO' THEN 1 ELSE 0 END) AS Enviados, " &
                  "SUM(CASE WHEN ISNULL(rpd.EstadoApertura,'PENDIENTE') = 'ABIERTO' THEN 1 ELSE 0 END) AS Abiertos, " &
                  "SUM(CASE WHEN UPPER(ISNULL(rpd.EstadoConfirmacion,'PENDIENTE')) = 'CONFIRMADO' THEN 1 ELSE 0 END) AS Firmados, " &
                  "SUM(CASE WHEN UPPER(ISNULL(rpd.EstadoEnvio,'PENDIENTE')) <> 'ENVIADO' THEN 1 ELSE 0 END) AS NoEnviados, " &
                  "SUM(CASE WHEN UPPER(ISNULL(rpd.EstadoEnvio,'PENDIENTE')) = 'ENVIADO' AND UPPER(ISNULL(rpd.EstadoApertura,'PENDIENTE')) <> 'ABIERTO' THEN 1 ELSE 0 END) AS SinAbrir, " &
                  "SUM(CASE WHEN UPPER(ISNULL(rpd.EstadoApertura,'PENDIENTE')) = 'ABIERTO' AND UPPER(ISNULL(rpd.EstadoConfirmacion,'PENDIENTE')) <> 'CONFIRMADO' THEN 1 ELSE 0 END) AS AbiertosSinFirmar " &
                  "FROM RecibosProcesos rp " &
                  "INNER JOIN RecibosProcesosDetalles rpd ON rp.IdProceso = rpd.IdProceso " &
                  "WHERE rp.PeriodoProcesado >= @FechaDesde AND rp.PeriodoProcesado < @FechaHasta " &
                  "AND (@IdProceso = 0 OR rp.IdProceso = @IdProceso) " &
                  "GROUP BY rp.IdProceso, rp.PeriodoProcesado, ISNULL(rp.FechaFin, rp.FechaInicio) " &
                  "ORDER BY FechaProceso DESC, rp.IdProceso DESC"

        Dim parametros = CmdParams(
            "@FechaDesde", New DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1),
            "@FechaHasta", New DateTime(dtpHasta.Value.Year, dtpHasta.Value.Month, 1).AddMonths(1),
            "@IdProceso", idProceso)

        Return DSM.ExecuteQuery(DSM.Personal, sql, parametros)
    End Function

    Private Function ObtenerDetalle(idProceso As Integer) As DataTable
        Dim sql = "SELECT rp.IdProceso, rp.PeriodoProcesado, rpd.Legajo, a.Nombre, " &
                  "ISNULL(rpd.EstadoEnvio,'PENDIENTE') AS EstadoEnvio, rpd.FechaEnvio, " &
                  "ISNULL(rpd.EstadoApertura,'PENDIENTE') AS EstadoApertura, rpd.FechaPrimerAccesoLink AS FechaApertura, " &
                  "ISNULL(rpd.EstadoConfirmacion,'PENDIENTE') AS EstadoConfirmacion, rpd.FechaConfirmacion, " &
                  "ISNULL(rc.Accion, 'PENDIENTE') AS Accion, " &
                  "CASE " &
                  "WHEN UPPER(ISNULL(rpd.EstadoConfirmacion,'PENDIENTE')) = 'CONFIRMADO' THEN 'FIRMADO' " &
                  "WHEN UPPER(ISNULL(rpd.EstadoEnvio,'PENDIENTE')) <> 'ENVIADO' THEN 'NO ENVIADO' " &
                  "WHEN UPPER(ISNULL(rpd.EstadoApertura,'PENDIENTE')) <> 'ABIERTO' THEN 'ENVIADO SIN ABRIR' " &
                  "ELSE 'ABIERTO SIN FIRMAR' END AS EstadoReporte " &
                  "FROM RecibosProcesos rp " &
                  "INNER JOIN RecibosProcesosDetalles rpd ON rp.IdProceso = rpd.IdProceso " &
                  "INNER JOIN Agentes a ON rpd.Legajo = a.Legajo " &
                  "LEFT JOIN ( " &
                  "    SELECT IdDetalle, MAX(FechaConfirmacion) AS FechaConfirmacion, MAX(ISNULL(Accion,'')) AS Accion " &
                  "    FROM RecibosConfirmaciones GROUP BY IdDetalle" &
                  ") rc ON rc.IdDetalle = rpd.IdDetalle " &
                  "WHERE rp.PeriodoProcesado >= @FechaDesde AND rp.PeriodoProcesado < @FechaHasta " &
                  "AND (@IdProceso = 0 OR rp.IdProceso = @IdProceso) " &
                  "ORDER BY rp.IdProceso DESC, a.Nombre"

        Dim parametros = CmdParams(
            "@FechaDesde", New DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1),
            "@FechaHasta", New DateTime(dtpHasta.Value.Year, dtpHasta.Value.Month, 1).AddMonths(1),
            "@IdProceso", idProceso)

        Return DSM.ExecuteQuery(DSM.Personal, sql, parametros)
    End Function

    Private Sub ConfigurarResumen()
        If dgvResumen.Columns.Count = 0 Then
            Return
        End If

        For Each col As DataGridViewColumn In dgvResumen.Columns
            col.Visible = True
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        If dgvResumen.Columns.Contains("PeriodoProcesado") Then
            dgvResumen.Columns("PeriodoProcesado").HeaderText = "Período"
            dgvResumen.Columns("PeriodoProcesado").DefaultCellStyle.Format = "MM/yyyy"
            dgvResumen.Columns("PeriodoProcesado").Width = 80
        End If

        If dgvResumen.Columns.Contains("FechaProceso") Then
            dgvResumen.Columns("FechaProceso").HeaderText = "Fecha Proceso"
            dgvResumen.Columns("FechaProceso").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
            dgvResumen.Columns("FechaProceso").Width = 125
        End If

        If dgvResumen.Columns.Contains("IdProceso") Then
            dgvResumen.Columns("IdProceso").HeaderText = "Proceso"
            dgvResumen.Columns("IdProceso").Width = 70
        End If

        AplicarAnchoColumnaResumen("TotalAgentes", "Total", 55)
        AplicarAnchoColumnaResumen("Enviados", "Enviados", 65)
        AplicarAnchoColumnaResumen("Abiertos", "Abiertos", 65)
        AplicarAnchoColumnaResumen("Firmados", "Firmados", 65)
        AplicarAnchoColumnaResumen("NoEnviados", "No Enviados", 85)
        AplicarAnchoColumnaResumen("SinAbrir", "Sin Abrir", 75)
        AplicarAnchoColumnaResumen("AbiertosSinFirmar", "Sin Firmar", 75)

        Funciones.ConfigurarEstiloGrid(dgvResumen)
    End Sub

    Private Sub AplicarAnchoColumnaResumen(nombreColumna As String, header As String, width As Integer)
        If dgvResumen.Columns.Contains(nombreColumna) Then
            dgvResumen.Columns(nombreColumna).HeaderText = header
            dgvResumen.Columns(nombreColumna).Width = width
        End If
    End Sub

    Private Sub ConfigurarDetalle()
        If dgvDetalle.Columns.Count = 0 Then
            Return
        End If

        For Each col As DataGridViewColumn In dgvDetalle.Columns
            col.Visible = False
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        MostrarColumnaDetalle("IdProceso", "Proceso", 60)
        MostrarColumnaDetalle("PeriodoProcesado", "Período", 70, "MM/yyyy")
        MostrarColumnaDetalle("Legajo", "Legajo", 60)

        If dgvDetalle.Columns.Contains("Nombre") Then
            dgvDetalle.Columns("Nombre").Visible = True
            dgvDetalle.Columns("Nombre").HeaderText = "Agente"
            dgvDetalle.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        MostrarColumnaDetalle("EstadoReporte", "Estado Reporte", 120)
        MostrarColumnaDetalle("EstadoEnvio", "Estado Envío", 90)
        MostrarColumnaDetalle("FechaEnvio", "Fecha Envío", 115, "dd/MM/yyyy HH:mm")
        MostrarColumnaDetalle("EstadoApertura", "Estado Apertura", 95)
        MostrarColumnaDetalle("FechaApertura", "Fecha Apertura", 115, "dd/MM/yyyy HH:mm")
        MostrarColumnaDetalle("EstadoConfirmacion", "Estado Firma", 90)
        MostrarColumnaDetalle("FechaConfirmacion", "Fecha Firma", 115, "dd/MM/yyyy HH:mm")
        MostrarColumnaDetalle("Accion", "Acción", 90)

        Funciones.ConfigurarEstiloGrid(dgvDetalle)
    End Sub

    Private Sub MostrarColumnaDetalle(nombreColumna As String, header As String, width As Integer, Optional formato As String = Nothing)
        If Not dgvDetalle.Columns.Contains(nombreColumna) Then
            Return
        End If

        dgvDetalle.Columns(nombreColumna).Visible = True
        dgvDetalle.Columns(nombreColumna).HeaderText = header
        dgvDetalle.Columns(nombreColumna).Width = width

        If Not String.IsNullOrWhiteSpace(formato) Then
            dgvDetalle.Columns(nombreColumna).DefaultCellStyle.Format = formato
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        CargarProcesos(ObtenerProcesoSeleccionadoCombo())
        CargarDatos()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub

    Private Sub dgvResumen_SelectionChanged(sender As Object, e As EventArgs) Handles dgvResumen.SelectionChanged
        If dgvResumen.CurrentRow Is Nothing OrElse dgvResumen.CurrentRow.Cells("IdProceso")?.Value Is Nothing Then
            Return
        End If

        Dim idProceso = Convert.ToInt32(dgvResumen.CurrentRow.Cells("IdProceso").Value)
        If cboProceso.SelectedValue IsNot Nothing AndAlso Convert.ToInt32(cboProceso.SelectedValue) = 0 Then
            dgvDetalle.DataSource = ObtenerDetalle(idProceso)
            ConfigurarDetalle()
            lblTotales.Text = $"Procesos: {dgvResumen.Rows.Count}    Registros: {dgvDetalle.Rows.Count}    Proceso actual: {idProceso}"
        End If
    End Sub
End Class
