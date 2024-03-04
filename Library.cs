using Microsoft.EntityFrameworkCore;
using Bookish.Models;

namespace Bookish;

public class Library : DbContext
{
    public DbSet<Book> Books {get;set;}= null!;
    public DbSet<Member> Members {get;set;} = null!;
    public DbSet<Loan> BooksOnLoan {get;set;} = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=bookish; Username=bookish; Password=bookish;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var book1 = new Book
        {
            BookId = -1,
            Title = "Harry Potter and The Goblet of Fire",
            Author = "J.K.Rowling",
            TotalCopies = 5,
            AvailableCopies = 5,
        };
        var book2 = new Book
        {
            BookId = -2,
            Title = "Little Women",
            Author = "Louisa May Alcott",
            TotalCopies = 6,
            AvailableCopies = 6,
        };
        modelBuilder.Entity<Book>().HasData(book1);
        modelBuilder.Entity<Book>().HasData(book2);

        var member1 = new Member
        {
            MemberId = -1,
            Name = "Jane Smith",
            MembershipNo = "123456"
        };
        modelBuilder.Entity<Member>().HasData(member1);
    }
}
