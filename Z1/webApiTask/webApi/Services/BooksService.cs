using webApi.DataClasses;
using webApi.DataClasses.Entities;
using AutoMapper;
using webApi.DataClasses.EntitiesCl;

namespace webApi.Services;

public class BooksService : IBooksService
{
    private DataContext dataContext;
    private Mapper mapper;

    public BooksService(DataContext context)
    {
        dataContext = context;

        var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<BookCl, Book>()
                );
        mapper = new Mapper(config);
    }

    private bool IsTitleExists(string title)
    {
        return dataContext.Books.ToList().Exists(b => b.Title.ToUpper().Equals(title.ToUpper()));
    }

    public async Task<bool> AddBook(BookCl bookCl)
    {
        Book book = mapper.Map<Book>(bookCl);

        if (IsTitleExists(book.Title))
            return false;

        var writer = dataContext.Writers.FirstOrDefault(w => w.WriterId == book.WriterId);
        if (writer is null)
            return false;

        dataContext.Books.Add(book);

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

    public Book[] GetBooks()
    {
        Book[] books = dataContext.Books.ToArray();
        return books;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = dataContext.Books.Find(id);

        if (book == null)
            return false;

        try
        {
            dataContext.Remove(book);
            await dataContext.SaveChangesAsync();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteBooksByWriter(int id)
    {
        IEnumerable<Book> books = dataContext.Books.ToList().Where(x => x.WriterId == id);
        // var query = dataContext.Books.Join(dataContext.Writers, b => b.WriterId, w => w.WriterId, (b, w)
        //         => new { WriterName = w.FullName, BookTitle = b.Title });

        if (books.Count() == 0)
            return false;

        foreach (var book in books)
        {
            dataContext.Remove(book);
        }

        try
        {
            await dataContext.SaveChangesAsync();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateBook(Book book)
    {
        try
        {
            dataContext.Books.Update(book);
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