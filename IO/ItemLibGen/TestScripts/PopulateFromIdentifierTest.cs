using System;
using System.Collections.Generic;
using GeneratedCode;

namespace ItemLibGen
{
	public class PopulateFromIdentifierTest
	{
		[Test]
		public static void TestMethod()
		{
			Person benByName = new Person("Ben");
			Console.WriteLine(benByName.ID);

			Person benById = new Person(1);
			Console.WriteLine(benById.Name);
		}
	}
}
