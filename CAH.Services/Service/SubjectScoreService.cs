using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.SubScoreModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class SubjectScoreService : ISubjectScoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectScoreService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }


        public async Task<string> CreateSubScoreAsync(CreateSubScoreModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                SubjectScore subjectScore = _mapper.Map<SubjectScore>(model);

                subjectScore.CreatedTime = DateTimeOffset.UtcNow;
                subjectScore.CreatedBy = userId;

                await _unitOfWork.GetRepository<SubjectScore>().InsertAsync(subjectScore);
                await _unitOfWork.SaveAsync();

                return "SubjectScore added successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> DeleteSubjectScoreAsync(string id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var subjectScore = await _unitOfWork.GetRepository<SubjectScore>().GetByIdAsync(id);
                if (subjectScore == null)
                {
                    return "can not find subjectScore";
                }

                subjectScore.DeletedTime = DateTimeOffset.UtcNow;
                subjectScore.DeletedBy = userId;

                await _unitOfWork.GetRepository<SubjectScore>().UpdateAsync(subjectScore);
                await _unitOfWork.SaveAsync();

                return "SubjectScore delete successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> UpdateSubjectScoreAsync(string id, UpdateSubScoreModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var subjectScore = await _unitOfWork.GetRepository<SubjectScore>().GetByIdAsync(id);
                if (subjectScore == null || subjectScore.DeletedTime.HasValue)
                {
                    return "can not find or SubjectScore is deleted";
                }

                _mapper.Map(model, subjectScore);
                subjectScore.LastUpdatedBy = userId;
                subjectScore.LastUpdatedTime = DateTimeOffset.UtcNow;

                await _unitOfWork.GetRepository<SubjectScore>().UpdateAsync(subjectScore);
                await _unitOfWork.SaveAsync();

                return "SubjectScore updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<BasePaginatedList<ListSubScoreModelView>> GetAllSubjectScoreAsync(int pageNumber, int pageSize)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

            IQueryable<SubjectScore> subScoreQuery = _unitOfWork.GetRepository<SubjectScore>().Entities
                .Where(p => !p.DeletedTime.HasValue && p.UserId.ToString() == userId)
                .OrderByDescending(s => s.CreatedTime);

            int totalCount = await subScoreQuery.CountAsync();

            List<SubjectScore> paginatedServices = await subScoreQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<ListSubScoreModelView> reviewModelView = _mapper.Map<List<ListSubScoreModelView>>(paginatedServices);

            return new BasePaginatedList<ListSubScoreModelView>(reviewModelView, totalCount, pageNumber, pageSize);
        }


        public async Task<Object> GetSubScoreByIdAsync(string id)
        {
            try
            {
                var subjectScore = await _unitOfWork.GetRepository<SubjectScore>().GetByIdAsync(id);
                if (subjectScore == null || subjectScore.DeletedTime.HasValue)
                {
                    return "can not find or subjectScore is deleted";
                }

                return _mapper.Map<ListSubScoreModelView>(subjectScore);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
