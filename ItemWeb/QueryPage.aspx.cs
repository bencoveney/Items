using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Items;
using System.Data.SqlClient;
using ItemSelector;
using System.Configuration;

namespace ItemWeb
{
	public partial class QueryPage : System.Web.UI.Page
	{
		public SqlDataReader reader;

		public Thing Thing
		{
			get;
			set;
		}

		public IEnumerable<Relationship> Relationships
		{
			get;
			set;
		}

		public string executedSql;

		public string ThingType
		{
			get;
			set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			string thingName = RouteData.Values["ThingName"].ToString();
			Thing = Global.Model.Things.Single(modelThing => modelThing.Name == thingName);

			if(RouteData.Values["Relationships"] != null)
			{
				Relationships = RouteData.Values["Relationships"].ToString().Split('/').Select<string, Relationship>(relationship => Global.Model.Relationships[relationship]);
			}
			else
			{
				Relationships = new List<Relationship>();
			}
		}

		public string GetPageURL()
		{
			return string.Format(
				"http://{0}:{1}{2}",
				Request.ServerVariables["SERVER_NAME"],
				Request.ServerVariables["SERVER_PORT"],
				Request.ServerVariables["URL"]);
		}

		public string GetSiteURL()
		{
			return string.Format(
				"http://{0}:{1}",
				Request.ServerVariables["SERVER_NAME"],
				Request.ServerVariables["SERVER_PORT"]);
		}

		public void ProduceTable()
		{
			ModelQuery query = new ModelQuery(Thing as Item);

			foreach (Relationship relationship in Relationships)
			{
				query.JoinThroughRelationship(relationship);
			}

			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			connection.Open();
			executedSql = query.GetSql();
			SqlCommand command = new SqlCommand(executedSql, connection);
			this.reader = command.ExecuteReader();

			Response.Write(@"<table class=""table table-striped table-hover"">");
			ProduceTableHead();
			ProduceTableBody();
			Response.Write("</table>");

			connection.Close();
		}

		public void ProduceTableHead()
		{
			Response.Write("<thead>");
			Response.Write("<tr>");
			for(int i = 0; i < reader.FieldCount; i++)
			{
				Response.Write("<th>");
				Response.Write(reader.GetName(i));
				Response.Write("</th>");
			}
			Response.Write("</tr>");
			Response.Write("</thead>");
		}

		public void ProduceTableBody()
		{
			Response.Write("<tbody>");
			while(reader.Read())
			{
				Response.Write("<tr>");
				for (int i = 0; i < reader.FieldCount; i++)
				{
					Response.Write("<td>");
					Response.Write(reader.GetValue(i));
					Response.Write("</td>");
				}
				Response.Write("</tr>");
			}
			Response.Write("</tbody>");
		}
	}
}