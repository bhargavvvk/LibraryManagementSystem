namespace LibraryManagementModelLibrary.ReportModels;

public class OverdueReportModel
{
    public int BorrowingId { get; set; }

    public string MemberName { get; set; }
        = string.Empty;

    public string BookTitle { get; set; }
        = string.Empty;

    public DateTime DueDate { get; set; }

    public int DaysOverdue { get; set; }
}