namespace Bookish.Models.Data;

public class Book
{
    public int BookId {get; set;}
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
}