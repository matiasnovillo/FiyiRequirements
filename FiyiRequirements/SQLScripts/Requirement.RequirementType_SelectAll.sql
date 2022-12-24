CREATE PROCEDURE [dbo].[Requirement.RequirementType.SelectAll]

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
EXEC [dbo].[Requirement.RequirementType.SelectAll]
 *
 */

--Last modification on: 24/12/2022 6:47:16

SET DATEFORMAT DMY

SELECT
    [Requirement.RequirementType].[RequirementTypeId],
    [Requirement.RequirementType].[Active],
    [Requirement.RequirementType].[DateTimeCreation],
    [Requirement.RequirementType].[DateTimeLastModification],
    [Requirement.RequirementType].[UserCreationId],
    [Requirement.RequirementType].[UserLastModificationId],
    [Requirement.RequirementType].[Name],
    [Requirement.RequirementType].[Description]
FROM 
    [Requirement.RequirementType]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementType].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementType].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [Requirement.RequirementType].[RequirementTypeId]