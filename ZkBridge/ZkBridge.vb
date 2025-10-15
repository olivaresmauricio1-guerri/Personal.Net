Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Web.Script.Serialization
Imports zkemkeeper
Imports System.IO
Imports System.Net.Sockets



Friend Module ZkLog
    Private ReadOnly PathLog As String = "c:\util\zkbridge.log"

    Public Sub W(msg As String)
        Try
            ' Directory.CreateDirectory(IO.Path.GetDirectoryName(PathLog))
            File.AppendAllText(PathLog, $"{DateTime.Now:HH:mm:ss.fff} [T{Thread.CurrentThread.ManagedThreadId}] {msg}{Environment.NewLine}")
        Catch
        End Try
        Debug.WriteLine("[ZkBridge] " & msg) ' también al Output
    End Sub
End Module

<ComVisible(True)>
<Guid("D2E1B4F4-2A57-4C34-B842-8E1AF1B31234")>
<InterfaceType(ComInterfaceType.InterfaceIsIDispatch)>
Public Interface IZkBridge
    Function Conectar(ip As String, puerto As Integer, Optional commKey As Integer = 0) As Boolean
    Sub Desconectar()
    Function CargarLogs() As Integer
    Function ObtenerLogs(Optional pagina As Integer = 0) As String
    Function TotalLogs() As Integer
    Function PageSize() As Integer
    Function Ping() As Boolean
End Interface

