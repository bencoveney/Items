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

			return type;
		}

		/// <summary>
		/// Gets the model constraints that can be inferred from this type.
		/// </summary>
		/// <returns></returns>
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
