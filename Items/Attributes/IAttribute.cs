namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A named piece of data which an item possesses
    /// </summary>
    public interface IAttribute
    {
        // TODO something like this?
        // public object DefaultValue {get; set;}

        /// <summary>
        /// Gets the name of the data stored in this attribute
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets type of data in this attribute
        /// Should determine the ways in which the items can be searched for by attribute
        /// </summary>
        IType Type
        {
            get;
        }

        /// <summary>
        /// Gets the conditions which the value of an attribute must satisfy
        /// </summary>
        List<IConstraint> Constraints
        {
            get;
        }

        /// <summary>
        /// Gets the way an attribute treats null values
        /// </summary>
        Nullability Nullability
        {
            get;
        }
    }
}
