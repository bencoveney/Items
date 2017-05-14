using DatabaseObjects;
using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model = Items.Model;

namespace ItemLoader
{
	public class Factory
	{
		/// <summary>
		/// Constructs the model.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns></returns>
		public static Model ConstructModel(string connectionString)
		{
			Model result = new Model();
			DatabaseObjects.Model databaseObjects = DatabaseObjects.Model.LoadFromDatabase(connectionString);

			// Populate base items
			foreach (Table table in databaseObjects.Tables.Where(table => table.RepresentsItem))
			{
				DbiItem item = new DbiItem(table.Name, table.Catalog, table.Schema, table.Name);

				// Add columns (which aren't relationships)
				foreach (Column column in table.Columns.Where(dbColumn => !IsColumnReferencer(dbColumn, databaseObjects)))
				{
					DbiDataMember dataAttribute = new DbiDataMember(GetColumnAttributeName(column, databaseObjects), ConvertToItemsType(column.DataType), GetColumnNullability(column), column.Name, column.OrdinalPosition, column.ColumnDefault);

					// Populate constraints which can be inferred from the type
					foreach (IConstraint constraint in GetTypeConstraints(column.DataType))
					{
						dataAttribute.Constraints.Add(constraint);
					}

					item.Attributes.Add(dataAttribute);

					// If this is the primary key we definitely want to mark it as the integer identifier
					if (GetColumnReferencingConstraints(column, databaseObjects).Any(constraint => constraint.ConstraintType == ConstraintType.PrimaryKey) && column.DataType.IsInteger)
					{
						item.IntegerIdentifier = dataAttribute;
					}

					// If this column is enforced as unique we can assume it can be used to identify the item
					if (GetColumnReferencingConstraints(column, databaseObjects).Any(constraint => constraint.ConstraintType == ConstraintType.Unique))
					{
						// Assign the integer identifier if there isnt one and this attribute is suitable
						if (item.IntegerIdentifier == null && column.DataType.IsInteger)
						{
							item.IntegerIdentifier = dataAttribute;
						}

						// Assign the string identifier if there isnt one and this attribute is suitable
						if (item.StringIdentifier == null && column.DataType.IsText)
						{
							item.StringIdentifier = dataAttribute;
						}
					}
				}

				result.AddItem(item);
			}

			// Populate categories
			foreach (Table table in databaseObjects.Tables.Where(table => table.RepresentsCategory))
			{
				DbiCategory category = new DbiCategory(table.Name, table.Catalog, table.Schema, table.Name);

				// Add columns (which aren't relationships)
				foreach (Column column in table.Columns.Where(dbColumn => !IsColumnReferencer(dbColumn, databaseObjects) && !!IsColumnReferenced(dbColumn, databaseObjects)))
				{
					DbiDataMember dataAttribute = new DbiDataMember(GetColumnAttributeName(column, databaseObjects), ConvertToItemsType(column.DataType), GetColumnNullability(column), column.Name, column.OrdinalPosition, column.ColumnDefault);

					// Populate constraints which can be inferred from the type
					foreach (IConstraint constraint in GetTypeConstraints(column.DataType))
					{
						dataAttribute.Constraints.Add(constraint);
					}

					category.Attributes.Add(dataAttribute);
				}

				result.AddCategory(category);
			}

			// Populate relationships from link tables
			foreach (Table table in databaseObjects.Tables.Where(t => t.RepresentsRelationship))
			{
				// Find the first columns which are foreign keys and assume those are the right and left sides of the relationship
				IEnumerable<Column> columnsWithForeignKeys = table.Columns.Where(column => IsColumnReferencer(column, databaseObjects));

				Column leftColumn = columnsWithForeignKeys.OrderBy(column => column.OrdinalPosition).ToList()[0];
				IDbiThing leftThing = result.Things.OfType<IDbiThing>().Single(thing =>
					IsTableMatchForThing(
						FindTableContainingColumn(
							databaseObjects,
							GetColumnReferencedColumn(
								leftColumn,
								databaseObjects)),
						thing));

				Column rightColumn = columnsWithForeignKeys.OrderBy(column => column.OrdinalPosition).ToList()[1];
				IDbiThing rightThing = result.Things.OfType<IDbiThing>().Single(thing =>
					IsTableMatchForThing(
						FindTableContainingColumn(
							databaseObjects,
							GetColumnReferencedColumn(
								rightColumn,
								databaseObjects)),
						thing));

				DbiRelationship relationship = new DbiRelationship(table.Name, leftThing, rightThing, table.Catalog, table.Schema, table.Name, string.Join(",", columnsWithForeignKeys.Select<Column, string>(column => column.Name)), null);

				result.AddRelationship(relationship);

				// TODO Populate Attributes for relationship. should probably be done by refactoring other attribute selections
			}

			// Populate relationships from foreign keys for tables we haven't processed yet
			foreach (Table table in databaseObjects.Tables)
			{
				// Find the columns with foreign keys
				IEnumerable<Column> columnsWithForeignKeys = table.Columns.Where(column => IsColumnReferencer(column, databaseObjects));

				// If this is a relationship table then the first two columns are already a relationship
				if (table.RepresentsRelationship)
				{
					columnsWithForeignKeys = columnsWithForeignKeys.Skip(2);
				}

				foreach (Column column in columnsWithForeignKeys)
				{
					// Get the thing for this table
					IDbiThing leftThing = result.Things.OfType<IDbiThing>().Single(thing => IsTableMatchForThing(table, thing));

					// Find the table being referenced by the foreign key
					IDbiThing rightThing = result.Things.OfType<IDbiThing>().Single(thing =>
						IsTableMatchForThing(
							FindTableContainingColumn(
								databaseObjects,
								GetColumnReferencedColumn(
									column,
									databaseObjects)),
							thing));

					// Find the constraint which is doing the referencing
					// TODO move to DatabaseColumn property
					Constraint constraint = databaseObjects.AllConstraints.Single(dbConstraint => dbConstraint.ConstraintType == ConstraintType.ForeignKey && dbConstraint.Columns.Contains(column));

					// Create the relationship

					// If this constraint is not nullable then there will always be a reference
					int leftMinimum = column.IsNullable ? 0 : 1;

					// If there is a unique constraint which has this column as it's only column then this field has to be unique
					int? leftMaximum = null;
					if (table.Constraints.Any(uniqueConstraint => uniqueConstraint.IsUniqueConstraint && uniqueConstraint.Columns.Count() == 1 && uniqueConstraint.Columns.Single() == column))
					{
						// If the referring column is unique there can only be one row for each target row
						leftMaximum = 1;
					}

					DbiRelationship relationship = new DbiRelationship(
						constraint.Name,
						new RelationshipLink(leftThing, leftMinimum, leftMaximum),
						new RelationshipLink(rightThing, 1, 1),
						table.Catalog,
						table.Schema,
						table.Name,
						constraint.Name,
						string.Join(", ", constraint.Columns.Select<Column, string>(relationshipColumn => relationshipColumn.Name)));

					result.AddRelationship(relationship);
				}
			}

			foreach (Routine routine in databaseObjects.Routines)
			{
				DbiItem item = result.Items.Single(dbiItem => IsRoutineMatchForThing(routine, dbiItem as IDbiThing)) as DbiItem;

				string behaviorName = routine.Name;

				// If this behavior's name begins with the item name, strip it off
				if (routine.Name.IndexOf(item.Name) == 0)
				{
					behaviorName = routine.Name.Substring(item.Name.Length);
				}

				Behavior behavior = new Behavior(behaviorName);

				foreach (RoutineParameter routineParameter in routine.Parameters)
				{
					DbiParameter parameter = new DbiParameter(routineParameter.Name, ConvertToItemsType(routineParameter.DataType), NullConstraints.NotApplicable, routineParameter.OrdinalPosition, routineParameter.Mode.ToString());

					behavior.Parameters.Add(parameter);
				}

				item.Behaviors.Add(behavior);
			}

			PopulateAdditionalData(result);

			return result;
		}

