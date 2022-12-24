CREATE PROCEDURE [dbo].[Requirement.ClientApplication.SelectAll]

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
EXEC [dbo].[Requirement.ClientApplication.SelectAll]
 *
 */

--Last modification on: 24/12/2022 6:47:41

SET DATEFORMAT DMY

SELECT
    [Requirement.ClientApplication].[ClientApplicationId],
    [Requirement.ClientApplication].[Active],
    [Requirement.ClientApplication].[DateTimeCreation],
    [Requirement.ClientApplication].[DateTimeLastModification],
    [Requirement.ClientApplication].[UserCreationId],
    [Requirement.ClientApplication].[UserLastModificationId],
    [Requirement.ClientApplication].[ClientId],
    [Requirement.ClientApplication].[ApplicationId]
FROM 
    [Requirement.ClientApplication]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.ClientApplication].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.ClientApplication].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [Requirement.ClientApplication].[ClientApplicationId]