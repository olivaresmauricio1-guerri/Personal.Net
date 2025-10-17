
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module Vacaciones

    Public Sub ActualizarVacacionesPorAgente(Legajo As Integer)
        If Legajo <= 0 Then
            ' legajo invalido
            Exit Sub
        End If

        Dim sqlTipos = "SELECT Descripcion, AnioDto FROM Inasistencias WHERE Descripcion LIKE 'VACACIONES%'"
        Dim dtTipos As DataTable = DSM.ExecuteQuery(DSM.Personal, sqlTipos, Nothing)

        Dim sqlIngreso = "SELECT iNGRESO, AjusteAntiguedad FROM Agentes WHERE Legajo = @Legajo"
        Dim parametrosIngreso = CmdParams("@Legajo", Legajo)
        Dim dtIngreso = DSM.ExecuteQuery(DSM.Personal, sqlIngreso, parametrosIngreso)
        Dim fechaIngreso As Date = If(dtIngreso.Rows.Count > 0 AndAlso Not IsDBNull(dtIngreso.Rows(0)("iNGRESO")), CDate(dtIngreso.Rows(0)("iNGRESO")), Date.MinValue)

        If fechaIngreso = Date.MinValue Then
            ' no tiene fecha de ingreso, no se puede calcular
            Exit Sub
        End If

        ' encontrar el tipo de licencia del año actual
        Dim anioDtoActual As Integer = Date.Now.Year
        Dim tipoActual As DataRow() = dtTipos.Select("AnioDto = " & anioDtoActual)
        If tipoActual.Length = 0 Then
            ' no hay tipo para el año actual, no se puede calcular
            Exit Sub
        End If

        Dim motivo As String = CStr(tipoActual(0)("Descripcion"))
        Dim AjusteAntiguedad = dtIngreso.Rows(0).Field(Of Integer)("AjusteAntiguedad")
        Dim diasDisponibles = CalcularDiasVacaciones(fechaIngreso, AjusteAntiguedad) ' se calcula con fecha de ingreso real mas adelante
        Dim sqlInsert = "
            INSERT INTO Licencia (Legajo, Motivo, diasDisponibles, diasRestantes)
            SELECT @Legajo, @Motivo, @diasDisponibles, @diasRestantes
            WHERE NOT EXISTS (SELECT 1 FROM Licencia WHERE Legajo = @Legajo AND Motivo = @Motivo);"
        Dim parametrosInsert = CmdParams("@Legajo", Legajo, "@Motivo", motivo, "@diasDisponibles", diasDisponibles, "@diasRestantes", diasDisponibles)
        DSM.Execute(DSM.Personal, sqlInsert, parametrosInsert)

    End Sub

    ' funcion para calcular los dias de vacaciones segun la antiguedad, si tiene 0 años se calcula 1 dia disponible cada 20 dias de trabajo

    Public Function CalcularDiasVacaciones(fechaIngreso As Date, AjusteAntiguedad As Integer) As Integer
        Dim fechaHasta As Date = New Date(Date.Now.Year, 12, 31)

        ' Antigüedad base: años completos al momento de fechaHasta
        Dim antiguedad As Integer = fechaHasta.Year - fechaIngreso.Year
        If (fechaHasta.Month > fechaIngreso.Month) OrElse (fechaHasta.Month = fechaIngreso.Month AndAlso fechaHasta.Day >= fechaIngreso.Day) Then
            antiguedad += 1
        End If

        ' ajuste de antigüedad
        antiguedad += AjusteAntiguedad

        ' Normalizaciones
        If antiguedad < 0 Then antiguedad = 0
        If antiguedad > 40 Then antiguedad = 40

        ' Regla AR: en el PRIMER año
        ' - si ingresó el 30/06 o antes => contar como año completo (antigüedad = 1)
        ' - si ingresó el 01/07 o después => no cuenta año (se calculará días/20)
        If antiguedad = 0 Then
            Dim cortePrimerSemestre As New Date(fechaIngreso.Year, 6, 30)
            If fechaIngreso <= cortePrimerSemestre Then
                antiguedad = 1
            End If
        End If

        ' Aplicación de la tabla por antigüedad o fallback días/20
        If VacacionesPorAntiguedad.ContainsKey(antiguedad) Then
            Return VacacionesPorAntiguedad(antiguedad)
        Else
            ' retorna un día por cada 20 días trabajados (entero, truncado)
            Dim diasTrabajados As Integer = (fechaHasta - fechaIngreso).Days
            Dim diasVacaciones As Integer = diasTrabajados \ 20
            Return diasVacaciones
        End If
    End Function


    ' hasta 40 años
    Public ReadOnly VacacionesPorAntiguedad As New Dictionary(Of Integer, Integer) From {
        {1, 14}, {2, 14}, {3, 14}, {4, 14},
        {5, 21}, {6, 21}, {7, 21}, {8, 21}, {9, 21},
        {10, 28}, {11, 28}, {12, 28}, {13, 28}, {14, 28},
        {15, 28}, {16, 28}, {17, 28}, {18, 28}, {19, 28},
        {20, 35}, {21, 35}, {22, 35}, {23, 35}, {24, 35},
        {25, 35}, {26, 35}, {27, 35}, {28, 35}, {29, 35},
        {30, 35}, {31, 35}, {32, 35}, {33, 35}, {34, 35},
        {35, 35}, {36, 35}, {37, 35}, {38, 35}, {39, 35}, {40, 35}
    }


    Public Function ObtenerVacaciones(Optional Legajo As Integer = 0) As DataTable
        Dim whereClause As String = If(Legajo > 0, "WHERE L.Legajo = @Legajo", "")
        Dim sql As String = $"
            SELECT L.Legajo, A.Nombre, A.Instituto, L.Motivo, L.diasDisponibles, L.diasRestantes 
            FROM Licencia L 
            INNER JOIN Agentes A ON L.Legajo = A.Legajo
            {whereClause} ORDER BY L.Legajo, L.Motivo"
        Dim parametros = If(Legajo > 0, CmdParams("@Legajo", Legajo), Nothing)
        Return DSM.ExecuteQuery(DSM.Personal, sql, parametros)
    End Function

    ' obtiene el saldo de vacaciones del año en curso para un legajo y de todos los años anteriores
    ' devuelve un array de pares año-saldo
    Public Function ObtenerSaldosVacaciones(Legajo As Integer) As DataTable
        Dim sql As String = "
            SELECT * FROM Licencia
            WHERE Legajo = @Legajo AND Motivo LIKE 'VACACIONES%' AND diasRestantes > 0"
        Dim parametros = CmdParams("@Legajo", Legajo)
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Personal, sql, parametros)
        Dim count = dt.Rows.Count
        Return dt
    End Function
End Module
