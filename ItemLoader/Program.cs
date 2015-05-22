namespace ItemLoader
{
	using System;
	using System.Data.SqlClient;
	using ItemSerialiser;

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
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			Loader loader = new Loader();

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				loader.Load(connection);
			}

			XmlCreator creator = new XmlCreator(loader.Model);
			string output = creator.TransformText();
		}
	}
}
