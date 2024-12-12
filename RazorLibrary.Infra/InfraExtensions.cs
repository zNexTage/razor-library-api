using FluentMigrator.Runner;
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

            AddFluentMigration(services, configuration);

            MigrateDatabase(services);
        }

        private static void AddSqlContext(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BookContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("SqlServer");

                opt.UseSqlServer(connectionString);
            });
        }

        private static void AddFluentMigration(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            service.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(InfraExtensions).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);                
        }

        private static void MigrateDatabase(IServiceCollection service)
        {
            var provider = service.BuildServiceProvider();

            using(var scope = provider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                runner.MigrateUp();
            }
        }
    }
}
