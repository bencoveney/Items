namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines a type which exists in the system
    /// </summary>
    /// <typeparam name="T">The system type</typeparam>
    public partial class SystemType<T>
        : IType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemType{T}"/> class.
        /// </summary>
        public SystemType()
        {
        }

        /// <summary>
        /// Gets system type
        /// </summary>
        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// Gets the type's name through reflection
        /// </summary>
        public string Name
        {
            get
            {
                return Type.FullName;
            }
        }
    }
}
