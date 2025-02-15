using Microsoft.EntityFrameworkCore;
using CAH.Contract.Repositories.Entity;
using CAH.Repositories.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CAH.Repositories.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUsers, ApplicationRoles, Guid,
        ApplicationUserClaims, ApplicationUserRoles, ApplicationUserLogins, ApplicationRoleClaims,
        ApplicationUserTokens>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

		// user
		public virtual DbSet<ApplicationUsers> ApplicationUsers => Set<ApplicationUsers>();
		public virtual DbSet<ApplicationRoles> ApplicationRoles => Set<ApplicationRoles>();
		public virtual DbSet<ApplicationUserClaims> ApplicationUserClaims => Set<ApplicationUserClaims>();
		public virtual DbSet<ApplicationUserRoles> ApplicationUserRoles => Set<ApplicationUserRoles>();
		public virtual DbSet<ApplicationUserLogins> ApplicationUserLogins => Set<ApplicationUserLogins>();
		public virtual DbSet<ApplicationRoleClaims> ApplicationRoleClaims => Set<ApplicationRoleClaims>();
		public virtual DbSet<ApplicationUserTokens> ApplicationUserTokens => Set<ApplicationUserTokens>();

		public virtual DbSet<UserInfo> UserInfos => Set<UserInfo>();
		public virtual DbSet<Shop> Shops { get; set; }
		public virtual DbSet<Service> Services { get; set; }
		public virtual DbSet<Appointment> Appointments { get; set; }
		public virtual DbSet<SalaryPayment> SalaryPayments { get; set; }
		public virtual DbSet<ServiceAppointment> ServiceAppointments { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<Feedback> Feedbacks { get; set; }
		public virtual DbSet<Combo> Combos { get; set; }
		public virtual DbSet<Message> Messages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseLazyLoadingProxies();
			}
		}
	}
}