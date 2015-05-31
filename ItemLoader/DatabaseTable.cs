namespace ItemLoader
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
using Items;

	/// <summary>
	/// A table in the database
	/// TODO enforce constraints and columns not being assigned multiple times
	/// </summary>
	public class DatabaseTable
	{
		/// <summary>
		/// Query to find the names of the tables in the database
		/// </summary>
		private const string TablesQuery = @"
SELECT
	SchemaTables.Table_Catalog,
	SchemaTables.Table_Schema,
	SchemaTables.Table_Name
FROM
	Information_Schema.Tables AS SchemaTables
WHERE
	SchemaTables.Table_Type = 'BASE TABLE'
	AND SchemaTables.Table_Name != 'sysdiagrams'
	AND SchemaTables.Table_Name != '__RefactorLog'";

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseTable"/> class.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <param name="schema">The schema.</param>
		/// <param name="name">The name.</param>
		/// <exception cref="System.ArgumentNullException">
		/// catalog
		/// or
		/// schema
		/// or
		/// name
		/// </exception>
		public DatabaseTable(string catalog, string schema, string name)
		{
			if (string.IsNullOrEmpty(catalog))
			{
				throw new ArgumentNullException("catalog");
			}

			if (string.IsNullOrEmpty(schema))
			{
				throw new ArgumentNullException("schema");
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}

			this.Catalog = catalog;
			this.Schema = schema;
			this.Name = name;

			this.Columns = new List<DatabaseColumn>();
			this.Constraints = new List<DatabaseConstraint>();
		}

		/// <summary>
		/// Gets the catalog the table is in.
		/// </summary>
		/// <value>
		/// The catalog.
		/// </value>
		public string Catalog { get; private set; }

		/// <summary>
		/// Gets the schema the table is in.
		/// </summary>
		/// <value>
		/// The schema.
		/// </value>
		public string Schema { get; private set; }

		/// <summary>
		/// Gets the name of the table.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the columns the table contains.
		/// </summary>
		/// <value>
		/// The columns.
		/// </value>
		public List<DatabaseColumn> Columns { get; private set; }

		/// <summary>
		/// Gets the constraints.
		/// </summary>
		/// <value>
		/// The constraints.
		/// </value>
		public List<DatabaseConstraint> Constraints { get; private set; }

		/// <summary>
		/// Loads the tables from the database including all .
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns>
		/// The tables present in the database.
		/// </returns>
		public static void LoadTables(string connectionString)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Query the database for the table data
				using (SqlCommand command = new SqlCommand(TablesQuery, connection))
				{
					using (SqlDataReader result = command.ExecuteReader())
					{
						while (result.Read())
						{
							// Read the result data
							string tableCatalog = result.GetString(0);
							string tableSchema = result.GetString(1);
							string tableName = result.GetString(2);

							// Build the new table
							DatabaseTable table = new DatabaseTable(tableCatalog, tableSchema, tableName);

							DatabaseModel.Tables.Add(table);
						}
					}
				}

				// Populate additional schema objects
				foreach (DatabaseTable table in DatabaseModel.Tables)
				{
					DatabaseColumn.PopulateColumns(table, connection);
					DatabaseConstraint.PopulateUniqueConstraints(table, connection);
				}

				DatabaseConstraint.PopulateReferentialConstraints(DatabaseModel.Tables, connection);

				connection.Close();
			}
		}

		/// <summary>
		/// Determines whether this table is the basis behind the specified thing.
		/// </summary>
		/// <param name="thing">The thing.</param>
		/// <returns>A value indicating whether the thing is based on this table</returns>
		public bool IsThingMatch(Thing thing)
		{
			return (string)thing.Details["SqlCatalog"] == this.Catalog
				&& (string)thing.Details["SqlSchema"] == this.Schema
				&& (string)thing.Details["SqlTable"] == this.Name;
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.Catalog, this.Schema, this.Name);
		}
	}
}
