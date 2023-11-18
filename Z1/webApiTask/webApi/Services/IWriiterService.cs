using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;

namespace webApi.Services;

public interface IWritersService
{
    Task<bool> AddWriter(WriterCl writerCl);
    Task<bool> DeleteWriter(int id);
    Task<Writer?> GetWriters(int id);
    Writer[] GetWriters();
    Writer[] GetWriters(string name);
    Task<bool> UpdateWriter(Writer writer);
}
