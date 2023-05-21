using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTest(CommanTextFixture fixture)
        {
            _mapper = fixture.Mapper;
            _context = fixture.Context;
        }
        [Fact]
        public void WhenIdIsNotExist_InvalidOperationException_ShouldReturn()
        {
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = 1000;
            FluentActions.Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
        }
        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeReturned()
        {
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = 1;
            FluentActions.Invoking(() => query.Handle()).Should().NotBeNull();
        }
    }
}
