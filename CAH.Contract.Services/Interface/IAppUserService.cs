using CAH.ModelViews.AuthModelViews;
using CAH.Repositories.Entity;

namespace CAH.Contract.Services.Interface
{
	public interface IAppUserService
	{
		Task<ApplicationUsers> AuthenticateAsync(LoginModelView model);
	}
}
