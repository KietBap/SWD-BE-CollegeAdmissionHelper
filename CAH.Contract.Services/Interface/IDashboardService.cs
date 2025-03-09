using CAH.ModelViews.DashBoardModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.Contract.Services.Interface
{
    public interface IDashboardService
    {
        Task<int> GetUserCountAsync();
        Task<int> GetUniversityCountAsync();
        Task<List<UserLoginCountModelView>> GetUserLoginCountsAsync(DateTime startDate, DateTime endDate);
    }
}
