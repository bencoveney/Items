namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // TODO If an empty value could be NULL/defined value (e.g empty string) where is the empty value defined?

    /// <summary>
    /// Describes whether an attribute accepts null values
    /// </summary>
    [Flags]
    public enum NullConstraints
    {
        /// <summary>
        /// Null values are not permitted
        /// TODO should there be a separate invalid state?
        /// </summary>
        None = 0,

        /// <summary>
        /// Null values signify that the field is intentionally blank
        /// </summary>
        Empty = 1,

        /// <summary>
        /// Null values signify that the field isn't relevant to this instance of the item
        /// </summary>
        NotApplicable = 2,

        /// <summary>
        /// This value can be null or not applicable (for example string could be null or empty, date could be null or date.MinValue)
        /// </summary>
        EmptyOrNotApplicable = Empty | NotApplicable
    }
}
