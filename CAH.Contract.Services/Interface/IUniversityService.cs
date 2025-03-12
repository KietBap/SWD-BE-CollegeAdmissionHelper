using CAH.Core;
using CAH.ModelViews.UniversityModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface IUniversityService
	{
		Task<string> CreateUniversityAsync(CreateUniversityModelView model);
		Task<string> DeleteUniversityAsync(string id);
		Task<string> UpdateUniversityAsync(string id, CreateUniversityModelView model);
		Task<BasePaginatedList<UniversityMV>> GetAllUniversityAsync(int pageNumber, int pageSize, string? uniCode, string? type, string? location);
		Task<UniversityModelView> GetUniversityByIdAsync(string id);

    }
}
