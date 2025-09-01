Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports System.Globalization
Imports System.Threading

Public Class frmSesion
    Private intentos As Integer = 0
    Private bloqueado As Boolean = False

    Private usuarioParam As String = String.Empty
    Private passwordParam As String = String.Empty

    Private Sub frmSesion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim culturaFija As New CultureInfo("es-AR")
        CultureInfo.DefaultThreadCurrentCulture = culturaFija
        CultureInfo.DefaultThreadCurrentUICulture = culturaFija

        General.InicializarProyecto()

        If Not ProcesarArgumentos(Environment.GetCommandLineArgs()) Then
            TxtUsuario.Text = General.UsuarioActual.ToUpper()
#If DEBUG Then
            If General.UsuarioActual = "ADMIN" Then txtPassword.Text = "QSECOFR"
#End If
            Me.BeginInvoke(Sub() txtPassword.Focus())
        Else
            TxtUsuario.Text = usuarioParam.ToUpper()
            txtPassword.Text = passwordParam
            Me.BeginInvoke(Sub() btnAceptar.PerformClick())
        End If
    End Sub

    Public Function ProcesarArgumentos(args As String())
        If args.Length < 3 Then Return False
        usuarioParam = args(1)
        passwordParam = args(2)
        If String.IsNullOrEmpty(usuarioParam) OrElse String.IsNullOrEmpty(passwordParam) Then Return False
        Return True
    End Function

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAceptar.PerformClick()
        End If
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            intentos += 1
            Dim usuario As String = TxtUsuario.Text.Trim().ToUpper()
            Dim password As String = txtPassword.Text.Trim()

            General.UsuarioActual = usuario
            DSM.UsuarioActual = usuario

            If String.IsNullOrEmpty(usuario) OrElse String.IsNullOrEmpty(password) Then
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sql = "SELECT * FROM Usuarios WHERE IdUsuario = @Usuario"
            Dim tabla = DSM.ExecuteQuery(DSM.Seguridad, sql, CmdParams("@Usuario", usuario), True)

            If tabla.Rows.Count = 0 Then
                ManejarErrorLogin()
                Return
            End If

            Dim storedPassword As String = tabla.Rows(0)("Clave").ToString()
            Dim nombreUsuario As String = tabla.Rows(0)("Nombre").ToString()
            bloqueado = Convert.ToBoolean(tabla.Rows(0)("Bloqueado"))

            If bloqueado Then
                ManejarErrorLogin()
                Return
            End If

            If Encriptar(UCase(password)) <> storedPassword Then
                ManejarErrorLogin()
                Return
            End If

            General.OpcionesHabilitadas = ObtenerOpcionesHabilitadas(usuario)

            If String.IsNullOrEmpty(General.OpcionesHabilitadas) Then
                MessageBox.Show("No se encontraron opciones habilitadas para el usuario actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If

            Me.Hide()
            Dim mainForm As New MainForm()
            mainForm.Show()

        Catch ex As Exception
            MessageBox.Show("Error al intentar conectar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Application.Exit()
    End Sub

    Private Function ObtenerOpcionesHabilitadas(usuario As String) As String
        Try
            Dim sql = "SELECT Autoriz FROM Habilitaciones WHERE Usuario = @Usuario AND Modulo = @Modulo"
            Dim tabla = DSM.ExecuteQuery(DSM.Seguridad, sql, CmdParams("@Usuario", usuario, "@Modulo", General.modulo), True)
            If tabla.Rows.Count > 0 Then
                Return tabla.Rows(0)("Autoriz").ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al obtener habilitaciones: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return String.Empty
    End Function

    Private Sub ManejarErrorLogin()

        If bloqueado Then
            MessageBox.Show("USUARIO BLOQUEADO PARA ESTA APLICACION.......", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End If

        If intentos >= 3 Then
            MessageBox.Show("Ha excedido el número máximo de intentos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End If

        MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        txtPassword.Clear()
        txtPassword.Focus()
    End Sub

    Public Function Encriptar(Pass As String)
        Dim Encrip, Letra As String
        Dim i, j As Integer
        Encrip = ""
        If Len(Pass) > 0 Then
            For i = Len(Pass) To 1 Step -1
                Letra = Mid(Pass, i, 1)
                Encrip = Encrip & Letra
            Next
            Pass = Encrip
            Encrip = ""
            For i = 1 To Len(Pass)
                j = Asc(Mid(Pass, i, 1)) + 11 + i
                If j > 255 Then
                    j = j - 255
                End If
                Encrip = Encrip + Chr(j)
            Next
        End If
        Return Encrip
    End Function

End Class
