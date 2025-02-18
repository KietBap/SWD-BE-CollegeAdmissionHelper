using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class AdmissionMethodRepository : GenericRepository<AdmissionMethod>, IAdmissionMethodRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<AdmissionMethod> _dbSet;

		public AdmissionMethodRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<AdmissionMethod>();
		}
	}
}
