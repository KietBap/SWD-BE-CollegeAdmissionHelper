using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Repositories.IUOW;
using CAH.Contract.Services.Interface;
using CAH.ModelViews.DashBoardModelViews;
using CAH.ModelViews.UniMajorModelViews;
using CAH.ModelViews.UserModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.Services.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardService(IMapper mapper, IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> GetUniversityCountAsync()
        {
            return await _dashboardRepository.GetUniversityCountAsync();
        }

        public async Task<List<ListUserModelView>> GetUserCountAsync()

        {
            return await _dashboardRepository.GetUserCountAsync();
        }
        public async Task<List<UserLoginCountModelView>> GetUserLoginCountsAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var userLogins = await _dashboardRepository.GetUserLoginCountsAsync(startDate, endDate);
                return _mapper.Map<List<UserLoginCountModelView>>(userLogins);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê đăng nhập: {ex.Message}");
            }
        }

        public Task<List<ListUniMajorModelView>> GetTopReviewedUniMajors(int year, string period, int value)
        {
            return _dashboardRepository.GetTopReviewedUniMajors(year, period,value);
        }
    }
}
