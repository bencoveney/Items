namespace ItemLoader
{
	using System.Collections.Generic;
	using System.Data.SqlClient;

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
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				IEnumerable<DatabaseConstraint> constraints = DatabaseConstraint.LoadConstraints(connection);
				connection.Close();
			}

			////Loader loader = new Loader();

			////using (SqlConnection connection = new SqlConnection(ConnectionString))
			////{
			////    loader.Load(connection);
			////}

			////XmlCreator creator = new XmlCreator(loader.Model);
			////string output = creator.TransformText();
		}
	}
}
