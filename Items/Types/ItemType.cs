using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Defines an attribute type which is an Item in the model
    /// </summary>
    public class ItemType : IType
    {
        /// <summary>
        /// Gets the name of the Item
        /// </summary>
        public String Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ItemType(String name)
        {
            Name = name;
        }
    }
}
