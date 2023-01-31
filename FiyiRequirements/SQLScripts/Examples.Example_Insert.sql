CREATE PROCEDURE [dbo].[Examples.Example.Insert] 
(
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @Boolean TINYINT,
    @DateTime DATETIME,
    @Decimal NUMERIC(24,6),
    @ForeignKeyDropDown INT,
    @ForeignKeyOptions INT,
    @Integer INT,
    @TextBasic VARCHAR(8000),
    @TextEmail VARCHAR(8000),
    @TextFile VARCHAR(8000),
    @TextHexColour VARCHAR(6),
    @TextPassword VARCHAR(8000),
    @TextPhoneNumber VARCHAR(8000),
    @TextTag VARCHAR(8000),
    @TextTextArea VARCHAR(8000),
    @TextTextEditor VARCHAR(8000),
    @TextURL VARCHAR(8000),
    @Time TIME(3),

    @NewEnteredId INT OUTPUT
)

AS

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

/*
 * Execute this stored procedure with the next script as example
 *
DECLARE	@NewEnteredId INT
EXEC [dbo].[Examples.Example.Insert]

    @Active = 1,
    @DateTimeCreation = N'01/01/1753 0:00:00.001',
    @DateTimeLastModification = N'01/01/1753 0:00:00.001',
    @UserCreationId = 1,
    @UserLastModificationId = 1,
    @Boolean = 1,
    @DateTime = N'01/01/1753 0:00:00.001',
    @Decimal = 3.14,
     @ForeignKeyDropDown = 1,
    @ForeignKeyOptions = 1,
    @Integer = 1,
    @TextBasic = N'PutTextBasic',
    @TextEmail = N'PutTextEmail',
    @TextFile = N'PutTextFile',
    @TextHexColour = AABBCC,
    @TextPassword = N'PutTextPassword',
    @TextPhoneNumber = N'PutTextPhoneNumber',
    @TextTag = N'PutTextTag',
    @TextTextArea = N'PutTextTextArea',
    @TextTextEditor = N'PutTextTextEditor',
    @TextURL = N'PutTextURL',
    @Time = N'00:00:00.001',

@NewEnteredId = @NewEnteredId OUTPUT

SELECT @NewEnteredId AS N'@NewEnteredId'
 *
 */

--Last modification on: 31/01/2023 7:54:01

INSERT INTO [Examples.Example]
(
    [Active],
    [DateTimeCreation],
    [DateTimeLastModification],
    [UserCreationId],
    [UserLastModificationId],
    [Boolean],
    [DateTime],
    [Decimal],
    [ForeignKeyDropDown],
    [ForeignKeyOptions],
    [Integer],
    [TextBasic],
    [TextEmail],
    [TextFile],
    [TextHexColour],
    [TextPassword],
    [TextPhoneNumber],
    [TextTag],
    [TextTextArea],
    [TextTextEditor],
    [TextURL],
    [Time]
)
VALUES
(
    @Active,
    @DateTimeCreation,
    @DateTimeLastModification,
    @UserCreationId,
    @UserLastModificationId,
    @Boolean,
    @DateTime,
    @Decimal,
    @ForeignKeyDropDown,
    @ForeignKeyOptions,
    @Integer,
    @TextBasic,
    @TextEmail,
    @TextFile,
    @TextHexColour,
    @TextPassword,
    @TextPhoneNumber,
    @TextTag,
    @TextTextArea,
    @TextTextEditor,
    @TextURL,
    @Time
)

SELECT @NewEnteredId = @@IDENTITY