		private static bool IsColumnReferencer(Column column, DatabaseObjects.Model model)
		{
			Table containingTable = FindTableContainingColumn(model, column);

			return containingTable.Constraints.Any(dbConstraint => dbConstraint.ConstraintType == ConstraintType.ForeignKey && dbConstraint.Columns.Contains(column));
		}

		private static bool IsColumnReferenced(Column column, DatabaseObjects.Model model)
		{
			return model.AllConstraints.Any(dbConstraint => dbConstraint.ConstraintType == ConstraintType.ForeignKey && dbConstraint.ReferencedColumn == column);
		}

		private static IEnumerable<Constraint> GetColumnReferencingConstraints(Column column, DatabaseObjects.Model model)
		{
			return model.AllConstraints.Where(constraint => constraint.Columns.Contains(column));
		}

		private static NullConstraints GetColumnNullability(Column column)
		{
			return column.IsNullable ? NullConstraints.NotApplicable : NullConstraints.None;
		}

		private static string GetColumnAttributeName(Column column, DatabaseObjects.Model model)
		{
			// TODO maybe just pass in the table name
			Table containingTable = FindTableContainingColumn(model, column);

			if (string.Equals(column.Name, containingTable.Name, System.StringComparison.InvariantCultureIgnoreCase))
			{
				return "Name";
			}

			// If this attribute's name begins with the table name, strip it off
			if (column.Name.IndexOf(containingTable.Name) == 0)
			{
				return column.Name.Substring(containingTable.Name.Length);
			}
			else
			{
				return column.Name;
			}
		}

