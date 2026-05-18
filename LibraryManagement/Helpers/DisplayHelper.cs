using LibraryManagementModelLibrary.Models;
using LibraryManagementModelLibrary.ReportModels;
namespace LibraryManagement.Helpers;

public static class DisplayHelper
{
    public static void PrintBook(Book book, int availableCopies)
    {
        Console.WriteLine($"Book Id   : {book.BookId}");
        Console.WriteLine($"Title     : {book.Title}");
        Console.WriteLine($"Author    : {book.Author}");
        Console.WriteLine($"Category  : {book.BookCategory.CategoryName}");
        Console.WriteLine($"ISBN      : {book.ISBN}");
        Console.WriteLine($"Available Copies : {availableCopies}");
        Console.WriteLine("--------------------------------");
    }
    public static void PrintMember(Member member)
    {
        Console.WriteLine(
            $"Member Id : {member.Id}"
        );

        Console.WriteLine(
            $"Name      : {member.Name}"
        );

        Console.WriteLine(
            $"Email     : {member.Email}"
        );

        Console.WriteLine(
            $"Phone     : {member.PhoneNumber}"
        );

        Console.WriteLine(
            $"Type      : {member.MemberType}"
        );

        Console.WriteLine(
            $"Active    : {member.IsActive}"
        );

        Console.WriteLine("--------------------------------");
    }
    public static void PrintBorrowing(Borrowing borrowing)
    {
        Console.WriteLine(
            $"Borrowing Id : {borrowing.Id}"
        );

        Console.WriteLine(
            $"Book Title   : {borrowing.BookCopy.Book.Title}"
        );

        Console.WriteLine(
            $"Borrowed On  : {borrowing.BorrowedOn}"
        );

        Console.WriteLine(
            $"Due Date     : {borrowing.DueDate}"
        );

        Console.WriteLine(
            $"Returned On  : {borrowing.ReturnedOn}"
        );

        Console.WriteLine(
            $"Fine Settled : {borrowing.FineSettled}"
        );

        Console.WriteLine("--------------------------------");
    }
   public static void PrintFinePayment(FinePayment payment)
    {
        Console.WriteLine($"Payment Id : {payment.Id}");

        Console.WriteLine($"Borrowing Id : {payment.BorrowingId}");

        Console.WriteLine($"Book Title : {payment.Borrowing.BookCopy.Book.Title}");

        Console.WriteLine($"Amount Paid : {payment.AmountPaid}");

        Console.WriteLine($"Paid On : {payment.PaidOn}");

        Console.WriteLine("--------------------------------");
    }
    public static void PrintOverdueReport(OverdueReportModel report)
    {
        Console.WriteLine(
            $"Borrowing Id : {report.BorrowingId}"
        );

        Console.WriteLine(
            $"Member Name  : {report.MemberName}"
        );

        Console.WriteLine(
            $"Book Title   : {report.BookTitle}"
        );

        Console.WriteLine(
            $"Due Date     : {report.DueDate}"
        );

        Console.WriteLine(
            $"Days Overdue : {report.DaysOverdue}"
        );

        Console.WriteLine("--------------------------------");
    }
    public static void PrintMostBorrowedBook(MostBorrowedBookModel report)
    {
        Console.WriteLine(
            $"Book Id      : {report.BookId}"
        );

        Console.WriteLine(
            $"Title        : {report.Title}"
        );

        Console.WriteLine(
            $"Borrow Count : {report.BorrowCount}"
        );

        Console.WriteLine("--------------------------------");
    }

}
