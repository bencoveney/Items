using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Items;

namespace ItemsTests
{
	[TestClass]
	public class ModelTests
	{
		/// <summary>
		/// Tests model construction.
		/// </summary>
		[TestMethod]
		public void ModelConstruction()
		{
			Model model = new Model();
			Assert.IsNotNull(model.Categories);
			Assert.IsNotNull(model.Items);
			Assert.IsNotNull(model.Relationships);
		}

		/// <summary>
		/// Tests adding an item to the model.
		/// </summary>
		[TestMethod]
		public void ModelAddItem()
		{
			Model model = new Model();
			Item item = new Item("Test");

			model.AddItem(item);
			Assert.IsTrue(model.Items.Contains(item));
		}

		/// <summary>
		/// Tests adding a relationship to the model.
		/// </summary>
		[TestMethod]
		public void ModelAddRelationship()
		{
			Model model = new Model();
			Relationship relationship = new Relationship("Test", new Item("left"), new Item("right"));

			model.AddRelationship(relationship);
			Assert.IsTrue(model.Relationships.Contains(relationship));
		}

		/// <summary>
		/// Tests adding a category to the model.
		/// </summary>
		[TestMethod]
		public void ModelAddCategory()
		{
			Model model = new Model();
			Category category = new Category("Test");

			model.AddCategory(category);
			Assert.IsTrue(model.Categories.Contains(category));
		}

		/// <summary>
		/// Tests adding a thing to the model.
		/// </summary>
		[TestMethod]
		public void ModelAddThing()
		{
			Model model = new Model();
			Item item = new Item("Test");
			Relationship relationship = new Relationship("Test", new Item("left"), new Item("right"));
			Category category = new Category("Test");

			model.AddThing(item);
			model.AddThing(relationship);
			model.AddThing(category);

			Assert.IsTrue(model.Items.Contains(item));
			Assert.IsTrue(model.Relationships.Contains(relationship));
			Assert.IsTrue(model.Categories.Contains(category));
		}

		/// <summary>
		/// Tests adding an item to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ModelAddItemNull()
		{
			Model model = new Model();
			model.AddItem(null);
		}

		/// <summary>
		/// Tests adding a relationship to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ModelAddRelationshipNull()
		{
			Model model = new Model();
			model.AddRelationship(null);
		}

		/// <summary>
		/// Tests adding a category to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ModelAddCategoryNull()
		{
			Model model = new Model();
			model.AddCategory(null);
		}

		/// <summary>
		/// Tests adding a thing to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ModelAddThingNull()
		{
			Model model = new Model();
			model.AddThing(null);
		}

		/// <summary>
		/// Tests adding a duplicate item to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ModelAddDuplicateItem()
		{
			Model model = new Model();
			Item item1 = new Item("Test");
			Item item2 = new Item("Test");

			model.AddItem(item1);
			model.AddItem(item2);
		}

		/// <summary>
		/// Tests adding a duplicate relationship to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ModelAddDuplicateRelationship()
		{
			Model model = new Model();
			Relationship relationship1 = new Relationship("Test", new Item("left"), new Item("right"));
			Relationship relationship2 = new Relationship("Test", new Item("left"), new Item("right"));

			model.AddRelationship(relationship1);
			model.AddRelationship(relationship2);
		}

		/// <summary>
		/// Tests adding a duplicate category to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ModelAddDuplicateCategory()
		{
			Model model = new Model();
			Category category1 = new Category("Test");
			Category category2 = new Category("Test");

			model.AddCategory(category1);
			model.AddCategory(category2);
		}

		/// <summary>
		/// Dummy class for testing adding a thing of unknown type
		/// </summary>
		public class UnknownThingType : Thing
		{
			public UnknownThingType()
				: base("Test")
			{
			}
		}

		/// <summary>
		/// Tests adding a thing to the model.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ModelAddThingUnknownType()
		{
			Model model = new Model();
			model.AddThing(new UnknownThingType());
		}

		/// <summary>
		/// Tests the Things property.
		/// </summary>
		[TestMethod]
		public void ModelThingsTest()
		{
			Model model = new Model();

			Category category = new Category("Test Category");
			Item item = new Item("Test Item");
			Relationship relationship = new Relationship("Test Relationship", new RelationshipLink(new Item("Left Item"), 0), new RelationshipLink(new Item("Right Item"), 0));

			model.AddCategory(category);
			model.AddItem(item);
			model.AddRelationship(relationship);

			Assert.IsTrue(model.Things.Contains(category));
			Assert.IsTrue(model.Things.Contains(item));
			Assert.IsTrue(model.Things.Contains(relationship));
		}

		/// <summary>
		/// Tests the successful validation functionality.
		/// </summary>
		[TestMethod]
		public void ModelValidateTest()
		{
			Item item = new Item("Test Item");
			DataMember dataMember = new DataMember("String Attribute", new SystemType<string>(), NullConstraints.None);
			item.Attributes.Add(dataMember);
			item.StringIdentifier = dataMember;

			Model model = new Model();
			model.AddItem(item);
			model.Validate();
		}

		/// <summary>
		/// Tests the validation functionality identifies items without identifiers.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidModelException))]
		public void ModelValidateTestNoIdentifier()
		{
			Item item = new Item("Test Item");
			DataMember dataMember = new DataMember("String Attribute", new SystemType<string>(), NullConstraints.None);
			item.Attributes.Add(dataMember);

			Model model = new Model();
			model.AddItem(item);
			model.Validate();
		}
	}
}
