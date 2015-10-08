CREATE PROCEDURE [dbo].[ContainerUpdate]
	@ContainerID INT,
	@Name NCHAR(256),
	@KitchenID INT,
	@ContainerCategoryID INT
AS
	UPDATE Container
	SET Name = @Name, KitchenID = @KitchenID, ContainerCategoryID = @ContainerCategoryID
	WHERE ContainerID = @ContainerID
RETURN 0