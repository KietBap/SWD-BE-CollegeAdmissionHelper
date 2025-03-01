using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.UniMajorModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class UniMajorService : IUniMajorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UniMajorService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }


        public async Task<string> CreateUniMajorAsync(CreateUniMajorModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                UniMajor uniMajor = _mapper.Map<UniMajor>(model);

                uniMajor.CreatedTime = DateTimeOffset.UtcNow;
                uniMajor.CreatedBy = userId;

                await _unitOfWork.GetRepository<UniMajor>().InsertAsync(uniMajor);
                await _unitOfWork.SaveAsync();

                return "UniMajor added successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> DeleteUniMajorAsync(string id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var uniMajor = await _unitOfWork.GetRepository<UniMajor>().GetByIdAsync(id);
                if (uniMajor == null)
                {
                    return "can not find UniMajor";
                }

                uniMajor.DeletedTime = DateTimeOffset.UtcNow;
                uniMajor.DeletedBy = userId;

                await _unitOfWork.GetRepository<UniMajor>().UpdateAsync(uniMajor);
                await _unitOfWork.SaveAsync();

                return "UniMajor delete successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> UpdateUniMajorAsync(string id, UpdateUniMajorModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var uniMajor = await _unitOfWork.GetRepository<UniMajor>().GetByIdAsync(id);
                if (uniMajor == null || uniMajor.DeletedTime.HasValue)
                {
                    return "can not find or UniMajor is deleted";
                }

                _mapper.Map(model, uniMajor);
                uniMajor.LastUpdatedBy = userId;
                uniMajor.LastUpdatedTime = DateTimeOffset.UtcNow;

                await _unitOfWork.GetRepository<UniMajor>().UpdateAsync(uniMajor);
                await _unitOfWork.SaveAsync();

                return "UniMajor updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<BasePaginatedList<ListUniMajorModelView>> GetAllUniMajorAsync(int pageNumber, int pageSize)
        {
            IQueryable<UniMajor> uniMajorQuery = _unitOfWork.GetRepository<UniMajor>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .OrderByDescending(s => s.CreatedTime);

            int totalCount = await uniMajorQuery.CountAsync();

            List<UniMajor> paginatedServices = await uniMajorQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<ListUniMajorModelView> uniMajorModelView = _mapper.Map<List<ListUniMajorModelView>>(paginatedServices);

            return new BasePaginatedList<ListUniMajorModelView>(uniMajorModelView, totalCount, pageNumber, pageSize);
        }


        public async Task<Object> GetUniMajorByIdAsync(string id)
        {
            try
            {
                var uniMajor = await _unitOfWork.GetRepository<UniMajor>().GetByIdAsync(id);
                if (uniMajor == null || uniMajor.DeletedTime.HasValue)
                {
                    return "can not find or uniMajor is deleted";
                }

                return _mapper.Map<ListUniMajorModelView>(uniMajor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
