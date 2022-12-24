CREATE PROCEDURE [dbo].[Requirement.Technology.SelectAllPaged]
(
    @QueryString VARCHAR(100),
    @ActualPageNumber INT,
    @RowsPerPage INT,
    @SorterColumn VARCHAR(100),
    @SortToggler TINYINT,
    @TotalRows INT OUTPUT
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

/*Execute this stored procedure with the next script as example

DECLARE	@TotalRows int
EXEC [dbo].[Requirement.Technology.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'TechnologyId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:20

SET DATEFORMAT DMY
SET NOCOUNT ON

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
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.Technology].[TechnologyId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[Name] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Technology].[Description] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'TechnologyId' AND @SortToggler = 0) THEN [Requirement.Technology].[TechnologyId] END ASC,
    CASE WHEN (@SorterColumn = 'TechnologyId' AND @SortToggler = 1) THEN [Requirement.Technology].[TechnologyId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.Technology].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.Technology].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.Technology].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.Technology].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.Technology].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.Technology].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.Technology].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.Technology].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.Technology].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.Technology].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 0) THEN [Requirement.Technology].[Name] END ASC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 1) THEN [Requirement.Technology].[Name] END DESC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 0) THEN [Requirement.Technology].[Description] END ASC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 1) THEN [Requirement.Technology].[Description] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.Technology]