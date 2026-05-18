namespace LibraryManagementModelLibrary.Models;

public enum UserRole
{
    Admin = 1,
    Member
}
public class UserAccount
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PassWord { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int? MemberId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public Member? Member { get; set; }
    public UserAccount()
    {

    }
    public UserAccount(string userName, string password, int? memberId, UserRole userRole = UserRole.Member)
    {
        Username = userName;
        PassWord = password;
        Role = userRole;
        MemberId = memberId;
    }
    public override string ToString()
    {
        return $"User Id : {Id}\n" +
            $"Username : {Username}\n" +
            $"Role : {Role}\n" +
            $"Member Id : {MemberId}\n" +
            $"Created On : {CreatedOn}";
    }
}
