namespace RazorLibrary.Domain.Adapters.Services.Book
{
    using RazorLibrary.Domain.DataTransferObject.Book;

    public interface IWriteBookService
    {
        public Task<ReadBookDto> Add(WriteBookDto bookDto);

        public Task<ReadBookDto> Edit(WriteBookDto bookDto);

        public Task Delete(string id);
    }
}
