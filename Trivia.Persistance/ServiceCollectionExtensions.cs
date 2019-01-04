using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trivia.Domain.Interfaces;

namespace Trivia.Persistance
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TriviaContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IReadRepository>(provider => provider.GetService<TriviaContext>());
            services.AddScoped<IWriteRepository>(provider => provider.GetService<TriviaContext>());

            return services;
        }
    }
}
