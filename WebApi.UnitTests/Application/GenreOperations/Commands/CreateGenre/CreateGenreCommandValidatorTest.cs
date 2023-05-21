using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("ab")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // arrange
            CreateGenreCommand command = new(null);
            command.Model = new CreateGenreModel()
            {
                Name = name
            };
            //act
            CreateGenreCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            CreateGenreCommand command = new(null);
            command.Model = new CreateGenreModel()
            {
                Name = "valid name"
            };
            //act
            CreateGenreCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}
