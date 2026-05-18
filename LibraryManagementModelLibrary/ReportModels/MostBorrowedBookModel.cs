namespace LibraryManagementModelLibrary.ReportModels;

public class MostBorrowedBookModel
{
    public int BookId { get; set; }

    public string Title { get; set; }
        = string.Empty;

    public int BorrowCount { get; set; }
}