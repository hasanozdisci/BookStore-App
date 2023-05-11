using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _context;
        public int BookId { get; set; }
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı");
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
