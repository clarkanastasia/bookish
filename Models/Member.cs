namespace Bookish.Models;

public class Member
{
    public required int MemberId {get; set;}
    public required string Name {get; set;}
    public required string MembershipNo {get; set;}

    public readonly List<Book> BooksOnLoan = [];
    public void BorrowBook(Book book)
    {
        if(book.Checkout())
            BooksOnLoan.Add(book);
    }
    public void ReturnBook(Book book)
    {
        if(BooksOnLoan.Any(b => b.BookId == book.BookId))
        {
            book.CheckIn();
            BooksOnLoan.Remove(book);
        }
    }
}