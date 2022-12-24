CREATE PROCEDURE [dbo].[Requirement.Client.Select1ByClientId]
(
    @ClientId INT
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
EXEC [dbo].[Client.Select1ByClientId]
    @ClientId = 1
 *
 */

--Last modification on: 24/12/2022 6:47:32

SET DATEFORMAT DMY

SELECT
    [Requirement.Client].[ClientId],
    [Requirement.Client].[Active],
    [Requirement.Client].[DateTimeCreation],
    [Requirement.Client].[DateTimeLastModification],
    [Requirement.Client].[UserCreationId],
    [Requirement.Client].[UserLastModificationId],
    [Requirement.Client].[FirstName],
    [Requirement.Client].[LastName],
    [Requirement.Client].[BusinessName],
    [Requirement.Client].[PhoneNumber],
    [Requirement.Client].[Email]
FROM 
    [Requirement.Client]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.Client].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.Client].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE 
    1 = 1
    AND [Requirement.Client].[ClientId] = @ClientId
ORDER BY 
    [Requirement.Client].[ClientId]