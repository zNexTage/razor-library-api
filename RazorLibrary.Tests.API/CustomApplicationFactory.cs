using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorLibrary.Domain.Entities;
using RazorLibrary.Infra.Database;
using RazorLibrary.Tests.Commom.Seeds;

namespace RazorLibrary.Tests.API
{
    
    public class CustomApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Test")
                .ConfigureServices(services =>
            {
                var  dbContextDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<BookContext>));

                if(dbContextDescriptor is not null)
                {
                    services.Remove(dbContextDescriptor);
                }

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<BookContext>(opt =>
                {
                    opt.UseInMemoryDatabase("InMemoryDbForTesting");
                    opt.UseInternalServiceProvider(provider);
                });

                services.AddScoped<SeedBook>();

                var serviceProvider = services.BuildServiceProvider();

                var seedBook = serviceProvider.GetRequiredService<SeedBook>();

                seedBook.SeedData().Wait();
            });
        }
    }
}
