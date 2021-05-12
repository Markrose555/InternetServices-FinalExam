using midTerm.Models.Models.Question;

namespace midTerm.Models.Models.Option
{
    public class OptionModelExtended
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Order { get; set; }
        public int QuestionId { get; set; }

        public QuestionModelBase Question { get; set; }
    }
}
