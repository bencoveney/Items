using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Items;

namespace ItemWeb
{
	public partial class QueryPage : System.Web.UI.Page
	{
		public Thing Thing
		{
			get;
			set;
		}

		public string ThingType
		{
			get;
			set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				string thingName = RouteData.Values["ThingName"].ToString();
				Thing = Global.Model.Things.Single(modelThing => modelThing.Name == thingName);
			}
			catch
			{
				throw new HttpException(404, "Unable to find thing of specified type in model");
			}
		}
	}
}