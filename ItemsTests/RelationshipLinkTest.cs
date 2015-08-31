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
			IThing thing = new Item("Test");
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
			IThing thing = new Item("Test");
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
			IThing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, -1);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void RelationshipLinkConstructorTestUpperNegative()
		{
			IThing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 5, -1);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelationshipLinkConstructorTestNullThing()
		{
			IThing thing = null;
			RelationshipLink relationshipLink = new RelationshipLink(thing, 80);
		}

		/// <summary>
		/// A test for RelationshipLink Constructor
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void RelationshipLinkConstructorUnordered()
		{
			IThing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 100, 78);
		}

		/// <summary>
		/// A test for ToString
		/// </summary>
		[TestMethod()]
		public void ToStringTest()
		{
			IThing thing = new Item("Test Thing");

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
			IThing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 51);
			Assert.AreEqual(51, relationshipLink.AmountLower);
		}

		/// <summary>
		/// A test for AmountLower
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void AmountLowerTestUnordered()
		{
			IThing thing = new Item("Test");
			RelationshipLink_Accessor relationshipLink = new RelationshipLink_Accessor(thing, 40, 50);
			relationshipLink.AmountLower = 60;
		}

		/// <summary>
		/// A test for AmountUpper
		/// </summary>
		[TestMethod()]
		public void AmountUpperTest()
		{
			IThing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 50, 56);
			Assert.AreEqual(56, relationshipLink.AmountUpper);
		}

		/// <summary>
		/// A test for Thing
		/// </summary>
		[TestMethod()]
		public void ThingTest()
		{
			IThing thing = new Item("Test");
			RelationshipLink relationshipLink = new RelationshipLink(thing, 48);
			Assert.AreEqual(thing, relationshipLink.Thing);
		}
	}
}
