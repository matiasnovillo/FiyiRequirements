CREATE PROCEDURE [dbo].[Requirement.Application.SelectAll]

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
EXEC [dbo].[Requirement.Application.SelectAll]
 *
 */

--Last modification on: 24/12/2022 6:47:27

SET DATEFORMAT DMY

SELECT
    [Requirement.Application].[ApplicationId],
    [Requirement.Application].[Active],
    [Requirement.Application].[DateTimeCreation],
    [Requirement.Application].[DateTimeLastModification],
    [Requirement.Application].[UserCreationId],
    [Requirement.Application].[UserLastModificationId],
    [Requirement.Application].[Name],
    [Requirement.Application].[Description],
    [Requirement.Application].[TechnologyId]
FROM 
    [Requirement.Application]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.Application].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.Application].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [Requirement.Application].[ApplicationId]