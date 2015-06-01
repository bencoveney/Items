namespace ItemLoader
{
	using System;
	using System.Data.SqlClient;
	using System.Linq;
	using Items;

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
	DatabaseColumns.COLUMN_NAME,
	DatabaseColumns.ORDINAL_POSITION,
	DatabaseColumns.COLUMN_DEFAULT,
	DatabaseColumns.IS_NULLABLE,
	DatabaseColumns.DATA_TYPE,
	CAST(DatabaseColumns.CHARACTER_MAXIMUM_LENGTH AS INT) AS CHARACTER_MAXIMUM_LENGTH,
	CAST(DatabaseColumns.NUMERIC_PRECISION AS INT) AS NUMERIC_PRECISION,
	CAST(DatabaseColumns.NUMERIC_PRECISION_RADIX AS INT) AS NUMERIC_PRECISION_RADIX,
	CAST(DatabaseColumns.NUMERIC_SCALE AS INT) AS NUMERIC_SCALE,
	CAST(DatabaseColumns.DATETIME_PRECISION AS INT) AS DATETIME_PRECISION,
	DatabaseColumns.CHARACTER_SET_NAME,
	DatabaseColumns.COLLATION_NAME
FROM
	INFORMATION_SCHEMA.COLUMNS As DatabaseColumns
WHERE
	DatabaseColumns.TABLE_CATALOG = @TableCatalog
	AND DatabaseColumns.TABLE_SCHEMA = @TableSchema
	AND DatabaseColumns.TABLE_NAME = @TableName
ORDER BY
	TABLE_NAME,
	ORDINAL_POSITION";

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseColumn" /> class.
		/// </summary>
		/// <param name="name">Name of the column.</param>
		/// <param name="table">The table.</param>
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
		public DatabaseColumn(string name, DatabaseTable table, int ordinalPosition, string columnDefault, bool isNullable, string dataType, int? characterMaximumLength, int? numericPrecision, int? numericPrecisionRadix, int? numericScale, int? dateTimePrecision, string characterSetName, string collationName)
		{
			// Populate member variables
			// A lot of the varibales here are duplicated from the tables class. maybe these columns should just be properties of the tables?
			this.Name = name;
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

			table.Columns.Add(this);
		}

		/// <summary>
		/// Gets the name of the column.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the table this column is on.
		/// </summary>
		/// <value>
		/// The table.
		/// </value>
		public DatabaseTable Table
		{
			get
			{
				return DatabaseModel.Tables.Single(table => table.Columns.Contains(this));
			}
		}

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
		/// Gets a value indicating whether this column is a foreign key referencing another table (in the model).
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is references another column; otherwise, <c>false</c>.
		/// </value>
		public bool IsReferencer
		{
			get
			{
				return this.Table.Constraints.Any(dbConstraint => dbConstraint.Type == ConstraintType.ForeignKey && dbConstraint.Columns.Contains(this));
			}
		}

		/// <summary>
		/// Gets a value indicating whether this column is the column referred to by any foreign keys (in the model).
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is referenced; otherwise, <c>false</c>.
		/// </value>
		public bool IsReferenced
		{
			get
			{
				return DatabaseModel.AllConstraints.Any(dbConstraint => dbConstraint.Type == ConstraintType.ForeignKey && dbConstraint.ReferencedColumn == this);
			}
		}

		/// <summary>
		/// Gets the column referred to by this column's foreign key (if any).
		/// </summary>
		/// <value>
		/// The referenced column.
		/// </value>
		public DatabaseColumn ReferencedColumn
		{
			get
			{
				DatabaseConstraint foreignKey = this.Table.Constraints.Single(constraint => constraint.Type == ConstraintType.ForeignKey && constraint.Columns.Contains(this));
				return foreignKey.ReferencedColumn;
			}
		}

		/// <summary>
		/// Loads the columns from the database for a specific table.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="connection">The connection.</param>
		public static void PopulateColumns(DatabaseTable table, SqlConnection connection)
		{
			// Query the database for the column data
			using (SqlCommand command = new SqlCommand(ColumnsQuery, connection))
			{
				command.Parameters.AddWithValue("TableCatalog", table.Catalog);
				command.Parameters.AddWithValue("TableSchema", table.Schema);
				command.Parameters.AddWithValue("TableName", table.Name);

				using (SqlDataReader result = command.ExecuteReader())
				{
					while (result.Read())
					{
						// Read the result data
						string name = (string)result["COLUMN_NAME"];
						int ordinalPosition = (int)result["ORDINAL_POSITION"];
						string columnDefault = result.GetNullableString("COLUMN_DEFAULT");
						bool isNullable = ((string)result["IS_NULLABLE"]).Equals("NO") ? false : true;
						string dataType = (string)result["DATA_TYPE"];
						int? characterMaximumLength = result.GetNullable<int>("CHARACTER_MAXIMUM_LENGTH");
						int? numberPrecision = result.GetNullable<int>("NUMERIC_PRECISION");
						int? numberPrecisionRadix = result.GetNullable<int>("NUMERIC_PRECISION_RADIX");
						int? numericScale = result.GetNullable<int>("NUMERIC_SCALE");
						int? dateTimePrecision = result.GetNullable<int>("DATETIME_PRECISION");
						string characterSetName = result.GetNullableString("CHARACTER_SET_NAME");
						string collationName = result.GetNullableString("COLLATION_NAME");

						// Build the new column
						new DatabaseColumn(name, table, ordinalPosition, columnDefault, isNullable, dataType, characterMaximumLength, numberPrecision, numberPrecisionRadix, numericScale, dateTimePrecision, characterSetName, collationName);
					}
				}
			}
		}

		/// <summary>
		/// Gets a model system type from the column's data type.
		/// </summary>
		/// <returns>A model system type.</returns>
		public IType GetSystemType()
		{
			IType type;

			// TODO full list available here: https://msdn.microsoft.com/en-us/library/cc716729%28v=vs.110%29.aspx
			// TODO Xml not implemented
			// TODO Confirm which additional details applicable to the specific datatypes. this has been guessed for now
			switch (this.DataType.ToLower())
			{
				case "bit":
					SystemType<bool> booleanType = new SystemType<bool>();
					booleanType.Details["SqlDataType"] = this.DataType;
					type = booleanType;
					break;

				case "bigint":
					SystemType<long> int64Type = new SystemType<long>();
					int64Type.Details["SqlDataType"] = this.DataType;
					int64Type.Details["SqlNumericPrecision"] = this.NumericPrecision;
					int64Type.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					int64Type.Details["SqlNumericScale"] = this.NumericScale;
					type = int64Type;
					break;

				case "int":
					SystemType<int> int32Type = new SystemType<int>();
					int32Type.Details["SqlDataType"] = this.DataType;
					int32Type.Details["SqlNumericPrecision"] = this.NumericPrecision;
					int32Type.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					int32Type.Details["SqlNumericScale"] = this.NumericScale;
					type = int32Type;
					break;

				case "smallint":
					SystemType<short> int16Type = new SystemType<short>();
					int16Type.Details["SqlDataType"] = this.DataType;
					int16Type.Details["SqlNumericPrecision"] = this.NumericPrecision;
					int16Type.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					int16Type.Details["SqlNumericScale"] = this.NumericScale;
					type = int16Type;
					break;

				case "tinyint":
					SystemType<byte> byteType = new SystemType<byte>();
					byteType.Details["SqlDataType"] = this.DataType;
					byteType.Details["SqlNumericPrecision"] = this.NumericPrecision;
					byteType.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					byteType.Details["SqlNumericScale"] = this.NumericScale;
					type = byteType;
					break;

				case "char":
				case "nchar":
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					SystemType<string> stringType = new SystemType<string>();
					stringType.Details["SqlDataType"] = this.DataType;
					stringType.Details["SqlMaxCharacters"] = this.CharacterMaximumLength;
					stringType.Details["SqlCharacterSet"] = this.CharacterSetName;
					stringType.Details["SqlCollationName"] = this.CollationName;
					type = stringType;
					break;

				case "datetime":
				case "datetime2":
				case "smalldatetime":
					SystemType<DateTime> dateTimeType = new SystemType<DateTime>();
					dateTimeType.Details["SqlDataType"] = this.DataType;
					dateTimeType.Details["SqlDateTimePrecision"] = this.DateTimePrecision;
					type = dateTimeType;
					break;

				case "datetimeoffset":
					SystemType<DateTimeOffset> dateTimeOffsetType = new SystemType<DateTimeOffset>();
					dateTimeOffsetType.Details["SqlDataType"] = this.DataType;
					dateTimeOffsetType.Details["SqlDateTimePrecision"] = this.DateTimePrecision;
					type = dateTimeOffsetType;
					break;

				case "decimal":
				case "money":
				case "numeric":
				case "smallmoney":
					SystemType<decimal> decimalType = new SystemType<decimal>();
					decimalType.Details["SqlDataType"] = this.DataType;
					decimalType.Details["SqlNumericPrecision"] = this.NumericPrecision;
					decimalType.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					decimalType.Details["SqlNumericScale"] = this.NumericScale;
					type = decimalType;
					break;

				case "float":
					SystemType<double> doubleType = new SystemType<double>();
					doubleType.Details["SqlDataType"] = this.DataType;
					doubleType.Details["SqlNumericPrecision"] = this.NumericPrecision;
					doubleType.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					doubleType.Details["SqlNumericScale"] = this.NumericScale;
					type = doubleType;
					break;

				case "real":
					SystemType<float> singleType = new SystemType<float>();
					singleType.Details["SqlDataType"] = this.DataType;
					singleType.Details["SqlNumericPrecision"] = this.NumericPrecision;
					singleType.Details["SqlNumericPrecisionRadix"] = this.NumericPrecisionRadix;
					singleType.Details["SqlNumericScale"] = this.NumericScale;
					type = singleType;
					break;

				// TODO filestream ?
				case "binary":
				case "image":
				case "rowversion":
				case "timestamp":
				case "varbinary":
					SystemType<byte[]> byteArrayType = new SystemType<byte[]>();
					byteArrayType.Details["SqlDataType"] = this.DataType;
					type = byteArrayType;
					break;

				case "sql_variant":
					SystemType<object> objectType = new SystemType<object>();
					objectType.Details["SqlDataType"] = this.DataType;
					type = objectType;
					break;

				case "time":
					SystemType<TimeSpan> timeSpanType = new SystemType<TimeSpan>();
					timeSpanType.Details["SqlDataType"] = this.DataType;
					type = timeSpanType;
					break;

				case "uniqueidentifier":
					SystemType<Guid> guidType = new SystemType<Guid>();
					guidType.Details["SqlDataType"] = this.DataType;
					type = guidType;
					break;

				default:
					SystemType<object> defaultType = new SystemType<object>();
					defaultType.Details["SqlDataType"] = this.DataType;
					type = defaultType;
					break;
			}

			// Assign universal details
			type.Details["SqlOrdinalPosition"] = this.OrdinalPosition;
			type.Details["SqlColumnDefault"] = this.ColumnDefault;

			return type;
		}

		/// <summary>
		/// Gets the way the column treats null values
		/// </summary>
		/// <returns>The way the column treats null values</returns>
		public Nullability GetNullability()
		{
			return this.IsNullable ? Nullability.NotApplicable : Nullability.Invalid;
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Name, this.DataType);
		}
	}
}