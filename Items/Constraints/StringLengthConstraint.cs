using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Lists the types of comparison that can be performed against a string
    /// </summary>
    public enum LengthComparison
    {
        LongerThan = 1,
        ShorterThan = 2,
        Exactly = 3
    }

    /// <summary>
    /// Performs a string comparison against the specified static value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringLengthConstraint
        : IConstraint
    {
        /// <summary>
        /// Gets the value to perform the comparison against.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Int32 Value
        {
            get;
            private set;
        }

        /// <summary>
        /// The type of comparison the constraint will perform
        /// </summary>
        /// <value>
        /// The comparison.
        /// </value>
        public LengthComparison Comparison
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueConstraint" /> class.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value">The value.</param>
        public StringLengthConstraint(LengthComparison comparison, Int32 value)
        {
            Comparison = comparison;
            Value = value;
        }

        public static StringLengthConstraint NotEmpty
        {
            get
            {
                return new StringLengthConstraint(LengthComparison.LongerThan, 0);
            }
        }
    }
}
