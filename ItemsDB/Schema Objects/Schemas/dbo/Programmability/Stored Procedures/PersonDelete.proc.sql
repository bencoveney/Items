CREATE PROCEDURE [dbo].[PersonDelete]
	@PersonID INT
AS
	DELETE FROM Person
	WHERE PersonID = @PersonID
RETURN 0