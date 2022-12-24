CREATE PROCEDURE [dbo].[Requirement.Technology.Select1ByTechnologyId]
(
    @TechnologyId INT
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
EXEC [dbo].[Technology.Select1ByTechnologyId]
    @TechnologyId = 1
 *
 */

--Last modification on: 24/12/2022 6:47:20

SET DATEFORMAT DMY

SELECT
    [Requirement.Technology].[TechnologyId],
    [Requirement.Technology].[Active],
    [Requirement.Technology].[DateTimeCreation],
    [Requirement.Technology].[DateTimeLastModification],
    [Requirement.Technology].[UserCreationId],
    [Requirement.Technology].[UserLastModificationId],
    [Requirement.Technology].[Name],
    [Requirement.Technology].[Description]
FROM 
    [Requirement.Technology]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.Technology].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.Technology].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE 
    1 = 1
    AND [Requirement.Technology].[TechnologyId] = @TechnologyId
ORDER BY 
    [Requirement.Technology].[TechnologyId]