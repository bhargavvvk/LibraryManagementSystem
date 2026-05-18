using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagementDLLibrary.Repositories;

public class MemberRepository:AbstractRepository<int, Member>, IMemberRepository
{
    public MemberRepository(LibraryContext context) : base(context)
    {
    }
    public Member? GetByEmail(string email)
    {
        var member  = context.Members.SingleOrDefault(m => m.Email == email);
        return member;
    }
    public Member? GetByPhoneNumber(string phoneNumber)
    {
        var member = context.Members.SingleOrDefault(m => m.PhoneNumber == phoneNumber);
        return member;
    }
    public override Member? Get(int Id)
    {
        var member = context.Members.SingleOrDefault(m => m.Id == Id);
        return member;
    }
}