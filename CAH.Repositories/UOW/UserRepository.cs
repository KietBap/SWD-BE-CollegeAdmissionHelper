﻿using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.IUOW;
using CAH.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CAH.Repositories.UOW
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		protected readonly DatabaseContext _context;

		protected readonly DbSet<User> _dbSet;

		public UserRepository(DatabaseContext dbContext) : base(dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<User>();
		}
	}
}
