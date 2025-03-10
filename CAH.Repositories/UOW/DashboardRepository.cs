using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.ModelViews.DashBoardModelViews;
using CAH.Repositories.Context;
using CAH.Contract.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAH.ModelViews.UniMajorModelViews;

namespace CAH.Repositories.UOW
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Major> _majorRepo;
        private readonly IGenericRepository<UniMajor> _UniMajor;
        private readonly DatabaseContext _context;

        public DashboardRepository(DatabaseContext context,
                                   IGenericRepository<User> userRepo,
                                   IGenericRepository<Major> majorRepo,
                                   IGenericRepository<UniMajor> uniMajor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _majorRepo = majorRepo ?? throw new ArgumentNullException(nameof(majorRepo));
            _UniMajor = uniMajor;
        }

        public async Task<int> GetUniversityCountAsync()
        {
            return await _majorRepo.Entities.CountAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _userRepo.Entities.CountAsync();
        }

        /// <summary>
        /// Đếm số lượt đăng nhập của người dùng trong khoảng thời gian
        /// </summary>
        public async Task<List<UserLoginCountModelView>> GetUserLoginCountsAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.UserLoginHistories
                .Where(ulh => ulh.LoginTime >= startDate && ulh.LoginTime <= endDate)
                .GroupBy(ulh => ulh.UserId)
                .Select(g => new UserLoginCountModelView
                {
                    UserId = g.Key,
                    UserName = _context.Users.Where(u => u.Id == g.Key).Select(u => u.UserName).FirstOrDefault(),
                    LoginCount = g.Count() // Chỉ đếm số bản ghi
                })
                .ToListAsync();
        }
        public async Task<List<ListUniMajorModelView>> GetTopMajorsAsync(int year, string period)
        {
            var query = _context.UniMajors
                .Include(um => um.Reviews)
                .Include(um => um.Major)
                .Include(um => um.University)
                .AsQueryable();

            if (period.ToLower() == "month")
            {
                query = query.Where(um => um.Reviews.Any(r => r.CreatedTime.Year == year &&
                                                              r.CreatedTime.Month == DateTime.Now.Month));
            }
            else if (period.ToLower() == "quarter")
            {
                int currentMonth = DateTime.Now.Month;
                int quarterStart = ((currentMonth - 1) / 3) * 3 + 1;
                int quarterEnd = quarterStart + 2;

                query = query.Where(um => um.Reviews.Any(r => r.CreatedTime.Year == year &&
                                                              r.CreatedTime.Month >= quarterStart &&
                                                              r.CreatedTime.Month <= quarterEnd));
            }

            var topMajors = await query
                .OrderByDescending(um => um.Reviews.Count)
                .Take(5)
                .Select(um => new ListUniMajorModelView
                {
                    Id = um.Id.ToString(),
                    TuitionFee = um.TuitionFee,
                    MajorCode = um.MajorCode,
                    UniversityName = um.University.Name,
                    MajorName = um.Major.Name
                })
                .ToListAsync();

            return topMajors;
        }

       
    }
}
