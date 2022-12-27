USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 27/12/2022 16:32:18

CREATE TABLE [dbo].[Requirement.UserApplication] (
    [UserApplicationId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_RequirementUserApplication] PRIMARY KEY CLUSTERED ([UserApplicationId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]