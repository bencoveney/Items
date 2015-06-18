namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	/// <summary>
	/// Base object for any object which needs to define the data it contains
	/// </summary>
	public abstract class DataDefinition
		: INamedObject
	{
		/// <summary>
		/// The schema of implementation details which are allowed to be put into the implementation details dictionary for this class.
		/// </summary>
		private static Dictionary<string, Type> schema = new Dictionary<string, Type>();

		/// <summary>
		/// Initializes a new instance of the <see cref="DataDefinition"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="nullConstraint">The emptiness.</param>
		protected DataDefinition(string name, IType type, NullConstraints nullConstraint)
		{
			this.Name = name;
			this.DataType = type;
			this.NullConstraint = nullConstraint;
			this.Constraints = new Collection<IConstraint>();
			this.Details = new ImplementationDetailsDictionary(schema);
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
	}
}
