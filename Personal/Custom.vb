Imports System.Windows.Forms
Imports System.Drawing

' Emula un mat-select con selección múltiple
Public Class MultiSelectDropdown
    Private popup As Form
    Private selectedItems As New HashSet(Of String)
    Private buttonTarget As Button
    Private ownerForm As Form
    Private mouseFilter As MouseClickMessageFilter
    Private listboxItems As List(Of String)
    Private arrowLabel As Label

    ' Evento público para notificar cambios en la selección
    Public Event SelectionChanged As EventHandler(Of SelectionChangedEventArgs)

    Public Sub New(button As Button, form As Form, items As IEnumerable(Of String))
        buttonTarget = button
        ownerForm = form
        listboxItems = items.ToList()

        ' Apariencia tipo ComboBox
        button.FlatStyle = FlatStyle.Flat
        button.BackColor = SystemColors.Window
        button.ForeColor = SystemColors.WindowText
        button.TextAlign = ContentAlignment.MiddleLeft
        button.Height = 23
        button.Cursor = Cursors.Hand

        With button.FlatAppearance
            .BorderSize = 1
            .BorderColor = Color.Gray
            .MouseDownBackColor = Color.White
            .MouseOverBackColor = Color.White
        End With

        ' Texto inicial
        button.Text = FormatButtonText()

        ' Flecha "▼" como overlay
        arrowLabel = New Label With {
            .Text = "▼",
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = Color.Transparent,
            .ForeColor = Color.Black,
            .Width = 18,
            .Height = button.Height - 4,
            .Top = button.Top + 2,
            .Left = button.Left + button.Width - 20,
            .Cursor = Cursors.Hand
        }

        form.Controls.Add(arrowLabel)
        arrowLabel.BringToFront()

        AddHandler arrowLabel.Click, Sub() button.PerformClick()
        AddHandler button.Click, AddressOf ShowPopup
        AddHandler form.Move, Sub() ClosePopup()
        AddHandler form.Resize, Sub() ClosePopup()

        AddHandler button.SizeChanged,
            Sub()
                arrowLabel.Left = button.Left + button.Width - 25
                arrowLabel.Top = button.Top
                arrowLabel.Height = button.Height
            End Sub

        AddHandler button.LocationChanged,
            Sub()
                arrowLabel.Left = button.Left + button.Width - 25
                arrowLabel.Top = button.Top
            End Sub
    End Sub

    Private Sub ShowPopup(sender As Object, e As EventArgs)
        ClosePopup()

        popup = New Form With {
            .FormBorderStyle = FormBorderStyle.None,
            .Size = New Size(buttonTarget.Width, 155),
            .StartPosition = FormStartPosition.Manual,
            .TopMost = True,
            .ShowInTaskbar = False,
            .ShowIcon = False,
            .ControlBox = False,
            .BackColor = Color.White
        }

        Dim screenPos As Point = buttonTarget.PointToScreen(New Point(0, buttonTarget.Height))
        popup.Location = screenPos

        Dim listbox As New CheckedListBox With {
            .Dock = DockStyle.Fill,
            .CheckOnClick = True,
            .BackColor = Color.White
        }

        For Each item In listboxItems
            listbox.Items.Add(item, selectedItems.Contains(item))
        Next

        AddHandler listbox.ItemCheck,
            Sub(sender2, e2)
                popup.BeginInvoke(
                    New MethodInvoker(
                        Sub() UpdateSelectedItems(DirectCast(sender2, CheckedListBox))
                    ))
            End Sub

        popup.Controls.Add(listbox)
        popup.Show()
        listbox.Focus()

        mouseFilter = New MouseClickMessageFilter(popup)
        AddHandler mouseFilter.ClickOutside, Sub() ClosePopup()
        Application.AddMessageFilter(mouseFilter)
    End Sub

    Public Sub ClosePopup()
        If mouseFilter IsNot Nothing Then
            Application.RemoveMessageFilter(mouseFilter)
            mouseFilter = Nothing
        End If
        If popup IsNot Nothing AndAlso Not popup.IsDisposed Then
            popup.Close()
            popup.Dispose()
            popup = Nothing
        End If
    End Sub

    Private Sub UpdateSelectedItems(listbox As CheckedListBox)
        selectedItems.Clear()
        For Each item In listbox.CheckedItems
            selectedItems.Add(item.ToString())
        Next

        ' Siempre se muestra en el botón
        buttonTarget.Text = FormatButtonText()

        ' Notificar a quien se suscriba
        RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(selectedItems.ToList()))
    End Sub

    Private Function FormatButtonText() As String
        Using g As Graphics = buttonTarget.CreateGraphics()
            Dim texto = String.Join(", ", selectedItems)
            Dim finalText = If(String.IsNullOrWhiteSpace(texto), " seleccionar ", texto)
            Dim availableWidth = buttonTarget.Width - 20 ' margen para la flecha

            While g.MeasureString(finalText, buttonTarget.Font).Width > availableWidth AndAlso finalText.Contains(", ")
                finalText = finalText.Substring(0, finalText.LastIndexOf(","c))
            End While

            If g.MeasureString(finalText, buttonTarget.Font).Width > availableWidth AndAlso finalText.Length > 3 Then
                finalText = finalText.Substring(0, Math.Max(0, finalText.Length - 3)) & "..."
            End If

            Return finalText
        End Using
    End Function

    Public Function GetSelectedItems() As List(Of String)
        Return selectedItems.ToList()
    End Function

    ' Detecta clic fuera del popup
    Private Class MouseClickMessageFilter
        Implements IMessageFilter

        Public Event ClickOutside As EventHandler
        Private ReadOnly popupRef As Form

        Public Sub New(popup As Form)
            popupRef = popup
        End Sub

        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            If m.Msg = &H201 Then ' WM_LBUTTONDOWN
                Dim pos = Control.MousePosition
                If popupRef Is Nothing OrElse popupRef.IsDisposed OrElse Not popupRef.Bounds.Contains(pos) Then
                    RaiseEvent ClickOutside(Me, EventArgs.Empty)
                End If
            End If
            Return False
        End Function
    End Class
