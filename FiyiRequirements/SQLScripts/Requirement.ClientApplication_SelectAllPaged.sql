CREATE PROCEDURE [dbo].[Requirement.ClientApplication.SelectAllPaged]
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
EXEC [dbo].[Requirement.ClientApplication.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ClientApplicationId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:41

SET DATEFORMAT DMY
SET NOCOUNT ON

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
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.ClientApplication].[ClientApplicationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[ClientId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.ClientApplication].[ApplicationId] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ClientApplicationId' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[ClientApplicationId] END ASC,
    CASE WHEN (@SorterColumn = 'ClientApplicationId' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[ClientApplicationId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'ClientId' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[ClientId] END ASC,
    CASE WHEN (@SorterColumn = 'ClientId' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[ClientId] END DESC,
    CASE WHEN (@SorterColumn = 'ApplicationId' AND @SortToggler = 0) THEN [Requirement.ClientApplication].[ApplicationId] END ASC,
    CASE WHEN (@SorterColumn = 'ApplicationId' AND @SortToggler = 1) THEN [Requirement.ClientApplication].[ApplicationId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.ClientApplication]