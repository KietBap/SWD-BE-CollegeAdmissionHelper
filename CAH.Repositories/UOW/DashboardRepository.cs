using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.Repositories.UOW
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly GenericRepository<User> _userRepo;
        private readonly GenericRepository<Major> _majorRepo;

        public DashboardRepository(DatabaseContext context)
        {
            _userRepo = new GenericRepository<User>(context);
            _majorRepo = new GenericRepository<Major>(context);
           
        }

        public async Task<int> GetUniversityCountAsync()
        {
           return await _majorRepo.Entities.CountAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _userRepo.Entities.CountAsync();
        }


    }
}
