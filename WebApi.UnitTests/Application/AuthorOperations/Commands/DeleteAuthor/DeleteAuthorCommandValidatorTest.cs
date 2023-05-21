using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            // arrange
            DeleteAuthorCommand command = new(null);
            command.AuthorId = authorId;
            //act
            DeleteAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            DeleteAuthorCommand command = new(null);
            command.AuthorId = 1;
            //act
            DeleteAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}
