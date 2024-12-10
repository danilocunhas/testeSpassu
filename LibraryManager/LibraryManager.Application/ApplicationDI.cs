using LibraryManager.Application.Factories;
using LibraryManager.Application.Queries.Author;
using LibraryManager.Application.Queries.Book;
using LibraryManager.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryManager.Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services
                .AddQueryHandlers(configuration, hostEnvironment)
                .AddDBConnectionFactory(configuration, hostEnvironment);

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services.AddTransient<IBookQueryHandler, BookQueryHandler>();
            services.AddTransient<IAuthorQueryHandler, AuthorQueryHandler>();

            return services;
        }

        private static IServiceCollection AddDBConnectionFactory(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services.AddSingleton<DatabaseConnectionFactory>(provider
         => new(configuration.GetConnectionString("MSSQLWrite")!));

            return services;
        }

    }
}