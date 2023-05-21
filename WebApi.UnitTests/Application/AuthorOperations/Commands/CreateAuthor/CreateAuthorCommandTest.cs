using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTest(CommanTextFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var author = new Author
            {
                Name = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldReturn",
                Surname = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldReturn",
                DateOfBirth = new DateTime(1999, 01, 10)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            //act (Çalıştırma)
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel() { Name = author.Name, Surname = author.Surname };

            //assert (Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut");
        }
        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new(_context, _mapper);
            CreateAuthorModel model = new()
            {
                Name = "Test_WhenValidInputAreGiven_Author_ShouldBeCreated",
                Surname = "Test_WhenValidInputAreGiven_Author_ShouldBeCreated",
                DateOfBirth = DateTime.Now.Date.AddYears(-10)
            };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name && x.Surname == model.Surname);
            author.Should().NotBeNull();
        }
    }
}
