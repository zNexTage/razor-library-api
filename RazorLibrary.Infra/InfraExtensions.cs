using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLibrary.Domain.Adapters.Repositories;
using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Infra.Database;
using RazorLibrary.Infra.Repositories;
using RazorLibrary.Infra.Repositories.Book;

namespace RazorLibrary.Infra
{
    public static class InfraExtensions
    {

        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            AddSqlContext(services, configuration);

            AddFluentMigration(services, configuration);

            MigrateDatabase(services);

            AddRepositories(services);
            
            AddUnitOfWork(services);
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


        private static void AddRepositories(IServiceCollection service)
        {
            service.AddScoped<IWriteBookRepository, WriteBookRepository>();
            service.AddScoped<IReadBookRepository, ReadBookRepository>();
        }

        private static void AddUnitOfWork(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
