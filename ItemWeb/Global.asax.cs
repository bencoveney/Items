using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Items;
using ItemLoader;
using System.Configuration;

namespace ItemWeb
{
	public class Global : System.Web.HttpApplication
	{
		public static Model Model;

		/// <summary>
		/// Handles the Start event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_Start(object sender, EventArgs e)
		{
			RouteTable.Routes.MapPageRoute("Query", "Query/{ThingName}/{*Relationships}", "~/QueryPage.aspx");
			RouteTable.Routes.MapPageRoute("Thing", "Docs/{ThingType}/{ThingName}", "~/ThingPage.aspx");

			DatabaseModel.LoadFromDatabase(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

			Model = DatabaseModel.ConstructModel();

			RequireImplementationDetailSchemas();
		}

		/// <summary>
		/// Handles the Start event of the Session control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Session_Start(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handles the BeginRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handles the AuthenticateRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handles the Error event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_Error(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handles the End event of the Session control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Session_End(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handles the End event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_End(object sender, EventArgs e)
		{
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