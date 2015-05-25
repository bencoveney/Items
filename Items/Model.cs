namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

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
				return ((IEnumerable<Thing>)Items.Values).Concat(Categories.Values).Concat(Relationships.Values);
			}
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="item">The item.</param>
		public void AddItem(Item item)
		{
			this.Items.Add(item.Name, item);
		}

		/// <summary>
		/// Adds the category.
		/// </summary>
		/// <param name="category">The category.</param>
		public void AddCategory(Category category)
		{
			this.Categories.Add(category.Name, category);
		}

		public void AddRelationship(Relationship relationship)
		{
			this.Relationships.Add(relationship.Name, relationship);
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
				if (item.IntegerIdentifer == null && item.StringIdentifer == null)
				{
					throw new InvalidModelException(string.Format("Item {0} has no identifiers", item.Name));
				}

				// Check for attributes which refer to items which don't exist
				foreach (IAttribute attribute in item.Attributes.Values.Where(attribute => attribute.Type.GetType() == typeof(ItemType)))
				{
					// If the item type is neither a category nor an item
					// TODO handle categories seperately
					if (!this.Items.ContainsKey(((ItemType)attribute.Type).Name) && !this.Categories.ContainsKey(((ItemType)attribute.Type).Name))
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
