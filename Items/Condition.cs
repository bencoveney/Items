using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public class Condition
    {
        //readonly?
        public String Name
        {
            get;
            set;
        }

        // pointers to attributes of other items
        // static values
        // enums
        // Readonly?
        public List<object> Inputs
        {
            get;
            set;
        }

        // Condition comparison type? this = that? this in enum? this exists? this is of type?
    }
}
