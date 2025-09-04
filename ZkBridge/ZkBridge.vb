Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Web.Script.Serialization
Imports zkemkeeper

<ComVisible(True)>
<Guid("D2E1B4F4-2A57-4C34-B842-8E1AF1B31234")>
<InterfaceType(ComInterfaceType.InterfaceIsIDispatch)>
Public Interface IZkBridge
    Function Conectar(ip As String, puerto As Integer) As Boolean
    Sub Desconectar()
    Function LeerLogs() As String ' JSON

    Function Ping() As Boolean
End Interface

<ComVisible(True)>
<Guid("E81B5C72-49C8-4E1A-9C6E-9A07E52A5678")>
<ClassInterface(ClassInterfaceType.None)>
<ProgId("ZkBridge.Attendance")>
Public Class ZkBridge
    Implements IZkBridge

    ' IMPORTANTE: WithEvents para capturar eventos del ActiveX
    Private WithEvents _zk As New CZKEM()

    Private _ip As String = ""
    Private _puerto As Integer = 4370
    Private _connected As Boolean = False
    Private ReadOnly _lock As New Object()

    ' ===== Eventos del SDK: marcan el estado en tiempo real =====
    Private Sub _zk_OnConnected() Handles _zk.OnConnected
        _connected = True
    End Sub

    Private Sub _zk_OnDisConnected() Handles _zk.OnDisConnected
        _connected = False
    End Sub

    ' ===== API expuesta por COM =====
    Public Function Conectar(ip As String, puerto As Integer) As Boolean Implements IZkBridge.Conectar
        SyncLock _lock
            _ip = ip
            _puerto = puerto
            _connected = TryConectar()
            Return _connected
        End SyncLock
    End Function

    Public Sub Desconectar() Implements IZkBridge.Desconectar
        SyncLock _lock
            Try : _zk.Disconnect() : Catch : End Try
            _connected = False
        End SyncLock
    End Sub

    Public Function Ping() As Boolean Implements IZkBridge.Ping
        SyncLock _lock
            Return EnsureConnected()
        End SyncLock
    End Function

    Public Function LeerLogs() As String Implements IZkBridge.LeerLogs
        SyncLock _lock
            ' 1) Asegurar conexión (con heartbeat)
            If Not EnsureConnected() Then
                Return "[]"
            End If

            ' 2) Intentar lectura con política de reintentos
            Dim logs As List(Of Object) = TryOpWithReconnect(
                Function()
                    Dim ok As Boolean = False
                    Try
                        ok = _zk.ReadGeneralLogData(1)
                    Catch
                        ok = False
                    End Try
                    If Not ok Then
                        ' Si falla, forzamos excepción para disparar reconexión en TryOpWithReconnect
                        Throw New COMException("ReadGeneralLogData failed")
                    End If

                    Dim res As New List(Of Object)()
                    Dim dwTMachineNumber As Integer
                    Dim dwEnrollNumber As Integer
                    Dim dwEMachineNumber As Integer
                    Dim dwVerifyMode As Integer
                    Dim dwInOutMode As Integer
                    Dim dwYear As Integer
                    Dim dwMonth As Integer
                    Dim dwDay As Integer
                    Dim dwHour As Integer
                    Dim dwMinute As Integer

                    While _zk.GetGeneralLogData(1, dwTMachineNumber, dwEnrollNumber, dwEMachineNumber,
                                                dwVerifyMode, dwInOutMode, dwYear, dwMonth, dwDay, dwHour, dwMinute)
                        Dim fechaHora As New DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, 0)
                        res.Add(New With {
                            .id = dwEnrollNumber,
                            .fechaHora = fechaHora.ToString("yyyy-MM-dd HH:mm"),
                            .verifyMode = dwVerifyMode,
                            .inOutMode = dwInOutMode,
                            .tMachine = dwTMachineNumber,
                            .eMachine = dwEMachineNumber
                        })
                    End While
                    Return res
                End Function,
                maxRetries:=2
            )

            Dim ser = New JavaScriptSerializer()
            Return ser.Serialize(logs)
        End SyncLock
    End Function

    ' ===== Utilitarios internos =====

    ' Reintentos de conexión (3 intentos con backoff)
    Private Function TryConectar() As Boolean
        For i As Integer = 1 To 3
            Try
                If _zk.Connect_Net(_ip, _puerto) Then
                    _connected = True
                    Return True
                End If
            Catch
                ' ignore y reintentar
            End Try
            Thread.Sleep(500 * i)
        Next
        _connected = False
        Return False
    End Function

    ' Heartbeat suave + reconexión si fue necesario
    Private Function EnsureConnected() As Boolean
        If Not _connected Then
            Return TryConectar()
        End If

        ' Heartbeat: si esto tira excepción o falla, reconecto
        Try
            Dim y As Integer, m As Integer, d As Integer, h As Integer, n As Integer, s As Integer
            If Not _zk.GetDeviceTime(1, y, m, d, h, n, s) Then
                ' último error puede dar pista; si falla, reconecto
                Dim errno As Integer = 0
                Try : _zk.GetLastError(errno) : Catch : End Try
                _connected = False
                Return TryConectar()
            End If
        Catch
            _connected = False
            Return TryConectar()
        End Try

        Return True
    End Function

    ' Ejecuta una operación del SDK y, si cae la conexión (False o COMException), reconecta y reintenta.
    Private Function TryOpWithReconnect(Of T)(op As Func(Of T), Optional maxRetries As Integer = 2) As T
        Dim lastEx As Exception = Nothing
        For attempt As Integer = 0 To maxRetries
            Try
                Return op()
            Catch ex As COMException
                lastEx = ex
                _connected = False
                If Not TryConectar() Then
                    ' si no reconecta, sigue el loop a otro intento; si es el último, cae al final
                End If
            Catch ex As Exception
                lastEx = ex
                ' Si fue otra excepción (no conectividad), decidir si reintentar igual
                _connected = False
                If Not TryConectar() Then
                    ' idem
                End If
            End Try
            Thread.Sleep(300 * (attempt + 1))
        Next

        ' si agotó reintentos devolvemos vacío (para interfaz COM) o re-lanzamos
        ' elegimos devolver vacío para no romper al consumidor COM
        If GetType(T) Is GetType(List(Of Object)) Then
            Return CType(DirectCast(New List(Of Object)(), Object), T)
        End If
        Return Nothing
    End Function

End Class
