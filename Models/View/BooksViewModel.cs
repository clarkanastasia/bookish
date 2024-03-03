using Bookish.Models.Data;

namespace Bookish.Models.View;

public class BooksViewModel
{
    public required List<Book> Books { get; set; }
}