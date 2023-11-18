using FluentValidation;
using webApi.DataClasses;
using webApi.DataClasses.Validators;
using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;
using webApi.Services;

namespace webApi.Extensions;

public static class CustomExtensions
{
    public static IServiceCollection AdditionalServices(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddDbContext<DataContext>();
        services.AddTransient<IWritersService, WritersService>();
        services.AddTransient<IBooksService, BooksService>();
        services.AddSingleton<IValidator<WriterCl>, WriterClValidator>();
        services.AddSingleton<IValidator<Writer>, WriterValidator>();
        services.AddSingleton<IValidator<BookCl>, BookClValidator>();
        services.AddSingleton<IValidator<Book>, BookValidator>();

        return services;
    }
}