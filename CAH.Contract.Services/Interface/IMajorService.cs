using CAH.Core;
using CAH.ModelViews.MajorModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface IMajorService
	{
		Task<string> CreateMajorAsync(CreateMajorModelView model);
		Task<string> DeleteMajorAsync(string id);
		Task<string> UpdateMajorAsync(string id, CreateMajorModelView model);
		Task<BasePaginatedList<MajorModelView>> GetAllMajorAsync(int pageNumber, int pageSize);
		Task<Object> GetMajorByIdAsync(string id);
	}
}
