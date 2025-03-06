using CAH.Contract.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
       public readonly IDashboardService _service;
        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("total-users")]
        public async Task<IActionResult> GetTotalUsers()
        {
            var totalUsers = await _service.GetUserCountAsync();
            return Ok(totalUsers);
        }

        [HttpGet("total-universitys")]
        public async Task<IActionResult> GetTotalInstructors()
        {
            var totalUniversity = await _service.GetUniversityCountAsync();
            return Ok(totalUniversity);
        }
    }
}
