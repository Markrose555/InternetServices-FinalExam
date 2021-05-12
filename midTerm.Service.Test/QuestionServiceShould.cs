using System;
using Xunit;
using midTerm.Service.Test.Internal;
using midTerm.Services.Services;
using midTerm.Models.Profiles;
using midTerm.Models.Models.Question;
using AutoMapper;
using System.Threading.Tasks;
using FluentAssertions;
using System.Collections.Generic;

namespace midTerm.Service.Test
{
    public class QuestionServiceShould : SqlLiteContext
    {

        private readonly IMapper _mapper;
        private readonly QuestionService _service;

        public QuestionServiceShould()
        : base(true)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(QuestionProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new QuestionService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetQuestionById()
        {
            var expected = 1;

            var result = await _service.GetById(expected);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Models.Models.Question.QuestionModelExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetQuestions()
        {
            var expected = 3;
            var result = await _service.Get();
            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<Models.Models.Question.QuestionModelBase>>();
        }

        [Fact]
        public async Task InsertNewQuestion()
        {
            var question = new QuestionCreateModel
            {
                Text = "How often should every person brush their teeth?",
                Description = "(To maximize hygiene and reduce bacteria)"
            };

            var result = await _service.Insert(question);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<QuestionModelBase>();
            result.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateQuestion()
        {
            var question = new QuestionUpdateModel
            {
                Id = 3,
                Text = "What is the powerhouse of every cell?",
                Description = "Cells being the tiny organisms that make up all life."
            };

            var result = await _service.Update(question);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<QuestionModelBase>();
            result.Id.Should().Be(question.Id);
            result.Text.Should().Be(question.Text);
            result.Description.Should().Be(question.Description);
        }

        [Fact]
        public async Task DeleteQuestion()
        {
            var expected = 1;

            var result = await _service.Delete(expected);
            var question = await _service.GetById(expected);

            result.Should().Be(true);
            question.Should().BeNull();
        }
    }
}
