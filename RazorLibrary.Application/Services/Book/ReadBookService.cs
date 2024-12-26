using AutoMapper;
using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Domain.Adapters.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Domain.Exception;

namespace RazorLibrary.Application.Services.Book
{
    public class ReadBookService : IReadBookService
    {
        private readonly IReadBookRepository _readBookRepository;
        private readonly IMapper _mapper;

        public ReadBookService(IReadBookRepository bookRepository, IMapper mapper)
        {
            _readBookRepository = bookRepository;
            _mapper = mapper;
        }

        async Task<List<ReadBookDto>> IReadBookService.GetAll()
        {
            var books = await _readBookRepository.GetAll();

            return _mapper.Map<List<ReadBookDto>>(books);
        }

        async Task<ReadBookDto> IReadBookService.GetById(string id)
        {
            var book = await _readBookRepository.GetById(id);

            if (book is null) throw new NotFoundException("Livro não localizado");

            return _mapper.Map<ReadBookDto>(book);
        }
    }
}
