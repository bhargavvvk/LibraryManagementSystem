namespace LibraryManagementBLLibrary.Exceptions;

public class BorrowLimitExceededException : Exception
{
    public BorrowLimitExceededException()
        : base("Borrowing limit exceeded.")
    {
    }

    public BorrowLimitExceededException(string message)
        : base(message)
    {
    }
}
