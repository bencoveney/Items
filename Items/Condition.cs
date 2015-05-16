namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Dictates what needs to be met in order for something to happen/be allowed to happen
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the inputs.
        /// </summary>
        /// <value>
        /// The inputs.
        /// </value>
        public List<object> Inputs
        {
            get;
            set;
        }

        // Condition comparison type? this = that? this in enum? this exists? this is of type?
    }
}
