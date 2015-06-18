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
			Assert.IsNotNull(scriptAction.Details);
		}

		/// <summary>
		///A test for Details
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void DetailsTest()
		{
			ScriptAction.AddDetailsSchemaEntry("Test Key", typeof(string));
			ScriptAction scriptAction = new ScriptAction("Test Script", "scriptName.cs");
			Assert.IsNotNull(scriptAction.Details);

			scriptAction.Details["Test Key"] = "Test Value";
			Assert.AreEqual("Test Value", scriptAction.Details["Test Key"]);
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
