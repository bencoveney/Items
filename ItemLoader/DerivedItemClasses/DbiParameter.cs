using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	public class DbiParameter
		: Parameter, IDbiDataDefinition
	{
		public DbiParameter(string name, IType type, NullConstraints nullConstraint, int sqlOrdinal, string sqlMode)
			: base(name, type, nullConstraint)
		{
			if (string.IsNullOrEmpty(sqlMode))
			{
				throw new ArgumentNullException("sqlMode", "sqlMode must be provided");
			}

			this.SqlOrdinal = sqlOrdinal;
			this.SqlMode = sqlMode;
		}

		/// <summary>
		/// Gets the ordinal position of the parameter.
		/// </summary>
		/// <value>
		/// The SQL ordinal.
		/// </value>
		public int SqlOrdinal
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the mode (In/Out/Inout) for the parameter.
		/// </summary>
		/// <value>
		/// The SQL mode.
		/// </value>
		public string SqlMode
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the default value for the column.
		/// </summary>
		/// <value>
		/// The default value.
		/// </value>
		public object SqlDefaultValue
		{
			get;
			private set;
		}
	}
}
