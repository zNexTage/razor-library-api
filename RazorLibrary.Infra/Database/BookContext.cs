using Microsoft.EntityFrameworkCore;

namespace RazorLibrary.Infra.Database
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) 
        {
            
        }
    }
}
