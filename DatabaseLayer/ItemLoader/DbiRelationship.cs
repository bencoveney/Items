using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	public class DbiRelationship
		: Relationship, IDbiThing
	{
		public DbiRelationship(string name, IDbiThing leftThing, IDbiThing rightThing, string sqlCatalog, string sqlSchema, string sqlTable, string sqlColumns, string sqlConstraint)
			: base(name, leftThing, rightThing)
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

			if (string.IsNullOrEmpty(sqlColumns))
			{
				throw new ArgumentNullException("sqlColumns", "sqlColumns must be provided");
			}

			this.SqlCatalog = sqlCatalog;
			this.SqlSchema = sqlSchema;
			this.SqlTable = sqlTable;
			this.SqlColumns = sqlColumns;
			this.SqlConstraint = sqlConstraint;
		}

		public DbiRelationship(string name, RelationshipLink leftLink, RelationshipLink rightLink, string sqlCatalog, string sqlSchema, string sqlTable, string sqlColumns, string sqlConstraint)
			: base(name, leftLink, rightLink)
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

			if (string.IsNullOrEmpty(sqlColumns))
			{
				throw new ArgumentNullException("sqlColumns", "sqlColumns must be provided");
			}

			if (string.IsNullOrEmpty(sqlConstraint))
			{
				throw new ArgumentNullException("sqlConstraint", "sqlConstraint must be provided");
			}

			this.SqlCatalog = sqlCatalog;
			this.SqlSchema = sqlSchema;
			this.SqlTable = sqlTable;
			this.SqlColumns = sqlColumns;
			this.SqlConstraint = sqlConstraint;
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
