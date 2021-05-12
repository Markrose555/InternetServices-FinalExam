using midTerm.Models.Test.Internal;
using System;
using Xunit;
using midTerm.Models.Profiles;

namespace midTerm.Models.Test
{
    public class AutoMapperConfigurationIsValid
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid_Test()
        {
            var configuration = AutoMapperModule.CreateMapperConfiguration<QuestionProfile>();
            configuration.AssertConfigurationIsValid();
        }
    }
}
