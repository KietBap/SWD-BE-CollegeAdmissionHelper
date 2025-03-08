using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.ModelViews.AcademicYearModelViews;
using CAH.ModelViews.AdmissionInforModelViews;
using CAH.ModelViews.AdmissionMethodMVs;
using CAH.ModelViews.AuthModelViews;
using CAH.ModelViews.MajorModelViews;
using CAH.ModelViews.ReviewModelViews;
using CAH.ModelViews.SubjectModelViews;
using CAH.ModelViews.SubScoreModelViews;
using CAH.ModelViews.UniMajorModelViews;
using CAH.ModelViews.UniversityModelViews;
using CAH.ModelViews.UserModelView;

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

			CreateMap<User, UserModelView>();
			CreateMap<UserModelView, User>();

			CreateMap<AcademicYear, AcademicYearModelView>();
			CreateMap<AcademicYearModelView, AcademicYear>();
            CreateMap<ListAcaYearModelView, AcademicYear>();
            CreateMap<AcademicYear, ListAcaYearModelView>();

            CreateMap<UniMajor, CreateUniMajorModelView>();
            CreateMap<UniMajor, UpdateUniMajorModelView>();
            CreateMap<CreateUniMajorModelView, UniMajor>();
            CreateMap<UpdateUniMajorModelView, UniMajor>();
            CreateMap<ListUniMajorModelView, UniMajor>();
            CreateMap<UniMajor, ListUniMajorModelView>();

            CreateMap<Review, CreateReviewModelView>();
            CreateMap<CreateReviewModelView, Review>();
            CreateMap<UpdateReviewModelView, Review>();
            CreateMap<Review, UpdateReviewModelView>();
            CreateMap<Review, ListReviewModelView>();
            CreateMap<ListReviewModelView, Review>();

            CreateMap<SubjectScore, CreateSubScoreModelView>();
            CreateMap<CreateSubScoreModelView, SubjectScore>();
            CreateMap<UpdateSubScoreModelView, SubjectScore>();
            CreateMap<SubjectScore, UpdateSubScoreModelView>();
            CreateMap<SubjectScore, ListSubScoreModelView>();
            CreateMap<ListSubScoreModelView, SubjectScore>();

            CreateMap<User, ListUserModelView>();
            CreateMap<ListUserModelView, User>();

            CreateMap<AdmissionInfor, CreateAdInforModelView>();
            CreateMap<CreateAdInforModelView, AdmissionInfor>();
            CreateMap<InforMethodModel, InforMethod>();
        }
    }
}
