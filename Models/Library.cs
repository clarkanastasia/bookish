namespace Bookish.Models;

public class Library
{
    public readonly HashSet<Book> Books = [];
    public readonly HashSet<Member> Members = [];
    public readonly HashSet<BookMember> BooksOnLoan = [];

    public Book GetBookById(int bookId)
    {
        return Books.First(b => b.BookId == bookId); 
    }

    public Member GetMemberById(int memberId)
    {
        return Members.First(m => m.MemberId == memberId); 
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void AddMember(Member member)
    {
        Members.Add(member);
    }

    public void DeleteBook(Book book)
    {
        if(Books.Any(b => b.BookId == book.BookId))
            Books.Remove(book);
    }

    public void DeleteMember(Member member)
    {
        if(Members.Any(m => m.MemberId == member.MemberId))
            Members.Remove(member);
    }    

    public void BorrowBook(int bookId, int memberId)
    {
        Book book = GetBookById(bookId);        
        BookMember bookOnLoan = new(){
            BookId = bookId,
            MemberId = memberId,
            IssueDate = DateTime.Now,
        };
        
        if(book.Checkout())
            BooksOnLoan.Add(bookOnLoan);
    }
    public void ReturnBook(int bookId, int memberId)
    {
        Book book = GetBookById(bookId);        
        BookMember bookOnLoan = BooksOnLoan.First(bl => bl.BookId == bookId && bl.MemberId == memberId);
        if(bookOnLoan != null)
        {
            book.CheckIn();
            BooksOnLoan.Remove(bookOnLoan);
        }
    }
}