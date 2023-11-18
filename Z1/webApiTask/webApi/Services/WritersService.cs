using webApi.DataClasses;
using webApi.DataClasses.Entities;
using AutoMapper;
using webApi.DataClasses.EntitiesCl;

namespace webApi.Services;

public class WritersService : IWritersService
{
    private DataContext dataContext;
    private IBooksService _booksService;
    private Mapper mapper;

    public WritersService(IBooksService booksService, DataContext context)
    {
        dataContext = context;
        _booksService = booksService;

        var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<WriterCl, Writer>()
                );
        mapper = new Mapper(config);
    }

    private bool IsFullNameExists(string name)
    {
        return dataContext.Writers.ToList().Exists(w => w.FullName.ToUpper().Equals(name.ToUpper()));
    }

    public async Task<bool> AddWriter(WriterCl writerCl)
    {
        Writer writer = mapper.Map<Writer>(writerCl);

        if (IsFullNameExists(writer.FullName))
            return false;

        dataContext.Writers.Add(writer);

        try
        {
            var result = await dataContext.SaveChangesAsync();
            return result == 0 ? false : true;
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return false;
        }
    }

    public async Task<bool> DeleteWriter(int id)
    {
        var writer = dataContext.Writers.Find(id);

        if (writer is null)
            return false;

        try
        {
            dataContext.Remove(writer);
            await dataContext.SaveChangesAsync();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return false;
        }

        var result = await _booksService.DeleteBooksByWriter(id);

        return result;
    }

    public async Task<Writer?> GetWriters(int id)
    {
        var writer = await dataContext.FindAsync<Writer>(id);

        return writer;
    }

    public Writer[] GetWriters()
    {
        Writer[] writers = dataContext.Writers.ToArray();
        return writers;
    }

    public Writer[] GetWriters(string name)
    {
        Writer[] writers = dataContext.Writers.ToList()
            .Where<Writer>(x => x.FullName.ToUpper().Contains(name.ToUpper()))
            .ToArray<Writer>();

        return writers;
    }

    public async Task<bool> UpdateWriter(Writer writer)
    {
        try
        {
            dataContext.Writers.Update(writer);
            await dataContext.SaveChangesAsync();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return false;
        }

        return true;
    }
}
