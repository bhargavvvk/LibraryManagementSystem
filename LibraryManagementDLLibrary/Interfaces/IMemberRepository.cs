using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Interfaces;

public interface IMemberRepository : IRepository<int, Member>
{
    Member? GetByEmail(string email);

    Member? GetByPhoneNumber(string phoneNumber);
}
