using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public class Model
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public Dictionary<String, Item> Items
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
        public Dictionary<String, Category> Categories
        {
            get;
            set;
        }

        public Model()
        {
            Items = new Dictionary<string, Item>();
            Categories = new Dictionary<string, Category>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item.Name, item);
        }

        public void AddCategory(Category category)
        {
            Categories.Add(category.Name, category);
        }

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
