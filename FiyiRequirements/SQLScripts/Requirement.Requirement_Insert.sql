CREATE PROCEDURE [dbo].[Requirement.Requirement.Insert] 
(
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @ClientId INT,
    @Title VARCHAR(100),
    @Body VARCHAR(8000),
    @RequirementStateId INT,
    @RequirementTypeId INT,
    @RequirementPriorityId INT,
    @UserProgrammerId INT,

    @NewEnteredId INT OUTPUT
)

AS

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

/*
 * Execute this stored procedure with the next script as example
 *
DECLARE	@NewEnteredId INT
EXEC [dbo].[Requirement.Requirement.Insert]

    @Active = 1,
    @DateTimeCreation = N'01/01/1753 0:00:00.001',
    @DateTimeLastModification = N'01/01/1753 0:00:00.001',
    @UserCreationId = 1,
    @UserLastModificationId = 1,
     @ClientId = 1,
    @Title = N'PutTitle',
    @Body = N'PutBody',
     @RequirementStateId = 1,
     @RequirementTypeId = 1,
     @RequirementPriorityId = 1,
     @UserProgrammerId = 1,

@NewEnteredId = @NewEnteredId OUTPUT

SELECT @NewEnteredId AS N'@NewEnteredId'
 *
 */

--Last modification on: 24/12/2022 6:48:02

INSERT INTO [Requirement.Requirement]
(
    [Active],
    [DateTimeCreation],
    [DateTimeLastModification],
    [UserCreationId],
    [UserLastModificationId],
    [ClientId],
    [Title],
    [Body],
    [RequirementStateId],
    [RequirementTypeId],
    [RequirementPriorityId],
    [UserProgrammerId]
)
VALUES
(
    @Active,
    @DateTimeCreation,
    @DateTimeLastModification,
    @UserCreationId,
    @UserLastModificationId,
    @ClientId,
    @Title,
    @Body,
    @RequirementStateId,
    @RequirementTypeId,
    @RequirementPriorityId,
    @UserProgrammerId
)

SELECT @NewEnteredId = @@IDENTITY