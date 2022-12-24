CREATE PROCEDURE [dbo].[Requirement.Requirement.SelectAll]

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
EXEC [dbo].[Requirement.Requirement.SelectAll]
 *
 */

--Last modification on: 24/12/2022 6:48:02

SET DATEFORMAT DMY

SELECT
    [Requirement.Requirement].[RequirementId],
    [Requirement.Requirement].[Active],
    [Requirement.Requirement].[DateTimeCreation],
    [Requirement.Requirement].[DateTimeLastModification],
    [Requirement.Requirement].[UserCreationId],
    [Requirement.Requirement].[UserLastModificationId],
    [Requirement.Requirement].[ClientId],
    [Requirement.Requirement].[Title],
    [Requirement.Requirement].[Body],
    [Requirement.Requirement].[RequirementStateId],
    [Requirement.Requirement].[RequirementTypeId],
    [Requirement.Requirement].[RequirementPriorityId],
    [Requirement.Requirement].[UserProgrammerId]
FROM 
    [Requirement.Requirement]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.Requirement].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.Requirement].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [Requirement.Requirement].[RequirementId]