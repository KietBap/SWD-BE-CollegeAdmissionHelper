using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.MajorModelViews;
using CAH.ModelViews.UniMajorModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UniMajorController : ControllerBase
    {
        private readonly IUniMajorService _service;
        private readonly IMajorService _majorService;

        public UniMajorController(IUniMajorService uniMajorService, IMajorService majorService)
        {
            _service = uniMajorService;
            _majorService = majorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<string>> CreateUniMajor(CreateUniMajorModelView model)
        {
            string result = await _service.CreateUniMajorAsync(model);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUniMajor(string id)
        {
            string result = await _service.DeleteUniMajorAsync(id);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateUniMajor(string id, UpdateUniMajorModelView model)
        {
            string result = await _service.UpdateUniMajorAsync(id, model);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<ListUniMajorModelView>>> GetAllUniMajor(string? universityName,int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetAllUniMajorAsync(pageNumber, pageSize, universityName);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetUniMajorById(string id)
        {
            var result = await _service.GetUniMajorByIdAsync(id);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("by-university/{universityId}")]
        public async Task<ActionResult<BasePaginatedList<MajorModelView>>> GetAllMajorByUniId(string? universityId, int pageNumber = 1, int pageSize = 5)
        {
            var result = await _majorService.GetAllMajorByUniIdAsync(pageNumber, pageSize, universityId);

            return Ok(new { Message = result });
        }
    }
}
