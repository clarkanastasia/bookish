namespace Bookish.Models;


public class Book
{
    public int BookId {get; set;}
    public required string Title {get; set;}
    public required string Author {get; set;}
    public required int TotalCopies {get; set;}
    public required int AvailableCopies {get; set;}
}