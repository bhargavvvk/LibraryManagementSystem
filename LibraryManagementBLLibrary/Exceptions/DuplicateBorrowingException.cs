namespace LibraryManagementBLLibrary.Exceptions;

public class DuplicateBorrowingException : Exception
{
    public DuplicateBorrowingException()
        : base("Member already borrowed this book and has not returned it.")
    {
    }

    public DuplicateBorrowingException(string message)
        : base(message)
    {
    }
}
