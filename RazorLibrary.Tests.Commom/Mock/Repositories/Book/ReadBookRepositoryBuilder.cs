using Moq;
using RazorLibrary.Domain.Adapters.Repositories.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorLibrary.Tests.Commom.Mock.Repositories.Book
{
    public class ReadBookRepositoryBuilder
    {
        public static IReadBookRepository Build(bool existsReturn = true)
        {
            var mock = new Mock<IReadBookRepository>();

            mock.Setup(m => m.Exists(It.IsAny<string>())).ReturnsAsync(existsReturn);

            return mock.Object;
        }
    }
}
