Imports System.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Permisos
    Private autorizaciones As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
    Private usuarioSesion As String = String.Empty

    Public Sub Limpiar()
        autorizaciones.Clear()
        usuarioSesion = String.Empty
    End Sub

    Public Sub Cargar(usuario As String)
        Limpiar()
        If String.IsNullOrWhiteSpace(usuario) Then
            Throw New ArgumentException("Debe indicar el usuario para cargar sus permisos.")
        End If

        Dim sqlHabilitacion = "SELECT Usuario FROM Habilitaciones WHERE Usuario = @Usuario AND Modulo = @Modulo"
        Dim habilitaciones = DSM.ExecuteQuery(
            DSM.Seguridad, sqlHabilitacion,
            CmdParams("@Usuario", usuario, "@Modulo", General.modulo), True)
        If habilitaciones.Rows.Count = 0 Then
            Throw New InvalidOperationException("El usuario no esta habilitado para el modulo " & General.modulo & ".")
        End If

        Dim sql = "
            SELECT m.codigo, a.autorizado
            FROM Menues AS m
            LEFT JOIN Autorizaciones AS a
                ON a.codigoMenu = m.codigo AND a.usuario = @Usuario
            WHERE m.modulo = @Modulo"
        Dim tabla = DSM.ExecuteQuery(
            DSM.Seguridad, sql,
            CmdParams("@Usuario", usuario, "@Modulo", General.modulo), True)
        If tabla.Rows.Count = 0 Then
            Throw New InvalidOperationException("No hay menus registrados para el modulo " & General.modulo & ".")
        End If

        ' Publicar la carga solo cuando todas las filas hayan sido validadas.
        Dim nuevos As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
        For Each row As DataRow In tabla.Rows
            Dim codigo = row("codigo").ToString().Trim()
            If String.IsNullOrWhiteSpace(codigo) Then
                Throw New InvalidOperationException("El catalogo contiene un menu sin codigo.")
            End If
            If nuevos.ContainsKey(codigo) Then
                Throw New InvalidOperationException("Hay registros duplicados para el codigo de menu: " & codigo)
            End If
            nuevos.Add(codigo, Not row.IsNull("autorizado") AndAlso Convert.ToBoolean(row("autorizado")))
        Next
        autorizaciones = nuevos
        usuarioSesion = usuario
    End Sub

    Public Function ExisteEnCatalogo(codigo As String) As Boolean
        Return SesionValida() AndAlso Not String.IsNullOrWhiteSpace(codigo) AndAlso
            autorizaciones.ContainsKey(codigo)
    End Function

    Public Function Autorizado(codigo As String) As Boolean
        If Not SesionValida() OrElse String.IsNullOrWhiteSpace(codigo) Then Return False
        Dim permitido As Boolean
        Return autorizaciones.TryGetValue(codigo, permitido) AndAlso permitido
    End Function

    Private Function SesionValida() As Boolean
        Return Not String.IsNullOrWhiteSpace(usuarioSesion) AndAlso
            String.Equals(usuarioSesion, General.UsuarioActual, StringComparison.OrdinalIgnoreCase)
    End Function
End Module
