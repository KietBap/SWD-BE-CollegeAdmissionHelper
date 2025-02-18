using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class AcademicYearRepository : GenericRepository<AcademicYear>, IAcademicYearRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<AcademicYear> _dbSet;

		public AcademicYearRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<AcademicYear>();
		}
	}
}
