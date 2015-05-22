﻿namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Defines an attribute's type
	/// </summary>
	public partial interface IType
	{
		/// <summary>
		/// Gets a textual representation of the type
		/// </summary>
		string Name
		{
			get;
		}

		/// <summary>
		/// Gets the implementation specific details attached to the type
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		ImplementationDetails Details
		{
			get;
		}
	}

	// Should IType have a name? would it be better just to have "Item" for item and remove name

	// TODO Enum Types
	// Maybe named categories?
}
