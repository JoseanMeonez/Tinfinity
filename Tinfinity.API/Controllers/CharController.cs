using Microsoft.AspNetCore.Mvc;
using static Tinfinity.Application.Features.User.Queries.GetUser;

namespace Tinfinity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharController : ControllerBase
	{
		[HttpGet("GetUserBasicInfo/{name}")]
		public async Task<IActionResult> GetUserBasicInfo(string name)
		{
			var res = await GetUserBasicData(name);

			if (res.Completed)
				return Ok(res);
			else
				return BadRequest(res);
		}
	}
}
