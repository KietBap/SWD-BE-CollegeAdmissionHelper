using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class MajorRepository : GenericRepository<Major>, IMajorRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<Major> _dbSet;

		public MajorRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<Major>();
		}
	}
}
