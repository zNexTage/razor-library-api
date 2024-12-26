using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Infra.Database;

namespace RazorLibrary.Infra.Repositories.Book
{
    public class WriteBookRepository : IWriteBookRepository
    {
        private readonly BookContext _context;

        public WriteBookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Book> Add(Domain.Entities.Book book)
        {
            await _context.Books.AddAsync(book);

            return book;
        }

        public async Task Delete(string id)
        {
            // Apenas converte o id, que é uma string, para guid.           

            _context.Books.Remove(new Domain.Entities.Book() { Id = new Guid(id)});
        }

        public async Task<Domain.Entities.Book> Edit(Domain.Entities.Book book)
        {
            _context.Attach(book);
            _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return book;
        }       
    }
}
