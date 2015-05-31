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
		/// Gets all columns for all tables in the database.
		/// </summary>
		/// <value>
		/// The columns.
		/// </value>
		public static IEnumerable<DatabaseColumn> Columns
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
		public static IEnumerable<DatabaseConstraint> Constraints
		{
			get
			{
				return Tables.SelectMany(table => table.Constraints);
			}
		}

		/// <summary>
		/// Loads the objects from the database.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		public static void LoadFromDatabase(string connectionString)
		{
			// Load the tables
			DatabaseTable.LoadTables(connectionString);
		}

		/// <summary>
		/// Constructs a model from the loaded database objects.
		/// </summary>
		/// <returns>A model build from the schema</returns>
		public static Model ConstructModel()
		{
			Model result = new Model();

			// Populate base items
			foreach (DatabaseTable table in Tables.Where(ItemTablePredicate))
			{
				Item item = new Item(table.Name);

				// Add additional details
				item.Details.Add("SqlCatalog", table.Catalog);
				item.Details.Add("SqlSchema", table.Schema);
				item.Details.Add("SqlTable", table.Name);

				// Add columns (which aren't relationships)
				foreach (DatabaseColumn column in table.Columns.Where(dbColumn => !dbColumn.IsReferencer && !dbColumn.IsReferenced))
				{
					item.Attributes.Add(new ValueAttribute(column.Name, column.GetSystemType(), column.GetNullability()));
				}

				result.AddItem(item);
			}

			// Populate categories
			foreach (DatabaseTable table in Tables.Where(CategoryTablePredicate))
			{
				Category category = new Category(table.Name);

				category.Details.Add("SqlCatalog", table.Catalog);
				category.Details.Add("SqlSchema", table.Schema);
				category.Details.Add("SqlTable", table.Name);

				// Add columns (which aren't relationships)
				foreach (DatabaseColumn column in table.Columns.Where(dbColumn => !dbColumn.IsReferencer && !dbColumn.IsReferenced))
				{
					category.Attributes.Add(new ValueAttribute(column.Name, column.GetSystemType(), column.GetNullability()));
				}

				result.AddCategory(category);
			}

			foreach (DatabaseTable table in Tables.Where(t => t.Name.Contains("Collection")))
			{
				// Find the first columns which are foreign keys and assume those are the right and left sides of the relationship
				IEnumerable<DatabaseColumn> columnsWithForeignKeys = table.Columns.Where(column => table.Constraints.Any(constraint => constraint.Type == ConstraintType.ForeignKey && constraint.Columns.Contains(column)));

				DatabaseColumn leftColumn = columnsWithForeignKeys.OrderBy(column => column.OrdinalPosition).ToList()[0];
				Thing leftThing = result.Things.Single(thing => leftColumn.ReferencedColumn.Table.IsThingMatch(thing));

				DatabaseColumn rightColumn = columnsWithForeignKeys.OrderBy(column => column.OrdinalPosition).ToList()[1];
				Thing rightThing = result.Things.Single(thing => rightColumn.ReferencedColumn.Table.IsThingMatch(thing));

				Relationship relationship = new Relationship(table.Name, leftThing, rightThing);

				result.AddRelationship(relationship);
			}

			return result;
		}

		/// <summary>
		/// Predicate used to determine whether the table is an item.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <returns>A value indicating whether the table represents an item.</returns>
		private static bool ItemTablePredicate(DatabaseTable table)
		{
			return !RelationshipTablePredicate(table) && !CategoryTablePredicate(table);
		}

		/// <summary>
		/// Predicate used to determine whether the table is a relationship.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <returns>A value indicating whether the table represents a relationship.</returns>
		private static bool RelationshipTablePredicate(DatabaseTable table)
		{
			return table.Name.Contains("Collection");
		}

		/// <summary>
		/// Predicate used to determine whether the table is a category.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <returns>A value indicating whether the table represents a category.</returns>
		private static bool CategoryTablePredicate(DatabaseTable table)
		{
			return table.Name.Contains("Category");
		}
	}
}
