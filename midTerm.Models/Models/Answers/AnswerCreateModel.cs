using System.ComponentModel.DataAnnotations;

namespace midTerm.Models.Models.Answers
{
    public class AnswerCreateModel
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int OptionId { get; set; }
    }
}