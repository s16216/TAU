using webApi.DataClasses.Entities;
using webApi.DataClasses.EntitiesCl;

namespace webApi.Services;

public interface IBooksService
{
    Task<bool> AddBook(BookCl bookCl);
    Book[] GetBooks();
    Task<bool> DeleteBook(int id);
    Task<bool> DeleteBooksByWriter(int id);
    Task<bool> UpdateBook(Book book);
}