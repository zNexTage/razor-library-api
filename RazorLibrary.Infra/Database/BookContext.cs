using Microsoft.EntityFrameworkCore;
using RazorLibrary.Domain.Entities;

namespace RazorLibrary.Infra.Database
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)  : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}
