using Bogus;
using RazorLibrary.Domain.DataTransferObject.Book;

namespace RazorLibrary.Tests.Commom.Mock.Builders
{
    public static class WriteBookDtoBuilder
    {
        public static WriteBookDto Build()
        {
            var faker = new Faker();

            return new WriteBookDto()
            {
                Authors = {
                    faker.Person.FullName
                },
                Photo = faker.Image.PicsumUrl(),
                Publisher = faker.Company.CompanyName(),
                Title = faker.Commerce.ProductName()
            };
        }
    }
}
