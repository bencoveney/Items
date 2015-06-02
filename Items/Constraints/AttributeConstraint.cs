namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Categorizes whether a value is in a collection
	/// </summary>
	public enum CollectionComparison
	{
		/// <summary>
		/// Returns true if the value exists in the collection
		/// </summary>
		ExistsIn = 1,

		/// <summary>
		/// Returns true if the value doesn't exists in the collection
		/// </summary>
		DoesntExistIn = 2,

		/// <summary>
		/// Returns true if the value is unique within the collection
		/// Might require additional logic to determine whether the item instance is being included in the check as it might match itself
		/// </summary>
		IsUniqueWithin
	}

	/// <summary>
	/// Performs a comparison to all other values within an attribute for a given item
	/// Can be used for unique constraints (not in attribute)
	/// Can be used for foreign keys (in other item's attribute)
	/// </summary>
	public class AttributeConstraint
		: IConstraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttributeConstraint"/> class.
		/// </summary>
		/// <param name="attribute">The attribute.</param>
		/// <param name="comparison">The comparison.</param>
		public AttributeConstraint(DataAttribute attribute, CollectionComparison comparison)
		{
			this.Attribute = attribute;
			this.Comparison = comparison;
		}

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <value>
		/// The attribute.
		/// </value>
		public DataAttribute Attribute
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
		public CollectionComparison Comparison
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
