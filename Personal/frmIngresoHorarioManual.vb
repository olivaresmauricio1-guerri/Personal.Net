Imports Microsoft.Data.SqlClient
Imports Microsoft.Identity.Client
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmIngresoHorarioManual
    Private _suspenderAccionFiltros As Boolean = False
    Private tablaAgentes As New DataTable()
    Private agenteSeleccionado As DataRow = Nothing
    Private Shared instancia As frmIngresoHorarioManual
    Private _guardandoRegistro As Boolean = False
    ' Patrón Singleton para manejo de instancia única
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmIngresoHorarioManual()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmIngresoHorario_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub frmIngresoHorarioManual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _suspenderAccionFiltros = True
        CargarAgentes()
        _suspenderAccionFiltros = False
        Me.KeyPreview = True

        DtpDia.Value = DateTime.Today
        ' Configurar controles de tiempo con valores por defecto
        dtpHoraEntrada.Format = DateTimePickerFormat.Time
        dtpHoraSalida.Format = DateTimePickerFormat.Time
        dtpHoraEntrada.ShowUpDown = True
        dtpHoraSalida.ShowUpDown = True

        ' Valores por defecto
        dtpHoraEntrada.Value = DateTime.Today.AddHours(8) ' 08:00
        dtpHoraSalida.Value = DateTime.Today.AddHours(17).AddMinutes(30) ' 17:30

        CalcularHorasTrabajadas()
    End Sub

    Private Sub CargarAgentes()
        CargarCombos(cmbAgentes, "Agentes", "Nombre", "Nombre", "Legajo", "Baja IS NULL OR Baja = ''")
    End Sub

    Private Sub cmbAgentes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAgentes.SelectedIndexChanged
        CargarDatosExistentes()
    End Sub


    Private Sub dtpHoraEntrada_ValueChanged(sender As Object, e As EventArgs) Handles dtpHoraEntrada.ValueChanged
        CalcularHorasTrabajadas()
    End Sub

    Private Sub dtpHoraSalida_ValueChanged(sender As Object, e As EventArgs) Handles dtpHoraSalida.ValueChanged
        CalcularHorasTrabajadas()
    End Sub

    Private Sub CalcularHorasTrabajadas()
        Try
            ' Obtener las horas directamente como DateTime y trabajar con ellas
            Dim fechaBase As Date = Date.Today
            Dim entradaDateTime As DateTime = fechaBase.Add(dtpHoraEntrada.Value.TimeOfDay)
            Dim salidaDateTime As DateTime = fechaBase.Add(dtpHoraSalida.Value.TimeOfDay)

            ' Si la salida es menor que la entrada, es trabajo nocturno (día siguiente)
            If salidaDateTime <= entradaDateTime Then
                salidaDateTime = salidaDateTime.AddDays(1)
            End If

            ' Calcular la diferencia
            Dim horasTrabajadas As TimeSpan = salidaDateTime - entradaDateTime

            ' Formatear el resultado
            Dim horas As Integer = CInt(Math.Floor(horasTrabajadas.TotalHours))
            Dim minutos As Integer = horasTrabajadas.Minutes

            txtHorasTrabajadas.Text = String.Format("{0:D2}:{1:D2}", horas, minutos)
        Catch ex As Exception
            txtHorasTrabajadas.Text = "00:00"
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If _guardandoRegistro Then Return ' Evitar guardado múltiple

        If ValidarDatos() Then
            _guardandoRegistro = True
            Try
                GuardarMovimiento()
            Finally
                _guardandoRegistro = False
            End Try
        End If
    End Sub

    Private Function ValidarDatos() As Boolean
        If cmbAgentes.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un agente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbAgentes.Focus()
            Return False
        End If

        If dtpHoraEntrada.Value.TimeOfDay >= dtpHoraSalida.Value.TimeOfDay Then
            MessageBox.Show("La hora de entrada debe ser menor que la hora de salida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpHoraEntrada.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub GuardarMovimiento()
        Try
            Dim agenteSeleccionado As DataRow = CType(cmbAgentes.SelectedItem, DataRowView).Row
            Dim legajo As String = agenteSeleccionado("Legajo").ToString()
            Dim fecha As Date = DtpDia.Value.Date
            Dim entrada As TimeSpan = dtpHoraEntrada.Value.TimeOfDay
            Dim salida As TimeSpan = dtpHoraSalida.Value.TimeOfDay
            Dim comentario As String = txtComentario.Text.Trim()

            ' Calcular horas cumplidas (usando la misma lógica que CalcularHorasTrabajadas)
            Dim fechaBase As Date = Date.Today
            Dim entradaDateTime As DateTime = fechaBase.Add(entrada)
            Dim salidaDateTime As DateTime = fechaBase.Add(salida)

            ' Si la salida es menor que la entrada, es trabajo nocturno (día siguiente)
            If salidaDateTime <= entradaDateTime Then
                salidaDateTime = salidaDateTime.AddDays(1)
            End If

            Dim horasCumplidas As TimeSpan = salidaDateTime - entradaDateTime

            ' Verificar si ya existe un registro para este agente en esta fecha
            Dim sqlVerificar As String = "SELECT entro, salio FROM Movimiento WHERE Legajo = @Legajo AND dia = @Dia"
            Dim dtExistente = DSM.ExecuteQuery(DSM.Personal, sqlVerificar, CmdParams("@Legajo", legajo, "@Dia", fecha))

            Dim existeRegistro As Boolean = dtExistente IsNot Nothing AndAlso dtExistente.Rows.Count > 0

            If existeRegistro Then
                ' Actualizar registro existente
                Dim sqlUpdate As String = "UPDATE Movimiento SET entro = @Entrada, salio = @Salida, hscumplidas = @HorasCumplidas, comentario = @Comentario WHERE Legajo = @Legajo AND dia = @Dia"
                DSM.Execute(DSM.Personal, sqlUpdate, CmdParams("@Entrada", entrada, "@Salida", salida, "@HorasCumplidas", horasCumplidas, "@Comentario", comentario, "@Legajo", legajo, "@Dia", fecha))
                MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' Insertar nuevo registro
                Dim sqlInsert As String = "INSERT INTO Movimiento (Legajo, dia, entro, salio, hscumplidas, comentario, NoPromedia, Nopromedianada, SinFicha) 
                                            VALUES (@Legajo, @Dia, @Entrada, @Salida, @HorasCumplidas, @Comentario,0,0,0)"
                DSM.Execute(DSM.Personal, sqlInsert, CmdParams("@Legajo", legajo, "@Dia", fecha, "@Entrada", entrada, "@Salida", salida, "@HorasCumplidas", horasCumplidas, "@Comentario", comentario))
                MessageBox.Show("Registro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Limpiar el formulario después de guardar
            LimpiarFormulario()

        Catch ex As Exception
            MessageBox.Show($"Error al guardar el movimiento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LimpiarFormulario()
        cmbAgentes.SelectedIndex = -1
        DtpDia.Value = DateTime.Now
        dtpHoraEntrada.Value = DateTime.Today.AddHours(8) ' 08:00
        dtpHoraSalida.Value = DateTime.Today.AddHours(17) ' 17:00
        txtComentario.Clear()
        txtHorasTrabajadas.Clear()
    End Sub

    Private Sub CargarDatosExistentes()
        If cmbAgentes.SelectedIndex = -1 Then Return

        Try
            Dim agenteSeleccionado As DataRow = CType(cmbAgentes.SelectedItem, DataRowView).Row
            Dim legajo As String = agenteSeleccionado("Legajo").ToString()
            Dim fecha As Date = DtpDia.Value.Date

            Dim sql As String = "SELECT entro, salio, comentario FROM Movimiento WHERE Legajo = @Legajo AND dia = @Dia"
            Dim dt = DSM.ExecuteQuery(DSM.Personal, sql, CmdParams("@Legajo", legajo, "@Dia", fecha))

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row = dt.Rows(0)
                ' Cargar datos existentes
                If Not IsDBNull(row("entro")) Then
                    Dim entradaExistente As TimeSpan = CType(row("entro"), TimeSpan)
                    dtpHoraEntrada.Value = DateTime.Today.Add(entradaExistente)
                End If

                If Not IsDBNull(row("salio")) Then
                    Dim salidaExistente As TimeSpan = CType(row("salio"), TimeSpan)
                    dtpHoraSalida.Value = DateTime.Today.Add(salidaExistente)
                End If

                If Not IsDBNull(row("comentario")) Then
                    txtComentario.Text = row("comentario").ToString()
                End If
            Else
                ' No hay datos existentes, usar valores por defecto
                dtpHoraEntrada.Value = DateTime.Today.AddHours(8)
                dtpHoraSalida.Value = DateTime.Today.AddHours(17).AddMinutes(30)
                txtComentario.Clear()
            End If

        Catch ex As Exception
            ' No mostrar error si no se encuentran datos
        End Try
    End Sub

    Private Sub DtpDia_ValueChanged(sender As Object, e As EventArgs) Handles DtpDia.ValueChanged
        CargarDatosExistentes()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimpiarFormulario()
    End Sub
End Class