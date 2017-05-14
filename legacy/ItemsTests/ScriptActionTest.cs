using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for ScriptActionTest and is intended
	///to contain all ScriptActionTest Unit Tests
	///</summary>
	[TestClass()]
	public class ScriptActionTest
	{
		/// <summary>
		///A test for ScriptAction Constructor
		///</summary>
		[TestMethod()]
		public void ScriptActionConstructorTest()
		{
			ScriptAction scriptAction = new ScriptAction("Test Script", "scriptName.cs");
			Assert.AreEqual("Test Script", scriptAction.Name);
			Assert.AreEqual("scriptName.cs", scriptAction.ScriptName);
		}

		/// <summary>
		///A test for Name
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void NameTest()
		{
			ScriptAction scriptAction = new ScriptAction("Test Script", "scriptName.cs");
			Assert.AreEqual("Test Script", scriptAction.Name);
		}

		/// <summary>
		///A test for ScriptName
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ScriptNameTest()
		{
			ScriptAction scriptAction = new ScriptAction("Test Script", "scriptName.cs");
			Assert.AreEqual("scriptName.cs", scriptAction.ScriptName);
		}
	}
}
