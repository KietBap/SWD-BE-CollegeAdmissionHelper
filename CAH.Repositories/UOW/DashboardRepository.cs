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
using CAH.Core.Utils;
using CAH.ModelViews.UniMajorModelViews;
using CAH.ModelViews.UserModelView;

namespace CAH.Repositories.UOW
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Major> _majorRepo;
        private readonly IGenericRepository<University> _universityRepo;
        private readonly DatabaseContext _context;

        public DashboardRepository(DatabaseContext context,
                                   IGenericRepository<User> userRepo,
                                   IGenericRepository<Major> majorRepo,
                                   IGenericRepository<University> universityRepo)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _majorRepo = majorRepo ?? throw new ArgumentNullException(nameof(majorRepo));   
    _universityRepo = universityRepo ?? throw new ArgumentNullException(nameof(universityRepo));}
        


        public async Task<List<ListUniMajorModelView>> GetTopReviewedUniMajors(int year, string period, int value)
        {
            var uniMajorsQuery = _context.UniMajors
                .Include(um => um.Reviews)
                .Where(um => um.Reviews.Any())
                .AsQueryable();

            if (period.ToLower() == "month")
            {
                uniMajorsQuery = uniMajorsQuery.Where(um => um.Reviews
                    .Any(r => r.CreatedTime.Year == year && r.CreatedTime.Month == value));
            }
            else if (period.ToLower() == "quarter")
            {
                int startMonth = (value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                uniMajorsQuery = uniMajorsQuery.Where(um => um.Reviews
                    .Any(r => r.CreatedTime.Year == year && r.CreatedTime.Month >= startMonth && r.CreatedTime.Month <= endMonth));
            }

            var topUniMajors = await uniMajorsQuery
                .OrderByDescending(um => um.Reviews.Count)
                .Take(5)
                .Select(um => new ListUniMajorModelView
                {
                    Id = um.Id,
                    MajorName = um.Major.Name,
                    UniversityName = um.University.Name,
                    MajorCode = um.MajorCode,
                    TuitionFee = um.TuitionFee,
                })
                .ToListAsync();

            return topUniMajors;
        }


        public async Task<int> GetUniversityCountAsync()
        {
            return await _universityRepo.Entities.Where(u => u.DeletedTime == null).CountAsync();
        }

        public async Task<List<ListUserModelView>> GetUserCountAsync()
        {
            var usersLastLogin = await (from user in _context.Users.Where(u => u.DeletedTime == null)
                                        join login in
                                            (from ul in _context.UserLoginHistories
                                             group ul by ul.UserId into g
                                             select new { UserId = g.Key, LastLoginTime = g.Max(l => l.LoginTime) })
                                        on user.Id equals login.UserId into loginGroup
                                        from login in loginGroup.DefaultIfEmpty()
                                        select new ListUserModelView
                                        {
                                            Id = user.Id.ToString(),
                                            UserImage = user.UserImage,
                                            Name = user.Name,
                                            Email = user.Email,
                                            UserName = user.UserName,
                                            PhoneNumber = user.PhoneNumber,
                                            LastLoginTime = login != null ? login.LastLoginTime : null // Keep as null
                                        }).ToListAsync();

            return usersLastLogin;
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
        

    }
}
