CREATE PROCEDURE [dbo].[Requirement.Requirement.UpdateByRequirementId]
(
    @RequirementId INT,
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

    @RowsAffected INT OUTPUT
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
DECLARE	@RowsAffected int
EXEC [dbo].[Requirement.Requirement.UpdateByRequirementId]
    @RequirementId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 24/12/2022 6:48:02

UPDATE [Requirement.Requirement] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [ClientId] = @ClientId,
    [Title] = @Title,
    [Body] = @Body,
    [RequirementStateId] = @RequirementStateId,
    [RequirementTypeId] = @RequirementTypeId,
    [RequirementPriorityId] = @RequirementPriorityId,
    [UserProgrammerId] = @UserProgrammerId
WHERE 
    1 = 1 
    AND [Requirement.Requirement].[RequirementId] = @RequirementId 

SELECT @RowsAffected = @@ROWCOUNT