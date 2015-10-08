namespace ItemsApi.Controllers
{
	using Items;
	using ItemsApi;
	using ItemsApi.Models;
	using System.Collections.Generic;
	using System.Web.Http;
	using System.Linq;
	using System.Net.Http;

	[RoutePrefix("api/selectors")]
	public class ItemSelectorController : ApiController
	{
		[Route("data")]
		[HttpPost]
		public IHttpActionResult GetDataPost([FromBody]ItemSelector selection)
		{
			if (selection == null)
				return BadRequest("selection must be provided");

			return Ok("Bello!");
		}
	}
}
