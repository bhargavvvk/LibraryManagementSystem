using LibraryManagementBLLibrary.Exceptions;
using LibraryManagementBLLibrary.Interface;
using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementDLLibrary.Repositories;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Services;


public class UserAccountService:IUserAccountService
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly LibraryContext _context;
    public UserAccountService()
    {
         _context = new LibraryContext();
        _userAccountRepository = new UserAccountRepository(_context);
    }
    public UserAccount Login(string username, string password)
    {
        var account = _userAccountRepository.GetByUsername(username);
        if(account == null)
        {
            throw new AuthenticationFailedException("Invalid username.");
        }
        if(account.PassWord != password)
        {
            throw new AuthenticationFailedException("Invalid password.");
        }
        if(account.Member != null && account.Member.IsActive == false)
        {
            throw new InactiveMemberException();
        }
        return account;
    }
    public UserAccount CreateMemberAccount(Member member)
    {
        UserAccount userAccount = new UserAccount
        {
            Username = member.PhoneNumber,
            PassWord = "Welcome@123",
            Role = UserRole.Member,
            MemberId = member.Id
        };

        return _userAccountRepository.Create(userAccount);
    }
}
