using LibraryManagementBLLibrary.Exceptions;
using LibraryManagementBLLibrary.Interfaces;
using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementDLLibrary.Repositories;
using LibraryManagementModelLibrary.Models;
namespace LibraryManagementBLLibrary.Services;
using Microsoft.EntityFrameworkCore;

public class BorrowingService:IBorrowingService
{
    private readonly LibraryContext _context;
    private readonly IMemberRepository _memberRepository;

    private readonly IBookCopyRepository _bookCopyRepository;

    private readonly IBorrowingRepository _borrowingRepository;

    private readonly IFinePaymentRepository _finePaymentRepository;
    public BorrowingService()
    {
          _context = new LibraryContext();
        _memberRepository = new MemberRepository(_context);
        _bookCopyRepository = new BookCopyRepository(_context);
        _borrowingRepository = new BorrowingRepository(_context);
        _finePaymentRepository=new FinePaymentRepository(_context);
    }
    public Borrowing BorrowBook(  int memberId,  int bookId)
    {
        var member =
        _memberRepository.Get(memberId);

    if(member == null)
    {
        throw new InvalidMemberException(
            "Member not found."
        );
    }

    // Validate member active
    if(member.IsActive == false)
    {
        throw new InactiveMemberException(
            "Member is inactive."
        );
    }

    // Validate pending fine
    decimal pendingFine =
        GetPendingFineByMemberId(memberId);

    if(pendingFine > 500)
    {
        throw new FineLimitExceededException(
            "Pending fine exceeds allowed limit."
        );
    }

    // Validate borrowing count
    int activeBorrowingCount =
        _borrowingRepository
            .GetActiveBorrowingCountByMemberId(
                memberId
            );

    int maxBorrowLimit =
        member.MemberType switch
        {
            MemberShipType.Basic => 2,

            MemberShipType.Student => 3,

            MemberShipType.Premium => 5,

            _ => 0
        };

    if(activeBorrowingCount >= maxBorrowLimit)
    {
        throw new BorrowLimitExceededException(
            "Borrowing limit exceeded."
        );
    }

    // Get available copies
    var availableCopies =
        _bookCopyRepository
            .GetAvailableCopies(bookId);

    if(availableCopies == null ||
       availableCopies.Count == 0)
    {
        throw new BookUnavailableException(
            "No available copies found."
        );
    }

    // Select copy automatically
    var selectedCopy =
        availableCopies.First();

    // Validate duplicate same-book borrowing
    var activeBorrowings =
        _borrowingRepository
            .GetActiveBorrowingsByMemberId(
                memberId
            );

    bool alreadyBorrowed =
        activeBorrowings.Any(
            b => b.BookCopy.BookId == bookId
        );

    if(alreadyBorrowed)
    {
        throw new DuplicateBorrowingException(
            "Member already borrowed this book."
        );
        }
    int borrowDays =
        member.MemberType switch
        {
            MemberShipType.Basic => 7,

            MemberShipType.Student => 10,

            MemberShipType.Premium => 15,

            _ => 7
        };

        // 8. Create borrowing
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            Borrowing borrowing =
        new Borrowing
        {
            MemberId = memberId,

            BookCopyId = selectedCopy.Id,

            BorrowedOn = DateTime.Now,

            DueDate =
                DateTime.Now.AddDays(
                    borrowDays
                ),

            FineSettled = false
        };
        
        // Add borrowing to context
        _context.Borrowings.Add(borrowing);
        
        // Update copy status
        selectedCopy.CopyStatus =
        BookCopyStatus.Issued;
        
        // Save both changes together
        _context.SaveChanges();
        transaction.Commit();

    return borrowing;
        }
        catch
        {
                transaction.Rollback();
                throw;
        }
    }

    public Borrowing ReturnBook(
    int borrowingId,
    bool payFineNow
)
{
    // Validate borrowing exists
    var borrowing =
        _borrowingRepository.Get(
            borrowingId
        );

    if(borrowing == null)
    {
        throw new Exception(
            "Borrowing record not found."
        );
    }

    // Validate already returned
    if(borrowing.ReturnedOn != null)
    {
        throw new Exception(
            "Book already returned."
        );
    }

    using var transaction =
        _context.Database.BeginTransaction();

    try
    {
        // Update return date
        borrowing.ReturnedOn =
            DateTime.Now;

        // Update copy status
        borrowing.BookCopy.CopyStatus =
            BookCopyStatus.Available;

_context.SaveChanges();

        // Calculate fine
        decimal fineAmount =
            _borrowingRepository
                .CalculateBorrowingFine(
                    borrowing.Id
                );

        // No fine
        if(fineAmount == 0)
        {
            borrowing.FineSettled = true;
        }

        // Fine exists and user pays now
        else if(payFineNow)
        {
            FinePayment finePayment =
                new FinePayment
                {
                    BorrowingId =
                        borrowing.Id,

                    AmountPaid =
                        fineAmount,

                    PaidOn =
                        DateTime.Now
                };

            _finePaymentRepository
                .Create(finePayment);

            borrowing.FineSettled = true;
        }

        // Fine exists but pay later
        else
        {
            borrowing.FineSettled = false;
        }

        // Update borrowing
        _borrowingRepository.Update(
            borrowing.Id,
            borrowing
        );

        transaction.Commit();

        return borrowing;
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}

    public List<Borrowing>? GetActiveBorrowings()
    {
        var borrowings = _borrowingRepository.GetActiveBorrowings();
        if (borrowings == null || borrowings.Count == 0)
        {

            throw new  Exception("No borrowings found");
        }
        return borrowings;
    }

    public List<Borrowing>? GetBorrowingHistoryByMemberId(
    int memberId)
    {
        var member =
            _memberRepository.Get(memberId);

        if(member == null)
        {
            throw new InvalidMemberException(
                "Member not found."
            );
        }

        return _borrowingRepository
                .GetBorrowingsByMemberId(
                    memberId
                );
    }

    public decimal GetPendingFineByMemberId(
    int memberId)
    {
        var member =
            _memberRepository.Get(memberId);

        if(member == null)
        {
            throw new InvalidMemberException(
                "Member not found."
            );
        }

        var borrowings =
            _borrowingRepository
                .GetBorrowingsByMemberId(
                    memberId
                );

        decimal totalFine = 0;

        foreach(var borrowing in borrowings)
        {
            totalFine +=
                _borrowingRepository
                    .CalculateBorrowingFine(
                        borrowing.Id
                    );
        }

        return totalFine;
    }
    public List<Borrowing>? GetActiveBorrowingsByMemberId(int memberId)
    {
        var member = _memberRepository.Get(memberId);

        if(member == null)
        {
            throw new InvalidMemberException("Member not found.");
        }

        return _borrowingRepository.GetActiveBorrowingsByMemberId(memberId);
    }
    public void PayFine(int borrowingId)
{
    var borrowing = _borrowingRepository.Get(borrowingId);

    if(borrowing == null)
    {
        throw new Exception("Borrowing not found.");
    }

    if(borrowing.FineSettled)
    {
        throw new Exception("Fine already settled.");
    }

    decimal fineAmount = _borrowingRepository.CalculateBorrowingFine(borrowingId);

    if(fineAmount <= 0)
    {
        throw new Exception("No pending fine.");
    }

    using var transaction = _context.Database.BeginTransaction();

    try
    {
        FinePayment finePayment = new FinePayment
        {
            BorrowingId = borrowingId,
            AmountPaid = fineAmount,
            PaidOn = DateTime.Now
        };

        _finePaymentRepository.Create(finePayment);

        borrowing.FineSettled = true;

        _borrowingRepository.Update(borrowing.Id, borrowing);

        transaction.Commit();
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}
}
