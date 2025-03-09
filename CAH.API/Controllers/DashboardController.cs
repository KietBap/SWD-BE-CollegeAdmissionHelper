using CAH.Contract.Services.Interface;
using CAH.ModelViews.DashBoardModelViews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAH.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("total-users")]
        public async Task<ActionResult<int>> GetTotalUsers()
        {
            int totalUsers = await _dashboardService.GetUserCountAsync();
            return Ok(new { Message = "Success", TotalUsers = totalUsers });
        }

        [HttpGet("total-universitys")]
        public async Task<ActionResult<int>> GetTotalUniversities()
        {
            int totalUniversity = await _dashboardService.GetUniversityCountAsync();
            return Ok(new { Message = "Success", TotalUniversities = totalUniversity });
        }

        [HttpGet("user-login-stats")]
        public async Task<ActionResult<List<UserLoginCountModelView>>> GetUserLoginStats([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var defaultStartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1); // Ngày 1 của tháng hiện tại
            var defaultEndDate = DateTime.UtcNow; // Ngày hiện tại

            var actualStartDate = startDate ?? defaultStartDate;
            var actualEndDate = endDate ?? defaultEndDate;

            var loginStats = await _dashboardService.GetUserLoginCountsAsync(actualStartDate, actualEndDate);
            return Ok(new { Message = "Success", Data = loginStats });
        }

    }
}
