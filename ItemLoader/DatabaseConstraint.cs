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
	/// Represents a constraint applied to the database
	/// </summary>
	public class DatabaseConstraint
	{
		/// <summary>
		/// Query to find which columns on which tables have unique constraints
		/// This query does not handle unique constraints which apply to multiple columns
		/// </summary>
		private const string ConstraintsQuery = @"
SELECT
	SchemaConstraint.CONSTRAINT_NAME,
	SchemaConstraint.CONSTRAINT_TYPE,
	SchemaConstraint.TABLE_NAME,
	ConstrainedColumn.COLUMN_NAME,
	SchemaConstraint.IS_DEFERRABLE,
	SchemaConstraint.INITIALLY_DEFERRED
FROM
	INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS SchemaConstraint
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS ConstrainedColumn ON SchemaConstraint.CONSTRAINT_NAME = ConstrainedColumn.CONSTRAINT_NAME
WHERE
	SchemaConstraint.TABLE_NAME != '__RefactorLog'";

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseConstraint"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="isDeferrable">if set to <c>true</c> [is deferrable].</param>
		/// <param name="initiallyDeferred">if set to <c>true</c> [initially deferred].</param>
		public DatabaseConstraint(string name, ConstraintType type, string tableName, bool isDeferrable, bool initiallyDeferred)
		{
			this.ConstraintName = name;
			this.ConstraintType = type;
			this.TableName = tableName;
			this.ColumnNames = new List<string>();
			this.IsDeferrable = isDeferrable;
			this.InitiallyDeferred = initiallyDeferred;
		}

		/// <summary>
		/// Gets or sets the name of the constraint.
		/// </summary>
		/// <value>
		/// The name of the constraint.
		/// </value>
		public string ConstraintName { get; set; }

		/// <summary>
		/// Gets or sets the type of the constraint.
		/// </summary>
		/// <value>
		/// The type of the constraint.
		/// </value>
		public ConstraintType ConstraintType { get; set; }

		/// <summary>
		/// Gets or sets the name of the table this constraint applies to.
		/// </summary>
		/// <value>
		/// The name of the table.
		/// </value>
		public string TableName { get; set; }

		/// <summary>
		/// Gets or sets the column names covered by this constraint.
		/// </summary>
		/// <value>
		/// The column names.
		/// </value>
		public List<string> ColumnNames { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether enforcement of this constraint can be deferred.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is deferrable; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeferrable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the constraint is deferred until just before the transaction commits.
		/// </summary>
		/// <value>
		///   <c>true</c> if [initially deferred]; otherwise, <c>false</c>.
		/// </value>
		public bool InitiallyDeferred { get; set; }

		/// <summary>
		/// Loads constraints from the database.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <returns>The constraints</returns>
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
						string constraintName = result.GetString(0);
						string constraintTypeText = result.GetString(1);
						string tableName = result.GetString(2);
						string columnName = result.GetString(3);
						bool isDeferrable = result.GetString(4).Equals("NO") ? false : true;
						bool initiallyDeferred = result.GetString(5).Equals("NO") ? false : true;

						// Convert the constraint type
						ConstraintType constraintType = (ConstraintType)Enum.Parse(typeof(ConstraintType), constraintTypeText.Replace(" ", string.Empty), true);

						// If the constraint has already been read, then this is just an additional column
						// Otherwise it is a new constraint
						if (constraints.Any(constraint => constraint.ConstraintName == constraintName))
						{
							// Add the column to the constraint
							constraints.Single(constraint => constraint.ConstraintName == constraintName).ColumnNames.Add(columnName);
						}
						else
						{
							// Build the new constraint
							DatabaseConstraint newConstraint = new DatabaseConstraint(constraintName, constraintType, tableName, isDeferrable, initiallyDeferred);
							newConstraint.ColumnNames.Add(columnName);
							constraints.Add(newConstraint);
						}
					}
				}
			}

			return constraints;
		}
	}
}
