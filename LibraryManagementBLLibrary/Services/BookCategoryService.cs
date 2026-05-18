using LibraryManagementBLLibrary.Interfaces;
using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementDLLibrary.Repositories;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Services;

public class BookCategoryService : IBookCategoryService
{
    private readonly LibraryContext _context;
    private readonly BookCategoryRepository _categoryRepository;

    public BookCategoryService()
    {
        _context = new LibraryContext();

        _categoryRepository =
            new BookCategoryRepository(_context);
    }

    public List<BookCategory>? GetAllCategories()
    {
        return _categoryRepository.GetAll();
    }
}