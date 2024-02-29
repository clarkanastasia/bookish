using Bookish.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Controllers;
public class BooksController : Controller 
{    
    private readonly Library myLibrary;

    Book book1 = new Book{
        BookId = 1,
        Title = "Harry Potter and The Goblet of Fire",
        Author = "J.K.Rowling",
        TotalCopies = 5,
        AvailableCopies = 5,
    };
    Book book2 = new Book{
        BookId = 2,
        Title = "Little Women",
        Author = "Louisa May Alcott",
        TotalCopies = 6,
        AvailableCopies = 6,
    };

    public BooksController(Library library)
    {
        myLibrary = library;
    }

    [HttpGet("[controller]/catalogue")]
    public IActionResult ListAll()
    {
        myLibrary.AddBook(book1);
        myLibrary.AddBook(book2);
        return View("~/Views/Book/Catalogue.cshtml", myLibrary);
    }

    [HttpGet("[controller]/catalogue/{bookId}")]
    public IActionResult List([FromRoute]int bookId)
    {
        var book = myLibrary.GetBookById(bookId);

        return View("~/Views/Book/BookDetails.cshtml", book);
    }

    [HttpGet("[controller]/AddBook")]
    public IActionResult AddBookForm()
    {
        return View("~/Views/Book/AddBook.cshtml");
    }

    [HttpPost("[controller]/Add")]
    public IActionResult AddBook([FromForm] string title, [FromForm] string author, [FromForm] int totalCopies)
    {
        var newBook = new Book{
            BookId = myLibrary.Books.Count + 1,
            Title = title, 
            Author = author,
            TotalCopies = totalCopies,
            AvailableCopies = totalCopies,
        };
        myLibrary.AddBook(newBook);
        return Redirect("catalogue");
    }

    [HttpGet("[controller]/{id}/EditBook")]
    public IActionResult EditBookForm([FromRoute] int id)
    {
        var existingBook = myLibrary.GetBookById(id);
        return View("~/Views/Book/EditBook.cshtml", existingBook);
    }

    [HttpPost("[controller]/{id}/Edit")]
    public IActionResult EditBook([FromRoute] int id, [FromForm] string title, [FromForm] string author, [FromForm] int totalCopies)
    {
        var existingBook = myLibrary.GetBookById(id);
        existingBook.Title = title;
        existingBook.Author = author;
        existingBook.TotalCopies = totalCopies;
        return Redirect("/");
    }
}
