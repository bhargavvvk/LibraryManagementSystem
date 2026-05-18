namespace LibraryManagementModelLibrary.Models;

public class FinePayment
{
    public int Id { get; set; }
    public int BorrowingId { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime PaidOn { get; set; }
    public Borrowing? Borrowing {get; set;}
    public FinePayment()
    {

    }
    public FinePayment(int borrowingId, decimal amountPaid, DateTime paidOn)
    {
        BorrowingId = borrowingId;
        AmountPaid = amountPaid;
        PaidOn=paidOn;
    }
    public override string ToString()
{
    return $"Id: {Id}\n" +
           $"BorrowingId: {BorrowingId}\n" +
           $"AmountPaid: {AmountPaid}\n" +
           $"PaidOn: {PaidOn}";
}
}