End Class

' EventArgs para el evento SelectionChanged
Public Class SelectionChangedEventArgs
    Inherits EventArgs

    Public Sub New(selectedItems As List(Of String))
        Me.SelectedItems = selectedItems
    End Sub

    Public ReadOnly Property SelectedItems As List(Of String)
End Class



'-----------------------------------------------------------------------------------------------------------------
'-----------------------------------------------------------------------------------------------------------------
'-----------------------------------------------------------------------------------------------------------------
' Boton con un menú contextual 
Public Class MenuButton
    Private buttonTarget As Button
    Private contextMenu As ContextMenuStrip

    ''' <summary>
    ''' Agrega a un botón un menú contextual que se muestra al hacer clic, alineado a la derecha del botón.
    ''' El menú se construye a partir de un diccionario de elementos de menú y sus manejadores de eventos.
    ''' </summary>
    ''' <param name="button">El botón al que se adjunta el menú</param>
    ''' <param name="menuItems">Diccionario de textos y handlers para los ítems del menú</param>
    Public Sub New(button As Button, menuItems As Dictionary(Of String, EventHandler))
        buttonTarget = button
        contextMenu = New ContextMenuStrip()

        For Each kvp In menuItems
            contextMenu.Items.Add(kvp.Key, Nothing, kvp.Value)
        Next

        AddHandler button.Click,
            Sub()
                ' Forzar el cálculo del tamaño real del menú antes de mostrarlo
                contextMenu.Show(New Control(), New Point(-1000, -1000)) ' Mostrar fuera de pantalla
                contextMenu.Close() ' Cerrar inmediatamente

                ' Obtener el tamaño real ahora sí correcto
                Dim menuSize = contextMenu.GetPreferredSize(New Size(0, 0))
                Dim xOffset = button.Width - menuSize.Width
                contextMenu.Show(button, New Point(Math.Max(xOffset, 0), button.Height))
            End Sub
    End Sub
End Class

