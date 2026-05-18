namespace LibraryManagementModelLibrary.Models
{
    public enum MemberShipType
    {
        Basic=1,
        Premium,
        Student
    }
    public class Member{

        public int Id {get; set;}
        public string Name {get; set;}=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public MemberShipType MemberType { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Borrowing>? Borrowings {get; set;}
        public Member()
        {

        }
        public Member(string name, string email, string phoneNumber, MemberShipType memberType)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            MemberType = memberType;
        }
       public override string ToString()
        {
            return $"Member Id : {Id}\n" +
                $"Name : {Name}\n" +
                $"Email : {Email}\n" +
                $"Phone : {PhoneNumber}\n" +
                $"Type : {MemberType}\n" +
                $"Active : {IsActive}";
        }
    }
}