using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.AdmissionMethodMVs;
using CAH.ModelViews.UniversityModelViews;
using CAH.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class AdmissionMethodController : ControllerBase
	{
		public readonly IAdmissionMethodService _service;

        public AdmissionMethodController(IAdmissionMethodService admissionMethodService)
        {
            _service = admissionMethodService;
        }

		[HttpPost("create")]
		public async Task<ActionResult<string>> CreateAdmissionMethod([FromQuery] AdmissionMethodMV model)
		{
			string result = await _service.CreateAdmissionMethodAsync(model);

			return Ok(new { Message = result });
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<string>> DeleteAdmissionMethod([FromQuery] string id)
		{
			string result = await _service.DeleteAdmissionMethodAsync(id);

			return Ok(new { Message = result });
		}


		[HttpPatch("update")]
		public async Task<ActionResult<string>> UpdateAdmissionMethod(string id, [FromQuery] AdmissionMethodMV model)
		{
			string result = await _service.UpdateAdmissionMethodAsync(id, model);

			return Ok(new { Message = result });
		}

		[HttpGet("all")]
		public async Task<ActionResult<BasePaginatedList<ListAdMethodMV>>> GetAllAdMethod(int pageNumber = 1, int pageSize = 5)
		{
			var result = await _service.GetAllAdMethodAsync(pageNumber, pageSize);

			return Ok(new { Message = result });
		}

		[HttpGet("id")]
		public async Task<ActionResult<Object>> GetAdMethodById(string id)
		{
			var result = await _service.GetAdMethodByIdAsync(id);

			return Ok(new { Message = result });
		}
	}
}
