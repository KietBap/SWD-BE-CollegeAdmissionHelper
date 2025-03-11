using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.SubjectModelViews;
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
		private readonly ICacheService _cacheService;

		public SubjectController(ISubjectService subjectService, ICacheService cacheService)
		{
			_service = subjectService;
			_cacheService = cacheService;
		}


		[HttpPost]
		public async Task<ActionResult<string>> CreateSubject(SubjectModelView model)
		{
            string result = await _service.CreateSubjectAsync(model);
            await _cacheService.RemoveByPrefixAsync("subjects"); 
            return Ok(new { Message = result });
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<string>> DeleteSubject(string id)
		{
            string result = await _service.DeleteSubjectAsync(id);
            await _cacheService.RemoveByPrefixAsync("subjects");
            await _cacheService.RemoveByPrefixAsync($"subject:{id}");
            return Ok(new { Message = result });
        }


		[HttpPatch("{id}")]
		public async Task<ActionResult<string>> UpdateSubject(string id, SubjectModelView model)
		{
            string result = await _service.UpdateSubjectAsync(id, model);
            await _cacheService.RemoveByPrefixAsync("subjects");
            await _cacheService.RemoveByPrefixAsync($"subject:{id}");
            return Ok(new { Message = result });
        }


		[HttpGet]
		public async Task<ActionResult<BasePaginatedList<ListSubjectModelView>>> GetAllSubject(int pageNumber = 1, int pageSize = 5)
		{
            string cacheKey = $"subjects:page{pageNumber}:size{pageSize}";
            var cachedData = await _cacheService.GetListAsync<BasePaginatedList<ListSubjectModelView>>(cacheKey);

            if (cachedData != null)
            {
                return Ok(new { Message = cachedData, Cache = true });
            }

            var result = await _service.GetAllSubjectAsync(pageNumber, pageSize);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(15), "subjects");

            return Ok(new { Message = result, Cache = false });
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<Object>> GetSubjectById(string id)
		{
            string cacheKey = $"subject:{id}";
            var cachedData = await _cacheService.GetListAsync<ListSubjectModelView>(cacheKey);

            if (cachedData != null)
            {
                return Ok(new { Message = cachedData, Cache = true });
            }

            var result = await _service.GetSubjectByIdAsync(id);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "subjects");

            return Ok(new { Message = result, Cache = false });
        }

	}
}
