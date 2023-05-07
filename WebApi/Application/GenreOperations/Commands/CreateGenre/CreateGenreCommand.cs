using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public CreateGenreModel Model { get; set; }
        public CreateGenreCommand(BookStoreDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut.");
            genre = new Entities.Genre()
            {
                Name = Model.Name,
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
