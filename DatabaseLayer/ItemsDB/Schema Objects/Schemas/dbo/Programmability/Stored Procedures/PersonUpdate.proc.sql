CREATE PROCEDURE [dbo].[PersonUpdate]
	@PersonID INT,
	@Name NCHAR(256),
	@KitchenID INT
AS
	UPDATE Person
	SET Name = @Name, KitchenID = @KitchenID
	WHERE PersonID = @PersonID
RETURN 0