using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Interfaces;

public interface IBorrowingService
{
Borrowing BorrowBook(int memberId, int bookId);

Borrowing ReturnBook(
    int borrowingId,
     bool payFineNow
);

List<Borrowing>? GetActiveBorrowings();

List<Borrowing>? GetBorrowingHistoryByMemberId(
    int memberId
);

decimal GetPendingFineByMemberId(
    int memberId
);
List<Borrowing>? GetActiveBorrowingsByMemberId(int memberId);
void PayFine(int borrowingId);
}

