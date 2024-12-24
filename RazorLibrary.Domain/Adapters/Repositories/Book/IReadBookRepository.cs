namespace RazorLibrary.Domain.Adapters.Repositories.Book
{
    using RazorLibrary.Domain.Entities;

    public interface IReadBookRepository
    {
        public Task<List<Book>> GetAll();

        public Task<Book> GetById(string id);    
        
        public Task<bool> Exists(string id);
    }
}
