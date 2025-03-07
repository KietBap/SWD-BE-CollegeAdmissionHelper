using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.MajorModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class MajorController : ControllerBase
	{
		private readonly IMajorService _majorService;

		public MajorController(IMajorService majorService)
		{
			_majorService = majorService;
		}


		[HttpPost]
		public async Task<ActionResult<string>> CreateMajor(CreateMajorModelView model)
		{
			string result = await _majorService.CreateMajorAsync(model);

			return Ok(new { Message = result });
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<string>> DeleteMajor(string id)
		{
			string result = await _majorService.DeleteMajorAsync(id);

			return Ok(new { Message = result });
		}

		[HttpPatch("{id}")]
		public async Task<ActionResult<string>> UpdateMajor(string id, CreateMajorModelView model)
		{
			string result = await _majorService.UpdateMajorAsync(id, model);

			return Ok(new { Message = result });
		}

        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<MajorModelView>>> GetAllMajor(
			int pageNumber = 1,
			int pageSize = 5,
			string? majorName = null,
			string? universityName = null)
        {
            var result = await _majorService.GetAllMajorAsync(pageNumber, pageSize, majorName, universityName);
            return Ok(new { Message = result });
        }

        [HttpGet("by-university/{universityId}")]
        public async Task<ActionResult<BasePaginatedList<MajorModelView>>> GetAllMajorByUniId(string? universityId, int pageNumber = 1, int pageSize = 5)
        {
            var result = await _majorService.GetAllMajorByUniIdAsync(pageNumber, pageSize, universityId);

            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
		public async Task<ActionResult<Object>> GetMajorById(string id)
		{
			var result = await _majorService.GetMajorByIdAsync(id);

			return Ok(new { Message = result });
		}

	}
}
