using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.MajorModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	//[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class MajorController : ControllerBase
	{
		private readonly IMajorService _majorService;
		private readonly ICacheService _cacheService;

		public MajorController(IMajorService majorService, ICacheService cacheService)
		{
			_majorService = majorService;
            _cacheService = cacheService;
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<ActionResult<string>> CreateMajor(CreateMajorModelView model)
		{
			string result = await _majorService.CreateMajorAsync(model);

            //await _cacheService.RemoveByPrefixAsync("MajorList");

            return Ok(new { Message = result });
		}

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
		public async Task<ActionResult<string>> DeleteMajor(string id)
		{
            string result = await _majorService.DeleteMajorAsync(id);

            //await _cacheService.RemoveByPrefixAsync("MajorList");
            //await _cacheService.RemoveByPrefixAsync($"Major_{id}");

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
		public async Task<ActionResult<string>> UpdateMajor(string id, CreateMajorModelView model)
		{
            string result = await _majorService.UpdateMajorAsync(id, model);

            //await _cacheService.RemoveByPrefixAsync("MajorList");
            //await _cacheService.RemoveByPrefixAsync($"Major_{id}");

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<MajorModelView>>> GetAllMajor(
			int pageNumber = 1,
			int pageSize = 5,
			string? majorName = null,
			string? universityName = null)
        {
            //var cacheKey = $"MajorList_{pageNumber}_{pageSize}_{majorName}_{universityName}";
            //var cachedData = await _cacheService.GetListAsync<BasePaginatedList<MajorModelView>>(cacheKey);

            //if (cachedData != null)
            //{
            //    return Ok(new { Message = cachedData });
            //}

            var result = await _majorService.GetAllMajorAsync(pageNumber, pageSize, majorName, universityName);

            //if (result != null)
            //{
            //    await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "MajorList");
            //}

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("by-university/{universityId}")]
        public async Task<ActionResult<BasePaginatedList<MajorModelView>>> GetAllMajorByUniId(string? universityId, int pageNumber = 1, int pageSize = 5)
        {
            //string cacheKey = $"majors:{universityId}:page{pageNumber}:size{pageSize}";
            //var cachedData = await _cacheService.GetListAsync<BasePaginatedList<MajorModelView>>(cacheKey);

            //if (cachedData != null)
            //{
            //    return Ok(new { Message = cachedData, Cache = true });
            //}

            var result = await _majorService.GetAllMajorByUniIdAsync(pageNumber, pageSize, universityId);

            //await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(15), "majors");

            return Ok(new { Message = result, Cache = false });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
		public async Task<ActionResult<Object>> GetMajorById(string id)
		{
            //var cacheKey = $"Major_{id}";
            //var cachedData = await _cacheService.GetListAsync<MajorModelView>(cacheKey);

            //if (cachedData != null)
            //{
            //    return Ok(new { Message = cachedData });
            //}

            var result = await _majorService.GetMajorByIdAsync(id);

            //if (result != null)
            //{
            //    await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "Major");
            //}

            return Ok(new { Message = result });
        }

	}
}
