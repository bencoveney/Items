namespace ItemsTests
{
	using Items;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	/// <summary>
	/// This is a test class for ItemTest and is intended
	/// to contain all ItemTest Unit Tests
	/// </summary>
	[TestClass()]
	public class ItemTest
	{
		/// <summary>
		/// A test for Item Constructor
		/// </summary>
		[TestMethod()]
		public void ItemConstructorTest()
		{
			Item item = new Item("Test");

			Assert.IsNotNull(item.Attributes);
			Assert.IsNotNull(item.Behaviors);
			Assert.IsNotNull(item.Details);
		}

		/// <summary>
		/// A test for Behaviors
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void BehaviorsTest()
		{
			Item item = new Item("Test Item");
			Behavior behavior = new Behavior("Test Behavior");

			item.Behaviors.Add(behavior);

			Assert.IsTrue(item.Behaviors.Contains(behavior));
		}

		/// <summary>
		/// A test for Behaviors
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void BehaviorsDuplicateTest()
		{
			Item item = new Item("Test Item");
			Behavior behavior = new Behavior("Test Behavior");

			item.Behaviors.Add(behavior);

			Assert.IsTrue(item.Behaviors.Contains(behavior));
		}
	}
}