'---------------------------------------------------------
' selección única y búsqueda integrada
Public Class SingleSelectAutocompleteDropdown
    Private buttonTarget As Button
    Private ownerForm As Form
    Private arrowLabel As Label
    Private selectedItem As String = ""
    Private items As List(Of String)
    Private popup As Form
    Private listBox As ListBox
    Private searchBox As TextBox
    Private mouseFilter As IMessageFilter

    Public Event ItemSelected(value As String)

    Public Sub New(button As Button, form As Form, itemsSource As IEnumerable(Of String))
        buttonTarget = button
        ownerForm = form
        items = itemsSource.ToList()

        ' Estilo visual del botón
        button.FlatStyle = FlatStyle.Standard
        button.BackColor = SystemColors.Window
        button.ForeColor = SystemColors.WindowText
        button.TextAlign = ContentAlignment.MiddleLeft
        button.Cursor = Cursors.Hand
        button.Text = "Seleccionar..."

        ' Flecha ▼
        arrowLabel = New Label With {
          .Text = "▼",
          .AutoSize = False,
          .TextAlign = ContentAlignment.MiddleCenter,
          .BackColor = Color.Transparent,
          .ForeColor = Color.Black,
          .Width = 20,
          .Height = button.Height - 4,
          .Top = button.Top + 2,
          .Left = button.Left + button.Width - 25,
          .Cursor = Cursors.Hand
        }
        form.Controls.Add(arrowLabel)
        arrowLabel.BringToFront()

        AddHandler arrowLabel.Click, Sub() button.PerformClick()
        AddHandler button.Click, AddressOf ShowPopup
        AddHandler form.Move, Sub() ClosePopup()
        AddHandler form.Resize, Sub() ClosePopup()
        AddHandler button.SizeChanged, Sub()
                                           arrowLabel.Left = button.Left + button.Width - 25
                                           arrowLabel.Top = button.Top + 2
                                           arrowLabel.Height = button.Height - 4
                                       End Sub
        AddHandler button.LocationChanged, Sub()
                                               arrowLabel.Left = button.Left + button.Width - 25
                                               arrowLabel.Top = button.Top + 2
                                           End Sub
    End Sub

    Private Sub ShowPopup(sender As Object, e As EventArgs)
        ClosePopup()

        popup = New Form With {
          .FormBorderStyle = FormBorderStyle.None,
          .Size = New Size(buttonTarget.Width, 200),
          .StartPosition = FormStartPosition.Manual,
          .TopMost = True,
          .ShowInTaskbar = False,
          .ShowIcon = False,
          .ControlBox = False,
          .BackColor = Color.White
        }

        Dim screenPos As Point = buttonTarget.PointToScreen(New Point(0, buttonTarget.Height))
        popup.Location = screenPos

        ' Search box
        searchBox = New TextBox With {.Dock = DockStyle.Top, .PlaceholderText = "Buscar..."}
        AddHandler searchBox.TextChanged, AddressOf OnSearchTextChanged

        ' Listbox con opciones
        listBox = New ListBox With {.Dock = DockStyle.Fill, .BackColor = Color.White}
        listBox.Items.AddRange(items.ToArray())
        AddHandler listBox.Click, AddressOf OnItemClicked

        popup.Controls.Add(listBox)
        popup.Controls.Add(searchBox)
        popup.Show()
        searchBox.Focus()

        mouseFilter = New PopupMouseFilter(popup)
        AddHandler CType(mouseFilter, PopupMouseFilter).ClickOutside, Sub() ClosePopup()
        Application.AddMessageFilter(mouseFilter)
    End Sub

    Private Sub OnSearchTextChanged(sender As Object, e As EventArgs)
        listBox.BeginUpdate()
        listBox.Items.Clear()
        listBox.Items.AddRange(items.Where(Function(i) i.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToArray())
        listBox.EndUpdate()
    End Sub

    Private Sub OnItemClicked(sender As Object, e As EventArgs)
        If listBox.SelectedItem IsNot Nothing Then
            selectedItem = listBox.SelectedItem.ToString()
            buttonTarget.Text = selectedItem
            RaiseEvent ItemSelected(selectedItem)
        End If
        ClosePopup()
    End Sub

    Public Function GetSelectedItem() As String
        Return selectedItem
    End Function

    Private Sub ClosePopup()
        If popup IsNot Nothing AndAlso Not popup.IsDisposed Then popup.Close()
        If mouseFilter IsNot Nothing Then Application.RemoveMessageFilter(mouseFilter)
    End Sub

    Private Class PopupMouseFilter
        Implements IMessageFilter
        Public Event ClickOutside As EventHandler
        Private popupBounds As Rectangle

        Public Sub New(popup As Form)
            popupBounds = popup.Bounds
        End Sub

        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            If m.Msg = &H201 Then ' WM_LBUTTONDOWN
                Dim pos = Control.MousePosition
                If Not popupBounds.Contains(pos) Then RaiseEvent ClickOutside(Me, EventArgs.Empty)
            End If
            Return False
        End Function
    End Class
End Class