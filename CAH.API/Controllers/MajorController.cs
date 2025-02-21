using CAH.Contract.Services.Interface;
using CAH.ModelViews.MajorModelViews;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MajorController : ControllerBase
	{
		private readonly IMajorService _majorService;

		public MajorController(IMajorService majorService)
		{
			_majorService = majorService;
		}


		[HttpPost("create")]
		public async Task<ActionResult<string>> CreateMajor([FromQuery] CreateMajorModelView model)
		{
			string result = await _majorService.CreateMajorAsync(model);

			return Ok(new { Message = result });
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<string>> DeleteMajor([FromQuery] string id, string userId)
		{
			string result = await _majorService.DeleteMajorAsync(id, userId);

			return Ok(new { Message = result });
		}

		[HttpPatch("update")]
		public async Task<ActionResult<string>> UpdateMajor([FromQuery] string id,[FromQuery] CreateMajorModelView model)
		{
			string result = await _majorService.UpdateMajorAsync(id, model);

			return Ok(new { Message = result });
		}
	}
}
