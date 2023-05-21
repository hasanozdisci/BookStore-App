using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTest(CommanTextFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAuthorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 999;
            command.Model = new UpdateAuthorModel()
            {
                Name = "test",
                Surname = "test",
                DateOfBirth = DateTime.Now.Date.AddYears(-1)
            };
            //act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek yazar bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 2;
            command.Model = new UpdateAuthorModel()
            {
                Name = "test",
                Surname = "test",
                DateOfBirth = DateTime.Now.Date.AddYears(-1)
            };
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var author = _context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
            author.Name.Should().Be(command.Model.Name);
            author.Surname.Should().Be(command.Model.Surname);
            author.DateOfBirth.Should().Be(command.Model.DateOfBirth);
        }
    }
}
