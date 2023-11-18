using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using webApi.DataClasses.Entities;

namespace webApi.DataClasses;

public class DataContext : DbContext
{
    public DbSet<Writer> Writers { get; set; }
    public DbSet<Book> Books { get; set; }

    public string DbPath { get; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        var optionsForTests = options.FindExtension<InMemoryOptionsExtension>();

        DbPath = @"Data/base.db";

        if ((optionsForTests is null) && !Directory.Exists(Path.GetDirectoryName(DbPath)))
            throw new Exception("'Data' folder for database file not exists.");

        if (optionsForTests is null)
            this.Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
            options.UseSqlite($"Data Source={DbPath}");
    }
}
