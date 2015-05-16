namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This defines some functionality which is either performed on/by an instance of an item, or cohesively grouped with the item
    /// </summary>
    public class Behavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Behavior" /> class.
        /// A behavior has a name which identifies it and a level (instance or static)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="behaviorLevel">The behavior level.</param>
        public Behavior(string name, BehaviorLevel behaviorLevel)
        {
            this.Name = name;
            this.Conditions = new List<Condition>();
            this.Parameters = new List<IParameter>();
            this.Actions = new List<Action>();
            this.BehaviorLevel = behaviorLevel;
        }

        /// <summary>
        /// Gets or sets what the behavior is
        /// readonly?
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether you can or cannot do something
        /// </summary>
        public List<Condition> Conditions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets what the behavior takes as input
        /// </summary>
        public List<IParameter> Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the changes the behavior makes to the model, for example creation of an item, or a change to an existing single item, or multiple items, or itself
        /// Should be ordered
        /// </summary>
        public List<Action> Actions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets whether the behavior is on an instance of an object, or static
        /// Readonly?
        /// </summary>
        public BehaviorLevel BehaviorLevel
        {
            get;
            private set;
        }
    }
}
