namespace RazorLibrary.Domain.Adapters.Services.Book
{
    using RazorLibrary.Domain.Entities;

    public interface IWriteBookService
    {
        public Task<Book> Add();

        public Task<Book> Edit(string id);

        public Task Delete(string id);
    }
}
