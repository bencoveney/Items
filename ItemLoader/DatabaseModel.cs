namespace ItemLoader
{
	using System.Collections.Generic;
	using System.Linq;
	using Items;

	/// <summary>
	/// Singleton class to represent an entire database and it's objects.
	/// </summary>
	public static class DatabaseModel
	{
		/// <summary>
		/// Initializes static members of the <see cref="DatabaseModel"/> class.
		/// </summary>
		static DatabaseModel()
		{
			DatabaseModel.Tables = new List<DatabaseTable>();
			DatabaseModel.Routines = new List<DatabaseRoutine>();
		}

		/// <summary>
		/// Gets the tables represented in the database.
		/// </summary>
		/// <value>
		/// The tables.
		/// </value>
		public static List<DatabaseTable> Tables
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the routines represented in the database.
		/// </summary>
		/// <value>
		/// The routines.
		/// </value>
		public static List<DatabaseRoutine> Routines
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets all columns for all tables in the database.
		/// </summary>
		/// <value>
		/// The columns.
		/// </value>
		public static IEnumerable<DatabaseColumn> AllColumns
		{
			get
			{
				return Tables.SelectMany(table => table.Columns);
			}
		}

		/// <summary>
		/// Gets all constraints for all tables in the database.
		/// </summary>
		/// <value>
		/// The constraints.
		/// </value>
		public static IEnumerable<DatabaseConstraint> AllConstraints
		{
			get
			{
				return Tables.SelectMany(table => table.Constraints);
			}
		}

		/// <summary>
		/// Gets all parameters for all routines in the database.
		/// </summary>
		/// <value>
		/// All parameters.
		/// </value>
		public static IEnumerable<DatabaseRoutineParameter> AllParameters
		{
			get
			{
				return Routines.SelectMany(routine => routine.Parameters);
			}
		}

		/// <summary>
		/// Loads the objects from the database.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		public static void LoadFromDatabase(string connectionString)
		{
			// Load the database objects
			DatabaseTable.LoadTables(connectionString);
			DatabaseRoutine.LoadRoutines(connectionString);
		}

		/// <summary>
		/// Constructs a model from the loaded database objects.
		/// </summary>
		/// <returns>A model build from the schema</returns>
		public static Model ConstructModel()
		{
			Model result = new Model();

			// Populate base items
			foreach (DatabaseTable table in Tables.Where(table => table.RepresentsItem))
			{
				Item item = new Item(table.Name);

				// Add additional details
				item.Details.Add("SqlCatalog", table.Catalog);
				item.Details.Add("SqlSchema", table.Schema);
				item.Details.Add("SqlTable", table.Name);

				// Add columns (which aren't relationships)
				foreach (DatabaseColumn column in table.Columns.Where(dbColumn => !dbColumn.IsReferencer && !dbColumn.IsReferenced))
				{
					DataMember dataAttribute = new DataMember(column.Name, column.Type.GetSystemType(), column.GetNullability());

					// Populate implementation details
					dataAttribute.Details.Add("SqlColumn", column.Name);
					dataAttribute.Details.Add("OrdinalPosition", column.OrdinalPosition);
					dataAttribute.Details.Add("DefaultValue", column.ColumnDefault);

					// Populate constraints which can be inferred from the type
					foreach (IConstraint constraint in column.Type.GetConstraints())
					{
						dataAttribute.Constraints.Add(constraint);
					}

					item.Attributes.Add(dataAttribute);

					// If this is the primary key we definitely want to mark it as the integer identifier
					if (column.ReferencingConstraints.Any(constraint => constraint.Type == ConstraintType.PrimaryKey) && column.Type.IsInteger)
					{
						item.IntegerIdentifier = dataAttribute;
					}

					// If this column is enforced as unique we can assume it can be used to identify the item
					if (column.ReferencingConstraints.Any(constraint => constraint.Type == ConstraintType.Unique))
					{
						// Assign the integer identifier if there isnt one and this attribute is suitable
						if (item.IntegerIdentifier == null && column.Type.IsInteger)
						{
							item.IntegerIdentifier = dataAttribute;
						}

						// Assign the string identifier if there isnt one and this attribute is suitable
						if (item.StringIdentifier == null && column.Type.IsText)
						{
							item.StringIdentifier = dataAttribute;
						}
					}
				}

				result.AddItem(item);
			}

			// Populate categories
			foreach (DatabaseTable table in Tables.Where(table => table.RepresentsCategory))
			{
				Category category = new Category(table.Name);

				// Add additional details
				category.Details.Add("SqlCatalog", table.Catalog);
				category.Details.Add("SqlSchema", table.Schema);
				category.Details.Add("SqlTable", table.Name);

				// Add columns (which aren't relationships)
				foreach (DatabaseColumn column in table.Columns.Where(dbColumn => !dbColumn.IsReferencer && !dbColumn.IsReferenced))
				{
					DataMember dataAttribute = new DataMember(column.Name, column.Type.GetSystemType(), column.GetNullability());

					// Populate implementation details
					dataAttribute.Details.Add("SqlColumn", column.Name);
					dataAttribute.Details.Add("OrdinalPosition", column.OrdinalPosition);
					dataAttribute.Details.Add("DefaultValue", column.ColumnDefault);

					// Populate constraints which can be inferred from the type
					foreach (IConstraint constraint in column.Type.GetConstraints())
					{
						dataAttribute.Constraints.Add(constraint);
					}

					category.Attributes.Add(dataAttribute);
				}

				result.AddCategory(category);
			}

			// Populate relationships from link tables
			foreach (DatabaseTable table in Tables.Where(t => t.RepresentsRelationship))
			{
				// Find the first columns which are foreign keys and assume those are the right and left sides of the relationship
				IEnumerable<DatabaseColumn> columnsWithForeignKeys = table.Columns.Where(column => column.IsReferencer);

				DatabaseColumn leftColumn = columnsWithForeignKeys.OrderBy(column => column.OrdinalPosition).ToList()[0];
				Thing leftThing = result.Things.Single(thing => leftColumn.ReferencedColumn.Table.IsThingMatch(thing));

				DatabaseColumn rightColumn = columnsWithForeignKeys.OrderBy(column => column.OrdinalPosition).ToList()[1];
				Thing rightThing = result.Things.Single(thing => rightColumn.ReferencedColumn.Table.IsThingMatch(thing));

				Relationship relationship = new Relationship(table.Name, leftThing, rightThing);

				// Add additional details
				relationship.Details.Add("SqlCatalog", table.Catalog);
				relationship.Details.Add("SqlSchema", table.Schema);
				relationship.Details.Add("SqlTable", table.Name);

				result.AddRelationship(relationship);

				// TODO Populate Attributes for relationship. should probably be done by refactoring other attribute selections
			}

			// Populate relationships from foreign keys for tables we haven't processed yet
			foreach (DatabaseTable table in Tables)
			{
				// Find the columns with foreign keys
				IEnumerable<DatabaseColumn> columnsWithForeignKeys = table.Columns.Where(column => column.IsReferencer);

				// If this is a relationship table then the first two columns are already a relationship
				if (table.RepresentsRelationship)
				{
					columnsWithForeignKeys = columnsWithForeignKeys.Skip(2);
				}

				foreach (DatabaseColumn column in columnsWithForeignKeys)
				{
					// Get the thing for this table
					Thing leftThing = result.Things.Single(table.IsThingMatch);

					// Find the table being referenced by the foreign key
					Thing rightThing = result.Things.Single(column.ReferencedColumn.Table.IsThingMatch);

					// Find the constraint which is doing the referencing
					// TODO move to DatabaseColumn property
					DatabaseConstraint constraint = AllConstraints.Single(dbConstraint => dbConstraint.Type == ConstraintType.ForeignKey && dbConstraint.Columns.Contains(column));

					// Create the relationship
					// The left hand side (referencer) can only point to a single item on the right hand side (referenced)
					// TODO it might be possible to infer more detail here using unique and NOT NULL constraints
					Relationship relationship = new Relationship(constraint.Name, leftThing, rightThing);
					relationship.RightLink.AmountLower = 1;
					relationship.RightLink.AmountUpper = 1;

					// Add additional details
					relationship.Details.Add("SqlCatalog", table.Catalog);
					relationship.Details.Add("SqlSchema", table.Schema);
					relationship.Details.Add("SqlTable", table.Name);
					relationship.Details.Add("SqlConstraint", constraint.Name);
					relationship.Details.Add("SqlColumns", string.Join(", ", constraint.Columns));

					result.AddRelationship(relationship);
				}
			}

			foreach (DatabaseRoutine routine in DatabaseModel.Routines)
			{
				Behavior behavior = new Behavior(routine.Name);

				foreach (DatabaseRoutineParameter routineParameter in routine.Parameters)
				{
					Parameter parameter = new Parameter(routineParameter.Name, routineParameter.Type.GetSystemType(), NullConstraints.NotApplicable);

					behavior.Parameters.Add(parameter);
				}

				Item item = result.Items.Values.Single(routine.IsThingMatch);
				item.Behaviors.Add(behavior);
			}

			PopulateAdditionalData(result);

			return result;
		}

		/// <summary>
		/// Populates the additional data such as descriptions.
		/// </summary>
		private static void PopulateAdditionalData(Model model)
		{
			model.Items["Container"].Description = "A container is a basic tool, consisting of any device creating a partially or fully enclosed space that can be used to contain, store, and transport objects or materials. In commerce, it includes any receptacle or enclosure for holding a product used in packaging and shipping. Things kept inside of a container are protected by being inside of its structure. The term is most frequently applied to devices made from materials that are durable and at least partly rigid.";
			model.Items["Person"].Description = "A person is a being, such as a human, that has certain capacities or attributes constituting personhood, which in turn is defined differently by different authors in different disciplines, and by different cultures in different times and places. In ancient Rome, the word persona (Latin) or prosopon (πρόσωπον; Greek) originally referred to the masks worn by actors on stage. The various masks represented the various personae in the stage play.";
			model.Items["Kitchen"].Description = "A kitchen is a room or part of a room used for cooking and food preparation. In the West, a modern residential kitchen is typically equipped with a stove, a sink with hot and cold running water, a refrigerator and kitchen cabinets arranged according to a modular design. Many households have a microwave oven, a dishwasher and other electric appliances. The main function of a kitchen is cooking or preparing food but it may also be used for dining, food storage, entertaining, dishwashing, laundry.";
			model.Items["Foodstuff"].Description = "Food is any substance consumed to provide nutritional support for the body. It is usually of plant or animal origin, and contains essential nutrients, such as fats, proteins, vitamins, or minerals. The substance is ingested by an organism and assimilated by the organism's cells to provide energy, maintain life, or stimulate growth. Historically, people secured food through two methods: hunting and gathering, and agriculture. Today, most of the food energy required by the ever increasing population of the world is supplied by the food industry.";
		}
	}
}
