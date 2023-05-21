using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = genreId;
            //act
            GetGenreDetailQueryValidator validator = new();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 1;
            //act
            GetGenreDetailQueryValidator validator = new();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}
