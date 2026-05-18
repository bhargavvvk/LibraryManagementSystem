using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Interfaces;

public interface IUserAccountRepository : IRepository<int, UserAccount>
{
    UserAccount? GetByUsername(string username);
}