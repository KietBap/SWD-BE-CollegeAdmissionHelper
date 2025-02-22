using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.Core.Base;
using CAH.ModelViews.UniversityModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
	public class UniversityService : IUniversityService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IMapper _mapper;

		public UniversityService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContext = httpContextAccessor;
		}

		public async Task<string> CreateUniversityAsync(UniversityModelView model)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				University university = _mapper.Map<University>(model);

				university.CreatedTime = DateTimeOffset.UtcNow;
				university.CreatedBy = userId;

				await _unitOfWork.GetRepository<University>().InsertAsync(university);
				await _unitOfWork.SaveAsync();

				return "University added successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}

		public async Task<string> DeleteUniversityAsync(string id)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				var university = await _unitOfWork.GetRepository<University>().GetByIdAsync(id);
				if (university == null)
				{
					return "can not find University";
				}

				university.DeletedTime = DateTimeOffset.UtcNow;
				university.DeletedBy = userId;

				await _unitOfWork.GetRepository<University>().UpdateAsync(university);
				await _unitOfWork.SaveAsync();

				return "University delete successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}


		public async Task<string> UpdateUniversityAsync(string id, UniversityModelView model)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				var university = await _unitOfWork.GetRepository<University>().GetByIdAsync(id);
				if (university == null)
				{
					return "can not find university";
				}

				_mapper.Map(model, university);
				university.LastUpdatedBy = userId;
				university.LastUpdatedTime = DateTimeOffset.UtcNow;

				await _unitOfWork.GetRepository<University>().UpdateAsync(university);
				await _unitOfWork.SaveAsync();

				return "University updated successfully";
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}

		public async Task<BasePaginatedList<UniversityMV>> GetAllUniversityAsync(int pageNumber, int pageSize)
		{
			IQueryable<University> universityQuery = _unitOfWork.GetRepository<University>().Entities
				.Where(p => !p.DeletedTime.HasValue)
				.OrderByDescending(s => s.CreatedTime);

			int totalCount = await universityQuery.CountAsync();

			List<University> paginatedServices = await universityQuery
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			List<UniversityMV> universityModelView = _mapper.Map<List<UniversityMV>>(paginatedServices);

			return new BasePaginatedList<UniversityMV>(universityModelView, totalCount, pageNumber, pageSize);
		}


		public async Task<Object> GetUniversityByIdAsync(string id)
		{
			try
			{
				var university = await _unitOfWork.GetRepository<University>().GetByIdAsync(id);
				if (university == null || university.DeletedTime.HasValue)
				{
					return "can not find or university is deleted";
				}

				return _mapper.Map<UniversityMV>(university);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}
	}
}
