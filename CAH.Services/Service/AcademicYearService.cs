using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Core;
using CAH.Core.Base;
using CAH.ModelViews.AcademicYearModelViews;
using CAH.ModelViews.MajorModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class AcademicYearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AcademicYearService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> CreateAcademicYearAsync(AcademicYearModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                AcademicYear academicYear = _mapper.Map<AcademicYear>(model);

                academicYear.CreatedTime = DateTimeOffset.UtcNow;
                academicYear.CreatedBy = userId;

                await _unitOfWork.GetRepository<AcademicYear>().InsertAsync(academicYear);
                await _unitOfWork.SaveAsync();

                return "AcademicYear added successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string> DeleteAcademicYearAsync(string id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var academicYear = await _unitOfWork.GetRepository<AcademicYear>().GetByIdAsync(id);
                if (academicYear == null)
                {
                    return "can not find AcademicYear";
                }

                academicYear.DeletedTime = DateTimeOffset.UtcNow;
                academicYear.DeletedBy = userId;

                await _unitOfWork.GetRepository<AcademicYear>().UpdateAsync(academicYear);
                await _unitOfWork.SaveAsync();

                return "AcademicYear delete successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> UpdateAcademicYearAsync(string id, AcademicYearModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var academicYear = await _unitOfWork.GetRepository<AcademicYear>().GetByIdAsync(id);
                if (academicYear == null || academicYear.DeletedTime.HasValue)
                {
                    return "can not find or AcademicYear is deleted";
                }

                _mapper.Map(model, academicYear);
                academicYear.LastUpdatedBy = userId;
                academicYear.LastUpdatedTime = DateTimeOffset.UtcNow;

                await _unitOfWork.GetRepository<AcademicYear>().UpdateAsync(academicYear);
                await _unitOfWork.SaveAsync();

                return "AcademicYear updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<BasePaginatedList<ListAcaYearModelView>> GetAllAcademicYearAsync(int pageNumber, int pageSize)
        {
            IQueryable<AcademicYear> academicQuery = _unitOfWork.GetRepository<AcademicYear>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .OrderByDescending(s => s.CreatedTime);

            int totalCount = await academicQuery.CountAsync();

            List<AcademicYear> paginatedServices = await academicQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<ListAcaYearModelView> AYModelView = _mapper.Map<List<ListAcaYearModelView>>(paginatedServices);

            return new BasePaginatedList<ListAcaYearModelView>(AYModelView, totalCount, pageNumber, pageSize);
        }

        public async Task<Object> GetAcademicYearByIdAsync(string id)
        {
            try
            {
                var academicYear = await _unitOfWork.GetRepository<AcademicYear>().GetByIdAsync(id);
                if (academicYear == null || academicYear.DeletedTime.HasValue)
                {
                    return "can not find or academicYear is deleted";
                }

                return _mapper.Map<ListAcaYearModelView>(academicYear);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
