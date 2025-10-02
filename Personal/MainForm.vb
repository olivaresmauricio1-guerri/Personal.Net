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

        Catch ex As Exception
            MessageBox.Show($"Error al cargar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Actualizar los paneles con la información actual
        Panel1.Text = $"Sistema de Personal - Sucursal: {DescripcionSucursal}"
        Panel4.Text = UsuarioActual & " | " & Mid(General.Entorno, 1, 3).ToUpper()
        Panel2.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Panel3.Text = DateTime.Now.ToString("HH:mm")

        'AplicarOpcionesHabilitadas()
    End Sub

    ' Eventos del menú Configuración
    Private Sub MnuConImp_Click(sender As Object, e As EventArgs) Handles MnuConImp.Click
        ' Especificar Impresora
        Dim printDialog As New PrintDialog()
        printDialog.ShowDialog()
    End Sub

    Private Sub MnuCalculadora_Click(sender As Object, e As EventArgs) Handles MnuCalculadora.Click
        ' Abrir calculadora
        Process.Start("calc.exe")
    End Sub

    Private Sub MnuAlmanaque_Click(sender As Object, e As EventArgs) Handles MnuAlmanaque.Click
        ' Abrir almanaque
        MessageBox.Show("Función de almanaque no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuClave_Click(sender As Object, e As EventArgs) Handles MnuClave.Click
        ' Ingreso de claves
        MessageBox.Show("Función de ingreso de claves no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuSalir_Click(sender As Object, e As EventArgs) Handles MnuSalir.Click
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
        ' MessageBox.Show("Función de bajar novedades reloj no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        frmBajaReloj.AbrirInstancia(Me)
    End Sub

    Private Sub MnuImportarAuto_Click(sender As Object, e As EventArgs) Handles MnuImportarAuto.Click
        MessageBox.Show("Función de importar novedades AutoShop en desuso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Actualizaciones
    Private Sub MnuMantenimientoAgentes_Click(sender As Object, e As EventArgs) Handles MnuMantenimientoAgentes.Click
        frmAgentes.AbrirInstancia(Me)
    End Sub

    Private Sub MnuMantenimientoEventuales_Click(sender As Object, e As EventArgs) Handles MnuMantenimientoEventuales.Click
        MessageBox.Show("Función de mantenimiento de eventuales no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Ventanas
    Private Sub MnuVentanasCascada_Click(sender As Object, e As EventArgs) Handles MnuVentanasCascada.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub MnuVentanasVertical_Click(sender As Object, e As EventArgs) Handles MnuVentanasVertical.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub MnuVentanasHorizontal_Click(sender As Object, e As EventArgs) Handles MnuVentanasHorizontal.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub MnuVentanasCerrar_Click(sender As Object, e As EventArgs) Handles MnuVentanasCerrar.Click
        If Me.ActiveMdiChild IsNot Nothing Then
            Me.ActiveMdiChild.Close()
        End If
    End Sub

    Private Sub MnuVentanasCerrarTodas_Click(sender As Object, e As EventArgs) Handles MnuVentanasCerrarTodas.Click
        For Each childForm As Form In Me.MdiChildren
            childForm.Close()
        Next
    End Sub

    Private Sub MnuVentanasReorganizar_Click(sender As Object, e As EventArgs) Handles MnuVentanasReorganizar.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub MnuVentanasImprimir_Click(sender As Object, e As EventArgs) Handles MnuVentanasImprimir.Click
        If Me.ActiveMdiChild IsNot Nothing Then
            ' Implementar impresión de ventana activa
            MessageBox.Show("Función de imprimir ventana no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' Eventos del menú Ayuda
    Private Sub MnuAcercaDe_Click(sender As Object, e As EventArgs) Handles MnuAcercaDe.Click
        MessageBox.Show("Sistema de Gestión de Personal" & vbCrLf & "Guerrini Neumáticos S.A" & vbCrLf & "Versión: " & Application.ProductVersion, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuAyudaHelp_Click(sender As Object, e As EventArgs) Handles MnuAyudaHelp.Click
        Dim frm As New frmPruebas()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    ' Eventos adicionales del menú Actualizaciones
    Private Sub MnuIngresoHorario_Click(sender As Object, e As EventArgs) Handles MnuIngresoHorario.Click
        frmIngresoHorario.AbrirInstancia(Me)
    End Sub

    Private Sub MnuIngresoInasistencias_Click(sender As Object, e As EventArgs) Handles MnuIngresoInasistencias.Click
        frmInasistenciasJustificadas.AbrirInstancia(Me)
    End Sub

    Private Sub MnuBajaInasistencia_Click(sender As Object, e As EventArgs) Handles MnuBajaInasistencia.Click
        MessageBox.Show("Función de baja inasistencia no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuDetectarInasistencias_Click(sender As Object, e As EventArgs) Handles MnuDetectarInasistencias.Click
        MessageBox.Show("Función de detectar inasistencias no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuIngresoHorarioManual_Click(sender As Object, e As EventArgs) Handles MnuIngresoHorarioManual.Click
        frmIngresoHorarioManual.AbrirInstancia(Me)
    End Sub

    Private Sub MnuCambiarLegajo_Click(sender As Object, e As EventArgs) Handles MnuCambiarLegajo.Click
        MessageBox.Show("Función de cambiar legajo en movimiento no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuVacaciones_Click(sender As Object, e As EventArgs) Handles MnuVacaciones.Click
        MessageBox.Show("Función de vacaciones no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Consultas
    Private Sub MnuListadoMensualSucursal_Click(sender As Object, e As EventArgs) Handles MnuListadoMensualSucursal.Click
        frmLstMensual.AbrirInstancia(Me)
    End Sub

    Private Sub MnuListadoMensualOficina_Click(sender As Object, e As EventArgs) Handles MnuListadoMensualOficina.Click
        frmLstMensual.AbrirInstancia(Me)
    End Sub

    Private Sub MnuListadoDiario_Click(sender As Object, e As EventArgs) Handles MnuListadoDiario.Click
        frmHorario.AbrirInstancia(Me)
    End Sub

    Private Sub MnuConsultarAgentes_Click(sender As Object, e As EventArgs) Handles MnuConsultarAgentes.Click
        frmAgentes.AbrirInstancia(Me)
    End Sub

    Private Sub MnuResumenAsistencia_Click(sender As Object, e As EventArgs) Handles MnuResumenAsistencia.Click
        frmResumenAsistencia.AbrirInstancia(Me)
    End Sub

    Private Sub MnuResumenEventuales_Click(sender As Object, e As EventArgs) Handles MnuResumenEventuales.Click
        frmResumenAsistenciaEventuales.AbrirInstancia(Me)
    End Sub

    Private Sub MnuInasistenciaSinAviso_Click(sender As Object, e As EventArgs) Handles MnuInasistenciaSinAviso.Click
        MessageBox.Show("Función de inasistencia sin aviso no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuListadoInasistencias_Click(sender As Object, e As EventArgs) Handles MnuListadoInasistencias.Click
        MessageBox.Show("Función de listado de inasistencias no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuListadoAgentes_Click(sender As Object, e As EventArgs) Handles MnuListadoAgentes.Click
        MessageBox.Show("Función de listados de agentes no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuCumpleanos_Click(sender As Object, e As EventArgs) Handles MnuCumpleanos.Click
        MessageBox.Show("Función de cumpleaños del mes no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Nomencladores
    Private Sub MnuAreas_Click(sender As Object, e As EventArgs) Handles MnuAreas.Click
        frmAreas.AbrirInstancia(Me)
    End Sub

    Private Sub MnuCategorias_Click(sender As Object, e As EventArgs) Handles MnuCategorias.Click
        frmCategorias.AbrirInstancia(Me)
    End Sub

    Private Sub MnuFeriados_Click(sender As Object, e As EventArgs) Handles MnuFeriados.Click
        MessageBox.Show("Función de feriados no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuEncargados_Click(sender As Object, e As EventArgs) Handles MnuEncargados.Click
        frmEncargados.AbrirInstancia(Me)
    End Sub

    Private Sub MnuSucursales_Click(sender As Object, e As EventArgs) Handles MnuSucursales.Click
        frmSucursales.AbrirInstancia(Me)
    End Sub

    Private Sub MnuTiposInasistencias_Click(sender As Object, e As EventArgs) Handles MnuTiposInasistencias.Click
        frmTipoInasistencias.AbrirInstancia(Me)
    End Sub

    Private Sub MnuTipoActividad_Click(sender As Object, e As EventArgs) Handles MnuTipoActividad.Click
        MessageBox.Show("Función de tipo actividad no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Eventos del menú Seguridad
    Private Sub MnuInformacionReservada_Click(sender As Object, e As EventArgs) Handles MnuInformacionReservada.Click
        MessageBox.Show("Función de información reservada no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuIniciarSesion_Click(sender As Object, e As EventArgs) Handles MnuIniciarSesion.Click
        MessageBox.Show("Función de iniciar sesión no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuEditarINI_Click(sender As Object, e As EventArgs) Handles MnuEditarINI.Click
        MessageBox.Show("Función de editar .INI no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class