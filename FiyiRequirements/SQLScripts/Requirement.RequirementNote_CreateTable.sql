USE [fiyistack_FiyiRequirements]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:30:34

CREATE TABLE [dbo].[Requirement.RequirementNote] (
    [RequirementNoteId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_RequirementRequirementNote] PRIMARY KEY CLUSTERED ([RequirementNoteId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]