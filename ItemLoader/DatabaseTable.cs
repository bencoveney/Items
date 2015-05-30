namespace ItemLoader
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;

	/// <summary>
	/// A table in the database
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
		public static IEnumerable<DatabaseTable> LoadTables(string connectionString)
		{
			List<DatabaseTable> tables = new List<DatabaseTable>();

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

							tables.Add(table);
						}
					}
				}

				// Populate additional schema objects
				foreach (DatabaseTable table in tables)
				{
					DatabaseColumn.PopulateColumns(table, connection);
					DatabaseConstraint.PopulateUniqueConstraints(table, connection);
				}

				DatabaseConstraint.PopulateReferentialConstraints(tables, connection);

				connection.Close();
			}
			
			return tables;
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
