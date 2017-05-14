namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Lists the types of comparison that can be performed against a string
	/// </summary>
	public enum StringComparison
	{
		/// <summary>
		/// Invalid value
		/// </summary>
		None = 0,

		/// <summary>
		/// The match
		/// </summary>
		Match = 1,

		/// <summary>
		/// The doesn't match
		/// </summary>
		DoesNotMatch = 2,

		/// <summary>
		/// The begins with
		/// </summary>
		BeginsWith = 3,

		/// <summary>
		/// The doesn't begin with
		/// </summary>
		DoesNotBeginWith = 4,

		/// <summary>
		/// The end with
		/// </summary>
		EndWith = 5,

		/// <summary>
		/// The doesn't end with
		/// </summary>
		DoesNotEndWith = 6,

		/// <summary>
		/// The contains
		/// </summary>
		Contains = 7,

		/// <summary>
		/// The doesn't contain
		/// </summary>
		DoesNotContain = 8,

		/// <summary>
		/// The is contained by
		/// </summary>
		IsContainedBy = 9,

		/// <summary>
		/// The is not contained by
		/// </summary>
		IsNotContainedBy = 10,

		/// <summary>
		/// The matches regex
		/// </summary>
		MatchesRegex = 11,

		/// <summary>
		/// The doesn't match regex
		/// </summary>
		DoesNotMatchRegex = 12
	}

	/// <summary>
	/// Performs a string comparison against the specified static value
	/// </summary>
	public class StringValueConstraint
		: IConstraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringValueConstraint"/> class.
		/// </summary>
		/// <param name="comparison">The comparison.</param>
		/// <param name="comparer">The comparer.</param>
		/// <param name="value">The value.</param>
		public StringValueConstraint(StringComparison comparison, StringComparer comparer, string value)
		{
			this.Comparison = comparison;
			this.Comparer = comparer;
			this.Value = value;
			this.IsDeferrable = false;
		}

		/// <summary>
		/// Gets the value to perform the comparison against.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value
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
		public StringComparison Comparison
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets settings for culture and case sensitivity
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
		/// Gets or sets a value indicating whether this constraint only applies to instances when they are committed to the model.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is deferrable; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeferrable
		{
			get;
			set;
		}
	}
}
