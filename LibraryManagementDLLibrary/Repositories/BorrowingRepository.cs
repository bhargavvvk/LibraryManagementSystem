using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementModelLibrary.Models;
using LibraryManagementModelLibrary.ReportModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDLLibrary.Repositories;

public class BorrowingRepository 
    : AbstractRepository<int, Borrowing>, IBorrowingRepository
{
    public BorrowingRepository(LibraryContext context) : base(context)
    {

    }
    public override Borrowing? Get(int borrowingId)
    {
        return context.Borrowings
                      .Include(b => b.Member)
                      .Include(b => b.BookCopy)
                      .ThenInclude(bc => bc.Book)
                      .FirstOrDefault(b => b.Id == borrowingId);
    }

    public List<Borrowing>? GetActiveBorrowings()
    {
        return context.Borrowings
                      .Include(b => b.Member)
                      .Include(b => b.BookCopy)
                      .ThenInclude(bc => bc.Book)
                      .Where(b => b.ReturnedOn == null)
                      .ToList();
    }

    public List<Borrowing>? GetReturnedBorrowings()
    {
        return context.Borrowings
                      .Include(b => b.Member)
                      .Include(b => b.BookCopy)
                      .ThenInclude(bc => bc.Book)
                      .Where(b => b.ReturnedOn != null)
                      .ToList();
    }

    public List<Borrowing>? GetActiveBorrowingsByMemberId(int memberId)
    {
        return context.Borrowings
                      .Include(b => b.Member)
                      .Include(b => b.BookCopy)
                      .ThenInclude(bc => bc.Book)
                      .Where(b => b.MemberId == memberId &&
                                  b.ReturnedOn == null)
                      .ToList();
    }

    public int GetActiveBorrowingCountByMemberId(int memberId)
    {
        return context.Borrowings
                      .Count(b => b.MemberId == memberId &&
                                  b.ReturnedOn == null);
    }

    public Borrowing? GetActiveBorrowingByCopyId(int copyId)
    {
        return context.Borrowings
                      .Include(b => b.Member)
                      .Include(b => b.BookCopy)
                      .ThenInclude(bc => bc.Book)
                      .FirstOrDefault(b => b.BookCopyId == copyId &&
                                           b.ReturnedOn == null);
    }
    public List<Borrowing>? GetBorrowingsByMemberId(
    int memberId
)
    {
        return context.Borrowings
                    .Include(b => b.Member)
                    .Include(b => b.BookCopy)
                    .ThenInclude(bc => bc.Book)
                    .Where(b => b.MemberId == memberId)
                    .ToList();
    }
    public decimal CalculateBorrowingFine(
    int borrowingId)
    {
        var result = context.Database
            .SqlQueryRaw<decimal>(
                $"SELECT calculate_borrowing_fine({borrowingId})"
            )
            .AsEnumerable()
            .FirstOrDefault();

        return result;
    }
    public List<OverdueReportModel>
    GetOverdueBooksReport()
    {
        return context.Database
            .SqlQueryRaw<OverdueReportModel>(
                "SELECT * FROM get_overdue_books_report()"
            )
            .ToList();
    }
    public List<MostBorrowedBookModel>
    GetMostBorrowedBooksReport()
    {
        return context.Database
            .SqlQueryRaw<MostBorrowedBookModel>(
                "SELECT * FROM get_most_borrowed_books()"
            )
            .ToList();
    }
}