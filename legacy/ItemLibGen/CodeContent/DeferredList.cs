using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen.CodeContent
{
	class DeferredList<T>
		: ICollection<T>
	{
		private Loader<T> loader;

		private List<T> internalList;

		private bool isLoaded = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="DeferredList{T}" /> class.
		/// </summary>
		/// <param name="loader">The loader.</param>
		public DeferredList(Loader<T> loader)
		{
			this.loader = loader;
		}

		/// <summary>
		/// Triggers the population of the internal list.
		/// This should be called before any manipulation is performed on the objects
		/// </summary>
		private void TriggerPopulation()
		{
			this.internalList = this.loader.Invoke();
			this.isLoaded = true;
		}

		public void Add(T item)
		{
			if (!isLoaded)
			{
				TriggerPopulation();
			}

			internalList.Add(item);
		}

		public void Clear()
		{
			// No matter what would be loaded, if we are clearing it the result would be an empty list
			if (!isLoaded)
			{
				this.internalList = new List<T>();
				this.isLoaded = true;
			}
			else
			{
				internalList.Clear();
			}
		}

		public bool Contains(T item)
		{
			if (!isLoaded)
			{
				TriggerPopulation();
			}

			return internalList.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (!isLoaded)
			{
				TriggerPopulation();
			}

			internalList.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				if (!isLoaded)
				{
					TriggerPopulation();
				}

				return internalList.Count;
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(T item)
		{
			if (!isLoaded)
			{
				TriggerPopulation();
			}

			return internalList.Remove(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (!isLoaded)
			{
				TriggerPopulation();
			}

			return internalList.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			if (!isLoaded)
			{
				TriggerPopulation();
			}

			return internalList.GetEnumerator();
		}
	}

	public delegate List<T> Loader<T>();
}
