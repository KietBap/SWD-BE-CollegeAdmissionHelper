using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<Subject> _dbSet;

		public SubjectRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<Subject>();
		}
	}
}
