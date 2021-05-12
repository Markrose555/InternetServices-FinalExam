using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Option;

namespace midTerm.Models.Profiles
{
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionBaseModel>()
                .ReverseMap();
            CreateMap<Option, OptionModelExtended>()
                .ReverseMap();

            CreateMap<OptionCreateModel, Option>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore());

            CreateMap<OptionUpdateModel, Option>()
                .ForMember(dest => dest.Question, opt => opt.Ignore()); ;
        }
       
    }
}
