using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Domain.Interfaces;

namespace Schedule.Persistance
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ScheduleContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IRepository>(provider => provider.GetService<ScheduleContext>());

            return services;
        }
    }
}
