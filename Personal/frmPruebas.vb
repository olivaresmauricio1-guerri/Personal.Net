Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmPruebas
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' ImportarAgentes()
        ' ImportarCaracter()
        ' ImportarCategorias()
        ' ImportarComentarios()
        ImportarConsultoras()
        ' ImportarMovimientos()
        ' ImportarHoras()
    End Sub

    ' importar datos de la tabla Agentes desde la base de datos Personal_ a la base de datos Personal
    Private Sub ImportarAgentes()

        ' traer datos de la tabla Agentes desde Personal_
        Dim sql = "SELECT * FROM [Agentes]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Agentes en Personal
        Dim sqlDelete = "DELETE FROM Agentes"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Agentes en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
                INSERT INTO Agentes (
                    Legajo, TipoDto, NroDto, CargoPampa, Cargo, Instituto, Nombre, CorreoE, Sexo, Nacimiento, Calle, Nro, Localidad, Oficina,
                    Critico, MayorDedicacion, HorasDedicacion, HorasSemanales, HorasDiarias, Escalafon, Jefe, LicAnual, Caracter, Comentario, 
                    Nomarca, Secretaria, Vac2022, Vac2023, Vac2024, Vac2025, Vac2026, Vac2027, Vac2028, Vac2014, Vac2015, Vac2016, Vac2017, 
                    Vac2018, Vac2019, Vac2020, Vac2021, MarcaAqui, Telefono, Interno, Celular, Rpv, iNGRESO, Baja, CUIL, TITULO, 
                    UltimaActualizacion, Motivo, EstadoParental, FechaJubilacion
                ) VALUES (
                    @Legajo, @TipoDto, @NroDto, @CargoPampa, @Cargo, @Instituto, @Nombre, @CorreoE, @Sexo, @Nacimiento, @Calle, @Nro, @Localidad, @Oficina,
                    @Critico, @MayorDedicacion, @HorasDedicacion, @HorasSemanales, @HorasDiarias, @Escalafon, @Jefe, @LicAnual, @Caracter, @Comentario,
                    @Nomarca, @Secretaria, @Vac2022, @Vac2023, @Vac2024, @Vac2025, @Vac2026, @Vac2027, @Vac2028, @Vac2014, @Vac2015, @Vac2016, @Vac2017,
                    @Vac2018, @Vac2019, @Vac2020, @Vac2021, @MarcaAqui, @Telefono, @Interno, @Celular, @Rpv, @iNGRESO, @Baja, @CUIL, @TITULO,
                    @UltimaActualizacion, @Motivo, @EstadoParental, @FechaJubilacion
                )"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Legajo", row("Legajo")},
                {"@TipoDto", row("TipoDto")},
                {"@NroDto", row("NroDto")},
                {"@CargoPampa", row("CargoPampa")},
                {"@Cargo", row("Cargo")},
                {"@Instituto", row("Instituto")},
                {"@Nombre", row("Nombre")},
                {"@CorreoE", row("CorreroE")},
                {"@Sexo", row("Sexo")},
                {"@Nacimiento", row("Nacimiento")},
                {"@Calle", row("Calle")},
                {"@Nro", row("Nro")},
                {"@Localidad", row("Localidad")},
                {"@Oficina", row("Oficina")},
                {"@Critico", row("Critico")},
                {"@MayorDedicacion", row("MayorDedicacion")},
                {"@HorasDedicacion", row("HorasDedicacion")},
                {"@HorasSemanales", row("HorasSemanales")},
                {"@HorasDiarias", row("HorasDiarias")},
                {"@Escalafon", row("Escalafon")},
                {"@Jefe", row("Jefe")},
                {"@LicAnual", row("LicAnual")},
                {"@Caracter", row("Caracter")},
                {"@Comentario", row("Comentario")},
                {"@Nomarca", row("Nomarca")},
                {"@Secretaria", row("Secretaria")},
                {"@Vac2022", row("Vac2022")},
                {"@Vac2023", row("Vac2023")},
                {"@Vac2024", row("Vac2024")},
                {"@Vac2025", row("Vac2025")},
                {"@Vac2026", row("Vac2026")},
                {"@Vac2027", row("Vac2027")},
                {"@Vac2028", row("Vac2028")},
                {"@Vac2014", row("Vac2014")},
                {"@Vac2015", row("Vac2015")},
                {"@Vac2016", row("Vac2016")},
                {"@Vac2017", row("Vac2017")},
                {"@Vac2018", row("Vac2018")},
                {"@Vac2019", row("Vac2019")},
                {"@Vac2020", row("Vac2020")},
                {"@Vac2021", row("Vac2021")},
                {"@MarcaAqui", row("MarcaAqui")},
                {"@Telefono", row("Telefono")},
                {"@Interno", row("Interno")},
                {"@Celular", row("Celular")},
                {"@Rpv", row("Rpv")},
                {"@iNGRESO", row("iNGRESO")},
                {"@Baja", row("Baja")},
                {"@CUIL", row("CUIL")},
                {"@TITULO", row("TITULO")},
                {"@UltimaActualizacion", row("UltimaActualizacion")},
                {"@Motivo", row("Motivo")},
                {"@EstadoParental", row("EstadoParental")},
                {"@FechaJubilacion", row("FechaJubilacion")}
            }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarCaracter()

        ' traer datos de la tabla Caracter desde Personal_
        Dim sql = "SELECT * FROM [Caracter]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Caracter en Personal
        Dim sqlDelete = "DELETE FROM Caracter"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Caracter en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "INSERT INTO Caracter (Codigo, Descripcion) VALUES (@Codigo, @Descripcion)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@Codigo", row("Codigo")},
            {"@Descripcion", row("Descripcion")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarCategorias()

        ' traer datos de la tabla Categorias desde Personal_
        Dim sql = "SELECT Orden, Escala1, Escala2, Descripcion FROM [Categorias]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Categorias en Personal
        Dim sqlDelete = "DELETE FROM Categorias"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Categorias en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Categorias (Orden, Escala1, Escala2, Descripcion) 
            VALUES (@Orden, @Escala1, @Escala2, @Descripcion)"
            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Orden", row("Orden")},
                {"@Escala1", row("Escala1")},
                {"@Escala2", row("Escala2")},
                {"@Descripcion", row("Descripcion")}
            }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarComentarios()

        ' traer datos de la tabla Comentarios desde Personal_
        Dim sql = "SELECT Legajo, Fecha, Comenta, Motivo FROM [Comentarios]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Comentarios en Personal
        Dim sqlDelete = "DELETE FROM Comentarios"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Comentarios en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Comentarios (Legajo, Fecha, Comenta, Motivo) 
            VALUES (@Legajo, @Fecha, @Comenta, @Motivo)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@Legajo", row("Legajo")},
            {"@Fecha", row("Fecha")},
            {"@Comenta", row("Comenta")},
            {"@Motivo", row("Motivo")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarConsultoras()

        ' traer datos de la tabla Consultoras desde Personal_
        Dim sql = "SELECT idConsultora, Nombre FROM [Consultoras]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Consultoras en Personal
        Dim sqlDelete = "DELETE FROM Consultoras"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Consultoras en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Consultoras (idConsultora, Nombre) 
            VALUES (@idConsultora, @Nombre)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@idConsultora", row("idConsultora")},
            {"@Nombre", row("Nombre")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    ' importar datos de la tabla Movimientos desde la base de datos Personal_ a la base de datos Personal
    Private Sub ImportarMovimientos()

        ' traer datos de la tabla Movimiento desde Personal_
        Dim sql = "SELECT * FROM [Movimiento]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Movimientos en Personal
        Dim sqlDelete = "DELETE FROM Movimiento"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Movimientos en Personal
        ' campos: [Id],[Legajo],[Instituto],[Dia],[Entro],[Salio],[HsCumplidas],[MotivoInasistencia],[Comentario],[SinFicha],[Autorizo],[NoPromedia],[Nopromedianada]
        For Each row As DataRow In dt.Rows
            Dim legajo = row("Legajo")
            Try
                Dim instituto = row("Instituto")
                Dim dia = row("Dia")
                Dim dtDia As DateTime = Convert.ToDateTime(dia)


                Dim entro = row("Entro")
                If Not IsDBNull(entro) Then
                    Dim dtEntro As DateTime = Convert.ToDateTime(entro)
                    entro = New DateTime(dtDia.Year, dtDia.Month, dtDia.Day, dtEntro.Hour, dtEntro.Minute, 0)
                End If

                Dim salio = row("Salio")
                If Not IsDBNull(salio) Then
                    Dim dtSalio As DateTime = Convert.ToDateTime(salio)
                    salio = New DateTime(dtDia.Year, dtDia.Month, dtDia.Day, dtSalio.Hour, dtSalio.Minute, 0)
                End If

                Dim hsCumplidas = row("HsCumplidas")
                Dim motivoInasistencia = row("MotivoInasistencia")
                Dim comentario = row("Comentario")
                Dim sinFicha = row("SinFicha")
                Dim autorizo = row("Autorizo")
                Dim noPromedia = row("NoPromedia")
                Dim noPromediaNada = row("Nopromedianada")

                Dim sqlInsert = "INSERT INTO Movimiento (Legajo, Instituto, Dia, Entro, Salio, HsCumplidas, MotivoInasistencia, Comentario, SinFicha, Autorizo, NoPromedia, Nopromedianada) " &
                                "VALUES (@Legajo, @Instituto, @Dia, @Entro, @Salio, @HsCumplidas, @MotivoInasistencia, @Comentario, @SinFicha, @Autorizo, @NoPromedia, @Nopromedianada)"

                Dim parametros = New Dictionary(Of String, Object) From {
                    {"@Legajo", legajo},
                    {"@Instituto", instituto},
                    {"@Dia", dia},
                    {"@Entro", entro},
                    {"@Salio", salio},
                    {"@HsCumplidas", hsCumplidas},
                    {"@MotivoInasistencia", motivoInasistencia},
                    {"@Comentario", comentario},
                    {"@SinFicha", sinFicha},
                    {"@Autorizo", autorizo},
                    {"@NoPromedia", noPromedia},
                    {"@Nopromedianada", noPromediaNada}
                }
                DSM.Execute(DSM.Personal, sqlInsert, parametros)
                Debug.WriteLine("Insertado movimiento Legajo: " & legajo)
            Catch ex As Exception
                Debug.WriteLine("Error al insertar movimiento Legajo: " & legajo & ex.Message)
            End Try
        Next
    End Sub

    ' Importar datos de la tabla Horas desde la base de datos Personal_ a la base de datos Personal
    Private Sub ImportarHoras()

        ' traer datos de la tabla Horas desde Personal_
        Dim sql = "SELECT * FROM [Horas]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Horas en Personal
        Dim sqlDelete = "DELETE FROM Horas"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Horas en Personal
        For Each row As DataRow In dt.Rows
            Dim horas = row("Horas")
            Dim sqlInsert = "INSERT INTO Horas (Horas) VALUES (@Horas)"
            Dim parametros = New Dictionary(Of String, Object) From {
                {"@Horas", horas}
            }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub
End Class