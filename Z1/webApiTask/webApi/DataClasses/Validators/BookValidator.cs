using FluentValidation;
using webApi.DataClasses.Entities;

namespace webApi.DataClasses.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(b => b.Title).NotEmpty();
        RuleFor(b => b.WriterId).NotNull().GreaterThan(0);
        RuleFor(b => b.Genre).NotEmpty();
        RuleFor(b => b.BookId).NotNull().GreaterThan(0);
    }
}
