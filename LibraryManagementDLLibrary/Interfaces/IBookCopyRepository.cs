using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Interfaces;

public interface IBookCopyRepository:IRepository<int, BookCopy>
{
    public List<BookCopy>? GetAvailableCopies(int bookid);
    public int GetAvailableCount(int bookid);
    public List<BookCopy>? GetBookCopiesByBookId(int bookid);
}
