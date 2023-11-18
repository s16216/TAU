using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using webApi.Controllers;
using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;
using webApi.Services;
using Xunit;

namespace webApi.Test.Tests;

public class WritersControllerTests
{
    private WritersController controller;
    //private Mock<IBooksService> booksService;
    private Mock<IWritersService> writersService;
    private Mock<IValidator<WriterCl>> clValidator;
    private Mock<IValidator<Writer>> validator;

    public WritersControllerTests()
    {
        writersService = new Mock<IWritersService>();
        clValidator = new Mock<IValidator<WriterCl>>();
        validator = new Mock<IValidator<Writer>>();
        controller = new WritersController(writersService.Object, clValidator.Object, validator.Object);
    }

    [Fact]
    public async void AddWriter_Should_Return_OkObjectResult()
    {
        WriterCl writer = new WriterCl()
        {
            FullName = "Jan Kowalski",
            Country = "Polska",
            DateOfBirth = new DateTime(1975, 11, 11)
        };

        writersService.Setup(x => x.AddWriter(It.IsAny<WriterCl>())).ReturnsAsync(true);

        clValidator.Setup(x => x.Validate(It.IsAny<WriterCl>()))
                    .Returns(new ValidationResult()
                    {
                        Errors = new List<ValidationFailure>()
                    });


        var actionResult = await controller.AddWriter(writer);

        actionResult.Should().NotBeNull();

        var result = actionResult as ObjectResult;

        result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
    }
}
