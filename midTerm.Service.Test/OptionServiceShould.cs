using System;
using Xunit;
using midTerm.Service.Test.Internal;
using midTerm.Services.Services;
using midTerm.Models.Profiles;
using midTerm.Models.Models.Option;
using AutoMapper;
using System.Threading.Tasks;
using FluentAssertions;
using System.Collections.Generic;

namespace midTerm.Service.Test
{
    public class OptionServiceShould : SqlLiteContext
    {

        private readonly IMapper _mapper;
        private readonly OptionService _service;

        public OptionServiceShould()
        : base(true)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(OptionProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new OptionService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetOptionById()
        {
            var expected = 1;

            var result = await _service.GetById(expected);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Models.Models.Option.OptionModelExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetOptions()
        {
            var expected = 3;
            var result = await _service.Get();
            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<Models.Models.Option.OptionBaseModel>>();
        }

        [Fact]
        public async Task InsertNewOption()
        {
            var option = new OptionCreateModel
            {
                Order = 3,
                QuestionId = 3,
                Text = "InternetServices4187"
            };

            var result = await _service.Insert(option);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionBaseModel>();
            result.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateOption()
        {
            var option = new OptionUpdateModel
            {
                Id = 2,
                Order = 2,
                QuestionId = 3,
                Text = "Magenta"
            };

            var result = await _service.Update(option);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionBaseModel>();
            result.Id.Should().Be(option.Id);
            result.Order.Should().Be(option.Order);
            result.QuestionId.Should().Be(option.QuestionId);
        }

        [Fact]
        public async Task DeleteOption()
        {
            var expected = 1;

            var result = await _service.Delete(expected);
            var option = await _service.GetById(expected);

            result.Should().Be(true);
            option.Should().BeNull();
        }
    }
}
