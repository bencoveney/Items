namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An object which needs to define the data it contains
    /// </summary>
    public interface IDataDefinition
    {
        /// <summary>
        /// Gets the name of the data stored in this attribute
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the type of data in this attribute
        /// Should determine the ways in which the items can be searched for by attribute
        /// </summary>
        IType DataType
        {
            get;
        }

        /// <summary>
        /// Gets the conditions which the value of an attribute must satisfy
        /// </summary>
        Collection<IConstraint> Constraints
        {
            get;
        }

        /// <summary>
        /// Gets the way an attribute treats null values
        /// </summary>
        NullConstraints NullConstraint
        {
            get;
        }
    }
}
