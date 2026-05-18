namespace LibraryManagementModelLibrary.Models;

public class Book
{
    public int BookId { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string PublisherName { get; set; } = string.Empty;
    public DateTime? PublishedOn { get; set; }
    public BookCategory? BookCategory { get; set; }
    public ICollection<BookCopy>? BookCopies { get; set; }
    public Book()
    {

    }
    public Book(string isbn, string title, string author, int categoryId, string publisherName,DateTime publishedOn)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        CategoryId = categoryId;
        PublisherName = publisherName;
        PublishedOn=publishedOn;
    }

    public override string ToString()
    {
        return $"Book Id : {BookId}\n" +
            $"ISBN : {ISBN}\n" +
            $"Title : {Title}\n" +
            $"Author : {Author}\n" +
            $"Category Id : {CategoryId}\n" +
            $"Publisher Name : {PublisherName}\n" +
            $"Published At : {PublishedOn}";
    }
}
