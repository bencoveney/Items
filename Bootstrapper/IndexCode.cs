using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemLoader;
using System.Data.SqlClient;
using Items;

namespace Bootstrapper
{
    public partial class Index
    {
        private const String CONNECTION_STRING = @"Data Source=BENSDESKTOP\SQLEXPRESS;Initial Catalog=ItemsDB;Integrated Security=True";

        private Model model;

        public Index()
        {
            Loader loader = new Loader();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                loader.Load(connection);
            }

            model = loader.Model;
        }

        private String PrettifyType(IType type)
        {
            Type typeType = type.GetType();

            if (typeType.IsGenericType && typeType.GetGenericTypeDefinition() == typeof(SystemType<>))
            {
                return String.Format("The attribute stores {0} data.", typeType.GetGenericArguments()[0].Name);
            }

            if (typeType == typeof(ItemType))
            {
                return String.Format("This attribute stores {0} items.", type.Name);
            }

            if (typeType == typeof(CategoryType))
            {
                return String.Format("This attribute is a {0} categorisation.", type.Name);
            }

            return "<UnknownType>";
        }

        public String PrettifyAttributeConstraint(CollectionComparison constraint)
        {
            switch (constraint)
            {
                case CollectionComparison.DoesntExistIn:
                    return "not exist within";
                    break;
                case CollectionComparison.ExistsIn:
                    return "exist within";
                    break;
                case CollectionComparison.IsUniqueWithin:
                    return "be unique within";
                    break;
                default:
                    return "ERROR within";
                    break;
            }
        }
    }
}
