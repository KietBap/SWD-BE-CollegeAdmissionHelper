using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.SubScoreModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectScoreController : ControllerBase
    {
        private readonly ISubjectScoreService _service;
        private readonly ICacheService _cacheService;

        public SubjectScoreController(ISubjectScoreService subjectScoreService, ICacheService cacheService)
        {
            _service = subjectScoreService;
            _cacheService = cacheService;
        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateSubjectScore(CreateSubScoreModelView model)
        {
            string result = await _service.CreateSubScoreAsync(model);
            await _cacheService.RemoveByPrefixAsync("subject_scores"); 
            return Ok(new { Message = result });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteSubjectScore(string id)
        {
            string result = await _service.DeleteSubjectScoreAsync(id);
            await _cacheService.RemoveByPrefixAsync("subject_scores");
            await _cacheService.RemoveByPrefixAsync($"subject_score:{id}");

            return Ok(new { Message = result });
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateSubjectScore(string id, UpdateSubScoreModelView model)
        {
            string result = await _service.UpdateSubjectScoreAsync(id, model);
            await _cacheService.RemoveByPrefixAsync("subject_scores");
            await _cacheService.RemoveByPrefixAsync($"subject_score:{id}");

            return Ok(new { Message = result });
        }

        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<ListSubScoreModelView>>> GetAllSubjectScore(int pageNumber = 1, int pageSize = 5)
        {
            string cacheKey = $"subject_scores:page_{pageNumber}_size_{pageSize}";
            var cachedResult = await _cacheService.GetListAsync<BasePaginatedList<ListSubScoreModelView>>(cacheKey);

            if (cachedResult is not null)
            {
                return Ok(new { Message = cachedResult });
            }

            var result = await _service.GetAllSubjectScoreAsync(pageNumber, pageSize);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "subject_scores");
            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetSubjectScoreById(string id)
        {
            string cacheKey = $"subject_score:{id}";
            var cachedResult = await _cacheService.GetListAsync<object>(cacheKey);

            if (cachedResult is not null)
            {
                return Ok(new { Message = cachedResult });
            }

            var result = await _service.GetSubScoreByIdAsync(id);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), $"subject_score:{id}");
            return Ok(new { Message = result });
        }
    }
}
