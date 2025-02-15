using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.Contract.Repositories.IUOW
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
    }
}
