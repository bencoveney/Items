namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// A basic "thing". A container of data
	/// </summary>
	public abstract class Thing
	{
		/// <summary>
		/// The string identifier
		/// </summary>
		private DataMember stringIdentifier;

		/// <summary>
		/// The integer identifier
		/// </summary>
		private DataMember integerIdentifier;

		/// <summary>
		/// Initializes a new instance of the <see cref="Thing" /> class.
		/// Creates the item
		/// </summary>
		/// <param name="name">The name.</param>
		protected Thing(string name)
		{
			this.Name = name;
			this.Attributes = new AttributeDictionary();
			this.Details = new ImplementationDetailsDictionary();
		}

		/// <summary>
		/// Gets or sets the name of the item
		/// ReadOnly?
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a special case of unique attribute which can be used to identify the item
		/// Should be in the list of attribute
		/// should we require an identifier in order to perform lookups?
		/// </summary>
		public DataMember StringIdentifier
		{
			get
			{
				return this.stringIdentifier;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "value cannot be null");
				}

				if (!(value.DataType is SystemType<string>))
				{
					throw new ArgumentException("stringIdentifier must be of type SystemType<string>");
				}

				this.stringIdentifier = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a special case of unique attribute which can be used to identify the item
		/// Should be in the list of attribute
		/// should we require an identifier in order to perform lookups?
		/// </summary>
		public DataMember IntegerIdentifier
		{
			get
			{
				return this.integerIdentifier;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Value cannot be null");
				}

				// TODO accept other size ints?
				if (!(value.DataType is SystemType<int>))
				{
					throw new ArgumentException("IntegerIdentifer must be of type SystemType<Int32>");
				}

				// Ensure the value is marked as unique
				if (!value.Constraints.OfType<AttributeConstraint>().Any(constraint => constraint.Attribute == value && constraint.Comparison == CollectionComparison.IsUniqueWithin))
				{
					value.Constraints.Add(new AttributeConstraint(value, CollectionComparison.IsUniqueWithin));
				}

				// Ensure the value cannot be nulled
				if (value.NullConstraint != NullConstraints.None)
				{
					throw new ArgumentException("This attribute cannot be an identifier as it can be null");
				}

				this.integerIdentifier = value;
			}
		}

		/// <summary>
		/// Gets the data for the instance of the item
		/// Should be dictionary, with the identifier being a key on the dictionary
		/// </summary>
		public AttributeDictionary Attributes
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
		public ImplementationDetailsDictionary Details
		{
			get;
			private set;
		}
	}
}
