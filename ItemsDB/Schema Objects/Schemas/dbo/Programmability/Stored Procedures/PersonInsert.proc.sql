CREATE PROCEDURE [dbo].[PersonInsert]
	@Name NCHAR(256),
	@KitchenID INT
AS
	INSERT INTO Person (Name, KitchenID)
	VALUES (@Name, @KitchenID)
RETURN 0