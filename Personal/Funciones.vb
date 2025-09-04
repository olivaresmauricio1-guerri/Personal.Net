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

    Public Sub CmdShow(sql As String, parametros As IEnumerable(Of Object))
        MessageBox.Show(sql & " " & String.Join(", ", parametros.Select(Function(p) If(p IsNot Nothing, p.ToString(), "NULL")).ToArray()))
    End Sub

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


    Public Function DataRowToDictionary(row As DataRow) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)
        For Each col As DataColumn In row.Table.Columns
            dict(col.ColumnName) = row(col)
        Next
        Return dict
    End Function

    Public Function DataTableToListOfDictionary(dt As DataTable) As List(Of Dictionary(Of String, Object))
        Dim lista As New List(Of Dictionary(Of String, Object))
        For Each row As DataRow In dt.Rows
            lista.Add(DataRowToDictionary(row))
        Next
        Return lista
    End Function

    Public Function DataGridViewRowToDictionary(row As DataGridViewRow) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)
        For Each cell As DataGridViewCell In row.Cells
            dict(row.DataGridView.Columns(cell.ColumnIndex).Name) = cell.Value
        Next
        Return dict
    End Function

    Public Function DataGridViewToListOfDictionary(dgv As DataGridView) As List(Of Dictionary(Of String, Object))
        Dim lista As New List(Of Dictionary(Of String, Object))
        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then ' Ignora la fila vacía para nuevas entradas
                lista.Add(DataGridViewRowToDictionary(row))
            End If
        Next
        Return lista
    End Function

    Public Function RedimensionarImagen(original As Image, ancho As Integer, alto As Integer) As Image
        Dim bmp As New Bitmap(ancho, alto)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.Clear(Color.Transparent)
            g.DrawImage(original, 0, 0, ancho, alto)
        End Using
        Return bmp
    End Function

    Public Function InvertirColores(imagen As Image) As Bitmap
        Dim bmp As New Bitmap(imagen)
        For x = 0 To bmp.Width - 1
            For y = 0 To bmp.Height - 1
                Dim c = bmp.GetPixel(x, y)
                Dim invertido = Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B)
                bmp.SetPixel(x, y, invertido)
            Next
        Next
        Return bmp
    End Function

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

    ' ====== PUBLIC: usar esto donde mostrás la consulta ======
    Public Sub ApplySqlFormatting(rtb As RichTextBox, sql As String)
        Dim formatted = PrettySql(sql)

        ' Fuente base monoespaciada
        Dim baseFont As New Font("Consolas", 10.0F, FontStyle.Regular)

        ' Congelar repintado para que no parpadee
        ' SuspendRedraw(rtb)
        rtb.SuspendLayout()

        rtb.Clear()
        rtb.Font = baseFont
        rtb.Text = formatted

        ' Reset estilo base
        rtb.SelectAll()
        rtb.SelectionColor = Color.Black
        rtb.SelectionFont = baseFont

        ' ====== Resaltado por regex ======
        ' Comentarios
        Colorize(rtb, New Regex("--.*$", RegexOptions.Multiline),
                 Color.ForestGreen, FontStyle.Italic, baseFont)
        Colorize(rtb, New Regex("/\*.*?\*/", RegexOptions.Singleline),
                 Color.ForestGreen, FontStyle.Italic, baseFont)

        ' Strings '...'
        Colorize(rtb, New Regex("'(''|[^'])*'"),
                 Color.SaddleBrown, FontStyle.Regular, baseFont)

        ' Palabras clave (negrita + azul)
        Dim keywords = "\b(SELECT|UPDATE|INSERT|INTO|DELETE|FROM|WHERE|SET|VALUES|JOIN|INNER|LEFT|RIGHT|FULL|OUTER|ON|GROUP|BY|ORDER|HAVING|TOP|DISTINCT|AS|AND|OR|NOT|NULL|IS|LIKE|IN|BETWEEN|EXISTS|CASE|WHEN|THEN|ELSE|END)\b"
        Colorize(rtb, New Regex(keywords, RegexOptions.IgnoreCase),
                 Color.RoyalBlue, FontStyle.Bold, baseFont)

        ' Funciones (azul + itálica)
        Dim functions = "\b(AVG|COUNT|MIN|MAX|SUM|ABS|ROUND|LEN|SUBSTRING|DATEDIFF|DATEADD|GETDATE|COALESCE|ISNULL|CONVERT|CAST|UPPER|LOWER|FORMAT)\b"
        Colorize(rtb, New Regex(functions, RegexOptions.IgnoreCase),
                 Color.RoyalBlue, FontStyle.Italic, baseFont)

        ' Números
        Colorize(rtb, New Regex("\b\d+(\.\d+)?\b"),
                 Color.MediumVioletRed, FontStyle.Regular, baseFont)

        ' Fin
        rtb.SelectionLength = 0
        rtb.SelectionStart = 0
        rtb.ResumeLayout()
        ' ResumeRedraw(rtb)
        rtb.Invalidate()
    End Sub

    ' ====== PRETTY PRINT ======
    Private Function PrettySql(sql As String) As String
        If String.IsNullOrWhiteSpace(sql) Then Return String.Empty

        Dim s = sql.Trim()

        ' Normalizar CR/LF y tabs a espacios
        s = s.Replace(vbCr, "").Replace(vbLf, " ").Replace(vbTab, " ")

        ' Poner en mayúscula algunos tokens sin tocar strings (simple, rápido)
        ' Luego insertamos saltos de línea
        Dim tokens As String() = {
            "SELECT", "UPDATE", "INSERT INTO", "DELETE",
            "FROM", "WHERE", "SET", "VALUES",
            "JOIN", "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "FULL OUTER JOIN",
            "GROUP BY", "ORDER BY", "HAVING", "ON"
        }

        For Each t In tokens.OrderByDescending(Function(x) x.Length)
            s = Regex.Replace(s, "\b" & Regex.Escape(t) & "\b", t,
                              RegexOptions.IgnoreCase)
        Next

        ' Saltos de línea antes de bloques conocidos
        Dim breakers As String() = {
            "SELECT", "UPDATE", "INSERT INTO", "DELETE",
            "FROM", "WHERE", "SET", "VALUES",
            "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "FULL OUTER JOIN", "JOIN",
            "GROUP BY", "ORDER BY", "HAVING", "ON"
        }
        For Each b In breakers.OrderByDescending(Function(x) x.Length)
            s = Regex.Replace(s, "\s+" & Regex.Escape(b) & "\s+", vbCrLf & b & " ",
                              RegexOptions.IgnoreCase)
            ' Inicio de texto
            s = Regex.Replace(s, "^" & Regex.Escape(b) & "\s+", b & " ",
                              RegexOptions.IgnoreCase)
        Next

        ' Una línea por cada columna en SET y SELECT (básico)
        s = Regex.Replace(s, "\s*,\s*", "," & vbCrLf & "    ")

        ' Limpieza de espacios y saltos repetidos
        s = Regex.Replace(s, " +", " ")
        s = Regex.Replace(s, "(\r\n){2,}", vbCrLf)

        Return s.Trim()
    End Function

    ' ====== HERRAMIENTAS DE RESALTADO ======
    Private Sub Colorize(rtb As RichTextBox, re As Regex, color As Color, style As FontStyle, baseFont As Font)
        For Each m As Match In re.Matches(rtb.Text)
            rtb.Select(m.Index, m.Length)
            rtb.SelectionColor = color
            rtb.SelectionFont = New Font(baseFont, style)
        Next
    End Sub

    ' ====== EVITAR PARPADEO ======
    ' (Opcional, pero mejora la UX al colorear mucho texto)
    '<DllImport("user32.dll")>
    'Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, lParam As Integer) As IntPtr
    'End Function
    'Private Const WM_SETREDRAW As Integer = &HB
    'Private Sub SuspendRedraw(ctrl As Control)
    '    SendMessage(ctrl.Handle, WM_SETREDRAW, 0, 0)
    'End Sub
    'Private Sub ResumeRedraw(ctrl As Control)
    '    SendMessage(ctrl.Handle, WM_SETREDRAW, 1, 0)
    'End Sub

    Public Sub AsegurarRegistroZkBridge()

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
        Funciones.AsegurarRegistroZkBridge()
        Dim t = Type.GetTypeFromProgID("ZkBridge.Attendance")
        Dim zk = Activator.CreateInstance(t)
        Return zk
    End Function


End Module
