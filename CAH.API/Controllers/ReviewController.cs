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

        public ReviewController(IReviewService reviewService)
        {
            _service = reviewService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateReview(CreateReviewModelView model)
        {
            string result = await _service.CreateReviewAsync(model);

            return Ok(new { Message = result });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteReview(string id)
        {
            string result = await _service.DeleteReviewAsync(id);

            return Ok(new { Message = result });
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateReview(string id, UpdateReviewModelView model)
        {
            string result = await _service.UpdateReviewAsync(id, model);

            return Ok(new { Message = result });
        }

        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<ListReviewModelView>>> GetAllReview(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetAllReviewAsync(pageNumber, pageSize);

            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetReviewById(string id)
        {
            var result = await _service.GetReviewByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
