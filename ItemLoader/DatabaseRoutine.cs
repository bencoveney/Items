namespace ItemLoader
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Linq;

	/// <summary>
	/// Defines the type of a routine
	/// </summary>
	public enum RoutineType
	{
		/// <summary>
		/// A stored procedure
		/// </summary>
		Procedure = 1,

		/// <summary>
		/// A function
		/// </summary>
		Function = 2
	}

	/// <summary>
	/// A stored procedure in the database
	/// </summary>
	public class DatabaseRoutine
	{
		/// <summary>
		/// Query to find the routines in the database
		/// </summary>
		private const string RoutinesQuery = @"
SELECT
	DatabaseRoutines.SPECIFIC_CATALOG,
	DatabaseRoutines.SPECIFIC_SCHEMA,
	DatabaseRoutines.SPECIFIC_NAME,
	DatabaseRoutines.ROUTINE_TYPE,
	DatabaseRoutines.DATA_TYPE,
	DatabaseRoutines.CHARACTER_MAXIMUM_LENGTH,
	DatabaseRoutines.COLLATION_NAME,
	DatabaseRoutines.CHARACTER_SET_NAME,
	DatabaseRoutines.NUMERIC_PRECISION,
	DatabaseRoutines.NUMERIC_PRECISION_RADIX,
	DatabaseRoutines.NUMERIC_SCALE,
	DatabaseRoutines.DATETIME_PRECISION,
	DatabaseRoutines.ROUTINE_DEFINITION,
	DatabaseRoutines.CREATED,
	DatabaseRoutines.LAST_ALTERED
FROM
	INFORMATION_SCHEMA.ROUTINES AS DatabaseRoutines";

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseRoutine"/> class.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <param name="schema">The schema.</param>
		/// <param name="name">The name.</param>
		/// <param name="routineType">Type of the routine.</param>
		/// <param name="returnType">Type of the return.</param>
		/// <param name="definition">The definition.</param>
		/// <param name="created">The created.</param>
		/// <param name="lastAltered">The last altered.</param>
		public DatabaseRoutine(string catalog, string schema, string name, RoutineType routineType, DatabaseType returnType, string definition, DateTime created, DateTime lastAltered)
		{
			this.Catalog = catalog;
			this.Schema = schema;
			this.Name = name;
			this.RoutineType = routineType;
			this.ReturnType = returnType;
			this.Definition = definition;
			this.Created = created;
			this.LastAltered = lastAltered;

			this.Parameters = new List<DatabaseRoutineParameter>();
		}

		/// <summary>
		/// Gets the catalog this routine is in.
		/// </summary>
		/// <value>
		/// The catalog.
		/// </value>
		public string Catalog
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the schema this routine is in.
		/// </summary>
		/// <value>
		/// The schema.
		/// </value>
		public string Schema
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of this routine.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public List<DatabaseRoutineParameter> Parameters
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the type of routine (procedure or function).
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public RoutineType RoutineType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the SQL type of the value returned by this routine
		/// </summary>
		/// <value>
		/// The type of the return.
		/// </value>
		public DatabaseType ReturnType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the first 4000 characters of the routine's definition text.
		/// </summary>
		/// <value>
		/// The routine definition.
		/// </value>
		public string Definition
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the date this routine was created.
		/// </summary>
		/// <value>
		/// The created.
		/// </value>
		public DateTime Created
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the date this routine was last altered.
		/// </summary>
		/// <value>
		/// The last altered.
		/// </value>
		public DateTime LastAltered
		{
			get;
			private set;
		}

		/// <summary>
		/// Loads the routines from the database.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		public static void LoadRoutines(string connectionString)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Query the database for the table data
				using (SqlCommand command = new SqlCommand(RoutinesQuery, connection))
				{
					using (SqlDataReader result = command.ExecuteReader())
					{
						while (result.Read())
						{
							// Read the result data for the routine
							string catalog = (string)result["SPECIFIC_CATALOG"];
							string schema = (string)result["SPECIFIC_SCHEMA"];
							string name = (string)result["SPECIFIC_NAME"];
							string routineTypeAsText = (string)result["ROUTINE_TYPE"];
							string definition = (string)result["ROUTINE_DEFINITION"];
							DateTime created = (DateTime)result["CREATED"];
							DateTime lastAltered = (DateTime)result["LAST_ALTERED"];

							// Build the proper data structure for routine type
							RoutineType routineType = (RoutineType)Enum.Parse(typeof(RoutineType), routineTypeAsText, true);

							// Build the proper data structure for return type
							DatabaseType returnType = null;
							if (!result.IsDBNull("DATA_TYPE"))
							{
								// Read the result data for the routine's return type
								string dataType = (string)result["DATA_TYPE"];
								int? characterMaximumLength = result.GetNullable<int>("CHARACTER_MAXIMUM_LENGTH");
								int? numericPrecision = result.GetNullable<int>("NUMERIC_PRECISION");
								int? numericPrecisionRadix = result.GetNullable<int>("NUMERIC_PRECISION_RADIX");
								int? numericScale = result.GetNullable<int>("NUMERIC_SCALE");
								int? dateTimePrecision = result.GetNullable<int>("DATETIME_PRECISION");
								string characterSetName = result.GetNullableString("CHARACTER_SET_NAME");
								string collationName = result.GetNullableString("COLLATION_NAME");

								returnType = new DatabaseType(dataType, characterMaximumLength, characterSetName, collationName, numericPrecision, numericPrecisionRadix, numericScale, dateTimePrecision);
							}

							// Build the new routine
							DatabaseRoutine routine = new DatabaseRoutine(catalog, schema, name, routineType, returnType, definition, created, lastAltered);

							DatabaseModel.Routines.Add(routine);
						}
					}
				}

				// Populate parameters
				foreach (DatabaseRoutine routine in DatabaseModel.Routines)
				{
					DatabaseRoutineParameter.PopulateParameters(routine, connection);
				}
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			if (this.ReturnType != null)
			{
				return string.Format("{0}.{1}.{2} ({3})", this.Catalog, this.Schema, this.Name, this.ReturnType);
			}
			else
			{
				return string.Format("{0}.{1}.{2} (NULL)", this.Catalog, this.Schema, this.Name);
			}
		}
	}
}
