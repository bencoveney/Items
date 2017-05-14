using System;
using GeneratedCode;

namespace ItemLibGen
{
	class Test
	{
		[Test]
		public static void TestMethod()
		{
			Foodstuff foodStuff = new Foodstuff(5, "Burga", "Waitrose", 4, "Pcs");

			Console.WriteLine(foodStuff.ID);
			Console.WriteLine(foodStuff.Name);
			Console.WriteLine(foodStuff.Brand);
			Console.WriteLine(foodStuff.Quantity);
			Console.WriteLine(foodStuff.Units);
		}
	}
}
