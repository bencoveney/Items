using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	/// <summary>
	/// An Item backed by a database table
	/// </summary>
	public class DbiItem
		: Item, IDbiThing
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DbiItem"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="sqlCatalog">The SQL catalog.</param>
		/// <param name="sqlSchema">The SQL schema.</param>
		/// <param name="sqlTable">The SQL table.</param>
		/// <exception cref="System.ArgumentNullException">
		/// sqlCatalog;sqlCatalog must be provided
		/// or
		/// sqlSchema;sqlSchema must be provided
		/// or
		/// sqlTable;sqlTable must be provided
		/// </exception>
		public DbiItem(string name, string sqlCatalog, string sqlSchema, string sqlTable)
			: base(name)
		{
			if (string.IsNullOrEmpty(sqlCatalog))
			{
				throw new ArgumentNullException("sqlCatalog", "sqlCatalog must be provided");
			}

			if (string.IsNullOrEmpty(sqlSchema))
			{
				throw new ArgumentNullException("sqlSchema", "sqlSchema must be provided");
			}

			if (string.IsNullOrEmpty(sqlTable))
			{
				throw new ArgumentNullException("sqlTable", "sqlTable must be provided");
			}

			this.SqlCatalog = sqlCatalog;
			this.SqlSchema = sqlSchema;
			this.SqlTable = sqlTable;
		}

		/// <summary>
		/// Gets the SQL catalog the thing belongs to.
		/// </summary>
		/// <value>
		/// The SQL catalog.
		/// </value>
		public string SqlCatalog
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the SQL schema the thing belongs to.
		/// </summary>
		/// <value>
		/// The SQL schema.
		/// </value>
		public string SqlSchema
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the SQL table the thing belongs to.
		/// </summary>
		/// <value>
		/// The SQL table.
		/// </value>
		public string SqlTable
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the SQL columns that the thing represents.
		/// </summary>
		/// <value>
		/// The SQL columns.
		/// </value>
		public string SqlColumns
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the SQL columns that the thing represents.
		/// </summary>
		/// <value>
		/// The SQL columns.
		/// </value>
		public string SqlConstraint
		{
			get;
			private set;
		}
	}
}
