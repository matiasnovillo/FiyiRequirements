CREATE PROCEDURE [dbo].[Requirement.Application.Count]

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
DECLARE	@Counter int

EXEC	@Counter = [dbo].[Requirement.Application.Count]

SELECT	'Counter' = @Counter
 *
 */

--Last modification on: 27/12/2022 16:53:13

SELECT 
	COUNT(*)
FROM 
	[Requirement.Application]