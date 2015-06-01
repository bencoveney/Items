namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// An action which is a script execution.
	/// </summary>
	public class ScriptAction
		: IAction
	{
		/// <summary>
		/// Gets the name of the action.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the implementation specific details.
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public ImplementationDetails Details
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name the of the action's script file.
		/// </summary>
		/// <value>
		/// The name of the script.
		/// </value>
		public string ScriptName
		{
			get;
			private set;
		}
	}
}
