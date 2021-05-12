namespace midTerm.Models.Models.Option
{
    public class OptionBaseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Order { get; set; }
        public int QuestionId { get; set; }
    }
}