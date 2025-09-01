CREATE TABLE dbo.Marcaciones (
  id                   INT IDENTITY(1,1) PRIMARY KEY,

  dispositivo          VARCHAR(45)   NOT NULL,
  puerto               INT           NOT NULL,

  legajo               NVARCHAR(50)  NOT NULL,
  fechahora            DATETIME2(0)  NOT NULL,

  importado            DATETIME2(3)  NOT NULL DEFAULT SYSDATETIME(),
  procesado            BIT           NOT NULL DEFAULT (0),
);

CREATE UNIQUE INDEX UX_Marcaciones_Legajo_Fecha
  ON dbo.Marcaciones(legajo, fechahora);

CREATE INDEX IX_Marcaciones_Procesado ON dbo.Marcaciones(procesado, importado);
CREATE INDEX IX_Marcaciones_LegajoFecha ON dbo.Marcacion