		private static Table FindTableContainingColumn(DatabaseObjects.Model model, Column column)
		{
			return model.Tables.Single(table => table.Columns.Contains(column));
		}

		private static IType ConvertToItemsType(SqlType sqlType)
		{
			IType type;

			// TODO full list available here: https://msdn.microsoft.com/en-us/library/cc716729%28v=vs.110%29.aspx
			// TODO Xml not implemented
			// TODO Confirm which additional details applicable to the specific datatypes. this has been guessed for now
			switch (sqlType.DataType.ToLower())
			{
				case "bit":
					DbiSystemType<bool> booleanType = new DbiSystemType<bool>();
					booleanType.SqlDataType = sqlType.DataType;
					type = booleanType;
					break;

				case "bigint":
					DbiSystemType<long> int64Type = new DbiSystemType<long>();
					int64Type.SqlDataType = sqlType.DataType;
					int64Type.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					int64Type.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					int64Type.SqlNumericScale = sqlType.NumericScale.Value;
					type = int64Type;
					break;

				case "int":
					DbiSystemType<int> int32Type = new DbiSystemType<int>();
					int32Type.SqlDataType = sqlType.DataType;
					int32Type.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					int32Type.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					int32Type.SqlNumericScale = sqlType.NumericScale.Value;
					type = int32Type;
					break;

				case "smallint":
					DbiSystemType<short> int16Type = new DbiSystemType<short>();
					int16Type.SqlDataType = sqlType.DataType;
					int16Type.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					int16Type.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					int16Type.SqlNumericScale = sqlType.NumericScale.Value;
					type = int16Type;
					break;

				case "tinyint":
					DbiSystemType<byte> byteType = new DbiSystemType<byte>();
					byteType.SqlDataType = sqlType.DataType;
					byteType.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					byteType.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					byteType.SqlNumericScale = sqlType.NumericScale.Value;
					type = byteType;
					break;

				case "char":
				case "nchar":
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					DbiSystemType<string> stringType = new DbiSystemType<string>();
					stringType.SqlDataType = sqlType.DataType;
					stringType.SqlMaxCharacters = sqlType.CharacterMaximumLength;
					stringType.SqlCharacterSet = sqlType.CharacterSetName;
					stringType.SqlCollationName = sqlType.CollationName;
					type = stringType;
					break;

				case "datetime":
				case "datetime2":
				case "smalldatetime":
					DbiSystemType<DateTime> dateTimeType = new DbiSystemType<DateTime>();
					dateTimeType.SqlDataType = sqlType.DataType;
					dateTimeType.SqlDateTimePrecision = sqlType.DateTimePrecision.Value;
					type = dateTimeType;
					break;

				case "datetimeoffset":
					DbiSystemType<DateTimeOffset> dateTimeOffsetType = new DbiSystemType<DateTimeOffset>();
					dateTimeOffsetType.SqlDataType = sqlType.DataType;
					dateTimeOffsetType.SqlDateTimePrecision = sqlType.DateTimePrecision.Value;
					type = dateTimeOffsetType;
					break;

				case "decimal":
				case "money":
				case "numeric":
				case "smallmoney":
					DbiSystemType<decimal> decimalType = new DbiSystemType<decimal>();
					decimalType.SqlDataType = sqlType.DataType;
					decimalType.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					decimalType.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					decimalType.SqlNumericScale = sqlType.NumericScale.Value;
					type = decimalType;
					break;

				case "float":
					DbiSystemType<double> doubleType = new DbiSystemType<double>();
					doubleType.SqlDataType = sqlType.DataType;
					doubleType.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					doubleType.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					doubleType.SqlNumericScale = sqlType.NumericScale;
					type = doubleType;
					break;

				case "real":
					DbiSystemType<float> singleType = new DbiSystemType<float>();
					singleType.SqlDataType = sqlType.DataType;
					singleType.SqlNumericPrecision = sqlType.NumericPrecision.Value;
					singleType.SqlNumericPrecisionRadix = sqlType.NumericPrecisionRadix.Value;
					singleType.SqlNumericScale = sqlType.NumericScale.Value;
					type = singleType;
					break;

				// TODO filestream ?
				case "binary":
				case "image":
				case "rowversion":
				case "timestamp":
				case "varbinary":
					DbiSystemType<byte[]> byteArrayType = new DbiSystemType<byte[]>();
					byteArrayType.SqlDataType = sqlType.DataType;
					type = byteArrayType;
					break;

				case "sql_variant":
					DbiSystemType<object> objectType = new DbiSystemType<object>();
					objectType.SqlDataType = sqlType.DataType;
					type = objectType;
					break;

				case "time":
					DbiSystemType<TimeSpan> timeSpanType = new DbiSystemType<TimeSpan>();
					timeSpanType.SqlDataType = sqlType.DataType;
					type = timeSpanType;
					break;

				case "uniqueidentifier":
					DbiSystemType<Guid> guidType = new DbiSystemType<Guid>();
					guidType.SqlDataType = sqlType.DataType;
					type = guidType;
					break;

				default:
					DbiSystemType<object> defaultType = new DbiSystemType<object>();
					defaultType.SqlDataType = sqlType.DataType;
					type = defaultType;
					break;
			}

			return type;
		}

