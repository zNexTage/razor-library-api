using RazorLibrary.Domain.Adapters.Repositories;
using RazorLibrary.Infra.Database;

namespace RazorLibrary.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookContext _bookContext;
        public UnitOfWork(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task CommitAsync()
        {
            await _bookContext.SaveChangesAsync();
        }
    }
}
