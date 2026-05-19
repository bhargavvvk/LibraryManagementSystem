using System.Text.RegularExpressions;
namespace LibraryManagementBLLibrary.Services;

public class ValidationService
{
    public void ValidateName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Name cannot be empty.");
        }
    }

    public void ValidateEmail(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("Email cannot be empty.");
        }

        string pattern =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if(!Regex.IsMatch(email, pattern))
        {
            throw new Exception("Invalid email format.");
        }
    }

    public void ValidatePhoneNumber(string phoneNumber)
    {
        if(string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new Exception("Phone number cannot be empty.");
        }

        string pattern = @"^\d{10}$";

        if(!Regex.IsMatch(phoneNumber, pattern))
        {
            throw new Exception("Phone number must contain exactly 10 digits.");
        }
    }
}
