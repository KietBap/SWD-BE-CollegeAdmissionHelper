using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using CAH.Repositories.Entity;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class UserRepository : GenericRepository<ApplicationUsers>, IUserRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<ApplicationUsers> _dbSet;

		public UserRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<ApplicationUsers>();
		}
	}
}
