using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommandTest(CommanTextFixture fixture)
        {
            _mapper = fixture.Mapper;
            _context = fixture.Context;
        }
        [Fact]
        // when id is not exist in database then throw invalid operation exception
        public void WhenIdIsNotExist_InvalidOperationException_ShouldReturn()
        {
            // arrange
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = 999;

            // assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı");
        }
        [Fact]
        // when valid id is given then author should be deleted
        public void WhenValidIdIsGiven_Author_ShouldBeDeleted()
        {
            // arrange
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(b => b.Id == command.AuthorId);
            author.Should().BeNull();
        }

    }
}
