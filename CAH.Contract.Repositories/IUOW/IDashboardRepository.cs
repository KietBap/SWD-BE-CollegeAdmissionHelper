using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.Contract.Repositories.IUOW
{
    public interface IDashboardRepository
    {
        Task<int> GetUserCountAsync();
        Task<int> GetUniversityCountAsync();
    }
}
