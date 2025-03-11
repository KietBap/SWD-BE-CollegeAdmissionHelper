using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.ReviewModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;
        private readonly ICacheService _cacheService;   

        public ReviewController(IReviewService reviewService, ICacheService cacheService)
        {
            _service = reviewService;
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateReview(CreateReviewModelView model)
        {
            string result = await _service.CreateReviewAsync(model);
            await _cacheService.RemoveByPrefixAsync("reviews");

            return Ok(new { Message = result });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteReview(string id)
        {
            string result = await _service.DeleteReviewAsync(id);
            await _cacheService.RemoveByPrefixAsync("reviews");
            await _cacheService.RemoveByPrefixAsync($"review:{id}");

            return Ok(new { Message = result });
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateReview(string id, UpdateReviewModelView model)
        {
            string result = await _service.UpdateReviewAsync(id, model);
            await _cacheService.RemoveByPrefixAsync("reviews"); 
            await _cacheService.RemoveByPrefixAsync($"review:{id}");

            return Ok(new { Message = result });
        }

        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<ListReviewModelView>>> GetAllReview(int pageNumber = 1, int pageSize = 5)
        {
            string cacheKey = $"reviews:page_{pageNumber}_size_{pageSize}";
            var cachedResult = await _cacheService.GetListAsync<BasePaginatedList<ListReviewModelView>>(cacheKey);

            if (cachedResult is not null)
            {
                return Ok(new { Message = cachedResult }); // Trả về dữ liệu từ cache
            }

            var result = await _service.GetAllReviewAsync(pageNumber, pageSize);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "reviews"); // Lưu vào cache
            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetReviewById(string id)
        {
            string cacheKey = $"review:{id}";
            var cachedResult = await _cacheService.GetListAsync<object>(cacheKey);

            if (cachedResult is not null)
            {
                return Ok(new { Message = cachedResult }); // Trả về dữ liệu từ cache
            }

            var result = await _service.GetReviewByIdAsync(id);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), $"review:{id}"); // Lưu vào cache
            return Ok(new { Message = result });
        }
    }
}
