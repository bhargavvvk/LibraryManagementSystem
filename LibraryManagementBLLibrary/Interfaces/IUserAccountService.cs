using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Interface;

public interface IUserAccountService
{
    UserAccount Login(string username, string password);
    UserAccount CreateMemberAccount(Member member);
}
