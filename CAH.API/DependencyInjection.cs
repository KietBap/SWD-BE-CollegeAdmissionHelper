using CAH.Contract.Repositories.Entity;
using CAH.Contract.Services.Cache;
using CAH.Contract.Services.Interface;
using CAH.Repositories.Context;
using CAH.Repositories.Entity;
using CAH.Services;
using CAH.Services.Service;
using CAH.Services.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace CAH.API
{
	public static class DependencyInjection
	{
		public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.ConfigRoute();
			services.AddDatabase(configuration);
			services.AddIdentity();
			services.AddInfrastructure(configuration);
			services.AddServices();
			services.ConfigJwt(configuration);
			services.ConfigureRedis(configuration);

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			})
			.AddCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
				options.Cookie.SameSite = SameSiteMode.Lax;
			})
			.AddGoogle(options =>
			{
				options.ClientId = configuration["Authentication:Google:ClientId"];
				options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
			});


			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
			});

			services.AddControllers();
		}

		public static void ConfigRoute(this IServiceCollection services)
		{
			services.Configure<RouteOptions>(options =>
			{
				options.LowercaseUrls = true;
			});
		}

		public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("HairSalonDb"));
			});
		}

		public static void AddIdentity(this IServiceCollection services)
		{
			services.AddIdentity<ApplicationUsers, ApplicationRoles>(options =>
			{
			})
			 .AddEntityFrameworkStores<DatabaseContext>()
			 .AddDefaultTokenProviders();
		}

		public static void AddServices(this IServiceCollection services)
		{
			services
				.AddScoped<IAppUserService, AppUserService>()
				.AddScoped<TokenService>()
				.AddScoped<IPasswordHasher<ApplicationUsers>, PasswordHasher<ApplicationUsers>>()
				.AddScoped<ICacheService, RedisCacheService>()
				.AddSignalR();
		}

		public static void ConfigJwt(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("Jwt");
			var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = jwtSettings["Issuer"],
					ValidAudience = jwtSettings["Audience"],
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};

				x.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
						return Task.CompletedTask;
					},
					OnTokenValidated = context =>
					{
						Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
						return Task.CompletedTask;
					}
				};
			});
		}

        // Add Redis Configuration
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            // Register StackExchange.Redis connection multiplexer as a singleton
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetSection("Redis:ConnectionString").Value));

            // Register IDistributedCache with Redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Redis:ConnectionString").Value;
                options.InstanceName = configuration.GetSection("Redis:InstanceName").Value;
            });
        }


    }
}
