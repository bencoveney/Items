using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Items;
using ItemSerialiser;

namespace ItemLoader
{
    public class Loader
    {
        #region SQL Strings

        private const String ITEM_VALUE_ATTRIBUTES_QUERY = @"
WITH ForeignKeys AS (
    /* Find which columns of tables are foreign key constraints */
    SELECT
        t.name AS ParentTable,
        c.name AS ParentColumn,
        rt.name AS ReferencedTable,
        rc.name AS ReferencedColumn
    FROM
        sys.foreign_key_columns AS fk
        INNER JOIN sys.tables AS t ON fk.parent_object_id = t.object_id
        INNER JOIN sys.columns AS c ON fk.parent_object_id = c.object_id AND fk.parent_column_id = c.column_id
        INNER JOIN sys.tables AS rt ON fk.referenced_object_id = rt.object_id
        INNER JOIN sys.columns AS rc ON fk.referenced_object_id = rc.object_id AND fk.referenced_column_id = rc.column_id
)
SELECT
    COLUMN_NAME,
    IS_NULLABLE,
    DATA_TYPE, 
    CHARACTER_MAXIMUM_LENGTH,
    ReferencedTable,
    ReferencedColumn
FROM
    INFORMATION_SCHEMA.COLUMNS
    LEFT OUTER JOIN ForeignKeys ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = ParentTable AND INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = ParentColumn
WHERE
    Table_Name = @itemName";

        private const String ITEM_COLLECTION_ATTRIBUTES_QUERY = @"
SELECT
    /*fk.name AS Name,*/
    t.name AS TableWithForeignKey
    /*c.name AS ForeignKeyColumn*/
FROM
    sys.foreign_key_columns AS fk
    INNER JOIN sys.tables AS t ON fk.parent_object_id = t.object_id
    INNER JOIN sys.columns AS c ON fk.parent_object_id = c.object_id AND fk.parent_column_id = c.column_id
WHERE
    fk.referenced_object_id = OBJECT_ID(@itemName)
    AND t.name NOT LIKE '%Collection'";

        private const String COLLECTION_REFERENCES_QUERY = @"
SELECT
    c.name AS referencedTable
FROM
    sys.foreign_key_columns AS fk
    INNER JOIN sys.tables AS t ON fk.parent_object_id = t.object_id
    INNER JOIN sys.columns AS c ON fk.parent_object_id = c.object_id
        AND fk.parent_column_id = c.column_id
WHERE
    t.name = @collectionName";

        private const String ITEM_NAMES_QUERY = @"
SELECT
    Table_Name
FROM
    Information_Schema.Tables
WHERE
    Table_Name != 'sysdiagrams'
    AND Table_Name NOT LIKE '%Category'
    AND Table_Name NOT LIKE '%Collection'";

        private const String COLLECTION_NAMES_QUERY = @"
SELECT
    Table_Name
FROM
    Information_Schema.Tables
WHERE
    Table_Name LIKE '%Collection'";

        private const String CATEGORY_NAMES_QUERY = @"
SELECT
    Table_Name
FROM
    Information_Schema.Tables
WHERE
    Table_Name LIKE '%Category'";

        private const String UNIQUE_CONSTRAINTS_QUERY = @"
SELECT
	/*TC.CONSTRAINT_NAME,*/
	/*TC.CONSTRAINT_TYPE,*/
	TC.TABLE_NAME,
	CC.COLUMN_NAME
FROM
	INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CC ON TC.CONSTRAINT_NAME = CC.CONSTRAINT_NAME
WHERE
	TC.CONSTRAINT_TYPE IN ('UNIQUE', 'PRIMARY KEY')
    /* Dont populate unique constraints for categories/collections because they arent built yet */
    AND TC.TABLE_NAME NOT LIKE '%Category%'
    AND TC.TABLE_NAME NOT LIKE '%Collection%'
	/* Only add constraints which reference a single value (for now...) */
	AND TC.CONSTRAINT_NAME IN (
		SELECT
			CONSTRAINT_NAME
		FROM
			INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE
		GROUP BY
			CONSTRAINT_NAME
		HAVING
			SUM(1) <= 1
	)";

        // Should this be done seperately or in the column query
        private const String PRIMARY_IDENTIFIERS_QUERY = @"
SELECT
	/*TC.CONSTRAINT_NAME,*/
	/*TC.CONSTRAINT_TYPE,*/
	TC.TABLE_NAME,
	CC.COLUMN_NAME
FROM
	INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CC ON TC.CONSTRAINT_NAME = CC.CONSTRAINT_NAME
WHERE
	TC.CONSTRAINT_TYPE = 'PRIMARY KEY'";

        #endregion

        public Model Model;

