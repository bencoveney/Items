namespace Items
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// A key, value collection of implementation detail names and their values
	/// </summary>
	[Serializable]
	public class ImplementationDetailsDictionary
		: IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>
	{
		/// <summary>
		/// The schema defining: for each type, what you can put in it's implementation details dictionary
		/// This is expressed as a collection of detail names and the types of data they contain
		/// </summary>
		private static Dictionary<Type, Dictionary<string, Type>> schema = new Dictionary<Type, Dictionary<string, Type>>();

		/// <summary>
		/// The internal dictionary of implementation details.
		/// Additions to this collection should only be made through the class' Add() method as that checks against the Schema.
		/// </summary>
		private Dictionary<string, object> internalDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationDetailsDictionary" /> class.
        /// </summary>
		public ImplementationDetailsDictionary()
			: base()
		{
			this.internalDictionary = new Dictionary<string, object>();
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
		/// </summary>
		public ICollection<string> Keys
		{
			get
			{
				return this.internalDictionary.Keys;
			}
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
		/// </summary>
		public ICollection<object> Values
		{
			get
			{
				return this.internalDictionary.Values;
			}
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public int Count
		{
			get
			{
				return this.internalDictionary.Count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, object>>)this.internalDictionary).IsReadOnly;
			}
		}

		/// <summary>
		/// Gets or sets the element with the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>The element with the specified key.</returns>
		public object this[string key]
		{
			get
			{
				return this.internalDictionary[key];
			}

			set
			{
				this.Add(key, value);
			}
		}

		/// <summary>
		/// Adds an entry to the schema which limits what can be inserted into the implementation details dictionary.
		/// If the item already exists no error will be emitted.
		/// </summary>
		/// <param name="type">The type the schema entry is being added for.</param>
		/// <param name="key">The key allowed in this type's .</param>
		/// <param name="value">The type for the key's corresponding value.</param>
		/// <exception cref="ArgumentNullException">key;key cannot be null or empty
		/// or
		/// value;value cannot be null</exception>
		/// <exception cref="ArgumentException">Schema already contains a different type for this key;value</exception>
		public static void AddSchemaEntry(Type type, string key, Type value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", "type cannot be null");
			}

			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key", "key cannot be null or empty");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value", "value cannot be null");
			}

			// The key might already exist
			if (schema.ContainsKey(type) && schema[type].ContainsKey(key))
			{
				// Check for clashes
				if (schema[type][key] != value)
				{
					throw new ArgumentException("Schema already contains a different type for this key", "value");
				}
				else
				{
					// If it has already been added this is fine
					return;
				}
			}

			// make sure the type exists in the schema
			if (!schema.ContainsKey(type))
			{
				schema.Add(type, new Dictionary<string, Type>());
			}

			// Add it to the schema
			schema[type].Add(key, value);
		}

		/// <summary>
		/// Checks that the requested schema entry exists.
		/// </summary>
		/// <param name="type">The type the schema entry is being added for.</param>
		/// <param name="key">The key allowed in this type's .</param>
		/// <param name="value">The type for the key's corresponding value.</param>
		/// <exception cref="ArgumentNullException">
		/// type;type cannot be null
		/// or
		/// key;key cannot be null or empty
		/// or
		/// value;value cannot be null
		/// </exception>
		/// <exception cref="InvalidModelException">Implementation details schema does not contain required key
		/// or
		/// Implementation details schema's value does not match the type value given</exception>
		public static void RequireSchemaEntry(Type type, string key, Type value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", "type cannot be null");
			}

			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key", "key cannot be null or empty");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value", "value cannot be null");
			}

			if (!schema.ContainsKey(type))
			{
				throw new InvalidModelException("No implementation details schema entries have been defined for this type");
			}

			if (!schema[type].ContainsKey(key))
			{
				throw new InvalidModelException("Implementation details schema does not contain required key");
			}

			if (schema[type][key] != value)
			{
				throw new InvalidModelException("Implementation details schema's value does not match the type value given");
			}
		}

		/// <summary>
		/// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
		/// </summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		/// <exception cref="ArgumentNullException">
		/// key;key cannot be null or empty
		/// or
		/// value;value cannot be null
		/// </exception>
		/// <exception cref="KeyNotFoundException">The given key does not exist in this dictionary's schema</exception>
		/// <exception cref="ArgumentException">value does not have the correct type;value</exception>
		public void Add(string key, object value)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key", "key cannot be null or empty");
			}

			this.internalDictionary.Add(key, value);
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
		/// <returns>
		/// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
		/// </returns>
		public bool ContainsKey(string key)
		{
			return this.internalDictionary.ContainsKey(key);
		}

		/// <summary>
		/// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
		/// </summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns>
		/// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
		/// </returns>
		public bool Remove(string key)
		{
			return this.internalDictionary.Remove(key);
		}

		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		/// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
		/// </returns>
		public bool TryGetValue(string key, out object value)
		{
			return this.internalDictionary.TryGetValue(key, out value);
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		public void Add(KeyValuePair<string, object> item)
		{
			// TODO Are we breaking the IEnumerable interface by not doing preserving the reference here?
			this.internalDictionary.Add(item.Key, item.Value);
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public void Clear()
		{
			this.internalDictionary.Clear();
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
		/// </returns>
		public bool Contains(KeyValuePair<string, object> item)
		{
			object obj;
			this.internalDictionary.TryGetValue(item.Key, out obj);
			return obj == item.Value;
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified value.
		/// </summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
		/// <returns>
		/// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the value; otherwise, false.
		/// </returns>
		public bool ContainsValue(object value)
		{
			return this.internalDictionary.ContainsValue(value);
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, object>>)this.internalDictionary).CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </returns>
		public bool Remove(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this.internalDictionary).Remove(item);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((ICollection<KeyValuePair<string, object>>)this.internalDictionary).GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((ICollection<KeyValuePair<string, object>>)this.internalDictionary).GetEnumerator();
		}
	}
}
