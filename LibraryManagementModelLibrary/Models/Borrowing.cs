namespace LibraryManagementModelLibrary.Models;

public class Borrowing
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public int BookCopyId { get; set; }
    public DateTime BorrowedOn { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedOn { get; set; }
    public bool WasDamagedOnIssue { get; set; }
    public bool FineSettled { get; set; }
    public Member? Member { get; set; }
    public BookCopy? BookCopy { get; set; }
    public FinePayment? FinePayment {get; set;}
    public Borrowing(){

    }

    public Borrowing(int memberId, int bookCopyId, DateTime borrowedOn, DateTime dueDate)
    {
        MemberId = memberId;
        BookCopyId = bookCopyId;
        BorrowedOn = borrowedOn;
        DueDate = dueDate;
        WasDamagedOnIssue = false;
        FineSettled = true;
    }
   public override string ToString()
{
    return $"Borrowing Id : {Id}\n" +
           $"Member Id : {MemberId}\n" +
           $"Book Copy Id : {BookCopyId}\n" +
           $"Borrowed On : {BorrowedOn}\n" +
           $"Due Date : {DueDate}\n" +
           $"Returned On : {ReturnedOn}\n" +
           $"Was Damaged On Issue : {WasDamagedOnIssue}\n"+
           $"Fine Settled : {FineSettled}";
}
}
