using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "Ozdisci")]
        [InlineData("Hasan", "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnsErrors(string name, string surname)
        {
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.AddYears(-1)
            };
            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = "test",
                Surname = "test",
                DateOfBirth = DateTime.Now.Date
            };
            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "hasan",
                Surname = "ozdisci",
                DateOfBirth = DateTime.Now.Date.AddYears(-2)
            };
            //act
            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}
