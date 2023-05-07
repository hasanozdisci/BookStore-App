using System.Linq;
using System;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.Where(g => g.Id == GenreId).FirstOrDefault();
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
            
            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimde kitap türü mevcut");
            
            genre.Name = Model.Name != default ? Model.Name : genre.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();

        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
