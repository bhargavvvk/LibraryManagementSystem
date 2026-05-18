using LibraryManagementModelLibrary.Models;
using LibraryManagementModelLibrary.ReportModels;

namespace LibraryManagementBLLibrary.Interfaces;

public interface IReportService
{
    List<Borrowing>? GetCurrentlyBorrowedBooks();

    List<OverdueReportModel>
        GetOverdueBooks();

    List<Member>
        GetMembersWithPendingFines();

    List<MostBorrowedBookModel>
        GetMostBorrowedBooks();

    List<Book> GetAvailableBooksByCategory(
        string category
    );

    List<Borrowing> GetMemberBorrowingHistory(
        int memberId
    );
}