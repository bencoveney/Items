CREATE PROCEDURE [dbo].[ContainerDelete]
	@ContainerID INT
AS
	DELETE FROM Container
	WHERE ContainerID = @ContainerID
RETURN 0