using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.Core.Base;
using CAH.ModelViews.SubjectModelViews;
using CAH.ModelViews.UniversityModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
	public class SubjectService : ISubjectService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IMapper _mapper;

		public SubjectService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContext = httpContextAccessor;
		}


		public async Task<string> CreateSubjectAsync(SubjectModelView model)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				Subject subject = _mapper.Map<Subject>(model);

				subject.CreatedTime = DateTimeOffset.UtcNow;
				subject.CreatedBy = userId;

				await _unitOfWork.GetRepository<Subject>().InsertAsync(subject);
				await _unitOfWork.SaveAsync();

				return "Subject added successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}

		public async Task<string> DeleteSubjectAsync(string id)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				var subject = await _unitOfWork.GetRepository<Subject>().GetByIdAsync(id);
				if (subject == null)
				{
					return "can not find Subject";
				}

				subject.DeletedTime = DateTimeOffset.UtcNow;
				subject.DeletedBy = userId;

				await _unitOfWork.GetRepository<Subject>().UpdateAsync(subject);
				await _unitOfWork.SaveAsync();

				return "Subject delete successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}


		public async Task<string> UpdateSubjectAsync(string id, SubjectModelView model)
		{
			try
			{
				var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

				var subject = await _unitOfWork.GetRepository<Subject>().GetByIdAsync(id);
				if (subject == null || subject.DeletedTime.HasValue)
				{
					return "can not find or Subject is deleted";
				}

				_mapper.Map(model, subject);
				subject.LastUpdatedBy = userId;
				subject.LastUpdatedTime = DateTimeOffset.UtcNow;

				await _unitOfWork.GetRepository<Subject>().UpdateAsync(subject);
				await _unitOfWork.SaveAsync();

				return "Subject updated successfully";
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}


		public async Task<BasePaginatedList<ListSubjectModelView>> GetAllSubjectAsync(int pageNumber, int pageSize)
		{
			IQueryable<Subject> subjectQuery = _unitOfWork.GetRepository<Subject>().Entities
				.Where(p => !p.DeletedTime.HasValue)
				.OrderByDescending(s => s.CreatedTime);

			int totalCount = await subjectQuery.CountAsync();

			List<Subject> paginatedServices = await subjectQuery
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			List<ListSubjectModelView> subjectModelView = _mapper.Map<List<ListSubjectModelView>>(paginatedServices);

			return new BasePaginatedList<ListSubjectModelView>(subjectModelView, totalCount, pageNumber, pageSize);
		}


		public async Task<Object> GetSubjectByIdAsync(string id)
		{
			try
			{
				var subject = await _unitOfWork.GetRepository<Subject>().GetByIdAsync(id);
				if (subject == null || subject.DeletedTime.HasValue)
				{
					return "can not find or university is deleted";
				}

				return _mapper.Map<ListSubjectModelView>(subject);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}
	}
}
