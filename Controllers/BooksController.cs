using Bookish.Models.Data;
using Bookish.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Controllers;
public class BooksController : Controller 
{    
    private readonly Library myLibrary;

    public BooksController(Library library)
    {
        myLibrary = library;
    }

    [HttpGet("[controller]/catalogue")]
    public IActionResult GetAll()
    {
        var books = myLibrary.Books.ToList();
        var viewModel = new BooksViewModel
        {
            Books = books,
        };
        return View(viewModel);
    }

    [HttpGet("[controller]/catalogue/{bookId}")]
    public IActionResult GetById([FromRoute]int bookId)
    {
        var book = myLibrary.Books.SingleOrDefault(book => book.BookId == bookId);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    public IActionResult AddBook()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddBook([FromForm] string title, [FromForm] string author, [FromForm] int totalCopies) 
    {
        var newBook = new Book
        {
            Title = title,
            Author = author,
            TotalCopies = totalCopies,
            AvailableCopies = totalCopies,

        };
        myLibrary.Books.Add(newBook);
        myLibrary.SaveChanges();
        return RedirectToAction(nameof(GetAll));
    }

    [HttpGet("[controller]/{bookId}/EditBook")]
    public IActionResult EditBook([FromRoute] int bookId)
    {
        var existingBook = myLibrary.Books.SingleOrDefault(book => book.BookId == bookId);
        return View(existingBook);
    }

    [HttpPost("[controller]/{bookId}/EditBook")]
    public IActionResult EditBook([FromRoute] int bookId, [FromForm] string title, [FromForm] string author)
    {
        var existingBook = myLibrary.Books.SingleOrDefault(book => book.BookId == bookId);
        if (existingBook == null){
            return NotFound();
        }
        existingBook.Title = title;
        existingBook.Author = author;
        myLibrary.SaveChanges();
        return RedirectToAction(nameof(GetAll));
    }
    
    [HttpGet("[controller]/{bookId}/RemoveBook")]
    public IActionResult RemoveBook([FromRoute] int bookId)
    {
        var existingBook = myLibrary.Books.SingleOrDefault(book => book.BookId == bookId);
        if (existingBook == null){
            return NotFound();
        }
        myLibrary.Books.Remove(existingBook);
        myLibrary.SaveChanges();
        return RedirectToAction(nameof(GetAll));
    }

    public IActionResult AllLoans()
    {
        var loans = myLibrary.BooksOnLoan.ToList();
        var viewModel = new LoansViewModel
        {
            Loans = loans,
        };
        return View(viewModel);
    }

    [HttpPost("[controller]/{bookId}/Borrow")]
    public IActionResult BorrowBook([FromRoute] int bookId,[FromForm] int memberId)
    {
        var book = myLibrary.Books.SingleOrDefault(book => book.BookId == bookId);
            var newLoan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
            };
            if (book == null){
                return NotFound();
            }
            book.AvailableCopies -= 1;
            myLibrary.BooksOnLoan.Add(newLoan);
        myLibrary.SaveChanges();
        return RedirectToAction(nameof(AllLoans));
    }
    [HttpGet("[controller]/{loanId}/Return")]
    public IActionResult ReturnBook([FromRoute] int loanId)
    {
        var loan = myLibrary.BooksOnLoan.SingleOrDefault(loan => loan.LoanId == loanId);
        if (loan ==null)
        {
            return NotFound();
        }
        var book = loan.Book;
        myLibrary.BooksOnLoan.Remove(loan);
        book.AvailableCopies += 1;
        myLibrary.SaveChanges();
        return RedirectToAction(nameof(AllLoans));
    }
}
