using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using BookShelf.Models;

namespace BookShelf.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}
