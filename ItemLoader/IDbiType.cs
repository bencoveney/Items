using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	public interface IDbiType
		: IType
	{

		/// <summary>
		/// Gets the type of the SQL data.
		/// </summary>
		/// <value>
		/// The type of the SQL data.
		/// </value>
		string SqlDataType
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
		int? SqlNumericPrecision
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
		int? SqlNumericPrecisionRadix
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
		int? SqlNumericScale
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
		int? SqlMaxCharacters
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
		string SqlCharacterSet
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
		string SqlCollationName
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
		int? SqlDateTimePrecision
		{
			get;
			set;
		}
	}
}
