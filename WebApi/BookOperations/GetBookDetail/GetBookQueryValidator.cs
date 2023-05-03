using FluentValidation;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
