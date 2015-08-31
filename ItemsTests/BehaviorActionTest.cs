using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for BehaviorActionTest and is intended
	///to contain all BehaviorActionTest Unit Tests
	///</summary>
	[TestClass()]
	public class BehaviorActionTest
	{
		/// <summary>
		///A test for BehaviorAction Constructor
		///</summary>
		[TestMethod()]
		public void BehaviorActionConstructorTest()
		{
			Behavior behavior = new Behavior("Test Behavior");
			BehaviorAction behaviorAction = new BehaviorAction("Test Action", behavior);
			Assert.AreEqual("Test Action", behaviorAction.Name);
			Assert.AreEqual(behavior, behaviorAction.Behavior);
		}

		/// <summary>
		///A test for Behavior
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void BehaviorTest()
		{
			Behavior behavior = new Behavior("Test Behavior");
			BehaviorAction behaviorAction = new BehaviorAction("Test Action", behavior);
			Assert.AreEqual(behavior, behaviorAction.Behavior);
		}

		/// <summary>
		///A test for Name
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void NameTest()
		{
			Behavior behavior = new Behavior("Test Behavior");
			BehaviorAction behaviorAction = new BehaviorAction("Test Action", behavior);
			Assert.AreEqual("Test Action", behaviorAction.Name);
		}
	}
}
