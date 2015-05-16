/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE ItemsDB
GO

PRINT N'Inserting Kitchens'

INSERT INTO Kitchen (Name) VALUES ('The Kitchen')

PRINT N'Inserting Categories'

INSERT INTO ItemsDB.dbo.ContainerCategory ( ContainerCategory ) Values ('Cupboard' )
INSERT INTO ItemsDB.dbo.ContainerCategory ( ContainerCategory ) Values ('Fridge' )
INSERT INTO ItemsDB.dbo.ContainerCategory ( ContainerCategory ) Values ('Freezer' )

PRINT N'Inserting Containers'

INSERT INTO ItemsDB.dbo.Container ( Name, KitchenID, ContainerCategoryID ) SELECT 'Bens Cupboard', KitchenID, ContainerCategoryID FROM ItemsDB.dbo.Kitchen INNER JOIN ItemsDB.dbo.ContainerCategory ON ContainerCategory.ContainerCategory = 'Cupboard'
INSERT INTO ItemsDB.dbo.Container ( Name, KitchenID, ContainerCategoryID ) SELECT 'Tiffanys Cupboard', KitchenID, ContainerCategoryID FROM ItemsDB.dbo.Kitchen INNER JOIN ItemsDB.dbo.ContainerCategory ON ContainerCategory.ContainerCategory = 'Cupboard'
INSERT INTO ItemsDB.dbo.Container ( Name, KitchenID, ContainerCategoryID ) SELECT 'Sallys Cupboard', KitchenID, ContainerCategoryID FROM ItemsDB.dbo.Kitchen INNER JOIN ItemsDB.dbo.ContainerCategory ON ContainerCategory.ContainerCategory = 'Cupboard'
INSERT INTO ItemsDB.dbo.Container ( Name, KitchenID, ContainerCategoryID ) SELECT 'Shared Cupboard', KitchenID, ContainerCategoryID FROM ItemsDB.dbo.Kitchen INNER JOIN ItemsDB.dbo.ContainerCategory ON ContainerCategory.ContainerCategory = 'Cupboard'
INSERT INTO ItemsDB.dbo.Container ( Name, KitchenID, ContainerCategoryID ) SELECT 'The Fridge', KitchenID, ContainerCategoryID FROM ItemsDB.dbo.Kitchen INNER JOIN ItemsDB.dbo.ContainerCategory ON ContainerCategory.ContainerCategory = 'Fridge'
INSERT INTO ItemsDB.dbo.Container ( Name, KitchenID, ContainerCategoryID ) SELECT 'The Freezer', KitchenID, ContainerCategoryID FROM ItemsDB.dbo.Kitchen INNER JOIN ItemsDB.dbo.ContainerCategory ON ContainerCategory.ContainerCategory = 'Freezer'

PRINT N'Inserting People'

INSERT INTO ItemsDB.dbo.Person ( Name, KitchenID ) SELECT 'Ben', KitchenID FROM ItemsDB.dbo.Kitchen
INSERT INTO ItemsDB.dbo.Person ( Name, KitchenID ) SELECT 'Tiffany', KitchenID FROM ItemsDB.dbo.Kitchen
INSERT INTO ItemsDB.dbo.Person ( Name, KitchenID ) SELECT 'Sally', KitchenID FROM ItemsDB.dbo.Kitchen

PRINT N'Inserting ContainerPerson links'

INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'Bens Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'Shared Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Freezer'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Bens Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Shared Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'The Freezer'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Bens Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Shared Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Freezer'