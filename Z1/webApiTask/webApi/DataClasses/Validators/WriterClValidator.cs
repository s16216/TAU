using FluentValidation;
using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;

namespace webApi.DataClasses.Validators;

public class WriterClValidator : AbstractValidator<WriterCl>
{
    public WriterClValidator()
    {
        RuleFor(w => w.FullName).NotEmpty();
        RuleFor(w => w.Country).NotEmpty();
    }
}
