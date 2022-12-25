CREATE PROCEDURE [dbo].[Requirement.Client.UpdateByClientId]
(
    @ClientId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @BusinessName VARCHAR(100),
    @PhoneNumber VARCHAR(100),
    @Email VARCHAR(300),

    @RowsAffected INT OUTPUT
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
DECLARE	@RowsAffected int
EXEC [dbo].[Requirement.Client.UpdateByClientId]
    @ClientId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 24/12/2022 6:47:32

UPDATE [Requirement.Client] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [FirstName] = @FirstName,
    [LastName] = @LastName,
    [BusinessName] = @BusinessName,
    [PhoneNumber] = @PhoneNumber,
    [Email] = @Email
WHERE 
    1 = 1 
    AND [Requirement.Client].[ClientId] = @ClientId 

SELECT @RowsAffected = @@ROWCOUNT