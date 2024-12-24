using Microsoft.Extensions.DependencyInjection;
using RazorLibrary.Application.Services.Book;
using RazorLibrary.Domain.Adapters.Services.Book;

namespace RazorLibrary.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddServices(service);
        }

        private static void AddServices(IServiceCollection service)
        {
            service.AddScoped<IWriteBookService, WriteBookService>();
        }
    }
}
