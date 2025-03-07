using CAH.Contract.Services.Interface;
using CAH.Core;
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

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BasePaginatedList<ListUserModelView>>> GetAllUser(string? id,
                                                                                        string? email,
                                                                                        string? phoneNumber, 
                                                                                        int pageNumber = 1, int pageSize = 5)
        {
            var result = await _userService.GetAllUserAsync(pageNumber, pageSize, id, email, phoneNumber);

            return Ok(new { Message = result });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<BasePaginatedList<ListUserModelView>>> GetUserById(string? id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return Ok(new { Message = result });
        }
    }
}
