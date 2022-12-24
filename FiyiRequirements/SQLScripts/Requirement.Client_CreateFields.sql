USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:32

ALTER TABLE [dbo].[Requirement.Client] ADD [ClientId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [FirstName] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [LastName] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [BusinessName] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [PhoneNumber] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Client] ADD [Email] VARCHAR(300) NOT NULL
