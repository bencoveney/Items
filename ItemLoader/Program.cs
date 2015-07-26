namespace ItemLoader
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Items;

	/// <summary>
	/// Launches the program
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The connection string
		/// </summary>
		private const string ConnectionString = @"Data Source=BENSDESKTOP\SQLEXPRESS;Initial Catalog=ItemsDB;Integrated Security=True";

		/// <summary>
		/// Gets the tables.
		/// </summary>
		/// <value>
		/// The tables.
		/// </value>
		public static IEnumerable<DatabaseTable> Tables { get; private set; }

		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			DatabaseModel.LoadFromDatabase(ConnectionString);

			Model model = DatabaseModel.ConstructModel();
		}
	}
}
