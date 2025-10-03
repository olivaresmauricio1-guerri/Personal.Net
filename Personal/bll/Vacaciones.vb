
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Vacaciones

    Public Sub ActualizarVacacionesPorAgente(Legajo As Integer)
        If Legajo <= 0 Then
            ' legajo invalido
            Exit Sub
        End If

        Dim sqlTipos = "SELECT Descripcion, AnioDto FROM Inasistencias WHERE Descripcion LIKE 'VACACIONES%'"
        Dim dtTipos As DataTable = DSM.ExecuteQuery(DSM.Personal, sqlTipos, Nothing)



        Dim sqlIngreso = "SELECT iNGRESO FROM Agentes WHERE Legajo = @Legajo"
        Dim parametrosIngreso = CmdParams("@Legajo", Legajo)
        Dim dtIngreso = DSM.ExecuteQuery(DSM.Personal, sqlIngreso, parametrosIngreso)
        Dim fechaIngreso As Date = If(dtIngreso.Rows.Count > 0 AndAlso Not IsDBNull(dtIngreso.Rows(0)("iNGRESO")), CDate(dtIngreso.Rows(0)("iNGRESO")), Date.MinValue)

        If fechaIngreso = Date.MinValue Then
            ' no tiene fecha de ingreso, no se puede calcular
            Exit Sub
        End If


        ' por cada tipo, insert en Licencia
        For Each row As DataRow In dtTipos.Rows
            Dim motivo As String = CStr(row("Descripcion"))
            Dim anioDto As Integer = If(IsDBNull(row("AnioDto")), 0, Convert.ToInt32(row("AnioDto")))
            ' verificar si ya existe el registro
            Dim sqlVerif = "SELECT COUNT(*) FROM Licencia WHERE Legajo = @Legajo AND Motivo = @Motivo"
            Dim parametrosVerif = CmdParams("@Legajo", Legajo, "@Motivo", motivo)
            Dim dtCount = DSM.ExecuteQuery(DSM.Personal, sqlVerif, parametrosVerif)
            Dim count As Integer = If(dtCount.Rows.Count > 0 AndAlso Not IsDBNull(dtCount.Rows(0)(0)), Convert.ToInt32(dtCount.Rows(0)(0)), 0)

            ' no existe, insertar
            If count = 0 Then

                ' si anioDto > 0, se calcula hasta el 31/12 de ese año, si no, no insertar
                If anioDto < 0 Then
                    Continue For
                End If

                Dim fechaComparar = If(anioDto > 0, New Date(anioDto, 12, 31), Date.Now)

                ' si fechaComparar es menor a fechaIngreso, no insertar
                If fechaComparar < fechaIngreso Then
                    Continue For
                End If

                ' si fechaComparar es mayor a hoy, no insertar
                If fechaComparar.Year > Date.Now.Year Then
                    Continue For
                End If

                Dim diasDisponibles = CalcularDiasVacaciones(fechaIngreso, fechaComparar) ' se calcula con fecha de ingreso real mas adelante
                Dim sqlInsert = "INSERT INTO Licencia (Legajo, Motivo, diasDisponibles, diasRestantes) VALUES (@Legajo, @Motivo, @diasDisponibles, @diasRestantes)"
                Dim parametrosInsert = CmdParams("@Legajo", Legajo, "@Motivo", motivo, "@diasDisponibles", diasDisponibles, "@diasRestantes", diasDisponibles)
                DSM.Execute(DSM.Personal, sqlInsert, parametrosInsert)
            End If
        Next

    End Sub

    ' funcion para calcular los dias de vacaciones segun la antiguedad, si tiene 0 años se calcula 1 dia disponible cada 20 dias de trabajo
    Public Function CalcularDiasVacaciones(fechaIngreso As Date, fechaHasta As Date) As Integer

        Dim antiguedad As Integer = fechaHasta.Year - fechaIngreso.Year + 1
        If (fechaHasta.Month < fechaIngreso.Month) Or (fechaHasta.Month = fechaIngreso.Month And fechaHasta.Day < fechaIngreso.Day) Then
            antiguedad -= 1
        End If
        If antiguedad < 0 Then antiguedad = 0
        If antiguedad > 40 Then antiguedad = 40
        If VacacionesPorAntiguedad.ContainsKey(antiguedad) Then
            Return VacacionesPorAntiguedad(antiguedad)
        Else
            ' retorna un dia por cada 20 dias trabajados (entero, truncado)
            Dim diasTrabajados As Integer = (fechaHasta - fechaIngreso).Days
            Dim diasVacaciones As Integer = diasTrabajados \ 20
            Return diasVacaciones
        End If
    End Function

    ' hasta 40 años
    Public ReadOnly VacacionesPorAntiguedad As New Dictionary(Of Integer, Integer) From {
        {1, 14},
        {2, 14},
        {3, 14},
        {4, 14},
        {5, 21},
        {6, 21},
        {7, 21},
        {8, 21},
        {9, 21},
        {10, 28},
        {11, 28},
        {12, 28},
        {13, 28},
        {14, 28},
        {15, 35},
        {16, 35},
        {17, 35},
        {18, 35},
        {19, 35},
        {20, 42},  ' 20 o más años de antigüedad: 42 días
        {21, 42},
        {22, 42},
        {23, 42},
        {24, 42},
        {25, 42},
        {26, 42},
        {27, 42},
        {28, 42},
        {29, 42},
        {30, 49},  ' 30 o más años de antigüedad: 49 días
        {31, 49},
        {32, 49},
        {33, 49},
        {34, 49},
        {35, 49},
        {36, 49},
        {37, 49},
        {38, 49},
        {39, 49},
        {40, 56}   ' Más de 40 años de antigüedad: 56 días
    }

End Module
