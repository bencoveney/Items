CREATE PROCEDURE [dbo].[FoodstuffUpdate]
	@FoodstuffID INT,
	@Name NCHAR(256),
	@Brand NCHAR(512),
	@Quantity FLOAT,
	@Units NCHAR(64),
	@ContainerID INT,
	@PersonID INT
AS
	UPDATE Foodstuff
	SET Name = @Name, Brand = @Brand, Quantity = @Quantity, Units = @Units, ContainerID = @ContainerID, PersonID = @PersonID
	WHERE FoodstuffID = @FoodstuffID
RETURN 0