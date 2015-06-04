namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;

	/// <summary>
	/// A collection of items and categories
	/// </summary>
	public class Model
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Model"/> class.
		/// </summary>
		public Model()
		{
			this.Items = new Dictionary<string, Item>();
			this.Categories = new Dictionary<string, Category>();
			this.Relationships = new Dictionary<string, Relationship>();
		}

		/// <summary>
		/// Gets the items in this model.
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public Dictionary<string, Item> Items
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the categories in this model.
		/// </summary>
		/// <value>
		/// The categories.
		/// </value>
		public Dictionary<string, Category> Categories
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the relationships between things in this model.
		/// </summary>
		/// <value>
		/// The relationships.
		/// </value>
		public Dictionary<string, Relationship> Relationships
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a collection of all things represented by the model
		/// </summary>
		/// <value>
		/// The things.
		/// </value>
		public IEnumerable<Thing> Things
		{
			get
			{
				return ((IEnumerable<Thing>)this.Items.Values).Concat(this.Categories.Values).Concat(this.Relationships.Values);
			}
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="item">The item.</param>
		public void AddItem(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "item cannot be null");
			}

			this.Items.Add(item.Name, item);
		}

		/// <summary>
		/// Adds the category.
		/// </summary>
		/// <param name="category">The category.</param>
		public void AddCategory(Category category)
		{
			if (category == null)
			{
				throw new ArgumentNullException("category", "category cannot be null");
			}

			this.Categories.Add(category.Name, category);
		}

		/// <summary>
		/// Adds the relationship.
		/// </summary>
		/// <param name="relationship">The relationship.</param>
		public void AddRelationship(Relationship relationship)
		{
			if (relationship == null)
			{
				throw new ArgumentNullException("relationship", "relationship cannot be null");
			}

			this.Relationships.Add(relationship.Name, relationship);
		}

		/// <summary>
		/// Adds the thing to the correct collection.
		/// </summary>
		/// <param name="thing">The thing.</param>
		/// <exception cref="System.ArgumentException">Unknown thing type;thing</exception>
		public void AddThing(Thing thing)
		{
			// Only cast once
			// TODO there should probably only be one backing collection to avoid having to do this
			Item itemThing = thing as Item;
			Relationship relationshipThing = thing as Relationship;
			Category categoryThing = thing as Category;

			// Add to the relevant collection
			if (itemThing != null)
			{
				this.AddItem(itemThing);
			}
			else if (relationshipThing != null)
			{
				this.AddRelationship(relationshipThing);
			}
			else if (categoryThing != null)
			{
				this.AddCategory(categoryThing);
			}
			else
			{
				throw new ArgumentException("Unknown thing type", "thing");
			}
		}

		/// <summary>
		/// Validates the model
		/// </summary>
		public void Validate()
		{
			// For all items
			foreach (Item item in this.Items.Values)
			{
				// Check for items with no identifiers 
				if (item.IntegerIdentifier == null && item.StringIdentifier == null)
				{
					throw new InvalidModelException(string.Format(CultureInfo.InvariantCulture, "Item {0} has no identifiers", item.Name));
				}

				// Check for attributes which refer to items which don't exist
				foreach (DataMember attribute in item.Attributes.Values.Where(attribute => attribute.DataType.GetType() == typeof(ItemType)))
				{
					// If the item type is neither a category nor an item
					// TODO handle categories seperately
					if (!this.Items.ContainsKey(((ItemType)attribute.DataType).Name) && !this.Categories.ContainsKey(((ItemType)attribute.DataType).Name))
					{
						// Disallow
						throw new InvalidModelException("Attribute has an item type which is not found in the model");
					}
				}
			}

			// Check for attributes which arent constrained sufficiently
			// Check for items where referrals are only one way
			// Check for constraints which dont fit the datatype of the attribute
		}
	}
}
