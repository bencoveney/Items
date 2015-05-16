CREATE TABLE [dbo].[Kitchen]
(
	KitchenID int NOT NULL IDENTITY(1, 1),
	Name NCHAR(256) NOT NULL,
	PRIMARY KEY ( KitchenID ),
	CONSTRAINT UN_Kitchen_Name UNIQUE( Name )
)