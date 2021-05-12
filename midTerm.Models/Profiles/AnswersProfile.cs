using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Answers;

namespace midTerm.Models.Profiles
{
    class AnswersProfile : Profile
    {
        public AnswersProfile()
        {
            CreateMap<Answers, AnswersBaseModel>()
                .ReverseMap();
            CreateMap<Answers, AnswersExtended>()
                .ReverseMap();

            CreateMap<AnswerCreateModel, Answers>();
            CreateMap<AnswersUpdateModel, Answers>();
        }

    }
}