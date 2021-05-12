using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Option;

namespace midTerm.Models.Profiles
{
    class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionBaseModel>()
                .ReverseMap();
            CreateMap<Option, OptionModelExtended>()
                .ReverseMap();

            CreateMap<OptionCreateModel, Option>();
            CreateMap<OptionUpdateModel, Option>();
        }
       
    }
}
