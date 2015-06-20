using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;
	using System.Data.SqlClient;

namespace ItemSelector
{
	public class ModelQuery
	{
		ModelQueryItemLink rootItemLink;

		public ModelQuery(Item rootItem)
		{
			rootItemLink = new ModelQueryItemLink(rootItem);
		}

		internal IEnumerable<Item> GetAllItems()
		{
			return GetAllLinks().Select<ModelQueryItemLink, Item>(link => link.Item);
		}

		internal IEnumerable<ModelQueryItemLink> GetAllLinks()
		{
			return rootItemLink.GetAllItemLinks();
		}

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

		public SqlCommand GetQuery(SqlConnection connection)
		{
			StringBuilder queryText = new StringBuilder();
			queryText.AppendFormat(@"SELECT
	*
FROM
	{0}", this.rootItemLink.Item.Details["SqlTable"]);
			this.rootItemLink.AppendJoinsToStringBuilder(queryText);
			return new SqlCommand(queryText.ToString(), connection);
		}
	}
}
