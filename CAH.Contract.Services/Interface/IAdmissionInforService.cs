using CAH.Core;
using CAH.ModelViews.AdmissionInforModelViews;

namespace CAH.Contract.Services.Interface
{
    public interface IAdmissionInforService
    {
        Task<string> CreateAdmissionInforAsync(AdmissionInforModelView model);
        Task<BasePaginatedList<ListAdInforModelView>> GetAllAdmissionInforAsync(int pageNumber, int pageSize, string? uniName, string? majorName);
    }
}
