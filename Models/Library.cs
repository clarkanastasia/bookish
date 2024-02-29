namespace Bookish.Models;

public class Library 
{
    public string Name {get; set;} ="";
    public readonly HashSet<Book> Catalogue = [];
    public readonly HashSet<Member> Members = [];

    public void AddBook()
    {
    }
    public void AddMember()
    {
    }

    public void GetAvailableBooks()
    {  
    }
    public void GetBooksByMember()
    {      
    }

}