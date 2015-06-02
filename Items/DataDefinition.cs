namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Base object for any object which needs to define the data it contains
	/// </summary>
	public abstract class DataDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataDefinition"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="nullability">The emptiness.</param>
		public DataDefinition(string name, IType type, Nullability nullability)
		{
			this.Name = name;
			this.Type = type;
			this.Nullability = nullability;
			this.Constraints = new List<IConstraint>();
			this.Details = new ImplementationDetails();
		}

		/// <summary>
		/// Gets the name of the data stored in this attribute
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the type of data in this attribute
		/// Should determine the ways in which the items can be searched for by attribute
		/// </summary>
		public IType Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the conditions which the value of an attribute must satisfy
		/// </summary>
		public List<IConstraint> Constraints
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the way an attribute treats null values
		/// </summary>
		public Nullability Nullability
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the implementation specific details attached to the type
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public ImplementationDetails Details
		{
			get;
			private set;
		}
	}
}
