using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	/// <summary>
	/// An Items.IThing which represents a database object.
	/// Perhaps the Sql... properties could be replaced by a DBO class.
	/// Also not all of these properties are relevant to every type of thing eg fkconstraints are only really used for relationships
	/// </summary>
	public interface IDbiThing
		: IThing
	{
		/// <summary>
		/// Gets the SQL catalog the thing belongs to.
		/// </summary>
		/// <value>
		/// The SQL catalog.
		/// </value>
		string SqlCatalog
		{
			get;
		}

		/// <summary>
		/// Gets the SQL schema the thing belongs to.
		/// </summary>
		/// <value>
		/// The SQL schema.
		/// </value>
		string SqlSchema
		{
			get;
		}

		/// <summary>
		/// Gets the SQL table the thing represents.
		/// </summary>
		/// <value>
		/// The SQL table.
		/// </value>
		string SqlTable
		{
			get;
		}

		/// <summary>
		/// Gets the SQL constraint that the thing represents.
		/// </summary>
		/// <value>
		/// The SQL constraint.
		/// </value>
		string SqlConstraint
		{
			get;
		}

		/// <summary>
		/// Gets the SQL columns that the thing represents.
		/// </summary>
		/// <value>
		/// The SQL columns.
		/// </value>
		string SqlColumns
		{
			get;
		}
	}
}
