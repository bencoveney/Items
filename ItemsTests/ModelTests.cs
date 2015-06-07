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
		[TestMethod]
		public void ModelConstruction()
		{
			Model model = new Model();
			Assert.IsNotNull(model.Categories);
			Assert.IsNotNull(model.Items);
			Assert.IsNotNull(model.Relationships);
		}

		[TestMethod]
		public void ModelAddItem()
		{
			Model model = new Model();
			Item item = new Item("Test");

			model.AddItem(item);
			Assert.IsTrue(model.Items.ContainsValue(item));
		}

		[TestMethod]
		public void ModelAddRelationship()
		{
			Model model = new Model();
			Relationship relationship = new Relationship("Test", new Item("left"), new Item("right"));

			model.AddRelationship(relationship);
			Assert.IsTrue(model.Relationships.ContainsValue(relationship));
		}

		[TestMethod]
		public void ModelAddCategory()
		{
			Model model = new Model();
			Category category = new Category("Test");

			model.AddCategory(category);
			Assert.IsTrue(model.Categories.ContainsValue(category));
		}

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

			Assert.IsTrue(model.Items.ContainsValue(item));
			Assert.IsTrue(model.Relationships.ContainsValue(relationship));
			Assert.IsTrue(model.Categories.ContainsValue(category));
		}

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
	}
}
