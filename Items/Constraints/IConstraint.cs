using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Check which determines whether the value of an attribute is valid
    /// Could be determined by a check on the value of the attribute (limits checking)
    /// Could be determined by a check on the other attributes for this item
    /// Could be determined by a check of the same attributes for all instances of the item (unique constraint)
    /// Could be determined by a check for a value on another item (foreign key)
    /// Could be determined by a global check
    /// =, >, <, <=, >=, !=, in, not in, specificity
    /// 
    /// Some constraints (ie unique) only apply when the item exists in the database
    /// Some constraints should be ignored if null, some shouldn't
    /// </summary>
    public interface IConstraint
    {
        // Did used to have a predicate here but this is an implementation detail
    }
}