namespace RazorLibrary.Domain.Adapters.Repositories.Book
{
    using RazorLibrary.Domain.Entities;

    public interface IWriteBookRepository
    {
        public Task<Book> Add();

        public Task<Book> Edit();

        public Task Delete();
    }
}
