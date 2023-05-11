using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest :
        IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("Lord Of The Rings", 0, 0, 1)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 1, 1)]
        [InlineData("Lor", 100, 1, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 1)]
        [InlineData("Lord Of The Rings", 100, 0, 1)]
        [InlineData("Lord Of The Rings", 100, 1, 0)]
        [InlineData("Lord Of The Rings", 100, 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.AddYears(-1)
            };
            //act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "test datetime",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date
            };
            //act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "test datetime",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2)
            };
            //act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
        
    }
}
