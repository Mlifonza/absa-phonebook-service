USE MASTER
GO

IF EXISTS (SELECT NAME FROM master.dbo.sysdatabases WHERE NAME = 'phonebook')
	DROP DATABASE phonebook
GO

CREATE DATABASE phonebook
ON PRIMARY
(
	NAME = 'phonebook_data',
	FILENAME = 'C:\SQL\phonebook.mdf',
	SIZE = 10MB,
	FILEGROWTH = 5%
)
LOG ON 
(
	NAME = 'phonebook_log',
	FILENAME = 'C:\SQL\phonebook.ldf',
	SIZE = 10MB,
	FILEGROWTH = 5%
)
GO

USE phonebook
GO

IF (OBJECT_ID('[dbo].[Entry]') IS NOT NULL)
	DROP TABLE [dbo].[Entry]
GO

CREATE TABLE [dbo].[Entry]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(50) NOT NULL,
	[number] NVARCHAR(50) NOT NULL
)




IF (OBJECT_ID('[dbo].[sp_GetEntries]') IS NOT NULL)
	DROP PROC [dbo].[sp_GetEntries]
GO

CREATE PROCEDURE [dbo].[sp_GetEntries]
AS
BEGIN
    
	SELECT [id],[name],[number]
	FROM [dbo].[Entry]
END 
GO




IF (OBJECT_ID('[dbo].[sp_AddEntry]') IS NOT NULL)
	DROP PROC [dbo].[sp_AddEntry]
GO

CREATE PROCEDURE [dbo].[sp_AddEntry]
	@name nvarchar(50),
	@number nvarchar(50)
AS
BEGIN
    
	INSERT INTO [dbo].[Entry]([name],[number])
	VALUES (@name,@number)

	SELECT 1 AS RETVAL
END 
GO



IF (OBJECT_ID('[dbo].[sp_RemoveEntry]') IS NOT NULL)
	DROP PROC [dbo].[sp_RemoveEntry]
GO

CREATE PROCEDURE [dbo].[sp_RemoveEntry]
	@id int
AS
BEGIN
    
	DELETE FROM [dbo].[Entry]
	WHERE [id] = @id

	SELECT 1 AS RETVAL
END 
GO




IF (OBJECT_ID('[dbo].[sp_UpdateName]') IS NOT NULL)
	DROP PROC [dbo].[sp_UpdateName]
GO

CREATE PROCEDURE [dbo].[sp_UpdateName]
	@id int,
	@name nvarchar(50)
AS
BEGIN
    
	UPDATE [dbo].[Entry]
	SET [name] = @name
	WHERE [id] = @id

	SELECT 1 AS RETVAL
END 
GO




IF (OBJECT_ID('[dbo].[sp_UpdateNumber]') IS NOT NULL)
	DROP PROC [dbo].[sp_UpdateNumber]
GO

CREATE PROCEDURE [dbo].[sp_UpdateNumber]
	@id int,
	@number nvarchar(50)
AS
BEGIN
    
	UPDATE [dbo].[Entry]
	SET [number] = @number
	WHERE [id] = @id

	SELECT 1 AS RETVAL
END 
GO




















