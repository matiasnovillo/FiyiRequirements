USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/01/2023 7:54:01

ALTER TABLE [dbo].[Examples.Example] ADD [ExampleId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [Boolean] TINYINT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [DateTime] DATETIME NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [Decimal] NUMERIC(24,6) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [ForeignKeyDropDown] INT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [ForeignKeyOptions] INT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [Integer] INT NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextBasic] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextEmail] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextFile] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextHexColour] VARCHAR(6) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextPassword] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextPhoneNumber] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextTag] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextTextArea] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextTextEditor] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [TextURL] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Examples.Example] ADD [Time] TIME(3) NOT NULL
