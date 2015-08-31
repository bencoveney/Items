namespace ItemLoader
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Items;

	/// <summary>
	/// Represents a data type from the database
	/// </summary>
	public class DatabaseType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseType"/> class.
		/// </summary>
		/// <param name="dataType">Type of the data.</param>
		/// <param name="characterMaximumLength">Maximum length of the character.</param>
		/// <param name="characterSetName">Name of the character set.</param>
		/// <param name="collationName">Name of the collation.</param>
		/// <param name="numericPrecision">The numeric precision.</param>
		/// <param name="numericPrecisionRadix">The numeric precision radix.</param>
		/// <param name="numericScale">The numeric scale.</param>
		/// <param name="dateTimePrecision">The date time precision.</param>
		public DatabaseType(string dataType, int? characterMaximumLength, string characterSetName, string collationName, int? numericPrecision, int? numericPrecisionRadix, int? numericScale, int? dateTimePrecision)
		{
			this.DataType = dataType;
			this.CharacterMaximumLength = characterMaximumLength;
			this.CharacterSetName = characterSetName;
			this.CollationName = collationName;
			this.NumericPrecision = numericPrecision;
			this.NumericPrecisionRadix = numericPrecisionRadix;
			this.NumericScale = numericScale;
			this.DateTimePrecision = dateTimePrecision;
		}

		/// <summary>
		/// Gets the type of the data.
		/// </summary>
		/// <value>
		/// The type of the data.
		/// </value>
		public string DataType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the maximum number of characters that can be stored (regardless of char width)
		/// </summary>
		/// <value>
		/// The maximum length of the character.
		/// </value>
		public int? CharacterMaximumLength
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the character set.
		/// </summary>
		/// <value>
		/// The name of the character set.
		/// </value>
		public string CharacterSetName
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the collation.
		/// </summary>
		/// <value>
		/// The name of the collation.
		/// </value>
		public string CollationName
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the numeric precision.
		/// </summary>
		/// <value>
		/// The numeric precision.
		/// </value>
		public int? NumericPrecision
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the numeric precision radix (either 5 or 10).
		/// 5 precision with 10 radix means the number can represent 5 decimal digits
		/// 10 precision with 2 radix means the number can represent 10 bits
		/// </summary>
		/// <value>
		/// The numeric precision radix.
		/// </value>
		public int? NumericPrecisionRadix
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the numeric scale.
		/// </summary>
		/// <value>
		/// The numeric scale.
		/// </value>
		public int? NumericScale
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the date time precision.
		/// </summary>
		/// <value>
		/// The date time precision.
		/// </value>
		public int? DateTimePrecision
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a value indicating whether this type is an integer type.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is integer; otherwise, <c>false</c>.
		/// </value>
		public bool IsInteger
		{
			get
			{
				switch (this.DataType.ToLower())
				{
					case "bigint":
					case "int":
					case "smallint":
					case "tinyint":
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this type is a text type.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is text; otherwise, <c>false</c>.
		/// </value>
		public bool IsText
		{
			get
			{
				switch (this.DataType.ToLower())
				{
					case "char":
					case "nchar":
					case "varchar":
					case "nvarchar":
					case "text":
					case "ntext":
						return true;
					default:
						return false;
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
					DbiSystemType<bool> booleanType = new DbiSystemType<bool>();
					booleanType.SqlDataType = this.DataType;
					type = booleanType;
					break;

				case "bigint":
					DbiSystemType<long> int64Type = new DbiSystemType<long>();
					int64Type.SqlDataType = this.DataType;
					int64Type.SqlNumericPrecision = this.NumericPrecision.Value;
					int64Type.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					int64Type.SqlNumericScale = this.NumericScale.Value;
					type = int64Type;
					break;

				case "int":
					DbiSystemType<int> int32Type = new DbiSystemType<int>();
					int32Type.SqlDataType = this.DataType;
					int32Type.SqlNumericPrecision = this.NumericPrecision.Value;
					int32Type.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					int32Type.SqlNumericScale = this.NumericScale.Value;
					type = int32Type;
					break;

				case "smallint":
					DbiSystemType<short> int16Type = new DbiSystemType<short>();
					int16Type.SqlDataType = this.DataType;
					int16Type.SqlNumericPrecision = this.NumericPrecision.Value;
					int16Type.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					int16Type.SqlNumericScale = this.NumericScale.Value;
					type = int16Type;
					break;

				case "tinyint":
					DbiSystemType<byte> byteType = new DbiSystemType<byte>();
					byteType.SqlDataType = this.DataType;
					byteType.SqlNumericPrecision = this.NumericPrecision.Value;
					byteType.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					byteType.SqlNumericScale = this.NumericScale.Value;
					type = byteType;
					break;

				case "char":
				case "nchar":
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					DbiSystemType<string> stringType = new DbiSystemType<string>();
					stringType.SqlDataType = this.DataType;
					stringType.SqlMaxCharacters = this.CharacterMaximumLength;
					stringType.SqlCharacterSet = this.CharacterSetName;
					stringType.SqlCollationName = this.CollationName;
					type = stringType;
					break;

				case "datetime":
				case "datetime2":
				case "smalldatetime":
					DbiSystemType<DateTime> dateTimeType = new DbiSystemType<DateTime>();
					dateTimeType.SqlDataType = this.DataType;
					dateTimeType.SqlDateTimePrecision = this.DateTimePrecision.Value;
					type = dateTimeType;
					break;

				case "datetimeoffset":
					DbiSystemType<DateTimeOffset> dateTimeOffsetType = new DbiSystemType<DateTimeOffset>();
					dateTimeOffsetType.SqlDataType = this.DataType;
					dateTimeOffsetType.SqlDateTimePrecision = this.DateTimePrecision.Value;
					type = dateTimeOffsetType;
					break;

				case "decimal":
				case "money":
				case "numeric":
				case "smallmoney":
					DbiSystemType<decimal> decimalType = new DbiSystemType<decimal>();
					decimalType.SqlDataType = this.DataType;
					decimalType.SqlNumericPrecision = this.NumericPrecision.Value;
					decimalType.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					decimalType.SqlNumericScale = this.NumericScale.Value;
					type = decimalType;
					break;

				case "float":
					DbiSystemType<double> doubleType = new DbiSystemType<double>();
					doubleType.SqlDataType = this.DataType;
					doubleType.SqlNumericPrecision = this.NumericPrecision.Value;
					doubleType.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					doubleType.SqlNumericScale = this.NumericScale;
					type = doubleType;
					break;

				case "real":
					DbiSystemType<float> singleType = new DbiSystemType<float>();
					singleType.SqlDataType = this.DataType;
					singleType.SqlNumericPrecision = this.NumericPrecision.Value;
					singleType.SqlNumericPrecisionRadix = this.NumericPrecisionRadix.Value;
					singleType.SqlNumericScale = this.NumericScale.Value;
					type = singleType;
					break;

				// TODO filestream ?
				case "binary":
				case "image":
				case "rowversion":
				case "timestamp":
				case "varbinary":
					DbiSystemType<byte[]> byteArrayType = new DbiSystemType<byte[]>();
					byteArrayType.SqlDataType = this.DataType;
					type = byteArrayType;
					break;

				case "sql_variant":
					DbiSystemType<object> objectType = new DbiSystemType<object>();
					objectType.SqlDataType = this.DataType;
					type = objectType;
					break;

				case "time":
					DbiSystemType<TimeSpan> timeSpanType = new DbiSystemType<TimeSpan>();
					timeSpanType.SqlDataType = this.DataType;
					type = timeSpanType;
					break;

				case "uniqueidentifier":
					DbiSystemType<Guid> guidType = new DbiSystemType<Guid>();
					guidType.SqlDataType = this.DataType;
					type = guidType;
					break;

				default:
					DbiSystemType<object> defaultType = new DbiSystemType<object>();
					defaultType.SqlDataType = this.DataType;
					type = defaultType;
					break;
			}

			return type;
		}

		/// <summary>
		/// Gets the model constraints that can be inferred from this type.
		/// </summary>
		/// <returns>The model constraints that can be inferred from this type.</returns>
		public IEnumerable<IConstraint> GetConstraints()
		{
			List<IConstraint> constraints = new List<IConstraint>();

			switch (this.DataType)
			{
				case "char":
				case "nchar":
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, this.CharacterMaximumLength.Value));
					break;

				default:
					// No constraints available
					break;
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
			// Add the text length to string data types
			switch (this.DataType)
			{
				case "char":
				case "nchar":
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					return string.Format("{0} ({1})", this.DataType, this.CharacterMaximumLength);

				default:
					return this.DataType;
			}
		}
	}
}
