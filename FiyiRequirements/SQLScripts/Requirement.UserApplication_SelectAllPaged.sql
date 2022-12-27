CREATE PROCEDURE [dbo].[Requirement.UserApplication.SelectAllPaged]
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
EXEC [dbo].[Requirement.UserApplication.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'UserApplicationId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 27/12/2022 16:32:18

SET DATEFORMAT DMY
SET NOCOUNT ON

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
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.UserApplication].[UserApplicationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[ApplicationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.UserApplication].[UserId] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'UserApplicationId' AND @SortToggler = 0) THEN [Requirement.UserApplication].[UserApplicationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserApplicationId' AND @SortToggler = 1) THEN [Requirement.UserApplication].[UserApplicationId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.UserApplication].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.UserApplication].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.UserApplication].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.UserApplication].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.UserApplication].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.UserApplication].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.UserApplication].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.UserApplication].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.UserApplication].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.UserApplication].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'ApplicationId' AND @SortToggler = 0) THEN [Requirement.UserApplication].[ApplicationId] END ASC,
    CASE WHEN (@SorterColumn = 'ApplicationId' AND @SortToggler = 1) THEN [Requirement.UserApplication].[ApplicationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserId' AND @SortToggler = 0) THEN [Requirement.UserApplication].[UserId] END ASC,
    CASE WHEN (@SorterColumn = 'UserId' AND @SortToggler = 1) THEN [Requirement.UserApplication].[UserId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.UserApplication]