-- Tabla de RELOJES
CREATE TABLE dbo.Relojes (
    RelojId         INT IDENTITY(1,1) CONSTRAINT PK_Relojes PRIMARY KEY,
    Nombre          NVARCHAR(100) NOT NULL,
    Ip              VARCHAR(45)   NOT NULL,
    Puerto          INT           NOT NULL CONSTRAINT DF_Reloj_Puerto DEFAULT (4370),
    ClaveCom        INT           NOT NULL CONSTRAINT DF_Reloj_ClaveCom DEFAULT (0), -- comm key
    RutaId          INT           NULL,
    Activo          BIT           NOT NULL CONSTRAINT DF_Reloj_Activo DEFAULT (1),
    Ubicacion       NVARCHAR(100) NULL,
    Notas           NVARCHAR(500) NULL,
    UltimaLectura   DATETIME2(3)  NULL,

    CONSTRAINT CK_Reloj_Puerto_Rango   CHECK (Puerto BETWEEN 1 AND 65535),
    CONSTRAINT CK_Reloj_ClaveCom_Rango CHECK (ClaveCom BETWEEN 0 AND 999999),

    CONSTRAINT UQ_Reloj_Nombre UNIQUE (Nombre),
    CONSTRAINT UQ_Reloj_Ip     UNIQUE (Ip),

    CONSTRAINT FK_Reloj_Ruta FOREIGN KEY (RutaId)
        REFERENCES dbo.RutasEstaticas(RutaId)
        ON UPDATE NO ACTION
        ON DELETE SET NULL
);

BEGIN TRAN;

-- Permitir insertar IDs fijos
SET IDENTITY_INSERT dbo.Relojes ON;

INSERT INTO dbo.Relojes
    (RelojId, Nombre,              Ip,               Puerto, ClaveCom, RutaId, Activo, Ubicacion, Notas, UltimaLectura)
VALUES
    (      1, N'Casa Central',     '192.168.2.5',      4370,        0,   NULL,      1, 'Mendoza',  NULL, NULL),
    (      2, N'Autoshop Mdz',     '192.168.2.6',      4370,        0,   NULL,      1, 'Mendoza',  NULL, NULL),
    (      3, N'Zona Franca',      '192.168.3.5',      4370,        0,      1,      1, 'Mendoza',  NULL, NULL),
    (      4, N'Deposito Halpern', '192.168.4.50',     4371,        0,      2,      1, 'Mendoza',  NULL, NULL),
    (      5, N'Neuquen',          '181.171.90.106',   4370,     1234,   NULL,      1, 'Neuquen',  NULL, NULL),
    (      6, N'Alcorta',          '192.168.1.73/A',   4370,        0,   NULL,      1, 'BsAires',  NULL, NULL),
    (      7, N'Garay',            '192.168.1.73/B',   4370,        0,   NULL,      1, 'BsAires',  NULL, NULL),
    (      8, N'Belgrano',         '192.168.1.73/C',   4370,        0,   NULL,      1, 'BsAires',  NULL, NULL);

SET IDENTITY_INSERT dbo.Relojes OFF;

COMMIT TRAN;