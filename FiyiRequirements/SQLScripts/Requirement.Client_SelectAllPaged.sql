CREATE PROCEDURE [dbo].[Requirement.Client.SelectAllPaged]
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
EXEC [dbo].[Requirement.Client.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ClientId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:32

SET DATEFORMAT DMY
SET NOCOUNT ON

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
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.Client].[ClientId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[FirstName] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[LastName] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[BusinessName] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[PhoneNumber] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Client].[Email] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ClientId' AND @SortToggler = 0) THEN [Requirement.Client].[ClientId] END ASC,
    CASE WHEN (@SorterColumn = 'ClientId' AND @SortToggler = 1) THEN [Requirement.Client].[ClientId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.Client].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.Client].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.Client].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.Client].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.Client].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.Client].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.Client].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.Client].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.Client].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.Client].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'FirstName' AND @SortToggler = 0) THEN [Requirement.Client].[FirstName] END ASC,
    CASE WHEN (@SorterColumn = 'FirstName' AND @SortToggler = 1) THEN [Requirement.Client].[FirstName] END DESC,
    CASE WHEN (@SorterColumn = 'LastName' AND @SortToggler = 0) THEN [Requirement.Client].[LastName] END ASC,
    CASE WHEN (@SorterColumn = 'LastName' AND @SortToggler = 1) THEN [Requirement.Client].[LastName] END DESC,
    CASE WHEN (@SorterColumn = 'BusinessName' AND @SortToggler = 0) THEN [Requirement.Client].[BusinessName] END ASC,
    CASE WHEN (@SorterColumn = 'BusinessName' AND @SortToggler = 1) THEN [Requirement.Client].[BusinessName] END DESC,
    CASE WHEN (@SorterColumn = 'PhoneNumber' AND @SortToggler = 0) THEN [Requirement.Client].[PhoneNumber] END ASC,
    CASE WHEN (@SorterColumn = 'PhoneNumber' AND @SortToggler = 1) THEN [Requirement.Client].[PhoneNumber] END DESC,
    CASE WHEN (@SorterColumn = 'Email' AND @SortToggler = 0) THEN [Requirement.Client].[Email] END ASC,
    CASE WHEN (@SorterColumn = 'Email' AND @SortToggler = 1) THEN [Requirement.Client].[Email] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.Client]