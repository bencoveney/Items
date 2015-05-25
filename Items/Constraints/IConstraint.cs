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
		/// <summary>
		/// Gets or sets a value indicating whether this constraint only applies to instances when they are committed to the model.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is deferrable; otherwise, <c>false</c>.
		/// </value>
		bool IsDeferrable
		{
			get;
			set;
		}
	}
}