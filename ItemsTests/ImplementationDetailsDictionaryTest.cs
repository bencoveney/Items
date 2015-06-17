using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

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
		///A test for ImplementationDetailsDictionary Constructor
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ImplementationDetailsDictionaryConstructorTestSerialization()
		{
			SerializationInfo info = new SerializationInfo(typeof(ImplementationDetailsDictionary), new FormatterConverter());
			StreamingContext context = new StreamingContext();

			ImplementationDetailsDictionary_Accessor accessor = new ImplementationDetailsDictionary_Accessor(info, context);

			// TODO what am I supposed to do with this to test it?
		}

		/// <summary>
		///A test for ImplementationDetailsDictionary Constructor
		///</summary>
		[TestMethod()]
		public void ImplementationDetailsDictionaryConstructorTest()
		{
			object obj = new object();
			ImplementationDetailsDictionary target = new ImplementationDetailsDictionary(new Dictionary<string, Type>());
			Assert.IsFalse(target.ContainsKey("Hello"));
			Assert.IsFalse(target.ContainsValue(obj));
			target["Hello"] = obj;
			Assert.AreEqual(obj, target["Hello"]);
		}
	}
}
