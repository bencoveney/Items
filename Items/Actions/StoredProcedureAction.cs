namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// An action which is a stored procedure execution.
	/// </summary>
	public class StoredProcedureAction
		: IAction
	{
		/// <summary>
		/// The schema of implementation details which are allowed to be put into the implementation details dictionary for this class.
		/// </summary>
		private static Dictionary<string, Type> schema = new Dictionary<string, Type>();

		/// <summary>
		/// Initializes a new instance of the <see cref="StoredProcedureAction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		public StoredProcedureAction(string name, string storedProcedureName)
		{
			this.Name = name;
			this.StoredProcedureName = storedProcedureName;
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
		/// Gets the name of the action's stored procedure.
		/// </summary>
		/// <value>
		/// The name of the stored procedure.
		/// </value>
		public string StoredProcedureName
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
	}
}