        public Loader()
        {
            Model = new Model();
        }

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Load(SqlConnection connection)
        {
            connection.Open();

            foreach (String itemName in GetItemNames(connection))
            {
                // TODO Make Add only take the string/item?
                Item newItem = new Item(itemName);
                Model.Items.Add(itemName, newItem);
                PopulateAttributesForItemBase(connection, newItem);
            }

            foreach (String categoryName in GetCategoryNames(connection))
            {
                Category newCategory = new Category(categoryName);
                Model.Categories.Add(categoryName, newCategory);
                PopulateAttributesForItemBase(connection, newCategory);
            }

            foreach (String collectionName in GetCollectionNames(connection))
            {
                PopulateCollectionAttributes(connection, collectionName);
            }

            PopulateUniqueConstraints(connection);

            connection.Close();
        }

        /// <summary>
        /// Gets the item names.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public List<String> GetItemNames(SqlConnection connection)
        {
            List<String> itemNames = new List<string>();

            using (SqlCommand command = new SqlCommand(ITEM_NAMES_QUERY, connection))
            {
                using (SqlDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        itemNames.Add(result.GetString(0));
                    }
                }
            }

            return itemNames;
        }

        /// <summary>
        /// Gets the category names.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public List<String> GetCategoryNames(SqlConnection connection)
        {
            List<String> categoryNames = new List<string>();

            using (SqlCommand command = new SqlCommand(CATEGORY_NAMES_QUERY, connection))
            {
                using (SqlDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        categoryNames.Add(result.GetString(0));
                    }
                }
            }

