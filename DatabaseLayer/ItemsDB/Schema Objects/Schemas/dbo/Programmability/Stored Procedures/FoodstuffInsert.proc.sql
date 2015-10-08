CREATE PROCEDURE [dbo].[FoodstuffInsert]
	@Name NCHAR(256),
	@Brand NCHAR(512),
	@Quantity FLOAT,
	@Units NCHAR(64),
	@ContainerID INT,
	@PersonID INT
AS
	INSERT INTO Foodstuff (Name, Brand, Quantity, Units, ContainerID, PersonID)
	VALUES (@Name, @Brand, @Quantity, @Units, @ContainerID, @PersonID)
RETURN 0