namespace Bookish.Models;

public class BookMember
{
    public required int BookId {get;set;}
    public required int MemberId {get;set;}
    public required DateTime IssueDate {get;set;}

    private readonly double _loadPeriod = 30;

    public DateTime GetDueDate()
    {
        DateTime dueDate;
        dueDate = IssueDate.AddDays(_loadPeriod);
        return dueDate;
    }    
}