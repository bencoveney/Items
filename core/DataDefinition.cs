﻿namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	/// <summary>
	/// Base object for any object which needs to define the data it contains
	/// </summary>
	public abstract class DataDefinition
		: INamedObject, IDataDefinition
	{
		/// <summary>
		/// The backing variable for the name
		/// </summary>
		private string name;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataDefinition"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="nullConstraint">The emptiness.</param>
		protected internal DataDefinition(string name, IType type, NullConstraints nullConstraint)
		{
			this.Name = name;
			this.DataType = type;
			this.NullConstraint = nullConstraint;
			this.Constraints = new Collection<IConstraint>();
		}

		/// <summary>
		/// Gets the name of the data stored in this attribute
		/// </summary>
		public string Name
		{
			get
			{
				return this.name;
			}

			private set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value", "value cannot be null or empty");
				}
				else
				{
					this.name = value;
				}
			}
		}

		/// <summary>
		/// Gets the type of data in this attribute
		/// Should determine the ways in which the items can be searched for by attribute
		/// </summary>
		public IType DataType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the conditions which the value of an attribute must satisfy
		/// </summary>
		public Collection<IConstraint> Constraints
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the way an attribute treats null values
		/// </summary>
		public NullConstraints NullConstraint
		{
			get;
			private set;
		}
	}
}
