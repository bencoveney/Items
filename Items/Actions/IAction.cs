namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// perform actions on the given entities
	/// manipulate the given parameters
	/// make the following changes to the model
	/// </summary>
	public interface IAction
	{
		/// <summary>
		/// Gets the name of the action.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; }

		/// <summary>
		/// Gets the implementation specific details.
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		ImplementationDetails Details { get; }
	}
}
