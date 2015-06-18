using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for ImplementationDetailsDictionaryTest and is intended
	///to contain all ImplementationDetailsDictionaryTest Unit Tests
	///</summary>
	[TestClass()]
	public class ImplementationDetailsDictionaryTest
	{
		/// <summary>
		/// Gets the dummy schema for testing.
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, Type> GetSchemaForTesting()
		{
			Dictionary<string, Type> schema = new Dictionary<string, Type>();
			schema.Add("Test String", typeof(string));
			schema.Add("Test Int", typeof(int));
			schema.Add("Test Bool", typeof(bool));
			schema.Add("Test Object", typeof(object));
			return schema;
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Constructor
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryConstructorTest()
		{
			Dictionary<string, Type> schema = GetSchemaForTesting();
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(schema);
			Assert.AreEqual(schema, target.Schema);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ImplementationDetailsDictionaryConstructorTestNullSchema()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(null);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Schema
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionarySchemaTest()
		{
			Dictionary<string, Type> schema = GetSchemaForTesting();
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(schema);
			Assert.AreEqual(schema, target.Schema);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Keys
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryKeysTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			Assert.IsFalse(target.Keys.Contains("Test String"));

			target.Add("Test String", "Hello");
			Assert.IsTrue(target.Keys.Contains("Test String"));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Values
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryValuesTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			Assert.IsFalse(target.Values.Contains("Hello"));

			target.Add("Test String", "Hello");
			Assert.IsTrue(target.Values.Contains("Hello"));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Count
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryCountTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			Assert.AreEqual(0, target.Count);

			target.Add("Test String", "Hello");
			Assert.AreEqual(1, target.Count);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary IsReadOnly
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryIsReadOnlyTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			Assert.AreEqual(false, target.IsReadOnly);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Indexer
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryIndexerTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test String", "Hello");
			Assert.AreEqual("Hello", target["Test String"]);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Indexer when NotFound
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void ImplementationDetailsDictionaryIndexerNotFoundTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			object output = target["Test String"];
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Indexer Setter
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryIndexerTestSetter()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target["Test String"] = "Hello";
			Assert.AreEqual("Hello", target["Test String"]);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Add
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryAddTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test String", "Hello");
			Assert.AreEqual("Hello", target["Test String"].ToString());
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Add when OutsideSchema
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void ImplementationDetailsDictionaryAddTestOutsideSchema()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test IEnumerable", "Hello");
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Add when IncorrectType
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void ImplementationDetailsDictionaryAddTestIncorrectType()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test String", 5);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary ContainsKey
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryContainsKeyTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test String", "Hello");
			Assert.IsTrue(target.ContainsKey("Test String"));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Remove
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryRemoveTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test String", "Hello");
			Assert.IsTrue(target.ContainsValue("Hello"));
			target.Remove("Test String");
			Assert.IsFalse(target.ContainsValue("Hello"));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary TryGetValue
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryTryGetValueTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			object output = "Dummy Text";
			Assert.IsFalse(target.TryGetValue("Test String", out output));
			Assert.AreNotEqual("Hello", output);

			target.Add("Test String", "Hello");
			Assert.IsTrue(target.TryGetValue("Test String", out output));
			Assert.AreEqual("Hello", output);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Clear
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryClearTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test String", "Hello");
			target.Add("Test Int", 5);
			Assert.AreEqual(2, target.Count);
			target.Clear();
			Assert.AreEqual(0, target.Count);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Contains
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryContainsTest()
		{
			object dummy = new object();
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			Assert.IsFalse(target.Contains(new KeyValuePair<string, object>("Test Object", dummy)));

			target.Add("Test Object", dummy);
			Assert.IsTrue(target.Contains(new KeyValuePair<string, object>("Test Object", dummy)));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary ContainsValue
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryContainsValueTest()
		{
			object dummy = new object();
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			Assert.IsFalse(target.ContainsValue(dummy));

			target.Add("Test Object", dummy);
			Assert.IsTrue(target.ContainsValue(dummy));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary CopyTo
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryCopyToTest()
		{
			object dummyObject = new object();
			string dummyString = "Dummy String Test";
			int dummyInt = new int();
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add("Test Object", dummyObject);
			target.Add("Test String", dummyString);
			target.Add("Test Int", dummyInt);

			KeyValuePair<string, object>[] output = new KeyValuePair<string, object>[3];
			target.CopyTo(output, 0);

			foreach (KeyValuePair<string, object> result in output)
			{
				if (result.Key == "Test Object")
				{
					Assert.AreEqual(dummyObject, result.Value);
				}
				else if (result.Key == "Test String")
				{
					Assert.AreEqual(dummyString, result.Value);
				}
				else if (result.Key == "Test Int")
				{
					Assert.AreEqual(dummyInt, result.Value);
				}
				else
				{
					Assert.Fail();
				}
			}
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary Remove
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryRemoveKVPTest()
		{
			KeyValuePair<string, object> dummy = new KeyValuePair<string, object>("Test String", "Hello");
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			target.Add(dummy);
			Assert.IsTrue(target.Contains(dummy));
			Assert.IsTrue(target.Remove(dummy));
			Assert.IsFalse(target.Contains(dummy));
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary GetEnumerator Generic
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryGetEnumeratorGenericTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			IEnumerator<KeyValuePair<string, object>> result = target.GetEnumerator();
			Assert.IsNotNull(result);
		}

		/// <summary>
		/// A test for ImplementationDetailsDictionary GetEnumerator
		/// </summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryGetEnumeratorTest()
		{
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(GetSchemaForTesting());
			System.Collections.IEnumerator result = ((IEnumerable)target).GetEnumerator();
			Assert.IsNotNull(result);
		}
	}
}
