using Microsoft.AspNetCore.Mvc;
using static Tinfinity.Application.Features.User.Queries.GetCharacter;

namespace Tinfinity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharController : ControllerBase
	{
		[HttpGet("GetUserBasicInfo/{name}")]
		public async Task<IActionResult> GetUserBasicInfo(string name)
		{
			var res = await GetCharacterData(name);

			if (res.Completed)
				return Ok(res);
			else
				return BadRequest(res);
		}
	}
}
