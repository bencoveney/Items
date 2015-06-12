using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	
	
	/// <summary>
	/// This is a test class for ParameterTest and is intended
	/// to contain all ParameterTest Unit Tests
	/// </summary>
	[TestClass()]
	public class ParameterTest
	{
		/// <summary>
		/// A test for Parameter Constructor
		/// </summary>
		[TestMethod()]
		public void ParameterConstructorTest()
		{
			IType type = new SystemType<int>();
			Parameter parameter = new Parameter("Data Member Test", type, NullConstraints.None);
			Assert.AreEqual("Data Member Test", parameter.Name);
			Assert.AreEqual(type, parameter.DataType);
			Assert.AreEqual(NullConstraints.None, parameter.NullConstraint);
		}
	}
}
