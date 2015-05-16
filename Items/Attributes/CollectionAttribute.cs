using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public partial class CollectionAttribute : IAttribute
    {
        // TODO can a collection really be null instead of just empty?
        // TODO support for attributes which dictate ordering

        /// <summary>
        /// The name of the data stored in this attribute
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The type of data in this attribute
        /// Should determine the ways in which the items can be searched for by attribute
        /// </summary>
        public IType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the conditions which the value of an attribute must satisfy
        /// </summary>
        public List<IConstraint> Constraints
        {
            get;
            private set;
        }

        /// <summary>
        /// Describes the way an attribute treats null values
        /// Do collections need a different type of nullability
        /// </summary>
        public Nullability Nullability
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="nullability">The nullability.</param>
        public CollectionAttribute(String name, ItemType type, Nullability nullability)
        {
            Name = name;
            Type = type;
            Constraints = new List<IConstraint>();
            Nullability = nullability;
        }
    }
}