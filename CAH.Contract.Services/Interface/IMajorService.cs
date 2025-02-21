using CAH.ModelViews.MajorModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface IMajorService
	{
		Task<string> CreateMajorAsync(CreateMajorModelView model);
		Task<string> DeleteMajorAsync(string id, string userId);
		Task<string> UpdateMajorAsync(string id, CreateMajorModelView model);
	}
}
