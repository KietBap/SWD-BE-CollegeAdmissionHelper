using CAH.Core;
using CAH.ModelViews.SubjectModelViews;

namespace CAH.Contract.Services.Interface
{
	public interface ISubjectService
	{
		Task<string> CreateSubjectAsync(SubjectModelView model);
		Task<string> DeleteSubjectAsync(string id);
		Task<string> UpdateSubjectAsync(string id, SubjectModelView model);
		Task<BasePaginatedList<ListSubjectModelView>> GetAllSubjectAsync(int pageNumber, int pageSize);
		Task<Object> GetSubjectByIdAsync(string id);
	}
}
