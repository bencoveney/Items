using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemsApi.Controllers
{
    [RoutePrefix("things")]
    public class ThingController : ApiController
    {
        /// <summary>
        /// Gets all available things.
        /// </summary>
        /// <returns>A collection containing all availible things</returns>
        [Route("{thingType}")]
        [HttpGet]
        public IHttpActionResult GetAllThings(string thingType = "all")
        {
            try
            {
                return Ok(GetThingsOfType(thingType));
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        private IEnumerable<Thing> GetThingsOfType(string thingType)
        {
            switch (thingType)
            {
                case "all":
                    return WebApiApplication.Model.Things;
                case "item":
                case "items":
                    return WebApiApplication.Model.Items;
                case "category":
                case "categories":
                    return WebApiApplication.Model.Categories;
                case "relationship":
                case "relationships":
                    return WebApiApplication.Model.Relationships;
                default:
                    throw new ArgumentException("Invalid thing type. Please specify either all, items, categories or relationships.", thingType);
            }
        }
    }
}
