namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines an attribute type which is an Item in the model
    /// </summary>
    public class CategoryType : IType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryType" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CategoryType(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the Item
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
    }
}
