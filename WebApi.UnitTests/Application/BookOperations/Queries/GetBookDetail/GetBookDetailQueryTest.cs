using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommanTextFixture fixture)
        {
            _mapper = fixture.Mapper;
            _context = fixture.Context;
        }
        [Fact]
        public void WhenIdIsNotExist_InvalidOperationException_ShouldReturn()
        {
            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = 1000;
            FluentActions.Invoking(() => query.Handle())
                .Should()
                .Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }
        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeReturned()
        {
            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = 1;
            FluentActions.Invoking(() => query.Handle()).Should().NotBeNull();
        }
    }
}
