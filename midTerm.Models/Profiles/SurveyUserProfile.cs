using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.SurveyUser;

namespace midTerm.Models.Profiles
{
    class SurveyUserProfile : Profile
    {
        public SurveyUserProfile()
        {
            CreateMap<SurveyUser, SurveyUserBaseModel>()
                .ReverseMap();
            CreateMap<SurveyUser, SurveyUserExtended>()
                .ReverseMap();

            CreateMap<SurveyUserCreate, SurveyUser>();
            CreateMap<SurveyUserUpdate, SurveyUser>();
        }

    }
}