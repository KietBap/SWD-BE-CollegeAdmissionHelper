using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class UniversityRepository : GenericRepository<University>, IUniversityRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<University> _dbSet;

		public UniversityRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<University>();
		}
	}
}
