using LibraryManagementModelLibrary.Models;
using LibraryManagementModelLibrary.ReportModels;

namespace LibraryManagementDLLibrary.Interfaces;

public interface IBorrowingRepository:IRepository<int,Borrowing>
{
   public List<Borrowing>? GetActiveBorrowings();

   public List<Borrowing>? GetReturnedBorrowings();

   public List<Borrowing>? GetActiveBorrowingsByMemberId(int memberId);

   public int GetActiveBorrowingCountByMemberId(int memberId);

    public Borrowing? GetActiveBorrowingByCopyId(int copyId);
    List<Borrowing>? GetBorrowingsByMemberId( int memberId);

    public decimal CalculateBorrowingFine(int borrowingId);
    public List<OverdueReportModel> GetOverdueBooksReport();
    public List<MostBorrowedBookModel> GetMostBorrowedBooksReport();
}
