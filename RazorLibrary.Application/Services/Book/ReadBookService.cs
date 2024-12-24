using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Domain.Adapters.Services.Book;
using RazorLibrary.Domain.Exception;

namespace RazorLibrary.Application.Services.Book
{
    public class ReadBookService : IReadBookService
    {
        private readonly IReadBookRepository _readBookRepository;

        public ReadBookService(IReadBookRepository bookRepository)
        {
            _readBookRepository = bookRepository;
        }

        async Task<List<Domain.Entities.Book>> IReadBookService.GetAll()
        {
            var books = await _readBookRepository.GetAll();

            return books;
        }

        async Task<Domain.Entities.Book> IReadBookService.GetById(string id)
        {
            var book = await _readBookRepository.GetById(id);

            if (book is null) throw new NotFoundException("Livro não localizado");

            return book;
        }
    }
}
