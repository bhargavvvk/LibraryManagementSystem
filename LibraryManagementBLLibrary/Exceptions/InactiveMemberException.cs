namespace LibraryManagementBLLibrary.Exceptions;

public class InactiveMemberException : Exception
{
    public InactiveMemberException()
        : base("Member account is inactive.")
    {
    }

    public InactiveMemberException(string message)
        : base(message)
    {
    }
}