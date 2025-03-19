using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAH.ModelViews.DashBoardModelViews;
using CAH.ModelViews.UniMajorModelViews;

namespace CAH.Contract.Repositories.IUOW
{
    public interface IDashboardRepository
    {
        Task<int> GetUserCountAsync();
        Task<int> GetUniversityCountAsync();
        Task<List<UserLoginCountModelView>> GetUserLoginCountsAsync(DateTime startDate, DateTime endDate);
        Task<List<ListUniMajorModelView>> GetTopReviewedUniMajors(int year, string period);
    }
}
