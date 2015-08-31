using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	public class DbiDataMember
		: DataMember, IDbiDataDefinition
	{
		public DbiDataMember(string name, IType type, NullConstraints nullConstraint, string sqlColumn, int sqlOrdinalPosition, object sqlDefaultValue)
			: base(name, type, nullConstraint)
		{
			if (string.IsNullOrEmpty(sqlColumn))
			{
				throw new ArgumentNullException("sqlColumn", "sqlColumn must be provided");
			}

			this.SqlColumn = sqlColumn;
			this.SqlOrdinal = sqlOrdinalPosition;
			this.SqlDefaultValue = sqlDefaultValue;
		}

		/// <summary>
		/// Gets the SQL column name this data definition represents.
		/// </summary>
		/// <value>
		/// The SQL column.
		/// </value>
		public string SqlColumn
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the ordinal position of the column in the table.
		/// </summary>
		/// <value>
		/// The SQL ordinal position.
		/// </value>
		public int SqlOrdinal
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
