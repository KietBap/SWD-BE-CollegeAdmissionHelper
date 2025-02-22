using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.UniversityModelViews;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UniversityController : ControllerBase
	{
		private readonly IUniversityService _universityService;

		public UniversityController(IUniversityService universityService)
		{
			_universityService = universityService;
		}


		[HttpPost("create")]
		public async Task<ActionResult<string>> CreateUniversity([FromQuery] UniversityModelView model)
		{
			string result = await _universityService.CreateUniversityAsync(model);

			return Ok(new { Message = result });
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<string>> DeleteUniversity([FromQuery] string id, string userId)
		{
			string result = await _universityService.DeleteUniversityAsync(id, userId);

			return Ok(new { Message = result });
		}


		[HttpPatch("update")]
		public async Task<ActionResult<string>> UpdateUniversity(string id, [FromQuery] UniversityModelView model)
		{
			string result = await _universityService.UpdateUniversityAsync(id, model);

			return Ok(new { Message = result });
		}


		[HttpGet("all")]
		public async Task<ActionResult<BasePaginatedList<UniversityMV>>> GetAllUniversity(int pageNumber = 1, int pageSize = 5)
		{
			var result = await _universityService.GetAllUniversityAsync(pageNumber, pageSize);

			return Ok(new { Message = result });
		}

		[HttpGet("id")]
		public async Task<ActionResult<Object>> GetMajorById(string id)
		{
			var result = await _universityService.GetUniversityByIdAsync(id);

			return Ok(new { Message = result });
		}
	}
}
