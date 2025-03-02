using CAH.Core;
using CAH.ModelViews.UserModelView;

namespace CAH.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<BasePaginatedList<ListUserModelView>> GetAllUserAsync(int pageNumber, int pageSize, string id, string email, string phoneNumber);
    }
}
