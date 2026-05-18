using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Interfaces;

public interface IBookCategoryService
{
    List<BookCategory> GetAllCategories();
}
