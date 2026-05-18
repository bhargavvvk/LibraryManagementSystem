using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Interfaces;

public interface IBookRepository:IRepository<int, Book>
{
    public Book? GetByTitle(string title);
    public List<Book>? GetByAuthor(string author);
    public List<Book>? GetByCategory(string category);
}
