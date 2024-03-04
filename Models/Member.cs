using System.ComponentModel.DataAnnotations.Schema;
namespace Bookish.Models;

public class Member
{
    public int MemberId {get; set;}
    public required string Name {get; set;}
    public required string MembershipNo {get; set;}
    
    [InverseProperty(nameof(Loan.Member))]
    public List<Loan> Loans { get; set; } = [];
}