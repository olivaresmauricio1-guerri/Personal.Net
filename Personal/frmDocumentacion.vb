Imports System.IO
Imports System.Diagnostics

Public Class frmDocumentacion


    Private _legajo As String = ""
    Private _rutaDocumentos As String = "\\servernt\D\Sistema\Imagenes\"

    Public Property Legajo As String
        Get
            Return _legajo
        End Get
        Set(value As String)
            _legajo = value
            Me.Text = $"Documentación - Legajo {_legajo}"
            CargarArchivosExistentes()
        End Set
    End Property

    Private Sub frmDocumentacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarFormulario()
        rbCurriculum.Checked = True ' Seleccionar por defecto
    End Sub

    Private Sub btnSubirArchivo_Click(sender As Object, e As EventArgs) Handles btnSubirArchivo.Click
        SubirArchivoPDF()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        EliminarArchivo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub lstArchivos_DoubleClick(sender As Object, e As EventArgs) Handles lstArchivos.DoubleClick
        AbrirArchivo()
    End Sub

    Private Sub ConfigurarFormulario()
        ' Configurar posición del formulario
        Me.StartPosition = FormStartPosition.CenterParent
        
        ' Configurar OpenFileDialog
        openFileDialog.Filter = "Archivos PDF|*.pdf"
        openFileDialog.Title = "Seleccionar archivo PDF"
        openFileDialog.Multiselect = False
    End Sub

    Private Sub CargarArchivosExistentes()
        If String.IsNullOrEmpty(_legajo) Then Return

        lstArchivos.Items.Clear()

        Try
            ' Verificar si existe el directorio
            If Not Directory.Exists(_rutaDocumentos) Then
                MessageBox.Show("No se puede acceder al directorio de documentos.", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Buscar archivos del legajo actual
            Dim patron As String = $"*{_legajo}*.pdf"
            Dim archivos As String() = Directory.GetFiles(_rutaDocumentos, patron)

            For Each archivo As String In archivos
                Dim nombreArchivo As String = Path.GetFileName(archivo)
                lstArchivos.Items.Add(nombreArchivo)
            Next

        Catch ex As Exception
            MessageBox.Show($"Error al cargar archivos: {ex.Message}", "Error", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SubirArchivoPDF()
        ' Validar que se haya seleccionado un tipo de archivo
        If Not (rbCurriculum.Checked Or rbComprobanteAlta.Checked Or rbOtrosDatos.Checked) Then
            MessageBox.Show("Seleccione el tipo de archivo a importar.", "Validación", 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validar que se haya especificado un legajo
        If String.IsNullOrEmpty(_legajo) Then
            MessageBox.Show("No se ha especificado un legajo.", "Error", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Mostrar diálogo para seleccionar archivo
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim archivoOrigen As String = openFileDialog.FileName
                
                ' Determinar el prefijo según el tipo seleccionado
                Dim prefijo As String = ""
                If rbCurriculum.Checked Then
                    prefijo = "CUR_"
                ElseIf rbComprobanteAlta.Checked Then
                    prefijo = "ALT_"
                ElseIf rbOtrosDatos.Checked Then
                    prefijo = "Otro_"
                End If

                ' Crear nombre del archivo destino
                Dim nombreDestino As String = $"{prefijo}{_legajo}.pdf"
                Dim rutaDestino As String = Path.Combine(_rutaDocumentos, nombreDestino)

                ' Verificar si existe el directorio destino
                If Not Directory.Exists(_rutaDocumentos) Then
                    Directory.CreateDirectory(_rutaDocumentos)
                End If

                ' Verificar si el archivo ya existe
                If File.Exists(rutaDestino) Then
                    Dim resultado As DialogResult = MessageBox.Show(
                        $"Ya existe un archivo con el nombre {nombreDestino}. ¿Desea reemplazarlo?",
                        "Archivo existente",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question)
                    
                    If resultado = DialogResult.No Then
                        Return
                    End If
                End If

                ' Copiar el archivo
                File.Copy(archivoOrigen, rutaDestino, True)

                MessageBox.Show("Archivo subido correctamente.", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Actualizar la lista de archivos
                CargarArchivosExistentes()

            Catch ex As Exception
                MessageBox.Show($"Error al subir el archivo: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub AbrirArchivo()
        If lstArchivos.SelectedItem Is Nothing Then
            MessageBox.Show("Seleccione un archivo para abrir.", "Validación", 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim nombreArchivo As String = lstArchivos.SelectedItem.ToString()
            Dim rutaCompleta As String = Path.Combine(_rutaDocumentos, nombreArchivo)

            If File.Exists(rutaCompleta) Then
                ' Abrir el archivo con la aplicación predeterminada
                Process.Start(New ProcessStartInfo() With {
                    .FileName = rutaCompleta,
                    .UseShellExecute = True
                })
            Else
                MessageBox.Show("El archivo no existe.", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                CargarArchivosExistentes() ' Actualizar la lista
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EliminarArchivo()
        If lstArchivos.SelectedItem Is Nothing Then
            MessageBox.Show("Seleccione un archivo para eliminar.", "Validación", 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim nombreArchivo As String = lstArchivos.SelectedItem.ToString()
            Dim rutaCompleta As String = Path.Combine(_rutaDocumentos, nombreArchivo)

            Dim resultado As DialogResult = MessageBox.Show(
                $"¿Está seguro que quiere eliminar el archivo {nombreArchivo}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If resultado = DialogResult.Yes Then
                If File.Exists(rutaCompleta) Then
                    File.Delete(rutaCompleta)
                    MessageBox.Show("Archivo eliminado correctamente.", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarArchivosExistentes() ' Actualizar la lista
                Else
                    MessageBox.Show("El archivo no existe.", "Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error)
                    CargarArchivosExistentes() ' Actualizar la lista
                End If
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al eliminar el archivo: {ex.Message}", "Error", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class