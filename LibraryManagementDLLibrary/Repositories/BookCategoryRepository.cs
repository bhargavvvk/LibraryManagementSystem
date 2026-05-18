using LibraryManagementDLLibrary.Context;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Repositories;

public class BookCategoryRepository:AbstractRepository<int,BookCategory>
{
    public BookCategoryRepository(LibraryContext context) : base(context)
    {

    }
    public override BookCategory? Get(int categoryid)
    {
        return context.BookCategories.SingleOrDefault(c=>c.Id==categoryid);
    }
}

