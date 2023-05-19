using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            GetBookDetailQuery query = new(null, null);
            query.BookId = bookId;
            //act
            GetBookQueryValidator validator = new();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
