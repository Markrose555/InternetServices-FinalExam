using System;
using System.Collections.Generic;
using midTerm.Data.Enums;
using midTerm.Models.Models.Answers;

namespace midTerm.Models.Models.SurveyUser
{
    public class SurveyUserExtended
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DoB { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }

        public virtual ICollection<AnswersBaseModel> Answers { get; set; }
    }
}
