using CAH.Contract.Services.Interface;
using CAH.ModelViews.AuthModelViews;
using CAH.Repositories.Entity;
using CAH.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly TokenService _tokenService;

		public AuthController(TokenService tokenService, IAppUserService userService) {
            _tokenService = tokenService;
            _appUserService = userService;
        }

        [HttpPost("auth-account")]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Authenticate user
            ApplicationUsers account = await _appUserService.AuthenticateAsync(model);
            if (account == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = await _tokenService.GenerateJwtTokenAsync(account.Id.ToString(), account.UserName);
            return Ok(token);
        }
	}
}

