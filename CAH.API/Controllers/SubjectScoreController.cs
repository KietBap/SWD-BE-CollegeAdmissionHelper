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

        public SubjectScoreController(ISubjectScoreService subjectScoreService)
        {
            _service = subjectScoreService;
        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateSubjectScore([FromBody] CreateSubScoreModelView model)
        {
            string result = await _service.CreateSubScoreAsync(model);

            return Ok(new { Message = result });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteSubjectScore([FromBody] string id)
        {
            string result = await _service.DeleteSubjectScoreAsync(id);

            return Ok(new { Message = result });
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateSubjectScore(string id, [FromBody] UpdateSubScoreModelView model)
        {
            string result = await _service.UpdateSubjectScoreAsync(id, model);

            return Ok(new { Message = result });
        }

        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<ListSubScoreModelView>>> GetAllSubjectScore(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetAllSubjectScoreAsync(pageNumber, pageSize);

            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetSubjectScoreById(string id)
        {
            var result = await _service.GetSubScoreByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
