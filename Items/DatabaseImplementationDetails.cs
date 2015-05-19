namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial interface IType
    {
        string SqlDataType { get; set; }
    }

    public partial class SystemType<T>
        : IType
    {
        public string SqlDataType { get; set; }
    }

    public partial class ItemType
        : IType
    {
        public string SqlDataType { get; set; }
    }

    public partial class CategoryType
        : IType
    {
        public string SqlDataType { get; set; }
    }

    public partial class ItemBase
    {
        public string Description { get; set; }
    }

    public partial interface IAttribute
    {
        string SqlColumn { get; set; }
    }

    public partial class ValueAttribute
        : IAttribute
    {
        public string SqlColumn { get; set; }
    }

    public partial class CollectionAttribute
        : IAttribute
    {
        public string SqlColumn { get; set; }
    }
}