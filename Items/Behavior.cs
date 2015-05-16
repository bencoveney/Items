using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// This defines some functionality which is either performed on/by an instance of an item, or cohesively grouped with the item
    /// </summary>
    public class Behavior
    {
        /// <summary>
        /// What the behavior is
        /// readonly?
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Defines whether you can or cannot do something
        /// </summary>
        public List<Condition> Conditions
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines what the behavior takes as input
        /// </summary>
        public List<IParameter> Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines the changes the behavior makes to the model, for example creation of an item, or a change to an existing single item, or multiple items, or itself
        /// Should be ordered
        /// </summary>
        public List<Action> Actions
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether the behavior is on an instance of an object, or static
        /// Readonly?
        /// </summary>
        public BehaviorLevel BehaviorLevel
        {
            get;
            private set;
        }

        /// <summary>
        /// A behavior has a name which identifies it and a level (instance or static)
        /// </summary>
        public Behavior(string name, BehaviorLevel behaviorLevel)
        {
            Name = name;
            Conditions = new List<Condition>();
            Parameters = new List<IParameter>();
            Actions = new List<Action>();
            BehaviorLevel = behaviorLevel;
        }
    }
}
