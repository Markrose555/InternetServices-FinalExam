using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Answers;

namespace midTerm.Models.Profiles
{
    public class AnswersProfile : Profile
    {
        public AnswersProfile()
        {
            CreateMap<Answers, AnswersBaseModel>()
                .ReverseMap();
            CreateMap<Answers, AnswersExtended>()
                .ReverseMap();

            CreateMap<AnswerCreateModel, Answers>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Option, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

                
            CreateMap<AnswersUpdateModel, Answers>()
                .ForMember(dest => dest.Option, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }

    }
}