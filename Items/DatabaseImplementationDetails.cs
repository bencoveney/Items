namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A type which is natively represented in c#
    /// </summary>
    /// <typeparam name="T">The system type</typeparam>
    public partial class SystemType<T>
        : IType
    {
        /// <summary>
        /// Gets or sets the type of the SQL data.
        /// </summary>
        /// <value>
        /// The type of the SQL data.
        /// </value>
        public string SqlDataType { get; set; }
    }
}