using System.ComponentModel.DataAnnotations;

namespace midTerm.Models.Models.Answers
{
    public class AnswersUpdateModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int OptionId { get; set; }

    }
}