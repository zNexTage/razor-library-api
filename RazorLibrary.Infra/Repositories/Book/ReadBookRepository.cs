using Microsoft.EntityFrameworkCore;
using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Infra.Database;

namespace RazorLibrary.Infra.Repositories.Book
{
    public class ReadBookRepository : IReadBookRepository
    {
        private readonly BookContext _context;

        public ReadBookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Domain.Entities.Book> GetById(string id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id.ToString() == id);
        }
    }
}
