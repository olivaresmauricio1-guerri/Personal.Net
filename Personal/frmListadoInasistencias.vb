Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmListadoInasistencias
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Private Shared instancia As frmListadoInasistencias
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmListadoInasistencias()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmListadoInasistencias_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmListadoInasistencias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        ' Setear rango por defecto al mes actual
        Dim hoy = DateTime.Today

        ' Cargar combos
        CargarCombos(cmbMes, "Meses", "idMes", "Mes", "idMes")

        'cargo en txtAnio.text el anio actual
        txtAnio.Text = hoy.Year.ToString()

    End Sub



    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub ObtenerInasistencias()
        'obtengo las inasistencias del mes seleccionado de la tabla movimientos insertando en la tabla temporal faltas el Legajo, Nombre, Instituto,dia,  MotivoInasistencia, CuentaDeDia, mes
        'seleccionando solo los agentes que no estan dados de baja de la tabla agentes y los movimientos de la tabla movimientos que tienen el motivo de inasistencia distinto de vacio
        'contando la cantidad de dias si la inasistencias perteneces al dia y mes seleccionado
        Try
            Dim mesSeleccionado As Integer = CInt(cmbMes.SelectedValue)
            Dim anioSeleccionado As Integer = CInt(txtAnio.Text)
            ' Validar que el año sea un número válido
            If anioSeleccionado < 1900 Or anioSeleccionado > DateTime.Now.Year Then
                MessageBox.Show("Por favor, ingrese un año válido.", "Año inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ' Limpiar la tabla temporal Faltas antes de insertar nuevos datos
            Dim sqlDelete As String = "DELETE FROM Faltas"
            DSM.Execute(DSM.Personal, sqlDelete, Nothing, True)
            ' Migración del query original de Access a SQL Server (sin columna Dia)
            Dim mesTexto As String = CStr(cmbMes.Text)
            Dim mesNumero As Integer = CInt(cmbMes.SelectedValue)

            Dim consultaInsert As String =
                "INSERT INTO Faltas (Legajo, Nombre, Instituto, MotivoInasistencia, CuentaDeDia, mes) " &
                "SELECT M.Legajo, A.Nombre, A.Instituto, M.MotivoInasistencia, COUNT(M.Dia) AS CuentaDeDia, @MesTexto AS mes " &
                "FROM Movimiento AS M INNER JOIN Agentes AS A ON M.Legajo = A.Legajo " &
                "WHERE (A.Baja IS NULL OR A.Baja = '') " &
                "AND MONTH(M.Dia) = @MesNumero " &
                "AND YEAR(M.Dia) = @Anio " &
                "AND M.MotivoInasistencia IS NOT NULL " &
                "AND M.MotivoInasistencia <> 'Asueto' " &
                "GROUP BY M.Legajo, A.Nombre, A.Instituto, M.MotivoInasistencia " &
                "ORDER BY A.Nombre"
            ' Parámetros para la consulta
            Dim parametros = CmdParams("@MesNumero", mesNumero, "@MesTexto", mesTexto, "@Anio", anioSeleccionado)
            ' Ejecutar la consulta de inserción
            DSM.Execute(DSM.Personal, consultaInsert, parametros)
            ' Imprimir el reporte
            Process.Start(General.ReportesPath, "Personal listafaltas")
        Catch ex As Exception
            MessageBox.Show($"Error al obtener la lista de inasistencias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        ObtenerInasistencias()
    End Sub

End Class