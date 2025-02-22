using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.Core.Base;
using CAH.ModelViews.AdmissionMethodMVs;
using CAH.ModelViews.UniversityModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
	public class AdmissionMethodService : IAdmissionMethodService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IMapper _mapper;

		public AdmissionMethodService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContext = httpContextAccessor;
		}


		public async Task<string> CreateAdmissionMethodAsync(AdmissionMethodMV model)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				AdmissionMethod admissionMethod = _mapper.Map<AdmissionMethod>(model);

				admissionMethod.CreatedTime = DateTimeOffset.UtcNow;
				admissionMethod.CreatedBy = userId;

				await _unitOfWork.GetRepository<AdmissionMethod>().InsertAsync(admissionMethod);
				await _unitOfWork.SaveAsync();

				return "AdmissionMethod added successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}


		public async Task<string> DeleteAdmissionMethodAsync(string id)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				var admissionMethod = await _unitOfWork.GetRepository<AdmissionMethod>().GetByIdAsync(id);
				if (admissionMethod == null)
				{
					return "can not find admissionMethod";
				}

				admissionMethod.DeletedTime = DateTimeOffset.UtcNow;
				admissionMethod.DeletedBy = userId;

				await _unitOfWork.GetRepository<AdmissionMethod>().UpdateAsync(admissionMethod);
				await _unitOfWork.SaveAsync();

				return "AdmissionMethod delete successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}


		public async Task<string> UpdateAdmissionMethodAsync(string id, AdmissionMethodMV model)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				var admissionMethod = await _unitOfWork.GetRepository<AdmissionMethod>().GetByIdAsync(id);
				if (admissionMethod == null)
				{
					return "can not find admissionMethod";
				}

				_mapper.Map(model, admissionMethod);
				admissionMethod.LastUpdatedBy = userId;
				admissionMethod.LastUpdatedTime = DateTimeOffset.UtcNow;

				await _unitOfWork.GetRepository<AdmissionMethod>().UpdateAsync(admissionMethod);
				await _unitOfWork.SaveAsync();

				return "AdmissionMethod updated successfully";
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}


		public async Task<BasePaginatedList<ListAdMethodMV>> GetAllAdMethodAsync(int pageNumber, int pageSize)
		{
			IQueryable<AdmissionMethod> adMethodQuery = _unitOfWork.GetRepository<AdmissionMethod>().Entities
				.Where(p => !p.DeletedTime.HasValue)
				.OrderByDescending(s => s.CreatedTime);

			int totalCount = await adMethodQuery.CountAsync();

			List<AdmissionMethod> paginatedServices = await adMethodQuery
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			List<ListAdMethodMV> adMethodModelView = _mapper.Map<List<ListAdMethodMV>>(paginatedServices);

			return new BasePaginatedList<ListAdMethodMV>(adMethodModelView, totalCount, pageNumber, pageSize);
		}

		public async Task<Object> GetAdMethodByIdAsync(string id)
		{
			try
			{
				var admissionMethod = await _unitOfWork.GetRepository<AdmissionMethod>().GetByIdAsync(id);
				if (admissionMethod == null || admissionMethod.DeletedTime.HasValue)
				{
					return "can not find or AdmissionMethod is deleted";
				}

				return _mapper.Map<ListAdMethodMV>(admissionMethod);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}
	}
}
