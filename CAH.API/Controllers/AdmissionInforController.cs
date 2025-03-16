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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<string>> CreateAdmissionInfor(CreateAdInforModelView model)
        {
            string result = await _admissionInforService.CreateAdmissionInforAsync(model);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<BasePaginatedList<UniversityMV>>> GetAllAdmissionInfor(string? uniName,
                                                                                          string? majorName,
                                                                                          int? year,
                                                                                          int pageNumber = 1, int pageSize = 5)
        {
            var result = await _admissionInforService.GetAllAdmissionInforAsync(pageNumber, pageSize, uniName, majorName, year);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetAdmissionInforById(string id)
        {
            var result = await _admissionInforService.GetAdInforByIdAsync(id);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAdmissionInfor(string id)
        {
            string result = await _admissionInforService.DeleteAdmissionInforAsync(id);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("mark-wishlist")]
        public async Task<ActionResult<string>> MarkWishlist(string admissionInforId)
        {
            string result = await _admissionInforService.MarkWishlistAsync(admissionInforId);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("unmark-wishlist")]
        public async Task<ActionResult<string>> UnMarkWishlist(string admissionInforId)
        {
            string result = await _admissionInforService.UnMarkWishlistAsync(admissionInforId);

            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("wishlist")]
        public async Task<ActionResult<BasePaginatedList<UniversityMV>>> GetWishlist(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _admissionInforService.GetWishlistAsync(pageNumber, pageSize);

            return Ok(new { Message = result });
        }
    }
}
