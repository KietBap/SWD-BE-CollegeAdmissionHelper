using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.SubjectModelViews;
using CAH.ModelViews.UniversityModelViews;
using CAH.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private readonly ISubjectService _service;

		public SubjectController(ISubjectService subjectService)
		{
			_service = subjectService;
		}


		[HttpPost("create")]
		public async Task<ActionResult<string>> CreateSubject([FromQuery] SubjectModelView model)
		{
			string result = await _service.CreateSubjectAsync(model);

			return Ok(new { Message = result });
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<string>> DeleteSubject([FromQuery] string id)
		{
			string result = await _service.DeleteSubjectAsync(id);

			return Ok(new { Message = result });
		}


		[HttpPatch("update")]
		public async Task<ActionResult<string>> UpdateSubject(string id, [FromQuery] SubjectModelView model)
		{
			string result = await _service.UpdateSubjectAsync(id, model);

			return Ok(new { Message = result });
		}


		[HttpGet("all")]
		public async Task<ActionResult<BasePaginatedList<ListSubjectModelView>>> GetAllSubject(int pageNumber = 1, int pageSize = 5)
		{
			var result = await _service.GetAllSubjectAsync(pageNumber, pageSize);

			return Ok(new { Message = result });
		}

		[HttpGet("id")]
		public async Task<ActionResult<Object>> GetSubjectById(string id)
		{
			var result = await _service.GetSubjectByIdAsync(id);

			return Ok(new { Message = result });
		}

	}
}
