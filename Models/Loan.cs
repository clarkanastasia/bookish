using System.ComponentModel.DataAnnotations.Schema;
namespace Bookish.Models;

public class Loan
{
    public int LoanId {get;set;}
    public required int BookId {get;set;}
    [ForeignKey("BookId")]
    public Book Book {get;set;} = null!;
    public required int MemberId {get;set;}
    [ForeignKey("MemberId")]
    public Member Member {get;set;} = null!;
    private readonly static int _loanPeriod = 30;

    public DateOnly IssueDate {get;set; } = DateOnly.FromDateTime(DateTime.Today);
    
    public DateOnly DueDate {get; set;} = DateOnly.FromDateTime(DateTime.Today).AddDays(_loanPeriod);

    public DateOnly? DateReturned {get; set;}
}