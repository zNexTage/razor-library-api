using Bogus;
using RazorLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorLibrary.Tests.Commom.Mock.Builders
{
    public class BookBuilder
    {
        public static Book Build()
        {
            var id = Guid.NewGuid();

            var faker = new Faker();

            return new Book()
            {
                Id = Guid.NewGuid(),
                Authors = faker.Person.FullName,
                Photo = faker.Image.PicsumUrl(),
                Publisher = faker.Company.CompanyName(),
                Title = faker.Commerce.ProductName()
            };
        }
    }
}
