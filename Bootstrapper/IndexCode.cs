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
        // TODO sort out prettify methods and fix their names. should they be tostring/todescription methods on the object?

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

            return String.Format("This attribute is of unknown type ({0})", typeType.FullName);
        }

        public String PrettifyAttributeConstraint(CollectionComparison constraint)
        {
            switch (constraint)
            {
                case CollectionComparison.DoesntExistIn:
                    return "not exist within";
                case CollectionComparison.ExistsIn:
                    return "exist within";
                case CollectionComparison.IsUniqueWithin:
                    return "be unique within";
                default:
                    return "ERROR";
            }
        }

        public String PrettifyNumericConstraint(NumericValueComparison comparison)
        {
            switch (comparison)
            {
                case NumericValueComparison.EqualTo:
                    return "be equal to";
                case NumericValueComparison.NotEqualTo:
                    return "not be equal to";
                case NumericValueComparison.EvenlyDivisibleBy:
                    return "be evenly divisible by";
                case NumericValueComparison.NotEvenlyDivisibleBy:
                    return "not be evenly divisible by";
                case NumericValueComparison.GreaterThan:
                    return "be greater than";
                case NumericValueComparison.GreaterThanOrEqualTo:
                    return "be greater than or equal to";
                case NumericValueComparison.LessThan:
                    return "be less than";
                case NumericValueComparison.LessThanOrEqualTo:
                    return "be less than or equal to";
                default:
                    return "ERROR";
            }
        }

        public String PrettifyLengthConstraint(LengthComparison comparison)
        {
            switch (comparison)
            {
                case LengthComparison.Exactly:
                    return "be exactly";
                case LengthComparison.LongerThan:
                    return "be longer than";
                case LengthComparison.ShorterThan:
                    return "be shorter than";
                default:
                    return "ERROR";
            }
        }
    }
}
