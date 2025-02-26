using CAH.Contract.Repositories.Entity;
using CAH.ModelViews.AuthModelViews;
using CAH.ModelViews.TokenModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface IAppUserService
	{
		Task<User> AuthenticateAsync(LoginModelView model);
		Task<TokenModelView> LoginGoogleAsync(GoogleToken model);

    }
}
