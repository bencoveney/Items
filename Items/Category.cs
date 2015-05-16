using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    // TODO From model - make into proper category classes

    /// <summary>
    /// Categories are similar to items in the way they contain data
    /// Unlike items, categories do not have any functionality (not even add/edit/delete)
    /// instances of them are not created
    /// </summary>
    public class Category
        : ItemBase
    {
        public Category(String name)
            : base(name)
        {

        }
    }
}
