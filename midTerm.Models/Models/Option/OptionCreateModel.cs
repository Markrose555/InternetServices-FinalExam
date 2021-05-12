using System.ComponentModel.DataAnnotations;

namespace midTerm.Models.Models.Option
{
    public class OptionCreateModel
    {
        [Required]
        public string Text { get; set; }
        public int? Order { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}
