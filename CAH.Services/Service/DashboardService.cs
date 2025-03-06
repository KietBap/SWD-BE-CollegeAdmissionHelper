using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Repositories.IUOW;
using CAH.Contract.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.Services.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository dashboardRepository;
        private IMapper mapper;
        public DashboardService( IMapper mapper, IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
            this.mapper = mapper;
        }
  
        public async Task<int> GetUniversityCountAsync()
        {
            return await dashboardRepository.GetUniversityCountAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await dashboardRepository.GetUserCountAsync();
        }
    }
}
