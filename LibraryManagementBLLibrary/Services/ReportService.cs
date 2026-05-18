using LibraryManagementBLLibrary.Interfaces;
using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementDLLibrary.Repositories;
using LibraryManagementModelLibrary.Models;
using LibraryManagementModelLibrary.ReportModels;

namespace LibraryManagementBLLibrary.Services;

public class ReportService:IReportService
{
    private readonly IBorrowingRepository
        _borrowingRepository;

    private readonly IMemberRepository
        _memberRepository;

    private readonly IBookRepository
        _bookRepository;

    private readonly IBookCopyRepository
        _bookCopyRepository;
    private readonly LibraryContext _context;
    public ReportService()
    {
        _context = new LibraryContext();

        _borrowingRepository =
            new BorrowingRepository(_context);

        _memberRepository =
            new MemberRepository(_context);

        _bookRepository =
            new BookRepository(_context);

        _bookCopyRepository =
            new BookCopyRepository(_context);
    }
    public List<Borrowing>?GetCurrentlyBorrowedBooks()
    {
        return _borrowingRepository
                .GetActiveBorrowings();
    }
    public List<OverdueReportModel>
    GetOverdueBooks()
    {
        return _borrowingRepository
                .GetOverdueBooksReport();
    }
    public List<MostBorrowedBookModel>
    GetMostBorrowedBooks()
    {
        return _borrowingRepository
                .GetMostBorrowedBooksReport();
    }
    public List<Member>
    GetMembersWithPendingFines()
    {
        var members =
            _memberRepository.GetAll();

        List<Member> pendingFineMembers =
            new List<Member>();

        foreach(var member in members)
        {
            decimal totalFine = 0;

            var borrowings =
                _borrowingRepository
                    .GetBorrowingsByMemberId(
                        member.Id
                    );

            foreach(var borrowing in borrowings)
            {
                totalFine +=
                    _borrowingRepository
                        .CalculateBorrowingFine(
                            borrowing.Id
                        );
            }

            if(totalFine > 0)
            {
                pendingFineMembers.Add(member);
            }
        }

        return pendingFineMembers;
    }
    public List<Book>
    GetAvailableBooksByCategory(
        string category
    )
    {
        var books =
            _bookRepository
                .GetByCategory(category);

        List<Book> availableBooks =
            new List<Book>();

        foreach(var book in books)
        {
            int availableCount =
                _bookCopyRepository
                    .GetAvailableCount(
                        book.BookId
                    );

            if(availableCount > 0)
            {
                availableBooks.Add(book);
            }
        }

        return availableBooks;
    }
    public List<Borrowing>
    GetMemberBorrowingHistory(
        int memberId
    )
    {
        var member =
            _memberRepository.Get(memberId);

        if(member == null)
        {
            throw new Exception(
                "Member not found."
            );
        }

        return _borrowingRepository
                .GetBorrowingsByMemberId(
                    memberId
                );
    }
}