<ComVisible(True)>
<Guid("E81B5C72-49C8-4E1A-9C6E-9A07E52A5678")>
<ClassInterface(ClassInterfaceType.None)>
<ProgId("ZkBridge.Attendance")>
Public Class ZkBridge
    Implements IZkBridge
    Private Const PAGE_SIZE As Integer = 50

    ' IMPORTANTE: WithEvents para capturar eventos del ActiveX
    Private WithEvents _zk As New CZKEM()

    Private _ip As String = ""
    Private _puerto As Integer = 4370
    Private _commKey As Integer = 0
    Private _connected As Boolean = False
    Private ReadOnly _lock As New Object()
    Private _logsCache As List(Of Object) = New List(Of Object)()

    ' ===== Eventos del SDK: marcan el estado en tiempo real =====
    Private Sub _zk_OnConnected() Handles _zk.OnConnected
        _connected = True
    End Sub

    Private Sub _zk_OnDisConnected() Handles _zk.OnDisConnected
        _connected = False
    End Sub

    ' ===== API expuesta por COM =====
    Public Function Conectar(ip As String, puerto As Integer, Optional commKey As Integer = 0) As Boolean Implements IZkBridge.Conectar
        SyncLock _lock
            _ip = ip : _puerto = puerto : _commKey = commKey
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

    Private _machine As Integer = 1 ' el que uses/detectes

    Public Function CargarLogs() As Integer Implements IZkBridge.CargarLogs
        SyncLock _lock
            If Not EnsureConnected() Then Return 0

            Dim logs As New List(Of Object)()
            Dim m = _machine

            Try
                ' Si estás conviviendo con otro sistema, mejor NO deshabilitar el equipo:
                'Try : _zk.EnableDevice(m, False) : Catch : End Try

                ' 1) Conteo total (rápido). Si 0, salir.
                Dim total As Integer = -1
                Try : _zk.GetDeviceStatus(m, 6, total) : Catch : total = -1 : End Try
                If total = 0 Then
                    _logsCache = logs
                    Return 0
                End If

                ' 2) Preparar buffer: usar SSR "all" y, si no está, fallback a legacy
                Dim prepared As Boolean = False
                Try : prepared = _zk.ReadAllGLogData(m) : Catch : prepared = False : End Try
                If Not prepared Then
                    Try : prepared = _zk.ReadGeneralLogData(m) : Catch : prepared = False : End Try
                End If
                If Not prepared Then
                    _logsCache = logs
                    Return 0
                End If

                ' 3) Enumerar primero con SSR (string + seconds)
                Dim any As Boolean = False
                Try
                    Dim enroll As String = ""
                    Dim verify, inout, y, mm, d, h, nn, ss, workcode As Integer
                    While _zk.SSR_GetGeneralLogData(m, enroll, verify, inout, y, mm, d, h, nn, ss, workcode)
                        ' Dim fh As New DateTime(y, mm, d, h, nn, Math.Max(0, ss))
                        Dim fh As New DateTime(y, mm, d, h, nn, 0)

                        logs.Add(New With {
                            .id = If(enroll, ""),
                            .fechaHora = fh.ToString("yyyy-MM-dd HH:mm:ss"),
                            .verifyMode = verify,
                            .inOutMode = inout
                        })
                        any = True
                    End While
                Catch
                    any = False
                End Try

                ' 4) Si SSR no devolvió nada, fallback a legacy (sin seconds, id entero)
                If Not any Then
                    Dim tMachine, enrollInt, eMachine As Integer
                    Dim verify, inout As Integer
                    Dim y, mm, d, h, nn As Integer
                    While _zk.GetGeneralLogData(m, tMachine, enrollInt, eMachine, verify, inout, y, mm, d, h, nn)
                        Dim fh As New DateTime(y, mm, d, h, nn, 0)
                        logs.Add(New With {
                            .id = enrollInt.ToString(),
                            .fechaHora = fh.ToString("yyyy-MM-dd HH:mm:ss"),
                            .verifyMode = verify,
                            .inOutMode = inout
                        })
                    End While
                End If

            Finally
                ' Si deshabilitaste arriba, re-habilitá acá:
                'Try : _zk.EnableDevice(m, True) : Catch : End Try
                Try : _zk.RefreshData(m) : Catch : End Try
            End Try

            _logsCache = logs
            Return _logsCache.Count
        End SyncLock
    End Function

    Public Function ObtenerLogs(Optional pagina As Integer = 0) As String Implements IZkBridge.ObtenerLogs
        SyncLock _lock
            If _logsCache Is Nothing OrElse _logsCache.Count = 0 Then Return "[]"
            If pagina < 0 Then pagina = 0

            Dim start As Integer = pagina * PAGE_SIZE
            If start >= _logsCache.Count Then Return "[]"

            Dim count As Integer = Math.Min(PAGE_SIZE, _logsCache.Count - start)
            Dim slice = _logsCache.GetRange(start, count)

            Dim ser = New JavaScriptSerializer()
            Return ser.Serialize(slice)
        End SyncLock
    End Function

    Public Function TotalLogs() As Integer Implements IZkBridge.TotalLogs
        SyncLock _lock
            If _logsCache Is Nothing Then Return 0
            Return _logsCache.Count
        End SyncLock
    End Function

    Public Function PageSize() As Integer Implements IZkBridge.PageSize
        Return PAGE_SIZE
    End Function

    ' ===== Utilitarios internos =====

    ' Reintentos de conexión (3 intentos con backoff)
    Private Function TryConectar() As Boolean
        For i As Integer = 1 To 3
            Try
                W($"TryConectar intento {i} a {_ip}:{_puerto} con commKey={_commKey}")

                W($"Precheck TCP FAIL a {_ip}:{_puerto} (puerto cerrado/filtrado)")

                ' 1) Intento con la clave indicada
                Try
                    _zk.SetCommPassword(_commKey)
                    W("SetCommPassword OK")
                Catch ex As Exception
                    W($"SetCommPassword EX: {ex.Message}")
                End Try

                Dim ok As Boolean = _zk.Connect_Net(_ip, _puerto)
                If ok Then
                    W("Connect_Net OK (con clave)")
                    _connected = True
                    Return True
                Else
                    Dim errno As Integer = 0
                    Try : _zk.GetLastError(errno) : Catch : End Try
                    W($"Connect_Net FAIL (con clave), errno={errno}")
                End If

                ' 2) Si falló y la clave no era 0, probá SIN clave (dispositivos sin commkey)
                If _commKey <> 0 Then
                    Try
                        _zk.SetCommPassword(0)
                        W("Retry SetCommPassword(0) OK")
                    Catch ex As Exception
                        W($"Retry SetCommPassword(0) EX: {ex.Message}")
                    End Try

                    ok = _zk.Connect_Net(_ip, _puerto)
                    If ok Then
                        W("Connect_Net OK (sin clave, el equipo no usa comm key)")
                        _connected = True
                        Return True
                    Else
                        Dim errno2 As Integer = 0
                        Try : _zk.GetLastError(errno2) : Catch : End Try
                        W($"Connect_Net FAIL (sin clave), errno={errno2}")
                    End If
                End If
            Catch ex As Exception
                W($"TryConectar EX: {ex.Message}")
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
