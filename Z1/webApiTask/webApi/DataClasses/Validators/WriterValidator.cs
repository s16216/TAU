using FluentValidation;
using webApi.DataClasses.Entities;

namespace webApi.DataClasses.Validators;

public class WriterValidator : AbstractValidator<Writer>
{
    public WriterValidator()
    {
        RuleFor(w => w.FullName).NotEmpty();
        RuleFor(w => w.Country).NotEmpty();
        RuleFor(w => w.WriterId).NotNull().GreaterThan(0);
    }
}
