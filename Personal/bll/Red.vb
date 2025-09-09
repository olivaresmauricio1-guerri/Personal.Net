Imports System.Diagnostics
Imports System.Net
Imports System.Text.RegularExpressions

Module Red

    Private rutasNecesarias As List(Of (red As String, mask As String, gw As String)) =
        New List(Of (String, String, String))()

    Public Sub EstablecerRutas(nuevas As IEnumerable(Of (red As String, mask As String, gw As String)))
        rutasNecesarias = nuevas.ToList()
    End Sub

    Public Sub AsegurarRutasParaIp(ipDestino As String)
        Dim ip As IPAddress = IPAddress.Parse(ipDestino)
        For Each ru In rutasNecesarias
            If PerteneceARed(ip, IPAddress.Parse(ru.red), IPAddress.Parse(ru.mask)) Then
                AsegurarRutaPersistente(ru.red, ru.mask, ru.gw)
            End If
        Next
    End Sub

    Public Function NecesitaRutaParaIp(ipDestino As String) As Boolean
        Dim ip As IPAddress = IPAddress.Parse(ipDestino)
        For Each ru In rutasNecesarias
            If PerteneceARed(ip, IPAddress.Parse(ru.red), IPAddress.Parse(ru.mask)) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function PerteneceARed(ip As IPAddress, network As IPAddress, mask As IPAddress) As Boolean
        Dim ipb = ip.GetAddressBytes()
        Dim nb = network.GetAddressBytes()
        Dim mb = mask.GetAddressBytes()
        For i = 0 To 3
            If (ipb(i) And mb(i)) <> (nb(i) And mb(i)) Then Return False
        Next
        Return True
    End Function

    Private Function ExisteRuta(dest As String, mask As String, gw As String) As Boolean
        Dim psi As New ProcessStartInfo("route.exe", "print -4") With {
            .RedirectStandardOutput = True,
            .UseShellExecute = False,
            .CreateNoWindow = True
        }
        Using p = Process.Start(psi)
            Dim outp = p.StandardOutput.ReadToEnd()
            p.WaitForExit()
            Dim pat = $"(?m)^\s*{Regex.Escape(dest)}\s+{Regex.Escape(mask)}\s+{Regex.Escape(gw)}\s+"
            Return Regex.IsMatch(outp, pat)
        End Using
    End Function

    Private Sub AsegurarRutaPersistente(dest As String, mask As String, gw As String)
        If ExisteRuta(dest, mask, gw) Then Exit Sub
        Dim args = $"-p add {dest} mask {mask} {gw}"
        Dim psi As New ProcessStartInfo("route.exe", args) With {
            .UseShellExecute = True,
            .Verb = "runas"
        }
        Using p = Process.Start(psi)
            p.WaitForExit()
        End Using
        If Not ExisteRuta(dest, mask, gw) Then
            Throw New Exception($"No se pudo agregar la ruta {dest}/{mask} via {gw}.")
        End If
    End Sub

    Public Function ProbeTcp(host As String, port As Integer, timeoutMs As Integer) As Boolean
        Try
            Using c As New Net.Sockets.TcpClient()
                Dim ar = c.BeginConnect(host, port, Nothing, Nothing)
                If ar.AsyncWaitHandle.WaitOne(timeoutMs) AndAlso c.Connected Then
                    c.EndConnect(ar)
                    Return True
                End If
            End Using
        Catch
        End Try
        Return False
    End Function
End Module
