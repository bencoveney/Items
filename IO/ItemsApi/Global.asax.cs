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

			Model = Factory.ConstructModel(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
		}
	}
}
