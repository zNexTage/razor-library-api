using Bogus;
using RazorLibrary.Domain.DataTransferObject.Book;

namespace RazorLibrary.Tests.Commom.Mock.Builders
{
    class ReadBookDtoBuilder
    {
        public static ReadBookDto Build()
        {
            var faker = new Faker();

            var readBookDto = new ReadBookDto()
            {
                Id = new Guid().ToString(),
                Authors = {
                    faker.Person.FullName
                },
                Photo = faker.Image.PicsumUrl(),
                Publisher = faker.Company.CompanyName(),
                Title = faker.Commerce.ProductName()
            };

            return readBookDto;
        }
    }
}
