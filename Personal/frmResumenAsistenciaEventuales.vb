Imports System.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmResumenAsistenciaEventuales
    Private Shared instancia As frmResumenAsistenciaEventuales
    Private filaActual As DataRow
    Private filaActualIndice As Integer = -1
    Private dtEmpleados As DataTable
    Private dtInasistencias As DataTable
    Private fechaDesde As Date
    Private fechaHasta As Date

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumenAsistenciaEventuales()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    ' Singleton pattern
    Public Shared Function ObtenerInstancia() As frmResumenAsistenciaEventuales
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumenAsistenciaEventuales()
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


            ' Establecer fechas por defecto
            TxtAño.Text = DateTime.Now.Year.ToString()

        Catch ex As Exception
            MessageBox.Show("Error al cargar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarMeses()
        Try
            CmbMeses.Items.Clear()

            Dim sql As String = "SELECT * From Meses;"
            Dim dtEmpleados = DSM.ExecuteQuery(DSM.Personal, sql)

            ' Cargar ComboBox de nombres
            CmbMeses.DataSource = dtEmpleados
            CmbMeses.DisplayMember = "Mes"
            CmbMeses.ValueMember = "idMes"
            CmbMeses.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show("Error al cargar meses: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarEmpleados()
        Try
            ' Deshabilitar el evento temporalmente
            RemoveHandler CmbNombres.SelectedIndexChanged, AddressOf CmbNombres_SelectedIndexChanged

            Dim sql As String = "SELECT * From Eventuales WHERE Baja = '' OR Baja IS NULL ORDER BY NOMBRE;"
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
            Dim sql As String = "SELECT * FROM Eventuales WHERE Legajo = @legajo"
            Dim parametros As New Dictionary(Of String, Object) From {{"@legajo", legajo}}
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                TxtOficina.Text = If(row("Escalafon") IsNot DBNull.Value, row("Escalafon").ToString(), "")
                TxtEncargado.Text = If(row("Jefe") IsNot DBNull.Value, row("Jefe").ToString(), "")
                TxtCategoria.Text = If(row("Cargo") IsNot DBNull.Value, row("Cargo").ToString(), "")
                TxtCaracter.Text = If(row("Caracter") IsNot DBNull.Value, row("Caracter").ToString(), "")
                TxtInstituto.Text = If(row("Instituto") IsNot DBNull.Value, row("Instituto").ToString(), "")
                TxtDiasLicencia.Text = If(row("LicAnual") IsNot DBNull.Value, row("LicAnual").ToString(), "0")
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

            ' Consulta similar al VB6: contar días únicos con movimientos sin motivo de inasistencia
            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Legajo", TxtLegajo.Text},
                {"@FechaDesde", fechaDesde},
                {"@FechaHasta", fechaHasta}
            }

            ' Consulta que replica la lógica del VB6
            Dim consultaDiasTrabajados As String = "SELECT Dia, COUNT(HsCumplidas) AS Cuenta " &
                "FROM Movimiento " &
                "WHERE Legajo = @Legajo " &
                "AND Dia BETWEEN @FechaDesde AND @FechaHasta " &
                "AND (MotivoInasistencia IS NULL) " &
                "GROUP BY Dia"

            Dim dtMovimientos As DataTable = DSM.ExecuteQuery(DSM.Personal, consultaDiasTrabajados, parametros)

            ' Contar los días trabajados (similar al loop del VB6)
            Dim diasTrabajados As Integer = dtMovimientos.Rows.Count

            ' Actualizar el campo de días trabajados en la interfaz
            TxtDiasTrabajados.Text = diasTrabajados.ToString()

            ' Calcular promedio basado en días trabajados vs días del período
            Dim totalDiasPeriodo As Integer = (fechaHasta - fechaDesde).Days + 1
            If totalDiasPeriodo > 0 Then
                Dim promedio As Double = (diasTrabajados / totalDiasPeriodo) * 100
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
            Dim consultaMovimientos As String = "SELECT Legajo,  " &
                "CONVERT(varchar, Dia, 103) as Dia, " &
                "CONVERT(varchar, Entro, 108) as Entro, " &
                "CONVERT(varchar, Salio, 108) as Salio, " &
                "HsCumplidas, " &
                "MotivoInasistencia, Comentario " &
                "FROM Movimiento " &
                "WHERE Legajo = @Legajo AND Dia BETWEEN @FechaDesde AND @FechaHasta " &
                "ORDER BY Dia"

            ' DSM es una clase estática, no necesita validación de instancia
            Dim dtMovimientos As DataTable = DSM.ExecuteQuery(DSM.Personal, consultaMovimientos, parametros)

            ' Validar que la consulta devolvió resultados válidos
            If dtMovimientos Is Nothing Then
                Return
            End If

            ' Mostrar movimientos en el DataGridView
            DgvListado.DataSource = dtMovimientos.DefaultView

            ' Variables para cálculos
            Dim totalHoras As Double = 0
            Dim diasConMovimientos As Integer = 0
            Dim promedioHoras As Double = 0
            Dim diasTrabajados As Integer = 0

            ' Procesar movimientos y calcular totales
            If dtMovimientos.Rows.Count > 0 Then
                For Each row As DataRow In dtMovimientos.Rows
                    Dim horas As Double = 0
                    If Not IsDBNull(row("HsCumplidas")) AndAlso Not String.IsNullOrEmpty(row("HsCumplidas").ToString()) Then
                        Try
                            ' Convertir tiempo HH:MM:SS a horas decimales
                            Dim timeValue As TimeSpan = TimeSpan.Parse(row("HsCumplidas").ToString())
                            horas = timeValue.TotalHours
                        Catch ex As Exception
                            ' Si falla la conversión, intentar como double directo
                            Try
                                horas = Convert.ToDouble(row("HsCumplidas"))
                            Catch
                                horas = 0
                            End Try
                        End Try
                    End If

                    If horas > 0 Then
                        totalHoras += horas
                        diasConMovimientos += 1
                    End If
                Next

                ' Calcular promedio de horas
                If diasConMovimientos > 0 Then
                    promedioHoras = totalHoras / diasConMovimientos
                End If
            End If

            ' Calcular días trabajados (considerando feriados y ausencias)
            diasTrabajados = CalcularDiasTrabajados()

            ' Actualizar campos de totales
            If TxtDiasTrabajados IsNot Nothing Then
                TxtDiasTrabajados.Text = diasTrabajados.ToString()
            End If

            If TxtPromedio IsNot Nothing Then
                TxtPromedio.Text = promedioHoras.ToString("N2")
            End If

            If TxtDiasPromedio IsNot Nothing Then
                TxtDiasPromedio.Text = diasConMovimientos.ToString()
            End If

            ' Mostrar mensaje de resumen
            If dtMovimientos.Rows.Count <= 0 Then
                DgvListado.DataSource = Nothing
                MessageBox.Show("No se encontraron movimientos para el período seleccionado", "Resumen de Asistencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            GridConfigurarColumnas()
            ConfigurarEstiloGrid(DgvListado)


        Catch ex As Exception
            MessageBox.Show("Error al generar el resumen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdInasistencias_Click(sender As Object, e As EventArgs) Handles CmdInasistencias.Click
        Try
            If String.IsNullOrEmpty(TxtLegajo.Text) Then
                MessageBox.Show("Debe seleccionar un empleado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrEmpty(TxtFechaDesde.Text) OrElse String.IsNullOrEmpty(TxtFechaHasta.Text) Then
                MessageBox.Show("Debe seleccionar un período de fechas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Parámetros para las consultas
            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Legajo", TxtLegajo.Text},
                {"@FechaDesde", Convert.ToDateTime(TxtFechaDesde.Text)},
                {"@FechaHasta", Convert.ToDateTime(TxtFechaHasta.Text)}
            }

            ' Consulta para obtener movimientos del empleado (similar a VB6)
            Dim consultaInasistencias As String = "SELECT Legajo,  " &
                "CONVERT(varchar, Dia, 103) as Dia, " &
                "MotivoInasistencia, " &
                "Comentario " &
                "FROM Movimiento " &
                "WHERE Legajo = @Legajo AND Dia BETWEEN @FechaDesde AND @FechaHasta " &
                "AND MotivoInasistencia <> '' " &
                "ORDER BY Dia"

            ' DSM es una clase estática, no necesita validación de instancia

            Dim dtMovimientos As DataTable = DSM.ExecuteQuery(DSM.Personal, consultaInasistencias, parametros)

            ' Validar que la consulta devolvió resultados válidos
            If dtMovimientos Is Nothing Then
                Return
            End If

            ' Mostrar movimientos en el DataGridView
            DgvInasistencias.DataSource = dtMovimientos.DefaultView
            DgvInasistencias.Visible = True
            ConfigurarEstiloGrid(DgvInasistencias)
            GridInasistenciasConfigurarColumnas()

        Catch ex As Exception
            MessageBox.Show("Error al generar el resumen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdImprime_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        ImprimirReporte()
    End Sub

    Private Sub CmdImprimo_Click(sender As Object, e As EventArgs)
        ImprimirReporteDetallado()
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

    Private Sub GridConfigurarColumnas()
        Try
            If DgvListado.Columns.Count > 0 Then
                DgvListado.Columns("Legajo").HeaderText = "Legajo"
                DgvListado.Columns("Legajo").Width = 50
                DgvListado.Columns("Dia").HeaderText = "Día"
                DgvListado.Columns("Dia").Width = 80
                DgvListado.Columns("Entro").HeaderText = "Entró"
                DgvListado.Columns("Entro").Width = 80
                DgvListado.Columns("Salio").HeaderText = "Salió"
                DgvListado.Columns("Salio").Width = 80
                DgvListado.Columns("HsCumplidas").HeaderText = "Hs Cumplidas"
                DgvListado.Columns("HsCumplidas").Width = 80
                DgvListado.Columns("HsCumplidas").DefaultCellStyle.Format = "hh\:mm\:ss"
                DgvListado.Columns("MotivoInasistencia").HeaderText = "Motivo Inasistencia"
                DgvListado.Columns("MotivoInasistencia").Width = 180
                DgvListado.Columns("Comentario").HeaderText = "Comentario"
                DgvListado.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            End If
        Catch ex As Exception
            MessageBox.Show("Error al configurar columnas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridInasistenciasConfigurarColumnas()
        Try
            If DgvInasistencias.Columns.Count > 0 Then
                DgvInasistencias.Columns("Legajo").HeaderText = "Legajo"
                DgvInasistencias.Columns("Legajo").Width = 50
                DgvInasistencias.Columns("Dia").HeaderText = "Día"
                DgvInasistencias.Columns("Dia").Width = 80
                DgvInasistencias.Columns("MotivoInasistencia").HeaderText = "Motivo Inasistencia"
                DgvInasistencias.Columns("MotivoInasistencia").Width = 180
                DgvInasistencias.Columns("Comentario").HeaderText = "Comentario"
                DgvInasistencias.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            End If
        Catch ex As Exception
            MessageBox.Show("Error al configurar columnas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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


    Private Sub DgvInasistencias_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvInasistencias.CellContentClick
        DgvInasistencias.Visible = False
    End Sub

    Private Sub DgvListado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellContentClick
        DgvInasistencias.Visible = False
    End Sub

    Private Sub frmResumenAsistencia_Click(sender As Object, e As EventArgs) Handles Me.Click
        DgvInasistencias.Visible = False
    End Sub
End Class