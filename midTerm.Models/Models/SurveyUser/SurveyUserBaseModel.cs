using System;
using midTerm.Data.Enums;

namespace midTerm.Models.Models.SurveyUser
{
    public class SurveyUserBaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DoB { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }
    }
}