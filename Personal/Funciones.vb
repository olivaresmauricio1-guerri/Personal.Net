Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.DataVisualization.Charting

Imports System.Diagnostics

Public Module Funciones

    Public Function CmdParams(ParamArray values() As Object) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)

        For i As Integer = 0 To values.Length - 2 Step 2
            Dim key = values(i).ToString()
            Dim val = values(i + 1)
            dict(key) = val
        Next

        Return dict
    End Function

    Public Sub CopiarDataGrid(grid As DataGridView, Optional incluirEncabezados As Boolean = True)
        If grid Is Nothing Then Exit Sub

        Dim dataObj As DataObject = Nothing

        ' Guardamos configuración actual
        Dim modoOriginal = grid.ClipboardCopyMode
        grid.ClipboardCopyMode = If(incluirEncabezados,
                                DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText,
                                DataGridViewClipboardCopyMode.EnableWithoutHeaderText)

        If grid.SelectedCells.Count > 0 Then
            ' Obtener el rango mínimo y máximo
            Dim selectedCells = grid.SelectedCells.Cast(Of DataGridViewCell)().OrderBy(Function(c) c.RowIndex).ThenBy(Function(c) c.ColumnIndex).ToList()

            Dim minRow = selectedCells.Min(Function(c) c.RowIndex)
            Dim maxRow = selectedCells.Max(Function(c) c.RowIndex)
            Dim minCol = selectedCells.Min(Function(c) c.ColumnIndex)
            Dim maxCol = selectedCells.Max(Function(c) c.ColumnIndex)

            Dim sb As New System.Text.StringBuilder()

            ' Encabezados si se pidió
            If incluirEncabezados Then
                For col = minCol To maxCol
                    If grid.Columns(col).Visible Then
                        sb.Append(grid.Columns(col).HeaderText)
                        If col < maxCol Then sb.Append(vbTab)
                    End If
                Next
                sb.AppendLine()
            End If

            ' Filas de datos
            For row = minRow To maxRow
                For col = minCol To maxCol
                    If grid.Columns(col).Visible Then
                        Dim cell = grid.Rows(row).Cells(col)
                        If cell.Selected Then
                            sb.Append(cell.Value?.ToString())
                        End If
                        If col < maxCol Then sb.Append(vbTab)
                    End If
                Next
                sb.AppendLine()
            Next

            dataObj = New DataObject()
            dataObj.SetText(sb.ToString())
        Else
            ' Nada seleccionado, copiar todo
            grid.SelectAll()
            dataObj = grid.GetClipboardContent()
            grid.ClearSelection()
        End If

        If dataObj IsNot Nothing Then
            Clipboard.SetDataObject(dataObj)
        End If

        ' Restaurar configuración
        grid.ClipboardCopyMode = modoOriginal
    End Sub

    Public Sub SetControlesEnabled(estado As Boolean, ParamArray controles() As Control)
        For Each ctrl In controles
            ctrl.Enabled = estado
            ' si es textbox cambiar back color, y si es dropdown tambien pero con la propiedad correcta
            ctrl.BackColor = SystemColors.Window
        Next
    End Sub

    Public Sub ConfigurarEstiloGrid(dgv As DataGridView)
        dgv.RowHeadersWidth = 20

        ' dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToResizeColumns = False
        dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgv.AllowUserToResizeRows = False
        ' dgv.RowHeadersVisible = False

        ' Fuente común para todas las celdas
        Dim fuenteCeldas As New Font("Segoe UI", 8, FontStyle.Regular)

        ' Estilo por defecto (filas impares - fondo blanco)
        dgv.DefaultCellStyle.BackColor = Color.White
        dgv.DefaultCellStyle.Font = fuenteCeldas
        dgv.DefaultCellStyle.ForeColor = Color.Black
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.SteelBlue) 'FromArgb(100, 149, 237) ' Azul para selección
        dgv.DefaultCellStyle.SelectionForeColor = Color.White

        ' Estilo alternativo (filas pares - gris muy claro)
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.FloralWhite)
        dgv.AlternatingRowsDefaultCellStyle.Font = fuenteCeldas
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.SteelBlue)
        dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White

        ' Estilo del encabezado de columnas
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub


    ' registra ZkBridge si no está registrado
    Public Function AsegurarRegistroZkBridge()
        Try
            If (Type.GetTypeFromProgID("ZkBridge.Attendance", throwOnError:=False) Is Nothing) Then
                Funciones._AsegurarRegistroZkBridge()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Registro COM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return False
    End Function

    Public Sub _AsegurarRegistroZkBridge()

        ' ¿Ya está registrado?
        Dim t = Type.GetTypeFromProgID("ZkBridge.Attendance", throwOnError:=False)
        If t IsNot Nothing Then Exit Sub

        ' Ejecutar el registrador con UAC
        Dim script = IO.Path.Combine(AppContext.BaseDirectory, "register-zkbridge.cmd")
        If Not IO.File.Exists(script) Then
            Throw New Exception("Falta register-zkbridge.cmd en la carpeta de la app.")
        End If

        Dim psi As New ProcessStartInfo(script) With {
        .UseShellExecute = True,
        .Verb = "runas" ' fuerza UAC
    }
        Dim p = Process.Start(psi)
        p.WaitForExit()

        ' Reintenta resolver el ProgID
        t = Type.GetTypeFromProgID("ZkBridge.Attendance", throwOnError:=False)
        If t Is Nothing Then
            Throw New Exception("No se pudo registrar ZkBridge (necesita permisos de admin).")
        End If
    End Sub

    Public Function CrearZkBridge() As Object
        Funciones._AsegurarRegistroZkBridge()
        Dim t = Type.GetTypeFromProgID("ZkBridge.Attendance")
        Dim zk = Activator.CreateInstance(t)
        Return zk
    End Function


End Module
