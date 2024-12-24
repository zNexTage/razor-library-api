namespace RazorLibrary.Domain.Adapters.Services.Book
{
    using RazorLibrary.Domain.Entities;

    public interface IReadBookService
    {
        public Task<List<Book>> GetAll();
        public Task<Book> GetById(string id);
    }
}
