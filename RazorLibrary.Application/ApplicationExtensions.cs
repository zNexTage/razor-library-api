using Microsoft.Extensions.DependencyInjection;
using RazorLibrary.Application.Services.Book;
using RazorLibrary.Domain.Adapters.Services.Book;
using System.Reflection;

namespace RazorLibrary.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddServices(service);
            AddMapper(service);
        }

        private static void AddServices(IServiceCollection service)
        {
            service.AddScoped<IWriteBookService, WriteBookService>();
            service.AddScoped<IReadBookService, ReadBookService>();
        }

        private static void AddMapper(IServiceCollection service)
        {
            var assembly = typeof(ApplicationExtensions).Assembly;

            service.AddAutoMapper(assembly);
        }
    }
}
