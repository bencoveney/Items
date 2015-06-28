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

			string querySql = query.GetSql();

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
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(Thing), "SqlCatalog", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(Thing), "SqlSchema", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(Thing), "SqlTable", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(Thing), "SqlConstraint", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(Thing), "SqlColumns", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(DataDefinition), "SqlColumn", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(DataDefinition), "OrdinalPosition", typeof(int));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(DataDefinition), "DefaultValue", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(DataDefinition), "SqlOrdinal", typeof(int));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(DataDefinition), "SqlMode", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlDataType", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlNumericPrecision", typeof(int));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlNumericPrecisionRadix", typeof(int));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlNumericScale", typeof(int?));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlMaxCharacters", typeof(int));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlCharacterSet", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlCollationName", typeof(string));
			ImplementationDetailsDictionary.RequireSchemaEntry(typeof(SystemTypeBase), "SqlDateTimePrecision", typeof(string));
		}
	}
}
