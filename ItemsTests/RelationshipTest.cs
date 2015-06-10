using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for RelationshipTest and is intended
	///to contain all RelationshipTest Unit Tests
	///</summary>
	[TestClass()]
	public class RelationshipTest
	{
		/// <summary>
		/// A test for Relationship Constructor
		/// </summary>
		[TestMethod()]
		public void RelationshipConstructorTest()
		{
			Item left = new Item("Left");
			Item right = new Item("Right");
			Relationship relationship = new Relationship("Test", left, right);

			Assert.IsNotNull(relationship.Attributes);
			Assert.IsNotNull(relationship.Details);
			Assert.IsNotNull(relationship.LeftLink);
			Assert.IsNotNull(relationship.RightLink);
		}
		/// <summary>
		/// A test for Relationship Constructor
		/// </summary>
		[TestMethod()]
		public void RelationshipConstructorLinkTest()
		{
			RelationshipLink leftLink = new RelationshipLink(new Item("Left"), 0);
			RelationshipLink rightLink = new RelationshipLink(new Item("Right"), 0);
			Relationship relationship = new Relationship("Test", leftLink, rightLink);

			Assert.IsNotNull(relationship.Attributes);
			Assert.IsNotNull(relationship.Details);
			Assert.IsNotNull(relationship.LeftLink);
			Assert.IsNotNull(relationship.RightLink);
		}

		/// <summary>
		///A test for LeftLink
		///</summary>
		[TestMethod()]
		public void LeftLinkTest()
		{
			Item left = new Item("Left");
			Item right = new Item("Right");
			Relationship relationship = new Relationship("Test", left, right);

			Assert.AreEqual(relationship.LeftLink.Thing, left);
		}

		/// <summary>
		/// A test for Links
		/// </summary>
		[TestMethod()]
		public void LinksTest()
		{
			Item left = new Item("Left");
			Item right = new Item("Right");
			Relationship relationship = new Relationship("Test", left, right);

			Assert.IsTrue(relationship.Links.Count == 2);
			Assert.IsTrue(relationship.Links.Any(link => link.Thing == left));
			Assert.IsTrue(relationship.Links.Any(link => link.Thing == right));
		}

		/// <summary>
		/// A test for RightLink
		/// </summary>
		[TestMethod()]
		public void RightLinkTest()
		{
			Item left = new Item("Left");
			Item right = new Item("Right");
			Relationship relationship = new Relationship("Test", left, right);

			Assert.AreEqual(relationship.RightLink.Thing, right);
		}
	}
}
