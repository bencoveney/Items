namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Defines an object with a name
	/// </summary>
	public interface INamedObject
	{
		/// <summary>
		/// Gets the name of this object.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name
		{
			get;
		}
	}
}
