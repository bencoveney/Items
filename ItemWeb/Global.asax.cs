using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Items;
using ItemLoader;

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
			RouteTable.Routes.MapPageRoute("Thing", "{ThingType}/{ThingName}", "~/ThingPage.aspx");

			DatabaseModel.LoadFromDatabase(@"Data Source=BENSDESKTOP\SQLEXPRESS;Initial Catalog=ItemsDB;Integrated Security=True");
			Model = DatabaseModel.ConstructModel();
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
	}
}