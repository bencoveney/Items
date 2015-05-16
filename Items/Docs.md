Items
=====

General todo list:

	* Maybe the implementation specific details should be stored in a dictionary rather than in partial classes.
		* Currently makes the object model messy and spreads implementation out incohesively.
		* Currently has DB implementation details in the Items project.
		* Examples: "description": "blah blah", "sql datatype": "blah blah", "sql column": "blah blah".
		* Might still need to mess up object model in order to add functionality instead of data.
	* Develop a smarter way of loading from the database rather than one long messy file.
	* Generate code.
	* Plan for different "stages" of model building. May want to generate the model from multiple different sources at different times.
		* Adding to the model can be expressed in xml/code/however and then "unioned".
		* Removing/changing could be more complicated.
			* Could be required to remove/change implementation details from generated base model (e.g. additional columns, truncated oracle names).
		* Would be valuable to be able to express these changes as transforms.
			* Would allow them to be re-applied after regenerating from a changed database.
			* Would allow you to check them in.
			* Would allow you to get visibility of how your database implementation deviates from the pure model.
	* Split code/xml/text generation out better into files.
	* All items are stringly identified rather than done by reference.
		* Makes building the model much simpler but causes the need to validation (this may be required anyway).
	* Serialisation and deserialisation via text templates probably isn't a good idea.
	* Loader should use enums for columns. all db code could be improved.
	* Classes like Attributes and Behaviors (in order to string index things) could be improved
	* Behavior lol.
	* Don't re-implement C#.
	* Don't re-implement SQL

Class
-----

### Implementation

Details

#### Todo

	*

### Loading from Database

Details

### Generating Xml

Details

### Generating Code

Not implemented.

### Generating Documentation

Details

Model
-----

### Implementation

Mainly just a simple data container.

#### Todo

	* Model validation

### Loading from Database

Instance is created rather than loaded.

### Generating Xml

Not applicable.

### Generating Code

Not implemented.

### Generating Documentation

Not applicable.

Item
----

### Implementation

Represents an object in the model. Has data attributes and behaviours. Allows (admittedly not great) specification of which attributes can be used to identify instances of the item.

#### Todo

	* Identifiers should have to have certain constraints (eg unique).
	* The way in which attributes are marked as identifiers could be improved.
	* Singletons.
	* Support for static behaviour (not tied to an instance of an object but related to it).

### Loading from Database

Done, loads table names.

### Generating Xml

Done, creates item nodes.

### Generating Code

Not implemented.

### Generating Documentation

Could be improved by adding support for things like a description.

Attributes
----------

### Implementation

A named data member for an item. Allows specification of what type of data this attribute contains and what constraints are placed on that data.

There is a concept of nullability which defines whether the attribute accepts different types of null values (Not applicable, Applicable but empty).

There are two types of attribute: Value attributes and Collection attributes. Value attributes are where each instance of an item has one value for the attribute. Collection attributes are where each instance of an item has multiple values for the attribute.

#### Todo

	* Nullability isn't specified in a great way.
		* Nullability is essentially a constraint and should be represented as such.
		* Applicable but empty is usually going to be an in-band value however there is no way for specifying this.
	* I have chosen to represent One to Many and Many to Many relationships as a collection attribute on one or both items however other models use a relationship concept which may have merit (and simplify parsing collection tables).
	* Some attributes should have a default value (even Items).
	* There could be value in providing "calculated" attributes which define an operation to perform in order to get a value/collection. Maybe a special case of behaviour?

### Loading from Database

Columns get loaded as attributes. Foreign keys which reference the table are assumed to be collections. Collection tables (which have a many to many relationship) are not loaded.

### Generating Xml

Implemented.

### Generating Code

Not implemented.

### Generating Documentation

Could be improved by adding support for things like a description.

Types
-----

### Implementation

Defines what type of data an attribute represents. This can either be a C# type (system type), an item or a category.

#### Todo

	* Category type implementation.
	* Types currently have a Name however this might be redundant.

### Loading from Database

System types are calculated by looking at foreign keys and calculating what 

### Generating Xml

Implemented however TypeToXml() should be inside the template really.

### Generating Code

Not implemented.

### Generating Documentation

Implemented however PrettifyType() should be inside the template really.

Constraints
-----------

### Implementation

Constraints dictate rules about what data (not what type of data) can be put in an attribute. Types of constraint currently implemented include:
	* Numeric Value - Is generic, allows for dates etc (might fuck up on %).
		* ==
		* >
		* <
		* >=
		* <=
		* !=
		* %
		* !%
	* String Length
		* Longer than
		* Shorter than
		* Exactly (could be a combination)
	* String value - Allows specification of comparison culture
		* Match
		* Doesn't match
		* Begins with
		* Doesn't begin with
		* Ends with
		* Doesn't end with
		* Contains
		* Doesn't contain
		* Is contained by
		* Is not contained by
		* Matched regex
		* Doesn't match regex
	* Attribute - Checks that your value is/n't present in all instances of an attribute
		* Exists in
		* Doesn't exist in
		* Is unique within

#### Todo

	* Logical operators to allow grouping of operators.
		* (Check1 AND Check2) OR (Check1 AND Check3).
		* NOT(%) instead of !%.
	* Attribute value constraints.
		* Check against a value of another attribute for a specific item instance.
		* eg. This startDate must be before this endDate.
		* This isn't a great name for this.
	* Constraints which apply to multiple attributes.
		* For example wanting to check that the combination of Attribute A and Attribute B is unique rather than just each individually. 
		* Could this just be check Attribute A is unique WHERE items have a matching Attribute B.
		* Really don't want to re-implement SQL.
	* Would there be use in allowing predicates on collection constraints/elsewhere? Needs more thought.
	* "Commited".
		* Some constraints should always be enforced however some are only relevant once the item instance becomes "commited" to the model.
		* An example of this is ID, which should probably only populated and validated when the object is added to the database.
	* Nullability and Constraints have overlapping responsibility.
	* Constraints can overlap in responsibility.
	* String value # of instances of in addition to contains.
	* Allow you allow/disallow the default value?

### Loading from Database

String length is loaded from the column definition. Unique constraints are used to apply unique attribute constraints.

Null values arent enforced here (enforced by nullability instead) but some useful info could be inferred from it.

Foreign keys are not currently used as constraints as we don't need to specify that kitchen.containerid points to container.containerid. Instead we can just say that kitchen contains a container. We might want to enforce null values?

### Generating Xml

Numeric Value Constraint isn't correctly writing the generic type info.

### Generating Code

Not implemented.

### Generating Documentation

Implementation is a mess and only "finished" for attribute constraints.

Category
--------

### Implementation

Represents a set of categorisations. These are designed to parallel enumerations in that they have attributes (however unlike c# enums they can have data additional to the name and number) but do not have any behavior associated. Instances of them (eg new values for the category) probably shouldn't be added/removed at runtime, however it might not be beneficial to universally restrict this (maybe allow as a special case?).

#### Todo

	* Flags?

### Loading from Database

Categories are loaded and constraints are added using the same logic as items.

### Generating Xml

Not implemented.

### Generating Code

Not implemented.

### Generating Documentation

Not implemented.