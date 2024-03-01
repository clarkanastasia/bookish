namespace Bookish.Models;

public class Library
{
    public readonly HashSet<Book> Books = [];
    public readonly HashSet<Member> Members = [];
    public readonly HashSet<Loan> BooksOnLoan = [];

    public Library()
    {
        var book1 = new Book{
            BookId = 1,
            Title = "Harry Potter and The Goblet of Fire",
            Author = "J.K.Rowling",
            TotalCopies = 5,
            AvailableCopies = 5,
        };
        var book2 = new Book{
            BookId = 2,
            Title = "Little Women",
            Author = "Louisa May Alcott",
            TotalCopies = 6,
            AvailableCopies = 6,
        };
        Member member1 = new Member{
            MemberId = 1,
            Name = "Jane Smith",
            MembershipNo = "123456"
        };
        Books.Add(book1);
        Books.Add(book2);
        Members.Add(member1);
    }
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
        Loan bookOnLoan = new(){
            BookId = bookId,
            MemberId = memberId,
        };
        
        if(book.Checkout())
            BooksOnLoan.Add(bookOnLoan);
    }
    public void ReturnBook(int bookId, int memberId)
    {
        Book book = GetBookById(bookId);        
        Loan bookOnLoan = BooksOnLoan.First(bl => bl.BookId == bookId && bl.MemberId == memberId);
        if(bookOnLoan != null)
        {
            book.CheckIn();
            BooksOnLoan.Remove(bookOnLoan);
        }
    }
}