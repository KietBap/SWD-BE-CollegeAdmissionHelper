using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.UniversityModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	//[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class UniversityController : ControllerBase
	{
		private readonly IUniversityService _universityService;
		private readonly ICacheService _cacheService;

		public UniversityController(IUniversityService universityService, ICacheService cacheService)
		{
			_universityService = universityService;
			_cacheService = cacheService;
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<ActionResult<string>> CreateUniversity(CreateUniversityModelView model)
		{
            string result = await _universityService.CreateUniversityAsync(model);
            //await _cacheService.RemoveByPrefixAsync("universities"); 

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
		public async Task<ActionResult<string>> DeleteUniversity(string id)
		{
            string result = await _universityService.DeleteUniversityAsync(id);
            //await _cacheService.RemoveByPrefixAsync("universities");
            //await _cacheService.RemoveByPrefixAsync($"university:{id}");

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
		public async Task<ActionResult<string>> UpdateUniversity(string id, CreateUniversityModelView model)
		{
            string result = await _universityService.UpdateUniversityAsync(id, model);
            //await _cacheService.RemoveByPrefixAsync("universities");
            //await _cacheService.RemoveByPrefixAsync($"university:{id}"); 

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<UniversityMV>>> GetAllUniversity(string? uniCode,
																						  string? type,
																						  string? location,
																						  int pageNumber = 1, int pageSize = 5)
		{
            //string cacheKey = $"universities:page_{pageNumber}_size_{pageSize}_code_{uniCode}_type_{type}_location_{location}";
            //var cachedResult = await _cacheService.GetListAsync<BasePaginatedList<UniversityMV>>(cacheKey);

            //if (cachedResult is not null)
            //{
            //    return Ok(new { Message = cachedResult });
            //}

            var result = await _universityService.GetAllUniversityAsync(pageNumber, pageSize, uniCode, type, location);

            //await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "universities");
            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityModelView>> GetUniversityById(string id)
		{
            //string cacheKey = $"university:{id}";
            //var cachedResult = await _cacheService.GetListAsync<object>(cacheKey);

            //if (cachedResult is not null)
            //{
            //    return Ok(new { Message = cachedResult });
            //}

            var result = await _universityService.GetUniversityByIdAsync(id);

            //await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), $"university:{id}");
            return Ok(new { Message = result });
        }
	}
}
