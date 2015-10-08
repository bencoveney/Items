CREATE TABLE [dbo].[ContainerCategory]
(
	ContainerCategoryID int NOT NULL IDENTITY(1, 1),
	ContainerCategory NCHAR(256) NOT NULL
	PRIMARY KEY ( ContainerCategoryID )
)
