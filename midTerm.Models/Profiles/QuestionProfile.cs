using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Question;

namespace midTerm.Models.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionModelBase>()
                .ReverseMap();

            CreateMap<Question, QuestionModelExtended>()
                .ReverseMap();

            CreateMap<QuestionCreateModel, Question>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            
            CreateMap<QuestionUpdateModel, Question>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));


        }
    }
}
