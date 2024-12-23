namespace RazorLibrary.Domain.Adapters.Repositories.Book
{
    using RazorLibrary.Domain.DataTransferObject.Book;
    using RazorLibrary.Domain.Entities;

    public interface IWriteBookRepository
    {
        public Task<Book> Add(Book book);

        public Task<Book> Edit(Book book);

        public Task Delete(string id);
    }
}
