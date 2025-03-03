using AutoMapper;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.SubScoreModelViews;
using CAH.ModelViews.UserModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BasePaginatedList<ListUserModelView>>> GetAllUser([FromQuery] string? id,
                                                                                        [FromQuery] string? email,
                                                                                        [FromQuery] string? phoneNumber, 
                                                                                        int pageNumber = 1, int pageSize = 5)
        {
            var result = await _userService.GetAllUserAsync(pageNumber, pageSize, id, email, phoneNumber);

            return Ok(new { Message = result });
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<BasePaginatedList<ListUserModelView>>> GetUserById([FromQuery] string? id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
