namespace Bookish.Models;

public class Member
{
    public required int MemberId {get; set;}
    public required string Name {get; set;}
    public required string MembershipNo {get; set;}

    public override bool Equals(object? obj)
    {
        return obj is Member member && member.MemberId == MemberId;
    }
    
    public override int GetHashCode()
    {
        return MemberId.GetHashCode();
    }
}