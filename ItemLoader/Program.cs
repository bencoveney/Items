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
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			// Load the tables
			IEnumerable<DatabaseTable> tables = DatabaseTable.LoadTables(ConnectionString);

			tables.ToList().ForEach(table => Console.WriteLine(table));

			//Model model = ConstructModel(tables);

			////Loader loader = new Loader();

			////using (SqlConnection connection = new SqlConnection(ConnectionString))
			////{
			////    loader.Load(connection);
			////}

			////XmlCreator creator = new XmlCreator(loader.Model);
			////string output = creator.TransformText();
		}

		/// <summary>
		/// Constructs a model from the given database objects
		/// </summary>
		/// <param name="tables">The tables.</param>
		/// <returns>A model build from the schema</returns>
		public static Model ConstructModel(IEnumerable<DatabaseTable> tables)
		{
			Model result = new Model();

			foreach (DatabaseTable table in tables)
			{
				Thing thing;

				if (table.Name.Contains("Collection"))
				{
					// Its a relationship
					// Assume the second and third columns are the important ones
					thing = new Relationship(table.Name, result.Things.Single(match => match.Name == table.Columns[1].Name), result.Things.Single(match => match.Name == table.Columns[2].Name));
				}
				else if (table.Name.Contains("Category"))
				{
					// Its a category
					thing = new Category(table.Name);
				}
				else
				{
					// Its a generic thing
					thing = new Item(table.Name);
				}

				foreach (DatabaseColumn column in table.Columns)
				{
					// TODO stuff
				}

				result.AddThing(thing);
			}

			return result;
		}
	}
}
