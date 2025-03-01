using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.MajorModelViews;
using CAH.ModelViews.UniMajorModelViews;
using CAH.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UniMajorController : ControllerBase
    {
        private readonly IUniMajorService _service;

        public UniMajorController(IUniMajorService uniMajorService)
        {
            _service = uniMajorService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateUniMajor([FromQuery] CreateUniMajorModelView model)
        {
            string result = await _service.CreateUniMajorAsync(model);

            return Ok(new { Message = result });
        }


        [HttpDelete("delete")]
        public async Task<ActionResult<string>> DeleteUniMajor([FromQuery] string id)
        {
            string result = await _service.DeleteUniMajorAsync(id);

            return Ok(new { Message = result });
        }


        [HttpPatch("update")]
        public async Task<ActionResult<string>> UpdateUniMajor(string id, [FromQuery] UpdateUniMajorModelView model)
        {
            string result = await _service.UpdateUniMajorAsync(id, model);

            return Ok(new { Message = result });
        }

        [HttpGet("all")]
        public async Task<ActionResult<BasePaginatedList<ListUniMajorModelView>>> GetAllUniMajor(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetAllUniMajorAsync(pageNumber, pageSize);

            return Ok(new { Message = result });
        }

        [HttpGet("id")]
        public async Task<ActionResult<Object>> GetUniMajorById(string id)
        {
            var result = await _service.GetUniMajorByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
