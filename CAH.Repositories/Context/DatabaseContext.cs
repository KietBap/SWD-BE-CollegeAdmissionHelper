using Microsoft.EntityFrameworkCore;
using CAH.Contract.Repositories.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CAH.Repositories.Context
{
    public class DatabaseContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		public virtual DbSet<User> ApplicationUsers => Set<User>();
		public virtual DbSet<Role> ApplicationRoles => Set<Role>();
		public virtual DbSet<UserClaim> ApplicationUserClaims => Set<UserClaim>();
		public virtual DbSet<UserRole> ApplicationUserRoles => Set<UserRole>();
		public virtual DbSet<UserLogin> ApplicationUserLogins => Set<UserLogin>();
		public virtual DbSet<RoleClaim> ApplicationRoleClaims => Set<RoleClaim>();
		public virtual DbSet<UserToken> ApplicationUserTokens => Set<UserToken>();
		public virtual DbSet<Subject> Subjects { get; set; }
		public virtual DbSet<Wishlist> Wishlists { get; set; }
		public virtual DbSet<AcademicYear> AcademicYears { get; set; }
		public virtual DbSet<Major> Majors { get; set; }
		public virtual DbSet<UniMajor> UniMajors { get; set; }
		public virtual DbSet<University> Universities { get; set; }
		public virtual DbSet<AdmissionInfor> AdmissionInfors { get; set; }
		public virtual DbSet<AdmissionMethod> AdmissionMethods { get; set; }
		public virtual DbSet<InforMethod> InforMethods { get; set; }
		public virtual DbSet<SubjectScore> SubjectScores { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);

			// Thiết lập các quan hệ giữa các bảng
			modelBuilder.Entity<UserLogin>()
				.HasKey(login => new { login.UserId, login.LoginProvider, login.ProviderKey });
			modelBuilder.Entity<UserRole>(userRole =>
			{
				userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
				userRole.HasOne(ur => ur.User)
					.WithMany(u => u.UserRoles)
					.HasForeignKey(ur => ur.UserId)
					.IsRequired();
				userRole.HasOne(ur => ur.Role)
					.WithMany(r => r.UserRoles)
					.HasForeignKey(ur => ur.RoleId)
					.IsRequired();
			});

			modelBuilder.Entity<UserToken>()
				.HasKey(token => new { token.UserId, token.LoginProvider, token.Name });

			// Wishlist - User (N-1)
			modelBuilder.Entity<Wishlist>()
				.HasOne(w => w.User)
				.WithMany(u => u.Wishlists)
				.HasForeignKey(w => w.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			// Wishlist - AdmissionInformation (N-1)
			modelBuilder.Entity<Wishlist>()
				.HasOne(w => w.AdmissionInfor)
				.WithMany(ai => ai.Wishlists)
				.HasForeignKey(w => w.AdmissionInforId)
				.OnDelete(DeleteBehavior.Restrict);

			// AdmissionInformation - AcademicYear (N-1)
			modelBuilder.Entity<AdmissionInfor>()
				.HasOne(ai => ai.AcademicYear)
				.WithMany(ay => ay.AdmissionInfors)
				.HasForeignKey(ai => ai.AcademicYearId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<InforMethod>()
				.HasOne(im => im.AdmissionInfor)  
				.WithMany(ai => ai.InforMethods)       
				.HasForeignKey(im => im.AdmissionInforId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<InforMethod>()
				.HasOne(im => im.AdmissionMethod)
				.WithMany(ai => ai.InforMethods)
				.HasForeignKey(im => im.AdmissionMethodId)
				.OnDelete(DeleteBehavior.Restrict);

			// AdmissionInformation - UniMajor (N-1)
			modelBuilder.Entity<AdmissionInfor>()
				.HasOne(ai => ai.UniMajor)
				.WithMany(um => um.AdmissionInfors)
				.HasForeignKey(ai => ai.UniMajorId)
				.OnDelete(DeleteBehavior.Restrict);

			// UniMajor - University (N-1)
			modelBuilder.Entity<UniMajor>()
				.HasOne(um => um.University)
				.WithMany(u => u.UniMajors)
				.HasForeignKey(um => um.UniversityId)
				.OnDelete(DeleteBehavior.Restrict);

			// UniMajor - Major (N-1)
			modelBuilder.Entity<UniMajor>()
				.HasOne(um => um.Major)
				.WithMany(m => m.UniMajors)
				.HasForeignKey(um => um.MajorId)
				.OnDelete(DeleteBehavior.Restrict);

			//===================================================Seed data================================================================

			//role
			var roleIdAdmin = Guid.NewGuid();
			var roleIdUser = Guid.NewGuid();
			modelBuilder.Entity<Role>().HasData(
				new Role
				{
					Id = roleIdAdmin,
					Name = "Admin",
					NormalizedName = "ADMIN",
					CreatedBy = "System",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				},
				new Role
				{
					Id = roleIdUser,
					Name = "User",
					NormalizedName = "USER",
					CreatedBy = "System",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				});

			// user
			var adminId = Guid.NewGuid();
			var userId = Guid.NewGuid();
			var adminUser = new User { Id = adminId };
			var normalUser = new User { Id = userId };
			var passwordHasher = new PasswordHasher<User>();
			var adminPasswordHash = passwordHasher.HashPassword(adminUser, "123");
			var userPasswordHash = passwordHasher.HashPassword(normalUser, "123");

			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = adminId,
					UserName = "admin",
					NormalizedUserName = "ADMIN@EXAMPLE.COM",
					Email = "admin@example.com",
					NormalizedEmail = "ADMIN@EXAMPLE.COM",
					EmailConfirmed = true,
					PasswordHash = adminPasswordHash,
					SecurityStamp = "SeedData",
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				},
				new User
				{
					Id = userId,
					UserName = "user",
					NormalizedUserName = "USER@EXAMPLE.COM",
					Email = "user@example.com",
					NormalizedEmail = "USER@EXAMPLE.COM",
					EmailConfirmed = true,
					PasswordHash = userPasswordHash,
					SecurityStamp = "SeedData",
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				});

			//user role
			modelBuilder.Entity<UserRole>().HasData(
				new UserRole
				{
					UserId = adminId,
					RoleId = roleIdAdmin,
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				},
				new UserRole
				{
					UserId = userId,
					RoleId = roleIdUser,
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				});
		}
	}
}