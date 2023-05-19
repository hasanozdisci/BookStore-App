using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTest(CommanTextFixture fixture)
        {
            _mapper = fixture.Mapper;
            _context = fixture.Context;
        }
        [Fact]
        // when id is not exist in database then throw invalid operation exception
        public void WhenIdIsNotExist_InvalidOperationException_ShouldReturn()
        {
            // arrange
            DeleteBookCommand command = new(_context);
            command.BookId = 999;
            
            // assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı");
        }
        [Fact]
        // when valid id is given then book should be deleted
        public void WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            // arrange
            DeleteBookCommand command = new(_context);
            command.BookId = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(b => b.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}
