using CAH.Core;
using CAH.ModelViews.AdmissionMethodMVs;

namespace CAH.Contract.Services.Interface
{
	public interface IAdmissionMethodService
	{
		Task<string> CreateAdmissionMethodAsync(AdmissionMethodMV model);
		Task<string> UpdateAdmissionMethodAsync(string id, AdmissionMethodMV model);
		Task<string> DeleteAdmissionMethodAsync(string id);
		Task<BasePaginatedList<ListAdMethodMV>> GetAllAdMethodAsync(int pageNumber, int pageSize);
		Task<Object> GetAdMethodByIdAsync(string id);
	}
}
