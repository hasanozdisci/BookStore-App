using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
