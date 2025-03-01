using CAH.Core;
using CAH.ModelViews.SubScoreModelViews;

namespace CAH.Contract.Services.Interface
{
    public interface ISubjectScoreService
    {
        Task<string> CreateSubScoreAsync(CreateSubScoreModelView model);
        Task<string> DeleteSubjectScoreAsync(string id);
        Task<string> UpdateSubjectScoreAsync(string id, UpdateSubScoreModelView model);
        Task<BasePaginatedList<ListSubScoreModelView>> GetAllSubjectScoreAsync(int pageNumber, int pageSize);
        Task<Object> GetSubScoreByIdAsync(string id);
    }
}
