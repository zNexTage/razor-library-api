using RazorLibrary.Domain.DataTransferObject.Book;

namespace RazorLibrary.Domain.Adapters.Services.Book
{

    public interface IReadBookService
    {
        public Task<List<ReadBookDto>> GetAll();
        public Task<ReadBookDto> GetById(string id);
    }
}
