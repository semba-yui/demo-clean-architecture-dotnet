-- CREATE DATABASE

CREATE DATABASE testdb
GO

USE testdb;

ALTER DATABASE testdb COLLATE Japanese_CS_AS_KS_WS;

-- CREATE SCHEMA

IF NOT EXISTS (SELECT 1
               FROM sys.schemas
               WHERE name = 'dca')
    BEGIN
        EXEC ('CREATE SCHEMA dca;');
    END;