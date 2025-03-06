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
    }
}
