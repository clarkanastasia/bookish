using Bookish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;
public class BooksController : Controller 
{    
    private readonly Library myLibrary;

    public BooksController(Library library)
    {
        myLibrary = library;
    }

    [HttpGet("[controller]/catalogue")]
    public IActionResult ListAll()
    {
        var books = myLibrary.Books.ToList();
        
        var viewModel = new BooksView
        {
            Books = books,
        };
        return View(viewModel);
    }

    [HttpGet("[controller]/catalogue/{bookId}")]
    public IActionResult List([FromRoute]int bookId)
    {
        var book = myLibrary.Books.FirstOrDefault(book => book.BookId == bookId);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
    [HttpGet("[controller]/AddBook")]
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
        return RedirectToAction("ListAll");
    }

    [HttpGet("[controller]/{id}/EditBook")]
    public IActionResult EditBook([FromRoute] int id)
    {
        var existingBook = myLibrary.Books.FirstOrDefault(book => book.BookId == id);
        if (existingBook == null)
        {
            return NotFound();
        }
        return View(existingBook);
    }

    [HttpPost("[controller]/{id}/Edit")]
    public IActionResult EditBook([FromRoute] int id, [FromForm] string title, [FromForm] string author, [FromForm] int totalCopies)
    {
        var existingBook = myLibrary.Books.FirstOrDefault(book => book.BookId == id);
        if(existingBook == null)
        {
            return NotFound();
        }
        var currentLoans = existingBook.TotalCopies-existingBook.AvailableCopies;
        int oldTotalCopies = existingBook.TotalCopies;
        if (existingBook == null)
        {
            return NotFound();
        }
        if(totalCopies<currentLoans)
        {
            existingBook.TotalCopies = currentLoans;
            existingBook.AvailableCopies = 0;
        } 
        else
        {
            existingBook.TotalCopies = totalCopies;
            existingBook.AvailableCopies = totalCopies - currentLoans;
        }
        
        existingBook.Title = title;
        existingBook.Author = author;
        myLibrary.SaveChanges();
        return RedirectToAction("ListAll");
    }

    [HttpGet("[controller]/{bookId}/DeleteBook")]
    public IActionResult DeleteBook([FromRoute] int bookId)
    {
        var existingBook = myLibrary.Books.FirstOrDefault(book => book.BookId == bookId);
        if (existingBook == null)
        {
            return NotFound();
        }
        myLibrary.Books.Remove(existingBook);
        myLibrary.SaveChanges();
        return RedirectToAction("ListAll");
    }
    public IActionResult AllLoans()
    {
        var loans = myLibrary.BooksOnLoan.Include(loan => loan.Book)
                    .Include(loan => loan.Member)
                    .ThenInclude(member => member.Loans)
                    .ToList();
        
        var viewModel = new LoansView
        {
            Loans = loans,        
        };
        return View(viewModel);
    }

    [HttpPost("[controller]/{bookId}/Borrow")]
    public IActionResult BorrowBook([FromRoute] int bookId, [FromForm] int memberId)    
    {
        var existingBook = myLibrary.Books.FirstOrDefault(book => book.BookId == bookId);
        var newLoan = new Loan()
        {
            BookId = bookId,
            MemberId = memberId,
        };
        if (existingBook == null)
        {
            return NotFound();
        }
        existingBook.AvailableCopies -=1;
        myLibrary.BooksOnLoan.Add(newLoan);
        myLibrary.SaveChanges();
        return RedirectToAction("AllLoans");
    }
    
    [HttpGet("[controller]/{loanId}/Return")]
    public IActionResult ReturnBook([FromRoute] int loanId)    
    {
        var existingLoan = myLibrary.BooksOnLoan.FirstOrDefault(loan => loan.LoanId == loanId);
        if (existingLoan == null)
        {
            return NotFound();
        }
        existingLoan.Book.AvailableCopies += 1;
        existingLoan.DateReturned = DateOnly.FromDateTime(DateTime.Today);
        myLibrary.SaveChanges();
        return RedirectToAction("AllLoans");
    }
}
