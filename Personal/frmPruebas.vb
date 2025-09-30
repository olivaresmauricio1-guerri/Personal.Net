Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmPruebas
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ImportarMovimientos()
        ImportarHoras()
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
                Dim entro = row("Entro")
                Dim salio = row("Salio")
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