		private static IEnumerable<IConstraint> GetTypeConstraints(SqlType type)
		{
			List<IConstraint> constraints = new List<IConstraint>();

			switch (type.DataType)
			{
				case "char":
				case "nchar":
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, type.CharacterMaximumLength.Value));
					break;

				default:
					// No constraints available
					break;
			}

			return constraints;
		}

		private static Column GetColumnReferencedColumn(Column column, DatabaseObjects.Model model)
		{
			Constraint foreignKey = FindTableContainingColumn(model, column).Constraints.Single(constraint => constraint.ConstraintType == ConstraintType.ForeignKey && constraint.Columns.Contains(column));
			return foreignKey.ReferencedColumn;
		}

		private static bool IsTableMatchForThing(Table table, IDbiThing thing)
		{
			return thing.SqlCatalog == table.Catalog
				&& thing.SqlSchema == table.Schema
				&& thing.SqlTable == table.Name
				&& string.IsNullOrEmpty(thing.SqlColumns);
		}

		/// <summary>
		/// Populates the additional data such as descriptions.
		/// </summary>
		/// <param name="model">The model.</param>
		private static void PopulateAdditionalData(Model model)
		{
			model.Items["Container"].Description = "A container is a basic tool, consisting of any device creating a partially or fully enclosed space that can be used to contain, store, and transport objects or materials. In commerce, it includes any receptacle or enclosure for holding a product used in packaging and shipping. Things kept inside of a container are protected by being inside of its structure. The term is most frequently applied to devices made from materials that are durable and at least partly rigid.";
			model.Items["Person"].Description = "A person is a being, such as a human, that has certain capacities or attributes constituting personhood, which in turn is defined differently by different authors in different disciplines, and by different cultures in different times and places. In ancient Rome, the word persona (Latin) or prosopon (πρόσωπον; Greek) originally referred to the masks worn by actors on stage. The various masks represented the various personae in the stage play.";
			model.Items["Kitchen"].Description = "A kitchen is a room or part of a room used for cooking and food preparation. In the West, a modern residential kitchen is typically equipped with a stove, a sink with hot and cold running water, a refrigerator and kitchen cabinets arranged according to a modular design. Many households have a microwave oven, a dishwasher and other electric appliances. The main function of a kitchen is cooking or preparing food but it may also be used for dining, food storage, entertaining, dishwashing, laundry.";
			model.Items["Foodstuff"].Description = "Food is any substance consumed to provide nutritional support for the body. It is usually of plant or animal origin, and contains essential nutrients, such as fats, proteins, vitamins, or minerals. The substance is ingested by an organism and assimilated by the organism's cells to provide energy, maintain life, or stimulate growth. Historically, people secured food through two methods: hunting and gathering, and agriculture. Today, most of the food energy required by the ever increasing population of the world is supplied by the food industry.";
		}

		/// <summary>
		/// Determines whether this routine is an operation on the specified thing.
		/// </summary>
		/// <param name="thing">The thing.</param>
		/// <returns>A value indicating whether the thing is based on this table</returns>
		private static bool IsRoutineMatchForThing(Routine routine, IDbiThing thing)
		{
			return routine.Name.IndexOf(thing.Name) == 0
				&& (string)thing.SqlCatalog == routine.Catalog
				&& (string)thing.SqlSchema == routine.Schema;
		}
	}
}
