using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public class InvalidModelException
        : Exception
    {
        public InvalidModelException(String message)
            : base(message)
        {
        }
    }
}
