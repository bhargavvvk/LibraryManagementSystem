namespace LibraryManagementBLLibrary.Exceptions;

public class AuthenticationFailedException : Exception
{
    public AuthenticationFailedException()
        : base("Invalid username or password.")
    {
    }

    public AuthenticationFailedException(string message)
        : base(message)
    {
    }
}