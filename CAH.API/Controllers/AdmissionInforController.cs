using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.AdmissionInforModelViews;
using CAH.ModelViews.UniversityModelViews;
using CAH.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionInforController : Controller
    {
        private readonly IAdmissionInforService _admissionInforService;

        public AdmissionInforController(IAdmissionInforService admissionInforService)
        {
            _admissionInforService = admissionInforService;
        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateAdmissionInfor(CreateAdInforModelView model)
        {
            string result = await _admissionInforService.CreateAdmissionInforAsync(model);

            return Ok(new { Message = result });
        }


        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<UniversityMV>>> GetAllUniversity(string? uniName,
                                                                                          string? majorName,
                                                                                          int? year,
                                                                                          int pageNumber = 1, int pageSize = 5)
        {
            var result = await _admissionInforService.GetAllAdmissionInforAsync(pageNumber, pageSize, uniName, majorName, year);

            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetAdmissionInforById(string id)
        {
            var result = await _admissionInforService.GetAdInforByIdAsync(id);

            return Ok(new { Message = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAdmissionInfor(string id)
        {
            string result = await _admissionInforService.DeleteAdmissionInforAsync(id);

            return Ok(new { Message = result });
        }
    }
}
