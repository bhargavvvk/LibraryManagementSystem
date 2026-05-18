namespace LibraryManagementBLLibrary.Exceptions;

public class FineLimitExceededException : Exception
{
    public FineLimitExceededException()
        : base("Pending fine exceeds allowed limit.")
    {
    }

    public FineLimitExceededException(string message)
        : base(message)
    {
    }
}
