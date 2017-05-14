using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for StoredProcedureActionTest and is intended
	///to contain all StoredProcedureActionTest Unit Tests
	///</summary>
	[TestClass()]
	public class StoredProcedureActionTest
	{
		/// <summary>
		///A test for StoredProcedureAction Constructor
		///</summary>
		[TestMethod()]
		public void StoredProcedureActionConstructorTest()
		{
			StoredProcedureAction storedProcedureAction = new StoredProcedureAction("Test Procedure", "MyProc");
			Assert.AreEqual("Test Procedure", storedProcedureAction.Name);
			Assert.AreEqual("MyProc", storedProcedureAction.StoredProcedureName);
		}

		/// <summary>
		///A test for Name
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void NameTest()
		{
			StoredProcedureAction storedProcedureAction = new StoredProcedureAction("Test Procedure", "MyProc");
			Assert.AreEqual("Test Procedure", storedProcedureAction.Name);
		}

		/// <summary>
		///A test for StoredProcedureName
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void StoredProcedureNameTest()
		{
			StoredProcedureAction storedProcedureAction = new StoredProcedureAction("Test Procedure", "MyProc");
			Assert.AreEqual("MyProc", storedProcedureAction.StoredProcedureName);
		}
	}
}
