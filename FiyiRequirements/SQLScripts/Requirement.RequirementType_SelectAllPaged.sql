CREATE PROCEDURE [dbo].[Requirement.RequirementType.SelectAllPaged]
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
EXEC [dbo].[Requirement.RequirementType.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementTypeId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:16

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.RequirementType].[RequirementTypeId],
    [Requirement.RequirementType].[Active],
    [Requirement.RequirementType].[DateTimeCreation],
    [Requirement.RequirementType].[DateTimeLastModification],
    [Requirement.RequirementType].[UserCreationId],
    [Requirement.RequirementType].[UserLastModificationId],
    [Requirement.RequirementType].[Name],
    [Requirement.RequirementType].[Description]
FROM 
    [Requirement.RequirementType]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementType].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementType].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.RequirementType].[RequirementTypeId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[Name] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementType].[Description] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementTypeId' AND @SortToggler = 0) THEN [Requirement.RequirementType].[RequirementTypeId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementTypeId' AND @SortToggler = 1) THEN [Requirement.RequirementType].[RequirementTypeId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.RequirementType].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.RequirementType].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.RequirementType].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.RequirementType].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.RequirementType].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.RequirementType].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.RequirementType].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.RequirementType].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.RequirementType].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.RequirementType].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 0) THEN [Requirement.RequirementType].[Name] END ASC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 1) THEN [Requirement.RequirementType].[Name] END DESC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 0) THEN [Requirement.RequirementType].[Description] END ASC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 1) THEN [Requirement.RequirementType].[Description] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementType]