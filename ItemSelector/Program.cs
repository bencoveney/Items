namespace ItemSelector
{
	using System;
	using ItemLoader;
	using Items;

	/// <summary>
	/// The program
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// The connection string
		/// </summary>
		private const string ConnectionString = @"Data Source=BENSDESKTOP\SQLEXPRESS;Initial Catalog=ItemsDB;Integrated Security=True";

		/// <summary>
		/// The model
		/// </summary>
		private static Model model;

		/// <summary>
		/// The program entry point.
		/// </summary>
		public static void Main()
		{
			Initialize();

			// Create the query object by specifying a starting thing
			ModelQuery query = new ModelQuery(model.Items["Foodstuff"] as DbiItem);
			
			// Follow the relationship to another item
			query.JoinThroughRelationship(model.Relationships["FK_Foodstuff_PersonID"] as DbiRelationship);
			query.JoinThroughRelationship(model.Relationships["FK_Person_KitchenID"] as DbiRelationship);

			string querySql = query.BuildSql();
			Console.Write(querySql);

			// Might be useful to be able to get all available data members for the query
			// query.DataMembers

			// Maybe make it so only identifiers are shown and other columns can be included or hidden
			// Alternatively you could have it so all columns for all relationships are availible and bringing in a column automatically brings in the relationship
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public static void Initialize()
		{
			DatabaseModel.LoadFromDatabase(ConnectionString);

			model = DatabaseModel.ConstructModel();
		}
	}
}
