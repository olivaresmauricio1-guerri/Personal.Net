-- Tabla de RUTAS ESTÁTICAS
CREATE TABLE dbo.RutasEstaticas (
    RutaId          INT IDENTITY(1,1) CONSTRAINT PK_RutasEstaticas PRIMARY KEY,
    Red             VARCHAR(15)  NOT NULL,   -- p.ej. 192.168.3.0
    Mascara         VARCHAR(15)  NOT NULL,   -- p.ej. 255.255.255.0
    Gateway         VARCHAR(15)  NOT NULL,   -- p.ej. 192.168.2.33
    InterfaceIndex  INT          NULL,       -- opcional: índice de interfaz si lo necesitás
    Persistente     BIT          NOT NULL CONSTRAINT DF_Rutas_Persistente DEFAULT(1),
    Activo          BIT          NOT NULL CONSTRAINT DF_Rutas_Activo DEFAULT(1),
    CreadoUtc       DATETIME2(3) NOT NULL CONSTRAINT DF_Rutas_CreadoUtc DEFAULT(SYSUTCDATETIME())
);

-- Evita duplicados de la misma ruta
CREATE UNIQUE INDEX UX_RutasEstaticas_Unique
ON dbo.RutasEstaticas (Red, Mascara, Gateway);