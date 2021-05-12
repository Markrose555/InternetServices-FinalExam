using System.Collections.Generic;
using midTerm.Models.Models.Option;

namespace midTerm.Models.Models.Question
{
    public class QuestionModelExtended
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public ICollection<OptionBaseModel> Options { get; set; }
    }
}
