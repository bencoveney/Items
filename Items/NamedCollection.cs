namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;

	/// <summary>
	/// Specific class for behavior collections which guarantees unique names
	/// 
	/// TODO: upate method documentation to match ICollection
	/// </summary>
	/// <typeparam name="T">The type of the named elements in the collection.</typeparam>
	public class NamedCollection<T>
		: ICollection<T> where T : INamedObject
	{
		/// <summary>
		/// The internal collection
		/// </summary>
		private ICollection<T> internalCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="NamedCollection{T}"/> class.
		/// </summary>
		public NamedCollection()
		{
			this.internalCollection = new Collection<T>();
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public int Count
		{
			get
			{
				return this.internalCollection.Count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return this.internalCollection.IsReadOnly;
			}
		}

		/// <summary>
		/// Gets the <see cref="T"/> with the specified name.
		/// </summary>
		/// <value>
		/// The <see cref="T"/>.
		/// </value>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="T"/> with the specified name.</returns>
		public T this[string name]
		{
			get
			{
				return this.internalCollection.Single(namedObject => namedObject.Name == name);
			}
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <exception cref="ArgumentNullException">item;item cannot be null</exception>
		/// <exception cref="ArgumentException">A behavior with that name already exists;item</exception>
		public void Add(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "item cannot be null");
			}

			if (this.internalCollection.Any(existingItem => existingItem.Name == item.Name))
			{
				throw new ArgumentException("A behavior with that name already exists", "item");
			}

			this.internalCollection.Add(item);
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public void Clear()
		{
			this.internalCollection.Clear();
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
		/// </returns>
		public bool Contains(T item)
		{
			return this.internalCollection.Contains(item);
		}

		/// <summary>
		/// Copies the elements of the NamedCollection<T> to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from NamedCollection<T>. The Array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.internalCollection.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </returns>
		public bool Remove(T item)
		{
			return this.internalCollection.Remove(item);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			return this.internalCollection.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.internalCollection.GetEnumerator();
		}
	}
}
