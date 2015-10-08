CREATE TABLE [dbo].[Person]
(
	PersonID int NOT NULL IDENTITY(1,1),
	Name NCHAR(256) NOT NULL,
	KitchenID INT NOT NULL,
	PRIMARY KEY ( PersonID ),
	CONSTRAINT FK_Person_KitchenID FOREIGN KEY ( KitchenID ) REFERENCES Kitchen ( KitchenID ),
	CONSTRAINT UN_Person_Name UNIQUE ( Name )
)