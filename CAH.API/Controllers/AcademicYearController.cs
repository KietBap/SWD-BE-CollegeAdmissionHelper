using CAH.Core;
using CAH.ModelViews.AcademicYearModelViews;
using CAH.ModelViews.SubjectModelViews;
using CAH.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly AcademicYearService _service;

        public AcademicYearController(AcademicYearService academicYearService)
        {
            _service = academicYearService;
        }


        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateAcademicYear([FromQuery] AcademicYearModelView model)
        {
            string result = await _service.CreateAcademicYearAsync(model);

            return Ok(new { Message = result });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<string>> DeleteAcademicYear([FromQuery] string id)
        {
            string result = await _service.DeleteAcademicYearAsync(id);

            return Ok(new { Message = result });
        }


        [HttpPatch("update")]
        public async Task<ActionResult<string>> UpdateAcademicYear(string id, [FromQuery] AcademicYearModelView model)
        {
            string result = await _service.UpdateAcademicYearAsync(id, model);

            return Ok(new { Message = result });
        }


        [HttpGet("all")]
        public async Task<ActionResult<BasePaginatedList<ListSubjectModelView>>> GetAllAcademicYear(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetAllAcademicYearAsync(pageNumber, pageSize);

            return Ok(new { Message = result });
        }

        [HttpGet("id")]
        public async Task<ActionResult<Object>> GetAcademicYearById(string id)
        {
            var result = await _service.GetAcademicYearByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
