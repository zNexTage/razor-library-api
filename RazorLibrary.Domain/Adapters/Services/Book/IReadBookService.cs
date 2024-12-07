namespace RazorLibrary.Domain.Adapters.Services.Book
{
    using RazorLibrary.Domain.Entities;

    public interface IReadBookService
    {
        public List<Book> GetAll();
        public Book GetById(string id);
    }
}
