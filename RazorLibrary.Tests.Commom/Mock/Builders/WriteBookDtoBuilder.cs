using Bogus;
using RazorLibrary.Domain.DataTransferObject.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
