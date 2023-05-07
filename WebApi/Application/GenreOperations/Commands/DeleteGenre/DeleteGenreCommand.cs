using System;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public int GenreId { get; set; }
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.Find(GenreId);
            if(genre is null)
                throw new InvalidOperationException("Silinecek tür bulunamadı");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}
