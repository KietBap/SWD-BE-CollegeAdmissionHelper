using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.ReviewModelViews;
using CAH.ModelViews.UniMajorModelViews;
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

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateReview([FromQuery] CreateReviewModelView model)
        {
            string result = await _service.CreateReviewAsync(model);

            return Ok(new { Message = result });
        }


        [HttpDelete("delete")]
        public async Task<ActionResult<string>> DeleteReview([FromQuery] string id)
        {
            string result = await _service.DeleteReviewAsync(id);

            return Ok(new { Message = result });
        }


        [HttpPatch("update")]
        public async Task<ActionResult<string>> UpdateReview(string id, [FromQuery] UpdateReviewModelView model)
        {
            string result = await _service.UpdateReviewAsync(id, model);

            return Ok(new { Message = result });
        }

        [HttpGet("all")]
        public async Task<ActionResult<BasePaginatedList<ListReviewModelView>>> GetAllReview(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetAllReviewAsync(pageNumber, pageSize);

            return Ok(new { Message = result });
        }

        [HttpGet("id")]
        public async Task<ActionResult<Object>> GetReviewById(string id)
        {
            var result = await _service.GetReviewByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
