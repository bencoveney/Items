﻿namespace Items
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
		/// The schema of implementation details which are allowed to be put into the implementation details dictionary for this class.
		/// </summary>
		private static Dictionary<string, Type> schema = new Dictionary<string, Type>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ScriptAction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="scriptName">Name of the script.</param>
		public ScriptAction(string name, string scriptName)
		{
			this.Name = name;
			this.ScriptName = scriptName;
			this.Details = new ImplementationDetailsDictionary();
		}

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
		public ImplementationDetailsDictionary Details
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
