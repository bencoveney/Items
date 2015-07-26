namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

	/// <summary>
	/// An action which is a behavior execution.
	/// </summary>
    [DataContract]
	public class BehaviorAction
		: IAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BehaviorAction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="behavior">The behavior.</param>
		public BehaviorAction(string name, Behavior behavior)
		{
			this.Name = name;
			this.Behavior = behavior;
			this.Details = new ImplementationDetailsDictionary();
		}

		/// <summary>
		/// Gets the name of the action.
		/// </summary>
		/// <value>
		/// The name.
        /// </value>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
		public Behavior Behavior
		{
			get;
			private set;
		}
	}
}
