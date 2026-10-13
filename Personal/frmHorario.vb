Imports System.Security.Cryptography
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmHorario

    Private Shared instancia As frmHorario

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmHorario()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmAreas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmHorario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboSucursales()

        Dim hoy = DateTime.Today
        dtpDesde.Value = New DateTime(hoy.Year, hoy.Month, 1)
        dtpHasta.Value = New DateTime(hoy.Year, hoy.Month, DateTime.DaysInMonth(hoy.Year, hoy.Month))
        radNormal.Checked = True
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click

        If dtpDesde.Value > dtpHasta.Value Then
            MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim sucursalId As Integer = CInt(cmbSucursal.SelectedValue)
        Dim sucursalTexto As String = cmbSucursal.Text
        Dim soloTarde As Boolean = chkTarde.Checked
        Dim vista As String = If(radSemana.Checked, "semana", If(radAgente.Checked, "agente", "normal"))
        Dim reporte As String = If(chkDuplicado.Checked, "lsthorario2", "lsthorario")

        Reportes.ListadoHorario(dtpDesde.Value, dtpHasta.Value, sucursalId, sucursalTexto, soloTarde)

        Try
            Process.Start(General.ReportesPath, $"Personal {reporte} vista {vista}")
        Catch ex As Exception
            MessageBox.Show("Error al abrir el reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPublicar_Click(sender As Object, e As EventArgs) Handles btnPublicar.Click
        If cmbSucursal.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione una sucursal.", "Publicar control horario", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fechaDesde As Date = dtpDesde.Value.Date
        Dim fechaHasta As Date = dtpHasta.Value.Date
        If fechaDesde > fechaHasta Then
            MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Se publica exactamente el rango elegido. Cada periodo se corta el domingo
        ' o el ultimo dia del mes, para que nunca mezcle dos meses distintos.
        If fechaHasta >= Date.Today Then
            fechaHasta = Date.Today.AddDays(-1)
        End If
        If fechaHasta < fechaDesde Then
            MessageBox.Show("El rango seleccionado no contiene dias ya finalizados.",
                            "Publicar control horario", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If (fechaHasta - fechaDesde).TotalDays > 181 Then
            MessageBox.Show("Seleccione como maximo 182 dias por publicacion.", "Publicar control horario", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim cantidadPeriodos As Integer = 0
        Dim inicioPeriodo As Date = fechaDesde
        While inicioPeriodo <= fechaHasta
            Dim diasHastaDomingo As Integer = (7 - CInt(inicioPeriodo.DayOfWeek)) Mod 7
            Dim finSemana As Date = inicioPeriodo.AddDays(diasHastaDomingo)
            Dim finMes As New Date(inicioPeriodo.Year, inicioPeriodo.Month, Date.DaysInMonth(inicioPeriodo.Year, inicioPeriodo.Month))
            Dim finPeriodo As Date = If(finSemana < finMes, finSemana, finMes)
            If fechaHasta < finPeriodo Then finPeriodo = fechaHasta
            cantidadPeriodos += 1
            inicioPeriodo = finPeriodo.AddDays(1)
        End While

        Dim sucursalTexto As String = cmbSucursal.Text
        Dim confirmacion As String =
            $"Se publicaran {cantidadPeriodos} periodo(s) semanal(es) para {sucursalTexto}:" & Environment.NewLine &
            $"Desde {fechaDesde:dd/MM/yyyy} hasta {fechaHasta:dd/MM/yyyy}." & Environment.NewLine &
            "Cada periodo termina el domingo o el ultimo dia del mes." & Environment.NewLine & Environment.NewLine &
            "Si un periodo ya fue publicado, se creara una nueva version y se conservara la anterior." & Environment.NewLine &
            "Desea continuar?"
        If MessageBox.Show(confirmacion, "Publicar control horario", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Return
        End If

        btnPublicar.Enabled = False
        Try
            Dim usuario As String = If(String.IsNullOrWhiteSpace(General.UsuarioActual), Environment.UserName, General.UsuarioActual.Trim())
            Dim resultado = Reportes.PublicarControlHorario(fechaDesde, fechaHasta, sucursalTexto, usuario)
            MessageBox.Show(
                $"Publicación finalizada.{Environment.NewLine}" &
                $"Periodos semanales: {resultado.Semanas}{Environment.NewLine}" &
                $"Agentes: {resultado.Agentes}{Environment.NewLine}" &
                $"Marcaciones: {resultado.Registros}",
                "Publicar control horario", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("No se pudo publicar el control horario: " & ex.Message,
                            "Publicar control horario", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnPublicar.Enabled = True
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub CargarComboSucursales()
        Dim sql As String = "Select * from Institutos order by Descripcion"
        Dim sucursales As DataTable = DSM.ExecuteQuery(DSM.Personal, sql)

        cmbSucursal.DataSource = sucursales
        cmbSucursal.DisplayMember = "Descripcion"
        cmbSucursal.ValueMember = "id"

        cmbSucursal.SelectedValue = 2
    End Sub

    Private Calcular()
End Class