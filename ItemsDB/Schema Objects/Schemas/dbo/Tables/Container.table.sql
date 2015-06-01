CREATE TABLE [dbo].[Container]
(
	ContainerID INT NOT NULL IDENTITY(1,1), 
	Name NCHAR(256) NOT NULL,
	KitchenID INT NOT NULL,
	ContainerCategoryID INT NOT NULL,
	PRIMARY KEY ( ContainerID ),
	CONSTRAINT FK_Container_KitchenID FOREIGN KEY ( KitchenID ) REFERENCES Kitchen ( KitchenID ),
	CONSTRAINT FK_Container_ContainerCategoryID FOREIGN KEY ( ContainerCategoryID ) REFERENCES ContainerCategory ( ContainerCategoryID ),
	CONSTRAINT UN_Container_Name_Kitchen UNIQUE( Name, KitchenID )
)