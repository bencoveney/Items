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
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public Dictionary<string, Item> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public Dictionary<string, Category> Categories
        {
            get;
            set;
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

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            // Iterate through the model
            // Check for types which don't map to items/categories
            // Check for items which don't have identifiers
            // Check for attributes which arent constrained sufficiently
            // Check for items where referrals are only one way
            // Check for constraints which dont fit the datatype of the attribute
        }
    }
}
