using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	/// This is a test class for DataMemberTest and is intended
	/// to contain all DataMemberTest Unit Tests
	/// </summary>
	[TestClass()]
	public class DataMemberTest
	{
		/// <summary>
		/// A test for DataMember Constructor
		/// </summary>
		[TestMethod()]
		public void DataMemberConstructorTest()
		{
			IType type = new SystemType<int>();
			DataMember dataMember = new DataMember("Data Member Test", type, NullConstraints.None);
			Assert.AreEqual("Data Member Test", dataMember.Name);
			Assert.AreEqual(type, dataMember.DataType);
			Assert.AreEqual(NullConstraints.None, dataMember.NullConstraint);
		}
	}
}
