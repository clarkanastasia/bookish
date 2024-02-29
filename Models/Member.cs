namespace Bookish.Models;

public class Member 
{
    public int Id {get; set;}
    public string Name {get;set;} ="";
    public int Membership {get;set;}
    public readonly IEnumerable<Book> CurrentLoans = [];

    public void BorrowBook()
    {
    }
    public void ReturnBook()
    {
    }
}