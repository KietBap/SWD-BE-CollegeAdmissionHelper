using CAH.Core;
using CAH.ModelViews.UniversityModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface IUniversityService
	{
		Task<string> CreateUniversityAsync(UniversityModelView model);
		Task<string> DeleteUniversityAsync(string id);
		Task<string> UpdateUniversityAsync(string id, UniversityModelView model);
		Task<BasePaginatedList<UniversityMV>> GetAllUniversityAsync(int pageNumber, int pageSize);
		Task<Object> GetUniversityByIdAsync(string id);
	}
}
