using CAH.Contract.Repositories.Entity;
using CAH.ModelViews.AuthModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface IAppUserService
	{
		Task<User> AuthenticateAsync(LoginModelView model);
	}
}
