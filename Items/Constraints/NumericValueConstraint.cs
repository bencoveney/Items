namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

	/// <summary>
	/// Lists the type of comparisons that can be performed on a
	/// </summary>
	public enum NumericValueComparison
	{
		/// <summary>
		/// Invalid value
		/// </summary>
		None = 0,

		/// <summary>
		/// The equal to
		/// </summary>
		EqualTo = 1,

		/// <summary>
		/// The greater than
		/// </summary>
		GreaterThan = 2,

		/// <summary>
		/// The less than
		/// </summary>
		LessThan = 3,

		/// <summary>
		/// The greater than or equal to
		/// </summary>
		GreaterThanOrEqualTo = 4,

		/// <summary>
		/// The less than or equal to
		/// </summary>
		LessThanOrEqualTo = 5,

		/// <summary>
		/// The not equal to
		/// </summary>
		NotEqualTo = 6,

		/// <summary>
		/// The evenly divisible by
		/// </summary>
		EvenlyDivisibleBy = 7,

		/// <summary>
		/// The not evenly divisible by
		/// </summary>
		NotEvenlyDivisibleBy = 8
	}

	/// <summary>
	/// Performs a numeric (including dates and times) comparison against the specified static value
	/// Can be used for bounds checking (for example must be greater than, must be less than)
	/// </summary>
	/// <typeparam name="T">The type of numeric value</typeparam>
    [DataContract]
	public class NumericValueConstraint<T>
		: IConstraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NumericValueConstraint{T}"/> class.
		/// </summary>
		/// <param name="comparison">The comparison.</param>
		/// <param name="value">The value.</param>
		public NumericValueConstraint(NumericValueComparison comparison, T value)
		{
			this.Comparison = comparison;
			this.Value = value;
		}

		/// <summary>
		/// Gets the value to perform the comparison against.
		/// </summary>
		/// <value>
		/// The value.
        /// </value>
        [DataMember]
		public T Value
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the comparison.
		/// </summary>
		/// <value>
		/// The comparison.
        /// </value>
        [DataMember]
		public NumericValueComparison Comparison
		{
			get;
			private set;
		}

        /// <summary>
        /// Gets or sets a value indicating whether this constraint only applies to instances when they are committed to the model.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deferrable; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
		public bool IsDeferrable
		{
			get;
			set;
		}
	}
}