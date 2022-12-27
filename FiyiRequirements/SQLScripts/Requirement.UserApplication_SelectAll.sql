CREATE PROCEDURE [dbo].[Requirement.UserApplication.SelectAll]

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
EXEC [dbo].[Requirement.UserApplication.SelectAll]
 *
 */

--Last modification on: 27/12/2022 16:32:18

SET DATEFORMAT DMY

SELECT
    [Requirement.UserApplication].[UserApplicationId],
    [Requirement.UserApplication].[Active],
    [Requirement.UserApplication].[DateTimeCreation],
    [Requirement.UserApplication].[DateTimeLastModification],
    [Requirement.UserApplication].[UserCreationId],
    [Requirement.UserApplication].[UserLastModificationId],
    [Requirement.UserApplication].[ApplicationId],
    [Requirement.UserApplication].[UserId]
FROM 
    [Requirement.UserApplication]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.UserApplication].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.UserApplication].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [Requirement.UserApplication].[UserApplicationId]