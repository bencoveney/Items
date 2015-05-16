using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Defines a type which exists in the system
    /// </summary>
    public partial class SystemType<T> : IType
    {
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
        public String Name
        {
            get
            {
                return Type.FullName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemType{T}"/> class.
        /// </summary>
        public SystemType()
        {
        }
    }
}
