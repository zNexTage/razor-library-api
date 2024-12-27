using RazorLibrary.Tests.Commom.Mock.Builders;
using System.Net.Http.Json;
using System.Text.Json;

namespace RazorLibrary.Tests.API.Controllers
{
    public class BookControllerTest : IClassFixture<CustomApplicationFactory>
    {
        private readonly HttpClient _client;

        public BookControllerTest(CustomApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void Post_WithValidData_ReturnsCreated()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            await using var bookResp = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(bookResp);

            Assert.NotNull(respData);

            var bookJson = respData.RootElement;

            Assert.NotNull(bookJson.GetProperty("id").GetString());
            Assert.Equal(bookJson.GetProperty("title").GetString(), bookDto.Title);
            Assert.Equal(bookJson.GetProperty("publisher").GetString(), bookDto.Publisher);
            Assert.Equal(bookJson.GetProperty("photo").GetString(), bookDto.Photo);

            var authors = JsonSerializer.Deserialize<List<string>>(bookJson.GetProperty("authors"));            
            Assert.Equal(authors, bookDto.Authors);
        }

        [Fact]
        public async void Post_WithInvalidTitle_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Title = string.Empty;

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Title"));
            
            Assert.Equal("Por favor, informe o título do livro", errors["Title"]);
        }

        [Fact]
        public async void Post_WithInvalidTitleLength_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Title = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Title"));

            Assert.Equal("O título deve ter no máximo 70 caracteres", errors["Title"]);
        }

        [Fact]
        public async void Post_WithInvalidPublisher_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Publisher = string.Empty;

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Publisher"));

            Assert.Equal("Por favor, informe a editora", errors["Publisher"]);
        }

        [Fact]
        public async void Post_WithInvalidPublisherLength_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Publisher = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Publisher"));

            Assert.Equal("A editora deve ter no máximo 70 caracteres", errors["Publisher"]);
        }

        [Fact]
        public async void Post_WithInvalidPhoto_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Photo = string.Empty;

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Photo"));

            Assert.Equal("Por favor, informe a foto do livro", errors["Photo"]);
        }

        [Fact]
        public async void Post_WithEmptyAuthors_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Authors = [];

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Authors"));

            Assert.Equal("Por favor, informe pelo menos 1 autor.", errors["Authors"]);
        }

        [Fact]
        public async void Post_WithInvalidLenghtAuthorName_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Authors = ["Pedro de Alcântara João Carlos Leopoldo Salvador Bibiano Francisco Xavier de Paula Leocádio Miguel Gabriel Rafael Gonzaga"];

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Authors[0]"));

            Assert.Equal("O nome do autor deve ter no máximo 70 caracteres", errors["Authors[0]"]);
        }

        [Fact]
        public async void Post_WithAuthorBlankName_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Authors = [string.Empty];

            // Act
            var response = await _client.PostAsJsonAsync("api/Book", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Authors[0]"));

            Assert.Equal("Por favor, informe o nome do autor", errors["Authors[0]"]);
        }

        [Fact]
        public async void Delete_WithValidId_ReturnsOk() {
            var id = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var response = await _client.DeleteAsync($"api/Book/{id}");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Delete_WithNotExistsId_ReturnsNotFound()
        {
            var id = Guid.Parse("21111111-1111-1111-1111-111111111111");

            var response = await _client.DeleteAsync($"api/Book/{id}");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Put_WithValidData_ReturnsCreated()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}/", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            await using var bookResp = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(bookResp);

            Assert.NotNull(respData);

            var bookJson = respData.RootElement;

            Assert.Equal(bookJson.GetProperty("id").GetString(), id.ToString());
            Assert.Equal(bookJson.GetProperty("title").GetString(), bookDto.Title);
            Assert.Equal(bookJson.GetProperty("publisher").GetString(), bookDto.Publisher);
            Assert.Equal(bookJson.GetProperty("photo").GetString(), bookDto.Photo);

            var authors = JsonSerializer.Deserialize<List<string>>(bookJson.GetProperty("authors"));
            Assert.Equal(authors, bookDto.Authors);
        }

        [Fact]
        public async void Put_WithInvalidTitle_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Title = string.Empty;
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}/", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Title"));

            Assert.Equal("Por favor, informe o título do livro", errors["Title"]);
        }

        [Fact]
        public async void Put_WithInvalidTitleLength_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Title = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Title"));

            Assert.Equal("O título deve ter no máximo 70 caracteres", errors["Title"]);
        }

        [Fact]
        public async void Put_WithInvalidPublisher_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Publisher = string.Empty;
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Publisher"));

            Assert.Equal("Por favor, informe a editora", errors["Publisher"]);
        }

        [Fact]
        public async void Put_WithInvalidPublisherLength_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Publisher = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Publisher"));

            Assert.Equal("A editora deve ter no máximo 70 caracteres", errors["Publisher"]);
        }

        [Fact]
        public async void Put_WithInvalidPhoto_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Photo = string.Empty;
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Photo"));

            Assert.Equal("Por favor, informe a foto do livro", errors["Photo"]);
        }

        [Fact]
        public async void Put_WithEmptyAuthors_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Authors = [];
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Authors"));

            Assert.Equal("Por favor, informe pelo menos 1 autor.", errors["Authors"]);
        }

        [Fact]
        public async void Put_WithInvalidLenghtAuthorName_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Authors = ["Pedro de Alcântara João Carlos Leopoldo Salvador Bibiano Francisco Xavier de Paula Leocádio Miguel Gabriel Rafael Gonzaga"];
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Authors[0]"));

            Assert.Equal("O nome do autor deve ter no máximo 70 caracteres", errors["Authors[0]"]);
        }

        [Fact]
        public async void Put_WithAuthorBlankName_ReturnsBadRequest()
        {
            // Arrange
            var bookDto = WriteBookDtoBuilder.Build();
            bookDto.Authors = [string.Empty];
            var id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            // Act
            var response = await _client.PutAsJsonAsync($"api/Book/{id}", bookDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            await using var content = await response.Content.ReadAsStreamAsync();

            var respData = await JsonDocument.ParseAsync(content);

            var errors = respData.RootElement.Deserialize<Dictionary<string, string>>();

            Assert.NotNull(errors);
            Assert.Single(errors);

            Assert.True(errors.ContainsKey("Authors[0]"));

            Assert.Equal("Por favor, informe o nome do autor", errors["Authors[0]"]);
        }

    }
}
