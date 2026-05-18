namespace LibraryManagementModelLibrary.Models;

public enum BookCopyStatus
{
    Available = 1,
    Issued,
    Damaged,
    Lost
}
public class BookCopy
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public BookCopyStatus CopyStatus { get; set; }
    public Book? Book { get; set; }
   public ICollection<Borrowing>? Borrowings { get; set; }
    public BookCopy()
    {

    }
    public BookCopy(int bookId, BookCopyStatus copyStatus = BookCopyStatus.Available)
    {
        BookId = bookId;
        CopyStatus = copyStatus;
    }
    public override string ToString()
    {
        return $"Id : {Id}\n" +
            $"Book Id : {BookId}\n" +
            $"Status : {CopyStatus}";
    }
}
