using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLibrary.Infra.Database;

namespace RazorLibrary.Infra
{
    public static class InfraExtensions
    {

        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            AddSqlContext(services, configuration);
        }

        private static void AddSqlContext(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BookContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("SqlServer");

                opt.UseSqlServer(connectionString);
            });
        }
    }
}
