namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Lists the types of comparison that can be performed against a string
    /// </summary>
    public enum LengthComparison
    {
        /// <summary>
        /// The longer than
        /// </summary>
        LongerThan = 1,

        /// <summary>
        /// The shorter than
        /// </summary>
        ShorterThan = 2,

        /// <summary>
        /// The exactly
        /// </summary>
        Exactly = 3
    }

    /// <summary>
    /// Performs a string comparison against the specified static value
    /// </summary>
    public class StringLengthConstraint
        : IConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthConstraint" /> class.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value">The value.</param>
        public StringLengthConstraint(LengthComparison comparison, int value)
        {
            this.Comparison = comparison;
            this.Value = value;
        }

        /// <summary>
        /// Gets the not empty.
        /// </summary>
        /// <value>
        /// The not empty.
        /// </value>
        public static StringLengthConstraint NotEmpty
        {
            get
            {
                return new StringLengthConstraint(LengthComparison.LongerThan, 0);
            }
        }

        /// <summary>
        /// Gets the value to perform the comparison against.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of comparison the constraint will perform
        /// </summary>
        /// <value>
        /// The comparison.
        /// </value>
        public LengthComparison Comparison
        {
            get;
            private set;
        }
    }
}
