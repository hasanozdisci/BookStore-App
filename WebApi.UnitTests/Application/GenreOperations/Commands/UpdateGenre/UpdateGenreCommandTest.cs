using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTest(CommanTextFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGenreIsNotFound_Handle_WhenBookIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            UpdateGenreCommand command = new(_context);
            command.GenreId = 999;
            command.Model = new UpdateGenreModel
            {
                Name = "Fear"
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new(_context);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel
            {
                Name = "Fear",
                IsActive = true
            };
            FluentActions.Invoking(() => command.Handle()).Invoke();
            var genre = _context.Genres.SingleOrDefault(g => g.Id == command.GenreId);
            genre.Name.Should().Be(command.Model.Name);
        }
        [Fact]
        // if genre name is not unique, it should throw InvalidOperationException
        public void WhenInvalidInputsAreGiven_Genre_ShouldNotBeUpdated()
        {
            UpdateGenreCommand command = new(_context);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel
            {
                Name = "Personal Growth"
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimde kitap türü mevcut");
        }
    }
}
