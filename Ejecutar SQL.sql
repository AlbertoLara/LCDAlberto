CREATE DATABASE BdContribuyentes
GO 
PRINT 'La base de datos: BdContribuyentes ha sido creada con exito.' 
GO 

USE BdContribuyentes 
GO 
CREATE TABLE tblCertificado
( 
RFC NVARCHAR(50),
ValidezObligaciones NVARCHAR(50),
EstatusCertificado NVARCHAR(50),
noCertificado NVARCHAR(50),
FechaInicio NVARCHAR(50),
FechaFinal NVARCHAR(50), 
) 
GO 

USE [BdContribuyentes]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERTARXML1]
AS
BEGIN   
    SET NOCOUNT ON; 
    DECLARE @xmlDoc XML

SELECT @xmlDoc = BulkColumn
FROM OPENROWSET (
	BULK 'c:\archivo\a1nuevo.xml', SINGLE_CLOB 
	) AS xmlData
	;WITH XMLNAMESPACES(N'http://www.w3.org/2001/XMLSchema-instance' as xsi, 'http:/www.sat.gob.mx/cfd/LCO' as lco)


INSERT tblCertificado(RFC, ValidezObligaciones, EstatusCertificado, noCertificado, FechaFinal,FechaInicio)
SELECT Contribuyente.value('@RFC', 'VARCHAR(50)') as RFC,
	   Certificado.value('@ValidezObligaciones', 'VARCHAR(50)') as ValidezObligaciones,
	   Certificado.value('@EstatusCertificado', 'VARCHAR(50)') as EstatusCertificado,
	   Certificado.value('@noCertificado', 'VARCHAR(50)') as noCertificado,
	   Certificado.value('@FechaInicio', 'VARCHAR(50)') as FechaInicio,
	   Certificado.value('@FechaFinal', 'VARCHAR(50)') as FechaFinal
FROM @xmlDoc.nodes('lco:LCO/lco:Contribuyente') as x1(Contribuyente)
CROSS APPLY x1.Contribuyente.nodes('lco:Certificado') AS x2(Certificado)

END

USE [BdContribuyentes]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERTARXML2]
AS
BEGIN   
    SET NOCOUNT ON; 
    DECLARE @xmlDoc XML
SELECT @xmlDoc = BulkColumn
FROM OPENROWSET (
	BULK 'c:\archivo\a2nuevo.xml', SINGLE_CLOB 
	) AS xmlData
	;WITH XMLNAMESPACES(N'http://www.w3.org/2001/XMLSchema-instance' as xsi, 'http:/www.sat.gob.mx/cfd/LCO' as lco)

INSERT tblCertificado(RFC, ValidezObligaciones, EstatusCertificado, noCertificado, FechaFinal,FechaInicio)
SELECT Contribuyente.value('@RFC', 'VARCHAR(50)') as RFC,
	   Certificado.value('@ValidezObligaciones', 'VARCHAR(50)') as ValidezObligaciones,
	   Certificado.value('@EstatusCertificado', 'VARCHAR(50)') as EstatusCertificado,
	   Certificado.value('@noCertificado', 'VARCHAR(50)') as noCertificado,
	   Certificado.value('@FechaInicio', 'VARCHAR(50)') as FechaInicio,
	   Certificado.value('@FechaFinal', 'VARCHAR(50)') as FechaFinal
FROM @xmlDoc.nodes('lco:LCO/lco:Contribuyente') as x1(Contribuyente)
CROSS APPLY x1.Contribuyente.nodes('lco:Certificado') AS x2(Certificado)

END

USE [BdContribuyentes]
GO
/****** Object:  StoredProcedure [dbo].[INSERTARXML3]    Script Date: 25/01/2015 05:37:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERTARXML3]
AS
BEGIN   
    SET NOCOUNT ON; 
    DECLARE @xmlDoc XML
--DECLARE @filePath VARCHAR(300)
--SET @filePath = 'C:\Users\ACER\Documents\LCO.xml'
SELECT @xmlDoc = BulkColumn
FROM OPENROWSET (
	BULK 'c:\archivo\a3nuevo.xml', SINGLE_CLOB 
	) AS xmlData
	;WITH XMLNAMESPACES(N'http://www.w3.org/2001/XMLSchema-instance' as xsi, 'http:/www.sat.gob.mx/cfd/LCO' as lco)
--PRINT CAST(@xmlDoc as varchar(max)) 

INSERT tblCertificado(RFC, ValidezObligaciones, EstatusCertificado, noCertificado, FechaFinal,FechaInicio)
SELECT Contribuyente.value('@RFC', 'VARCHAR(50)') as RFC,
	   Certificado.value('@ValidezObligaciones', 'VARCHAR(50)') as ValidezObligaciones,
	   Certificado.value('@EstatusCertificado', 'VARCHAR(50)') as EstatusCertificado,
	   Certificado.value('@noCertificado', 'VARCHAR(50)') as noCertificado,
	   Certificado.value('@FechaInicio', 'VARCHAR(50)') as FechaInicio,
	   Certificado.value('@FechaFinal', 'VARCHAR(50)') as FechaFinal
FROM @xmlDoc.nodes('lco:LCO/lco:Contribuyente') as x1(Contribuyente)
CROSS APPLY x1.Contribuyente.nodes('lco:Certificado') AS x2(Certificado)

END

USE [BdContribuyentes]
GO
/****** Object:  StoredProcedure [dbo].[INSERTARXML4]    Script Date: 25/01/2015 05:38:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERTARXML4]
AS
BEGIN   
    SET NOCOUNT ON; 
    DECLARE @xmlDoc XML
SELECT @xmlDoc = BulkColumn
FROM OPENROWSET (
	BULK 'c:\archivo\a4nuevo.xml', SINGLE_CLOB 
	) AS xmlData
	;WITH XMLNAMESPACES(N'http://www.w3.org/2001/XMLSchema-instance' as xsi, 'http:/www.sat.gob.mx/cfd/LCO' as lco)

INSERT tblCertificado(RFC, ValidezObligaciones, EstatusCertificado, noCertificado, FechaFinal,FechaInicio)
SELECT Contribuyente.value('@RFC', 'VARCHAR(50)') as RFC,
	   Certificado.value('@ValidezObligaciones', 'VARCHAR(50)') as ValidezObligaciones,
	   Certificado.value('@EstatusCertificado', 'VARCHAR(50)') as EstatusCertificado,
	   Certificado.value('@noCertificado', 'VARCHAR(50)') as noCertificado,
	   Certificado.value('@FechaInicio', 'VARCHAR(50)') as FechaInicio,
	   Certificado.value('@FechaFinal', 'VARCHAR(50)') as FechaFinal
FROM @xmlDoc.nodes('lco:LCO/lco:Contribuyente') as x1(Contribuyente)
CROSS APPLY x1.Contribuyente.nodes('lco:Certificado') AS x2(Certificado)

END



