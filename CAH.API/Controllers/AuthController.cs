using CAH.Contract.Repositories.Entity;
using CAH.Contract.Services.Interface;
using CAH.ModelViews.AuthModelViews;
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
            User account = await _appUserService.AuthenticateAsync(model);
            if (account == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = await _tokenService.GenerateJwtTokenAsync(account.Id.ToString(), account.UserName);
            return Ok(token);
        }

        [HttpPost("login-google")]
        public async Task<IActionResult> LoginGoogle(GoogleToken model)
        {
            var token = await _appUserService.LoginGoogleAsync(model);

            return Ok(token);
        }
    }
}

