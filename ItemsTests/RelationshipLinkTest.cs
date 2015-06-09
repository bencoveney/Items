using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ItemsTests
{
	/// <summary>
	/// This is a test class for RelationshipLinkTest and is intended
	/// to contain all RelationshipLinkTest Unit Tests
	/// </summary>
	[TestClass()]
	public class RelationshipLinkTest
	{
		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		public void RelationshipLinkConstructorTestOnlyLower()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 53);

			Assert.AreEqual(thing, relationshipLink.Thing);
			Assert.AreEqual(53, relationshipLink.AmountLower);
			Assert.IsNull(relationshipLink.AmountUpper);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		public void RelationshipLinkConstructorTestLowerAndUpper()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 62, 65);

			Assert.AreEqual(thing, relationshipLink.Thing);
			Assert.AreEqual(62, relationshipLink.AmountLower);
			Assert.AreEqual(65, relationshipLink.AmountUpper);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void RelationshipLinkConstructorTestLowerNegative()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, -1);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void RelationshipLinkConstructorTestUpperNegative()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 5, -1);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelationshipLinkConstructorTestNullThing()
		{
			Thing thing = null;
			RelationshipLink relationshipLink = new RelationshipLink(thing, 80);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void RelationshipLinkConstructorUnordered()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 100, 78);
		}

		/// <summary>
		/// A test for ToString
		/// </summary>
		[TestMethod()]
		public void ToStringTest()
		{
			Thing thing = new Item("Test Thing");

			RelationshipLink relationshipLink1 = new RelationshipLink(thing, 54);
			Assert.AreEqual("Test Thing (54 - *)", relationshipLink1.ToString());

			RelationshipLink relationshipLink2 = new RelationshipLink(thing, 54, 54);
			Assert.AreEqual("Test Thing (54)", relationshipLink2.ToString());

			RelationshipLink relationshipLink3 = new RelationshipLink(thing, 56, 57);
			Assert.AreEqual("Test Thing (56 - 57)", relationshipLink3.ToString());
		}

		/// <summary>
		/// A test for AmountLower
		/// </summary>
		[TestMethod()]
		public void AmountLowerTest()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 51);
			Assert.AreEqual(51, relationshipLink.AmountLower);
		}

		/// <summary>
		/// A test for AmountUpper
		/// </summary>
		[TestMethod()]
		public void AmountUpperTest()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 50, 56);
			Assert.AreEqual(56, relationshipLink.AmountUpper);
		}

		/// <summary>
		/// A test for Thing
		/// </summary>
		[TestMethod()]
		public void ThingTest()
		{
			Thing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 48);
			Assert.AreEqual(thing, relationshipLink.Thing);
		}
	}
}
