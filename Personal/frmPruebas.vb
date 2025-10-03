Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmPruebas
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ImportarAgentes()
        ImportarCaracter()
        ImportarCategorias()
        ImportarComentarios()
        ImportarConsultoras()
        ImportarEncargados()
        ImportarEscalafon()
        ImportarEstadoParental()
        ImportarEventuales()
        ImportarExpediente()
        ImportarGrupoFamiliar()
        ImportarHoras()
        ImportarInasistencias()
        ImportarInstitutos()
        ImportarMeses()
        ImportarMinutos()
        ImportarMovimientos()
        ImportarParametros()
    End Sub

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

    Private Sub ImportarEncargados()

        ' traer datos de la tabla Encargados desde Personal_
        Dim sql = "SELECT Instituto, Encargado, Oficina, Telefono FROM [Encargados]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Encargados en Personal
        Dim sqlDelete = "DELETE FROM Encargados"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Encargados en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Encargados (Instituto, Encargado, Oficina, Telefono) 
            VALUES (@Instituto, @Encargado, @Oficina, @Telefono)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@Instituto", row("Instituto")},
            {"@Encargado", row("Encargado")},
            {"@Oficina", row("Oficina")},
            {"@Telefono", row("Telefono")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarEscalafon()

        ' traer datos de la tabla Escalafon desde Personal_
        Dim sql = "SELECT Principal, Descripcion FROM [Escalafon]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla destino
        DSM.Execute(DSM.Personal, "DELETE FROM Escalafon", Nothing)

        ' insertar fila por fila
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Escalafon (Principal, Descripcion) 
            VALUES (@Principal, @Descripcion)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@Principal", row("Principal")},
            {"@Descripcion", row("Descripcion")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarEstadoParental()

        ' traer datos de la tabla EstadoParental desde Personal_
        Dim sql = "SELECT ID, idEstado, Estado FROM [EstadoParental]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla destino
        DSM.Execute(DSM.Personal, "DELETE FROM EstadoParental", Nothing)

        ' insertar fila por fila
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO EstadoParental (ID, idEstado, Estado) 
            VALUES (@ID, @idEstado, @Estado)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@ID", row("ID")},
            {"@idEstado", row("idEstado")},
            {"@Estado", row("Estado")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarEventuales()

        ' traer datos de la tabla Eventuales desde Personal_
        Dim sql = "SELECT * FROM [Eventuales]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Eventuales en Personal
        Dim sqlDelete = "DELETE FROM Eventuales"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Eventuales en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Eventuales (
                Legajo, TipoDto, NroDto, CargoPampa, Cargo, Instituto, Nombre, CorreroE, Sexo, Nacimiento, 
                Calle, Nro, Localidad, Oficina, Critico, MayorDedicacion, HorasDedicacion, HorasSemanales, HorasDiarias, 
                Escalafon, Jefe, LicAnual, Caracter, Comentario, Nomarca, Secretaria, 
                Vac2022, Vac2023, Vac2024, Vac2025, Vac2026, Vac2027, Vac2028,
                Vac2014, Vac2015, Vac2016, Vac2017, Vac2018, Vac2019, Vac2020, Vac2021,
                MarcaAqui, Telefono, Interno, Celular, Rpv, iNGRESO, Baja, CUIL, TITULO, UltimaActualizacion, Motivo, Consultora
            ) VALUES (
                @Legajo, @TipoDto, @NroDto, @CargoPampa, @Cargo, @Instituto, @Nombre, @CorreoE, @Sexo, @Nacimiento,
                @Calle, @Nro, @Localidad, @Oficina, @Critico, @MayorDedicacion, @HorasDedicacion, @HorasSemanales, @HorasDiarias,
                @Escalafon, @Jefe, @LicAnual, @Caracter, @Comentario, @Nomarca, @Secretaria,
                @Vac2022, @Vac2023, @Vac2024, @Vac2025, @Vac2026, @Vac2027, @Vac2028,
                @Vac2014, @Vac2015, @Vac2016, @Vac2017, @Vac2018, @Vac2019, @Vac2020, @Vac2021,
                @MarcaAqui, @Telefono, @Interno, @Celular, @Rpv, @iNGRESO, @Baja, @CUIL, @TITULO, @UltimaActualizacion, @Motivo, @Consultora
            )"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Legajo", row("Legajo")},
                {"@TipoDto", row("TipoDto")},
                {"@NroDto", row("NroDto")},
                {"@CargoPampa", row("CargoPampa")},
                {"@Cargo", row("Cargo")},
                {"@Instituto", row("Instituto")},
                {"@Nombre", row("Nombre")},
                {"@CorreoE", row("CorreroE")}, ' <- en la tabla está como CorreroE
                {"@Sexo", row("Sexo")},
                {"@Nacimiento", If(IsDBNull(row("Nacimiento")) OrElse CDate(row("Nacimiento")) < #1/1/1753#, DBNull.Value, row("Nacimiento"))},
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
                {"@Consultora", row("Consultora")}
            }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarExpediente()

        ' 1) Traer datos desde Personal_
        Dim sql = "SELECT * FROM [Expediente]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' 2) Limpiar destino
        DSM.Execute(DSM.Personal, "DELETE FROM Expediente", Nothing)

        ' 3) Insertar (sin Id porque es IDENTITY)
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Expediente (
                Legajo, Dia, Motivo, CantidadDias, Articulo, Inciso, Secretaria, Nombre, Anexo, Listar,
                Mes, Anio, Insti, Decreto, Orden, Autorizo, Cargo
            ) VALUES (
                @Legajo, @Dia, @Motivo, @CantidadDias, @Articulo, @Inciso, @Secretaria, @Nombre, @Anexo, @Listar,
                @Mes, @Anio, @Insti, @Decreto, @Orden, @Autorizo, @Cargo
            )"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Legajo", row("Legajo")},
                {"@Dia", If(IsDBNull(row("Dia")) OrElse CDate(row("Dia")) < #1/1/1753#, DBNull.Value, row("Dia"))},
                {"@Motivo", row("Motivo")},
                {"@CantidadDias", row("CantidadDias")},
                {"@Articulo", row("Articulo")},
                {"@Inciso", row("Inciso")},
                {"@Secretaria", row("Secretaria")},
                {"@Nombre", row("Nombre")},
                {"@Anexo", row("Anexo")},
                {"@Listar", If(IsDBNull(row("Listar")), 0, row("Listar"))},  ' NOT NULL
                {"@Mes", row("Mes")},
                {"@Anio", row("Anio")},
                {"@Insti", row("Insti")},
                {"@Decreto", row("Decreto")},
                {"@Orden", row("Orden")},
                {"@Autorizo", row("Autorizo")},
                {"@Cargo", row("Cargo")}
            }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarGrupoFamiliar()

        ' traer datos de la tabla GrupoFamiliar desde Personal_
        Dim sql = "SELECT * FROM [GrupoFamiliar]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla GrupoFamiliar en Personal
        Dim sqlDelete = "DELETE FROM GrupoFamiliar"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla GrupoFamiliar en Personal (sin Id porque es IDENTITY)
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO GrupoFamiliar (
                Legajo, Nombre, Parentesco, Nacimiento, Edad, Ocupacion, Nivel
            ) VALUES (
                @Legajo, @Nombre, @Parentesco, @Nacimiento, @Edad, @Ocupacion, @Nivel
            )"

            Dim parametros As New Dictionary(Of String, Object) From {
            {"@Legajo", row("Legajo")},
            {"@Nombre", row("Nombre")},
            {"@Parentesco", row("Parentesco")},
            {"@Nacimiento", If(IsDBNull(row("Nacimiento")), DBNull.Value, row("Nacimiento"))},
            {"@Edad", row("Edad")},
            {"@Ocupacion", row("Ocupacion")},
            {"@Nivel", row("Nivel")}
        }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

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

    Private Sub ImportarInasistencias()

        ' traer datos de la tabla Inasistencias desde Personal_
        Dim sql = "SELECT * FROM [Inasistencias]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Inasistencias en Personal
        Dim sqlDelete = "DELETE FROM Inasistencias"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Inasistencias en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Inasistencias (
                Codigo, Descripcion, Articulo, Inciso, Punto, Goce, Año, Mes, Corrido, Habil, Mensual, Anexo, AnioDto, Decreto, Sale
            ) VALUES (
                @Codigo, @Descripcion, @Articulo, @Inciso, @Punto, @Goce, @Año, @Mes, @Corrido, @Habil, @Mensual, @Anexo, @AnioDto, @Decreto, @Sale
            )"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Codigo", row("Codigo")},
                {"@Descripcion", row("Descripcion")},
                {"@Articulo", row("Articulo")},
                {"@Inciso", row("Inciso")},
                {"@Punto", row("Punto")},
                {"@Goce", If(IsDBNull(row("Goce")), 0, row("Goce"))},
                {"@Año", row("Año")},
                {"@Mes", row("Mes")},
                {"@Corrido", If(IsDBNull(row("Corrido")), 0, row("Corrido"))},
                {"@Habil", If(IsDBNull(row("Habil")), 0, row("Habil"))},
                {"@Mensual", If(IsDBNull(row("Mensual")), 0, row("Mensual"))},
                {"@Anexo", row("Anexo")},
                {"@AnioDto", row("AnioDto")},
                {"@Decreto", row("Decreto")},
                {"@Sale", If(IsDBNull(row("Sale")), 0, row("Sale"))}
            }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarInstitutos()

        ' traer datos de la tabla Institutos desde Personal_
        Dim sql = "SELECT * FROM [Institutos]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Institutos en Personal
        Dim sqlDelete = "DELETE FROM Institutos"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Institutos en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Institutos (
                id, Descripcion
            ) VALUES (
                @id, @Descripcion
            )"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@id", row("id")},
                {"@Descripcion", row("Descripcion")}
            }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarMeses()

        ' traer datos de la tabla Meses desde Personal_
        Dim sql = "SELECT * FROM [Meses]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Meses en Personal
        Dim sqlDelete = "DELETE FROM Meses"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Meses en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Meses (
                IdMes, Mes
            ) VALUES (
                @IdMes, @Mes
            )"

            Dim parametros As New Dictionary(Of String, Object) From {
            {"@IdMes", row("IdMes")},
            {"@Mes", row("Mes")}
        }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

    Private Sub ImportarMinutos()

        ' traer datos de la tabla Minutos desde Personal_
        Dim sql = "SELECT * FROM [Minutos]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Minutos en Personal
        Dim sqlDelete = "DELETE FROM Minutos"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Minutos en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
            INSERT INTO Minutos (Minutos) 
            VALUES (@Minutos)"
            Dim parametros As New Dictionary(Of String, Object) From {
            {"@Minutos", row("Minutos")}
        }
            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

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

    Private Sub ImportarParametros()

        ' traer datos de la tabla Parametros desde Personal_
        Dim sql = "SELECT * FROM [Parametros]"
        Dim dt = DSM.ExecuteQuery(DSM.Personal_, sql, Nothing)

        ' borrar datos en la tabla Parametros en Personal
        Dim sqlDelete = "DELETE FROM Parametros"
        DSM.Execute(DSM.Personal, sqlDelete, Nothing)

        ' insertar datos en la tabla Parametros en Personal
        For Each row As DataRow In dt.Rows
            Dim sqlInsert = "
                INSERT INTO Parametros (
                    ParEmpresa, ParRespon, ParAdmin, ParTelef, ParPath, ParNombre, ParPathExcel, ParPathWord
                ) VALUES (
                    @ParEmpresa, @ParRespon, @ParAdmin, @ParTelef, @ParPath, @ParNombre, @ParPathExcel, @ParPathWord
                )"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@ParEmpresa", row("ParEmpresa")},
                {"@ParRespon", row("ParRespon")},
                {"@ParAdmin", row("ParAdmin")},
                {"@ParTelef", row("ParTelef")},
                {"@ParPath", row("ParPath")},
                {"@ParNombre", row("ParNombre")},
                {"@ParPathExcel", row("ParPathExcel")},
                {"@ParPathWord", row("ParPathWord")}
            }

            DSM.Execute(DSM.Personal, sqlInsert, parametros)
        Next
    End Sub

End Class