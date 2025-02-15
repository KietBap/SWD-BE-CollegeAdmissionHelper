using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core.Utils;
using CAH.ModelViews.AuthModelViews;
using CAH.Repositories.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<ApplicationUsers> _passwordHasher;

        public AppUserService(IUnitOfWork unitOfWork, IPasswordHasher<ApplicationUsers> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApplicationUsers> AuthenticateAsync(LoginModelView model)
        {
            var accountRepository = _unitOfWork.GetRepository<ApplicationUsers>();

            // Find the user by Username
            var user = await accountRepository.Entities
                .FirstOrDefaultAsync(x => x.UserName == model.Username && x.EmailConfirmed == true);

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
            var loginRepository = _unitOfWork.GetRepository<ApplicationUserLogins>();
            var existingLogin = await loginRepository.Entities
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.LoginProvider == "CustomLoginProvider");

            if (existingLogin == null)
            {
                // If no login record exists, add a new one
                var loginInfo = new ApplicationUserLogins
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
	}
}
