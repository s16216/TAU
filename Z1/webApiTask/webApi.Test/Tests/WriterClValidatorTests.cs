using System;
using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
using webApi.DataClasses.EntitiesCl;
using webApi.DataClasses.Validators;
using Xunit;

namespace webApi.Test.Tests;

public class WriterClValidatorTests
{
    private WriterClValidator validator;

    public WriterClValidatorTests()
    {
        validator = new WriterClValidator();
    }


    
    [Fact]
    public void Validate_Should_Return_Errors()
    {
        WriterCl writer = new WriterCl()
        {
            FullName = "",
            Country = "",
            DateOfBirth = new DateTime(1977, 3, 1)
        };
    
        ValidationResult result = validator.Validate(writer);
    
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Validate_Should_Return_No_Errors()
    {
        WriterCl writer = new WriterCl()
        {
            FullName = "Jan Kowalski",
            Country = "Polska",
            DateOfBirth = new DateTime(1977, 3, 1)
        };

        ValidationResult result = validator.Validate(writer);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Should_Return_Errors_For_FullName()
    {
        WriterCl writer = new WriterCl()
        {
            FullName = "",
            Country = "USA",
            DateOfBirth = new DateTime(1980, 5, 15)
        };

        ValidationResult result = validator.Validate(writer);

        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(error => error.PropertyName == "FullName"));
    }

    [Fact]
    public void Validate_Should_Return_Errors_For_Country()
    {
        WriterCl writer = new WriterCl()
        {
            FullName = "Alice Smith",
            Country = "",
            DateOfBirth = new DateTime(1985, 5, 15)
        };

        ValidationResult result = validator.Validate(writer);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "Country");
    }
    
    

    [Fact]
    public void Validate_Should_Return_Errors_For_Date_Null()
    {
        WriterCl writer = new WriterCl()
        {
            FullName = "John Doe",
            Country = null,
            //DateOfBirth = new DateTime(1980, 7, 20)
        };

        ValidationResult result = validator.Validate(writer);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors, error => error.PropertyName == "Country");
    }
}