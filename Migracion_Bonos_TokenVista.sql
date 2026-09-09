-- ============================================================================
-- MIGRACION: Portal publico de bonos con visor PDF integrado
-- Paso 1: Columnas nuevas en RecibosProcesosDetalles
-- Ejecutar en base: Personal
-- ============================================================================
SET NOCOUNT ON;

PRINT '============================================================';
PRINT 'INICIO MIGRACION BONOS - TOKEN VISTA Y ACCESO LINK';
PRINT '============================================================';
PRINT '';

-- ----------------------------------------------------------------------------
-- 1. Columna TokenVista: token PERMANENTE para ver/descargar PDF
--    No expira. No se borra al confirmar.
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'RecibosProcesosDetalles'
      AND COLUMN_NAME = 'TokenVista'
)
BEGIN
    PRINT '[1/4] Agregando columna TokenVista...';
    ALTER TABLE RecibosProcesosDetalles ADD TokenVista NVARCHAR(128) NULL;
    PRINT '       OK - TokenVista agregada.';
END
ELSE
BEGIN
    PRINT '[1/4] Columna TokenVista YA EXISTE - Saltando.';
END
GO

-- ----------------------------------------------------------------------------
-- 2. Indice unico en TokenVista para busquedas rapidas y no repetir tokens
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_RecibosProcesosDetalles_TokenVista'
      AND object_id = OBJECT_ID('RecibosProcesosDetalles')
)
BEGIN
    PRINT '[2/4] Creando indice unico IX_RecibosProcesosDetalles_TokenVista...';
    CREATE UNIQUE NONCLUSTERED INDEX IX_RecibosProcesosDetalles_TokenVista
        ON RecibosProcesosDetalles (TokenVista)
        WHERE TokenVista IS NOT NULL;
    PRINT '       OK - Indice creado.';
END
ELSE
BEGIN
    PRINT '[2/4] Indice IX_RecibosProcesosDetalles_TokenVista YA EXISTE - Saltando.';
END
GO

-- ----------------------------------------------------------------------------
-- 3. Columna FechaPrimerAccesoLink: fecha y hora de la PRIMERA vez que
--    el agente abrio el link del mail (sin importar si confirmo o no).
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'RecibosProcesosDetalles'
      AND COLUMN_NAME = 'FechaPrimerAccesoLink'
)
BEGIN
    PRINT '[3/4] Agregando columna FechaPrimerAccesoLink...';
    ALTER TABLE RecibosProcesosDetalles ADD FechaPrimerAccesoLink DATETIME NULL;
    PRINT '       OK - FechaPrimerAccesoLink agregada.';
END
ELSE
BEGIN
    PRINT '[3/4] Columna FechaPrimerAccesoLink YA EXISTE - Saltando.';
END
GO

-- ----------------------------------------------------------------------------
-- 4. Permisos para el usuario SQL USR_APIBonos (si existe)
--    La APIBonos necesita:
--      - SELECT sobre las columnas nuevas
--      - UPDATE sobre FechaPrimerAccesoLink (cuando el agente abre el link)
--    Nota: si ya tiene db_datawriter/reader, de nada sirve pero no molesta.
-- ----------------------------------------------------------------------------
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'USR_APIBonos')
BEGIN
    PRINT '[4/4] Asignando permisos a USR_APIBonos...';

    DECLARE @sql NVARCHAR(MAX);

    IF NOT EXISTS (
        SELECT 1 FROM sys.database_permissions dp
        INNER JOIN sys.database_principals dpr ON dp.grantee_principal_id = dpr.principal_id
        INNER JOIN sys.objects o ON dp.major_id = o.object_id
        WHERE dpr.name = 'USR_APIBonos'
          AND o.name = 'RecibosProcesosDetalles'
          AND dp.permission_name = 'SELECT'
    )
    BEGIN
        SET @sql = N'GRANT SELECT ON RecibosProcesosDetalles TO USR_APIBonos;';
        EXEC sp_executesql @sql;
        PRINT '       OK - GRANT SELECT sobre RecibosProcesosDetalles.';
    END
    ELSE
    BEGIN
        PRINT '       SELECT ya estaba otorgado.';
    END

    IF NOT EXISTS (
        SELECT 1 FROM sys.database_permissions dp
        INNER JOIN sys.database_principals dpr ON dp.grantee_principal_id = dpr.principal_id
        INNER JOIN sys.objects o ON dp.major_id = o.object_id
        WHERE dpr.name = 'USR_APIBonos'
          AND o.name = 'RecibosProcesosDetalles'
          AND dp.permission_name = 'UPDATE'
    )
    BEGIN
        SET @sql = N'GRANT UPDATE ON RecibosProcesosDetalles TO USR_APIBonos;';
        EXEC sp_executesql @sql;
        PRINT '       OK - GRANT UPDATE sobre RecibosProcesosDetalles.';
    END
    ELSE
    BEGIN
        PRINT '       UPDATE ya estaba otorgado.';
    END

    IF NOT EXISTS (
        SELECT 1 FROM sys.database_permissions dp
        INNER JOIN sys.database_principals dpr ON dp.grantee_principal_id = dpr.principal_id
        INNER JOIN sys.objects o ON dp.major_id = o.object_id
        WHERE dpr.name = 'USR_APIBonos'
          AND o.name = 'RecibosConfirmaciones'
          AND dp.permission_name = 'INSERT'
    )
    BEGIN
        SET @sql = N'GRANT INSERT ON RecibosConfirmaciones TO USR_APIBonos;';
        EXEC sp_executesql @sql;
        PRINT '       OK - GRANT INSERT sobre RecibosConfirmaciones.';
    END
    ELSE
    BEGIN
        PRINT '       INSERT en RecibosConfirmaciones ya estaba otorgado.';
    END
END
ELSE
BEGIN
    PRINT '[4/4] Usuario USR_APIBonos NO EXISTE en esta base - Saltando permisos.';
END
GO

PRINT '';
PRINT '============================================================';
PRINT 'MIGRACION FINALIZADA CORRECTAMENTE';
PRINT '============================================================';
PRINT '';
PRINT 'Resumen de cambios:';
PRINT '  - Tabla RecibosProcesosDetalles';
PRINT '    * Columna TokenVista (NVARCHAR(128) NULL) - Token permanente vista/descarga PDF';
PRINT '    * Columna FechaPrimerAccesoLink (DATETIME NULL) - Fecha primer click al link del mail';
PRINT '    * Indice UNICO IX_RecibosProcesosDetalles_TokenVista (filtra NULLs)';
PRINT '';
PRINT 'Ejecuta el siguiente query para confirmar:';
PRINT '  SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE';
PRINT '  FROM INFORMATION_SCHEMA.COLUMNS';
PRINT '  WHERE TABLE_NAME = ''RecibosProcesosDetalles''';
PRINT '    AND COLUMN_NAME IN (''TokenVista'', ''FechaPrimerAccesoLink'');';
PRINT '';
GO
