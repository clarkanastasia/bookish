namespace Bookish.Models;

public class Book
{
    public int Id {get; set;}
    public string Name {get; set;}= "";
    public string Title {get; set;} = "";
    public int TotalCopies {get; set;}
    public int AvailableCopies {get; set;}

    public void Checkout()
    {
        if (AvailableCopies > 0)
        {
            AvailableCopies -= 1;
        }
    }
    public void CheckIn()
    {
        AvailableCopies += 1;
    }
}