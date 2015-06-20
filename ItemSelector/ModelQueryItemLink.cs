using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;
using System.Collections.ObjectModel;

namespace ItemSelector
{
	internal class ModelQueryItemLink
	{
		/// <summary>
		/// The item
		/// </summary>
		private Item item;

		/// <summary>
		/// The child links
		/// </summary>
		private Dictionary<Relationship, ModelQueryItemLink> childLinks;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelQueryItemLink"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		internal ModelQueryItemLink(Item item)
		{
			this.item = item;
			this.childLinks = new Dictionary<Relationship, ModelQueryItemLink>();
		}

		internal Item Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>
		/// Gets all items in this link and all child links.
		/// </summary>
		/// <returns></returns>
		internal IEnumerable<Item> GetAllItems()
		{
			List<Item> items = new List<Item>();

			items.Add(this.item);

			foreach (ModelQueryItemLink childLink in this.childLinks.Values)
			{
				items.AddRange(childLink.GetAllItems());
			}

			return items;
		}

		/// <summary>
		/// Gets all items in this link and all child links.
		/// </summary>
		/// <returns></returns>
		internal IEnumerable<ModelQueryItemLink> GetAllItemLinks()
		{
			List<ModelQueryItemLink> links = new List<ModelQueryItemLink>();

			links.Add(this);

			foreach (ModelQueryItemLink childLink in this.childLinks.Values)
			{
				links.AddRange(childLink.GetAllItemLinks());
			}

			return links;
		}

		internal void JoinThroughRelationship(Relationship relationship, Item target)
		{
			// Check the relationship goes from the current item to the target
			// Check the relationship/target hasn't already been linked?
			// Maybe allow an overload where you only specify the relationship? is there any instance where you'd need the target?
			// What happens when an item is in a relationship with itself? lol

			childLinks.Add(relationship, new ModelQueryItemLink(target));
		}

		/// <summary>
		/// Builds a string containing the join statements for all child links.
		/// TODO pass the bulder around.
		/// </summary>
		/// <returns></returns>
		internal void AppendJoinsToStringBuilder(StringBuilder stringBuilder)
		{
			foreach (KeyValuePair<Relationship, ModelQueryItemLink> childLink in this.childLinks)
			{
				// Work out if we are travelling from left to right
				bool isLeftToRight = childLink.Key.LeftLink.Thing == this.item;
				RelationshipLink fromLink = isLeftToRight ? childLink.Key.LeftLink : childLink.Key.RightLink;
				RelationshipLink toLink = isLeftToRight ? childLink.Key.RightLink : childLink.Key.LeftLink;
				
				// If there is definitely going to be an outgoing link then we can INNER JOIN
				// TODO this makes a big assumption that both columns will be named the same thing. maybe this should be checked?
				bool isInnerJoin = fromLink.AmountLower >= 1;
				stringBuilder.AppendFormat(
					@"
	{0} JOIN {1}.{2}.{3} ON {5}.{4} = {3}.{4}",
					isInnerJoin ? "INNER" : "LEFT OUTER",
					toLink.Thing.Details["SqlCatalog"],
					toLink.Thing.Details["SqlSchema"],
					toLink.Thing.Details["SqlTable"],
					childLink.Key.Details["SqlColumns"],
					childLink.Key.Details["SqlTable"]);

				childLink.Value.AppendJoinsToStringBuilder(stringBuilder);
			}
		}
	}
}
