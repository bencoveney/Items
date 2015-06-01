CREATE PROCEDURE [dbo].[ContainerInsert]
	@Name NCHAR(256),
	@KitchenID INT,
	@ContainerCategoryID INT
AS
	INSERT INTO Container(Name, KitchenID, ContainerCategoryID)
	VALUES (@Name, @KitchenID, @ContainerCategoryID)
RETURN 0