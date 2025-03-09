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
		public virtual DbSet<Wishlist> Wishlists { get; set; } // note
		public virtual DbSet<AcademicYear> AcademicYears { get; set; }
		public virtual DbSet<Major> Majors { get; set; }
		public virtual DbSet<UniMajor> UniMajors { get; set; }
		public virtual DbSet<University> Universities { get; set; }
		public virtual DbSet<AdmissionInfor> AdmissionInfors { get; set; } // Note
		public virtual DbSet<AdmissionMethod> AdmissionMethods { get; set; }// Note
        public virtual DbSet<InforMethod> InforMethods { get; set; } // Note
        public virtual DbSet<SubjectScore> SubjectScores { get; set; }
		public virtual DbSet<Review> Reviews { get; set; } // Note
		public virtual DbSet<UserLoginHistory> UserLoginHistories { get; set; }
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

            modelBuilder.Entity<Review>()
                .HasOne(um => um.User)
                .WithMany(m => m.Reviews)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(um => um.UniMajor)
                .WithMany(m => m.Reviews)
                .HasForeignKey(um => um.UniMajorId)
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
					Name = "KICM vippro",
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
					Name = "Jack 97",
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
				}); ;

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

			//University
			var uniId1 = Guid.NewGuid().ToString();
            var uniId2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<University>().HasData(
				new University
				{
					Id = uniId1,
					Name = "FPT University",
					Location = "Thu Duc",
					UniversityCode = "FPT",
					Email = "fptuniversity@fpt.edu.vn",
					PhoneNumber = "0987654321",
					Accreditation = "So 1 Viet Nam",
					Type = "Private",
					EstablishedDate = DateTime.Now,
					RankingInternational = 1,
					RankingNational = 2,
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				},
				new University
				{
					Id = uniId2,
					Name = "Su Pham Ki Thuat",
					Location = "Vo Van Ngan, Thu Duc",
					UniversityCode = "HCMUTE",
					Email = "suphamkithuat@gmail.com",
					PhoneNumber = "0987654321",
					Accreditation = "So 1 VVN",
					Type = "State",
					EstablishedDate = DateTime.Now,
					RankingInternational = 1,
					RankingNational = 2,
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				});

            //Major
            var majorId1 = Guid.NewGuid().ToString();
            var majorId2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<Major>().HasData(
				new Major
				{
					Id = majorId1,
					Name = "Cong Nghe Thong Tin",
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				},
				new Major
				{
					Id = majorId2,
					Name = "Kinh Te",
					CreatedBy = "SeedData",
					LastUpdatedBy = "SeedData",
					CreatedTime = DateTimeOffset.UtcNow,
					LastUpdatedTime = DateTimeOffset.UtcNow
				});

			//UniMajor
			var uniMajor1 = Guid.NewGuid().ToString();
			var uniMajor2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<UniMajor>().HasData(
                new UniMajor
                {
                    Id = uniMajor1,
					UniversityId = uniId1,
					MajorId = majorId1,
                    TuitionFee = "30.000.000",
					MajorCode = "SE",
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new UniMajor
                {
                    Id = uniMajor2,
                    UniversityId = uniId1,
                    MajorId = majorId2,
                    TuitionFee = "20.000.000",
                    MajorCode = "SS",
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });

            //Subject
            var sub1 = Guid.NewGuid().ToString();
            var sub2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    Id = sub1,
                    Name = "Toan",
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new Subject
                {
                    Id = sub2,
                    Name = "Tieng Anh",
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });

            //AdmissionMethod
            var am1 = Guid.NewGuid().ToString();
            var am2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<AdmissionMethod>().HasData(
                new AdmissionMethod
                {
                    Id = am1,
                    MethodName = "Xet Diem Tot Nghiep",
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new AdmissionMethod
                {
                    Id = am2,
                    MethodName = "Xet diem danh gia nang luc",
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });


            // AdmissionInfor
            var ai1 = Guid.NewGuid().ToString();
            var ai2 = Guid.NewGuid().ToString();
            var ay1 = Guid.NewGuid().ToString();
            var ay2 = Guid.NewGuid().ToString();

            modelBuilder.Entity<AdmissionInfor>().HasData(
                new AdmissionInfor
                {
                    Id = ai1,
                    UniMajorId = uniMajor1,
                    AcademicYearId = ay1, // ✅ OK, dùng ID của AcademicYear
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new AdmissionInfor
                {
                    Id = ai2,
                    UniMajorId = uniMajor2,
                    AcademicYearId = ay2, // ✅ OK
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });

            // Wishlist
            modelBuilder.Entity<Wishlist>().HasData(
                new Wishlist
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    AdmissionInforId = ai1, // ✅ Dùng đúng ID của AdmissionInfor
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });


            //AcademicYear

            modelBuilder.Entity<AcademicYear>().HasData(
                new AcademicYear
                {
                    Id = ay1,
                    Year = 2025,
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new AcademicYear
                {
                    Id = ay2,
                    Year = 2024,
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });
            // InforMethod
            modelBuilder.Entity<InforMethod>().HasData(
                new InforMethod
                {
                    Id = Guid.NewGuid().ToString(),
                    AdmissionInforId = ai1,
                    AdmissionMethodId = am1,
                    ScoreType = "GPA", // Bổ sung giá trị cho ScoreType
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new InforMethod
                {
                    Id = Guid.NewGuid().ToString(),
                    AdmissionInforId = ai2,
                    AdmissionMethodId = am2,
                    ScoreType = "Percentage", // Bổ sung giá trị cho ScoreType
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });

            //Review
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    UniMajorId = uniMajor1,
                    Comment = "Truong rat tot!",
                    Rating = 5,
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                },
                new Review
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = adminId,
                    UniMajorId = uniMajor2,
                    Comment = "Moi truong hoc tap nang dong!",
                    Rating = 4,
                    CreatedBy = "SeedData",
                    LastUpdatedBy = "SeedData",
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow
                });
        }
	}
}