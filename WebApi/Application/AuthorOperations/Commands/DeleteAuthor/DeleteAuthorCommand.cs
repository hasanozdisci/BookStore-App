using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
