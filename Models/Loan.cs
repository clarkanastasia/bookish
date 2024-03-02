namespace Bookish.Models;

public class Loan
{
    public required int BookId {get;set;}
    public required int MemberId {get;set;}
    private static readonly int _loanPeriod = 30;
    public DateOnly IssueDate {get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly DueDate {get; set;} = DateOnly.FromDateTime(DateTime.Today).AddDays(_loanPeriod);

    // public DateOnly GetDueDate()
    // {
    //     DateOnly dueDate;
    //     dueDate = IssueDate.AddDays(_loanPeriod);
    //     return dueDate;
    // }    
}