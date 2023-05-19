using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTest(CommanTextFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenBookIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999;
            command.Model = new UpdateBookModel()
            {
                Title = "test",
                GenreId = 1
            };
            //act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 2;
            command.Model = new UpdateBookModel()
            {
                Title = "test",
                GenreId = 1
            };
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var book = _context.Books.SingleOrDefault(b => b.Id == command.BookId);
            book.Title.Should().Be(command.Model.Title);
            book.GenreId.Should().Be(command.Model.GenreId);
        }
    }
}
