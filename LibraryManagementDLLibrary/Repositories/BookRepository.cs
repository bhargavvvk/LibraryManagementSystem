using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementModelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDLLibrary.Repositories
{
    public class BookRepository : AbstractRepository<int, Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {

        }
        
        public override Book Create(Book book)
        {
            context.Add(book);
            context.SaveChanges();
            // Reload the book with its navigation properties
            return Get(book.BookId)!;
        }
        
        public Book? GetByTitle(string title)
        {
            var book = context.Books
                            .Include(b => b.BookCategory)
                            .FirstOrDefault(b => b.Title == title);
            return book;
        }
        public List<Book>? GetByAuthor(string author)
        {
            var books = context.Books.Include(b=>b.BookCategory)
            .Where(b => b.Author == author).ToList();
            return books;
        }
        public List<Book> GetByCategory(string category)
        {
            var books = context.Books.Include(b => b.BookCategory).Where(b => b.BookCategory.CategoryName == category).ToList();
            return books;
        }
        public override Book? Get(int bookid)
        {
            var book=context.Books
                            .Include(b => b.BookCategory)
                            .FirstOrDefault(b => b.BookId == bookid);
            return book;
        }
        public override List<Book>? GetAll()
        {
            return context.Books
                        .Include(b => b.BookCategory)
                        .ToList();
        }
    }
}