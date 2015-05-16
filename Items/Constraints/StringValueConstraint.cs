using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Performs a string comparison against the specified static value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringValueConstraint
        : IConstraint
    {
        /// <summary>
        /// Lists the types of comparison that can be performed against a string
        /// </summary>
        public enum StringComparison
        {
            Match = 1,
            DoesntMatch = 2,
            BeginsWith = 3,
            DoesntBeginWith = 4,
            EndWith = 5,
            DoesntEndWith = 6,
            Contains = 7,
            DoesntContain = 8,
            IsContainedBy = 9,
            IsNotContainedBy = 10,
            MatchesRegex = 11,
            DoesntMatchRegex = 12 
        }

        /// <summary>
        /// Gets the value to perform the comparison against.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public String Value
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
        public StringComparison Comparison
        {
            get;
            private set;
        }

        /// <summary>
        /// Settings for culture and case sensitivity
        /// </summary>
        /// <value>
        /// The comparer.
        /// </value>
        public StringComparer Comparer
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueConstraint"/> class.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="value">The value.</param>
        public StringValueConstraint(StringComparison comparison, StringComparer comparer, String value)
        {
            Comparison = comparison;
            Comparer = comparer;
            Value = value;
        }
    }
}
