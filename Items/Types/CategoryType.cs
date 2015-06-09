namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Defines an attribute type which is an Item in the model
	/// TODO Remove, this is a relationship now
	/// </summary>
	public class CategoryType
		: IType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryType" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public CategoryType(string name)
		{
			this.Name = name;
			this.Details = new ImplementationDetailsDictionary();
		}

		/// <summary>
		/// Gets the name of the Item
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the implementation specific details attached to the type
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public ImplementationDetailsDictionary Details
		{
			get;
			private set;
		}
	}
}
