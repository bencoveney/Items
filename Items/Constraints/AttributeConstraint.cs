using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Performs a comparison to all other values within an attribute for a given item
    /// Can be used for unique constraints (not in attribute)
    /// Can be used for foreign keys (in other item's attribute)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttributeConstraint
        : IConstraint
    {
        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public IAttribute Attribute
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the comparison.
        /// </summary>
        /// <value>
        /// The comparison.
        /// </value>
        public CollectionComparison Comparison
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeConstraint{T}"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="comparison">The comparison.</param>
        public AttributeConstraint(IAttribute attribute, CollectionComparison comparison)
        {
            Attribute = attribute;
            Comparison = comparison;
        }
    }

    public enum CollectionComparison
    {
        /// <summary>
        /// Returns true if the value exists in the collection
        /// </summary>
        ExistsIn = 1,

        /// <summary>
        /// Returns true if the value doesnt exists in the collection
        /// </summary>
        DoesntExistIn = 2,

        /// <summary>
        /// Returns true if the value is unique within the collection
        /// Might require additional logic to determine whether the item instance is being included in the check as it might match itself
        /// </summary>
        IsUniqueWithin
    }
}
