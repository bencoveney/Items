using ItemLoader;
using Items;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ItemsApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static Model Model;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DatabaseModel.LoadFromDatabase(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            Model = DatabaseModel.ConstructModel();

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
