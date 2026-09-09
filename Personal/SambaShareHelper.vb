Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices

Public Class SambaShareHelper

    Private Const RESOURCETYPE_DISK As Integer = &H1
    Private Const CONNECT_NO_FLAGS As Integer = &H0
    Private Const CONNECT_UPDATE_PROFILE As Integer = &H1
    Private Const ERROR_ALREADY_ASSIGNED As Integer = 85
    Private Const ERROR_DEVICE_ALREADY_REMEMBERED As Integer = 1202
    Private Const ERROR_BAD_NET_NAME As Integer = 67
    Private Const ERROR_ACCESS_DENIED As Integer = 5
    Private Const ERROR_LOGON_FAILURE As Integer = 1326

    <StructLayout(LayoutKind.Sequential)>
    Private Structure NETRESOURCE
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        <MarshalAs(UnmanagedType.LPWStr)> Public lpLocalName As String
        <MarshalAs(UnmanagedType.LPWStr)> Public lpRemoteName As String
        <MarshalAs(UnmanagedType.LPWStr)> Public lpComment As String
        <MarshalAs(UnmanagedType.LPWStr)> Public lpProvider As String
    End Structure

    <DllImport("mpr.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Shared Function WNetAddConnection2(ByRef lpNetResource As NETRESOURCE,
                                               ByVal lpPassword As String,
                                               ByVal lpUserName As String,
                                               ByVal dwFlags As Integer) As Integer
    End Function

    <DllImport("mpr.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Shared Function WNetCancelConnection2(ByVal lpName As String,
                                                  ByVal dwFlags As Integer,
                                                  <MarshalAs(UnmanagedType.Bool)> ByVal fForce As Boolean) As Integer
    End Function

    Public Shared Function ConectarShare(ByVal uncPath As String,
                                          ByVal usuario As String,
                                          ByVal dominio As String,
                                          ByVal password As String,
                                          <Out> Optional ByRef mensajeError As String = Nothing) As Boolean
        If String.IsNullOrWhiteSpace(uncPath) Then
            mensajeError = "Ruta UNC no puede estar vacía."
            Return False
        End If

        Dim nr As New NETRESOURCE With {
            .dwType = RESOURCETYPE_DISK,
            .lpRemoteName = uncPath.TrimEnd("\"c)
        }

        Dim userFull As String = usuario
        If Not String.IsNullOrWhiteSpace(dominio) Then
            userFull = dominio & "\" & usuario
        End If

        Dim resultado As Integer = WNetAddConnection2(nr, password, userFull, CONNECT_NO_FLAGS)

        If resultado = 0 OrElse resultado = ERROR_ALREADY_ASSIGNED OrElse resultado = ERROR_DEVICE_ALREADY_REMEMBERED Then
            mensajeError = Nothing
            Return True
        End If

        mensajeError = ObtenerMensajeError(resultado)
        Return False
    End Function

    Public Shared Function DesconectarShare(ByVal uncPath As String,
                                             Optional ByVal forzar As Boolean = True,
                                             <Out> Optional ByRef mensajeError As String = Nothing) As Boolean
        If String.IsNullOrWhiteSpace(uncPath) Then
            mensajeError = "Ruta UNC no puede estar vacía."
            Return False
        End If

        Dim resultado As Integer = WNetCancelConnection2(uncPath.TrimEnd("\"c), CONNECT_UPDATE_PROFILE, forzar)

        If resultado = 0 Then
            mensajeError = Nothing
            Return True
        End If

        mensajeError = ObtenerMensajeError(resultado)
        Return False
    End Function

    Public Shared Function CopiarPdfASamba(ByVal rutaLocalPdf As String,
                                            ByVal rutaDestinoUnc As String,
                                            ByVal uncRaiz As String,
                                            ByVal usuario As String,
                                            ByVal dominio As String,
                                            ByVal password As String,
                                            Optional sobreescribir As Boolean = True,
                                            <Out> Optional ByRef mensajeError As String = Nothing) As Boolean
        mensajeError = Nothing

        If Not File.Exists(rutaLocalPdf) Then
            mensajeError = "Archivo local no existe: " & rutaLocalPdf
            Return False
        End If

        Dim raiz As String = If(uncRaiz Is Nothing, "", uncRaiz.TrimEnd("\"c))
        If String.IsNullOrWhiteSpace(raiz) OrElse Not rutaDestinoUnc.StartsWith(raiz, StringComparison.OrdinalIgnoreCase) Then
            mensajeError = "Ruta destino UNC fuera de la raíz permitida."
            Return False
        End If

        Dim conectadoOk As Boolean = ConectarShare(uncRaiz, usuario, dominio, password, mensajeError)
        If Not conectadoOk Then
            Return False
        End If

        Dim carpetaDestino As String = Path.GetDirectoryName(rutaDestinoUnc)
        Try
            If Not Directory.Exists(carpetaDestino) Then
                Directory.CreateDirectory(carpetaDestino)
            End If

            File.Copy(rutaLocalPdf, rutaDestinoUnc, sobreescribir)
            Return True
        Catch ex As Exception
            If String.IsNullOrWhiteSpace(mensajeError) Then
                If ex.Message.IndexOf("carpeta", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                   Not String.IsNullOrWhiteSpace(carpetaDestino) AndAlso
                   Not Directory.Exists(carpetaDestino) Then
                    mensajeError = "Error al crear carpeta destino UNC: " & ex.Message
                Else
                    mensajeError = "Error copiando PDF a share Samba: " & ex.Message
                End If
            End If
            Return False
        Finally
            Dim errDummy As String = Nothing
            DesconectarShare(uncRaiz, True, errDummy)
        End Try
    End Function

    Public Shared Function AbrirPdfShareSamba(ByVal rutaPdf As String,
                                               ByVal uncRaiz As String,
                                               ByVal usuario As String,
                                               ByVal dominio As String,
                                               ByVal password As String,
                                               <Out> Optional ByRef mensajeError As String = Nothing) As Boolean
        mensajeError = Nothing

        If String.IsNullOrWhiteSpace(rutaPdf) Then
            mensajeError = "No se informó la ruta del PDF."
            Return False
        End If

        Dim raiz As String = If(uncRaiz Is Nothing, "", uncRaiz.TrimEnd("\"c))
        Dim requiereSamba As Boolean =
            Not String.IsNullOrWhiteSpace(raiz) AndAlso
            Not String.IsNullOrWhiteSpace(usuario) AndAlso
            rutaPdf.StartsWith(raiz, StringComparison.OrdinalIgnoreCase)

        If requiereSamba Then
            Dim okConectar As Boolean = ConectarShare(uncRaiz, usuario, dominio, password, mensajeError)
            If Not okConectar Then
                If String.IsNullOrWhiteSpace(mensajeError) Then
                    mensajeError = "No se pudo autenticar contra el share Samba para acceder al PDF."
                End If
                Return False
            End If
        End If

        Try
            If Not File.Exists(rutaPdf) Then
                mensajeError = "No se encontró el archivo PDF seleccionado."
                Return False
            End If

            Process.Start(New ProcessStartInfo(rutaPdf) With {
                .UseShellExecute = True
            })
            Return True
        Catch ex As Exception
            mensajeError = "No se pudo abrir el archivo PDF seleccionado: " & ex.Message
            Return False
        Finally
            If requiereSamba Then
                Dim errDummy As String = Nothing
                DesconectarShare(uncRaiz, True, errDummy)
            End If
        End Try
    End Function

    Private Shared Function ObtenerMensajeError(ByVal codigo As Integer) As String
        Select Case codigo
            Case ERROR_ACCESS_DENIED : Return "Acceso denegado (5). Verifique credenciales."
            Case ERROR_BAD_NET_NAME : Return "Nombre de recurso red incorrecto (67). Verifique ruta UNC."
            Case ERROR_LOGON_FAILURE : Return "Error de inicio de sesión (1326). Usuario/clave incorrectos."
            Case Else
                Try
                    Return New Win32Exception(codigo).Message
                Catch
                    Return "Error Win32 código: " & codigo.ToString()
                End Try
        End Select
    End Function

End Class
