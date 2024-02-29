namespace Bookish.Models;


public class Book
{
    public required int BookId {get; set;}
    public required string Title {get; set;}
    public required string Author {get; set;}
    public required int TotalCopies {get; set;}
    public required int AvailableCopies {get; set;}

    public bool Checkout()
    {
        bool success = false;

        if(AvailableCopies>0)
        {    
            AvailableCopies -= 1;
            success = true;
        }
        return success;
    }

    public void CheckIn()
    {
        AvailableCopies += 1;
    }

    public void IncreaseTotalCopies()
    {
        TotalCopies += 1;
    }

    public void DecreaseTotalCopies()
    {
        TotalCopies -= 1;
    }

    public override bool Equals(object? obj)
    {
        return obj is Book book && book.BookId == BookId;
    }

    public override int GetHashCode()
    {
        return BookId.GetHashCode();
    }
}