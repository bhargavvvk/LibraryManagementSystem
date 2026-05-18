using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementModelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDLLibrary.Repositories;

public class FinePaymentRepository 
    : AbstractRepository<int, FinePayment>, IFinePaymentRepository
{
    public FinePaymentRepository(LibraryContext context) : base(context)
    {

    }
    public override FinePayment? Get(int finePaymentId)
    {
        return context.FinePayments
                      .Include(f => f.Borrowing)
                      .ThenInclude(b => b.Member)
                      .FirstOrDefault(f => f.Id == finePaymentId);
    }

    public FinePayment? GetByBorrowingId(int borrowingId)
    {
        return context.FinePayments
                      .Include(f => f.Borrowing)
                      .ThenInclude(b => b.Member)
                      .FirstOrDefault(f => f.BorrowingId == borrowingId);
    }
    public List<FinePayment>? GetByMemberId(int memberId)
    {
        return context.FinePayments.Include(f => f.Borrowing).ThenInclude(b=>b.Member).Where(f=>f.Borrowing.MemberId==memberId).ToList();
    }
}