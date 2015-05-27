namespace ItemLoader
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Linq;

	/// <summary>
	/// The type of database constraint
	/// </summary>
	public enum ConstraintType
	{
		/// <summary>
		/// A limit on the values that can be placed in a column
		/// </summary>
		Check,

		/// <summary>
		/// A requirement that the column's values not be duplicated
		/// </summary>
		Unique,

		/// <summary>
		/// A special case of unique constraint
		/// </summary>
		PrimaryKey,

		/// <summary>
		/// Points to a primary key on another table
		/// </summary>
		ForeignKey
	}

	/// <summary>
	/// Represents a constraint applied to a table in the database
	/// </summary>
	public class DatabaseConstraint
	{
		/// <summary>
		/// Query to find which columns on which tables have unique constraints
		/// This query does not handle unique constraints which apply to multiple columns
		/// </summary>
		private const string ConstraintsQuery = @"
SELECT
	DatabaseConstraints.CONSTRAINT_CATALOG,
	DatabaseConstraints.CONSTRAINT_SCHEMA,
	DatabaseConstraints.CONSTRAINT_NAME,
	DatabaseConstraints.CONSTRAINT_TYPE,
	DatabaseConstraints.TABLE_NAME,
	ConstrainedColumn.COLUMN_NAME,
	DatabaseConstraints.IS_DEFERRABLE,
	DatabaseConstraints.INITIALLY_DEFERRED
FROM
	INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS DatabaseConstraints
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS ConstrainedColumn ON DatabaseConstraints.CONSTRAINT_NAME = ConstrainedColumn.CONSTRAINT_NAME
WHERE
	DatabaseConstraints.TABLE_NAME != '__RefactorLog'";

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseConstraint" /> class.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <param name="schema">The schema.</param>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="isDeferrable">if set to <c>true</c> [is deferrable].</param>
		/// <param name="initiallyDeferred">if set to <c>true</c> [initially deferred].</param>
		public DatabaseConstraint(string catalog, string schema, string name, ConstraintType type, string tableName, bool isDeferrable, bool initiallyDeferred)
		{
			// Populate member variables
			// A lot of the varibales here are duplicated from the tables class. maybe these constraints should just be properties of the tables?
			this.Catalog = catalog;
			this.Schema = schema;
			this.Name = name;
			this.Type = type;
			this.TableName = tableName;
			this.ColumnNames = new List<string>();
			this.IsDeferrable = isDeferrable;
			this.InitiallyDeferred = initiallyDeferred;
		}

		/// <summary>
		/// Gets the catalog.
		/// </summary>
		/// <value>
		/// The catalog.
		/// </value>
		public string Catalog { get; private set; }

		/// <summary>
		/// Gets the schema.
		/// </summary>
		/// <value>
		/// The schema.
		/// </value>
		public string Schema { get; private set; }

		/// <summary>
		/// Gets the name of the constraint.
		/// </summary>
		/// <value>
		/// The name of the constraint.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type of the constraint.
		/// </summary>
		/// <value>
		/// The type of the constraint.
		/// </value>
		public ConstraintType Type { get; private set; }

		/// <summary>
		/// Gets the name of the table this constraint applies to.
		/// </summary>
		/// <value>
		/// The name of the table.
		/// </value>
		public string TableName { get; private set; }

		/// <summary>
		/// Gets the column names covered by this constraint.
		/// </summary>
		/// <value>
		/// The column names.
		/// </value>
		public List<string> ColumnNames { get; private set; }

		/// <summary>
		/// Gets a value indicating whether enforcement of this constraint can be deferred.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is deferrable; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeferrable { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the constraint is deferred until just before the transaction commits.
		/// </summary>
		/// <value>
		///   <c>true</c> if [initially deferred]; otherwise, <c>false</c>.
		/// </value>
		public bool InitiallyDeferred { get; private set; }

		/// <summary>
		/// Loads constraints from the database.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <returns>The constraints present in the database.</returns>
		public static IEnumerable<DatabaseConstraint> LoadConstraints(SqlConnection connection)
		{
			List<DatabaseConstraint> constraints = new List<DatabaseConstraint>();

			// Query the database for the constraint data
			using (SqlCommand command = new SqlCommand(ConstraintsQuery, connection))
			{
				using (SqlDataReader result = command.ExecuteReader())
				{
					while (result.Read())
					{
						// Read the result data
						string catalog = result.GetString(0);
						string schema = result.GetString(1);
						string name = result.GetString(2);
						string typeAsText = result.GetString(3);
						string tableName = result.GetString(4);
						string columnName = result.GetString(5);
						bool isDeferrable = result.GetString(6).Equals("NO") ? false : true;
						bool initiallyDeferred = result.GetString(7).Equals("NO") ? false : true;

						// Convert the constraint type
						ConstraintType type = (ConstraintType)Enum.Parse(typeof(ConstraintType), typeAsText.Replace(" ", string.Empty), true);

						// If the constraint has already been read, then this is just an additional column
						// Otherwise it is a new constraint
						if (constraints.Any(constraint => constraint.Name == name))
						{
							// Add the column to the constraint
							constraints.Single(constraint => constraint.Name == name).ColumnNames.Add(columnName);
						}
						else
						{
							// Build the new constraint
							DatabaseConstraint newConstraint = new DatabaseConstraint(catalog, schema, name, type, tableName, isDeferrable, initiallyDeferred);
							newConstraint.ColumnNames.Add(columnName);
							constraints.Add(newConstraint);
						}
					}
				}
			}

			return constraints;
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} ({3})", this.Catalog, this.Schema, this.Name, string.Join(", ", this.ColumnNames));
		}
	}
}