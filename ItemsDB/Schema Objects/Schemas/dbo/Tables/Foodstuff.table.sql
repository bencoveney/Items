CREATE TABLE [dbo].[Foodstuff]
(
	FoodstuffID int NOT NULL IDENTITY(1,1),
	Name NCHAR(256) NOT NULL,
	Brand NCHAR(512) NULL,
	Quantity FLOAT NOT NULL,
	Units NCHAR(64) NULL,
	ContainerID INT NOT NULL,
	PersonID INT NOT NULL,
	PRIMARY KEY ( FoodstuffID ),
	CONSTRAINT FK_Foodstuff_ContainerID FOREIGN KEY ( ContainerID ) REFERENCES Container ( ContainerID ),
	CONSTRAINT FK_Foodstuff_PersonID FOREIGN KEY ( PersonID ) REFERENCES Person ( PersonID )
)