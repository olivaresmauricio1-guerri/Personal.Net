DELETE FROM [dbo].[MovimientoCopia];

SET IDENTITY_INSERT [dbo].[MovimientoCopia] ON;

INSERT INTO [dbo].[MovimientoCopia] (
    [Id],
    [Legajo],
    [Instituto],
    [Dia],
    [Entro],
    [Salio],
    [HsCumplidas],
    [MotivoInasistencia],
    [Comentario],
    [SinFicha],
    [Autorizo],
    [NoPromedia],
    [Nopromedianada]
)
SELECT
    [Id],
    [Legajo],
    [Instituto],
    [Dia],
    [Entro],
    [Salio],
    [HsCumplidas],
    [MotivoInasistencia],
    [Comentario],
    [SinFicha],
    [Autorizo],
    [NoPromedia],
    [Nopromedianada]
FROM [dbo].[Movimiento];

SET IDENTITY_INSERT [dbo].[MovimientoCopia] OFF;