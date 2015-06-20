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