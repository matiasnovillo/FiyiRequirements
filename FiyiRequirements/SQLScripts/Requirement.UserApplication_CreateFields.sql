USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 27/12/2022 16:32:18

ALTER TABLE [dbo].[Requirement.UserApplication] ADD [UserApplicationId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [ApplicationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.UserApplication] ADD [UserId] INT NOT NULL
