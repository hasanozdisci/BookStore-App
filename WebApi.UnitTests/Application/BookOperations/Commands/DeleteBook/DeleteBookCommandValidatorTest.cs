using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            DeleteBookCommand command = new(null);
            command.BookId = bookId;
            //act
            DeleteBookCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
