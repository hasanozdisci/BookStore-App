using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            DeleteGenreCommand command = new(null);
            command.GenreId = genreId;
            //act
            DeleteGenreCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            DeleteGenreCommand command = new(null);
            command.GenreId = 1;
            //act
            DeleteGenreCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}
