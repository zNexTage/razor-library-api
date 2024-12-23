namespace RazorLibrary.Domain.Adapters.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
