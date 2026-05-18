using LibraryManagementBLLibrary.Exceptions;
using LibraryManagementBLLibrary.Interfaces;
using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;
using LibraryManagementDLLibrary.Repositories;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagementBLLibrary.Services;

public class MemberService: IMemberService
{
    private readonly LibraryContext _context;
    private readonly IMemberRepository _memberrepository;
    private readonly IUserAccountRepository _userAccountRepository;
    public MemberService()
    {
         _context = new LibraryContext();
        _memberrepository = new MemberRepository(_context);
        _userAccountRepository = new UserAccountRepository(_context);
    }
    public List<Member>? GetAllMembers()
    {
        var members = _memberrepository.GetAll();
        return members;
    }
    public Member? GetMemberByEmail(string email)
    {
        var member=_memberrepository.GetByEmail(email);
        if (member == null)
        {
            throw new InvalidMemberException("Member not found with the emailid");
        }
        return member;
    }
    public Member? GetMemberByPhoneNumber(string phoneNumber)
    {

        var member = _memberrepository.GetByPhoneNumber(phoneNumber);
        if (member == null)
        {
            throw new InvalidMemberException("Member not found with the phonenumber");
        }
        return member;
    }
    public Member DeactivateMember(int memberId)
    {
        var member = _memberrepository.Get(memberId);
        if (member == null)
        {
            throw new InvalidMemberException("Member not found with the id");
        }
        member.IsActive = false;
        return _memberrepository.Update(memberId, member);
    }
    public Member AddMember(Member member)
    {
        var existingEmail =
            _memberrepository.GetByEmail(member.Email);

        if(existingEmail != null)
        {
            throw new Exception(
                "Email already exists."
            );
        }

        var existingPhone =
            _memberrepository.GetByPhoneNumber(
                member.PhoneNumber
            );

        if(existingPhone != null)
        {
            throw new Exception(
                "Phone number already exists."
            );
        }

        return _memberrepository.Create(member);
    }
     public Member UpdateMember(Member member)
    {
        var existingMember =_memberrepository.Get(member.Id);

        if(existingMember == null)
        {
            throw new InvalidMemberException("Member not found.");
        }
        var existingEmail = _memberrepository.GetByEmail(member.Email);
        if(existingEmail != null &&
           existingEmail.Id != member.Id)
        {
            throw new Exception(
                "Email already exists."
            );
        }
        var existingPhone =
            _memberrepository.GetByPhoneNumber(
                member.PhoneNumber
            );

        if(existingPhone != null &&
           existingPhone.Id != member.Id)
        {
            throw new Exception(
                "Phone number already exists."
            );
        }
        existingMember.Name = member.Name;
        existingMember.Email = member.Email;
        existingMember.PhoneNumber =
            member.PhoneNumber;
        existingMember.MemberType =
            member.MemberType;
        existingMember.IsActive =
            member.IsActive;
        return _memberrepository.Update(existingMember.Id,existingMember);
    }

}
