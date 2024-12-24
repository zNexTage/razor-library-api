using Moq;
using RazorLibrary.Domain.Adapters.Repositories.Book;

namespace RazorLibrary.Tests.Commom.Mock.Repositories.Book
{
    using RazorLibrary.Domain.Entities;

    public static class WriteBookRepositoryBuilder
    {
        public static IWriteBookRepository BuildAdd()
        {
            var mock = new Mock<IWriteBookRepository>();

            mock.Setup(repo => repo.Add(It.IsAny<Book>()));

            return mock.Object;
        }
    }
}
