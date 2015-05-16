using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// Performs a numeric (incl datetimes) comparison against the specified static value
    /// Can be used for bounds checking (eg must be greater than, must be less than)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NumericValueConstraint<T>
        : IConstraint
    {
        /// <summary>
        /// Gets the value to perform the comparison against.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value
        {
            get;
            private set;
        }

        public ValueComparison Comparison
        {
            get;
            private set;
        }

        public NumericValueConstraint(ValueComparison comparison, T value)
        {
            Comparison = comparison;
            Value = value;
        }
    }

    /// <summary>
    /// Lists the type of comparisons that can be performed on a
    /// </summary>
    public enum ValueComparison
    {
        EqualTo = 1,
        GreaterThan = 2,
        LessThan = 3,
        GreaterThanOrEqualTo = 4,
        LessThanOrEqualTo = 5,
        NotEqualTo = 6,
        EvenlyDivisibleBy = 7,
        NotEvenlyDivisibleBy = 8
    }
}