            return categoryNames;
        }

        /// <summary>
        /// Gets the collection names.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public List<String> GetCollectionNames(SqlConnection connection)
        {
            List<String> collectionNames = new List<string>();

            using (SqlCommand command = new SqlCommand(COLLECTION_NAMES_QUERY, connection))
            {
                using (SqlDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        collectionNames.Add(result.GetString(0));
                    }
                }
            }

            return collectionNames;
        }

        /// <summary>
        /// Gets the attributes for item.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public void PopulateAttributesForItemBase(SqlConnection connection, ItemBase item)
        {
            // Value Attributes
            using (SqlCommand command = new SqlCommand(ITEM_VALUE_ATTRIBUTES_QUERY, connection))
            {
                command.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = item.Name;
                using (SqlDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        // Keep the name of the db column safe to decorate the object with
                        String columnName = result.GetString(0);
                        String attributeName = columnName;

                        // Remove the items name from the beginning of the attribute
                        // e.g. KitchenID becomes ID
                        if (attributeName.StartsWith(item.Name))
                            attributeName = attributeName.Substring(item.Name.Length);

                        // Calculate the datatype
                        IType type;
                        // If there is no referenced column
                        if (result.IsDBNull(4))
                        {
                            // TODO Data type size
                            type = SqlTypeToSystemType(result.GetString(2));
                        }
                        else
                        {
                            String referencedItem = result.GetString(4);
                            String referencedColumn = result.GetString(5);

                            if (String.Equals(referencedItem + "ID", referencedColumn, System.StringComparison.InvariantCultureIgnoreCase))
                            {
                                // If we're referencing the primary key of the item type
                                // TODO check if we're referencing the key explicity
                                // TODO perform the same logic on the itemName here that is done elsewhere
                                type = new ItemType(referencedItem);
                                attributeName = referencedItem;
                            }
                            else
                            {
                                // If we're referencing some other attribute of the item
                                throw new NotImplementedException();
                            }
                        }

                        // TODO guessing at empty, could be the other type/both
                        Nullability nullability = result.GetString(1) == "YES" ? Nullability.Empty : Nullability.Invalid;

                        // TODO What about default value?

                        ValueAttribute attribute = new ValueAttribute(attributeName, type, nullability);

                        if (!result.IsDBNull(3))
                            attribute.Constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, result.GetInt32(3) + 1));

                        item.Attributes.Add(attribute);
                    }
                }
            }

            // Collection Attributes
            using (SqlCommand command = new SqlCommand(ITEM_COLLECTION_ATTRIBUTES_QUERY, connection))
            {
                command.Parameters.Add("@itemName", SqlDbType.NVarChar).Value = item.Name;
                using (SqlDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        String collectionName = String.Format("{0}s", result.GetString(0).Replace("Collection", String.Empty));
                        String itemTypeName = result.GetString(0);
                        ItemType itemType = new ItemType(itemTypeName);
                        CollectionAttribute collection = new CollectionAttribute(collectionName, itemType, Nullability.Empty);

                        item.Attributes.Add(collection);
                    }
                }
            }
        }

        public void PopulateCollectionAttributes(SqlConnection connection, String collectionName)
        {
            // Value Attributes
            using (SqlCommand command = new SqlCommand(COLLECTION_REFERENCES_QUERY, connection))
            {
                String[] referencedTables = new String[2];

                command.Parameters.Add("@collectionName", SqlDbType.NVarChar).Value = collectionName;
                using (SqlDataReader result = command.ExecuteReader())
                {
                    for(int i = 0; i < 2; i++)
                    {
                        result.Read();
                        string columnName = result.GetString(0);
                        string itemName = columnName.Substring(0, columnName.Length - 2);
                        referencedTables[i] = itemName;
                    }
                }

                Model.Items[referencedTables[0]].Attributes.Add(new CollectionAttribute(referencedTables[1] + "s", new ItemType(referencedTables[1]), Nullability.Invalid));
                Model.Items[referencedTables[1]].Attributes.Add(new CollectionAttribute(referencedTables[0] + "s", new ItemType(referencedTables[0]), Nullability.Invalid));
            }
        }

        /// <summary>
        /// Writes the reader to console.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void WriteReaderToConsole(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write("{0}, ", reader.GetName(i));
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write("{0}, ", reader.GetValue(i));
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Adds constraints for db unique contraints
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void PopulateUniqueConstraints(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(UNIQUE_CONSTRAINTS_QUERY, connection))
            {
                using (SqlDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        String itemName = result.GetString(0);
                        String columnName = result.GetString(1);

                        // Check whether the column matches the nameID pattern, if it is then strip the ID
                        // this is duplicated code and should be refactored
                        String attributeName = String.Equals(itemName + "ID", columnName, System.StringComparison.InvariantCultureIgnoreCase) ? "ID" : columnName;

                        IAttribute attribute = Model.Items[itemName].Attributes[attributeName];
                        attribute.Constraints.Add(new AttributeConstraint(attribute, CollectionComparison.IsUniqueWithin));
                    }
                }
            }
        }

        public IType SqlTypeToSystemType(String sqlType)
        {
            IType type;

            // TODO full list available here: https://msdn.microsoft.com/en-us/library/cc716729%28v=vs.110%29.aspx
            switch (sqlType.ToLower())
            {
                case "bit":
                    SystemType<Boolean> booleanType = new SystemType<Boolean>();
                    booleanType.SqlDataType = sqlType;
                    type = booleanType;
                    break;

                case "bigint":
                    SystemType<Int64> int64Type = new SystemType<Int64>();
                    int64Type.SqlDataType = sqlType;
                    type = int64Type;
                    break;

                case "int":
                    SystemType<Int32> int32Type = new SystemType<Int32>();
                    int32Type.SqlDataType = sqlType;
                    type = int32Type;
                    break;

                case "smallint":
                    SystemType<Int16> int16Type = new SystemType<Int16>();
                    int16Type.SqlDataType = sqlType;
                    type = int16Type;
                    break;

                case "tinyint":
                    type = new SystemType<Byte>();
                    SystemType<Byte> byteType = new SystemType<Byte>();
                    byteType.SqlDataType = sqlType;
                    type = byteType;
                    break;

                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    type = new SystemType<String>();
                    SystemType<String> stringType = new SystemType<String>();
                    stringType.SqlDataType = sqlType;
                    type = stringType;
                    break;

                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    SystemType<DateTime> dateTimeType = new SystemType<DateTime>();
                    dateTimeType.SqlDataType = sqlType;
                    type = dateTimeType;
                    break;

                case "datetimeoffset":
                    SystemType<DateTimeOffset> dateTimeOffsetType = new SystemType<DateTimeOffset>();
                    dateTimeOffsetType.SqlDataType = sqlType;
                    type = dateTimeOffsetType;
                    break;

                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    SystemType<Decimal> decimalType = new SystemType<Decimal>();
                    decimalType.SqlDataType = sqlType;
                    type = decimalType;
                    break;

                case "float":
                    SystemType<Double> doubleType = new SystemType<Double>();
                    doubleType.SqlDataType = sqlType;
                    type = doubleType;
                    break;

                case "real":
                    SystemType<Single> singleType = new SystemType<Single>();
                    singleType.SqlDataType = sqlType;
                    type = singleType;
                    break;

                // TODO filestream ?
                case "binary":
                case "image":
                case "rowversion":
                case "timestamp":
                case "varbinary":
                    SystemType<Byte[]> byteArrayType = new SystemType<Byte[]>();
                    byteArrayType.SqlDataType = sqlType;
                    type = byteArrayType;
                    break;

                case "sql_variant":
                    SystemType<Object> objectType = new SystemType<Object>();
                    objectType.SqlDataType = sqlType;
                    type = objectType;
                    break;

                case "time":
                    SystemType<TimeSpan> timeSpanType = new SystemType<TimeSpan>();
                    timeSpanType.SqlDataType = sqlType;
                    type = timeSpanType;
                    break;

                case "uniqueidentifier":
                    SystemType<Guid> guidType = new SystemType<Guid>();
                    guidType.SqlDataType = sqlType;
                    type = guidType;
                    break;

                //case "xml":
                //    type = new SystemType<>();
                //    break;

                default:
                    SystemType<Object> defaultType = new SystemType<Object>();
                    defaultType.SqlDataType = sqlType;
                    type = defaultType;
                    break;
            }

            return type;
        }

        enum ColumnType
        {
            Data = 1,
            Item = 2,
            Category = 3
        }
    }
}
