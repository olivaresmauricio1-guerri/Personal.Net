Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Pdf.Canvas.Parser.Filter
Imports iText.Kernel.Pdf.Canvas.Parser.Listener
Imports iText.Signatures.Validation
Imports Microsoft.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmEnvioRecibos
    Private Const NombreColumnaSeleccion As String = "Seleccionado"
    Private Const CarpetaBaseRecibos As String = "F:\Personal.Net\Recibos"

    Private Sub frmEnvioRecibos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrepararGrilla()
        CargoProcesos()
    End Sub
    Private Sub CargoProcesos(Optional idProcesoSeleccionado As Integer? = Nothing)
        'Cargo los procesos en funciona del mes y año seleccionado en el dtpFechaProceso
        Dim sql = "SELECT IdProceso, PeriodoProcesado, FechaInicio, FechaFin, ISNULL(FechaFin, FechaInicio) AS FechaProceso, UsuarioProceso, ArchivoOrigen, CarpetaOrigen, EstadoProceso, CantidadPaginasOrigen, CantidadLegajosDetectados, CantidadArchivosGenerados, CantidadPaginasOmitidas, Observaciones " &
                  "FROM RecibosProcesos WHERE PeriodoProcesado >= @FechaInicio AND PeriodoProcesado <= @FechaFin " &
                  "ORDER BY ISNULL(FechaFin, FechaInicio) DESC"
        Dim parametros = CmdParams(
            "@FechaInicio", New DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, 1),
            "@FechaFin", New DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, DateTime.DaysInMonth(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month)))
        Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
        dgvProcesos.DataSource = tabla
        ConfiguraColProcesos()
        Funciones.ConfigurarEstiloGrid(dgvProcesos)
        SeleccionarProcesoEnGrilla(idProcesoSeleccionado)
    End Sub
    Private Sub CargoDetalleProceso(idProceso As Integer)
        Dim sql = "SELECT IdDetalle, Legajo, ArchivoPdf AS Archivo, CantidadPaginas AS Paginas, PaginasOrigen, RutaPdf AS Ruta, EstadoEnvio, MensajeEnvio, FechaEnvio " &
                  "FROM RecibosProcesosDetalles WHERE IdProceso = @IdProceso ORDER BY Legajo"
        Dim parametros = CmdParams("@IdProceso", idProceso)
        Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
        dgvListaRecibos.DataSource = tabla
        AsegurarColumnaSeleccion()
        ConfigurarColumnasGrilla()
        ConfigurarEstiloGrid(dgvListaRecibos)
    End Sub
    Private Sub btnCargaRecibos_Click(sender As Object, e As EventArgs) Handles btnCargaRecibos.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf"
            openFileDialog.Title = "Seleccione el archivo PDF original"
            openFileDialog.Multiselect = False

            If openFileDialog.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If

            ProcesarArchivoSeleccionado(openFileDialog.FileName)
        End Using
    End Sub

    Private Sub ProcesarArchivoSeleccionado(rutaPdfOrigen As String)
        Dim cursorAnterior = Cursor.Current
        Dim idProceso As Integer? = Nothing
        Dim fechaProceso = DateTime.Now
        Dim periodoProcesado = ObtenerPeriodoProcesadoFecha()

        Try
            Cursor.Current = Cursors.WaitCursor
            btnCargaRecibos.Enabled = False
            idProceso = CrearProcesoLog(rutaPdfOrigen, fechaProceso, periodoProcesado)

            Dim resultado = SepararRecibosPorLegajo(rutaPdfOrigen, idProceso.Value, fechaProceso, periodoProcesado)
            ActualizarProcesoLogFinalizado(idProceso.Value, resultado)
            CargoProcesos(idProceso.Value)

            Dim mensaje = $"Se generaron {resultado.ArchivosGenerados.Count} archivo(s) en '{CarpetaBaseRecibos}'."
            If resultado.PaginasOmitidas.Count > 0 Then
                mensaje &= Environment.NewLine & $"Páginas omitidas por no tener legajo detectable: {String.Join(", ", resultado.PaginasOmitidas)}."
            End If

            MessageBox.Show(mensaje, "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If idProceso.HasValue Then
                ActualizarProcesoLogError(idProceso.Value, ex.Message)
            End If
            MessageBox.Show($"No se pudo procesar el archivo seleccionado.{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnCargaRecibos.Enabled = True
            Cursor.Current = cursorAnterior
        End Try
    End Sub

    Private Function SepararRecibosPorLegajo(rutaPdfOrigen As String, idProceso As Integer, fechaProceso As DateTime, periodoProcesado As DateTime) As ResultadoProcesoRecibos
        Dim resultado As New ResultadoProcesoRecibos()
        Dim paginasPorLegajo As New Dictionary(Of String, List(Of Integer))(StringComparer.OrdinalIgnoreCase)
        Dim carpetaDestino = Path.GetDirectoryName(rutaPdfOrigen)
        Dim marcaTiempo = $"{periodoProcesado:yyyyMM}_{fechaProceso:yyyyMMdd_HHmmss}"

        Using lector As New PdfReader(rutaPdfOrigen)
            Using pdfOrigen As New PdfDocument(lector)
                resultado.CantidadPaginasOrigen = pdfOrigen.GetNumberOfPages()

                For numeroPagina = 1 To pdfOrigen.GetNumberOfPages()
                    Dim textoPagina = ExtraerTextoPagina(pdfOrigen, numeroPagina)

                    If Not EsPaginaOriginal(textoPagina) Then
                        Continue For
                    End If

                    Dim legajo = ObtenerLegajo(textoPagina)

                    If String.IsNullOrWhiteSpace(legajo) Then
                        resultado.PaginasOmitidas.Add(numeroPagina)
                        Continue For
                    End If

                    If Not paginasPorLegajo.ContainsKey(legajo) Then
                        paginasPorLegajo(legajo) = New List(Of Integer)()
                    End If

                    paginasPorLegajo(legajo).Add(numeroPagina)
                Next

                If paginasPorLegajo.Count = 0 Then
                    Throw New InvalidOperationException("No se encontró ningún legajo válido dentro del archivo.")
                End If

                resultado.CantidadLegajosDetectados = paginasPorLegajo.Count
                Dim legajosOrdenados = New List(Of String)(paginasPorLegajo.Keys)
                legajosOrdenados.Sort(AddressOf CompararLegajos)

                For Each legajo In legajosOrdenados
                    Dim paginas = paginasPorLegajo(legajo)
                    Dim rutaDestino = ObtenerRutaDestinoUnica(carpetaDestino, legajo, marcaTiempo)

                    Using escritor As New PdfWriter(rutaDestino)
                        Using pdfDestino As New PdfDocument(escritor)
                            pdfOrigen.CopyPagesTo(paginas, pdfDestino)
                        End Using
                    End Using

                    resultado.ArchivosGenerados.Add(New ArchivoGeneradoInfo With {
                        .Legajo = legajo,
                        .Archivo = Path.GetFileName(rutaDestino),
                        .CantidadPaginas = paginas.Count,
                        .PaginasOrigen = String.Join(", ", paginas),
                        .RutaCompleta = rutaDestino
                    })

                    GuardarDetalleProceso(idProceso, resultado.ArchivosGenerados(resultado.ArchivosGenerados.Count - 1), fechaProceso)
                Next
            End Using
        End Using

        Return resultado
    End Function

    Private Function ExtraerTextoPagina(pdfOrigen As PdfDocument, numeroPagina As Integer) As String
        Dim pagina = pdfOrigen.GetPage(numeroPagina)
        Dim tamañoPagina = pagina.GetPageSize()
        Dim anchoMitad = tamañoPagina.GetWidth() / 2.0F
        Dim regionIzquierda As New iText.Kernel.Geom.Rectangle(tamañoPagina.GetLeft(), tamañoPagina.GetBottom(), anchoMitad, tamañoPagina.GetHeight())
        Dim estrategiaRegion As ITextExtractionStrategy =
            New FilteredTextEventListener(New LocationTextExtractionStrategy(), New TextRegionEventFilter(regionIzquierda))

        Dim textoIzquierdo = PdfTextExtractor.GetTextFromPage(pagina, estrategiaRegion)
        If Not String.IsNullOrWhiteSpace(textoIzquierdo) Then
            Return textoIzquierdo
        End If

        Return PdfTextExtractor.GetTextFromPage(pagina, New LocationTextExtractionStrategy())
    End Function

    Private Function ObtenerLegajo(textoPagina As String) As String
        If String.IsNullOrWhiteSpace(textoPagina) Then
            Return String.Empty
        End If

        Dim patronesTextoOriginal = {
            "LEGAJO\s*[\r\n]+\s*APELLIDO.*?[\r\n]+\s*(\d{1,6})\b",
            "LEGAJO\s*[\r\n]+\s*(\d{1,6})\b",
            "APELLIDO\s+Y\s+NOMBRE.*?[\r\n]+\s*(\d{1,6})\b\s+[A-ZÁÉÍÓÚÜÑ]"
        }

        For Each patron In patronesTextoOriginal
            Dim coincidencia = Regex.Match(textoPagina, patron, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
            If coincidencia.Success Then
                Return coincidencia.Groups(1).Value
            End If
        Next

        Dim textoNormalizado = NormalizarTextoParaBusqueda(textoPagina)
        Dim patrones = {
            "\bLEGAJO\s+APELLIDOYNOMBREDELEMPLEA(?:DO)?\s+CUIL(?:FECHAÚLTIMOINGRESO)?\s+(\d{1,6})\b",
            "\bLEGAJOCUIL\s+(\d{1,6})\b",
            "\bLEGAJO\s+(\d{1,6})\b",
            "\bLEGAJO\b.*?\b(\d{1,6})\b\s+[A-Z]",
            "\bNRO\.?\s+LEGAJO\b\D{0,20}(\d{1,6})\b",
            "\bAPELLIDO\b.*?\bCUIL\b.*?\b(\d{1,6})\b\s+[A-Z]"
        }

        For Each patron In patrones
            Dim coincidencia = Regex.Match(textoNormalizado, patron, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
            If coincidencia.Success Then
                Return coincidencia.Groups(1).Value
            End If
        Next

        Return String.Empty
    End Function

    Private Function EsPaginaOriginal(textoPagina As String) As Boolean
        If String.IsNullOrWhiteSpace(textoPagina) Then
            Return False
        End If

        Dim lineas = textoPagina.Split({ControlChars.Cr, ControlChars.Lf}, StringSplitOptions.RemoveEmptyEntries)
        Dim cantidadLineas = Math.Min(5, lineas.Length)
        Dim primerasLineas = String.Join(" ", lineas, 0, cantidadLineas)
        Dim textoEncabezado = NormalizarTextoParaBusqueda(primerasLineas)

        If textoEncabezado.Contains("DUPLICADO") Then
            Return False
        End If

        Return textoEncabezado.Contains("ORIGINAL")
    End Function

    Private Function NormalizarTextoParaBusqueda(textoOriginal As String) As String
        Dim texto = textoOriginal.ToUpperInvariant()
        texto = Regex.Replace(texto, "(?<=[A-ZÁÉÍÓÚÜÑ])[^A-ZÁÉÍÓÚÜÑ0-9\r\n]+(?=[A-ZÁÉÍÓÚÜÑ])", "")
        texto = Regex.Replace(texto, "(?<=[0-9])[^A-ZÁÉÍÓÚÜÑ0-9\r\n]+(?=[0-9])", "")
        texto = Regex.Replace(texto, "[^A-ZÁÉÍÓÚÜÑ0-9\r\n]+", " ")
        texto = Regex.Replace(texto, "\s+", " ").Trim()
        Return texto
    End Function

    Private Function ObtenerRutaDestinoUnica(carpetaDestino As String, legajo As String, marcaTiempo As String) As String
        Dim carpetaLegajo = Path.Combine(CarpetaBaseRecibos, legajo)
        Directory.CreateDirectory(carpetaLegajo)
        Dim indice = 1

        Do
            Dim nombreArchivo = $"{legajo}_{marcaTiempo}_{indice:000}.pdf"
            Dim rutaDestino = Path.Combine(carpetaLegajo, nombreArchivo)

            If Not File.Exists(rutaDestino) Then
                Return rutaDestino
            End If

            indice += 1
        Loop
    End Function

    Private Function CompararLegajos(legajo1 As String, legajo2 As String) As Integer
        Dim valor1 As Integer
        Dim valor2 As Integer

        If Integer.TryParse(legajo1, valor1) AndAlso Integer.TryParse(legajo2, valor2) Then
            Return valor1.CompareTo(valor2)
        End If

        Return StringComparer.OrdinalIgnoreCase.Compare(legajo1, legajo2)
    End Function

    Private Function CrearProcesoLog(rutaPdfOrigen As String, fechaProceso As DateTime, periodoProcesado As DateTime) As Integer
        Dim sql = "INSERT INTO RecibosProcesos (PeriodoProcesado, FechaInicio, UsuarioProceso, ArchivoOrigen, CarpetaOrigen, EstadoProceso) " &
                  "OUTPUT INSERTED.IdProceso " &
                  "VALUES (@PeriodoProcesado, @FechaInicio, @UsuarioProceso, @ArchivoOrigen, @CarpetaOrigen, @EstadoProceso)"

        Dim parametros = CmdParams(
            "@PeriodoProcesado", New DateTime(periodoProcesado.Year, periodoProcesado.Month, 1),
            "@FechaInicio", fechaProceso,
            "@UsuarioProceso", If(String.IsNullOrWhiteSpace(UsuarioActual), Environment.UserName, UsuarioActual),
            "@ArchivoOrigen", Path.GetFileName(rutaPdfOrigen),
            "@CarpetaOrigen", Path.GetDirectoryName(rutaPdfOrigen),
            "@EstadoProceso", "PROCESANDO")

        Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros, True)
        If tabla Is Nothing OrElse tabla.Rows.Count = 0 Then
            Throw New InvalidOperationException("No se pudo registrar el inicio del proceso en la base de datos.")
        End If

        Return Convert.ToInt32(tabla.Rows(0)(0))
    End Function

    Private Function ObtenerPeriodoProcesadoFecha() As DateTime
        Return New DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, 1)
    End Function

    Private Sub SeleccionarProcesoEnGrilla(idProcesoSeleccionado As Integer?)
        If dgvProcesos.Rows.Count = 0 Then
            dgvListaRecibos.DataSource = Nothing
            Return
        End If

        Dim filaObjetivo As DataGridViewRow = dgvProcesos.Rows(0)

        If idProcesoSeleccionado.HasValue Then
            For Each fila As DataGridViewRow In dgvProcesos.Rows
                If fila.Cells("IdProceso")?.Value IsNot Nothing AndAlso Convert.ToInt32(fila.Cells("IdProceso").Value) = idProcesoSeleccionado.Value Then
                    filaObjetivo = fila
                    Exit For
                End If
            Next
        End If

        filaObjetivo.Selected = True
        dgvProcesos.CurrentCell = filaObjetivo.Cells("FechaProceso")

        If filaObjetivo.Cells("IdProceso")?.Value IsNot Nothing Then
            CargoDetalleProceso(Convert.ToInt32(filaObjetivo.Cells("IdProceso").Value))
        End If
    End Sub

    Private Sub GuardarDetalleProceso(idProceso As Integer, archivo As ArchivoGeneradoInfo, fechaProceso As DateTime)
        Dim sql = "INSERT INTO RecibosProcesosDetalles " &
                  "(IdProceso, Legajo, ArchivoPdf, RutaPdf, CantidadPaginas, PaginasOrigen, FechaGeneracion, EstadoGeneracion, MensajeGeneracion, FechaEnvio, EstadoEnvio, MensajeEnvio, PeriodoProcesado) " &
                  "VALUES (@IdProceso, @Legajo, @ArchivoPdf, @RutaPdf, @CantidadPaginas, @PaginasOrigen, @FechaGeneracion, @EstadoGeneracion, @MensajeGeneracion, NULL, 'PENDIENTE', NULL, @PeriodoProcesado)"

        Dim parametros = CmdParams(
            "@IdProceso", idProceso,
            "@Legajo", archivo.Legajo,
            "@ArchivoPdf", archivo.Archivo,
            "@RutaPdf", archivo.RutaCompleta,
            "@CantidadPaginas", archivo.CantidadPaginas,
            "@PaginasOrigen", archivo.PaginasOrigen,
            "@FechaGeneracion", fechaProceso,
            "@EstadoGeneracion", "GENERADO",
            "@MensajeGeneracion", "Archivo generado correctamente",
            "@PeriodoProcesado", New DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, 1))

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Sub ActualizarProcesoLogFinalizado(idProceso As Integer, resultado As ResultadoProcesoRecibos)
        Dim sql = "UPDATE RecibosProcesos SET " &
                  "FechaFin = @FechaFin, " &
                  "EstadoProceso = @EstadoProceso, " &
                  "CantidadPaginasOrigen = @CantidadPaginasOrigen, " &
                  "CantidadLegajosDetectados = @CantidadLegajosDetectados, " &
                  "CantidadArchivosGenerados = @CantidadArchivosGenerados, " &
                  "CantidadPaginasOmitidas = @CantidadPaginasOmitidas, " &
                  "Observaciones = @Observaciones " &
                  "WHERE IdProceso = @IdProceso"

        Dim observaciones As String = If(resultado.PaginasOmitidas.Count = 0,
                                         "Proceso completado sin páginas omitidas.",
                                         $"Páginas omitidas por no detectar legajo: {String.Join(", ", resultado.PaginasOmitidas)}")

        Dim parametros = CmdParams(
            "@FechaFin", DateTime.Now,
            "@EstadoProceso", "FINALIZADO",
            "@CantidadPaginasOrigen", resultado.CantidadPaginasOrigen,
            "@CantidadLegajosDetectados", resultado.CantidadLegajosDetectados,
            "@CantidadArchivosGenerados", resultado.ArchivosGenerados.Count,
            "@CantidadPaginasOmitidas", resultado.PaginasOmitidas.Count,
            "@Observaciones", observaciones,
            "@IdProceso", idProceso)

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Sub ActualizarProcesoLogError(idProceso As Integer, mensajeError As String)
        Dim sql = "UPDATE RecibosProcesos SET " &
                  "FechaFin = @FechaFin, " &
                  "EstadoProceso = @EstadoProceso, " &
                  "Observaciones = @Observaciones " &
                  "WHERE IdProceso = @IdProceso"

        Dim parametros = CmdParams(
            "@FechaFin", DateTime.Now,
            "@EstadoProceso", "ERROR",
            "@Observaciones", LimitarTexto(mensajeError, 1000),
            "@IdProceso", idProceso)

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Function LimitarTexto(texto As String, longitudMaxima As Integer) As String
        If String.IsNullOrEmpty(texto) Then
            Return String.Empty
        End If

        If texto.Length <= longitudMaxima Then
            Return texto
        End If

        Return texto.Substring(0, longitudMaxima)
    End Function

    Private Sub PrepararGrilla()
        dgvListaRecibos.ReadOnly = False
        dgvListaRecibos.MultiSelect = False
        dgvListaRecibos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvListaRecibos.AutoGenerateColumns = True
        dgvListaRecibos.AllowUserToAddRows = False
        dgvListaRecibos.AllowUserToDeleteRows = False
        Funciones.ConfigurarEstiloGrid(dgvListaRecibos)
    End Sub

    Private Sub dgvListaRecibos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListaRecibos.CellDoubleClick
        If e.RowIndex < 0 Then
            Return
        End If

        Dim fila = dgvListaRecibos.Rows(e.RowIndex)
        Dim valorRuta = fila.Cells("Ruta").Value
        Dim rutaArchivo = If(valorRuta IsNot Nothing, valorRuta.ToString(), String.Empty)

        If String.IsNullOrWhiteSpace(rutaArchivo) OrElse Not File.Exists(rutaArchivo) Then
            MessageBox.Show("No se encontró el archivo PDF seleccionado.", "Archivo no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Process.Start(New ProcessStartInfo(rutaArchivo) With {
                .UseShellExecute = True
            })
        Catch ex As Exception
            MessageBox.Show($"No se pudo abrir el archivo seleccionado.{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ConfigurarColumnasGrilla()
        If dgvListaRecibos.Columns.Count = 0 Then
            Return
        End If

        For Each col As DataGridViewColumn In dgvListaRecibos.Columns
            col.ReadOnly = True
            col.Visible = False
        Next


        If dgvListaRecibos.Columns.Contains(NombreColumnaSeleccion) Then
            dgvListaRecibos.Columns(NombreColumnaSeleccion).HeaderText = ""
            dgvListaRecibos.Columns(NombreColumnaSeleccion).Width = 30
            dgvListaRecibos.Columns(NombreColumnaSeleccion).ReadOnly = False
            dgvListaRecibos.Columns(NombreColumnaSeleccion).DisplayIndex = 0
            dgvListaRecibos.Columns(NombreColumnaSeleccion).Visible = rdbSeleccion.Checked
        End If

        dgvListaRecibos.Columns("Legajo").HeaderText = "Legajo"
        dgvListaRecibos.Columns("Legajo").Width = 50
        dgvListaRecibos.Columns("Legajo").Visible = True

        dgvListaRecibos.Columns("Archivo").HeaderText = "Archivo Generado"
        dgvListaRecibos.Columns("Archivo").Width = 170
        dgvListaRecibos.Columns("Archivo").Visible = True

        dgvListaRecibos.Columns("Paginas").HeaderText = "Páginas"
        dgvListaRecibos.Columns("Paginas").Width = 60
        dgvListaRecibos.Columns("Paginas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvListaRecibos.Columns("Paginas").Visible = True


        dgvListaRecibos.Columns("Ruta").HeaderText = "Ruta"
        dgvListaRecibos.Columns("Ruta").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvListaRecibos.Columns("Ruta").Visible = True
        dgvListaRecibos.Columns("Ruta").Width = 60

        If dgvListaRecibos.Columns.Contains("EstadoEnvio") Then
            dgvListaRecibos.Columns("EstadoEnvio").HeaderText = "Estado"
            dgvListaRecibos.Columns("EstadoEnvio").Width = 90
            dgvListaRecibos.Columns("EstadoEnvio").Visible = True
        End If

        If dgvListaRecibos.Columns.Contains("MensajeEnvio") Then
            dgvListaRecibos.Columns("MensajeEnvio").HeaderText = "Mensaje Envío"
            dgvListaRecibos.Columns("MensajeEnvio").Width = 220
            dgvListaRecibos.Columns("MensajeEnvio").Visible = True
        End If

        If dgvListaRecibos.Columns.Contains("FechaEnvio") Then
            dgvListaRecibos.Columns("FechaEnvio").HeaderText = "Fecha Envío"
            dgvListaRecibos.Columns("FechaEnvio").Width = 120
            dgvListaRecibos.Columns("FechaEnvio").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"
            dgvListaRecibos.Columns("FechaEnvio").Visible = True
        End If
    End Sub

    Private Sub ConfiguraColProcesos()
        If dgvProcesos.Columns.Count = 0 Then
            Return
        End If

        For Each col As DataGridViewColumn In dgvProcesos.Columns
            col.Visible = False
        Next

        If dgvProcesos.Columns.Contains("PeriodoProcesado") Then
            dgvProcesos.Columns("PeriodoProcesado").Visible = True
            dgvProcesos.Columns("PeriodoProcesado").HeaderText = "Período Liquidado"
            dgvProcesos.Columns("PeriodoProcesado").Width = 100
            dgvProcesos.Columns("PeriodoProcesado").DefaultCellStyle.Format = "MM/yyyy"
        End If

        dgvProcesos.Columns("FechaProceso").Visible = True
        dgvProcesos.Columns("FechaProceso").HeaderText = "Fecha de Proceso"
        dgvProcesos.Columns("FechaProceso").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvProcesos.Columns("FechaProceso").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"

    End Sub

    Private Sub dgvProcesos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProcesos.CellClick

        CargoDetalleProceso(If(dgvProcesos.CurrentRow?.Cells("IdProceso")?.Value IsNot Nothing, Convert.ToInt32(dgvProcesos.CurrentRow.Cells("IdProceso").Value), 0))
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub dtpPeriodo_ValueChanged(sender As Object, e As EventArgs) Handles dtpPeriodo.ValueChanged
        CargoProcesos()
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.Expect100Continue = True

        Dim idProceso = ObtenerProcesoSeleccionado()
        If idProceso <= 0 Then
            MessageBox.Show("Seleccione un proceso para enviar los recibos.", "Proceso no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim dtCorreoConfig = ObtenerConfiguracionCorreo()
        If dtCorreoConfig.Rows.Count = 0 Then
            MessageBox.Show("No se encontraron configuraciones de correo para el envío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Cursor.Current = Cursors.WaitCursor
            btnEnviar.Enabled = False

            Dim detalles = ObtenerDetallesProceso(idProceso)
            detalles = FiltrarDetallesSegunModo(detalles)
            If detalles.Rows.Count = 0 Then
                Dim mensajeSinDatos = If(rdbSeleccion.Checked,
                                         "No hay recibos seleccionados para enviar.",
                                         "El proceso seleccionado no tiene detalles para enviar.")
                MessageBox.Show(mensajeSinDatos, "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim filaConfig = dtCorreoConfig.Rows(0)
            Dim servidorSMTP As String = filaConfig("Servidor_SMTP").ToString().Trim()
            Dim puertoSMTP As Integer = 587
            Dim usuario As String = filaConfig("Envio_Mail").ToString().Trim()
            Dim contraseña As String = filaConfig("password_mail").ToString()
            Dim asuntoBase As String = filaConfig("Asunto").ToString().Trim()
            Dim mensajeBase As String = filaConfig("Mensaje").ToString().Trim()
            Dim periodoTexto = ObtenerPeriodoProcesado()

            If String.IsNullOrWhiteSpace(servidorSMTP) OrElse String.IsNullOrWhiteSpace(usuario) Then
                MessageBox.Show("La configuración SMTP está incompleta.", "Configuración inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim enviados As Integer = 0
            Dim sinMail As Integer = 0
            Dim sinArchivo As Integer = 0
            Dim conError As Integer = 0

            Using smtpClient As New SmtpClient(servidorSMTP, puertoSMTP) With {
                .DeliveryMethod = SmtpDeliveryMethod.Network,
                .UseDefaultCredentials = False,
                .EnableSsl = True,
                .Credentials = New NetworkCredential(usuario, contraseña),
                .Timeout = 100000
            }
                For Each filaDetalle As DataRow In detalles.Rows
                    Dim idDetalle = Convert.ToInt32(filaDetalle("IdDetalle"))
                    Dim legajo = filaDetalle("Legajo").ToString().Trim()
                    Dim rutaPdf = filaDetalle("RutaPdf").ToString().Trim()
                    Dim correoDestino = ObtenerCorreoAgente(legajo)

                    If String.IsNullOrWhiteSpace(correoDestino) Then
                        ActualizarResultadoEnvioDetalle(idDetalle, "SIN_MAIL", "El agente no tiene CorreoE informado.")
                        sinMail += 1
                        Continue For
                    End If

                    If String.IsNullOrWhiteSpace(rutaPdf) OrElse Not File.Exists(rutaPdf) Then
                        ActualizarResultadoEnvioDetalle(idDetalle, "ARCHIVO_NO_ENCONTRADO", "No se encontró el archivo PDF a adjuntar.")
                        sinArchivo += 1
                        Continue For
                    End If

                    Dim asunto = ConstruirTextoCorreo(asuntoBase, periodoTexto)
                    Dim mensaje = ConstruirTextoCorreo(mensajeBase, periodoTexto)

                    Try
                        Using mail As New MailMessage()
                            mail.From = New MailAddress(usuario)
                            mail.Subject = asunto
                            mail.Body = mensaje
                            mail.To.Add(correoDestino)
                            mail.Attachments.Add(New Attachment(rutaPdf))

                            smtpClient.Send(mail)
                        End Using

                        ActualizarResultadoEnvioDetalle(idDetalle, "ENVIADO", $"Enviado correctamente a {correoDestino}.")
                        enviados += 1
                    Catch exEnvio As Exception
                        Dim detalleError = If(exEnvio.InnerException?.Message, exEnvio.Message)
                        ActualizarResultadoEnvioDetalle(idDetalle, "ERROR", LimitarTexto(detalleError, 500))
                        conError += 1
                    End Try
                Next
            End Using

            CargoDetalleProceso(idProceso)

            MessageBox.Show(
                $"Proceso de envío finalizado.{Environment.NewLine}" &
                $"Enviados: {enviados}{Environment.NewLine}" &
                $"Sin mail: {sinMail}{Environment.NewLine}" &
                $"Sin archivo: {sinArchivo}{Environment.NewLine}" &
                $"Con error: {conError}",
                "Envío finalizado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

        Catch ex As Exception
            Dim detail = If(ex.InnerException?.Message, ex.Message)
            MessageBox.Show("Error al enviar correos: " & detail, "SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            btnEnviar.Enabled = True
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub AsegurarColumnaSeleccion()
        If dgvListaRecibos.Columns.Contains(NombreColumnaSeleccion) Then
            Return
        End If

        Dim columnaSeleccion As New DataGridViewCheckBoxColumn() With {
            .Name = NombreColumnaSeleccion,
            .HeaderText = "",
            .Width = 30,
            .ReadOnly = False,
            .FalseValue = False,
            .TrueValue = True
        }

        dgvListaRecibos.Columns.Insert(0, columnaSeleccion)
    End Sub

    Private Function FiltrarDetallesSegunModo(detalles As DataTable) As DataTable
        If Not rdbSeleccion.Checked Then
            Return detalles
        End If

        Dim idsSeleccionados = ObtenerIdsDetallesSeleccionados()
        If idsSeleccionados.Count = 0 Then
            Return detalles.Clone()
        End If

        Dim detallesSeleccionados = detalles.Clone()
        For Each fila As DataRow In detalles.Rows
            Dim idDetalle = Convert.ToInt32(fila("IdDetalle"))
            If idsSeleccionados.Contains(idDetalle) Then
                detallesSeleccionados.ImportRow(fila)
            End If
        Next

        Return detallesSeleccionados
    End Function

    Private Function ObtenerIdsDetallesSeleccionados() As HashSet(Of Integer)
        Dim ids As New HashSet(Of Integer)()

        For Each fila As DataGridViewRow In dgvListaRecibos.Rows
            If fila.IsNewRow Then
                Continue For
            End If

            Dim valorSeleccion = fila.Cells(NombreColumnaSeleccion).Value
            Dim seleccionado = False
            If valorSeleccion IsNot Nothing AndAlso valorSeleccion IsNot DBNull.Value Then
                Boolean.TryParse(valorSeleccion.ToString(), seleccionado)
            End If

            If Not seleccionado Then
                Continue For
            End If

            If fila.Cells("IdDetalle")?.Value Is Nothing Then
                Continue For
            End If

            ids.Add(Convert.ToInt32(fila.Cells("IdDetalle").Value))
        Next

        Return ids
    End Function

    Private Function ObtenerProcesoSeleccionado() As Integer
        If dgvProcesos.CurrentRow Is Nothing OrElse dgvProcesos.CurrentRow.Cells("IdProceso")?.Value Is Nothing Then
            Return 0
        End If

        Return Convert.ToInt32(dgvProcesos.CurrentRow.Cells("IdProceso").Value)
    End Function

    Private Sub dgvListaRecibos_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvListaRecibos.CurrentCellDirtyStateChanged
        If dgvListaRecibos.IsCurrentCellDirty AndAlso dgvListaRecibos.CurrentCell IsNot Nothing AndAlso
            dgvListaRecibos.CurrentCell.OwningColumn.Name = NombreColumnaSeleccion Then
            dgvListaRecibos.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub rdbTodos_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTodos.CheckedChanged, rdbSeleccion.CheckedChanged
        If dgvListaRecibos.Columns.Contains(NombreColumnaSeleccion) Then
            dgvListaRecibos.Columns(NombreColumnaSeleccion).Visible = rdbSeleccion.Checked
        End If
    End Sub

    Private Function ObtenerConfiguracionCorreo() As DataTable
        Dim sql = "SELECT TOP 1 Servidor_SMTP, Envio_Mail, password_mail, Asunto, Mensaje " &
                  "FROM EnvioCorreos WHERE NroEnvio = @NroEnvio"

        Dim parametros = CmdParams("@NroEnvio", 5)
        Return DSM.ExecuteQuery(DSM.Stock, sql, parametros)
    End Function

    Private Function ObtenerDetallesProceso(idProceso As Integer) As DataTable
        Dim sql = "SELECT IdDetalle, Legajo, RutaPdf " &
                  "FROM RecibosProcesosDetalles " &
                  "WHERE IdProceso = @IdProceso " &
                  "ORDER BY Legajo"

        Dim parametros = CmdParams("@IdProceso", idProceso)
        Return DSM.ExecuteQuery(DSM.Personal, sql, parametros)
    End Function

    Private Function ObtenerCorreoAgente(legajo As String) As String
        Dim sql = "SELECT TOP 1 CorreoE FROM Agentes WHERE Legajo = @Legajo"
        Dim parametros = CmdParams("@Legajo", legajo)
        Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

        If tabla Is Nothing OrElse tabla.Rows.Count = 0 OrElse IsDBNull(tabla.Rows(0)("CorreoE")) Then
            Return String.Empty
        End If

        Return tabla.Rows(0)("CorreoE").ToString().Trim()
    End Function

    Private Function ObtenerPeriodoProcesado() As String
        Dim periodo = New DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, 1)
        Return periodo.ToString("MMMM yyyy", New CultureInfo("es-AR"))
    End Function

    Private Function ConstruirTextoCorreo(textoBase As String, periodoTexto As String) As String
        Dim texto = If(textoBase, String.Empty).Trim()
        If String.IsNullOrWhiteSpace(texto) Then
            Return periodoTexto
        End If

        If texto.EndsWith(" ") Then
            Return texto & periodoTexto
        End If

        Return texto & " " & periodoTexto
    End Function

    Private Sub ActualizarResultadoEnvioDetalle(idDetalle As Integer, estadoEnvio As String, mensajeEnvio As String)
        Dim sql = "UPDATE RecibosProcesosDetalles SET " &
                  "FechaEnvio = @FechaEnvio, " &
                  "EstadoEnvio = @EstadoEnvio, " &
                  "MensajeEnvio = @MensajeEnvio " &
                  "WHERE IdDetalle = @IdDetalle"

        Dim parametros = CmdParams(
            "@FechaEnvio", DateTime.Now,
            "@EstadoEnvio", estadoEnvio,
            "@MensajeEnvio", LimitarTexto(mensajeEnvio, 500),
            "@IdDetalle", idDetalle)

        DSM.Execute(DSM.Personal, sql, parametros, True)
    End Sub

    Private Sub dgvBonos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
End Class

Public Class ArchivoGeneradoInfo
    Public Property Legajo As String
    Public Property Archivo As String
    Public Property CantidadPaginas As Integer
    Public Property PaginasOrigen As String
    Public Property RutaCompleta As String
End Class

Public Class ResultadoProcesoRecibos
    Public Sub New()
        ArchivosGenerados = New List(Of ArchivoGeneradoInfo)()
        PaginasOmitidas = New List(Of Integer)()
    End Sub

    Public Property ArchivosGenerados As List(Of ArchivoGeneradoInfo)
    Public Property PaginasOmitidas As List(Of Integer)
    Public Property CantidadPaginasOrigen As Integer
    Public Property CantidadLegajosDetectados As Integer
End Class
