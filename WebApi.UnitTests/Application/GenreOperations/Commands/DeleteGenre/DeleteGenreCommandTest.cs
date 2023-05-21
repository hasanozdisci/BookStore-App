using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommandTest(CommanTextFixture fixture)
        {
            _mapper = fixture.Mapper;
            _context = fixture.Context;
        }
        [Fact]
        // when id is not exist in database then throw invalid operation exception
        public void WhenIdIsNotExist_InvalidOperationException_ShouldReturn()
        {
            // arrange
            DeleteGenreCommand command = new(_context);
            command.GenreId = 999;

            // assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek tür bulunamadı");
        }
        [Fact]
        // when valid id is given then genre should be deleted
        public void WhenValidIdIsGiven_Genre_ShouldBeDeleted()
        {
            // arrange
            DeleteGenreCommand command = new(_context);
            command.GenreId = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var genre = _context.Genres.SingleOrDefault(b => b.Id == command.GenreId);
            genre.Should().BeNull();
        }
    }
}
