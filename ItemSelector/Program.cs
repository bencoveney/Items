using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;
using ItemLoader;
using System.Configuration;
using System.Data.SqlClient;

namespace ItemSelector
{
	class Program
	{
		/// <summary>
		/// The connection string
		/// </summary>
		private const string ConnectionString = @"Data Source=BENSDESKTOP\SQLEXPRESS;Initial Catalog=ItemsDB;Integrated Security=True";

		private static Model model;

		static void Main(string[] args)
		{
			Initialise();

			// Create the query object by specifying a starting thing
			ModelQuery query = new ModelQuery(model.Items["Foodstuff"]);
			
			// Follow the relationship to another item
			query.JoinThroughRelationship(model.Relationships["FK_Foodstuff_PersonID"]);
			query.JoinThroughRelationship(model.Relationships["FK_Person_KitchenID"]);

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				using (SqlCommand command = query.GetQuery(connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
						}
					}
				}
			}

			// Might be useful to be able to get all available data members for the query
			// query.DataMembers

			// Maybe make it so only identifiers are shown and other columns can be included or hidden
			// Alternatively you could have it so all columns for all relationships are availible and bringing in a column automatically brings in the relationship
		}

		public static void Initialise()
		{
			DatabaseModel.LoadFromDatabase(ConnectionString);

			model = DatabaseModel.ConstructModel();

			RequireImplementationDetailSchemas();
		}

		private static void RequireImplementationDetailSchemas()
		{
			Thing.RequireSchemaEntry("SqlCatalog", typeof(string));
			Thing.RequireSchemaEntry("SqlSchema", typeof(string));
			Thing.RequireSchemaEntry("SqlTable", typeof(string));
			Thing.RequireSchemaEntry("SqlConstraint", typeof(string));
			Thing.RequireSchemaEntry("SqlColumns", typeof(string));

			DataDefinition.RequireSchemaEntry("SqlColumn", typeof(string));
			DataDefinition.RequireSchemaEntry("OrdinalPosition", typeof(int));
			DataDefinition.RequireSchemaEntry("DefaultValue", typeof(string));
			DataDefinition.RequireSchemaEntry("SqlOrdinal", typeof(int));
			DataDefinition.RequireSchemaEntry("SqlMode", typeof(string));

			SystemTypeBase.RequireSchemaEntry("SqlDataType", typeof(string));
			SystemTypeBase.RequireSchemaEntry("SqlNumericPrecision", typeof(int));
			SystemTypeBase.RequireSchemaEntry("SqlNumericPrecisionRadix", typeof(int));
			SystemTypeBase.RequireSchemaEntry("SqlNumericScale", typeof(int?));
			SystemTypeBase.RequireSchemaEntry("SqlMaxCharacters", typeof(int));
			SystemTypeBase.RequireSchemaEntry("SqlCharacterSet", typeof(string));
			SystemTypeBase.RequireSchemaEntry("SqlCollationName", typeof(string));
			SystemTypeBase.RequireSchemaEntry("SqlDateTimePrecision", typeof(string));
		}
	}
}
