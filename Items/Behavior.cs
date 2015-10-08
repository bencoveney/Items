namespace Items
{
	using System;
	using System.Collections.ObjectModel;
	using System.Runtime.Serialization;

	/// <summary>
	/// This defines some functionality which is either performed on/by an instance of an item, or cohesively grouped with the item
	/// </summary>
	[DataContract]
	public class Behavior
		: INamedObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Behavior" /> class.
		/// A behavior has a name which identifies it and a level (instance or static)
		/// </summary>
		/// <param name="name">The name.</param>
		public Behavior(string name)
		{
			this.Name = name;
			this.Conditions = new Collection<Condition>();
			this.Parameters = new Collection<Parameter>();
			this.Actions = new Collection<Action>();
		}

		/// <summary>
		/// Gets the name of the behavior
		/// </summary>
		[DataMember]
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the list of conditions which determine whether behavior can be performed
		/// </summary>
		[DataMember]
		public Collection<Condition> Conditions
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets what the behavior takes as input
		/// </summary>
		[DataMember]
		public Collection<Parameter> Parameters
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the changes the behavior makes to the model, for example creation of an item, or a change to an existing single item, or multiple items, or itself
		/// </summary>
		[DataMember]
		public Collection<Action> Actions
		{
			get;
			private set;
		}
	}
}
