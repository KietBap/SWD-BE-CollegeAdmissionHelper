using CAH.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiService _service;

        public GeminiController(GeminiService geminiService)
        {
            _service = geminiService;
        }

        [HttpGet]
        public async Task<ActionResult<Object>> GenerateText(string prompt)
        {
            var result = await _service.GenerateTextAsync(prompt);

            return Ok(new { Message = result });
        }
    }
}
