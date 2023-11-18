using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using webApi.DataClasses;
using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;
using webApi.Services;
using Xunit;

namespace webApi.Test.Tests;

public class WritersServiceTests
{
    private DataContext context;
    private IWritersService writersService;
    private Mock<IBooksService> booksService;

    private static IEnumerable<object[]> Writers()
    {
        yield return new object[]
        {
            new WriterCl
            {
                FullName = "Jan Kowalski",
                Country = "Polska",
                DateOfBirth = DateTime.Parse("1955-07-21")
            }
        };
        yield return new object[]
        {
            new WriterCl
            {
                FullName = "Zenon Zen",
                Country = "Zanumbia",
                DateOfBirth = DateTime.Parse("1975-05-03")
            }
        };
        yield return new object[]
        {
            new WriterCl
            {
                FullName = "Andrzej Andrzejewski",
                Country = "Polska",
                DateOfBirth = DateTime.Parse("1983-10-01")
            }
        };
    }

    public WritersServiceTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        context = new DataContext(options);

        booksService = new Mock<IBooksService>();
        writersService = new WritersService(booksService.Object, context);
    }

    [Theory]
    [MemberData(nameof(Writers))]
    public async void AddWriter_Should_Returns_True(WriterCl writer)
    {
        var result = await writersService.AddWriter(writer);

        result.Should().BeTrue();
    }

    [Fact]
    public void GetWriters_Returns_Three_Writers()
    {
        foreach (var writer in Writers())
        {
            writersService.AddWriter((writer[0] as WriterCl)!);
        }

        var result = writersService.GetWriters();

        result.Should().NotBeNull();
        result!.Length.Should().Be(3);
        result![1].Country.Should().Be("Zanumbia");
    }

    [Fact]
    public void GetWriters_Returns_Writers()
    {
        foreach (var writer in Writers())
        {
            writersService.AddWriter((writer[0] as WriterCl)!);
        }

        var result = writersService.GetWriters();

        result.Should().NotBeNull();
        result!.Length.Should();
    }


    [Fact]
    public void AddWriter_Should_Return_bool()
    {
        var result = writersService.AddWriter(new WriterCl());

        result.Should().NotBeOfType<bool>();
    }
    

    [Fact]
    public void DeleteWriter_Should_Return_bool()
    {
        var result = writersService.DeleteWriter(1);

        result.Should().NotBeOfType<bool>();
    }
    

    [Fact]
    public void UpdateWriter_Should_Return_bool()
    {
        var result = writersService.UpdateWriter(new Writer());

        result.Should().NotBeOfType<bool>();
    }
    
    /////////////////////
    
    [Fact]
    public void GetWriters_Returns_Correct_Writer_Data()
    {
        var expectedWriter = new WriterCl
        {
            FullName = "Marian Marianowski",
            Country = "Polska",
            DateOfBirth = DateTime.Parse("1990-01-15")
        };

        writersService.AddWriter(expectedWriter);

        var result = writersService.GetWriters();

        result.Should().ContainEquivalentOf(expectedWriter);
    }

    ////
    
            [Theory]
        [MemberData(nameof(Writers))]
        public async void AddWriter_Should_Returns_True1(WriterCl writer)
        {
            var result = await writersService.AddWriter(writer);

            result.Should().BeTrue();
        }

        [Fact]
        public void GetWriters_Returns_Three_Writers1()
        {
            foreach (var writer in Writers())
            {
                writersService.AddWriter((writer[0] as WriterCl)!);
            }

            var result = writersService.GetWriters();

            result.Should().NotBeNull();
            result!.Length.Should().Be(3);
            result![1].Country.Should().Be("Zanumbia");
        }

        [Fact]
        public void GetWriters_Returns_Writers1()
        {
            foreach (var writer in Writers())
            {
                writersService.AddWriter((writer[0] as WriterCl)!);
            }

            var result = writersService.GetWriters();

            result.Should().NotBeNull();
            result!.Length.Should().BeGreaterThan(0); // Use a more specific assertion for the actual count if available
        }

        [Fact]
        public void AddWriter_Should_Return_bool1()
        {
            var result = writersService.AddWriter(new WriterCl());

            result.Should().BeOfType<Task<bool>>();
        }

        [Fact]
        public void DeleteWriter_Should_Return_TaskBool()
        {
            var result = writersService.DeleteWriter(1);

            result.Should().BeOfType<Task<bool>>();
        }

        [Fact]
        public void UpdateWriter_Should_Return_TaskBool()
        {
            var result = writersService.UpdateWriter(new Writer());

            result.Should().BeOfType<Task<bool>>();
        }

        [Fact]
        public void DeleteWriter_Should_Decrease_Writers_Count()
        {
            foreach (var writer in Writers())
            {
                writersService.AddWriter((writer[0] as WriterCl)!);
            }

            var initialCount = writersService.GetWriters()?.Length ?? 0;
            writersService.DeleteWriter(1);
            var finalCount = writersService.GetWriters()?.Length ?? 0;

            finalCount.Should().Be(initialCount - 1);
        }


}