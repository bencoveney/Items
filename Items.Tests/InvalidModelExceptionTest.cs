using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for InvalidModelExceptionTest and is intended
	///to contain all InvalidModelExceptionTest Unit Tests
	///</summary>
	[TestClass()]
	public class InvalidModelExceptionTest
	{
		/// <summary>
		/// A test for InvalidModelException Constructor
		/// </summary>
		[TestMethod()]
		public void InvalidModelExceptionConstructorTest()
		{
			InvalidModelException target = new InvalidModelException();
		}

		/// <summary>
		/// A test for InvalidModelException Constructor
		/// </summary>
		[TestMethod()]
		public void InvalidModelExceptionConstructorTest1()
		{
			InvalidModelException target = new InvalidModelException("Message Test");
			Assert.AreEqual("Message Test", target.Message);
		}

		/// <summary>
		/// A test for InvalidModelException Constructor
		/// </summary>
		[TestMethod()]
		public void InvalidModelExceptionConstructorTest3()
		{
			Exception exception = new ArgumentException("Test Argument");
			InvalidModelException target = new InvalidModelException("Test Message", exception);
			Assert.AreEqual(exception, target.InnerException);
			Assert.AreEqual("Test Message", target.Message);
		}
	}
}
