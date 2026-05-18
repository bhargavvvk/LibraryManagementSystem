namespace LibraryManagementBLLibrary.Exceptions;

public class BookUnavailableException : Exception
{
    public BookUnavailableException()
        : base("Book copy is not available.")
    {
    }

    public BookUnavailableException(string message)
        : base(message)
    {
    }
}