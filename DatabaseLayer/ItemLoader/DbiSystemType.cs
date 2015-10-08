using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	public class DbiSystemType<T>
		: SystemType<T>, IDbiType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DbiSystemType{T}"/> class.
		/// TODO constructor variants that take the correct params ie one for numeric data, one for string data etc. Maybe as static factory methods?
		/// </summary>
		public DbiSystemType()
			: base()
		{
		}

		/// <summary>
		/// Gets the type of the SQL data.
		/// </summary>
		/// <value>
		/// The type of the SQL data.
		/// </value>
		public string SqlDataType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the SQL numeric precision.
		/// </summary>
		/// <value>
		/// The SQL numeric precision.
		/// </value>
		public int? SqlNumericPrecision
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the SQL numeric precision radix.
		/// </summary>
		/// <value>
		/// The SQL numeric precision radix.
		/// </value>
		public int? SqlNumericPrecisionRadix
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the SQL numeric scale.
		/// </summary>
		/// <value>
		/// The SQL numeric scale.
		/// </value>
		public int? SqlNumericScale
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the SQL maximum characters.
		/// </summary>
		/// <value>
		/// The SQL maximum characters.
		/// </value>
		public int? SqlMaxCharacters
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the SQL character set.
		/// </summary>
		/// <value>
		/// The SQL character set.
		/// </value>
		public string SqlCharacterSet
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the name of the SQL collation.
		/// </summary>
		/// <value>
		/// The name of the SQL collation.
		/// </value>
		public string SqlCollationName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the SQL date time precision.
		/// </summary>
		/// <value>
		/// The SQL date time precision.
		/// </value>
		public int? SqlDateTimePrecision
		{
			get;
			set;
		}
	}
}