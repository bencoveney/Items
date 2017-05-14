using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
		public void InvalidModelExceptionConstructorTest2()
		{
			InvalidModelException target = new InvalidModelException("Serialization Test");

			// Serialize it to memory
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(memoryStream, target);

			// Deserialize it back out
			memoryStream.Flush();
			memoryStream.Seek(0, SeekOrigin.Begin);
			InvalidModelException output = (InvalidModelException)formatter.Deserialize(memoryStream);

			Assert.AreEqual("Serialization Test", output.Message);
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
