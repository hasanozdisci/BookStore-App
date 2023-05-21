using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(0, "Hasan", "Ozdisci")]
        [InlineData(1, "", "Ozdisci")]
        [InlineData(1, "H", "Ozdisci")]
        [InlineData(1, "Hasan", "")]
        [InlineData(1, "Hasan", "O")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = id;
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.Date.AddYears(-1)
            };
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorModel
            {
                Name = "test1",
                Surname = "test1",
                DateOfBirth = DateTime.Now.Date
            };
            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = "hasan",
                Surname = "ozdisci",
                DateOfBirth = DateTime.Now.Date.AddYears(-2)
            };
            //act
            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Equals(0);
        }



    }
}
