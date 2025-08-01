using FluentValidation;
using Library.Aplication.DTOs.Authors;
using Library.Aplication.Interfaces;
using Library.Infrastructure.Configurations;
using Library.Infrastructure.Data;
using Library.Infrastructure.ExternaServices;
using Library.Infrastructure.Implementations;
using Library.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("lib_db"));
            });
            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthorMappingProfile).Assembly);
            return services;
        }

        public static IServiceCollection ConfigureValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();
            return services;
        }

        public static IServiceCollection ConfigureAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGoogleService, GoogleService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.Configure<GoogleConfigurations>(configuration.GetSection(GoogleConfigurations.SectionName));
            services.AddHttpClient<IGoogleService, GoogleService>(client =>
            {
                client.BaseAddress = new Uri(configuration[$"{GoogleConfigurations.SectionName}:TokenUrl"]);
            });
            return services;
        }
    }
}
