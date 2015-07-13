namespace ItemSelector
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using Items;

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
		public ModelQuery(Item rootItem)
		{
			this.rootItemLink = new ModelQueryItemLink(rootItem);
		}

		/// <summary>
		/// Includes the data member.
		/// </summary>
		/// <param name="dataMember">The data member.</param>
		/// <exception cref="ArgumentNullException">dataMember;dataMember cannot be null</exception>
		public void IncludeDataMember(DataMember dataMember)
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
		public void ExcludedDataMember(DataMember dataMember)
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
		public void JoinThroughRelationship(Relationship relationship)
		{
			// Check a relationship was provided
			if (relationship == null)
			{
				throw new ArgumentNullException("relationship", "relationship cannot be null");
			}

			if (string.IsNullOrEmpty(relationship.Details["SqlConstraint"] as string))
			{
				throw new NotSupportedException("Currently only foreign key relationships are supported");
			}

			// Check the relationship is a link between two Items
			if (relationship.LinkedThings.OfType<Item>().Count() != 2)
			{
				throw new NotSupportedException("Currently only two items can be linked");
			}

			// Check the relationship isn't linked to itself
			if (relationship.LeftLink.Thing == relationship.RightLink.Thing)
			{
				throw new NotSupportedException("Relationships between the same item are not currently supported");
			}

			// Check the relationship is going from an item we have to an item we don't
			IEnumerable<Item> allItems = this.GetAllItems();
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
				linkFrom.JoinThroughRelationship(relationship, relationship.RightLink.Thing as Item);
			}
			else
			{
				linkFrom.JoinThroughRelationship(relationship, relationship.LeftLink.Thing as Item);
			}
		}

		/// <summary>
		/// Gets the SQL which represents the current query.
		/// </summary>
		/// <returns>The SQL which represents the current query.</returns>
		public string GetSql()
		{
			// Begin the select clause
			StringBuilder queryText = new StringBuilder();
			queryText.AppendFormat(@"SELECT
	");
			
			// Work out which columns are being selected
			Dictionary<Item, IEnumerable<DataMember>> queryColumns = new Dictionary<Item, IEnumerable<DataMember>>();
			this.rootItemLink.PopulateSelectedColumns(ref queryColumns);

			// Build the statements for the columns in the select statement
			// Iterate through the items
			Collection<string> columnClauses = new Collection<string>();
			foreach (KeyValuePair<Item, IEnumerable<DataMember>> itemWithColumns in queryColumns)
			{
				// Iterate through the data members for those items
				foreach (DataMember column in itemWithColumns.Value)
				{
					columnClauses.Add(string.Format(@"{0}.{1} AS ""{0} {1}""", itemWithColumns.Key.Details["SqlTable"], column.Details["SqlColumn"]));
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
				this.rootItemLink.Item.Details["SqlTable"]);

			// Add all the joins to the FROM clause
			this.rootItemLink.AppendJoins(queryText);
			return queryText.ToString();
		}

		/// <summary>
		/// Gets all items included in the query.
		/// </summary>
		/// <returns>A collection of all items included in the query.</returns>
		internal IEnumerable<Item> GetAllItems()
		{
			return this.GetAllLinks().Select<ModelQueryItemLink, Item>(link => link.Item);
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
		private ModelQueryItemLink GetLinkContainingDataMember(DataMember dataMember)
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
