using FluentValidation;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
        }
     
    }
}
