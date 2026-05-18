using LibraryManagementBLLibrary.Exceptions;
using LibraryManagementBLLibrary.Interfaces;
using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementDLLibrary.Repositories;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Services;

public class BookService : IBookService
{
    private readonly LibraryContext _context;
    private readonly IBookRepository _bookrepository;
    private readonly IBookCopyRepository _bookcopyrepository;
    public BookService()
    {
        _context = new LibraryContext();

        _bookrepository =
            new BookRepository(_context);

        _bookcopyrepository =
            new BookCopyRepository(_context);
    }
    public List<Book>? GetAllBooks()
    {
        var books = _bookrepository.GetAll();
        return books;
    }
    public Book? GetBookByTitle(string title)
    {
        var book = _bookrepository.GetByTitle(title);
        if (book == null)
        {
            throw new BookUnavailableException($"No Book found with the title {title}");
        }
        return book;
    }
    public List<Book>? GetBooksByAuthor(string author)
    {
        var books = _bookrepository.GetByAuthor(author);
        if (books == null || books.Count == 0)
        {
            throw new BookUnavailableException($"No Book found with the author {author}");
        }
        return books;
    }
    public List<Book>? GetBooksByCategory(string category)
    {
        var books = _bookrepository.GetByCategory(category);
       if (books == null || books.Count == 0)
        {
            throw new BookUnavailableException($"No Book found with the category {category}");
        }
        return books;
    }
    public Book AddBook(Book book)
    {
        string randomPart =new Random().Next(1000,9999).ToString();

        string datePart =DateTime.Now.ToString("yyyyMMdd");

        book.ISBN =$"ISBN-{datePart}-{randomPart}";

        return _bookrepository.Create(book);
    }
    public List<BookCopy>? GetAvailableCopies(int bookId)
    {
        var copies =
            _bookcopyrepository.GetAvailableCopies(bookId);

        if(copies == null || copies.Count == 0)
        {
            throw new BookUnavailableException(
                "No available copies found."
            );
        }

        return copies;
    }
    public BookCopy AddBookCopy(BookCopy copy)
    {
        var book =
            _bookrepository.Get(copy.BookId);

        if(book == null)
        {
            throw new BookUnavailableException(
                "Book not found."
            );
        }

        copy.CopyStatus =
            BookCopyStatus.Available;

        return _bookcopyrepository.Create(copy);
    }
    public BookCopy MarkBookCopyDamaged(int copyId)
    {
        var copy = _bookcopyrepository.Get(copyId);

        if(copy == null)
        {
            throw new BookUnavailableException(
                "Book copy not found."
            );
        }

        copy.CopyStatus =
            BookCopyStatus.Damaged;

        return _bookcopyrepository.Update(copy.Id,copy);
    }
    public List<BookCopy>? AddMultipleCopies(int bookId,int count)
    {
        List<BookCopy> copies =
            new List<BookCopy>();

        for(int i = 0; i < count; i++)
        {
            BookCopy copy = new BookCopy
            {
                BookId = bookId
            };

            copies.Add(AddBookCopy(copy));
        }

        return copies;
    }
    public int GetAvailableBookCount(int bookId)
    {
        return _bookcopyrepository.GetAvailableCount(bookId);
    }
}
