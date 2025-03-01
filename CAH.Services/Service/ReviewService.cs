using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.ReviewModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<string> CreateReviewAsync(CreateReviewModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                Review review = _mapper.Map<Review>(model);

                review.CreatedTime = DateTimeOffset.UtcNow;
                review.CreatedBy = userId;

                await _unitOfWork.GetRepository<Review>().InsertAsync(review);
                await _unitOfWork.SaveAsync();

                return "Review added successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> DeleteReviewAsync(string id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var review = await _unitOfWork.GetRepository<Review>().GetByIdAsync(id);
                if (review == null)
                {
                    return "can not find Review";
                }

                review.DeletedTime = DateTimeOffset.UtcNow;
                review.DeletedBy = userId;

                await _unitOfWork.GetRepository<Review>().UpdateAsync(review);
                await _unitOfWork.SaveAsync();

                return "Review delete successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> UpdateReviewAsync(string id, UpdateReviewModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var review = await _unitOfWork.GetRepository<Review>().GetByIdAsync(id);
                if (review == null || review.DeletedTime.HasValue)
                {
                    return "can not find or Review is deleted";
                }

                _mapper.Map(model, review);
                review.LastUpdatedBy = userId;
                review.LastUpdatedTime = DateTimeOffset.UtcNow;

                await _unitOfWork.GetRepository<Review>().UpdateAsync(review);
                await _unitOfWork.SaveAsync();

                return "Review updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<BasePaginatedList<ListReviewModelView>> GetAllReviewAsync(int pageNumber, int pageSize)
        {
            IQueryable<Review> reviewQuery = _unitOfWork.GetRepository<Review>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .OrderByDescending(s => s.CreatedTime);

            int totalCount = await reviewQuery.CountAsync();

            List<Review> paginatedServices = await reviewQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<ListReviewModelView> reviewModelView = _mapper.Map<List<ListReviewModelView>>(paginatedServices);

            return new BasePaginatedList<ListReviewModelView>(reviewModelView, totalCount, pageNumber, pageSize);
        }


        public async Task<Object> GetReviewByIdAsync(string id)
        {
            try
            {
                var review = await _unitOfWork.GetRepository<Review>().GetByIdAsync(id);
                if (review == null || review.DeletedTime.HasValue)
                {
                    return "can not find or review is deleted";
                }

                return _mapper.Map<ListReviewModelView>(review);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
