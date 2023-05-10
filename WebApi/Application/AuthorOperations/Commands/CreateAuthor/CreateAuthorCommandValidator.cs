using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.DateOfBirth.Date).NotEmpty().LessThan(System.DateTime.Now.Date);
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Surname).NotEmpty();
        }
    }
}
