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
			this.Items = new NamedCollection<Item>();
			this.Categories = new NamedCollection<Category>();
			this.Relationships = new NamedCollection<Relationship>();
		}

		/// <summary>
		/// Gets the items in this model.
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public NamedCollection<Item> Items
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
		public NamedCollection<Category> Categories
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
		public NamedCollection<Relationship> Relationships
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
				return ((IEnumerable<Thing>)this.Items).Concat(this.Categories).Concat(this.Relationships);
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

			this.Items.Add(item);
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

			this.Categories.Add(category);
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

			this.Relationships.Add(relationship);
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
			foreach (Item item in this.Items)
			{
				// Check for items with no identifiers 
				if (item.IntegerIdentifier == null && item.StringIdentifier == null)
				{
					throw new InvalidModelException(string.Format(CultureInfo.InvariantCulture, "Item {0} has no identifiers", item.Name));
				}
			}

			// Check for attributes which arent constrained sufficiently
			// Check for constraints which dont fit the datatype of the attribute
		}
	}
}
