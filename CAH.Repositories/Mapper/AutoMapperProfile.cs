using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.ModelViews.AdmissionMethodMVs;
using CAH.ModelViews.MajorModelViews;
using CAH.ModelViews.SubjectModelViews;
using CAH.ModelViews.UniversityModelViews;

namespace CAH.Repositories.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
			CreateMap<Major, CreateMajorModelView>();
			CreateMap<CreateMajorModelView, Major>();
			CreateMap<MajorModelView, Major>();
			CreateMap<Major, MajorModelView>();

			CreateMap<University, UniversityModelView>();
			CreateMap<UniversityModelView, University>();
			CreateMap<UniversityMV, University>();
			CreateMap<University, UniversityMV>();

			CreateMap<AdmissionMethod, AdmissionMethodMV>();
			CreateMap<AdmissionMethodMV, AdmissionMethod>();
			CreateMap<ListAdMethodMV, AdmissionMethod>();
			CreateMap<AdmissionMethod, ListAdMethodMV>();

			CreateMap<Subject, SubjectModelView>();
			CreateMap<SubjectModelView, Subject>();
			CreateMap<ListSubjectModelView, Subject>();
			CreateMap<Subject, ListSubjectModelView>();
		}
    }
}
