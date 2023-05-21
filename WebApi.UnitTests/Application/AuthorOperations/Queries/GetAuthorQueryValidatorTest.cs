using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries
{
    public class GetAuthorQueryValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            // arrange
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = authorId;
            //act
            GetAuthorDetailQueryValidator validator = new();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
        {
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = 1;
            //act
            GetAuthorDetailQueryValidator validator = new();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}
