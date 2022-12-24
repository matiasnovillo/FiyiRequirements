USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:48:02

ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [ClientId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [Title] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [Body] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementStateId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementTypeId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementPriorityId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserProgrammerId] INT NOT NULL
