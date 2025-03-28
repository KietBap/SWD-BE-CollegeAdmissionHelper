using CAH.Contract.Services.Interface;
using CAH.ModelViews.DashBoardModelViews;
using CAH.ModelViews.UniMajorModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAH.API.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<ActionResult> GetTotalUsers()
        {
            var users = await _dashboardService.GetUserCountAsync(); // Lấy danh sách user kèm LastLoginTime
            int totalUsers = users.Count; // Đếm số lượng user

            return Ok(new
            {
                Message = "Success",
                TotalUsers = totalUsers,
                Users = users
            });
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
        [HttpGet("top-reviewed-unimajors")]
        public async Task<ActionResult<List<ListUniMajorModelView>>> GetTopReviewedUniMajors([FromQuery] int year, [FromQuery] string period, [FromQuery] int value)
        {
            if (year < 2000 || year > DateTime.UtcNow.Year)
            {
                return BadRequest(new { Message = "Invalid year. Please enter a valid year." });
            }

            if (string.IsNullOrEmpty(period) || (period.ToLower() != "month" && period.ToLower() != "quarter"))
            {
                return BadRequest(new { Message = "Invalid period. Please use 'month' or 'quarter'." });
            }

            if ((period.ToLower() == "month" && (value < 1 || value > 12)) || (period.ToLower() == "quarter" && (value < 1 || value > 4)))
            {
                return BadRequest(new { Message = "Invalid value. Please enter a valid month (1-12) or quarter (1-4)." });
            }

            var topReviewedUniMajors = await _dashboardService.GetTopReviewedUniMajors(year, period, value);
            return Ok(new { Message = "Success", Data = topReviewedUniMajors });
        }
    }
}
