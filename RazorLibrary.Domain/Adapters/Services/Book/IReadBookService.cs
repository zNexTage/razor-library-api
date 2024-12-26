namespace RazorLibrary.Domain.Adapters.Services.Book
{
    using RazorLibrary.Domain.DataTransferObject.Book;

    public interface IReadBookService
    {
        public Task<List<ReadBookDto>> GetAll();
        public Task<ReadBookDto> GetById(string id);
    }
}
