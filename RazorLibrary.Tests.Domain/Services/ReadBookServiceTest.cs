using RazorLibrary.Application.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Domain.Exception;
using RazorLibrary.Tests.Commom.Mock.Builders;
using RazorLibrary.Tests.Commom.Mock.Mapper;
using RazorLibrary.Tests.Commom.Mock.Repositories.Book;

namespace RazorLibrary.Tests.Application.Services
{
    public class ReadBookServiceTest
    {
        [Fact]
        public async void GetById_WithInvalidId_ThrowsException()
        {
            //Arrange
            var rRepo = ReadBookRepositoryBuilder.Build(existsReturn: false);
            var mapper = MapperBuilder.Build();

            //Act
            var readBookService = new ReadBookService(rRepo, mapper);
            var result = await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await readBookService.GetById("fake_id");
            });

            //Assert

            Assert.NotNull(result);
            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Livro não localizado", result.Message);
        }

        [Fact]
        public async void GetById_WithValidId_ReturnsBook()
        {
            //Arrange
            var book = BookBuilder.Build();
            var rRepo = ReadBookRepositoryBuilder.Build(book.Id.ToString(), book);
            var mapper = MapperBuilder.Build();

            //Act
            var readBookService = new ReadBookService(rRepo, mapper);
            var result = await readBookService.GetById(book.Id.ToString());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, book.Id.ToString());
            Assert.Equal(result.Title, book.Title);
            Assert.Equal(result.Publisher, book.Publisher);
            Assert.Equal(result.Authors, book.Authors.Split(", ").ToList());
            Assert.Equal(result.Photo, book.Photo);
        }
    }
}
