namespace ItemSelector
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using Items;
	using ItemLoader;

	/// <summary>
	/// A query against the model data
	/// </summary>
	public class ModelQuery
	{
		/// <summary>
		/// The item link at the root of the query
		/// </summary>
		private ModelQueryItemLink rootItemLink;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelQuery"/> class.
		/// </summary>
		/// <param name="rootItem">The root item.</param>
		public ModelQuery(DbiItem rootItem)
		{
			this.rootItemLink = new ModelQueryItemLink(rootItem);
		}

		/// <summary>
		/// Includes the data member.
		/// </summary>
		/// <param name="dataMember">The data member.</param>
		/// <exception cref="ArgumentNullException">dataMember;dataMember cannot be null</exception>
		public void IncludeDataMember(DbiDataMember dataMember)
		{
			// Check for nulls
			if (dataMember == null)
			{
				throw new ArgumentNullException("dataMember", "dataMember cannot be null");
			}

			// Add the data member as an included column on the link
			this.GetLinkContainingDataMember(dataMember).IncludeDataMember(dataMember);
		}

		/// <summary>
		/// Excludes the specified data member.
		/// </summary>
		/// <param name="dataMember">The data member.</param>
		/// <exception cref="ArgumentNullException">dataMember;dataMember cannot be null</exception>
		public void ExcludedDataMember(DbiDataMember dataMember)
		{
			// Check for nulls
			if (dataMember == null)
			{
				throw new ArgumentNullException("dataMember", "dataMember cannot be null");
			}

			// Add the data member as an included column on the link
			this.GetLinkContainingDataMember(dataMember).ExcludeDataMember(dataMember);
		}

		/// <summary>
		/// Joins the through relationship.
		/// </summary>
		/// <param name="relationship">The relationship.</param>
		public void JoinThroughRelationship(DbiRelationship relationship)
		{
			// Check a relationship was provided
			if (relationship == null)
			{
				throw new ArgumentNullException("relationship", "relationship cannot be null");
			}

			if (string.IsNullOrEmpty(relationship.SqlConstraint as string))
			{
				throw new NotSupportedException("Currently only foreign key relationships are supported");
			}

			// Check the relationship is a link between two Items
			if (relationship.LinkedThings.OfType<DbiItem>().Count() != 2)
			{
				throw new NotSupportedException("Currently only two items can be linked");
			}

			// Check the relationship isn't linked to itself
			if (relationship.LeftLink.Thing == relationship.RightLink.Thing)
			{
				throw new NotSupportedException("Relationships between the same item are not currently supported");
			}

			// Check the relationship is going from an item we have to an item we don't
			IEnumerable<DbiItem> allItems = this.GetAllItems();
			if (allItems.Count(item => relationship.LinkedThings.Contains(item)) != 1)
			{
				throw new InvalidOperationException("The relationship must contain exactly one Thing which already exists in the query");
			}

			// Work out which existing link the new link is being created from
			IEnumerable<ModelQueryItemLink> allLinks = this.GetAllLinks();
			ModelQueryItemLink linkFrom = allLinks.Single(link => relationship.LinkedThings.Contains(link.Item));

			// At this point the only thing left to work out is whether the link follows the relationship from left to right or vice versa
			if (linkFrom.Item == relationship.LeftLink.Thing)
			{
				linkFrom.JoinThroughRelationship(relationship, relationship.RightLink.Thing as DbiItem);
			}
			else
			{
				linkFrom.JoinThroughRelationship(relationship, relationship.LeftLink.Thing as DbiItem);
			}
		}

		/// <summary>
		/// Gets the SQL which represents the current query.
		/// </summary>
		/// <returns>The SQL which represents the current query.</returns>
		public string BuildSql()
		{
			const string SelectStatement = @"SELECT
	";

			// Begin the select clause
			StringBuilder queryText = new StringBuilder();
			queryText.AppendFormat(CultureInfo.InvariantCulture, SelectStatement);
			
			// Work out which columns are being selected
			Dictionary<DbiItem, IEnumerable<DbiDataMember>> queryColumns = new Dictionary<DbiItem, IEnumerable<DbiDataMember>>();
			this.rootItemLink.PopulateSelectedColumns(ref queryColumns);

			// Build the statements for the columns in the select statement
			// Iterate through the items
			Collection<string> columnClauses = new Collection<string>();
			foreach (KeyValuePair<DbiItem, IEnumerable<DbiDataMember>> itemWithColumns in queryColumns)
			{
				// Iterate through the data members for those items
				foreach (DbiDataMember column in itemWithColumns.Value)
				{
					columnClauses.Add(string.Format(CultureInfo.InvariantCulture, @"{0}.{1} AS ""{0} {1}""", itemWithColumns.Key.SqlTable, column.SqlColumn));
				}
			}

			// join the individual select statements into a single unit
			queryText.Append(
				string.Join(
					@",
	",
					columnClauses));

			// Begin the from clause
			queryText.AppendFormat(
				@"
FROM
	{0}", 
				this.rootItemLink.Item.SqlTable);

			// Add all the joins to the FROM clause
			this.rootItemLink.AppendJoins(queryText);
			return queryText.ToString();
		}

		/// <summary>
		/// Gets all items included in the query.
		/// </summary>
		/// <returns>A collection of all items included in the query.</returns>
		internal IEnumerable<DbiItem> GetAllItems()
		{
			return this.GetAllLinks().Select<ModelQueryItemLink, DbiItem>(link => link.Item);
		}

		/// <summary>
		/// Gets all links included in the query.
		/// </summary>
		/// <returns>A collection of all items included in the query.</returns>
		internal IEnumerable<ModelQueryItemLink> GetAllLinks()
		{
			return this.rootItemLink.GetAllItemLinks();
		}

		/// <summary>
		/// Gets the link containing the specified data member.
		/// </summary>
		/// <param name="dataMember">The data member.</param>
		/// <returns>The link which contains the specified data member.</returns>
		/// <exception cref="ArgumentException">The specified data member could not be found in the current query;dataMember</exception>
		private ModelQueryItemLink GetLinkContainingDataMember(DbiDataMember dataMember)
		{
			// Attempt to find the link containing the data member
			try
			{
				return this.GetAllLinks().Single(link => link.Item.Attributes.Contains(dataMember));
			}
			catch (InvalidOperationException exception)
			{
				throw new ArgumentException("The specified data member could not be found in the current query", "dataMember", exception);
			}
		}
	}
}
