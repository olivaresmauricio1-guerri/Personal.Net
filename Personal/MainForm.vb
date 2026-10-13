Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class MainForm

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Personal V.{My.Application.Info.Version}"
        Me.WindowState = FormWindowState.Maximized

        Try
            Dim sucursales = DSM.ExecuteQuery(
               DSM.Stock,
                "SELECT Descripcion FROM Sucursales WHERE idSucursal = @Sucursal",
                CmdParams("@Sucursal", SucursalActual))

            If sucursales.Rows.Count > 0 Then
                General.DescripcionSucursal = sucursales.Rows(0)("Descripcion").ToString()
            End If

            Dim empresas = DSM.ExecuteQuery(
               DSM.Stock,
                "SELECT Descripcion FROM Empresas WHERE Codigo = @Empresa",
                CmdParams("@Empresa", 1))

            If empresas.Rows.Count > 0 Then
                General.EmpresaActual = empresas.Rows(0)("Descripcion").ToString()
            End If

            Dim bonoSQl = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM Autoriza WHERE Usuario= @Usuario", CmdParams("@Usuario", UsuarioActual))
            If bonoSQl.Rows(0)("VeBonos").ToString() = True Then
                VeBonos = True
            Else
                VeBonos = False
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al cargar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Actualizar los paneles con la información actual
        Panel1.Text = $"Sistema de Personal - Sucursal: {DescripcionSucursal}"
        Panel4.Text = UsuarioActual & " | " & Mid(General.Entorno, 1, 3).ToUpper()
        Panel2.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Panel3.Text = DateTime.Now.ToString("HH:mm")


        AplicarOpcionesHabilitadas()
        MostrarDatosAgentes()
        Timer1.Start()
    End Sub

    Private Sub AplicarOpcionesHabilitadas()
        Dim errores As New List(Of String)
        AplicarEnColeccion(MenuStrip1.Items, True, errores)
        If errores.Count > 0 Then
            MessageBox.Show("Las siguientes opciones quedaron ocultas por falta de configuracion:" &
                            vbCrLf & String.Join(vbCrLf, errores),
                            "Configuracion de permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub AplicarEnColeccion(items As ToolStripItemCollection, padrePermitido As Boolean, errores As List(Of String))
        For Each item As ToolStripItem In items
            If TypeOf item Is ToolStripMenuItem Then
                Dim codigo = TryCast(item.Tag, String)
                If String.IsNullOrWhiteSpace(codigo) Then
                    errores.Add(item.Text.Replace("&", "") & ": sin codigo en Tag.")
                ElseIf Not Permisos.ExisteEnCatalogo(codigo) Then
                    errores.Add(item.Text.Replace("&", "") & ": codigo no registrado (" & codigo & ").")
                End If

                ' Available conserva la visibilidad local definida en el disenador.
                Dim permitido = padrePermitido AndAlso item.Available AndAlso Permisos.Autorizado(codigo)
                item.Visible = permitido
                Dim menuItem = DirectCast(item, ToolStripMenuItem)
                AplicarEnColeccion(menuItem.DropDownItems, permitido AndAlso item.Enabled, errores)
            End If
        Next

        ' Evitar separadores al inicio, al final o entre opciones ocultas.
        Dim hayOpcion As Boolean = False
        Dim separadorPendiente As ToolStripSeparator = Nothing
        For Each item As ToolStripItem In items
            If TypeOf item Is ToolStripSeparator Then
                item.Visible = False
                If hayOpcion AndAlso separadorPendiente Is Nothing Then
                    separadorPendiente = DirectCast(item, ToolStripSeparator)
                End If
            ElseIf item.Available Then
                If separadorPendiente IsNot Nothing Then separadorPendiente.Visible = True
                separadorPendiente = Nothing
                hayOpcion = True
            End If
        Next
    End Sub

    Private Function PuedeEjecutar(item As ToolStripMenuItem) As Boolean
        Dim actual = item
        While actual IsNot Nothing
            If Not actual.Available OrElse Not actual.Enabled OrElse
                Not Permisos.Autorizado(TryCast(actual.Tag, String)) Then
                MessageBox.Show("No tiene permiso para ejecutar esta opcion.",
                                "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            actual = TryCast(actual.OwnerItem, ToolStripMenuItem)
        End While
        Return True
    End Function

    Private Sub MainForm_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MostrarDatosAgentes()
    End Sub

    Private Sub MostrarDatosAgentes()
        Dim agentesActivos As Integer = 0
        Dim agentesEventuales As Integer = 0
        Dim agentesSinMarcarEntrada As Integer = 0
        Dim reporte = Reportes.ObtenerTotalesAgentes()
        lblDatosAgentes.Text =
            $"Agentes: {reporte("TotalAgentes")}" & vbCrLf &
            $"Ag.Marcan: {reporte("TotalAgentesQueMarcan")}" & vbCrLf &
            $"Marcaron: {reporte("AgentesHoy")}" & vbCrLf &
            $"Sin marcar: {reporte("AgentesSinMarcar")}" & vbCrLf &
            $"Inas/Vacac: {reporte("Inasistencias")}" & vbCrLf &
            $"Cumpleaños: {reporte("CumpleMes")}" & vbCrLf &
            $"Ingreso 6m: {reporte("Ingresaron_4a6m")}" & vbCrLf &
            $"Edad 65: {reporte("Edad_64a65")}"
    End Sub

    ' Eventos del menú Configuración
    Private Sub MnuConImp_Click(sender As Object, e As EventArgs) Handles MnuConImp.Click
        If Not PuedeEjecutar(MnuConImp) Then Return
        ' Especificar Impresora
        Dim printDialog As New PrintDialog()
        printDialog.ShowDialog()
    End Sub

    Private Sub MnuCalculadora_Click(sender As Object, e As EventArgs) Handles MnuCalculadora.Click
        If Not PuedeEjecutar(MnuCalculadora) Then Return
        ' Abrir calculadora
        Process.Start("calc.exe")
    End Sub

    Private Sub MnuAlmanaque_Click(sender As Object, e As EventArgs) Handles MnuAlmanaque.Click
        If Not PuedeEjecutar(MnuAlmanaque) Then Return
        ' Abrir almanaque
        MessageBox.Show("Función de almanaque no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuClave_Click(sender As Object, e As EventArgs) Handles MnuClave.Click
        If Not PuedeEjecutar(MnuClave) Then Return
        ' Ingreso de claves
        MessageBox.Show("Función de ingreso de claves no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuSalir_Click(sender As Object, e As EventArgs) Handles MnuSalir.Click
        If Not PuedeEjecutar(MnuSalir) Then Return
        Me.Close()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Cerrar todas las conexiones de base de datos
        Try
            DSM.CloseAllConnections()
        Catch ex As Exception
            ' Ignorar errores al cerrar conexiones
        End Try

        ' Terminar la aplicación completamente
        Application.Exit()
    End Sub

    ' Eventos del menú Importaciones
    Private Sub MnuBajaNovedades_Click(sender As Object, e As EventArgs) Handles MnuBajaNovedades.Click
        If Not PuedeEjecutar(MnuBajaNovedades) Then Return
        ' MessageBox.Show("Función de bajar novedades reloj no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        frmBajaReloj.AbrirInstancia(Me)
    End Sub

    Private Sub MnuImportarAuto_Click(sender As Object, e As EventArgs) Handles MnuImportarAuto.Click
        If Not PuedeEjecutar(MnuImportarAuto) Then Return
        MessageBox.Show("Función de importar novedades AutoShop en desuso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Actualizaciones
    Private Sub MnuMantenimientoAgentes_Click(sender As Object, e As EventArgs) Handles MnuMantenimientoAgentes.Click
        If Not PuedeEjecutar(MnuMantenimientoAgentes) Then Return
        frmAgentes.AbrirInstancia(Me, False) ' Solo permanentes (Eventual = 0)
    End Sub

    Private Sub MnuMantenimientoEventuales_Click(sender As Object, e As EventArgs) Handles MnuMantenimientoEventuales.Click
        If Not PuedeEjecutar(MnuMantenimientoEventuales) Then Return
        frmAgentes.AbrirInstancia(Me, True) ' Solo permanentes (Eventual = 0)
    End Sub

    ' Eventos del menú Ventanas
    Private Sub MnuVentanasCascada_Click(sender As Object, e As EventArgs) Handles MnuVentanasCascada.Click
        If Not PuedeEjecutar(MnuVentanasCascada) Then Return
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub MnuVentanasVertical_Click(sender As Object, e As EventArgs) Handles MnuVentanasVertical.Click
        If Not PuedeEjecutar(MnuVentanasVertical) Then Return
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub MnuVentanasHorizontal_Click(sender As Object, e As EventArgs) Handles MnuVentanasHorizontal.Click
        If Not PuedeEjecutar(MnuVentanasHorizontal) Then Return
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub MnuVentanasCerrar_Click(sender As Object, e As EventArgs) Handles MnuVentanasCerrar.Click
        If Not PuedeEjecutar(MnuVentanasCerrar) Then Return
        If Me.ActiveMdiChild IsNot Nothing Then
            Me.ActiveMdiChild.Close()
        End If
    End Sub

    Private Sub MnuVentanasCerrarTodas_Click(sender As Object, e As EventArgs) Handles MnuVentanasCerrarTodas.Click
        If Not PuedeEjecutar(MnuVentanasCerrarTodas) Then Return
        For Each childForm As Form In Me.MdiChildren
            childForm.Close()
        Next
    End Sub

    Private Sub MnuVentanasReorganizar_Click(sender As Object, e As EventArgs) Handles MnuVentanasReorganizar.Click
        If Not PuedeEjecutar(MnuVentanasReorganizar) Then Return
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub MnuVentanasImprimir_Click(sender As Object, e As EventArgs) Handles MnuVentanasImprimir.Click
        If Not PuedeEjecutar(MnuVentanasImprimir) Then Return
        If Me.ActiveMdiChild IsNot Nothing Then
            ' Implementar impresión de ventana activa
            MessageBox.Show("Función de imprimir ventana no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' Eventos del menú Ayuda
    Private Sub MnuAcercaDe_Click(sender As Object, e As EventArgs) Handles MnuAcercaDe.Click
        If Not PuedeEjecutar(MnuAcercaDe) Then Return
        MessageBox.Show("Sistema de Gestión de Personal" & vbCrLf & "Guerrini Neumáticos S.A" & vbCrLf & "Versión: " & Application.ProductVersion, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuAyudaHelp_Click(sender As Object, e As EventArgs) Handles MnuAyudaHelp.Click
        If Not PuedeEjecutar(MnuAyudaHelp) Then Return
        Dim frm As New frmPruebas()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    ' Eventos adicionales del menú Actualizaciones
    Private Sub MnuIngresoHorario_Click(sender As Object, e As EventArgs) Handles MnuIngresoHorario.Click
        If Not PuedeEjecutar(MnuIngresoHorario) Then Return
        frmIngresoHorario.AbrirInstancia(Me)
    End Sub

    Private Sub MnuIngresoInasistencias_Click(sender As Object, e As EventArgs) Handles MnuIngresoInasistencias.Click
        If Not PuedeEjecutar(MnuIngresoInasistencias) Then Return
        frmInasistenciasJustificadas.AbrirInstancia(Me)
    End Sub

    Private Sub MnuBajaInasistencia_Click(sender As Object, e As EventArgs) Handles MnuBajaInasistencia.Click
        If Not PuedeEjecutar(MnuBajaInasistencia) Then Return
        MessageBox.Show("Función de baja inasistencia no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuDetectarInasistencias_Click(sender As Object, e As EventArgs) Handles MnuDetectarInasistencias.Click
        If Not PuedeEjecutar(MnuDetectarInasistencias) Then Return
        ' MessageBox.Show("Función de detectar inasistencias no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        frmBuscaFalta.AbrirInstancia(Me)
    End Sub

    Private Sub MnuIngresoHorarioManual_Click(sender As Object, e As EventArgs) Handles MnuIngresoHorarioManual.Click
        If Not PuedeEjecutar(MnuIngresoHorarioManual) Then Return
        frmIngresoHorarioManual.AbrirInstancia(Me)
    End Sub

    Private Sub MnuCambiarLegajo_Click(sender As Object, e As EventArgs) Handles MnuCambiarLegajo.Click
        If Not PuedeEjecutar(MnuCambiarLegajo) Then Return
        MessageBox.Show("Función de cambiar legajo en movimiento no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuVacaciones_Click(sender As Object, e As EventArgs) Handles MnuVacaciones.Click
        If Not PuedeEjecutar(MnuVacaciones) Then Return
        frmVacaciones.AbrirInstancia(Me)
    End Sub

    ' Eventos del menú Consultas
    Private Sub MnuListadoMensualSucursal_Click(sender As Object, e As EventArgs) Handles MnuListadoMensualSucursal.Click
        If Not PuedeEjecutar(MnuListadoMensualSucursal) Then Return
        frmLstMensual.AbrirInstancia(Me)
    End Sub

    Private Sub MnuListadoMensualOficina_Click(sender As Object, e As EventArgs) Handles MnuListadoMensualOficina.Click
        If Not PuedeEjecutar(MnuListadoMensualOficina) Then Return
        frmLstMensual.AbrirInstancia(Me)
    End Sub

    Private Sub MnuListadoDiario_Click(sender As Object, e As EventArgs) Handles MnuListadoDiario.Click
        If Not PuedeEjecutar(MnuListadoDiario) Then Return
        frmHorario.AbrirInstancia(Me)
    End Sub

    Private Sub MnuConsultarAgentes_Click(sender As Object, e As EventArgs) Handles MnuConsultarAgentes.Click
        If Not PuedeEjecutar(MnuConsultarAgentes) Then Return
        frmAgentes.AbrirInstancia(Me)
    End Sub

    Private Sub MnuResumenAsistencia_Click(sender As Object, e As EventArgs) Handles MnuResumenAsistencia.Click
        If Not PuedeEjecutar(MnuResumenAsistencia) Then Return
        frmResumenAsistencia.AbrirInstancia(Me)
    End Sub

    Private Sub MnuResumenEventuales_Click(sender As Object, e As EventArgs) Handles MnuResumenEventuales.Click
        If Not PuedeEjecutar(MnuResumenEventuales) Then Return
        'frmResumenAsistencia.AbrirInstancia(Me, True)
    End Sub

    Private Sub MnuInasistenciaSinAviso_Click(sender As Object, e As EventArgs) Handles MnuInasistenciaSinAviso.Click
        If Not PuedeEjecutar(MnuInasistenciaSinAviso) Then Return
        MessageBox.Show("Función de inasistencia sin aviso no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuListadoInasistencias_Click(sender As Object, e As EventArgs) Handles MnuListadoInasistencias.Click
        If Not PuedeEjecutar(MnuListadoInasistencias) Then Return
        frmListadoInasistencias.AbrirInstancia(Me)
    End Sub

    Private Sub MnuListadoAgentes_Click(sender As Object, e As EventArgs) Handles MnuListadoAgentes.Click
        If Not PuedeEjecutar(MnuListadoAgentes) Then Return
        frmLstAgentes.AbrirInstancia(Me)
    End Sub

    Private Sub MnuCumpleanos_Click(sender As Object, e As EventArgs) Handles MnuCumpleanos.Click
        If Not PuedeEjecutar(MnuCumpleanos) Then Return
        frmCumples.AbrirInstancia(Me)
    End Sub

    ' Eventos del menú Nomencladores
    Private Sub MnuAreas_Click(sender As Object, e As EventArgs) Handles MnuAreas.Click
        If Not PuedeEjecutar(MnuAreas) Then Return
        frmAreas.AbrirInstancia(Me)
    End Sub

    Private Sub MnuCategorias_Click(sender As Object, e As EventArgs) Handles MnuCategorias.Click
        If Not PuedeEjecutar(MnuCategorias) Then Return
        frmCategorias.AbrirInstancia(Me)
    End Sub

    Private Sub MnuFeriados_Click(sender As Object, e As EventArgs) Handles MnuFeriados.Click
        If Not PuedeEjecutar(MnuFeriados) Then Return
        frmFeriados.AbrirInstancia(Me)
    End Sub

    Private Sub MnuEncargados_Click(sender As Object, e As EventArgs) Handles MnuEncargados.Click
        If Not PuedeEjecutar(MnuEncargados) Then Return
        frmEncargados.AbrirInstancia(Me)
    End Sub

    Private Sub MnuSucursales_Click(sender As Object, e As EventArgs) Handles MnuSucursales.Click
        If Not PuedeEjecutar(MnuSucursales) Then Return
        frmSucursales.AbrirInstancia(Me)
    End Sub

    Private Sub MnuTiposInasistencias_Click(sender As Object, e As EventArgs) Handles MnuTiposInasistencias.Click
        If Not PuedeEjecutar(MnuTiposInasistencias) Then Return
        frmTipoInasistencias.AbrirInstancia(Me)
    End Sub

    Private Sub MnuTipoActividad_Click(sender As Object, e As EventArgs) Handles MnuTipoActividad.Click
        If Not PuedeEjecutar(MnuTipoActividad) Then Return
        MessageBox.Show("Función de tipo actividad no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Seguridad
    Private Sub MnuInformacionReservada_Click(sender As Object, e As EventArgs) Handles MnuInformacionReservada.Click
        If Not PuedeEjecutar(MnuInformacionReservada) Then Return
        MessageBox.Show("Función de información reservada no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuIniciarSesion_Click(sender As Object, e As EventArgs) Handles MnuIniciarSesion.Click
        If Not PuedeEjecutar(MnuIniciarSesion) Then Return
        MessageBox.Show("Función de iniciar sesión no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuEditarINI_Click(sender As Object, e As EventArgs) Handles MnuEditarINI.Click
        If Not PuedeEjecutar(MnuEditarINI) Then Return
        MessageBox.Show("Función de editar .INI no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MainForm_MdiChildActivate(sender As Object, e As EventArgs) Handles MyBase.MdiChildActivate

    End Sub

    Private Sub pnlDatosAgentes_Click(sender As Object, e As EventArgs) Handles pnlDatosAgentes.Click
        frmDatosAgentes.AbrirInstancia(Me)
        MostrarDatosAgentes()
    End Sub

    Private Sub lblDatosAgentes_Click(sender As Object, e As EventArgs) Handles lblDatosAgentes.Click
        frmDatosAgentes.AbrirInstancia(Me)
    End Sub

    Private Sub TallesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TallesToolStripMenuItem.Click
        If Not PuedeEjecutar(TallesToolStripMenuItem) Then Return
        frmTalles.AbrirInstancia(Me)
    End Sub

    Private Sub UniformesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UniformesToolStripMenuItem.Click
        If Not PuedeEjecutar(UniformesToolStripMenuItem) Then Return
        frmTipoUniformes.AbrirInstancia(Me)
    End Sub
End Class
