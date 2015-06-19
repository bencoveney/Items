using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;
using System.Collections.ObjectModel;

namespace ItemSelector
{
	class ModelQueryItemLink
	{
		private Item item;

		private Dictionary<Relationship, ModelQueryItemLink> childLinks;

		public ModelQueryItemLink(Item item)
		{
			this.item = item;
			this.childLinks = new Dictionary<Relationship, ModelQueryItemLink>();
		}

		public void JoinThroughRelationship(Relationship relationship, Item target)
		{
			// Check the relationship goes from the current item to the target
			// Check the relationship/target hasn't already been linked?
			// Maybe allow an overload where you only specify the relationship? is there any instance where you'd need the target?
			// What happens when an item is in a relationship with itself? lol
		}
	}
}
