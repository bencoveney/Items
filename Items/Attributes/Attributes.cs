namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A named collection of attributes.
    /// </summary>
    public class Attributes : IDictionary<string, IAttribute>
    {
        /// <summary>
        /// The internal attributes
        /// </summary>
        private List<IAttribute> internalAttributes = new List<IAttribute>();

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<IAttribute> Values
        {
            get { return this.internalAttributes; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<string> Keys
        {
            get
            {
                return this.internalAttributes.Select<IAttribute, string>(attribute => attribute.Name).ToList();
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count
        {
            get { return this.internalAttributes.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The attribute</returns>
        public IAttribute this[string key]
        {
            get
            {
                return this.internalAttributes.Single(attribute => attribute.Name == key);
            }

            set
            {
                this.Add(key, value);
            }
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(IAttribute value)
        {
            this.Add(value.Name, value);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="System.Exception">
        /// Attribute key is it's name
        /// or
        /// Attribute with that name already exists
        /// </exception>
        public void Add(string key, IAttribute value)
        {
            if (value.Name != key)
            {
                throw new Exception("Attribute key is it's name");
            }

            if (this.internalAttributes.Any(attribute => attribute.Name == key))
            {
                throw new Exception("Attribute with that name already exists");
            }

            this.internalAttributes.Add(value);
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
            return this.internalAttributes.Any(attribute => attribute.Name == key);
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
            return this.internalAttributes.RemoveAll(attribute => attribute.Name == key) > 0;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(string key, out IAttribute value)
        {
            if (this.ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(KeyValuePair<string, IAttribute> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            this.internalAttributes = new List<IAttribute>();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<string, IAttribute> item)
        {
            IAttribute result;
            this.TryGetValue(item.Key, out result);
            return result == item.Value;
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        /// <exception cref="System.IndexOutOfRangeException">Index out of range</exception>
        public void CopyTo(KeyValuePair<string, IAttribute>[] array, int arrayIndex)
        {
            if (this.internalAttributes.Count + arrayIndex > array.Length)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            for (int i = 0; i < array.Length - arrayIndex; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<string, IAttribute>(this.internalAttributes[i].Name, this.internalAttributes[i]);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(KeyValuePair<string, IAttribute> item)
        {
            // doesnt check whether the item only matches on one, could be bad data
            return this.internalAttributes.RemoveAll(attribute => attribute.Name == item.Key && attribute == item.Value) > 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, IAttribute>> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
