using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ItemsTests
{
	/// <summary>
	///This is a test class for ThingTest and is intended
	///to contain all ThingTest Unit Tests
	///</summary>
	[TestClass()]
	public class ThingTest
	{
		internal IEnumerable<Thing> CreateThings()
		{
			Thing.AddDetailsSchemaEntry("Test", typeof(int));
			List<Thing> things = new List<Thing>();
			things.Add(new Item("Test Item"));
			things.Add(new Relationship("Test Relationship", new Item("Left"), new Item("Right")));
			things.Add(new Category("Test Category"));
			return things;
		}

		/// <summary>
		/// A test for GetReferenceRelationships
		/// </summary>
		[TestMethod()]
		public void GetReferenceRelationshipsTest()
		{
			Thing target = new Item("Referenced");

			// Build a model containing relationships which reference the target
			Model model = new Model();
			foreach (Thing thing in CreateThings())
			{
				model.AddThing(thing);
				model.AddRelationship(new Relationship("Test Relationship " + model.Relationships.Count, target, thing));
			}

			// Add an extra which doesn't reference the target
			model.AddRelationship(new Relationship("Dummy", new Item("Left Item"), new Item("Right Item")));

			IEnumerable<Relationship> referenceRelationships = target.GetReferenceRelationships(model);
			Assert.AreEqual(3, referenceRelationships.Count());
			foreach (Relationship relationship in referenceRelationships)
			{
				Assert.IsTrue(relationship.Name.StartsWith("Test Relationship "));
			}
		}

		/// <summary>
		/// A test for GetReferenceRelationships
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetReferenceRelationshipsTestNullModel()
		{
			Thing target = new Item("Referenced");
			target.GetReferenceRelationships(null);
		}

		/// <summary>
		/// A test for Attributes
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void AttributesTest()
		{
			foreach (Thing thing in CreateThings())
			{
				Assert.IsNotNull(thing.Attributes);
			}
		}

		/// <summary>
		/// A test for Attributes
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void AttributesAddTest()
		{
			foreach (Thing thing in CreateThings())
			{
				DataMember dataMember = new DataMember("DataMember", new SystemType<Int16>(), NullConstraints.None);
				thing.Attributes.Add(dataMember);
				Assert.IsTrue(thing.Attributes.Contains(dataMember));
			}
		}

		/// <summary>
		/// A test for Attributes
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		[ExpectedException(typeof(ArgumentException))]
		public void AttributesAddDuplicateTest()
		{
			foreach (Thing thing in CreateThings())
			{
				DataMember dataMember1 = new DataMember("DataMember", new SystemType<Int16>(), NullConstraints.None);
				DataMember dataMember2 = new DataMember("DataMember", new SystemType<Int16>(), NullConstraints.None);
				thing.Attributes.Add(dataMember1);
				thing.Attributes.Add(dataMember2);
			}
		}

		/// <summary>
		/// A test for Description
		/// </summary>
		[TestMethod()]
		public void DescriptionTest()
		{
			foreach (Thing thing in CreateThings())
			{
				thing.Description = "Test Description";
				Assert.AreEqual(thing.Description, "Test Description");
			}
		}

		/// <summary>
		/// A test for Details
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void DetailsTest()
		{
			foreach (Thing thing in CreateThings())
			{
				Assert.IsNotNull(thing.Details);
			}
		}

		/// <summary>
		/// A test for Details
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void DetailsAddTest()
		{
			foreach (Thing thing in CreateThings())
			{
				thing.Details.Add("Test", 1);
				Assert.AreEqual(thing.Details["Test"], 1);
			}
		}

		/// <summary>
		/// A test for Details
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		[ExpectedException(typeof(ArgumentException))]
		public void DetailsAddDuplicateTest()
		{
			foreach (Thing thing in CreateThings())
			{
				thing.Details.Add("Test", 1);
				thing.Details.Add("Test", 2);
			}
		}

		/// <summary>
		/// A test for IntegerIdentifier
		/// </summary>
		[TestMethod()]
		public void IntegerIdentifierTest()
		{
			foreach (Thing thing in CreateThings())
			{
				DataMember dataMember = new DataMember("Integer Identifier", new SystemType<Int32>(), NullConstraints.None);
				thing.IntegerIdentifier = dataMember;
				Assert.AreEqual(thing.IntegerIdentifier, dataMember);
			}
		}

		/// <summary>
		/// A test for IntegerIdentifier
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void IntegerIdentifierNullTest()
		{
			foreach (Thing thing in CreateThings())
			{
				thing.IntegerIdentifier = null;
			}
		}

		/// <summary>
		/// A test for IntegerIdentifier
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void IntegerIdentifierNullable()
		{
			foreach (Thing thing in CreateThings())
			{
				thing.IntegerIdentifier = new DataMember("Test Member", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable);
			}
		}

		/// <summary>
		/// A test for IntegerIdentifier
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void IntegerIdentifierWrongTypeTest()
		{
			foreach (Thing thing in CreateThings())
			{
				DataMember dataMember = new DataMember("Integer Identifier", new SystemType<string>(), NullConstraints.None);
				thing.IntegerIdentifier = dataMember;
				Assert.AreEqual(thing.IntegerIdentifier, dataMember);
			}
		}

		/// <summary>
		/// A test for Name
		/// </summary>
		[TestMethod()]
		public void NameTest()
		{
			foreach (Thing thing in CreateThings())
			{
				Assert.AreEqual(thing.Name.IndexOf("Test"), 0);
			}
		}

		/// <summary>
		/// A test for setting an empty Name
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameTestNullOrEmpty()
		{
			Item item = new Item("");
		}

		/// <summary>
		/// A test for StringIdentifier
		/// </summary>
		[TestMethod()]
		public void StringIdentifierTest()
		{
			foreach (Thing thing in CreateThings())
			{
				DataMember dataMember = new DataMember("String Identifier", new SystemType<string>(), NullConstraints.None);
				thing.StringIdentifier = dataMember;
				Assert.AreEqual(thing.StringIdentifier, dataMember);
			}
		}

		/// <summary>
		/// A test for StringIdentifier
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringIdentifierNullTest()
		{
			foreach (Thing thing in CreateThings())
			{
				thing.StringIdentifier = null;
			}
		}

		/// <summary>
		/// A test for StringIdentifier
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void StringIdentifierWrongTypeTest()
		{
			foreach (Thing thing in CreateThings())
			{
				DataMember dataMember = new DataMember("String Identifier", new SystemType<Int32>(), NullConstraints.None);
				thing.StringIdentifier = dataMember;
				Assert.AreEqual(thing.IntegerIdentifier, dataMember);
			}
		}
	}
}
