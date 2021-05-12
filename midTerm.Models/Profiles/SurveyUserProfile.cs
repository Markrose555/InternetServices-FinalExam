using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.SurveyUser;

namespace midTerm.Models.Profiles
{
    public class SurveyUserProfile : Profile
    {
        public SurveyUserProfile()
        {
            CreateMap<SurveyUser, SurveyUserBaseModel>()
                .ReverseMap();
            CreateMap<SurveyUser, SurveyUserExtended>()
                .ReverseMap();

            CreateMap<SurveyUserCreate, SurveyUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Answers, opt => opt.Ignore());
            CreateMap<SurveyUserUpdate, SurveyUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Answers, opt => opt.Ignore());
        }

    }
}