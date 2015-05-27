namespace ItemLoader
{
	using System.Collections.Generic;
	using System.Data.SqlClient;

	/// <summary>
	/// Represents a table's column in the database
	/// </summary>
	public class DatabaseColumn
	{
		/// <summary>
		/// Query to find the columns for all tables in the database
		/// </summary>
		private const string ColumnsQuery = @"
SELECT
	DatabaseColumns.TABLE_CATALOG,
	DatabaseColumns.TABLE_SCHEMA,
	DatabaseColumns.TABLE_NAME,
	DatabaseColumns.COLUMN_NAME,
	DatabaseColumns.ORDINAL_POSITION,
	DatabaseColumns.COLUMN_DEFAULT,
	DatabaseColumns.IS_NULLABLE,
	DatabaseColumns.DATA_TYPE,
	CAST(DatabaseColumns.CHARACTER_MAXIMUM_LENGTH AS INT),
	CAST(DatabaseColumns.NUMERIC_PRECISION AS INT),
	CAST(DatabaseColumns.NUMERIC_PRECISION_RADIX AS INT),
	CAST(DatabaseColumns.NUMERIC_SCALE AS INT),
	CAST(DatabaseColumns.DATETIME_PRECISION AS INT),
	DatabaseColumns.CHARACTER_SET_NAME,
	DatabaseColumns.COLLATION_NAME
FROM
	INFORMATION_SCHEMA.COLUMNS As DatabaseColumns
WHERE
	DatabaseColumns.Table_Name != 'sysdiagrams'
	AND DatabaseColumns.Table_Name != '__RefactorLog'
ORDER BY
	TABLE_NAME,
	ORDINAL_POSITION";

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseColumn"/> class.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <param name="schema">The schema.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="ordinalPosition">The ordinal position.</param>
		/// <param name="columnDefault">The column default.</param>
		/// <param name="isNullable">if set to <c>true</c> the column can be set to null.</param>
		/// <param name="dataType">Type of the data.</param>
		/// <param name="characterMaximumLength">Maximum length of the character.</param>
		/// <param name="numericPrecision">The numeric precision.</param>
		/// <param name="numericPrecisionRadix">The numeric precision radix.</param>
		/// <param name="numericScale">The numeric scale.</param>
		/// <param name="dateTimePrecision">The date time precision.</param>
		/// <param name="characterSetName">Name of the character set.</param>
		/// <param name="collationName">Name of the collation.</param>
		public DatabaseColumn(string catalog, string schema, string tableName, string columnName, int ordinalPosition, string columnDefault, bool isNullable, string dataType, int? characterMaximumLength, int? numericPrecision, int? numericPrecisionRadix, int? numericScale, int? dateTimePrecision, string characterSetName, string collationName)
		{
			// Populate member variables
			// A lot of the varibales here are duplicated from the tables class. maybe these columns should just be properties of the tables?
			this.Catalog = catalog;
			this.Schema = schema;
			this.TableName = tableName;
			this.Name = columnName;
			this.OrdinalPosition = ordinalPosition;
			this.ColumnDefault = columnDefault;
			this.IsNullable = isNullable;
			this.DataType = dataType;
			this.CharacterMaximumLength = characterMaximumLength;
			this.NumericPrecision = numericPrecision;
			this.NumericPrecisionRadix = numericPrecisionRadix;
			this.NumericScale = numericScale;
			this.DateTimePrecision = dateTimePrecision;
			this.CharacterSetName = characterSetName;
			this.CollationName = collationName;
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
		/// Gets the name of the table.
		/// </summary>
		/// <value>
		/// The name of the table.
		/// </value>
		public string TableName { get; private set; }

		/// <summary>
		/// Gets the name of the column.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the ordinal position.
		/// </summary>
		/// <value>
		/// The ordinal position.
		/// </value>
		public int OrdinalPosition { get; private set; }

		/// <summary>
		/// Gets the column default.
		/// </summary>
		/// <value>
		/// The column default.
		/// </value>
		public string ColumnDefault { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance can be set to null.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance can be set to null; otherwise, <c>false</c>.
		/// </value>
		public bool IsNullable { get; private set; }

		/// <summary>
		/// Gets the type of the data.
		/// </summary>
		/// <value>
		/// The type of the data.
		/// </value>
		public string DataType { get; private set; }

		/// <summary>
		/// Gets the maximum number of characters that can be stored (regardless of char width)
		/// </summary>
		/// <value>
		/// The maximum length of the character.
		/// </value>
		public int? CharacterMaximumLength { get; private set; }

		/// <summary>
		/// Gets the numeric precision.
		/// </summary>
		/// <value>
		/// The numeric precision.
		/// </value>
		public int? NumericPrecision { get; private set; }

		/// <summary>
		/// Gets the numeric precision radix (either 5 or 10).
		/// 5 precision with 10 radix means the number can represent 5 decimal digits
		/// 10 precision with 2 radix means the number can represent 10 bits
		/// </summary>
		/// <value>
		/// The numeric precision radix.
		/// </value>
		public int? NumericPrecisionRadix { get; private set; }

		/// <summary>
		/// Gets the numeric scale.
		/// </summary>
		/// <value>
		/// The numeric scale.
		/// </value>
		public int? NumericScale { get; private set; }

		/// <summary>
		/// Gets the date time precision.
		/// </summary>
		/// <value>
		/// The date time precision.
		/// </value>
		public int? DateTimePrecision { get; private set; }

		/// <summary>
		/// Gets the name of the character set.
		/// </summary>
		/// <value>
		/// The name of the character set.
		/// </value>
		public string CharacterSetName { get; private set; }

		/// <summary>
		/// Gets the name of the collation.
		/// </summary>
		/// <value>
		/// The name of the collation.
		/// </value>
		public string CollationName { get; private set; }

		/// <summary>
		/// Loads the columns from the database.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <returns>The columns present in the database.</returns>
		public static IEnumerable<DatabaseColumn> LoadColumns(SqlConnection connection)
		{
			List<DatabaseColumn> columns = new List<DatabaseColumn>();

			// Query the database for the column data
			using (SqlCommand command = new SqlCommand(ColumnsQuery, connection))
			{
				using (SqlDataReader result = command.ExecuteReader())
				{
					while (result.Read())
					{
						// Read the result data
						string catalog = result.GetString(0);
						string schema = result.GetString(1);
						string tableName = result.GetString(2);
						string name = result.GetString(3);
						int ordinalPosition = result.GetInt32(4);
						string columnDefault = result.IsDBNull(5) ? null : result.GetString(5);
						bool isNullable = result.GetString(6).Equals("NO") ? false : true;
						string dataType = result.GetString(7);
						int? characterMaximumLength = result.IsDBNull(8) ? null : (int?)result.GetInt32(8);
						int? numberPrecision = result.IsDBNull(9) ? null : (int?)result.GetInt32(9);
						int? numberPrecisionRadix = result.IsDBNull(10) ? null : (int?)result.GetInt32(10);
						int? numericScale = result.IsDBNull(11) ? null : (int?)result.GetInt32(11);
						int? dateTimePrecision = result.IsDBNull(12) ? null : (int?)result.GetInt32(12);
						string characterSetName = result.IsDBNull(13) ? null : result.GetString(13);
						string collationName = result.IsDBNull(14) ? null : result.GetString(14);

						// Build the new column
						DatabaseColumn column = new DatabaseColumn(catalog, schema, tableName, name, ordinalPosition, columnDefault, isNullable, dataType, characterMaximumLength, numberPrecision, numberPrecisionRadix, numericScale, dateTimePrecision, characterSetName, collationName);
						columns.Add(column);
					}
				}
			}

			return columns;
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}.{3} ({4})", this.Catalog, this.Schema, this.TableName, this.Name, this.DataType);
		}
	}
}