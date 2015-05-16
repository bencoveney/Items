namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An attribute which is a collection of items
    /// </summary>
    public partial class CollectionAttribute
        : IAttribute
    {
        // TODO can a collection really be null instead of just empty?
        // TODO support for attributes which dictate ordering

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="nullability">The emptiness.</param>
        public CollectionAttribute(string name, ItemType type, Nullability nullability)
        {
            this.Name = name;
            this.Type = type;
            this.Constraints = new List<IConstraint>();
            this.Nullability = nullability;
        }

        /// <summary>
        /// Gets the name of the data stored in this attribute
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of data in this attribute
        /// Should determine the ways in which the items can be searched for by attribute
        /// </summary>
        public IType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the conditions which the value of an attribute must satisfy
        /// </summary>
        public List<IConstraint> Constraints
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the way an attribute treats null values
        /// Do collections need a different type of emptiness
        /// </summary>
        public Nullability Nullability
        {
            get;
            private set;
        }
    }
}