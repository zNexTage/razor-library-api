using RazorLibrary.Application.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Tests.Commom.Mock.Builders;
using RazorLibrary.Tests.Commom.Mock.Repositories.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorLibrary.Tests.Application.Services
{
    public class WriteBookServiceTest
    {
        [Fact]
        public async void Add_WithValidValues_NewBook()
        {
            //Arrange
            var book = new WriteBookDto()
            {
                Authors = {
                    "Arthur Conan Doyle"
                },
                Photo = "https://m.media-amazon.com/images/I/71kaG9KFfhL._SL1500_.jpg",
                Publisher = "Harper Collins",
                Title = "Box Sherlock Holmes - Obra completa"
            };

            var unit = UnitOfWorkBuilder.Build();
            var repo = WriteBookRepositoryBuilder.BuildAdd();

            //Act 
            var result = await new WriteBookService(repo, unit).Add(book);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Title, book.Title);
            Assert.Equal(result.Photo, book.Photo);
            Assert.Equal(result.Publisher, book.Publisher);
            Assert.Equal(result.Authors, book.Authors);
        }
    }
}
