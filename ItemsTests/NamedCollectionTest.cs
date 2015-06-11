using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ItemsTests
{
	/// <summary>
	/// This is a test class for NamedCollection and is intended
	/// to contain all NamedCollection Unit Tests
	/// 
	/// TODO: Test indexing a missing value
	/// TODO: Test adding a duplicate item
	/// TODO: Test adding items of different types
	/// TODO: Test adding duplicate items pf different types
	/// TODO: Test GetEnumerator
	/// </summary>
	[TestClass()]
	public class NamedCollectionTest
	{
		/// <summary>
		/// A test for NamedCollection Constructor
		/// </summary>
		[TestMethod()]
		public void NamedCollectionConstructorTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();
		}

		/// <summary>
		/// A test for NamedCollection Count
		/// </summary>
		[TestMethod()]
		public void CountTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();

			Assert.AreEqual(0, namedCollection.Count);

			Item item1 = new Item("Test 1");
			Item item2 = new Item("Test 2");

			namedCollection.Add(item1);
			namedCollection.Add(item2);

			Assert.AreEqual(2, namedCollection.Count);
		}

		/// <summary>
		/// A test for NamedCollection IsReadOnly
		/// </summary>
		[TestMethod()]
		public void IsReadOnlyTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();

			// TODO confirm that this should always be false? maybe the property should be changed?
			Assert.IsFalse(namedCollection.IsReadOnly);
		}

		/// <summary>
		/// A test for NamedCollection StringIndexer
		/// </summary>
		[TestMethod()]
		public void StringIndexerFoundTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();

			Item item = new Item("Test Item");
			namedCollection.Add(item);

			Assert.AreEqual(item, namedCollection["Test Item"]);
		}

		/// <summary>
		/// A test for NamedCollection Add
		/// </summary>
		[TestMethod()]
		public void AddTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();
			Item item = new Item("Test");
			namedCollection.Add(item);
			Assert.IsTrue(namedCollection.Contains(item));
		}

		/// <summary>
		/// A test for NamedCollection Count
		/// </summary>
		[TestMethod()]
		public void ClearTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();

			namedCollection.Add(new Item("Test 1"));
			namedCollection.Add(new Item("Test 2"));
			namedCollection.Add(new Item("Test 3"));
			Assert.AreEqual(3, namedCollection.Count);

			namedCollection.Clear();
			Assert.AreEqual(0, namedCollection.Count);
		}

		/// <summary>
		/// A test for NamedCollection Contains
		/// </summary>
		[TestMethod()]
		public void ContainsTestFound()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();
			Item found = new Item("Found Test");
			namedCollection.Add(found);
			Assert.IsTrue(namedCollection.Contains(found));
		}

		/// <summary>
		/// A test for NamedCollection Contains
		/// </summary>
		[TestMethod()]
		public void ContainsTestMissing()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();
			Item missing = new Item("Missing Test");
			Assert.IsFalse(namedCollection.Contains(missing));
		}

		/// <summary>
		/// A test for NamedCollection CopyTo
		/// </summary>
		[TestMethod()]
		public void CopyToTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();

			Item item1 = new Item("Test 1");
			Item item2 = new Item("Test 2");
			namedCollection.Add(item1);
			namedCollection.Add(item2);

			INamedObject[] namedObjectArray = new INamedObject[2];
			namedCollection.CopyTo(namedObjectArray, 0);

			Assert.AreEqual(item1, namedObjectArray[0]);
			Assert.AreEqual(item2, namedObjectArray[2]);
		}

		/// <summary>
		/// A test for NamedCollection Remove
		/// </summary>
		[TestMethod()]
		public void RemoveTest()
		{
			NamedCollection<INamedObject> namedCollection = new NamedCollection<INamedObject>();

			Item item1 = new Item("Test 1");
			Item item2 = new Item("Test 2");
			Item item3 = new Item("Test 3");

			namedCollection.Add(item1);
			namedCollection.Add(item2);
			namedCollection.Add(item3);

			Assert.AreEqual(3, namedCollection.Count);
			Assert.IsTrue(namedCollection.Contains(item2));

			namedCollection.Remove(item2);

			Assert.AreEqual(2, namedCollection.Count);
			Assert.IsFalse(namedCollection.Contains(item2));
		}
	}
}
