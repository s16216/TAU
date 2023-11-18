namespace webApi.DataClasses.Entities;

public class Book
{
    public int BookId { get; set; }
    public int WriterId { get; set; }
    public string Title { get; set; }
    public int? YearOfPublication { get; set; }
    public string Genre { get; set; }

    public Book()
    {
        this.Title = string.Empty;
        this.Genre = string.Empty;
    }
}
