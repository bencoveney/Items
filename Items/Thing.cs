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
		: INamedObject
	{
		/// <summary>
		/// The schema of implementation details which are allowed to be put into the implementation details dictionary for this class.
		/// </summary>
		private static Dictionary<string, Type> schema = new Dictionary<string, Type>();

		/// <summary>
		/// The backing variable for the string identifier
		/// </summary>
		private DataMember stringIdentifier;

		/// <summary>
		/// The backing variable for the integer identifier
		/// </summary>
		private DataMember integerIdentifier;

		/// <summary>
		/// The backing variable for the name
		/// </summary>
		private string name;

		/// <summary>
		/// Initializes a new instance of the <see cref="Thing" /> class.
		/// Creates the item
		/// </summary>
		/// <param name="name">The name.</param>
		protected Thing(string name)
		{
			this.Name = name;
			this.Attributes = new NamedCollection<DataMember>();
			this.Details = new ImplementationDetailsDictionary(schema);
		}

		/// <summary>
		/// Gets the name of the item
		/// ReadOnly?
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
		public NamedCollection<DataMember> Attributes
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

		/// <summary>
		/// Adds an entry to the schema which limits what can be inserted into the implementation details dictionary.
		/// If the item already exists no error will be emitted.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <exception cref="ArgumentNullException">
		/// key;key cannot be null or empty
		/// or
		/// value;value cannot be null
		/// </exception>
		/// <exception cref="ArgumentException">Schema already contains a different type for this key;value</exception>
		public static void AddDetailsSchemaEntry(string key, Type value)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key", "key cannot be null or empty");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value", "value cannot be null");
			}

			// The key might already exist
			if (schema.ContainsKey(key))
			{
				// Check for clashes
				if (schema[key] != value)
				{
					throw new ArgumentException("Schema already contains a different type for this key", "value");
				}
				else
				{
					// If it has already been added this is fine
					return;
				}
			}

			// Add it to the schema
			schema.Add(key, value);
		}

		/// <summary>
		/// Checks that the requested schema entry exists.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="type">The type.</param>
		public static void RequireSchemaEntry(string key, Type value)
		{
			if (!schema.ContainsKey(key))
			{
				throw new InvalidModelException("Implementation details schema does not contain required key");
			}

			if (schema[key] != value)
			{
				throw new InvalidModelException("Implementation details schema's value does not match the type value given");
			}
		}

		/// <summary>
		/// Gets the relationships which reference this thing.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns>A collection of relationships which reference this thing.</returns>
		/// <exception cref="ArgumentNullException">model;model can not be null</exception>
		public IEnumerable<Relationship> GetReferenceRelationships(Model model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model", "model can not be null");
			}

			return model.Relationships.Where(relationship => relationship.Links.Any(link => link.Thing == this));
		}
	}
}
