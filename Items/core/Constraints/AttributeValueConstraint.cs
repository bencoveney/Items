namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Performs a comparison against the value of the specified attribute on this item instance
    /// </summary>
    public class AttributeValueConstraint
        : IConstraint
    {
        /// <summary>
        /// Gets or sets a value indicating whether this constraint only applies to instances when they are committed to the model.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deferrable; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeferrable
        {
            get;
            set;
        }
    }
}
