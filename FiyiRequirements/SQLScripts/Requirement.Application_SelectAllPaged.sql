CREATE PROCEDURE [dbo].[Requirement.Application.SelectAllPaged]
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
EXEC [dbo].[Requirement.Application.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ApplicationId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 27/12/2022 16:53:13

SET DATEFORMAT DMY
SET NOCOUNT ON

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
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.Application].[ApplicationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[Name] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[Description] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Application].[TechnologyId] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ApplicationId' AND @SortToggler = 0) THEN [Requirement.Application].[ApplicationId] END ASC,
    CASE WHEN (@SorterColumn = 'ApplicationId' AND @SortToggler = 1) THEN [Requirement.Application].[ApplicationId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.Application].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.Application].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.Application].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.Application].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.Application].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.Application].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.Application].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.Application].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.Application].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.Application].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 0) THEN [Requirement.Application].[Name] END ASC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 1) THEN [Requirement.Application].[Name] END DESC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 0) THEN [Requirement.Application].[Description] END ASC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 1) THEN [Requirement.Application].[Description] END DESC,
    CASE WHEN (@SorterColumn = 'TechnologyId' AND @SortToggler = 0) THEN [Requirement.Application].[TechnologyId] END ASC,
    CASE WHEN (@SorterColumn = 'TechnologyId' AND @SortToggler = 1) THEN [Requirement.Application].[TechnologyId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.Application]