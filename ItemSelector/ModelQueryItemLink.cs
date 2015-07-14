namespace ItemSelector
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Text;
	using Items;
	
	/// <summary>
	/// A link to an item in the query
	/// </summary>
	internal class ModelQueryItemLink
	{
		/// <summary>
		/// The item this link points to
		/// </summary>
		private Item item;

		/// <summary>
		/// The child links
		/// </summary>
		private Dictionary<Relationship, ModelQueryItemLink> childLinks;

		/// <summary>
		/// The selected data members
		/// </summary>
		private Collection<DataMember> selectedDataMembers;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelQueryItemLink"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		internal ModelQueryItemLink(Item item)
		{
			this.item = item;
			this.childLinks = new Dictionary<Relationship, ModelQueryItemLink>();
			this.selectedDataMembers = new Collection<DataMember>();
		}

		/// <summary>
		/// Gets the item.
		/// </summary>
		/// <value>
		/// The item.
		/// </value>
		internal Item Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>
		/// Gets this link and all child links.
		/// </summary>
		/// <returns>This link and all child links.</returns>
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

		/// <summary>
		/// Joins the through relationship.
		/// </summary>
		/// <param name="relationship">The relationship.</param>
		/// <param name="target">The target.</param>
		internal void JoinThroughRelationship(Relationship relationship, Item target)
		{
			// Check the relationship goes from the current item to the target
			// Check the relationship/target hasn't already been linked?
			// Maybe allow an overload where you only specify the relationship? is there any instance where you'd need the target?
			// What happens when an item is in a relationship with itself? lol

			this.childLinks.Add(relationship, new ModelQueryItemLink(target));
		}

		/// <summary>
		/// Populates the selected columns.
		/// </summary>
		/// <param name="columns">The columns.</param>
		/// <exception cref="ArgumentNullException">columns;columns cannot be null</exception>
		internal void PopulateSelectedColumns(ref Dictionary<Item, IEnumerable<DataMember>> columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns", "columns cannot be null");
			}

			// Work out which data members have been included from this link's items
			IEnumerable<DataMember> dataMembers;
			if (this.selectedDataMembers.Count > 0)
			{
				dataMembers = this.selectedDataMembers;
			}
			else
			{
				// If there are no data members defined, build up a selection of some.
				// TODO finalise what we should do in this situation. The user might specifically not want to show rows from this table.
				Collection<DataMember> identifiers = new Collection<DataMember>();

				// If there is an integer identifier, show it
				if (this.Item.IntegerIdentifier != null)
				{
					identifiers.Add(this.Item.IntegerIdentifier);
				}

				// If there is a string identifier, show it
				if (this.Item.StringIdentifier != null)
				{
					identifiers.Add(this.Item.StringIdentifier);
				}

				dataMembers = identifiers;
			}

			columns.Add(this.Item, dataMembers);

			// Recurse on child links
			foreach (ModelQueryItemLink link in this.childLinks.Values)
			{
				link.PopulateSelectedColumns(ref columns);
			}
		}

		/// <summary>
		/// Builds a string containing the join statements for all child links.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		internal void AppendJoins(StringBuilder stringBuilder)
		{
			const string JoinFormat = @"
	{0} JOIN {1}.{2}.{3} ON {5}.{4} = {3}.{4}";

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
					CultureInfo.InvariantCulture,
					JoinFormat,
					isInnerJoin ? "INNER" : "LEFT OUTER",
					toLink.Thing.Details["SqlCatalog"],
					toLink.Thing.Details["SqlSchema"],
					toLink.Thing.Details["SqlTable"],
					childLink.Key.Details["SqlColumns"],
					fromLink.Thing.Details["SqlTable"]);

				childLink.Value.AppendJoins(stringBuilder);
			}
		}

		/// <summary>
		/// Includes the column.
		/// </summary>
		/// <param name="dataMember">The data member.</param>
		internal void IncludeDataMember(DataMember dataMember)
		{
			this.selectedDataMembers.Add(dataMember);
		}

		/// <summary>
		/// Excludes the column.
		/// </summary>
		/// <param name="dataMember">The data member.</param>
		internal void ExcludeDataMember(DataMember dataMember)
		{
			this.selectedDataMembers.Remove(dataMember);
		}
	}
}
