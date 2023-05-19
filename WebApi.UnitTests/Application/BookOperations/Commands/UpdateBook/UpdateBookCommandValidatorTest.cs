using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 1, 80)]
        [InlineData("Lord Of The Rings", 0, 3)]
        [InlineData("", 1, 0)]
        [InlineData("", 0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int authorId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = "Lord Of The Rings",
                GenreId = 1,
                AuthorId = 1
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }

    }
}
