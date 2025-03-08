using CAH.Core;
using CAH.ModelViews.UniMajorModelViews;

namespace CAH.Contract.Services.Interface
{
    public interface IUniMajorService
    {
        Task<string> CreateUniMajorAsync(CreateUniMajorModelView model);
        Task<string> DeleteUniMajorAsync(string id);
        Task<string> UpdateUniMajorAsync(string id, UpdateUniMajorModelView model);
        Task<BasePaginatedList<ListUniMajorModelView>> GetAllUniMajorAsync(int pageNumber, int pageSize, string? universityName);
        Task<Object> GetUniMajorByIdAsync(string id);
    }
}
