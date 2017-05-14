namespace ItemsTests
{
	using Items;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	/// <summary>
	/// This is a test class for CategoryTest and is intended
	/// to contain all CategoryTest Unit Tests
	/// </summary>
	[TestClass()]
	public class CategoryTest
	{
		/// <summary>
		///A test for Category Constructor
		///</summary>
		[TestMethod()]
		public void CategoryConstructorTest()
		{
			Category category = new Category("Test");

			Assert.AreEqual(category.Name, "Test");
			Assert.IsNotNull(category.Attributes);
		}
	}
}
