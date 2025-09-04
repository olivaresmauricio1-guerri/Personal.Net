Imports System.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmResumenAsistencia
    Private Shared instancia As frmResumenAsistencia
    Private filaActual As DataRow
    Private filaActualIndice As Integer = -1
    Private dtEmpleados As DataTable
    Private dtInasistencias As DataTable
    Private fechaDesde As Date
    Private fechaHasta As Date

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumenAsistencia()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    ' Singleton pattern
    Public Shared Function ObtenerInstancia() As frmResumenAsistencia
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumenAsistencia()
        End If
        Return instancia
    End Function

    Private Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmResumenAsistencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CargarMeses()
            CargarEmpleados()
            GridConfigurarColumnas()
            GridConfigurarColumnasInasistencias()
            FormModoConsulta()

            ' Establecer fechas por defecto
            TxtAño.Text = DateTime.Now.Year.ToString()
            TxtFechaDesde.Text = New Date(DateTime.Now.Year, 1, 1).ToString("dd/MM/yyyy")
            TxtFechaHasta.Text = New Date(DateTime.Now.Year, 12, 31).ToString("dd/MM/yyyy")
            fechaDesde = New Date(DateTime.Now.Year, 1, 1)
            fechaHasta = New Date(DateTime.Now.Year, 12, 31)

        Catch ex As Exception
            MessageBox.Show("Error al cargar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarMeses()
        Try
            CmbMeses.Items.Clear()
            CmbMeses.Items.Add("Enero")
            CmbMeses.Items.Add("Febrero")
            CmbMeses.Items.Add("Marzo")
            CmbMeses.Items.Add("Abril")
            CmbMeses.Items.Add("Mayo")
            CmbMeses.Items.Add("Junio")
            CmbMeses.Items.Add("Julio")
            CmbMeses.Items.Add("Agosto")
            CmbMeses.Items.Add("Septiembre")
            CmbMeses.Items.Add("Octubre")
            CmbMeses.Items.Add("Noviembre")
            CmbMeses.Items.Add("Diciembre")
            CmbMeses.SelectedIndex = DateTime.Now.Month - 1
        Catch ex As Exception
            MessageBox.Show("Error al cargar meses: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarEmpleados()
        Try
            ' Deshabilitar el evento temporalmente
            RemoveHandler CmbNombres.SelectedIndexChanged, AddressOf CmbNombres_SelectedIndexChanged

            Dim sql As String = "SELECT * From Agentes WHERE Baja = '' OR Baja IS NULL ORDER BY NOMBRE;"
            Dim dtEmpleados = DSM.ExecuteQuery(DSM.Personal, sql)

            ' Cargar ComboBox de nombres
            CmbNombres.DataSource = dtEmpleados
            CmbNombres.DisplayMember = "Nombre"
            CmbNombres.ValueMember = "Legajo"
            CmbNombres.SelectedIndex = -1

            ' Rehabilitar el evento
            AddHandler CmbNombres.SelectedIndexChanged, AddressOf CmbNombres_SelectedIndexChanged

        Catch ex As Exception
            MessageBox.Show("Error al cargar empleados: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado)
        End If
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        AplicarSeleccionActual()
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

    Private Sub chkEncabezados_CheckedChanged(sender As Object, e As EventArgs) Handles chkEncabezados.CheckedChanged
        DgvListado.ColumnHeadersVisible = chkEncabezados.Checked
        DgvInasistencias.ColumnHeadersVisible = chkEncabezados.Checked
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado)
    End Sub

    ' Eventos de botones de fecha
    Private Sub cmdFechaDesde_Click(sender As Object, e As EventArgs) Handles CmdFechaDesde.Click
        ' Abrir selector de fecha para fecha desde
        Using frm As New Form()
            Dim dtp As New DateTimePicker()
            dtp.Value = If(DateTime.TryParse(TxtFechaDesde.Text, Nothing), DateTime.Parse(TxtFechaDesde.Text), DateTime.Now)
            dtp.Dock = DockStyle.Fill
            frm.Controls.Add(dtp)
            frm.Size = New Size(250, 100)
            frm.StartPosition = FormStartPosition.CenterParent
            If frm.ShowDialog() = DialogResult.OK Then
                TxtFechaDesde.Text = dtp.Value.ToString("dd/MM/yyyy")
            End If
        End Using
    End Sub

    Private Sub cmdFechaHasta_Click(sender As Object, e As EventArgs) Handles CmdFechaHasta.Click
        ' Abrir selector de fecha para fecha hasta
        Using frm As New Form()
            Dim dtp As New DateTimePicker()
            dtp.Value = If(DateTime.TryParse(TxtFechaHasta.Text, Nothing), DateTime.Parse(TxtFechaHasta.Text), DateTime.Now)
            dtp.Dock = DockStyle.Fill
            frm.Controls.Add(dtp)
            frm.Size = New Size(250, 100)
            frm.StartPosition = FormStartPosition.CenterParent
            If frm.ShowDialog() = DialogResult.OK Then
                TxtFechaHasta.Text = dtp.Value.ToString("dd/MM/yyyy")
            End If
        End Using
    End Sub

    ' Eventos de ComboBox
    Private Sub CmbMeses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMeses.SelectedIndexChanged
        If CmbMeses.SelectedIndex >= 0 Then
            Dim mesSeleccionado As Integer = CmbMeses.SelectedIndex + 1
            Dim año As Integer = If(String.IsNullOrEmpty(TxtAño.Text), DateTime.Now.Year, Convert.ToInt32(TxtAño.Text))

            ' Calcular primer y último día del mes
            Dim primerDia As New Date(año, mesSeleccionado, 1)
            Dim ultimoDia As Date = primerDia.AddMonths(1).AddDays(-1)

            TxtFechaDesde.Text = primerDia.ToString("dd/MM/yyyy")
            TxtFechaHasta.Text = ultimoDia.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Sub CmbNombres_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNombres.SelectedIndexChanged
        If CmbNombres.SelectedValue IsNot Nothing Then
            Dim legajo As String = CmbNombres.SelectedValue.ToString()
            TxtLegajo.Text = legajo
            CargarDatosEmpleado(legajo)
        End If
    End Sub

    ' Cargar datos del empleado seleccionado
    Private Sub CargarDatosEmpleado(legajo As String)
        Try
            Dim sql As String = "SELECT * FROM Agentes WHERE Legajo = @legajo"
            Dim parametros As New Dictionary(Of String, Object) From {{"@legajo", legajo}}
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                TxtOficina.Text = If(row("Oficina") IsNot DBNull.Value, row("Oficina").ToString(), "")
                TxtEncargado.Text = If(row("Jefe") IsNot DBNull.Value, row("Jefe").ToString(), "")
                TxtCategoria.Text = If(row("Cargo") IsNot DBNull.Value, row("Cargo").ToString(), "")
                TxtCaracter.Text = If(row("Caracter") IsNot DBNull.Value, row("Caracter").ToString(), "")
                TxtInstituto.Text = If(row("Instituto") IsNot DBNull.Value, row("Instituto").ToString(), "")
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar datos del empleado: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Cálculos de asistencia
    Private Function CalcularDiasTrabajados() As Integer
        Try
            If String.IsNullOrEmpty(TxtLegajo.Text) OrElse String.IsNullOrEmpty(TxtFechaDesde.Text) OrElse String.IsNullOrEmpty(TxtFechaHasta.Text) Then
                Return 0
            End If

            Dim fechaDesde As DateTime = Convert.ToDateTime(TxtFechaDesde.Text)
            Dim fechaHasta As DateTime = Convert.ToDateTime(TxtFechaHasta.Text)

            ' Calcular días hábiles en el período (lunes a viernes)
            Dim diasHabiles As Integer = 0
            Dim fechaActual As DateTime = fechaDesde

            While fechaActual <= fechaHasta
                ' Contar solo días de lunes a viernes
                If fechaActual.DayOfWeek <> DayOfWeek.Saturday AndAlso fechaActual.DayOfWeek <> DayOfWeek.Sunday Then
                    diasHabiles += 1
                End If
                fechaActual = fechaActual.AddDays(1)
            End While

            ' Restar feriados si existe tabla de feriados
            Dim parametrosFeriados As New Dictionary(Of String, Object) From {
                {"@FechaDesde", fechaDesde},
                {"@FechaHasta", fechaHasta}
            }

            Dim consultaFeriados As String = "SELECT COUNT(*) FROM Feriados WHERE Fecha BETWEEN @FechaDesde AND @FechaHasta AND DATEPART(weekday, Fecha) NOT IN (1, 7)"
            Dim feriados As Integer = 0

            Try
                Dim dtFeriados As DataTable = DSM.ExecuteQuery(DSM.Personal, consultaFeriados, parametrosFeriados)
                If dtFeriados.Rows.Count > 0 AndAlso Not IsDBNull(dtFeriados.Rows(0)(0)) Then
                    feriados = Convert.ToInt32(dtFeriados.Rows(0)(0))
                Else
                    feriados = 0
                End If
            Catch
                ' Si no existe la tabla Feriados, continuar sin restar feriados
                feriados = 0
            End Try

            ' Restar inasistencias
            Dim parametrosInasistencias As New Dictionary(Of String, Object) From {
                 {"@Legajo", TxtLegajo.Text},
                {"@FechaDesde", fechaDesde},
                {"@FechaHasta", fechaHasta}
            }

            Dim consultaInasistencias As String = "SELECT COUNT(*) FROM Inasistencias WHERE Legajo = @Legajo AND Fecha BETWEEN @FechaDesde AND @FechaHasta AND DATEPART(weekday, Fecha) NOT IN (1, 7)"
            Dim dtInasistencias As DataTable = DSM.ExecuteQuery(DSM.Personal, consultaInasistencias, parametrosInasistencias)
            Dim inasistencias As Integer = If(dtInasistencias.Rows.Count > 0, Convert.ToInt32(dtInasistencias.Rows(0)(0)), 0)

            ' Calcular días trabajados efectivos
            Dim diasTrabajados As Integer = Math.Max(0, diasHabiles - feriados - inasistencias)

            ' Actualizar el campo de días trabajados en la interfaz
            TxtDiasTrabajados.Text = diasTrabajados.ToString()

            ' Calcular promedio si hay días trabajados
            If diasTrabajados > 0 AndAlso diasHabiles > 0 Then
                Dim promedio As Double = (diasTrabajados / diasHabiles) * 100
                TxtPromedio.Text = promedio.ToString("F2") & "%"
            Else
                TxtPromedio.Text = "0.00%"
            End If

            Return diasTrabajados

        Catch ex As Exception
            MessageBox.Show("Error al calcular días trabajados: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    ' Eventos de botones principales
    Private Sub cmdResumen_Click(sender As Object, e As EventArgs) Handles CmdResumen.Click
        Try
            If String.IsNullOrEmpty(TxtLegajo.Text) Then
                MessageBox.Show("Debe seleccionar un empleado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrEmpty(TxtFechaDesde.Text) OrElse String.IsNullOrEmpty(TxtFechaHasta.Text) Then
                MessageBox.Show("Debe seleccionar un período de fechas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Limpiar campos de totales
            TxtDiasTrabajados.Text = ""
            TxtPromedio.Text = ""
            TxtDiasPromedio.Text = ""

            ' Parámetros para las consultas
            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Legajo", TxtLegajo.Text},
                {"@FechaDesde", Convert.ToDateTime(TxtFechaDesde.Text)},
                {"@FechaHasta", Convert.ToDateTime(TxtFechaHasta.Text)}
            }

            ' Consulta para obtener movimientos del empleado (similar a VB6)
            Dim consultaMovimientos As String = "SELECT " &
                "CONVERT(varchar, Dia, 103) as Fecha, " &
                "CONVERT(varchar, Entro, 108) as Entro, " &
                "CONVERT(varchar, Salio, 108) as Salio, " &
                "CASE WHEN Entro IS NOT NULL AND Salio IS NOT NULL " &
                "THEN DATEDIFF(minute, Entro, Salio) / 60.0 " &
                "ELSE 0 END as HorasTrabajadas, " &
                "DATENAME(weekday, Dia) as DiaSemana " &
                "FROM Movimiento " &
                "WHERE Legajo = @Legajo AND Dia BETWEEN @FechaDesde AND @FechaHasta " &
                "AND DATEPART(weekday, Dia) NOT IN (1, 7) " &
                "ORDER BY Dia"

            Dim dtMovimientos As DataTable = DSM.ExecuteQuery(DSM.Personal, consultaMovimientos, parametros)

            ' Variables para cálculos
            Dim totalHoras As Double = 0
            Dim diasConMovimientos As Integer = 0
            Dim promedioHoras As Double = 0
            Dim diasTrabajados As Integer = 0

            ' Procesar movimientos y calcular totales
            If dtMovimientos.Rows.Count > 0 Then
                For Each row As DataRow In dtMovimientos.Rows
                    Dim horas As Double = If(IsDBNull(row("HorasTrabajadas")), 0, Convert.ToDouble(row("HorasTrabajadas")))
                    If horas > 0 Then
                        totalHoras += horas
                        diasConMovimientos += 1
                    End If
                Next

                ' Calcular promedio de horas
                If diasConMovimientos > 0 Then
                    promedioHoras = totalHoras / diasConMovimientos
                End If

                ' Mostrar movimientos en el DataGridView
                DgvListado.DataSource = dtMovimientos
                ConfigurarEstiloGrid(DgvListado)

                ' Configurar columnas específicas
                If DgvListado.Columns.Contains("HorasTrabajadas") Then
                    DgvListado.Columns("HorasTrabajadas").DefaultCellStyle.Format = "N2"
                    DgvListado.Columns("HorasTrabajadas").HeaderText = "Horas"
                    DgvListado.Columns("HorasTrabajadas").Width = 80
                End If

                ' Colorear filas según horas trabajadas
                For Each row As DataGridViewRow In DgvListado.Rows
                    Dim horas As Double = If(IsDBNull(row.Cells("HorasTrabajadas").Value), 0, Convert.ToDouble(row.Cells("HorasTrabajadas").Value))
                    If horas = 0 Then
                        row.DefaultCellStyle.BackColor = Color.LightCoral
                    ElseIf horas < 8 Then
                        row.DefaultCellStyle.BackColor = Color.LightYellow
                    Else
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                    End If
                Next
            End If

            ' Calcular días trabajados (considerando feriados y ausencias)
            diasTrabajados = CalcularDiasTrabajados()

            ' Actualizar campos de totales
            TxtDiasTrabajados.Text = diasTrabajados.ToString()
            TxtPromedio.Text = promedioHoras.ToString("N2")
            TxtDiasPromedio.Text = diasConMovimientos.ToString()

            ' Mostrar mensaje de resumen
            If dtMovimientos.Rows.Count > 0 Then
                MessageBox.Show($"Resumen generado.{vbCrLf}" &
                              $"Días trabajados: {diasTrabajados}{vbCrLf}" &
                              $"Días con movimientos: {diasConMovimientos}{vbCrLf}" &
                              $"Total horas: {totalHoras:N2}{vbCrLf}" &
                              $"Promedio horas/día: {promedioHoras:N2}",
                              "Resumen de Asistencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                DgvListado.DataSource = Nothing
                MessageBox.Show("No se encontraron movimientos para el período seleccionado", "Resumen de Asistencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al generar el resumen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdFaltas_Click(sender As Object, e As EventArgs) Handles CmdInasistencias.Click
        Try
            If String.IsNullOrEmpty(TxtLegajo.Text) Then
                MessageBox.Show("Debe seleccionar un empleado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Cargar inasistencias del empleado en el período seleccionado
            Dim parametros As New Dictionary(Of String, Object) From {
                 {"@Legajo", TxtLegajo.Text},
                {"@FechaDesde", Convert.ToDateTime(TxtFechaDesde.Text)},
                {"@FechaHasta", Convert.ToDateTime(TxtFechaHasta.Text)}
            }

            Dim consulta As String = "SELECT Fecha, TipoInasistencia, Observaciones FROM Inasistencias WHERE Legajo = @Legajo AND Fecha BETWEEN @FechaDesde AND @FechaHasta ORDER BY Fecha"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, consulta, parametros)

            ' Mostrar las inasistencias en el DataGridView
            If dt.Rows.Count > 0 Then
                DgvInasistencias.DataSource = dt
                ConfigurarEstiloGrid(DgvInasistencias)

                ' Configurar columnas específicas para inasistencias
                DgvInasistencias.Columns("Fecha").HeaderText = "Fecha"
                DgvInasistencias.Columns("Fecha").Width = 100
                DgvInasistencias.Columns("TipoInasistencia").HeaderText = "Tipo"
                DgvInasistencias.Columns("TipoInasistencia").Width = 150
                DgvInasistencias.Columns("Observaciones").HeaderText = "Observaciones"
                DgvInasistencias.Columns("Observaciones").Width = 200

                MessageBox.Show($"Se encontraron {dt.Rows.Count} inasistencias en el período seleccionado", "Inasistencias", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                DgvInasistencias.DataSource = Nothing
                MessageBox.Show("No se encontraron inasistencias en el período seleccionado", "Inasistencias", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al cargar las inasistencias: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdImprime_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        ImprimirReporte()
    End Sub

    Private Sub CmdImprimo_Click(sender As Object, e As EventArgs)
        ImprimirReporteDetallado()
    End Sub

    Private Sub Command1_Click(sender As Object, e As EventArgs)
        ' Funcionalidad adicional según sea necesario
        MessageBox.Show("Funcionalidad en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    ' Métodos de impresión y reportes
    Private Sub ImprimirReporte()
        Try
            If String.IsNullOrEmpty(TxtLegajo.Text) Then
                MessageBox.Show("Debe seleccionar un empleado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Ejecutar reporte externo usando Reportes.exe
            Dim parametros As String = $"Personal resumen {TxtLegajo.Text} {TxtFechaDesde.Text} {TxtFechaHasta.Text}"
            Process.Start(General.ReportesPath, parametros)

        Catch ex As Exception
            MessageBox.Show("Error al generar el reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ImprimirReporteDetallado()
        Try
            If String.IsNullOrEmpty(TxtLegajo.Text) Then
                MessageBox.Show("Debe seleccionar un empleado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Ejecutar reporte detallado usando Reportes.exe
            Dim parametros As String = $"Personal detallado {TxtLegajo.Text} {TxtFechaDesde.Text} {TxtFechaHasta.Text}"
            Process.Start(General.ReportesPath, parametros)

        Catch ex As Exception
            MessageBox.Show("Error al generar el reporte detallado: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AplicarSeleccionActual()
        Try
            If DgvListado.SelectedRows.Count > 0 Then
                filaActualIndice = DgvListado.SelectedRows(0).Index
                Dim dv As DataView = CType(DgvListado.DataSource, DataView)
                If dv IsNot Nothing AndAlso filaActualIndice < dv.Count Then
                    filaActual = dv(filaActualIndice).Row
                    FormObtenerSeleccionado()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error al aplicar selección: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridConfigurarColumnas()
        Try
            If DgvListado.Columns.Count > 0 Then
                DgvListado.Columns("Dia").HeaderText = "Día"
                DgvListado.Columns("Dia").Width = 80
                DgvListado.Columns("Entro").HeaderText = "Entró"
                DgvListado.Columns("Entro").Width = 100
                DgvListado.Columns("Salio").HeaderText = "Salió"
                DgvListado.Columns("Salio").Width = 100
                DgvListado.Columns("HsCumplidas").HeaderText = "Hs Cumplidas"
                DgvListado.Columns("HsCumplidas").Width = 100
                DgvListado.Columns("MotivoInasistencia").HeaderText = "Motivo Inasistencia"
                DgvListado.Columns("MotivoInasistencia").Width = 200
                DgvListado.Columns("SinFicha").HeaderText = "Sin Ficha"
                DgvListado.Columns("SinFicha").Width = 50
                DgvListado.Columns("Comentario").HeaderText = "Comentario"
                DgvListado.Columns("Comentario").Width = 250

            End If
        Catch ex As Exception
            MessageBox.Show("Error al configurar columnas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridConfigurarColumnasInasistencias()
        Try
            ' Configurar columnas del grid de inasistencias cuando se carguen los datos
        Catch ex As Exception
            MessageBox.Show("Error al configurar columnas de inasistencias: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormLimpiarSeleccionado()
        Try
            TxtLegajo.Text = ""
            TxtOficina.Text = ""
            TxtEncargado.Text = ""
            TxtCategoria.Text = ""
            TxtCaracter.Text = ""
            TxtInstituto.Text = ""
            TxtHorasContrato.Text = ""
            TxtObligacionMensual.Text = ""
            TxtDiasLicencia.Text = ""
            TxtVac2019.Text = ""
            TxtVac2020.Text = ""
            TxtVac2021.Text = ""
            TxtVac2022.Text = ""
            TxtVac2023.Text = ""
            TxtVac2024.Text = ""
            TxtVac2025.Text = ""
            TxtVac2026.Text = ""
            TxtDiasTrabajados.Text = ""
            TxtPromedio.Text = ""
            TxtDiasPromedio.Text = ""
            TxtComentarios.Text = ""

            DgvInasistencias.DataSource = Nothing
        Catch ex As Exception
            MessageBox.Show("Error al limpiar campos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormObtenerSeleccionado()
        Try
            If filaActual IsNot Nothing Then
                TxtLegajo.Text = filaActual("Legajo").ToString()
                TxtOficina.Text = filaActual("Oficina").ToString()
                TxtEncargado.Text = filaActual("Encargado").ToString()
                TxtCategoria.Text = filaActual("Categoria").ToString()
                TxtCaracter.Text = filaActual("Caracter").ToString()
                TxtInstituto.Text = filaActual("Instituto").ToString()
                TxtHorasContrato.Text = filaActual("HorasContrato").ToString()
                TxtObligacionMensual.Text = filaActual("ObligacionMensual").ToString()
                TxtDiasLicencia.Text = filaActual("DiasLicenciaAnual").ToString()
                TxtVac2019.Text = filaActual("Vac2019").ToString()
                TxtVac2020.Text = filaActual("Vac2020").ToString()
                TxtVac2021.Text = filaActual("Vac2021").ToString()
                TxtVac2022.Text = filaActual("Vac2022").ToString()
                TxtVac2023.Text = filaActual("Vac2023").ToString()
                TxtVac2024.Text = filaActual("Vac2024").ToString()
                TxtVac2025.Text = filaActual("Vac2025").ToString()
                TxtVac2026.Text = filaActual("Vac2026").ToString()

                ' Sincronizar ComboBox
                For i As Integer = 0 To CmbNombres.Items.Count - 1
                    CmbNombres.SelectedIndex = i
                    If CmbNombres.SelectedValue.ToString() = TxtLegajo.Text Then
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error al obtener datos seleccionados: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormModoConsulta()
        Try
            ' En modo consulta, todos los campos están deshabilitados para edición
            ' Solo se permite navegación y consulta
        Catch ex As Exception
            MessageBox.Show("Error al configurar modo consulta: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GenerarResumenAsistencia()
        Try
            If filaActual IsNot Nothing Then
                Dim legajo As String = TxtLegajo.Text
                Dim sql As String = "SELECT COUNT(*) as DiasTrabajados " &
                                   "FROM Asistencia a " &
                                   "WHERE a.Legajo = @Legajo " &
                                   "AND a.Fecha BETWEEN @FechaDesde AND @FechaHasta " &
                                   "AND a.Presente = 1"

                Dim parametros As New Dictionary(Of String, Object)
                parametros.Add("@Legajo", legajo)
                parametros.Add("@FechaDesde", fechaDesde)
                parametros.Add("@FechaHasta", fechaHasta)

                Dim dtResumen As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

                If dtResumen.Rows.Count > 0 Then
                    Dim diasTrabajados As Integer = Convert.ToInt32(dtResumen.Rows(0)("DiasTrabajados"))
                    TxtDiasTrabajados.Text = diasTrabajados.ToString()

                    ' Calcular promedio
                    Dim diasTotales As Integer = (fechaHasta - fechaDesde).Days + 1
                    Dim promedio As Double = If(diasTotales > 0, (diasTrabajados / diasTotales) * 100, 0)
                    TxtPromedio.Text = promedio.ToString("F2") & "%"

                    ' Calcular días promedio
                    Dim horasContrato As Double = If(IsNumeric(TxtHorasContrato.Text), Convert.ToDouble(TxtHorasContrato.Text), 0)
                    Dim diasPromedio As Double = If(horasContrato > 0, (diasTrabajados * 8) / horasContrato, 0)
                    TxtDiasPromedio.Text = diasPromedio.ToString("F2")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error al generar resumen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarInasistencias()
        Try
            If filaActual IsNot Nothing Then
                Dim legajo As String = TxtLegajo.Text
                Dim sql As String = "SELECT i.Fecha, ti.Descripcion as TipoInasistencia, i.Observaciones " &
                                   "FROM Inasistencias i " &
                                   "INNER JOIN TipoInasistencias ti ON i.CodigoInasistencia = ti.Codigo " &
                                   "WHERE i.Legajo = @Legajo " &
                                   "AND i.Fecha BETWEEN @FechaDesde AND @FechaHasta " &
                                   "ORDER BY i.Fecha DESC"

                Dim parametros As New Dictionary(Of String, Object)
                parametros.Add("@Legajo", legajo)
                parametros.Add("@FechaDesde", fechaDesde)
                parametros.Add("@FechaHasta", fechaHasta)

                dtInasistencias = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
                DgvInasistencias.DataSource = dtInasistencias

                ' Configurar columnas
                If DgvInasistencias.Columns.Count > 0 Then
                    DgvInasistencias.Columns("Fecha").HeaderText = "Fecha"
                    DgvInasistencias.Columns("Fecha").Width = 100
                    DgvInasistencias.Columns("TipoInasistencia").HeaderText = "Tipo"
                    DgvInasistencias.Columns("TipoInasistencia").Width = 150
                    DgvInasistencias.Columns("Observaciones").HeaderText = "Observaciones"
                    DgvInasistencias.Columns("Observaciones").Width = 200
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar inasistencias: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CopiarDataGrid(dgv As DataGridView)
        Try
            If dgv.SelectedRows.Count > 0 Then
                Dim texto As String = ""

                ' Agregar encabezados si están visibles
                If chkEncabezados.Checked Then
                    For Each col As DataGridViewColumn In dgv.Columns
                        If col.Visible Then
                            texto += col.HeaderText + vbTab
                        End If
                    Next
                    texto = texto.TrimEnd(vbTab) + vbCrLf
                End If

                ' Agregar filas seleccionadas
                For Each fila As DataGridViewRow In dgv.SelectedRows
                    For Each col As DataGridViewColumn In dgv.Columns
                        If col.Visible Then
                            texto += If(fila.Cells(col.Index).Value?.ToString(), "") + vbTab
                        End If
                    Next
                    texto = texto.TrimEnd(vbTab) + vbCrLf
                Next

                Clipboard.SetText(texto)
                MessageBox.Show("Datos copiados al portapapeles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Debe seleccionar al menos una fila.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al copiar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class