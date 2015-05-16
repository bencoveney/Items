CREATE TABLE [dbo].[ContainerPersonCollection]
(
	ContainerPersonCollectionID int NOT NULL IDENTITY(1,1),
	PersonID INT NOT NULL,
	ContainerID int NOT NULL,
	PRIMARY KEY ( ContainerPersonCollectionID ),
	CONSTRAINT FK_ContainerPersonCollection_PersonID FOREIGN KEY ( PersonID ) REFERENCES Person ( PersonID ),
	CONSTRAINT FK_ContainerPersonCollection_ContainerID FOREIGN KEY ( ContainerID ) REFERENCES Container ( ContainerID )
)