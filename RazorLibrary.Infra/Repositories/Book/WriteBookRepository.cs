using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
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

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Book> Edit(Domain.Entities.Book book)
        {
            throw new NotImplementedException();
        }
    }
}
