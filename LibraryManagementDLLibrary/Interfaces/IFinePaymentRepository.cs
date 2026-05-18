using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Interfaces;

public interface IFinePaymentRepository:IRepository<int,FinePayment>
{
    public FinePayment? GetByBorrowingId(int borrowingId);
     public List<FinePayment>? GetByMemberId(int memberId);
}
