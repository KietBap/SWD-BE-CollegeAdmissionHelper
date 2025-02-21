using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.ModelViews.MajorModelViews;

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
		}
    }
}
