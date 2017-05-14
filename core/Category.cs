namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Categories are similar to items in the way they contain data
	/// Unlike items, categories do not have any functionality (not even add/edit/delete)
	/// instances of them are not created
	/// </summary>
	public class Category
		: Thing
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Category"/> class.
		/// </summary>
		/// <param name="name">The name</param>
		public Category(string name)
			: base(name)
		{
		}
	}
}
