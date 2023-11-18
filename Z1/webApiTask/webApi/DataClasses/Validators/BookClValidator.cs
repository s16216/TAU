using FluentValidation;
using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;

namespace webApi.DataClasses.Validators;

public class BookClValidator : AbstractValidator<BookCl>
{
    public BookClValidator()
    {
        RuleFor(b => b.Title).NotEmpty();
        RuleFor(b => b.WriterId).NotNull().GreaterThan(0);
        RuleFor(b => b.Genre).NotEmpty();
    }
}
