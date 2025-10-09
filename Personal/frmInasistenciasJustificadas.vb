Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmInasistenciasJustificadas
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmInasistenciasJustificadas

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmInasistenciasJustificadas()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmInasistenciasJustificadas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub FrmInasistenciasJustificadas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormModoConsulta()
        CargarComboBoxes()

        Me.KeyPreview = True
    End Sub
    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        Try
            filaActual = Nothing
            filaActualIndice = -1
            FormModoEdicion()

            If CmbAgente.SelectedIndex = -1 Then
                CmbAgente.Focus()
            Else
                CmbTipoInasistencia.Focus()
            End If

            DtpFecha.Value = DateTime.Now
            TxtDias.Text = "1"
            TxtComentario.Text = String.Empty
            ChkCorridos.Checked = False
            LblSaldo.Text = "Saldo: -"
        Catch ex As Exception
            MessageBox.Show($"Error al preparar el formulario para agregar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click

        EliminarInasistencia()
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If Not ValidarDatos() Then Return

        Try
            Dim legajo = Convert.ToInt32(CmbAgente.SelectedValue)
            Dim codigoInasistencia = Convert.ToInt32(CmbTipoInasistencia.SelectedValue)
            Dim fecha = DtpFecha.Value.Date
            Dim dias = Convert.ToInt32(TxtDias.Text.Trim)
            Dim comentario = TxtComentario.Text.Trim
            Dim corridos = ChkCorridos.Checked

            ' Obtener información del tipo de inasistencia
            Dim sqlTipoInasistencia = "SELECT Goce, Corrido, Habil FROM Inasistencias WHERE Codigo = @Codigo"
            Dim parametrosTipo = CmdParams("@Codigo", codigoInasistencia)
            Dim tablaTipo = DSM.ExecuteQuery(DSM.Personal, sqlTipoInasistencia, parametrosTipo)

            If tablaTipo.Rows.Count = 0 Then
                MessageBox.Show("Tipo de inasistencia no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim esGoce = Convert.ToBoolean(tablaTipo.Rows(0)("Goce"))
            Dim esCorrido = Convert.ToBoolean(tablaTipo.Rows(0)("Corrido"))
            Dim esHabil = Convert.ToBoolean(tablaTipo.Rows(0)("Habil"))

            ' Calcular días según el tipo
            Dim diasCalculados = CalcularDias(fecha, dias, corridos)

            If filaActual Is Nothing Then
                ' INSERT - Nueva inasistencia
                InsertarNuevaInasistencia(legajo, CmbTipoInasistencia.Text, fecha, diasCalculados, comentario, esGoce)
            End If

            FormModoConsulta()

            MessageBox.Show("Inasistencia guardada correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error al guardar la inasistencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Try
            FormModoConsulta()
        Catch ex As Exception
            MessageBox.Show($"Error al cancelar la operación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Try
            Close()
        Catch ex As Exception
            MessageBox.Show($"Error al cerrar el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmbAgente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgente.SelectedIndexChanged
        ActualizarSaldoVacaciones()
        CargarHistorialInasistencias()
        CargarLicencias()
    End Sub

    Private Sub CmbTipoInasistencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        ActualizarSaldoVacaciones()

        ' Verificar si el tipo de inasistencia requiere días corridos
        If CmbTipoInasistencia.SelectedIndex <> -1 Then
            Try
                Dim codigoInasistencia = Convert.ToInt32(CmbTipoInasistencia.SelectedValue)
                Dim sql = "SELECT Corrido FROM Inasistencias WHERE Codigo = @Codigo"
                Dim parametros = CmdParams("@Codigo", codigoInasistencia)
                Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

                If tabla.Rows.Count > 0 Then
                    ChkCorridos.Checked = Convert.ToBoolean(tabla.Rows(0)("Corrido"))
                End If
            Catch ex As Exception
                ' En caso de error, mantener el estado actual
            End Try
        End If
    End Sub

    Private Sub TxtDias_TextChanged(sender As Object, e As EventArgs)
        ActualizarSaldoVacaciones()
    End Sub

    Private Sub TxtDias_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Solo permitir números
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Limpia los campos de entrada del formulario, preservando la selección del agente.
    ''' </summary>
    Private Sub FormLimpiarSeleccionado()
        ' No limpiar CmbAgente para preservar la selección del usuario
        CmbTipoInasistencia.SelectedIndex = -1
        DtpFecha.Value = DateTime.Now
        TxtDias.Text = "1"
        TxtComentario.Text = String.Empty
        ChkCorridos.Checked = False
        LblSaldo.Text = "Saldo: -"
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para modo consulta.
    ''' </summary>
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdBorrar, CmbAgente)
        SetControlesEnabled(False, CmdAceptar, CmdCancelar, CmbTipoInasistencia, DtpFecha, TxtDias, TxtComentario, ChkCorridos)
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles para modo edición.
    ''' </summary>
    Public Sub FormModoEdicion()
        SetControlesEnabled(True, CmdAceptar, CmdCancelar, CmbTipoInasistencia, DtpFecha, TxtDias, TxtComentario, ChkCorridos)
        SetControlesEnabled(False, CmdAgregar, CmdBorrar, CmbAgente)

        ' En modo edición, CmbAgente solo es editable cuando se agrega un nuevo registro
        CmbAgente.Enabled = (filaActual Is Nothing)
    End Sub

    ''' <summary>
    ''' Carga los ComboBoxes con datos de la base de datos.
    ''' </summary>
    Private Sub CargarComboBoxes()
        Try

            ' Cargar Agentes
            CargarCombos(CmbAgente, "Agentes", "Nombre", "Nombre", "Legajo", "Baja IS NULL OR Baja = ''")

            ' Cargar Motivos de Desvinculacion
            CargarCombos(CmbTipoInasistencia, "Inasistencias", "Descripcion", "Descripcion", "Codigo")

        Catch ex As Exception
            MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Valida los datos antes de guardar.
    ''' </summary>
    Private Function ValidarDatos() As Boolean
        If CmbAgente.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un agente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbAgente.Focus()
            Return False
        End If

        If CmbTipoInasistencia.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un tipo de inasistencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbTipoInasistencia.Focus()
            Return False
        End If

        ' Validar fecha
        If Not ValidarFecha(DtpFecha.Value.Date) Then
            DtpFecha.Focus()
            Return False
        End If

        Dim dias As Integer
        If Not Integer.TryParse(TxtDias.Text.Trim(), dias) OrElse dias <= 0 Then
            MessageBox.Show("Los días deben ser un número entero mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtDias.Focus()
            Return False
        End If

        ' Validar que no exceda un límite razonable
        If dias > 365 Then
            MessageBox.Show("Los días no pueden exceder 365.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtDias.Focus()
            Return False
        End If

        ' Validar saldo de vacaciones si es necesario
        If Not ValidarSaldoVacaciones() Then
            Return False
        End If

        ' Validar que no exista duplicado
        If Not ValidarDuplicado() Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Valida que no exista una inasistencia duplicada para el mismo agente en el rango de fechas.
    ''' </summary>
    Private Function ValidarDuplicado() As Boolean
        Try
            Dim legajo = Convert.ToInt32(CmbAgente.SelectedValue)
            Dim fecha = DtpFecha.Value.Date
            Dim dias = Convert.ToInt32(TxtDias.Text.Trim)
            Dim corridos = ChkCorridos.Checked

            ' Calcular días según el tipo
            Dim diasCalculados = CalcularDias(fecha, dias, corridos)

            ' Validar cada día del rango de inasistencia
            For i As Integer = 0 To diasCalculados - 1
                Dim fechaActual As Date = fecha.AddDays(i)

                Dim sql = "SELECT COUNT(*) FROM Movimiento WHERE Legajo = @Legajo AND Dia = @Fecha"
                Dim parametros = CmdParams("@Legajo", legajo, "@Fecha", fechaActual)

                ' Si estamos editando, excluir los registros actuales
                If filaActual IsNot Nothing Then
                    Dim legajoAnterior = Convert.ToInt32(filaActual.Cells("Legajo").Value)
                    Dim fechaAnterior = Convert.ToDateTime(filaActual.Cells("Dia").Value)
                    Dim diasAnteriores = Convert.ToInt32(filaActual.Cells("CantidadDias").Value)

                    ' Excluir todo el rango de fechas del registro que se está editando
                    sql &= " AND NOT (Legajo = @LegajoAnterior AND Dia >= @FechaAnterior AND Dia < @FechaAnteriorFin)"
                    Dim fechaAnteriorFin = fechaAnterior.AddDays(diasAnteriores)
                    parametros = CmdParams("@Legajo", legajo, "@Fecha", fechaActual,
                                         "@LegajoAnterior", legajoAnterior, "@FechaAnterior", fechaAnterior, "@FechaAnteriorFin", fechaAnteriorFin)
                End If

                Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
                Dim count = Convert.ToInt32(tabla.Rows(0)(0))

                If count > 0 Then
                    MessageBox.Show($"Ya existe un movimiento registrado para este agente en la fecha {fechaActual:dd/MM/yyyy}.",
                                  "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show($"Error al validar duplicados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Valida el saldo de vacaciones solo para vacaciones.
    ''' </summary>
    Private Function ValidarSaldoVacaciones() As Boolean
        If CmbTipoInasistencia.SelectedIndex = -1 OrElse CmbAgente.SelectedIndex = -1 Then
            Return True
        End If

        Try
            Dim motivoDescripcion = CmbTipoInasistencia.Text

            ' Solo validar si es vacaciones
            If motivoDescripcion.ToUpper().Contains("VACACIONES") Then
                Dim legajo = Convert.ToInt32(CmbAgente.SelectedValue)
                Dim dias = Convert.ToInt32(TxtDias.Text.Trim())
                Dim saldoActual = ObtenerSaldoVacaciones(legajo)

                If dias > saldoActual Then
                    MessageBox.Show($"No hay suficiente saldo de vacaciones. Saldo disponible: {saldoActual} días.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show($"Error al validar saldo de vacaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Actualiza la etiqueta de saldo de vacaciones.
    ''' </summary>
    Private Sub ActualizarSaldoVacaciones()
        If CmbAgente.SelectedIndex = -1 OrElse CmbTipoInasistencia.SelectedIndex = -1 Then
            LblSaldo.Text = "Saldo: -"
            LblSaldo.ForeColor = Color.Blue
            Return
        End If

        Try
            Dim motivoDescripcion = CmbTipoInasistencia.Text

            ' Solo mostrar saldo si es vacaciones
            If motivoDescripcion.ToUpper().Contains("VACACIONES") Then
                Dim legajo = Convert.ToInt32(CmbAgente.SelectedValue)
                Dim saldo = ObtenerSaldoVacaciones(legajo)

                ' Obtener días solicitados para validar
                Dim diasSolicitados As Integer = 0
                If Integer.TryParse(TxtDias.Text.Trim(), diasSolicitados) Then
                    If diasSolicitados > saldo Then
                        LblSaldo.Text = $"Saldo: {saldo} días (Insuficiente)"
                        LblSaldo.ForeColor = Color.Red
                    Else
                        LblSaldo.Text = $"Saldo: {saldo} días (Disponible: {saldo - diasSolicitados})"
                        LblSaldo.ForeColor = Color.Green
                    End If
                Else
                    LblSaldo.Text = $"Saldo: {saldo} días"
                    LblSaldo.ForeColor = Color.Blue
                End If
            Else
                LblSaldo.Text = "Saldo: N/A (No es vacaciones)"
                LblSaldo.ForeColor = Color.Gray
            End If

        Catch ex As Exception
            LblSaldo.Text = "Saldo: Error"
            LblSaldo.ForeColor = Color.Red
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el saldo de vacaciones de un agente.
    ''' </summary>
    Private Function ObtenerSaldoVacaciones(legajo As Integer) As Integer
        Try
            Dim anoActual = DateTime.Now.Year
            Dim campoVacaciones = $"vac{anoActual}"

            Dim sql = $"SELECT ISNULL({campoVacaciones}, 0) AS Saldo FROM Agentes WHERE Legajo = @Legajo"
            Dim parametros = CmdParams("@Legajo", legajo)
            Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

            If tabla.Rows.Count > 0 Then
                Return Convert.ToInt32(tabla.Rows(0)("Saldo"))
            End If

            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Valida y ajusta los días de inasistencia según el tipo (corridos vs hábiles).
    ''' Para días corridos: devuelve los días tal como están.
    ''' Para días hábiles: valida que sean días laborables y ajusta si es necesario.
    ''' </summary>
    Private Function CalcularDias(fechaInicio As Date, diasSolicitados As Integer, esDiasCorridos As Boolean) As Integer
        ' Validación básica
        If diasSolicitados <= 0 Then
            Return 0
        End If

        If esDiasCorridos Then
            ' Días corridos - incluye fines de semana y feriados
            Return diasSolicitados
        Else
            ' Días hábiles - solo contar días laborables (lunes a viernes)
            ' Verificar si el período solicitado contiene suficientes días hábiles
            Dim fechaActual = fechaInicio
            Dim diasHabilesContados = 0
            Dim diasTotalesNecesarios = 0

            ' Contar hasta obtener los días hábiles solicitados
            While diasHabilesContados < diasSolicitados
                If fechaActual.DayOfWeek <> DayOfWeek.Saturday AndAlso fechaActual.DayOfWeek <> DayOfWeek.Sunday Then
                    diasHabilesContados += 1
                End If
                diasTotalesNecesarios += 1
                fechaActual = fechaActual.AddDays(1)
            End While

            ' Para días hábiles, devolvemos los días solicitados (no los días totales del período)
            ' porque el campo "días" en la base de datos representa los días de inasistencia efectivos
            Return diasSolicitados
        End If
    End Function

    ''' <summary>
    ''' Valida que la fecha de inasistencia no sea futura ni muy antigua.
    ''' </summary>
    Private Function ValidarFecha(fecha As Date) As Boolean
        Dim fechaActual = DateTime.Now.Date
        Dim fechaLimite = fechaActual.AddMonths(-6) ' No más de 6 meses atrás

        If fecha < fechaLimite Then
            Dim resultado = MessageBox.Show($"La fecha de inasistencia es anterior a {fechaLimite:dd/MM/yyyy}. ¿Desea continuar?",
                                          "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Return resultado = DialogResult.Yes
        End If

        Return True
    End Function

    ''' <summary>
    ''' Inserta una nueva inasistencia en la base de datos.
    ''' </summary>
    Private Sub InsertarNuevaInasistencia(legajo As Integer, codigoInasistencia As String, fecha As Date, dias As Integer, comentario As String, esGoce As Boolean)
        Try
            ' Insertar en tabla Movimiento - un registro por cada día de inasistencia
            Dim horaEntrada As DateTime = fecha.Date.AddHours(8) ' 08:00:00
            Dim horaSalida As DateTime = fecha.Date.AddHours(8)   ' 08:00:00
            Dim horasCumplidas As TimeSpan = TimeSpan.Zero       ' 0 horas trabajadas

            Dim sqlMovimiento = "INSERT INTO Movimiento (Legajo, Instituto, Dia, Entro, Salio, HsCumplidas, MotivoInasistencia, Comentario, Nopromedianada, SinFicha, NoPromedia) VALUES (@Legajo, @Instituto, @Dia, @Entro, @Salio, @HsCumplidas, @MotivoInasistencia, @Comentario, 0, 0, 0)"

            ' Crear un registro por cada día de inasistencia
            For i As Integer = 0 To dias - 1
                Dim fechaActual As Date = fecha.AddDays(i)
                Dim horaEntradaActual As DateTime = fechaActual.Date.AddHours(8) ' 08:00:00
                Dim horaSalidaActual As DateTime = fechaActual.Date.AddHours(8)   ' 08:00:00

                Dim parametrosMovimiento = CmdParams("@Legajo", legajo, "@Instituto", DBNull.Value, "@Dia", fechaActual, "@Entro", horaEntradaActual, "@Salio", horaSalidaActual, "@HsCumplidas", horasCumplidas, "@MotivoInasistencia", codigoInasistencia, "@Comentario", comentario)
                DSM.Execute(DSM.Personal, sqlMovimiento, parametrosMovimiento, True)
            Next

            ' Insertar en tabla Expediente
            Dim motivoDescripcion = CmbTipoInasistencia.Text
            Dim nombreAgente = CmbAgente.Text
            Dim sqlExpediente = "INSERT INTO Expediente (Legajo, Dia, Motivo, CantidadDias, Nombre, Listar, Autorizo, Cargo) VALUES (@Legajo, @Dia, @Motivo, @CantidadDias, @Nombre, 0, @Autorizo, @Cargo);"
            Dim parametrosExpediente = CmdParams("@Legajo", legajo, "@Dia", fecha, "@Motivo", motivoDescripcion, "@CantidadDias", dias, "@Nombre", nombreAgente, "@Autorizo", UsuarioActual, "@Cargo", "Recursos Humanos")
            DSM.Execute(DSM.Personal, sqlExpediente, parametrosExpediente, True)

            'Obtengo el ultimo id insertado en la tabla expediente para imprimir reporte
            Dim sqlUltimoId = "SELECT TOP 1 Id FROM Expediente WHERE Legajo = @Legajo AND Dia = @Dia ORDER BY Id DESC"
            Dim parametrosUltimoId = CmdParams("@Legajo", legajo, "@Dia", fecha)
            Dim tablaUltimoId = DSM.ExecuteQuery(DSM.Personal, sqlUltimoId, parametrosUltimoId)
            Dim idExpte As Integer = 0
            If tablaUltimoId IsNot Nothing AndAlso tablaUltimoId.Rows.Count > 0 Then
                idExpte = Convert.ToInt32(tablaUltimoId.Rows(0)("Id"))
            End If

            ' Insertar comentario si existe
            Dim sqlComentario = "INSERT INTO Comentarios (Legajo, Fecha, Comenta, Motivo) VALUES (@Legajo, @Fecha, @Comentario, @Motivo)"
            Dim parametrosComentario = CmdParams("@Legajo", legajo, "@Fecha", fecha, "@Comentario", comentario, "@Motivo", motivoDescripcion)
            DSM.Execute(DSM.Personal, sqlComentario, parametrosComentario, True)

            ' Si es vacaciones, descontar del saldo de vacaciones
            If motivoDescripcion.ToUpper().Contains("VACACIONES") Then
                Dim anoActual = DateTime.Now.Year
                Dim campoVacaciones = $"vac{anoActual}"

                Dim sqlActualizar = $"UPDATE Agentes SET {campoVacaciones} = ISNULL({campoVacaciones}, 0) - @Dias WHERE Legajo = @Legajo"
                Dim parametrosActualizar = CmdParams("@Dias", dias, "@Legajo", legajo)
                DSM.Execute(DSM.Personal, sqlActualizar, parametrosActualizar, True)
            End If

            ImprimirFicha(idExpte)
            FormModoConsulta()
            CargarHistorialInasistencias() ' Actualizar el grid después de insertar
        Catch ex As Exception
            MessageBox.Show($"Error al insertar la inasistencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Elimina una inasistencia de la base de datos.
    ''' </summary>
    Private Sub EliminarInasistencia()
        Try
            If filaActual Is Nothing OrElse DgvListado.Rows.Count = 0 Then
                MessageBox.Show("Debe seleccionar una fila válida")
                Return
            End If

            ' Confirmar eliminación
            Dim resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta inasistencia?",
                                          "Confirmar eliminación",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question)

            If resultado = DialogResult.No Then Return

            Dim legajo = Convert.ToInt32(filaActual.Cells("Legajo").Value)
            Dim fecha = Convert.ToDateTime(filaActual.Cells("Dia").Value)
            Dim motivoInasistencia = filaActual.Cells("Motivo").Value.ToString
            Dim dias = Convert.ToInt32(filaActual.Cells("CantidadDias").Value)

            ' Verificar si el tipo es vacaciones para restaurar saldo
            Dim esVacaciones = motivoInasistencia.ToUpper().Contains("VACACIONES")

            ' Eliminar de tabla Movimiento - todos los registros de los días de inasistencia
            For i As Integer = 0 To dias - 1
                Dim fechaActual As Date = fecha.AddDays(i)
                Dim sqlMovimiento = "DELETE FROM Movimiento WHERE Legajo = @Legajo AND Dia = @Fecha "
                Dim parametrosMovimiento = CmdParams("@Legajo", legajo, "@Fecha", fechaActual)
                DSM.Execute(DSM.Personal, sqlMovimiento, parametrosMovimiento, True)
            Next

            ' Eliminar de tabla Expediente
            Dim sqlExpediente = "DELETE FROM Expediente WHERE Legajo = @Legajo AND Dia = @Fecha "
            Dim parametrosExpediente = CmdParams("@Legajo", legajo, "@Fecha", fecha)
            DSM.Execute(DSM.Personal, sqlExpediente, parametrosExpediente, True)

            ' Eliminar de tabla comentarios
            Dim sqlComentario = "DELETE FROM Comentarios WHERE Legajo = @Legajo AND Fecha = @Fecha And Motivo = @Motivo"
            Dim parametrosComentario = CmdParams("@Legajo", legajo, "@Fecha", fecha, "@Motivo", motivoInasistencia)
            DSM.Execute(DSM.Personal, sqlComentario, parametrosComentario, True)


            ' Restaurar saldo de vacaciones si es necesario
            If esVacaciones Then
                Dim anoActual = DateTime.Now.Year
                Dim campoVacaciones = $"vac{anoActual}"

                Dim sqlRestaurar = $"UPDATE Agentes SET {campoVacaciones} = ISNULL({campoVacaciones}, 0) + @Dias WHERE Legajo = @Legajo"
                Dim parametrosRestaurar = CmdParams("@Dias", dias, "@Legajo", legajo)
                DSM.Execute(DSM.Personal, sqlRestaurar, parametrosRestaurar, True)
            End If

            MessageBox.Show($"Inasistencia eliminada correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error al eliminar la inasistencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try

        FormModoConsulta()
        CargarHistorialInasistencias() ' Actualizar el grid después de eliminar

    End Sub

    Private Sub ImprimirFicha(Id As Integer)
        If Id > 0 Then
            ' Actualizar datos adicionales del expediente si es necesario
            Dim sqlUpdate As String = "UPDATE Expediente " &
            "SET Expediente.Articulo = Inasistencias.Articulo, " &
            "Expediente.Inciso = Inasistencias.Inciso, " &
            "Expediente.Decreto = Inasistencias.Decreto, " &
            "Expediente.Secretaria = Agentes.Secretaria, " &
            "Expediente.Insti = Agentes.Instituto, " &
            "Expediente.ListaR = 1 " &
            "FROM Expediente " &
            "INNER JOIN Inasistencias ON Expediente.Motivo = Inasistencias.Descripcion " &
            "INNER JOIN Agentes ON Expediente.Legajo = Agentes.Legajo " &
            "WHERE Expediente.ID = @IdExpte"

            Dim parametrosUpdate As Dictionary(Of String, Object) = CmdParams("@IdExpte", Id)
            DSM.Execute(DSM.Personal, sqlUpdate, parametrosUpdate, True)

            ' Imprimir el reporte
            Process.Start(General.ReportesPath, "Personal ficha RecordSelectionFormula {Expediente.Id}=" & Id)

        End If

    End Sub

    Private Sub ConfigurarColLicencia()
        Try
            If dgvLicencia.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In dgvLicencia.Columns
                    col.Visible = False
                Next

                ' Configurar columnas del grid de historial de inasistencias

                dgvLicencia.Columns("Motivo").Visible = True
                dgvLicencia.Columns("Motivo").HeaderText = "Motivo"
                dgvLicencia.Columns("Motivo").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                dgvLicencia.Columns("diasDisponibles").Visible = True
                dgvLicencia.Columns("diasDisponibles").HeaderText = "Disp"
                dgvLicencia.Columns("diasDisponibles").Width = 60
                dgvLicencia.Columns("diasDisponibles").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                dgvLicencia.Columns("diasRestantes").Visible = True
                dgvLicencia.Columns("diasRestantes").HeaderText = "Resto"
                dgvLicencia.Columns("diasRestantes").Width = 60
                dgvLicencia.Columns("diasRestantes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                ' Aplicar estilo común
                ConfigurarEstiloGrid(dgvLicencia)
            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try

    End Sub

    ''' <summary>
    ''' Configura las columnas del DataGridView para mostrar el historial de inasistencias
    ''' </summary>
    Private Sub ConfiguraColListado()
        Try
            If DgvListado.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In DgvListado.Columns
                    col.Visible = False
                Next

                ' Configurar columnas del grid de historial de inasistencias
                DgvListado.Columns("Id").Visible = False
                DgvListado.Columns("Id").HeaderText = "Id"

                DgvListado.Columns("Legajo").Visible = True
                DgvListado.Columns("Legajo").HeaderText = "Legajo"
                DgvListado.Columns("Legajo").Width = 80

                DgvListado.Columns("Dia").Visible = True
                DgvListado.Columns("Dia").HeaderText = "Fecha"
                DgvListado.Columns("Dia").Width = 100
                DgvListado.Columns("Dia").DefaultCellStyle.Format = "dd/MM/yyyy"

                DgvListado.Columns("Motivo").Visible = True
                DgvListado.Columns("Motivo").HeaderText = "Motivo"
                DgvListado.Columns("Motivo").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                DgvListado.Columns("CantidadDias").Visible = True
                DgvListado.Columns("CantidadDias").HeaderText = "Días"
                DgvListado.Columns("CantidadDias").Width = 60
                DgvListado.Columns("CantidadDias").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                ' Aplicar estilo común
                ConfigurarEstiloGrid(DgvListado)
            End If
        Catch ex As Exception
            ' Ignorar errores de configuración de columnas
        End Try
    End Sub

    Private Sub CargarLicencias()
        Try
            If CmbAgente.SelectedValue Is Nothing OrElse TypeOf CmbAgente.SelectedValue Is DataRowView Then
                Return
            End If
            Dim legajo As Integer = Convert.ToInt32(CmbAgente.SelectedValue)

            Vacaciones.ActualizarVacacionesPorAgente(legajo)


            ' Consulta para obtener las licencias del agente
            Dim sql = "SELECT * FROM Licencia WHERE Legajo = @Legajo ORDER BY Motivo DESC"
            Dim parametros = CmdParams("@Legajo", legajo)
            Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
            dgvLicencia.DataSource = tabla
            ' Configurar columnas después de cargar los datos
            ConfigurarColLicencia()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar las licencias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el historial de inasistencias del agente seleccionado
    ''' </summary>
    Private Sub CargarHistorialInasistencias()
        Try

            If CmbAgente.SelectedValue Is Nothing OrElse TypeOf CmbAgente.SelectedValue Is DataRowView Then
                Return
            End If

            Dim legajo As Integer = Convert.ToInt32(CmbAgente.SelectedValue)


            ' Consulta para obtener el historial de inasistencias del agente
            Dim sql = "SELECT * FROM Expediente  " &
                     "WHERE Legajo = @Legajo " &
                     "ORDER BY Dia DESC"

            Dim parametros = CmdParams("@Legajo", legajo)
            Dim tabla = DSM.ExecuteQuery(DSM.Personal, sql, parametros)

            DgvListado.DataSource = tabla

            ' Configurar columnas después de cargar los datos
            ConfiguraColListado()

        Catch ex As Exception
            MessageBox.Show($"Error al cargar el historial de inasistencias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
    Private Sub AplicarSeleccionActual()
        If DgvListado.CurrentRow Is Nothing Then
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

    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

End Class