USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:41

ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [ClientApplicationId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [ClientId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.ClientApplication] ADD [ApplicationId] INT NOT NULL
