Imports Microsoft.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmIngresoHorario
    Private _suspenderAccionFiltros As Boolean = False
    Private tablaAgentes As New DataTable()
    Private agenteSeleccionado As DataRow = Nothing
    Private Shared instancia As frmIngresoHorario

    ' Patrón Singleton para manejo de instancia única
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmIngresoHorario()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmIngresoHorario_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmIngresoHorario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _suspenderAccionFiltros = True
        CargarAgentes()
        _suspenderAccionFiltros = False
        Me.KeyPreview = True
    End Sub

    Private Sub CargarAgentes()
        CargarCombos(cmbAgentes, "Agentes", "Nombre", "Nombre", "Legajo", "Baja IS NULL OR Baja = ''")
    End Sub

    Private Sub cmbAgentes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAgentes.SelectedIndexChanged
        If _suspenderAccionFiltros OrElse cmbAgentes.SelectedIndex = -1 Then Exit Sub

        agenteSeleccionado = CType(cmbAgentes.SelectedItem, DataRowView).Row
        txtDocumento.Text = agenteSeleccionado("NroDto").ToString()
        MostrarInformacionAgente()
    End Sub

    Private Sub MostrarInformacionAgente()
        If agenteSeleccionado Is Nothing Then Return

        Try

            If txtHora IsNot Nothing Then
                txtHora.Text = DateTime.Now.ToString("HH:mm:ss")
            End If

            ' Limpiar campos de movimiento
            LimpiarCamposMovimiento()

            ' Cargar movimiento del día actual si existe
            CargarMovimientoDiaActual()

        Catch ex As Exception
            MessageBox.Show("Error al mostrar información del agente: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LimpiarCamposMovimiento()
        If txtEntro IsNot Nothing Then txtEntro.Text = ""
        If txtSalio IsNot Nothing Then txtSalio.Text = ""
        If txtHsCumplidas IsNot Nothing Then txtHsCumplidas.Text = ""
    End Sub

    Private Sub CargarMovimientoDiaActual()
        If agenteSeleccionado Is Nothing Then Return

        Try
            Dim legajo As Integer = Convert.ToInt32(agenteSeleccionado("Legajo"))
            Dim fechaHoy = DtpDia.Value.Date

            Dim sql As String = "SELECT * FROM Movimiento WHERE Legajo = @Legajo AND CAST(dia AS DATE) = @Fecha ORDER BY entro DESC"
            Dim parametros = CmdParams(
                "@Legajo", legajo,
                "@Fecha", fechaHoy
            )

            Dim dtMovimiento As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros, True)

            If dtMovimiento IsNot Nothing AndAlso dtMovimiento.Rows.Count > 0 Then
                Dim movimiento As DataRow = dtMovimiento.Rows(0)

                If txtEntro IsNot Nothing AndAlso Not IsDBNull(movimiento("entro")) Then
                    txtEntro.Text = Convert.ToDateTime(movimiento("entro")).ToString("HH:mm:ss")
                End If

                If txtSalio IsNot Nothing AndAlso Not IsDBNull(movimiento("salio")) Then
                    txtSalio.Text = Convert.ToDateTime(movimiento("salio")).ToString("HH:mm:ss")
                End If

                If txtHsCumplidas IsNot Nothing AndAlso Not IsDBNull(movimiento("hscumplidas")) Then
                    txtHsCumplidas.Text = movimiento("hscumplidas").ToString()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error al cargar movimiento del día: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidarDocumento() As Boolean
        Try
            Dim documentoIngresado As String = InputBox("Ingrese Número de Documento", "Validación de Identidad", "")

            If String.IsNullOrEmpty(documentoIngresado) Then
                Return False
            End If

            Dim documentoAgente As String = agenteSeleccionado("NroDto").ToString()

            If documentoIngresado.Trim() <> documentoAgente.Trim() Then
                MessageBox.Show("Documento erróneo.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Error en validación de documento: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub ProcesarMarcaHorario()
        Try
            ' Validar que se haya ingresado una hora válida
            If String.IsNullOrWhiteSpace(txtHora.Text) Then
                MessageBox.Show("Por favor, ingrese una hora válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim legajo As Integer = Convert.ToInt32(agenteSeleccionado("Legajo"))
            Dim fechaSeleccionada As Date = DtpDia.Value.Date

            ' Parsear la hora del control txtHora
            Dim horaIngresada As TimeSpan
            If Not TimeSpan.TryParse(txtHora.Text, horaIngresada) Then
                MessageBox.Show("La hora ingresada no tiene un formato válido. Use el formato HH:mm:ss", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Buscar movimiento del día seleccionado sin horas cumplidas (entrada sin salida)
            Dim sql As String = "SELECT * FROM Movimiento WHERE Legajo = @Legajo AND CAST(dia AS DATE) = @Fecha AND (hscumplidas = '00:00:00' OR hscumplidas IS NULL) ORDER BY entro DESC"
            Dim parametros = CmdParams(
                "@Legajo", legajo,
                "@Fecha", fechaSeleccionada
            )

            Dim dtMovimiento As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros, True)

            If dtMovimiento IsNot Nothing AndAlso dtMovimiento.Rows.Count > 0 Then
                ' Ya existe entrada, registrar SALIDA
                RegistrarSalida(dtMovimiento.Rows(0), horaIngresada)
            Else
                ' No existe entrada, registrar ENTRADA
                RegistrarEntrada(legajo, fechaSeleccionada, horaIngresada)
            End If

            ' Actualizar la visualización
            CargarMovimientoDiaActual()
            MessageBox.Show("Su registro se realizó con éxito. Gracias", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error al procesar marca de horario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RegistrarEntrada(legajo As Integer, fecha As Date, hora As TimeSpan)
        Try
            ' Crear DateTime completo para la hora de entrada
            Dim fechaHoraEntrada As DateTime = fecha.Date.Add(hora)

            Dim sql As String = "INSERT INTO Movimiento (Legajo, dia, entro, salio, hscumplidas) VALUES (@Legajo, @Fecha, @Hora, NULL, '00:00:00')"
            Dim parametros = CmdParams(
                "@Legajo", legajo,
                "@Fecha", fechaHoraEntrada,
                "@Hora", fechaHoraEntrada
            )

            DSM.Execute(DSM.Personal, sql, parametros, True)

        Catch ex As Exception
            Throw New Exception("Error al registrar entrada: " & ex.Message)
        End Try
    End Sub

    Private Sub RegistrarSalida(movimiento As DataRow, horaSalida As TimeSpan)
        Try
            ' Usar la fecha seleccionada en el control DtpDia
            Dim fechaSeleccionada As DateTime = DtpDia.Value.Date
            Dim horaEntradaOriginal As DateTime = Convert.ToDateTime(movimiento("entro"))
            Dim horaEntrada As TimeSpan = horaEntradaOriginal.TimeOfDay
            Dim horasTrabajadas As TimeSpan = horaSalida.Subtract(horaEntrada)

            ' Asegurar que las horas trabajadas no sean negativas
            If horasTrabajadas.TotalSeconds < 0 Then
                horasTrabajadas = TimeSpan.FromDays(1).Add(horasTrabajadas) ' Manejo de cambio de día
            End If

            ' Crear DateTime completo para la hora de salida usando la fecha seleccionada
            Dim fechaHoraSalida As DateTime = fechaSeleccionada.Add(horaSalida)

            Dim sql As String = "UPDATE Movimiento SET salio = @HoraSalida, hscumplidas = @HorasTrabajadas WHERE Legajo = @Legajo AND CAST(dia AS DATE) = @Fecha AND entro = @HoraEntrada"
            Dim parametros = CmdParams(
                "@HoraSalida", fechaHoraSalida,
                "@HorasTrabajadas", horasTrabajadas.ToString("hh\:mm\:ss"),
                "@Legajo", Convert.ToInt32(movimiento("Legajo")),
                "@Fecha", fechaSeleccionada,
                "@HoraEntrada", horaEntradaOriginal
            )

            DSM.Execute(DSM.Personal, sql, parametros, True)

        Catch ex As Exception
            Throw New Exception("Error al registrar salida: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        LimpiarFormulario()
        Close()
    End Sub

    Private Sub LimpiarFormulario()
        If cmbAgentes IsNot Nothing Then cmbAgentes.SelectedIndex = -1
        agenteSeleccionado = Nothing
        LimpiarCamposMovimiento()
        If txtDocumento IsNot Nothing Then txtDocumento.Text = ""
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        Try
            If agenteSeleccionado Is Nothing Then
                MessageBox.Show("Debe seleccionar un agente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If Not ValidarDocumento() Then Return

            CrearMarcacion()

            ' Actualizar la visualización
            CargarMovimientoDiaActual()
            MessageBox.Show("Su registro se realizó con éxito. Gracias", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error al procesar marca de horario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CrearMarcacion()
        If agenteSeleccionado Is Nothing Then
            MessageBox.Show("Debe seleccionar un agente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fechaSeleccionada As Date = DtpDia.Value.Date
        Dim horaIngresada As String = txtHora.Text
        Dim marcacionHora As DateTime = String.Format("{0} {1}", fechaSeleccionada.ToString("yyyy-MM-dd"), horaIngresada)
        Dim sql = "
            INSERT INTO dbo.Marcaciones (dispositivo, puerto, legajo, fechahora)
            VALUES(@dispositivo, @puerto, @legajo, @fechahora)"
        Dim parametros = CmdParams("@dispositivo", "0.0.0.0", "@puerto", 0, "@legajo", agenteSeleccionado("Legajo"), "@fechahora", marcacionHora)
        DSM.Execute(DSM.Personal, sql, parametros, True)

        Try : Relojes.ProcesarMarcaciones() : Catch : End Try
    End Sub

    Private Sub DtpDia_ValueChanged(sender As Object, e As EventArgs) Handles DtpDia.ValueChanged
        CargarMovimientoDiaActual()
    End Sub
End Class