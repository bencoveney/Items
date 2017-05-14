using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	/// This is a test class for StringValueConstraintTest and is intended
	/// to contain all StringValueConstraintTest Unit Tests
	/// </summary>
	[TestClass()]
	public class StringValueConstraintTest
	{
		/// <summary>
		/// A test for StringValueConstraint Constructor
		/// </summary>
		[TestMethod()]
		public void StringValueConstraintConstructorTest()
		{
			StringValueConstraint stringValueConstraint = new StringValueConstraint(Items.StringComparison.BeginsWith, StringComparer.CurrentCulture, "Test Value");
			Assert.AreEqual(Items.StringComparison.BeginsWith, stringValueConstraint.Comparison);
			Assert.AreEqual(StringComparer.CurrentCulture, stringValueConstraint.Comparer);
			Assert.AreEqual("Test Value", stringValueConstraint.Value);
			Assert.AreEqual(false, stringValueConstraint.IsDeferrable);
		}

		/// <summary>
		/// A test for Comparer
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ComparerTest()
		{
			StringValueConstraint stringValueConstraint = new StringValueConstraint(Items.StringComparison.EndWith, StringComparer.OrdinalIgnoreCase, "Test Value");
			Assert.AreEqual(StringComparer.OrdinalIgnoreCase, stringValueConstraint.Comparer);
		}

		/// <summary>
		/// A test for Comparison
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ComparisonTest()
		{
			StringValueConstraint stringValueConstraint = new StringValueConstraint(Items.StringComparison.MatchesRegex, StringComparer.CurrentCulture, "Test Value");
			Assert.AreEqual(Items.StringComparison.MatchesRegex, stringValueConstraint.Comparison);
		}

		/// <summary>
		/// A test for IsDeferrable
		/// </summary>
		[TestMethod()]
		public void IsDeferrableTest()
		{
			StringValueConstraint stringValueConstraint = new StringValueConstraint(Items.StringComparison.MatchesRegex, StringComparer.CurrentCulture, "Test Value");
			Assert.AreEqual(false, stringValueConstraint.IsDeferrable);

			stringValueConstraint.IsDeferrable = true;
			Assert.AreEqual(true, stringValueConstraint.IsDeferrable);

			stringValueConstraint.IsDeferrable = false;
			Assert.AreEqual(false, stringValueConstraint.IsDeferrable);
		}

		/// <summary>
		/// A test for Value
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ValueTest()
		{
			StringValueConstraint stringValueConstraint = new StringValueConstraint(Items.StringComparison.BeginsWith, StringComparer.CurrentCulture, "Test Value_!_!_!0001");
			Assert.AreEqual("Test Value_!_!_!0001", stringValueConstraint.Value);
		}
	}
}
