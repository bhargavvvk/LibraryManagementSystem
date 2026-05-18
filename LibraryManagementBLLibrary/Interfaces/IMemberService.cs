using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Interfaces;

public interface IMemberService
{
    Member AddMember(Member member);

    List<Member>? GetAllMembers();

    Member? GetMemberByEmail(string email);

    Member? GetMemberByPhoneNumber(string phoneNumber);

    Member UpdateMember(Member member);

    Member DeactivateMember(int memberId);
}
