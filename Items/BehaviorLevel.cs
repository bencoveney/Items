namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Is the behavior on an instance or on the item (like static)
    /// </summary>
    public enum BehaviorLevel
    {
        /// <summary>
        /// The instance
        /// </summary>
        Instance = 1,

        /// <summary>
        /// The static
        /// </summary>
        Static = 2
    }
}
