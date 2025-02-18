using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class AdmissionInforRepository : GenericRepository<AdmissionInfor>, IAdmissionInforRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<AdmissionInfor> _dbSet;

		public AdmissionInforRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<AdmissionInfor>();
		}
	}
}
