Items
=====

Items is a toolkit designed for building (and applying) domain specific models backed by database schemas.

Project files
-------------
- `/core` The model building objects.
- `/legacy` Old projects that have not been updated.
  - `/ItemLibGen` Code generator which produces a C# dll for a given model.
  - `/ItemLoader` Item classes specific to a database context, with factory functionality for building a model from them.
  - `/ItemSelector` Query generation for database layer items.
  - `/ItemTests` Unit test code.

TODO List
---------

- [ ] Currently anything can be added to the ImplementationDetailsDictionary. It might be better to have a schema concept inside it which dictates what can be added and removed (For example the database loader might specift that the attribute details object takes a string called column name). This would help mitigate the fact that using an ImplementationDetailsDictionary removes a lot of type safety.
- [ ] Think about alternate ways of loading the database. It was one long file, and has now been split out somewhat into the classes in the Database Loader project however there is still a lot of logic in one very long method (in DatabaseModel.cs) and it doesn't allow for customisation (ie it is only tailored for the test database).
- [ ] More widespread implementation of ToString to aid debugging.
- [ ] More widespread code quality tools.
  - Currently only Items is under code analysis and (partial) unit testing.
  - Currently only Items and ItemsLoader are under coding styles (stylecop).
- [ ] Code Generation.
  - Probably best left until the project has matured and the object model is more stable.
  - Will require some interesting unit testing.
- [ ] Query Generation. This is specifying what you want in the model and then generating SQL queries from that to run on the database.
- [ ] Better building of .aspx pages. All text templating was removed when transitioning from "bootstrapper" project but it may be a good idea to bring some back to remove the big mess of Response.Write statements. There may be alternatives in ASP (components?).
- [ ] ItemsWeb needs to fully represent the model generated. Currently the relationships page is sparse.
- [ ] Plan for different "stages" of model building. May want to generate the model from multiple different sources at different times.
  - [ ] Adding to the model can be expressed in xml/code/however and then "unioned".
  - [ ] Removing/changing could be more complicated.
    - [ ] Could be required to remove/change implementation details from generated base model (e.g. additional columns, truncated oracle names).
  - [ ] Would be valuable to be able to express these changes as transforms.
    - [ ] Would allow them to be re-applied after regenerating from a changed database.
    - [ ] Would allow you to check them in.
    - [ ] Would allow you to get visibility of how your database implementation deviates from the pure model.
- [ ] Split code/xml/text generation out better into files
- [ ] A lot of classes should be using the NamedCollection (ie Item in the Model, Relationship in the Model). The model should probably only have one internal named collection as well.
- [ ] Add relationships to this document.
- [ ] Add behaviors to this document.
- [ ] Anything that has a name should be an INamedItem
- [ ] Anything that takes a string as a param (eg for name) should check it for null/empty

Model
-----

Represents the entire domain model and everything it contains

#### Todo

- [ ] Validation

Item
----

Represents an object in the model. Has data attributes and behaviours. Allows (admittedly not great) specification of which attributes can be used to identify instances of the item.

- [ ] The way in which attributes are marked as identifiers could be improved.
- [ ] Singletons.
- [ ] Support for static behaviour (not tied to an instance of an object but related to it).

Data Members
----------

A named attribute for an item. Allows specification of what type of data this attribute contains and what constraints are placed on that data.

There is a concept of nullability which defines whether the attribute accepts different types of null values (Not applicable, Applicable but empty).

There are two types of attribute: Value attributes and Collection attributes. Value attributes are where each instance of an item has one value for the attribute. Collection attributes are where each instance of an item has multiple values for the attribute.

#### Todo

- [ ] Nullability isn't specified in a great way.
  - [ ] Nullability is essentially a constraint and should be represented as such.
  - [ ] Applicable but empty is usually going to be an in-band value however there is no way for specifying this.
  - [ ] Maybe allow the specification of "special values" such as empty, default, other.
- [ ] Some attributes should have a default value (even Items).
- [ ] There could be value in providing "calculated" attributes which define an operation to perform in order to get a value/collection. Maybe a special case of behaviour?

Types
-----

Defines what type of data an attribute represents. This should only be a C# type now that we have relationships.

#### Todo

- [ ] Types currently have a Name however this might be redundant.

Constraints
-----------

Constraints dictate rules about what data (not what type of data) can be put in an attribute. Types of constraint currently implemented include:
* Numeric Value - Is generic, allows for dates etc.
  * ==
  * >
  * <
  * >=
  * <=
  * !=
  * %
  * !%
* string Length
  * Longer than
  * Shorter than
  * Exactly (could be a combination)
* string value - Allows specification of comparison culture
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

- [ ] Logical operators to allow grouping of operators.
  - [ ] (Check1 AND Check2) OR (Check1 AND Check3).
  - [ ] NOT(%) instead of !%.
- [ ] Attribute value constraints.
  - [ ] Check against a value of another attribute for a specific item instance.
  - [ ] eg. This startDate must be before this endDate.
  - [ ] This isn't a great name for this.
- [ ] Constraints which apply to multiple attributes.
  - [ ] For example wanting to check that the combination of Attribute A and Attribute B is unique rather than just each individually. 
  - [ ] Could this just be check Attribute A is unique WHERE items have a matching Attribute B.
  - [ ] Really don't want to re-implement SQL.
- [ ] Would there be use in allowing predicates on collection constraints/elsewhere? Needs more thought.
- [ ] "Commited".
  - [ ] Some constraints should always be enforced however some are only relevant once the item instance becomes "commited" to the model.
  - [ ] An example of this is ID, which should probably only populated and validated when the object is added to the database.
  - [ ] Database has deferred constraints, maybe look into leveraging this?
- [ ] Nullability and Constraints have overlapping responsibility.
- [ ] Constraints can overlap in responsibility.
- [ ] string value # of instances of in addition to contains.
- [ ] Allow you allow/disallow the default value?

Category
--------

Represents a set of categorisations. These are designed to parallel enumerations in that they have attributes (however unlike c# enums they can have data additional to the name and number) but do not have any behavior associated. Instances of them (eg new values for the category) probably shouldn't be added/removed at runtime, however it might not be beneficial to universally restrict this (maybe allow as a special case?).

#### Todo

- [ ] Flags?
- [ ] If the category's values are constant then they should probably be part of the model rather than part of the instance od the model.