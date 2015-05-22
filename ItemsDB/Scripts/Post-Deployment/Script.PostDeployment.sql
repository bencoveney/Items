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
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Tiffanys Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Shared Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'The Freezer'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Sallys Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Shared Cupboard'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.ContainerPersonCollection ( ContainerID, PersonID ) SELECT Container.ContainerID, Person.PersonID FROM ItemsDB.dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Freezer'

PRINT N'Inserting FoodStuffs'

INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Sausages',			'Co-Op',			6,		'Sausages',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Bacon',				'Co-Op',			12,		'Rashers',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Diet Coke',			'Coca Cola',		4,		'Cans',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cheddar Cheese',		'Pilgrims Choice',	400,	'Grams',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Mozzarella Cheese',	'Co-Op',			320,	'Grams',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Broccoli',			NULL,				0.7,	NULL,		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cauliflower',			NULL,				0.5,	NULL,		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Tuna',				'Co-Op',			4,		'Tins',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'Bens Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Tea',					NULL,				4,		'Pots',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Ben' WHERE Container.Name = 'Bens Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Gammon',				'Tesco',			2,		'Steaks',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cider',				'Old Mout',			3,		'Bottles',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Chocolate',			'Cadbury',			2,		'Bars',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Tiffanys Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Bread Rolls',			'Co-Op',			4,		'Rolls',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Tiffanys Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Chocolate',			'Galaxy',			1,		'Bars',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Tiffanys Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Chips',				'McCain',			3,		'Bags',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Freezer'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Ice Cream',			'Carte Dor',		2,		'Tubs',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Freezer'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Vegetables',			'Tesco',			2,		'Bags',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Tiffany' WHERE Container.Name = 'Freezer'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cheese',				'Dairy lea',		8,		'Tris',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Sausages',			'Morrisons',		12,		'Sausages',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cider',				'Old Mout',			5,		'Bottles',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Bacon',				'Morrisons',		500,	'Grams',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'The Fridge'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cereal',				'Coco Pops',		2,		'Boxes',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Sallys Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Kidney Beans',		'Morrisons',		1,		'Tins',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Sallys Cupboard'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Vegetables',			'Tesco',			1,		'Bags',		ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Freezer'
INSERT INTO ItemsDB.dbo.Foodstuff ( Name, Brand, Quantity, Units, ContainerID, PersonID ) SELECT 'Cereal',				'Quakers Porridge',	24,		'Sachets',	ContainerID, PersonID FROM ItemsDB.Dbo.Container INNER JOIN ItemsDB.dbo.Person ON Person.Name = 'Sally' WHERE Container.Name = 'Shared Cupboard'
