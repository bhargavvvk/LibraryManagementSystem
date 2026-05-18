using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementModelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDLLibrary.Repositories;

public class BookCopyRepository:AbstractRepository<int,BookCopy>,IBookCopyRepository
{
    public BookCopyRepository(LibraryContext context) : base(context)
    {

    }
    public List<BookCopy>? GetAvailableCopies(int bookid)
    {

        return context.BookCopies.Include(b=>b.Book).Where(b => b.BookId == bookid && b.CopyStatus == BookCopyStatus.Available).ToList();
    }
    public int GetAvailableCount(int bookid)
    {

        return context.BookCopies.Count(b => b.BookId == bookid && b.CopyStatus == BookCopyStatus.Available);
    }
    public  List<BookCopy>? GetBookCopiesByBookId(int bookid)
    {
        return context.BookCopies.Include(b=>b.Book).Where(b => b.BookId == bookid).ToList();
    }
    public override BookCopy? Get(int copyid)
    {
        var book = context.BookCopies.Include(b => b.Book).FirstOrDefault(b => b.Id == copyid);
        return book;
    }
}
