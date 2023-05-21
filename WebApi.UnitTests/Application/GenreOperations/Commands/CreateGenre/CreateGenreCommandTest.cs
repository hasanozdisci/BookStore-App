using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTest(CommanTextFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            var genre = new Genre
            {
                Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldReturn",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new(_context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut.");
        }
        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new(_context);
            CreateGenreModel model = new()
            {
                Name = "Action"
            };
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            var genre = _context.Genres.FirstOrDefault(g => g.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}
