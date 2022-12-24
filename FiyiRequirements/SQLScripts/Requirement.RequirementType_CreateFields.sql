USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:16

ALTER TABLE [dbo].[Requirement.RequirementType] ADD [RequirementTypeId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [Name] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementType] ADD [Description] VARCHAR(2000) NOT NULL
