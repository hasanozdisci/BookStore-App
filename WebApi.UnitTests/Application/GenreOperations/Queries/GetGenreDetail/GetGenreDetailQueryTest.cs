using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTest(CommanTextFixture fixture)
        {
            _mapper = fixture.Mapper;
            _context = fixture.Context;
        }
        [Fact]
        public void WhenIdIsNotExist_InvalidOperationException_ShouldReturn()
        {
            GetGenreDetailQuery query = new(_mapper,_context);
            query.GenreId = 1000;
            FluentActions.Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }
        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeReturned()
        {
            GetGenreDetailQuery query = new(_mapper, _context);
            query.GenreId = 1;
            FluentActions.Invoking(() => query.Handle()).Should().NotBeNull();
        }
    }
}
