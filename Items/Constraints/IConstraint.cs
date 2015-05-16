namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Some constraints (for example unique) only apply when the item exists in the database
    /// Some constraints should be ignored if null, some shouldn't
    /// </summary>
    public interface IConstraint
    {
        // Did used to have a predicate here but this is an implementation detail
    }
}