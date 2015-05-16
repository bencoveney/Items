using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public partial class ValueAttribute : IAttribute
    {
        public string Name
        {
            get;
            private set;
        }

        public IType Type
        {
            get;
            private set;
        }

        public List<IConstraint> Constraints
        {
            get;
            private set;
        }

        public Nullability Nullability
        {
            get;
            private set;
        }

        public ValueAttribute(String name, IType type, Nullability nullability)
        {
            Name = name;
            Type = type;
            Constraints = new List<IConstraint>();
            Nullability = nullability;
        }
    }
}