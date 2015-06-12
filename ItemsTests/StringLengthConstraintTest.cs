using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	/// This is a test class for StringLengthConstraintTest and is intended
	/// to contain all StringLengthConstraintTest Unit Tests
	/// TODO Disallow None
	/// </summary>
	[TestClass()]
	public class StringLengthConstraintTest
	{
		/// <summary>
		/// A test for StringLengthConstraint Constructor
		/// </summary>
		[TestMethod()]
		public void StringLengthConstraintConstructorTest()
		{
			StringLengthConstraint stringLengthConstraint = new StringLengthConstraint(LengthComparison.Exactly, 5);
			Assert.AreEqual(LengthComparison.Exactly, stringLengthConstraint.Comparison);
			Assert.AreEqual(5, stringLengthConstraint.Value);
			Assert.AreEqual(false, stringLengthConstraint.IsDeferrable);
		}

		/// <summary>
		/// A test for Comparison
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ComparisonTest()
		{
			StringLengthConstraint stringLengthConstraint = new StringLengthConstraint(LengthComparison.LongerThan, 6);
			Assert.AreEqual(LengthComparison.LongerThan, stringLengthConstraint.Comparison);
		}

		/// <summary>
		/// A test for IsDeferrable
		/// </summary>
		[TestMethod()]
		public void IsDeferrableTest()
		{
			StringLengthConstraint stringLengthConstraint = new StringLengthConstraint(LengthComparison.ShorterThan, 7);
			Assert.AreEqual(false, stringLengthConstraint.IsDeferrable);

			stringLengthConstraint.IsDeferrable = true;
			Assert.AreEqual(true, stringLengthConstraint.IsDeferrable);

			stringLengthConstraint.IsDeferrable = false;
			Assert.AreEqual(false, stringLengthConstraint.IsDeferrable);
		}

		/// <summary>
		///A test for NotEmpty
		///</summary>
		[TestMethod()]
		public void NotEmptyTest()
		{
			StringLengthConstraint stringLengthConstraint = StringLengthConstraint.NotEmpty;
			Assert.AreEqual(LengthComparison.LongerThan, stringLengthConstraint.Comparison);
			Assert.AreEqual(0, stringLengthConstraint.Value);
		}

		/// <summary>
		///A test for Value
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ValueTest()
		{
			StringLengthConstraint stringLengthConstraint = new StringLengthConstraint(LengthComparison.LongerThan, 8);
			Assert.AreEqual(8, stringLengthConstraint.Value);
		}
	}
}
