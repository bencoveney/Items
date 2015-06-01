CREATE PROCEDURE [dbo].[FoodstuffDelete]
	@FoodstuffID INT
AS
	DELETE FROM Foodstuff
	WHERE FoodstuffID = @FoodstuffID
RETURN 0