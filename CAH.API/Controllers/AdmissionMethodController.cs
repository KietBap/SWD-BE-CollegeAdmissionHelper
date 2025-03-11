using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.AdmissionMethodMVs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAH.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class AdmissionMethodController : ControllerBase
	{
		private readonly IAdmissionMethodService _service;
		private readonly ICacheService _cacheService;

        public AdmissionMethodController(IAdmissionMethodService admissionMethodService, ICacheService cacheService)
        {
            _service = admissionMethodService;
			_cacheService = cacheService;
        }

		[HttpPost]
		public async Task<ActionResult<string>> CreateAdmissionMethod(AdmissionMethodMV model)
		{
            string result = await _service.CreateAdmissionMethodAsync(model);
            await _cacheService.RemoveByPrefixAsync("admission_methods"); 

            return Ok(new { Message = result });
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<string>> DeleteAdmissionMethod(string id)
		{
            string result = await _service.DeleteAdmissionMethodAsync(id);
            await _cacheService.RemoveByPrefixAsync("admission_methods");
            await _cacheService.RemoveByPrefixAsync($"admission_method:{id}");

            return Ok(new { Message = result });
        }


		[HttpPatch("{id}")]
		public async Task<ActionResult<string>> UpdateAdmissionMethod(string id, AdmissionMethodMV model)
		{
            string result = await _service.UpdateAdmissionMethodAsync(id, model);
            await _cacheService.RemoveByPrefixAsync("admission_methods");
            await _cacheService.RemoveByPrefixAsync($"admission_method:{id}"); 

            return Ok(new { Message = result });
        }

		[HttpGet]
		public async Task<ActionResult<BasePaginatedList<ListAdMethodMV>>> GetAllAdMethod(int pageNumber = 1, int pageSize = 5)
		{
            string cacheKey = $"admission_methods:page_{pageNumber}_size_{pageSize}";
            var cachedResult = await _cacheService.GetListAsync<BasePaginatedList<ListAdMethodMV>>(cacheKey);

            if (cachedResult is not null)
            {
                return Ok(new { Message = cachedResult });
            }

            var result = await _service.GetAllAdMethodAsync(pageNumber, pageSize);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), "admission_methods");
            return Ok(new { Message = result });
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<Object>> GetAdMethodById(string id)
		{
            string cacheKey = $"admission_method:{id}";
            var cachedResult = await _cacheService.GetListAsync<object>(cacheKey);

            if (cachedResult is not null)
            {
                return Ok(new { Message = cachedResult });
            }

            var result = await _service.GetAdMethodByIdAsync(id);

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(30), $"admission_method:{id}");
            return Ok(new { Message = result });
        }
	}
}
