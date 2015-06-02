namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// This defines some functionality which is either performed on/by an instance of an item, or cohesively grouped with the item
	/// </summary>
	public class Behavior
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Behavior" /> class.
		/// A behavior has a name which identifies it and a level (instance or static)
		/// </summary>
		/// <param name="name">The name.</param>
		public Behavior(string name)
		{
			this.Name = name;
			this.Conditions = new List<Condition>();
			this.Parameters = new List<Parameter>();
			this.Actions = new List<Action>();
		}

		/// <summary>
		/// Gets the name of the behavior
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the list of conditions which determine whether behavior can be performed
		/// </summary>
		public List<Condition> Conditions
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets what the behavior takes as input
		/// </summary>
		public List<Parameter> Parameters
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the changes the behavior makes to the model, for example creation of an item, or a change to an existing single item, or multiple items, or itself
		/// </summary>
		public List<Action> Actions
		{
			get;
			private set;
		}
	}
}
