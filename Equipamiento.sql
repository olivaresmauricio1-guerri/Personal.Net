CREATE TABLE Equipamiento (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Legajo INT NOT NULL,
    Tipo NVARCHAR(100),
    Marca NVARCHAR(100),
    Modelo NVARCHAR(100),
    NroSerie NVARCHAR(50),
    IMEI NVARCHAR(20),
    Fecha DATE,
    Observaciones NVARCHAR(500)
);