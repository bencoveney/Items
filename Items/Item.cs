﻿namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines a thing at the level of the database table/entity/action
    /// </summary>
    public class Item
        : ItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// Creates the item
        /// </summary>
        /// <param name="name">The name</param>
        public Item(string name)
            : base(name)
        {
            Behaviors = new Behaviors();
        }

        /// <summary>
        /// Gets the list of actions the item can perform (or static actions related to the behavior)
        /// should static actions be a specific type of behavior which don't take the item as a parameter?
        /// should be Dictionary
        /// </summary>
        public Behaviors Behaviors
        {
            get;
            private set;
        }
    }
}