DELETE FROM [dbo].[Movimiento];

SET IDENTITY_INSERT [dbo].[Movimiento] ON;

INSERT INTO [dbo].[Movimiento] (
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
FROM [dbo].[MovimientoCopia];

SET IDENTITY_INSERT [dbo].[Movimiento] OFF;