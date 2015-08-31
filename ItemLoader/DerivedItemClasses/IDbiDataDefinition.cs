using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;

/// <summary>
/// 
/// </summary>
namespace ItemLoader
{
	/// <summary>
	/// An Items.IDataDefinition which represents a database object.
	/// Perhaps the Sql... properties could be replaced by a DBO class.
	/// </summary>
	public interface IDbiDataDefinition
		: IDataDefinition
	{
		/// <summary>
		/// Gets the ordinal position of the column in the table.
		/// </summary>
		/// <value>
		/// The SQL ordinal position.
		/// </value>
		int SqlOrdinal
		{
			get;
		}

		/// <summary>
		/// Gets the default value for the column.
		/// </summary>
		/// <value>
		/// The default value.
		/// </value>
		object SqlDefaultValue
		{
			get;
		}
	}
}