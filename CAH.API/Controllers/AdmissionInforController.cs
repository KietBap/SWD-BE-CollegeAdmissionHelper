using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.AdmissionInforModelViews;
using CAH.ModelViews.UniversityModelViews;
using CAH.Services.Service;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost()]
        public async Task<ActionResult<string>> CreateAdmissionInfor(AdmissionInforModelView model)
        {
            string result = await _admissionInforService.CreateAdmissionInforAsync(model);

            return Ok(new { Message = result });
        }


        [HttpGet()]
        public async Task<ActionResult<BasePaginatedList<UniversityMV>>> GetAllUniversity([FromQuery] string? uniName,
                                                                                          [FromQuery] string? majorName,
                                                                                          int pageNumber = 1, int pageSize = 5)
        {
            var result = await _admissionInforService.GetAllAdmissionInforAsync(pageNumber, pageSize, uniName, majorName);

            return Ok(new { Message = result });
        }
    }
}
