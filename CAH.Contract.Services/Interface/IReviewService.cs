using CAH.Core;
using CAH.ModelViews.ReviewModelViews;

namespace CAH.Contract.Services.Interface
{
    public interface IReviewService
    {
        Task<string> CreateReviewAsync(CreateReviewModelView model);
        Task<string> DeleteReviewAsync(string id);
        Task<string> UpdateReviewAsync(string id, UpdateReviewModelView model);
        Task<BasePaginatedList<ListReviewModelView>> GetAllReviewAsync(int pageNumber, int pageSize);
        Task<Object> GetReviewByIdAsync(string id);
    }
}
