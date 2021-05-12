using midTerm.Services.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using midTerm.Controllers;
using System.Threading.Tasks;
using FluentAssertions;

using midTerm.Models.Models.Question;
using midTerm.Models.Models.Option;
using midTerm.Models.Models.SurveyUser;
using midTerm.Models.Models.Answers;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace midTerm.Controller.Test
{
    public class QuestionControllerShould
    {

        private readonly Mock<IQuestionService> _mockService;
        private readonly QuestionsController _controller;

        public QuestionControllerShould()
        {
            _mockService = new Mock<IQuestionService>();

            _controller = new QuestionsController(_mockService.Object);
        }

        [Fact]
        public async Task ReturnBaseQuestionByIdWhenHasData()
        {
            int expectedId = 1;

            var option = new Faker<QuestionModelExtended>()
                .RuleFor(s => s.Id, v => ++v.IndexVariable)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1));

            var question = new Faker<QuestionModelExtended>()
                .RuleFor(s => s.Id, v => ++v.IndexVariable)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate(3);

            //cannot figure this out. spent a lot of time on it
            _mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(question.Find(x => x.Id == expectedId))
                .Verifiable();

            var result = await _controller.GetById(expectedId);
            result.Should().BeOfType<OkObjectResult>();
            var model = result as OkObjectResult;
            model?.Value.Should().BeOfType<QuestionModelExtended>().Subject.Id.Should().Be(expectedId);
        }

        [Fact]
        public async Task ReturnNoContentWhenHasNoData()
        {
            int expectedId = 1;

            var option = new Faker<QuestionModelExtended>()
                .RuleFor(s => s.Id, v => ++v.IndexVariable)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1));

            var question = new Faker<QuestionModelExtended>()
                .RuleFor(s => s.Id, v => ++v.IndexVariable)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate(3);

            //cannot figure this out. spent a lot of time on it
            _mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(question.Find(x => x.Id == expectedId))
                .Verifiable();

            var result = await _controller.GetById(expectedId);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReturnQuestionsWhenHasData()
        {
            int expectedCount = 10;
            var questions = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, v => ++v.IndexVariable)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate(expectedCount);

            _mockService.Setup(x => x.Get())
                .ReturnsAsync(questions)
                .Verifiable();

            var result = await _controller.Get();

            result.Should().BeOfType<OkObjectResult>();

            var model = result as OkObjectResult;
            model?.Value.Should().BeOfType<List<QuestionModelBase>>().Subject.Count().Should().Be(expectedCount);

        }

        [Fact]
        public async Task ReturnEmptyListWhenHasNoData()
        {
            var questions = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, v => ++v.IndexVariable)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate(0);

            _mockService.Setup(x => x.Get())
                .ReturnsAsync(questions)
                .Verifiable();

            var result = await _controller.Get();

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReturnCreatedQuestionOnCreateWhenCorrectModel()
        {
            var question = new Faker<QuestionCreateModel>()
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            var questionExpected = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<QuestionCreateModel>()))
                .ReturnsAsync(questionExpected)
                .Verifiable();

            var result = await _controller.Post(question);

            result.Should().BeOfType<CreatedResult>();

            var model = result as CreatedResult;

            model?.Value.Should().Be(1);
            model?.Location.Should().Be("/api/Questions/1");
        }

        [Fact]
        public async Task ReturnConflictOnCreateWhenRepositoryError()
        {
            var question = new Faker<QuestionCreateModel>()
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<QuestionCreateModel>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _controller.Post(question);

            result.Should().BeOfType<ConflictResult>();
        }

        [Fact]
        public async Task ReturnBadRequestOnCreateWhenModelNotValid()
        {
            _controller.ModelState.AddModelError("genericError", "Enjoy this error");
            var question = new Faker<QuestionCreateModel>()
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            var questionExpected = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<QuestionCreateModel>()))
                .ReturnsAsync(questionExpected)
                .Verifiable();

            var result = await _controller.Post(question);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task ReturnBadRequestOnUpdateWhenModelNotValid()
        {
            _controller.ModelState.AddModelError("genericError", "Enjoy this error");
            var question = new Faker<QuestionUpdateModel>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            var questionExpected = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<QuestionCreateModel>()))
                .ReturnsAsync(questionExpected)
                .Verifiable();

            var result = await _controller.Put(question.Id, question);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task ReturnQuestionOnUpdateWhenCorrectModel()
        {
            var question = new Faker<QuestionUpdateModel>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            var questionExpected = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<QuestionCreateModel>()))
                .ReturnsAsync(questionExpected)
                .Verifiable();

            var result = await _controller.Put(question.Id, question);

            result.Should().BeOfType<OkObjectResult>();

            var model = result as OkObjectResult;
            model?.Value.Should().Be(questionExpected);
        }

        [Fact]
        public async Task ReturnNoContentOnUpdateWhenRepositoryError()
        {
            var question = new Faker<QuestionUpdateModel>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            var questionExpected = new Faker<QuestionModelBase>()
                .RuleFor(s => s.Id, 1)
                .RuleFor(s => s.Text, v => v.Lorem.Lines(1))
                .RuleFor(s => s.Description, v => v.Lorem.Lines(1))
                .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<QuestionCreateModel>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _controller.Put(question.Id, question);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReturnOkWhenDeletedData()
        {
            int id = 1;
            bool expected = true;

            _mockService.Setup(x => x.Delete(It.IsAny<int>()))
                 .ReturnsAsync(expected)
                 .Verifiable();

            var result = await _controller.Delete(id);

            result.Should().BeOfType<OkObjectResult>();

            var model = result as OkObjectResult;
            model?.Value.Should().Be(expected);
        }

        [Fact]
        public async Task ReturnOkFalseWhenNoData()
        {
            int id = 1;
            bool expected = false;

            _mockService.Setup(x => x.Delete(It.IsAny<int>()))
                 .ReturnsAsync(expected)
                 .Verifiable();

            var result = await _controller.Delete(id);

            result.Should().BeOfType<OkObjectResult>();

            var model = result as OkObjectResult;
            model?.Value.Should().Be(expected);
        }

        [Fact]
        public async Task ReturnBadResultWhenModelNotValid()
        {
            _controller.ModelState.AddModelError("genericError", "Enjoy this error");
            var result = await _controller.Delete(1);
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
