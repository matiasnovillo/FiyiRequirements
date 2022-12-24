USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:27

ALTER TABLE [dbo].[Requirement.Application] ADD [ApplicationId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [Name] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [Description] VARCHAR(2000) NOT NULL
ALTER TABLE [dbo].[Requirement.Application] ADD [TechnologyId] INT NOT NULL
