using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace LibraryManager.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services
                .AddDatabaseServices(configuration, hostEnvironment)
                .AddMediatRServices();

            return services;
        }

        private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {

            services.AddDbContext<LibraryContext>((provider, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSQLWrite"), options =>
                {
                    options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                });

                options.EnableSensitiveDataLogging(hostEnvironment.IsDevelopment());
                options.EnableDetailedErrors(hostEnvironment.IsDevelopment());
            });

            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IBookAuthorRepository, BookAuthorRepository>();
            services.AddTransient<IBookSubjectRepository, BookSubjectRepository>();          

            return services;
        }

        private static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            return services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.Load("LibraryManager.Application"));
            });
        }
    }
}