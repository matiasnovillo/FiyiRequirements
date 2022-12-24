USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:20

ALTER TABLE [dbo].[Requirement.Technology] ADD [TechnologyId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [Name] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Technology] ADD [Description] VARCHAR(2000) NOT NULL
