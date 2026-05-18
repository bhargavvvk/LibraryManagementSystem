using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Interfaces;

public interface IBookService
{
    Book AddBook(Book book);

    List<Book>? GetAllBooks();

    Book? GetBookByTitle(string title);

    List<Book>? GetBooksByAuthor(string author);

    List<Book>? GetBooksByCategory(string category);

    List<BookCopy>? GetAvailableCopies(int bookId);

    BookCopy AddBookCopy(BookCopy copy);

    List<BookCopy>? AddMultipleCopies(int bookId, int count);

    BookCopy MarkBookCopyDamaged(int copyId);
}
