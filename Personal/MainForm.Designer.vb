<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        StatusBar1 = New StatusStrip()
        Panel1 = New ToolStripStatusLabel()
        Panel2 = New ToolStripStatusLabel()
        Panel3 = New ToolStripStatusLabel()
        Panel4 = New ToolStripStatusLabel()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        MnuConfiguracion = New ToolStripMenuItem()
        MnuConImp = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        MnuCalculadora = New ToolStripMenuItem()
        MnuAlmanaque = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        MnuClave = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        MnuSalir = New ToolStripMenuItem()
        MnuImportaciones = New ToolStripMenuItem()
        MnuBajaNovedades = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        MnuImportarAuto = New ToolStripMenuItem()
        MnuActualizaciones = New ToolStripMenuItem()
        MnuMantenimientoAgentes = New ToolStripMenuItem()
        MnuMantenimientoEventuales = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        MnuIngresoHorario = New ToolStripMenuItem()
        MnuIngresoInasistencias = New ToolStripMenuItem()
        MnuBajaInasistencia = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        MnuDetectarInasistencias = New ToolStripMenuItem()
        ToolStripSeparator7 = New ToolStripSeparator()
        MnuIngresoHorarioManual = New ToolStripMenuItem()
        ToolStripSeparator8 = New ToolStripSeparator()
        MnuCambiarLegajo = New ToolStripMenuItem()
        ToolStripSeparator9 = New ToolStripSeparator()
        MnuVacaciones = New ToolStripMenuItem()
        MnuConsultas = New ToolStripMenuItem()
        MnuListadoMensualSucursal = New ToolStripMenuItem()
        ToolStripSeparator10 = New ToolStripSeparator()
        MnuListadoMensualOficina = New ToolStripMenuItem()
        ToolStripSeparator11 = New ToolStripSeparator()
        MnuListadoDiario = New ToolStripMenuItem()
        ToolStripSeparator12 = New ToolStripSeparator()
        MnuConsultarAgentes = New ToolStripMenuItem()
        ToolStripSeparator13 = New ToolStripSeparator()
        MnuResumenAsistencia = New ToolStripMenuItem()
        MnuResumenEventuales = New ToolStripMenuItem()
        ToolStripSeparator14 = New ToolStripSeparator()
        MnuInasistenciaSinAviso = New ToolStripMenuItem()
        MnuListadoInasistencias = New ToolStripMenuItem()
        ToolStripSeparator15 = New ToolStripSeparator()
        MnuListadoAgentes = New ToolStripMenuItem()
        MnuCumpleanos = New ToolStripMenuItem()
        MnuNomencladores = New ToolStripMenuItem()
        MnuAreas = New ToolStripMenuItem()
        ToolStripSeparator16 = New ToolStripSeparator()
        MnuCategorias = New ToolStripMenuItem()
        ToolStripSeparator17 = New ToolStripSeparator()
        MnuFeriados = New ToolStripMenuItem()
        ToolStripSeparator18 = New ToolStripSeparator()
        MnuEncargados = New ToolStripMenuItem()
        ToolStripSeparator19 = New ToolStripSeparator()
        MnuSucursales = New ToolStripMenuItem()
        ToolStripSeparator20 = New ToolStripSeparator()
        MnuTiposInasistencias = New ToolStripMenuItem()
        ToolStripSeparator21 = New ToolStripSeparator()
        MnuTipoActividad = New ToolStripMenuItem()
        MnuSeguridad = New ToolStripMenuItem()
        MnuInformacionReservada = New ToolStripMenuItem()
        ToolStripSeparator22 = New ToolStripSeparator()
        MnuIniciarSesion = New ToolStripMenuItem()
        ToolStripSeparator23 = New ToolStripSeparator()
        MnuEditarINI = New ToolStripMenuItem()
        MnuVentanas = New ToolStripMenuItem()
        MnuVentanasActivas = New ToolStripMenuItem()
        MnuVentanasCerrar = New ToolStripMenuItem()
        MnuVentanasCerrarTodas = New ToolStripMenuItem()
        ToolStripSeparator24 = New ToolStripSeparator()
        MnuVentanasCascada = New ToolStripMenuItem()
        MnuVentanasVertical = New ToolStripMenuItem()
        MnuVentanasHorizontal = New ToolStripMenuItem()
        ToolStripSeparator25 = New ToolStripSeparator()
        MnuVentanasReorganizar = New ToolStripMenuItem()
        MnuVentanasImprimir = New ToolStripMenuItem()
        MnuAyuda = New ToolStripMenuItem()
        MnuAcercaDe = New ToolStripMenuItem()
        MnuAyudaHelp = New ToolStripMenuItem()
        MenuStrip1 = New MenuStrip()
        StatusBar1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' StatusBar1
        ' 
        StatusBar1.ImageScalingSize = New Size(20, 20)
        StatusBar1.Items.AddRange(New ToolStripItem() {Panel1, Panel2, Panel3, Panel4})
        StatusBar1.Location = New Point(0, 437)
        StatusBar1.Name = "StatusBar1"
        StatusBar1.Padding = New Padding(1, 0, 17, 0)
        StatusBar1.Size = New Size(1081, 22)
        StatusBar1.TabIndex = 1
        StatusBar1.Text = "StatusStrip1"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Silver
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(917, 17)
        Panel1.Spring = True
        Panel1.Text = "Sistema de Contabilidad"
        Panel1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Silver
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(65, 17)
        Panel2.Text = "01/01/2024"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Silver
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(34, 17)
        Panel3.Text = "16:02"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Silver
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(47, 17)
        Panel4.Text = "Usuario"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(23, 23)
        ' 
        ' MnuConfiguracion
        ' 
        MnuConfiguracion.DropDownItems.AddRange(New ToolStripItem() {MnuConImp, ToolStripSeparator1, MnuCalculadora, MnuAlmanaque, ToolStripSeparator2, MnuClave, ToolStripSeparator3, MnuSalir})
        MnuConfiguracion.Name = "MnuConfiguracion"
        MnuConfiguracion.Size = New Size(95, 20)
        MnuConfiguracion.Text = "&Configuración"
        ' 
        ' MnuConImp
        ' 
        MnuConImp.Name = "MnuConImp"
        MnuConImp.Size = New Size(186, 22)
        MnuConImp.Text = "Especificar &Impresora"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(183, 6)
        ' 
        ' MnuCalculadora
        ' 
        MnuCalculadora.Name = "MnuCalculadora"
        MnuCalculadora.Size = New Size(186, 22)
        MnuCalculadora.Text = "&Calculadora"
        ' 
        ' MnuAlmanaque
        ' 
        MnuAlmanaque.Name = "MnuAlmanaque"
        MnuAlmanaque.Size = New Size(186, 22)
        MnuAlmanaque.Text = "&Almanaque"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(183, 6)
        ' 
        ' MnuClave
        ' 
        MnuClave.Name = "MnuClave"
        MnuClave.Size = New Size(186, 22)
        MnuClave.Text = "Ingreso de Claves"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(183, 6)
        ' 
        ' MnuSalir
        ' 
        MnuSalir.Name = "MnuSalir"
        MnuSalir.Size = New Size(186, 22)
        MnuSalir.Text = "&Salir"
        ' 
        ' MnuImportaciones
        ' 
        MnuImportaciones.DropDownItems.AddRange(New ToolStripItem() {MnuBajaNovedades, ToolStripSeparator4, MnuImportarAuto})
        MnuImportaciones.Name = "MnuImportaciones"
        MnuImportaciones.Size = New Size(95, 20)
        MnuImportaciones.Text = "&Importaciones"
        ' 
        ' MnuBajaNovedades
        ' 
        MnuBajaNovedades.Name = "MnuBajaNovedades"
        MnuBajaNovedades.Size = New Size(267, 22)
        MnuBajaNovedades.Text = "Bajar Novedades Reloj"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(264, 6)
        ' 
        ' MnuImportarAuto
        ' 
        MnuImportarAuto.Name = "MnuImportarAuto"
        MnuImportarAuto.Size = New Size(267, 22)
        MnuImportarAuto.Text = "Importar Novedades Reloj AutoShop"
        ' 
        ' MnuActualizaciones
        ' 
        MnuActualizaciones.DropDownItems.AddRange(New ToolStripItem() {MnuMantenimientoAgentes, MnuMantenimientoEventuales, ToolStripSeparator5, MnuIngresoHorario, MnuIngresoInasistencias, MnuBajaInasistencia, ToolStripSeparator6, MnuDetectarInasistencias, ToolStripSeparator7, MnuIngresoHorarioManual, ToolStripSeparator8, MnuCambiarLegajo, ToolStripSeparator9, MnuVacaciones})
        MnuActualizaciones.Name = "MnuActualizaciones"
        MnuActualizaciones.Size = New Size(101, 20)
        MnuActualizaciones.Text = "&Actualizaciones"
        ' 
        ' MnuMantenimientoAgentes
        ' 
        MnuMantenimientoAgentes.Name = "MnuMantenimientoAgentes"
        MnuMantenimientoAgentes.Size = New Size(245, 22)
        MnuMantenimientoAgentes.Text = "Mantenimiento de &Agentes"
        ' 
        ' MnuMantenimientoEventuales
        ' 
        MnuMantenimientoEventuales.Name = "MnuMantenimientoEventuales"
        MnuMantenimientoEventuales.Size = New Size(245, 22)
        MnuMantenimientoEventuales.Text = "Mantenimiento de Eventuales"
        MnuMantenimientoEventuales.Visible = False
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(242, 6)
        ' 
        ' MnuIngresoHorario
        ' 
        MnuIngresoHorario.Name = "MnuIngresoHorario"
        MnuIngresoHorario.Size = New Size(245, 22)
        MnuIngresoHorario.Text = "&Ingreso Horario"
        ' 
        ' MnuIngresoInasistencias
        ' 
        MnuIngresoInasistencias.Name = "MnuIngresoInasistencias"
        MnuIngresoInasistencias.Size = New Size(245, 22)
        MnuIngresoInasistencias.Text = "&Ingreso Inasistencias Justificadas"
        ' 
        ' MnuBajaInasistencia
        ' 
        MnuBajaInasistencia.Name = "MnuBajaInasistencia"
        MnuBajaInasistencia.Size = New Size(245, 22)
        MnuBajaInasistencia.Text = "&Baja Inasistencia"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(242, 6)
        ' 
        ' MnuDetectarInasistencias
        ' 
        MnuDetectarInasistencias.Name = "MnuDetectarInasistencias"
        MnuDetectarInasistencias.Size = New Size(245, 22)
        MnuDetectarInasistencias.Text = "&Detectar Inasistencias"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(242, 6)
        ' 
        ' MnuIngresoHorarioManual
        ' 
        MnuIngresoHorarioManual.Name = "MnuIngresoHorarioManual"
        MnuIngresoHorarioManual.Size = New Size(245, 22)
        MnuIngresoHorarioManual.Text = "Ingreso Horario Manual"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(242, 6)
        ' 
        ' MnuCambiarLegajo
        ' 
        MnuCambiarLegajo.Name = "MnuCambiarLegajo"
        MnuCambiarLegajo.Size = New Size(245, 22)
        MnuCambiarLegajo.Text = "&Cambiar Legajo en Movimiento"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(242, 6)
        ' 
        ' MnuVacaciones
        ' 
        MnuVacaciones.Name = "MnuVacaciones"
        MnuVacaciones.Size = New Size(245, 22)
        MnuVacaciones.Text = "&Vacaciones"
        ' 
        ' MnuConsultas
        ' 
        MnuConsultas.DropDownItems.AddRange(New ToolStripItem() {MnuListadoMensualSucursal, ToolStripSeparator10, MnuListadoMensualOficina, ToolStripSeparator11, MnuListadoDiario, ToolStripSeparator12, MnuConsultarAgentes, ToolStripSeparator13, MnuResumenAsistencia, MnuResumenEventuales, ToolStripSeparator14, MnuInasistenciaSinAviso, MnuListadoInasistencias, ToolStripSeparator15, MnuListadoAgentes, MnuCumpleanos})
        MnuConsultas.Name = "MnuConsultas"
        MnuConsultas.Size = New Size(71, 20)
        MnuConsultas.Text = "C&onsultas"
        ' 
        ' MnuListadoMensualSucursal
        ' 
        MnuListadoMensualSucursal.Name = "MnuListadoMensualSucursal"
        MnuListadoMensualSucursal.Size = New Size(264, 22)
        MnuListadoMensualSucursal.Text = "Listado Mensual x Sucursal"
        ' 
        ' ToolStripSeparator10
        ' 
        ToolStripSeparator10.Name = "ToolStripSeparator10"
        ToolStripSeparator10.Size = New Size(261, 6)
        ' 
        ' MnuListadoMensualOficina
        ' 
        MnuListadoMensualOficina.Name = "MnuListadoMensualOficina"
        MnuListadoMensualOficina.Size = New Size(264, 22)
        MnuListadoMensualOficina.Text = "&Listado Mensual x Sucursal x Oficina"
        ' 
        ' ToolStripSeparator11
        ' 
        ToolStripSeparator11.Name = "ToolStripSeparator11"
        ToolStripSeparator11.Size = New Size(261, 6)
        ' 
        ' MnuListadoDiario
        ' 
        MnuListadoDiario.Name = "MnuListadoDiario"
        MnuListadoDiario.Size = New Size(264, 22)
        MnuListadoDiario.Text = "Listado Diario Control Horario"
        ' 
        ' ToolStripSeparator12
        ' 
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New Size(261, 6)
        ' 
        ' MnuConsultarAgentes
        ' 
        MnuConsultarAgentes.Name = "MnuConsultarAgentes"
        MnuConsultarAgentes.Size = New Size(264, 22)
        MnuConsultarAgentes.Text = "&Consultar Agentes"
        ' 
        ' ToolStripSeparator13
        ' 
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New Size(261, 6)
        ' 
        ' MnuResumenAsistencia
        ' 
        MnuResumenAsistencia.Name = "MnuResumenAsistencia"
        MnuResumenAsistencia.Size = New Size(264, 22)
        MnuResumenAsistencia.Text = "&Resumen de Asistencia"
        ' 
        ' MnuResumenEventuales
        ' 
        MnuResumenEventuales.Name = "MnuResumenEventuales"
        MnuResumenEventuales.Size = New Size(264, 22)
        MnuResumenEventuales.Text = "Resumen de Asistencia Eventuales"
        MnuResumenEventuales.Visible = False
        ' 
        ' ToolStripSeparator14
        ' 
        ToolStripSeparator14.Name = "ToolStripSeparator14"
        ToolStripSeparator14.Size = New Size(261, 6)
        ' 
        ' MnuInasistenciaSinAviso
        ' 
        MnuInasistenciaSinAviso.Name = "MnuInasistenciaSinAviso"
        MnuInasistenciaSinAviso.Size = New Size(264, 22)
        MnuInasistenciaSinAviso.Text = "Inasistencia sin aviso"
        ' 
        ' MnuListadoInasistencias
        ' 
        MnuListadoInasistencias.Name = "MnuListadoInasistencias"
        MnuListadoInasistencias.Size = New Size(264, 22)
        MnuListadoInasistencias.Text = "Listado de Inasistencias"
        ' 
        ' ToolStripSeparator15
        ' 
        ToolStripSeparator15.Name = "ToolStripSeparator15"
        ToolStripSeparator15.Size = New Size(261, 6)
        ' 
        ' MnuListadoAgentes
        ' 
        MnuListadoAgentes.Name = "MnuListadoAgentes"
        MnuListadoAgentes.Size = New Size(264, 22)
        MnuListadoAgentes.Text = "&Listados de Agentes"
        ' 
        ' MnuCumpleanos
        ' 
        MnuCumpleanos.Name = "MnuCumpleanos"
        MnuCumpleanos.Size = New Size(264, 22)
        MnuCumpleanos.Text = "Cumpleaños del Mes"
        ' 
        ' MnuNomencladores
        ' 
        MnuNomencladores.DropDownItems.AddRange(New ToolStripItem() {MnuAreas, ToolStripSeparator16, MnuCategorias, ToolStripSeparator17, MnuFeriados, ToolStripSeparator18, MnuEncargados, ToolStripSeparator19, MnuSucursales, ToolStripSeparator20, MnuTiposInasistencias, ToolStripSeparator21, MnuTipoActividad})
        MnuNomencladores.Name = "MnuNomencladores"
        MnuNomencladores.Size = New Size(103, 20)
        MnuNomencladores.Text = "&Nomencladores"
        ' 
        ' MnuAreas
        ' 
        MnuAreas.Name = "MnuAreas"
        MnuAreas.Size = New Size(172, 22)
        MnuAreas.Text = "&Áreas"
        ' 
        ' ToolStripSeparator16
        ' 
        ToolStripSeparator16.Name = "ToolStripSeparator16"
        ToolStripSeparator16.Size = New Size(169, 6)
        ' 
        ' MnuCategorias
        ' 
        MnuCategorias.Name = "MnuCategorias"
        MnuCategorias.Size = New Size(172, 22)
        MnuCategorias.Text = "&Categorías"
        ' 
        ' ToolStripSeparator17
        ' 
        ToolStripSeparator17.Name = "ToolStripSeparator17"
        ToolStripSeparator17.Size = New Size(169, 6)
        ' 
        ' MnuFeriados
        ' 
        MnuFeriados.Name = "MnuFeriados"
        MnuFeriados.Size = New Size(172, 22)
        MnuFeriados.Text = "&Feriados"
        ' 
        ' ToolStripSeparator18
        ' 
        ToolStripSeparator18.Name = "ToolStripSeparator18"
        ToolStripSeparator18.Size = New Size(169, 6)
        ' 
        ' MnuEncargados
        ' 
        MnuEncargados.Name = "MnuEncargados"
        MnuEncargados.Size = New Size(172, 22)
        MnuEncargados.Text = "Encargados"
        ' 
        ' ToolStripSeparator19
        ' 
        ToolStripSeparator19.Name = "ToolStripSeparator19"
        ToolStripSeparator19.Size = New Size(169, 6)
        ' 
        ' MnuSucursales
        ' 
        MnuSucursales.Name = "MnuSucursales"
        MnuSucursales.Size = New Size(172, 22)
        MnuSucursales.Text = "&Sucursales"
        ' 
        ' ToolStripSeparator20
        ' 
        ToolStripSeparator20.Name = "ToolStripSeparator20"
        ToolStripSeparator20.Size = New Size(169, 6)
        ' 
        ' MnuTiposInasistencias
        ' 
        MnuTiposInasistencias.Name = "MnuTiposInasistencias"
        MnuTiposInasistencias.Size = New Size(172, 22)
        MnuTiposInasistencias.Text = "&Tipos Inasistencias"
        ' 
        ' ToolStripSeparator21
        ' 
        ToolStripSeparator21.Name = "ToolStripSeparator21"
        ToolStripSeparator21.Size = New Size(169, 6)
        ' 
        ' MnuTipoActividad
        ' 
        MnuTipoActividad.Name = "MnuTipoActividad"
        MnuTipoActividad.Size = New Size(172, 22)
        MnuTipoActividad.Text = "&Tipo Actividad"
        ' 
        ' MnuSeguridad
        ' 
        MnuSeguridad.DropDownItems.AddRange(New ToolStripItem() {MnuInformacionReservada, ToolStripSeparator22, MnuIniciarSesion, ToolStripSeparator23, MnuEditarINI})
        MnuSeguridad.Name = "MnuSeguridad"
        MnuSeguridad.Size = New Size(72, 20)
        MnuSeguridad.Text = "&Seguridad"
        ' 
        ' MnuInformacionReservada
        ' 
        MnuInformacionReservada.Name = "MnuInformacionReservada"
        MnuInformacionReservada.Size = New Size(195, 22)
        MnuInformacionReservada.Text = "&Información Reservada"
        ' 
        ' ToolStripSeparator22
        ' 
        ToolStripSeparator22.Name = "ToolStripSeparator22"
        ToolStripSeparator22.Size = New Size(192, 6)
        ' 
        ' MnuIniciarSesion
        ' 
        MnuIniciarSesion.Name = "MnuIniciarSesion"
        MnuIniciarSesion.Size = New Size(195, 22)
        MnuIniciarSesion.Text = "&Iniciar Sesión"
        ' 
        ' ToolStripSeparator23
        ' 
        ToolStripSeparator23.Name = "ToolStripSeparator23"
        ToolStripSeparator23.Size = New Size(192, 6)
        ' 
        ' MnuEditarINI
        ' 
        MnuEditarINI.Name = "MnuEditarINI"
        MnuEditarINI.Size = New Size(195, 22)
        MnuEditarINI.Text = "&Editar .INI"
        ' 
        ' MnuVentanas
        ' 
        MnuVentanas.DropDownItems.AddRange(New ToolStripItem() {MnuVentanasActivas, MnuVentanasCerrar, MnuVentanasCerrarTodas, ToolStripSeparator24, MnuVentanasCascada, MnuVentanasVertical, MnuVentanasHorizontal, ToolStripSeparator25, MnuVentanasReorganizar, MnuVentanasImprimir})
        MnuVentanas.Name = "MnuVentanas"
        MnuVentanas.Size = New Size(66, 20)
        MnuVentanas.Text = "&Ventanas"
        ' 
        ' MnuVentanasActivas
        ' 
        MnuVentanasActivas.Name = "MnuVentanasActivas"
        MnuVentanasActivas.Size = New Size(174, 22)
        MnuVentanasActivas.Text = "Ventanas &Activas"
        ' 
        ' MnuVentanasCerrar
        ' 
        MnuVentanasCerrar.Name = "MnuVentanasCerrar"
        MnuVentanasCerrar.Size = New Size(174, 22)
        MnuVentanasCerrar.Text = "&Cerrar"
        ' 
        ' MnuVentanasCerrarTodas
        ' 
        MnuVentanasCerrarTodas.Name = "MnuVentanasCerrarTodas"
        MnuVentanasCerrarTodas.Size = New Size(174, 22)
        MnuVentanasCerrarTodas.Text = "Cerrar &Todas"
        ' 
        ' ToolStripSeparator24
        ' 
        ToolStripSeparator24.Name = "ToolStripSeparator24"
        ToolStripSeparator24.Size = New Size(171, 6)
        ' 
        ' MnuVentanasCascada
        ' 
        MnuVentanasCascada.Name = "MnuVentanasCascada"
        MnuVentanasCascada.Size = New Size(174, 22)
        MnuVentanasCascada.Text = "&Cascada"
        ' 
        ' MnuVentanasVertical
        ' 
        MnuVentanasVertical.Name = "MnuVentanasVertical"
        MnuVentanasVertical.Size = New Size(174, 22)
        MnuVentanasVertical.Text = "&Vertical"
        ' 
        ' MnuVentanasHorizontal
        ' 
        MnuVentanasHorizontal.Name = "MnuVentanasHorizontal"
        MnuVentanasHorizontal.Size = New Size(174, 22)
        MnuVentanasHorizontal.Text = "&Horizontal"
        ' 
        ' ToolStripSeparator25
        ' 
        ToolStripSeparator25.Name = "ToolStripSeparator25"
        ToolStripSeparator25.Size = New Size(171, 6)
        ' 
        ' MnuVentanasReorganizar
        ' 
        MnuVentanasReorganizar.Name = "MnuVentanasReorganizar"
        MnuVentanasReorganizar.Size = New Size(174, 22)
        MnuVentanasReorganizar.Text = "Reorganizar &Iconos"
        ' 
        ' MnuVentanasImprimir
        ' 
        MnuVentanasImprimir.Name = "MnuVentanasImprimir"
        MnuVentanasImprimir.Size = New Size(174, 22)
        MnuVentanasImprimir.Text = "&Imprimir Ventana"
        ' 
        ' MnuAyuda
        ' 
        MnuAyuda.DropDownItems.AddRange(New ToolStripItem() {MnuAcercaDe, MnuAyudaHelp})
        MnuAyuda.Name = "MnuAyuda"
        MnuAyuda.Size = New Size(24, 20)
        MnuAyuda.Text = "&?"
        ' 
        ' MnuAcercaDe
        ' 
        MnuAcercaDe.Name = "MnuAcercaDe"
        MnuAcercaDe.Size = New Size(135, 22)
        MnuAcercaDe.Text = "&Acerca de..."
        ' 
        ' MnuAyudaHelp
        ' 
        MnuAyudaHelp.Name = "MnuAyudaHelp"
        MnuAyudaHelp.Size = New Size(135, 22)
        MnuAyudaHelp.Text = "A&yuda"
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {MnuConfiguracion, MnuImportaciones, MnuActualizaciones, MnuConsultas, MnuNomencladores, MnuSeguridad, MnuVentanas, MnuAyuda})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.MdiWindowListItem = MnuVentanasActivas
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(5, 2, 0, 2)
        MenuStrip1.Size = New Size(1081, 24)
        MenuStrip1.TabIndex = 1
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Gray
        ClientSize = New Size(1081, 459)
        Controls.Add(StatusBar1)
        Controls.Add(MenuStrip1)
        IsMdiContainer = True
        MainMenuStrip = MenuStrip1
        Name = "MainForm"
        Text = "Sistema de Gestión de Personal - GUERRINI NEUMATICOS S.A"
        WindowState = FormWindowState.Maximized
        StatusBar1.ResumeLayout(False)
        StatusBar1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents StatusBar1 As StatusStrip
    Friend WithEvents Panel1 As ToolStripStatusLabel
    Friend WithEvents Panel2 As ToolStripStatusLabel
    Friend WithEvents Panel3 As ToolStripStatusLabel
    Friend WithEvents Panel4 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents MnuConfiguracion As ToolStripMenuItem
    Friend WithEvents MnuConImp As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents MnuCalculadora As ToolStripMenuItem
    Friend WithEvents MnuAlmanaque As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents MnuClave As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents MnuSalir As ToolStripMenuItem
    Friend WithEvents MnuImportaciones As ToolStripMenuItem
    Friend WithEvents MnuBajaNovedades As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents MnuImportarAuto As ToolStripMenuItem
    Friend WithEvents MnuActualizaciones As ToolStripMenuItem
    Friend WithEvents MnuMantenimientoAgentes As ToolStripMenuItem
    Friend WithEvents MnuMantenimientoEventuales As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents MnuIngresoHorario As ToolStripMenuItem
    Friend WithEvents MnuIngresoInasistencias As ToolStripMenuItem
    Friend WithEvents MnuBajaInasistencia As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents MnuDetectarInasistencias As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents MnuIngresoHorarioManual As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents MnuCambiarLegajo As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents MnuVacaciones As ToolStripMenuItem
    Friend WithEvents MnuConsultas As ToolStripMenuItem
    Friend WithEvents MnuListadoMensualSucursal As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents MnuListadoMensualOficina As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents MnuListadoDiario As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents MnuConsultarAgentes As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents MnuResumenAsistencia As ToolStripMenuItem
    Friend WithEvents MnuResumenEventuales As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents MnuInasistenciaSinAviso As ToolStripMenuItem
    Friend WithEvents MnuListadoInasistencias As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents MnuListadoAgentes As ToolStripMenuItem
    Friend WithEvents MnuCumpleanos As ToolStripMenuItem
    Friend WithEvents MnuNomencladores As ToolStripMenuItem
    Friend WithEvents MnuAreas As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As ToolStripSeparator
    Friend WithEvents MnuCategorias As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As ToolStripSeparator
    Friend WithEvents MnuFeriados As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As ToolStripSeparator
    Friend WithEvents MnuEncargados As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As ToolStripSeparator
    Friend WithEvents MnuSucursales As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As ToolStripSeparator
    Friend WithEvents MnuTiposInasistencias As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As ToolStripSeparator
    Friend WithEvents MnuTipoActividad As ToolStripMenuItem
    Friend WithEvents MnuSeguridad As ToolStripMenuItem
    Friend WithEvents MnuInformacionReservada As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator22 As ToolStripSeparator
    Friend WithEvents MnuIniciarSesion As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator23 As ToolStripSeparator
    Friend WithEvents MnuEditarINI As ToolStripMenuItem
    Friend WithEvents MnuVentanas As ToolStripMenuItem
    Friend WithEvents MnuVentanasActivas As ToolStripMenuItem
    Friend WithEvents MnuVentanasCerrar As ToolStripMenuItem
    Friend WithEvents MnuVentanasCerrarTodas As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator24 As ToolStripSeparator
    Friend WithEvents MnuVentanasCascada As ToolStripMenuItem
    Friend WithEvents MnuVentanasVertical As ToolStripMenuItem
    Friend WithEvents MnuVentanasHorizontal As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator25 As ToolStripSeparator
    Friend WithEvents MnuVentanasReorganizar As ToolStripMenuItem
    Friend WithEvents MnuVentanasImprimir As ToolStripMenuItem
    Friend WithEvents MnuAyuda As ToolStripMenuItem
    Friend WithEvents MnuAcercaDe As ToolStripMenuItem
    Friend WithEvents MnuAyudaHelp As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
End Class