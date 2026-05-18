namespace LibraryManagementBLLibrary.Exceptions;

public class InvalidMemberException: Exception
{
    public InvalidMemberException()
        : base("Invalid member details")
    {
    }
    public InvalidMemberException(string message)
        :base(message)
    {
    }
}
