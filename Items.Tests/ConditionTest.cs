using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for ConditionTest and is intended
	///to contain all ConditionTest Unit Tests
	///</summary>
	[TestClass()]
	public class ConditionTest
	{
		/// <summary>
		///A test for Condition Constructor
		///</summary>
		[TestMethod()]
		public void ConditionConstructorTest()
		{
			Condition condition = new Condition("Test Condition");
			Assert.AreEqual("Test Condition", condition.Name);
			Assert.IsNotNull(condition.Inputs);
		}

		/// <summary>
		///A test for Inputs
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void InputsTest()
		{
			Condition condition = new Condition("Test Condition");
			Assert.IsNotNull(condition.Inputs);
			object obj = new object();
			condition.Inputs.Add(obj);
			Assert.IsTrue(condition.Inputs.Contains(obj));
		}

		/// <summary>
		///A test for Name
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void NameTest()
		{
			Condition condition = new Condition("Test Condition");
			Assert.AreEqual("Test Condition", condition.Name);
		}
	}
}
