using RazorLibrary.Application.Services.Book;
using RazorLibrary.Domain.Exception;
using RazorLibrary.Tests.Commom.Mock.Builders;
using RazorLibrary.Tests.Commom.Mock.Mapper;
using RazorLibrary.Tests.Commom.Mock.Repositories.Book;

namespace RazorLibrary.Tests.Application.Services
{
    public class WriteBookServiceTest
    {
        [Fact]
        public async void Add_WithValidValues_NewBook()
        {
            //Arrange
            var book = WriteBookDtoBuilder.Build();

            var unit = UnitOfWorkBuilder.Build();
            var wRepo = WriteBookRepositoryBuilder.BuildAdd();
            var rRepo = ReadBookRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();

            //Act 
            var result = await new WriteBookService(wRepo, rRepo, unit, mapper).Add(book);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Title, book.Title);
            Assert.Equal(result.Photo, book.Photo);
            Assert.Equal(result.Publisher, book.Publisher);
            Assert.Equal(result.Authors, book.Authors);
        }

        [Fact]
        public async void Add_WithEmptyTitle_ThrowsException()
        {
            //Arrange
            var book = WriteBookDtoBuilder.Build();
            book.Title = string.Empty;

            var unit = UnitOfWorkBuilder.Build();
            var wRepo = WriteBookRepositoryBuilder.BuildAdd();
            var rRepo = ReadBookRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();

            //Act
            var result = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new WriteBookService(wRepo, rRepo, unit, mapper).Add(book);
            });

            //Assert
            Assert.Single(result.ErrorMessages);

            var error = result.ErrorMessages.TryGetValue("Title", out string message);

            Assert.Equal("Por favor, informe o título do livro", message);
        }


        [Fact]
        public async void Add_WithEmptyPhoto_ThrowsException()
        {
            //Arrange
            var book = WriteBookDtoBuilder.Build();
            book.Photo = string.Empty;

            var unit = UnitOfWorkBuilder.Build();
            var wRepo = WriteBookRepositoryBuilder.BuildAdd();
            var rRepo = ReadBookRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();

            //Act
            var result = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new WriteBookService(wRepo, rRepo, unit, mapper).Add(book);
            });

            //Assert
            Assert.Single(result.ErrorMessages);

            var error = result.ErrorMessages.TryGetValue("Photo", out string message);

            Assert.Equal("Por favor, informe a foto do livro", message);
        }

        [Fact]
        public async void Add_WithEmptyPublisher_ThrowsException()
        {
            //Arrange
            var book = WriteBookDtoBuilder.Build();
            book.Publisher = string.Empty;

            var unit = UnitOfWorkBuilder.Build();
            var wRepo = WriteBookRepositoryBuilder.BuildAdd();
            var rRepo = ReadBookRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();

            //Act
            var result = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new WriteBookService(wRepo, rRepo, unit, mapper).Add(book);
            });

            //Assert
            Assert.Single(result.ErrorMessages);

            result.ErrorMessages.TryGetValue("Publisher", out string message);

            Assert.Equal("Por favor, informe a editora", message);
        }

        [Fact]
        public async void Add_WithEmptyAuthorsList_ThrowsException()
        {
            //Arrange
            var book = WriteBookDtoBuilder.Build();
            book.Authors = [];

            var unit = UnitOfWorkBuilder.Build();
            var wRepo = WriteBookRepositoryBuilder.BuildAdd();
            var rRepo = ReadBookRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();

            //Act
            var result = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new WriteBookService(wRepo, rRepo, unit, mapper).Add(book);
            });

            //Assert
            Assert.Single(result.ErrorMessages);

            result.ErrorMessages.TryGetValue("Authors", out string message);

            Assert.Equal("Por favor, informe pelo menos 1 autor.", message);
        }


        [Fact]
        public async void Add_WithEmptyAuthorName_ThrowsException()
        {
            //Arrange
            var book = WriteBookDtoBuilder.Build();
            book.Authors = [""];

            var unit = UnitOfWorkBuilder.Build();
            var wRepo = WriteBookRepositoryBuilder.BuildAdd();
            var rRepo = ReadBookRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();

            //Act
            var result = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new WriteBookService(wRepo, rRepo, unit, mapper).Add(book);
            });

            //Assert
            Assert.Single(result.ErrorMessages);

            result.ErrorMessages.TryGetValue("Authors[0]", out string message);

            Assert.Equal("Por favor, informe o nome do autor", message);
        }
    }
}
