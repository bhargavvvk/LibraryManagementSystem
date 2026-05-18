using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementModelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDLLibrary.Repositories;

public class UserAccountRepository 
    : AbstractRepository<int, UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(LibraryContext context) : base(context)
    {

    }
    public override UserAccount? Get(int key)
    {
        return context.UserAccounts
                    .Include(u => u.Member)
                      .FirstOrDefault(u => u.Id == key);
    }

    public UserAccount? GetByUsername(string username)
    {
        return context.UserAccounts
                        .Include(u=>u.Member)
                      .FirstOrDefault(u => u.Username == username);
    }
}