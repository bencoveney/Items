﻿using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for SystemTypeTest and is intended
	///to contain all SystemTypeTest Unit Tests
	///</summary>
	[TestClass()]
	public class SystemTypeTest
	{
		/// <summary>
		/// A test for SystemType Constructor
		/// </summary>
		[TestMethod()]
		public void SystemTypeConstructorTest()
		{
			SystemType<Int32> target = new SystemType<Int32>();
		}

		/// <summary>
		/// A test for DataType
		/// </summary>
		[TestMethod()]
		public void DataTypeTest()
		{
			SystemType<Int32> target = new SystemType<Int32>(); // TODO: Initialize to an appropriate value
			Assert.AreEqual(typeof(Int32), target.DataType);
		}

		/// <summary>
		/// A test for Name
		/// </summary>
		[TestMethod()]
		public void NameTest()
		{
			SystemType<Int32> target = new SystemType<Int32>();
			Assert.AreEqual("System.Int32", target.Name);
		}
	}
}
