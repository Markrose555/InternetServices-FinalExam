using midTerm.Models.Models.Option;
using midTerm.Models.Models.SurveyUser;

namespace midTerm.Models.Models.Answers
{
    public class AnswersExtended
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OptionId { get; set; }

        
        public OptionBaseModel Option { get; set; }
        public SurveyUserBaseModel User { get; set; }
    }
}
