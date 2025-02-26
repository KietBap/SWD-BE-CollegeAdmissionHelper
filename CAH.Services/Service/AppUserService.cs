using AutoMapper;
using Azure.Core;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core.Utils;
using CAH.ModelViews.AuthModelViews;
using CAH.ModelViews.TokenModelViews;
using DocumentFormat.OpenXml.Spreadsheet;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AppUserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IMapper mapper, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
			_passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<User> AuthenticateAsync(LoginModelView model)
        {
			var accountRepository = _unitOfWork.GetRepository<User>();

			// Find the user by Username
			var user = await accountRepository.Entities
				.FirstOrDefaultAsync(x => x.UserName == model.Username);

			if (user == null)
			{
				return null; // User does not exist
			}

			// Verify the password
			var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

			if (passwordVerificationResult == PasswordVerificationResult.Failed)
			{
				return null; // Password does not match
			}

			// Check if a login record already exists
			var loginRepository = _unitOfWork.GetRepository<UserLogin>();
			var existingLogin = await loginRepository.Entities
				.FirstOrDefaultAsync(x => x.UserId == user.Id && x.LoginProvider == "CustomLoginProvider");

			if (existingLogin == null)
			{
				// If no login record exists, add a new one
				var loginInfo = new UserLogin
				{
					UserId = user.Id, // UserId from the authenticated user
					ProviderKey = user.Id.ToString(),
					LoginProvider = "CustomLoginProvider", // Or another provider name
					ProviderDisplayName = "Standard Login",
					CreatedBy = user.UserName, // Record who performed the login
					CreatedTime = CoreHelper.SystemTimeNow,
					LastUpdatedBy = user.UserName,
					LastUpdatedTime = CoreHelper.SystemTimeNow
				};

				await loginRepository.InsertAsync(loginInfo);
				await _unitOfWork.SaveAsync(); // Save changes to the database
			}
			else
			{
				// If the login record already exists, update it if necessary
				existingLogin.LastUpdatedBy = user.UserName;
				existingLogin.LastUpdatedTime = CoreHelper.SystemTimeNow;

				await loginRepository.UpdateAsync(existingLogin);
				await _unitOfWork.SaveAsync(); // Save changes to the database
			}

			return user; // Return the authenticated user
		}

        public async Task<UserModelView> GetUserByEmailAsync(string email)
        {
            // Check if the provided User email is valid (non-empty and non-whitespace)
            if (string.IsNullOrWhiteSpace(email))
            {
                return null; // Or you could throw an exception or return an error message
            }

            // Try to find the user by ID, ensuring hasn’t been marked as deleted
            var userEntity = await _unitOfWork.GetRepository<User>().Entities
                .FirstOrDefaultAsync(user => user.Email == email && !user.DeletedTime.HasValue);

            // If the user is not found, return null
            if (userEntity == null)
            {
                return null;
            }

            // Map the ApplicationRoles entity to a RoleModelView and return it
            UserModelView userModelView = _mapper.Map<UserModelView>(userEntity);
            return userModelView;
        }

        private async Task<UserModelView> CreateAccount(string email)
        {
            var newAccount = new User
            {
                Id = Guid.NewGuid(),
                UserName = email.Split('@')[0],
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var userRole = await _unitOfWork.GetRepository<Role>().Entities.FirstOrDefaultAsync(r => r.Name == "Admin");

            var applicationUserRole = new UserRole
            {
                UserId = newAccount.Id,
                RoleId = userRole.Id,
                CreatedBy = newAccount.Id.ToString(),
                CreatedTime = DateTime.UtcNow,
                LastUpdatedBy = newAccount.Id.ToString(),
                LastUpdatedTime = DateTime.UtcNow
            };

            // save changes
            await _unitOfWork.GetRepository<UserRole>().InsertAsync(applicationUserRole);

            await _unitOfWork.GetRepository<User>().InsertAsync(newAccount);
            await _unitOfWork.SaveAsync();
            UserModelView userModelView = _mapper.Map<UserModelView>(newAccount);

            return userModelView;
        }

        public async Task<TokenModelView> LoginGoogleAsync(GoogleToken model)
		{
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(model.Token);
                var uid = decodedToken.Uid;
                var email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : null;
                var displayName = decodedToken.Claims.ContainsKey("name") ? decodedToken.Claims["name"].ToString() : null;
                var user = await GetUserByEmailAsync(email);
                if (user == null)
                {
                    await CreateAccount(email);
                }

                var token = await _tokenService.GenerateJwtTokenAsync(user.Id, user.UserName);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
