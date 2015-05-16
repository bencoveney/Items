using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Defines a thing at the level of the database table/entity/action
    /// </summary>
    public class Item
        : ItemBase
    {
        /// <summary>
        /// The list of actions the item can perform (or static actions related to the behavior)
        /// should static actions be a specific type of behavior which dont take the iten as a parameter?
        /// should be Dictionary
        /// </summary>
        public Behaviors Behaviors
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates the item
        /// </summary>
        public Item(String name)
            : base(name)
        {
            Behaviors = new Behaviors();
        }
    }
}