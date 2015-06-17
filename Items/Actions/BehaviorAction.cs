namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// An action which is a behavior execution.
	/// </summary>
	public class BehaviorAction
		: IAction
	{
		/// <summary>
		/// The schema of implementation details which are allowed to be put into the implementation details dictionary for this class.
		/// </summary>
		private static Dictionary<string, Type> schema = new Dictionary<string, Type>();

		/// <summary>
		/// Initializes a new instance of the <see cref="BehaviorAction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="behavior">The behavior.</param>
		public BehaviorAction(string name, Behavior behavior)
		{
			this.Name = name;
			this.Behavior = behavior;
			this.Details = new ImplementationDetailsDictionary(schema);
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
		/// Gets the action's behavior.
		/// </summary>
		/// <value>
		/// The behavior.
		/// </value>
		public Behavior Behavior
		{
			get;
			private set;
		}
	}
}
