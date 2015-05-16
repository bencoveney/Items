using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public partial class CollectionAttribute : IAttribute
    {
        public String DatabaseColumnName;
    }

    public partial class ValueAttribute : IAttribute
    {
        public String DatabaseColumnName;
    }

    public partial class SystemType<T> : IType
    {
        public String SqlDataType;
    }
}
